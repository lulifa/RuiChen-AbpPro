using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Platform
{
    public class RoleMenuInput
    {
        [Required]
        [StringLength(80)]
        public string RoleName { get; set; }

        [Required]
        public List<Guid> MenuIds { get; set; } = new List<Guid>();
    }
}
