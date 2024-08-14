using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Identity
{
    public class OrganizationUnitAddRoleDto
    {
        [Required]
        public List<Guid> RoleIds { get; set; }
    }
}
