using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Identity
{
    public class IdentityUserOrganizationUnitUpdateDto
    {
        [Required]
        public Guid[] OrganizationUnitIds { get; set; }
    }
}
