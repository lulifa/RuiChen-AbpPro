using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Identity
{
    public class OrganizationUnitAddUserDto
    {
        [Required]
        public List<Guid> UserIds { get; set; }
    }
}
