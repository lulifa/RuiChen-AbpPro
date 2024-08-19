using System;
using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Account
{
    public class GetTwoFactorProvidersInput
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
