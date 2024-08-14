namespace RuiChen.AbpPro.Identity
{
    public interface IAuthenticatorUriGenerator
    {
        string Generate(string email, string unformattedKey);
    }
}
