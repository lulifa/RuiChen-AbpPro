using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Platform
{
    public class RoleMenuStartupInput
    {
        [Required]
        [StringLength(80)]
        public string RoleName { get; set; }
    }
}
