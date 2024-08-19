using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Account
{
    /// <summary>
    /// 账户登录
    /// </summary>
    [Route("api/account")]
    public class AccountController : AccountControllerBase, IAccountAppService
    {
        private readonly IAccountAppService accountAppService;

        public AccountController(IAccountAppService accountAppService)
        {
            this.accountAppService = accountAppService;
        }

        /// <summary>
        /// 通过手机号注册用户账户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("phone/register")]
        public async virtual Task RegisterPhoneAsync(PhoneRegisterDto input)
        {
            await accountAppService.RegisterPhoneAsync(input);
        }

        /// <summary>
        /// 通过手机号重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("phone/reset-password")]
        public async virtual Task ResetPasswordAsync(PhoneResetPasswordDto input)
        {
            await accountAppService.ResetPasswordAsync(input);
        }

        /// <summary>
        /// 发送手机登录验证码短信
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("phone/send-signin-code")]
        public async virtual Task SendPhoneSigninCodeAsync(SendPhoneSigninCodeDto input)
        {
            await accountAppService.SendPhoneSigninCodeAsync(input);
        }

        /// <summary>
        /// 发送邮件登录验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("email/send-signin-code")]
        public async virtual Task SendEmailSigninCodeAsync(SendEmailSigninCodeDto input)
        {
            await accountAppService.SendEmailSigninCodeAsync(input);
        }

        /// <summary>
        /// 发送手机注册验证码短信
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("phone/send-register-code")]
        public async virtual Task SendPhoneRegisterCodeAsync(SendPhoneRegisterCodeDto input)
        {
            await accountAppService.SendPhoneRegisterCodeAsync(input);
        }

        /// <summary>
        /// 发送手机重置密码验证码短信
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("phone/send-password-reset-code")]
        public async virtual Task SendPhoneResetPasswordCodeAsync(SendPhoneResetPasswordCodeDto input)
        {
            await accountAppService.SendPhoneResetPasswordCodeAsync(input);
        }

        /// <summary>
        /// 获取用户二次认证提供者列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("two-factor-providers")]
        public async virtual Task<ListResultDto<NameValue>> GetTwoFactorProvidersAsync(GetTwoFactorProvidersInput input)
        {
            return await accountAppService.GetTwoFactorProvidersAsync(input);
        }

    }
}
