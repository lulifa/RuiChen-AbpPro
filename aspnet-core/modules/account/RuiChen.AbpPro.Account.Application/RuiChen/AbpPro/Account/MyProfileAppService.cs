using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using RuiChen.AbpPro.Identity;
using System.Text;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Users;

namespace RuiChen.AbpPro.Account
{
    public class MyProfileAppService : AccountApplicationServiceBase, IMyProfileAppService
    {
        private readonly Identity.IIdentityUserRepository userRepository;
        private readonly IAccountSmsSecurityCodeSender securityCodeSender;
        private readonly IdentitySecurityLogManager identitySecurityLogManager;
        private readonly IDistributedCache<SecurityTokenCacheItem> securityTokenCache;

        public MyProfileAppService(Identity.IIdentityUserRepository userRepository, IAccountSmsSecurityCodeSender securityCodeSender, IdentitySecurityLogManager identitySecurityLogManager, IDistributedCache<SecurityTokenCacheItem> securityTokenCache)
        {
            this.userRepository = userRepository;
            this.securityCodeSender = securityCodeSender;
            this.identitySecurityLogManager = identitySecurityLogManager;
            this.securityTokenCache = securityTokenCache;
        }

        /// <summary>
        /// 更换手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async virtual Task ChangePhoneNumberAsync(ChangePhoneNumberInput input)
        {
            // 是否已有用户使用手机号绑定
            if (await userRepository.IsPhoneNumberConfirmedAsync(input.NewPhoneNumber))
            {
                throw new BusinessException(Identity.IdentityErrorCodes.DuplicatePhoneNumber);
            }

            var user = await GetCurrentUserAsync();

            // 更换手机号
            (await UserManager.ChangePhoneNumberAsync(user, input.NewPhoneNumber, input.Code)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();

            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.NewPhoneNumber, "SmsChangePhoneNumber");

            await securityTokenCache.RemoveAsync(securityTokenCacheKey);
        }

