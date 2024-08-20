using System.ComponentModel.DataAnnotations;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.FeatureManagement
{
    public class FeatureGroupDefinitionCreateDto : FeatureGroupDefinitionCreateOrUpdateDto
    {
        [Required]
        [DynamicStringLength(typeof(FeatureGroupDefinitionRecordConsts), nameof(FeatureGroupDefinitionRecordConsts.MaxNameLength))]
        public string Name { get; set; }
    }
}
