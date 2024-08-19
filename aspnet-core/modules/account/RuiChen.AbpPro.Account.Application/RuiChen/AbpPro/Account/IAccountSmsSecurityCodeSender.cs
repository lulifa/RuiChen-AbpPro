namespace RuiChen.AbpPro.Account
{
    public interface IAccountSmsSecurityCodeSender
    {
        Task SendSmsCodeAsync(
            string phone,
            string token,
            string template, // 传递模板号
            CancellationToken cancellation = default);
    }
}
