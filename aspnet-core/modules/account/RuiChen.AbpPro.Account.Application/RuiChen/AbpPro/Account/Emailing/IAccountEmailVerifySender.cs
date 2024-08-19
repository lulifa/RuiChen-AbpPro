namespace RuiChen.AbpPro.Account
{
    public interface IAccountEmailVerifySender
    {

        Task SendMailLoginVerifyCodeAsync(
        string code,
        string userName,
        string emailAddress);

    }
}
