using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.Account
{
    public class ChangeAvatarInput
    {
        [DynamicMaxLength(typeof(IdentityUserClaimConsts), nameof(IdentityUserClaimConsts.MaxClaimValueLength))]
        public string AvatarUrl { get; set; }
    }
}
