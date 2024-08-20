using Volo.Abp.Data;

namespace RuiChen.AbpPro.FeatureManagement
{
    public class FeatureGroupDefinitionDto : IHasExtraProperties
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsStatic { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