        /// <summary>
        /// 启用双因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task ChangeTwoFactorEnabledAsync(TwoFactorEnabledDto input)
        {
            // Removed See: https://github.com/abpframework/abp/pull/7719
            //if (!await SettingProvider.IsTrueAsync(IdentitySettingNames.TwoFactor.UsersCanChange))
            //{
            //    throw new BusinessException(Volo.Abp.Identity.IdentityErrorCodes.CanNotChangeTwoFactor);
            //}
            // TODO: Abp官方移除了双因素的设置,不排除以后会增加,如果在用户接口中启用了双因素认证,可能造成登录失败!
            var user = await GetCurrentUserAsync();

            (await UserManager.SetTwoFactorEnabledWithAccountConfirmedAsync(user, input.Enabled)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 确认邮件地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task ConfirmEmailAsync(ConfirmEmailInput input)
        {
            await IdentityOptions.SetAsync();

            var user = await UserManager.GetByIdAsync(CurrentUser.GetId());

            (await UserManager.ConfirmEmailAsync(user, input.ConfirmToken)).CheckErrors();

            await identitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
            {
                Identity = IdentitySecurityLogIdentityConsts.Identity,
                Action = "ConfirmEmail"
            });
        }

        /// <summary>
        /// 获取验证器信息
        /// </summary>
        /// <returns></returns>
        public async virtual Task<AuthenticatorDto> GetAuthenticator()
        {
            await IdentityOptions.SetAsync();

            var user = await UserManager.GetByIdAsync(CurrentUser.GetId());

            var hasAuthenticatorEnabled = user.GetProperty(UserManager.Options.Tokens.AuthenticatorTokenProvider, false);
            if (hasAuthenticatorEnabled)
            {
                return new AuthenticatorDto
                {
                    IsAuthenticated = true,
                };
            }

            var userEmail = await UserManager.GetEmailAsync(user);

            var unformattedKey = await UserManager.GetAuthenticatorKeyAsync(user);

            if (string.IsNullOrEmpty(unformattedKey))
            {
                await UserManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await UserManager.GetAuthenticatorKeyAsync(user);
            }

            var authenticatorUri = AuthenticatorUriGenerator.Generate(userEmail, unformattedKey);

            return new AuthenticatorDto
            {
                SharedKey = FormatKey(unformattedKey),
                AuthenticatorUri = authenticatorUri,
            };
        }

        /// <summary>
        /// 获取二次验证信息
        /// </summary>
        /// <returns></returns>
        public async virtual Task<TwoFactorEnabledDto> GetTwoFactorEnabledAsync()
        {
            var user = await GetCurrentUserAsync();

            return new TwoFactorEnabledDto
            {
                Enabled = await UserManager.GetTwoFactorEnabledAsync(user),
            };
        }

        /// <summary>
        /// 重置验证器
        /// </summary>
        /// <returns></returns>
        public async virtual Task ResetAuthenticator()
        {
            await IdentityOptions.SetAsync();

            var user = await UserManager.GetByIdAsync(CurrentUser.GetId());

            user.RemoveProperty(UserManager.Options.Tokens.AuthenticatorTokenProvider);

            await UserManager.ResetAuthenticatorKeyAsync(user);

            await UserStore.ReplaceCodesAsync(user, Array.Empty<string>());

            var validTwoFactorProviders = await UserManager.GetValidTwoFactorProvidersAsync(user);

            if (!validTwoFactorProviders.Where(provider => provider != UserManager.Options.Tokens.AuthenticatorTokenProvider).Any())
            {
                // 如果用户没有任何双因素认证提供程序,则禁用二次认证,否则将造成无法登录
                await UserManager.SetTwoFactorEnabledAsync(user, false);
            }

            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 发送重置手机号验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        /// <exception cref="BusinessException"></exception>
        public async virtual Task SendChangePhoneNumberCodeAsync(SendChangePhoneNumberCodeInput input)
        {
            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.NewPhoneNumber, "SmsChangePhoneNumber");

            var securityTokenCacheItem = await securityTokenCache.GetAsync(securityTokenCacheKey);

            var interval = await SettingProvider.GetAsync(IdentitySettingNames.User.SmsRepetInterval, 1);

            if (securityTokenCacheItem != null)
            {
                throw new UserFriendlyException(L["SendRepeatPhoneVerifyCode", interval]);
            }

            // 是否已有用户使用手机号绑定
            if (await userRepository.IsPhoneNumberConfirmedAsync(input.NewPhoneNumber))
            {
                throw new BusinessException(Identity.IdentityErrorCodes.DuplicatePhoneNumber);
            }

            var user = await GetCurrentUserAsync();

            var template = await SettingProvider.GetOrNullAsync(Identity.IdentitySettingNames.User.SmsPhoneNumberConfirmed);

            var token = await UserManager.GenerateChangePhoneNumberTokenAsync(user, input.NewPhoneNumber);

            // 发送验证码
            await securityCodeSender.SendSmsCodeAsync(input.NewPhoneNumber, token, template);

            securityTokenCacheItem = new SecurityTokenCacheItem(token, user.ConcurrencyStamp);

            await securityTokenCache.SetAsync(securityTokenCacheKey, securityTokenCacheItem, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
            });
        }

        /// <summary>
        /// 发送确认邮件验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        /// <exception cref="BusinessException"></exception>
        public async virtual Task SendEmailConfirmLinkAsync(SendEmailConfirmCodeDto input)
        {
            var user = await UserManager.FindByEmailAsync(input.Email);

            if (user == null)
            {
                throw new UserFriendlyException(L["Volo.Account:InvalidEmailAddress", input.Email]);
            }

            if (user.EmailConfirmed)
            {
                throw new BusinessException(Identity.IdentityErrorCodes.DuplicateConfirmEmailAddress);
            }

            var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);

            await AccountEmailConfirmSender.SendEmailConfirmLinkAsync(user, token, input.AppName, input.ReturnUrl, input.ReturnUrlHash);
        }

        /// <summary>
        /// 验证验证器代码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async virtual Task<AuthenticatorRecoveryCodeDto> VerifyAuthenticatorCode(VerifyAuthenticatorCodeInput input)
        {
            await IdentityOptions.SetAsync();

            var user = await UserManager.GetByIdAsync(CurrentUser.GetId());

            var tokenValid = await UserManager.VerifyTwoFactorTokenAsync(user, UserManager.Options.Tokens.AuthenticatorTokenProvider, input.AuthenticatorCode);

            if (!tokenValid)
            {
                throw new BusinessException(Identity.IdentityErrorCodes.AuthenticatorTokenInValid);
            }

            var recoveryCodes = await UserManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);

            user.SetProperty(UserManager.Options.Tokens.AuthenticatorTokenProvider, true);

            await UserStore.ReplaceCodesAsync(user, recoveryCodes);

            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();

            return new AuthenticatorRecoveryCodeDto
            {
                RecoveryCodes = recoveryCodes.ToList(),
            };
        }

        private static string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            var currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition, 4)).Append(' ');
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }
    }
}
