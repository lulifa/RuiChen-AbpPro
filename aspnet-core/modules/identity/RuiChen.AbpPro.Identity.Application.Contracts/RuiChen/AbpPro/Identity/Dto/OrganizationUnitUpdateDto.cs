using Volo.Abp.ObjectExtending;

namespace RuiChen.AbpPro.Identity
{
    public class OrganizationUnitUpdateDto : ExtensibleObject
    {
        public string DisplayName { get; set; }
    }
}
