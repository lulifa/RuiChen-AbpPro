using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using RuiChen.AbpPro.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Clients;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Users;
using Volo.Abp.Validation;
using IIdentityUserRepository = RuiChen.AbpPro.Identity.IIdentityUserRepository;

namespace RuiChen.AbpPro.Account
{
    public class AccountAppService : AccountApplicationServiceBase, IAccountAppService
    {
        private readonly ITotpService totpService;
        private readonly IIdentityUserRepository userRepository;
        private readonly IAccountSmsSecurityCodeSender securityCodeSender;
        private readonly IDistributedCache<SecurityTokenCacheItem> securityTokenCache;
        private readonly IdentitySecurityLogManager identitySecurityLogManager;

        public AccountAppService(ITotpService totpService, IIdentityUserRepository userRepository, IAccountSmsSecurityCodeSender securityCodeSender, IDistributedCache<SecurityTokenCacheItem> securityTokenCache, IdentitySecurityLogManager identitySecurityLogManager)
        {
            this.totpService = totpService;
            this.userRepository = userRepository;
            this.securityCodeSender = securityCodeSender;
            this.securityTokenCache = securityTokenCache;
            this.identitySecurityLogManager = identitySecurityLogManager;
        }

        /// <summary>
        /// 获取用户二次认证提供者列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<ListResultDto<NameValue>> GetTwoFactorProvidersAsync(GetTwoFactorProvidersInput input)
        {
            var user = await UserManager.GetByIdAsync(input.UserId);

            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(user);

            var items = userFactors.Select(key => new NameValue(L[$"TwoFactor:{key}"].Value, key)).ToList();

            return new ListResultDto<NameValue>(items);

        }

        /// <summary>
        /// 通过手机号注册用户账户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async virtual Task RegisterPhoneAsync(PhoneRegisterDto input)
        {

            await CheckSelfRegistrationAsync();

            await IdentityOptions.SetAsync();

            await CheckNewUserPhoneNumberNotBeUsedAsync(input.PhoneNumber);

            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.PhoneNumber, "SmsVerifyCode");

            var securityTokenCacheItem = await securityTokenCache.GetAsync(securityTokenCacheKey);

            if (securityTokenCacheItem == null)
            {
                // 验证码过期
                throw new UserFriendlyException(L["InvalidVerifyCode"]);
            }

            // 验证码是否有效
            if (input.Code.Equals(securityTokenCacheItem.Token) && int.TryParse(input.Code, out int token))
            {
                var securityToken = Encoding.Unicode.GetBytes(securityTokenCacheItem.SecurityToken);

                // 校验totp验证码
                if (totpService.ValidateCode(securityToken, token, securityTokenCacheKey))
                {
                    var userEmail = input.EmailAddress ?? $"{input.PhoneNumber}@{CurrentTenant.Name ?? "default"}.io";//如果邮件地址不验证,随意写入一个

                    var userName = input.UserName ?? input.PhoneNumber;

                    var user = new IdentityUser(GuidGenerator.Create(), userName, userEmail, CurrentTenant.Id)
                    {
                        Name = input.Name ?? input.PhoneNumber
                    };

                    await UserStore.SetPhoneNumberAsync(user, input.PhoneNumber);

                    await UserStore.SetPhoneNumberConfirmedAsync(user, true);

                    (await UserManager.CreateAsync(user, input.Password)).CheckErrors();

                    (await UserManager.AddDefaultRolesAsync(user)).CheckErrors();

                    await securityTokenCache.RemoveAsync(securityTokenCacheKey);

                    await identitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
                    {
                        Action = "PhoneNumberRegister",
                        ClientId = await FindClientIdAsync(),
                        Identity = "Account",
                        UserName = user.UserName
                    });

                    await CurrentUnitOfWork.SaveChangesAsync();

                    return;

                }
            }

