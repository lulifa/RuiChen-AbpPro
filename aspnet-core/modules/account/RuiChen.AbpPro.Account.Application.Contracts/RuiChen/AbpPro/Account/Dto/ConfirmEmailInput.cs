using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Account
{
    public class ConfirmEmailInput
    {
        [Required]
        public string ConfirmToken { get; set; }
    }
}
