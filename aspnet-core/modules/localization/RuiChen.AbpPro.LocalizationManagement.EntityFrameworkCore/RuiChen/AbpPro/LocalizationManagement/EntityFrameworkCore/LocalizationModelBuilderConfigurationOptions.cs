using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public class LocalizationModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public LocalizationModelBuilderConfigurationOptions([NotNull] string tablePrefix = "", [CanBeNull] string schema = null)
            : base(tablePrefix, schema)
        {

        }
    }
}
