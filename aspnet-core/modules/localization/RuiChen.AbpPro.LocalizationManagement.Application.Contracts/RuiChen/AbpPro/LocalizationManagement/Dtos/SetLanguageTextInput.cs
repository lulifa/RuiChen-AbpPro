using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public class SetLanguageTextInput
    {
        [Required]
        [DynamicStringLength(typeof(ResourceConsts), nameof(ResourceConsts.MaxNameLength))]
        public string ResourceName { get; set; }

        [Required]
        [DynamicStringLength(typeof(LanguageTextConsts), nameof(LanguageTextConsts.MaxKeyLength))]
        public string Key { get; set; }

        [Required]
        [DynamicStringLength(typeof(LanguageConsts), nameof(LanguageConsts.MaxCultureNameLength))]
        public string CultureName { get; set; }

        [DynamicStringLength(typeof(LanguageTextConsts), nameof(LanguageTextConsts.MaxValueLength))]
        public string Value { get; set; }

    }
}
