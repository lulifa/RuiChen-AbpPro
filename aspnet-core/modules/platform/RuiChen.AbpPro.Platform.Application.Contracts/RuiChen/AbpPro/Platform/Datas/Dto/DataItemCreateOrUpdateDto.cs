using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.Platform
{
    public class DataItemCreateOrUpdateDto : IValidatableObject
    {
        [Required]
        [DynamicStringLength(typeof(DataItemConsts), nameof(DataItemConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }

        [DynamicStringLength(typeof(DataItemConsts), nameof(DataItemConsts.MaxValueLength))]
        public string DefaultValue { get; set; }

        [DynamicStringLength(typeof(DataItemConsts), nameof(DataItemConsts.MaxDescriptionLength))]
        public string Description { get; set; }

        public bool AllowBeNull { get; set; }

        public ValueType ValueType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!AllowBeNull && DefaultValue.IsNullOrWhiteSpace())
            {
                var localizer = validationContext.GetRequiredService<IStringLocalizer<PlatformResource>>();
                yield return new ValidationResult(
                    localizer["The {0} field is required.", localizer["DisplayName:Value"]],
                    new string[] { nameof(DefaultValue) });
            }
        }
    }
}
