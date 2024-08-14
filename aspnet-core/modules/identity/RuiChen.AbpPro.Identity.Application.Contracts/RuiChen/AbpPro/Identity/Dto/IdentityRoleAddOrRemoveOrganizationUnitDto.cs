using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Identity
{
    public class IdentityRoleAddOrRemoveOrganizationUnitDto
    {
        [Required]
        public Guid[] OrganizationUnitIds { get; set; }
    }
}
