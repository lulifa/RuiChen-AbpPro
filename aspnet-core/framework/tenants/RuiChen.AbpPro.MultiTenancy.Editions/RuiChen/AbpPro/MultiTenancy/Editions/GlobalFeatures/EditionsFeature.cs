using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace RuiChen.AbpPro.MultiTenancy
{
    [GlobalFeatureName(Name)]
    public class EditionsFeature : GlobalFeature
    {
        public const string Name = "Abp.Editions";
        internal EditionsFeature([NotNull] GlobalEditionsFeatures module)
        : base(module)
        {
        }
    }
}
