using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Account;
public class VerifyAuthenticatorCodeInput
{
    [Required]
    [StringLength(6)]
    public string AuthenticatorCode { get; set; }
}
