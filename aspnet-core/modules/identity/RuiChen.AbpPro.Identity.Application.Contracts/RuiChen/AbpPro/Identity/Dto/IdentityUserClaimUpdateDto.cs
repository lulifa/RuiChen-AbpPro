using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.Identity
{
    public class IdentityUserClaimUpdateDto : IdentityUserClaimCreateOrUpdateDto
    {
        [DynamicMaxLength(typeof(IdentityUserClaimConsts), nameof(IdentityUserClaimConsts.MaxClaimValueLength))]
        public string NewClaimValue { get; set; }
    }
}