            // 验证码无效
            throw new UserFriendlyException(L["InvalidVerifyCode"]);

        }

        /// <summary>
        /// 通过手机号重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        /// <exception cref="BusinessException"></exception>
        public async virtual Task ResetPasswordAsync(PhoneResetPasswordDto input)
        {
            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.PhoneNumber, "SmsVerifyCode");

            var securityTokenCacheItem = await securityTokenCache.GetAsync(securityTokenCacheKey);

            if (securityTokenCacheItem == null)
            {
                // 验证码过期
                throw new UserFriendlyException(L["InvalidVerifyCode"]);
            }

            await IdentityOptions.SetAsync();

            // 传递 isConfirmed 用户必须是已确认过手机号的
            var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);

            // 外部认证用户不允许修改密码
            if (user.IsExternal)
            {
                throw new BusinessException(code: Volo.Abp.Identity.IdentityErrorCodes.ExternalUserPasswordChange);
            }

            // 验证二次认证码
            if (!await UserManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, input.Code))
            {
                // 验证码无效
                throw new UserFriendlyException(L["InvalidVerifyCode"]);
            }

            // 生成真正的重置密码Token
            var resetPwdToken = await UserManager.GeneratePasswordResetTokenAsync(user);

            // 重置密码
            (await UserManager.ResetPasswordAsync(user, resetPwdToken, input.NewPassword)).CheckErrors();

            // 移除缓存项
            await securityTokenCache.RemoveAsync(securityTokenCacheKey);

            await identitySecurityLogManager.SaveAsync(
                new IdentitySecurityLogContext
                {
                    Action = "ResetPassword",
                    ClientId = await FindClientIdAsync(),
                    Identity = "Account",
                    UserName = user.UserName
                });

            await CurrentUnitOfWork.SaveChangesAsync();

        }

        /// <summary>
        /// 发送邮件登录验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async virtual Task SendEmailSigninCodeAsync(SendEmailSigninCodeDto input)
        {
            var user = await UserManager.FindByEmailAsync(input.EmailAddress);

            if (user == null)
            {
                throw new UserFriendlyException(L["UserNotRegisterd"]);
            }

            if (!user.EmailConfirmed)
            {
                throw new UserFriendlyException(L["UserEmailNotConfirmed"]);
            }

            var code = await UserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

            await AccountEmailVerifySender.SendMailLoginVerifyCodeAsync(code, user.UserName, user.Email);

        }

        /// <summary>
        /// 发送手机注册验证码短信
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async virtual Task SendPhoneRegisterCodeAsync(SendPhoneRegisterCodeDto input)
        {
            await CheckSelfRegistrationAsync();

            await CheckNewUserPhoneNumberNotBeUsedAsync(input.PhoneNumber);

            var interval = await SettingProvider.GetAsync(IdentitySettingNames.User.SmsRepetInterval, 1);

            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.PhoneNumber, "SmsVerifyCode");

            var securityTokenCacheItem = await securityTokenCache.GetAsync(securityTokenCacheKey);

            if (securityTokenCacheItem != null)
            {
                throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
            }

            var template = await SettingProvider.GetOrNullAsync(IdentitySettingNames.User.SmsNewUserRegister);

            // 安全令牌
            var securityToken = GuidGenerator.Create().ToString("N");

            var code = totpService.GenerateCode(Encoding.Unicode.GetBytes(securityToken), securityTokenCacheKey);

            securityTokenCacheItem = new SecurityTokenCacheItem(code.ToString(), securityToken);

            await securityCodeSender.SendSmsCodeAsync(input.PhoneNumber, securityTokenCacheItem.Token, template);

            await securityTokenCache.SetAsync(securityTokenCacheKey, securityTokenCacheItem, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
            });

        }

        /// <summary>
        /// 发送手机重置密码验证码短信
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        /// <exception cref="UserFriendlyException"></exception>
        public async virtual Task SendPhoneResetPasswordCodeAsync(SendPhoneResetPasswordCodeDto input)
        {
            /*
             * 注解: 微软的重置密码方法通过 UserManager.GeneratePasswordResetTokenAsync 接口生成密码重置Token
             *       而这个Token设计的意义就是用户通过链接来重置密码,所以不适合短信验证
             *       某些企业是把链接生成一个短链发送短信的,不过这种方式不是很推荐,因为现在是真没几个人敢随便点短信链接的
             *  
             *  此处设计方式为:
             *  
             *  step1: 例行检查是否重复发送,这一点是很有必要的
             *  step2: 通过已确认的手机号来查询用户,如果用户未确认手机号,那就不能发送,这一点也是很有必要的
             *  step3(重点): 通过 UserManager.GenerateTwoFactorTokenAsync 接口来生成二次认证码,这就相当于伪验证码,只是用于确认用户传递的验证码是否通过
             *               比起自己生成随机数,这个验证码利用了TOTP算法,有时间限制的
             *  step4(重点): 用户传递验证码后,通过 UserManager.VerifyTwoFactorTokenAsync 接口来校验验证码
             *               验证通过后,再利用 UserManager.GeneratePasswordResetTokenAsync 接口来生成真正的用于重置密码的Token
            */

            // 传递 isConfirmed 用户必须是已确认过手机号的
            var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);

            // 外部认证用户不允许修改密码
            if (user.IsExternal)
            {
                throw new BusinessException(code: Volo.Abp.Identity.IdentityErrorCodes.ExternalUserPasswordChange);
            }

            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.PhoneNumber, "SmsVerifyCode");

            var securityTokenCacheItem = await securityTokenCache.GetAsync(securityTokenCacheKey);

            var interval = await SettingProvider.GetAsync(IdentitySettingNames.User.SmsRepetInterval, 1);

            // 能查询到缓存就是重复发送
            if (securityTokenCacheItem != null)
            {
                throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
            }

            var template = await SettingProvider.GetOrNullAsync(IdentitySettingNames.User.SmsResetPassword);

            // 生成二次认证码
            var code = await UserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);

            // 发送短信验证码
            await securityCodeSender.SendSmsCodeAsync(input.PhoneNumber, code, template);

            // 缓存这个手机号的记录,防重复
            securityTokenCacheItem = new SecurityTokenCacheItem(code, user.SecurityStamp);

            await securityTokenCache.SetAsync(securityTokenCacheKey, securityTokenCacheItem, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
            });

        }

        /// <summary>
        /// 发送手机登录验证码短信
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async virtual Task SendPhoneSigninCodeAsync(SendPhoneSigninCodeDto input)
        {
            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.PhoneNumber, "SmsVerifyCode");

            var securityTokenCacheItem = await securityTokenCache.GetAsync(securityTokenCacheKey);

            var interval = await SettingProvider.GetAsync(IdentitySettingNames.User.SmsRepetInterval, 1);

            if (securityTokenCacheItem != null)
            {
                throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
            }

            // 传递 isConfirmed 验证过的用户才允许通过手机登录
            var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);

            var code = await UserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);

            var template = await SettingProvider.GetOrNullAsync(IdentitySettingNames.User.SmsUserSignin);

            // 发送登录验证码短信
            await securityCodeSender.SendSmsCodeAsync(input.PhoneNumber, code, template);

            // 缓存登录验证码状态,防止同一手机号重复发送
            securityTokenCacheItem = new SecurityTokenCacheItem(code, user.SecurityStamp);

            await securityTokenCache.SetAsync(securityTokenCacheKey, securityTokenCacheItem, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
            });
        }

        protected async virtual Task<IdentityUser> GetUserByPhoneNumberAsync(string phoneNumber, bool isConfirmed = true)
        {
            var user = await userRepository.FindByPhoneNumberAsync(phoneNumber, isConfirmed, true);
            if (user == null)
            {
                throw new UserFriendlyException(L["PhoneNumberNotRegisterd"]);
            }
            return user;
        }

        protected async virtual Task CheckSelfRegistrationAsync()
        {
            if (!await SettingProvider.IsTrueAsync(Volo.Abp.Account.Settings.AccountSettingNames.IsSelfRegistrationEnabled))
            {
                throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
            }
        }

        protected async virtual Task CheckNewUserPhoneNumberNotBeUsedAsync(string phoneNumber)
        {
            if (await userRepository.IsPhoneNumberUedAsync(phoneNumber))
            {
                throw new UserFriendlyException(L["DuplicatePhoneNumber"]);
            }
        }

        protected virtual Task<string> FindClientIdAsync()
        {
            var client = LazyServiceProvider.LazyGetRequiredService<ICurrentClient>();

            return Task.FromResult(client.Id);
        }

        private void ThowIfInvalidEmailAddress(string inputEmail)
        {
            if (!inputEmail.IsNullOrWhiteSpace() &&
                !ValidationHelper.IsValidEmailAddress(inputEmail))
            {
                throw new AbpValidationException(
                    [
                        new ValidationResult(L["The {0} field is not a valid e-mail address.", L["DisplayName:EmailAddress"]], new string[]{ "EmailAddress" })
                    ]);
            }
        }

    }
}
