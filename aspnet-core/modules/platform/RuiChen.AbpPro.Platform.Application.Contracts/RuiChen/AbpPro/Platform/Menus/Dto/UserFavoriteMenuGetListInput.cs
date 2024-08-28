using Volo.Abp.Validation;

namespace RuiChen.AbpPro.Platform
{
    public class UserFavoriteMenuGetListInput
    {
        [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
        public string Framework { get; set; }
    }
}
