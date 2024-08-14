namespace RuiChen.AbpPro.Identity
{
    public interface ITotpService
    {
        int GenerateCode(byte[] securityToken, string modifier = null);

        bool ValidateCode(byte[] securityToken, int code, string modifier = null);
    }
}
