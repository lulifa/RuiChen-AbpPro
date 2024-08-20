using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.FeatureManagement
{
    public class FeatureDefinitionUpdateDto : FeatureDefinitionCreateOrUpdateDto, IHasConcurrencyStamp
    {
        [StringLength(40)]
        public string ConcurrencyStamp { get; set; }
    }
}
