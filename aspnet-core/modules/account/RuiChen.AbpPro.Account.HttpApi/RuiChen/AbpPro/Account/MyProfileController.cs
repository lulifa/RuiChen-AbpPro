using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;

namespace RuiChen.AbpPro.Account
{
    /// <summary>
    /// 个人信息
    /// </summary>
    [Route("/api/account/my-profile")]
    public class MyProfileController:AccountControllerBase, IMyProfileAppService
    {
        private readonly IMyProfileAppService myProfileAppService;

        public MyProfileController(IMyProfileAppService myProfileAppService)
        {
            this.myProfileAppService = myProfileAppService;
        }

        /// <summary>
        ///获取二次认证状态
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("two-factor")]
        public async virtual Task<TwoFactorEnabledDto> GetTwoFactorEnabledAsync()
        {
            return await myProfileAppService.GetTwoFactorEnabledAsync();
        }

        /// <summary>
        /// 改变二次认证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("change-two-factor")]
        public async virtual Task ChangeTwoFactorEnabledAsync(TwoFactorEnabledDto input)
        {
            await myProfileAppService.ChangeTwoFactorEnabledAsync(input);
        }

        /// <summary>
        /// 发送改变手机号验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("send-phone-number-change-code")]
        public async virtual Task SendChangePhoneNumberCodeAsync(SendChangePhoneNumberCodeInput input)
        {
            await myProfileAppService.SendChangePhoneNumberCodeAsync(input);
        }

        /// <summary>
        /// 改变手机认证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("change-phone-number")]
        public async virtual Task ChangePhoneNumberAsync(ChangePhoneNumberInput input)
        {
            await myProfileAppService.ChangePhoneNumberAsync(input);
        }

        /// <summary>
        /// 发送确认邮件验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("send-email-confirm-link")]
        public async virtual Task SendEmailConfirmLinkAsync(SendEmailConfirmCodeDto input)
        {
            await myProfileAppService.SendEmailConfirmLinkAsync(input);
        }

        /// <summary>
        /// 确认邮件地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("confirm-email")]
        public async virtual Task ConfirmEmailAsync(ConfirmEmailInput input)
        {
            await myProfileAppService.ConfirmEmailAsync(input);
        }

        /// <summary>
        /// 获取验证器信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("authenticator")]
        public async virtual Task<AuthenticatorDto> GetAuthenticator()
        {
            return await myProfileAppService.GetAuthenticator();
        }

        /// <summary>
        /// 验证验证器代码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("verify-authenticator-code")]
        public async virtual Task<AuthenticatorRecoveryCodeDto> VerifyAuthenticatorCode(VerifyAuthenticatorCodeInput input)
        {
            return await myProfileAppService.VerifyAuthenticatorCode(input);
        }

        /// <summary>
        /// 重置验证器
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("reset-authenticator")]
        public async virtual Task ResetAuthenticator()
        {
            await myProfileAppService.ResetAuthenticator();
        }

    }
}
