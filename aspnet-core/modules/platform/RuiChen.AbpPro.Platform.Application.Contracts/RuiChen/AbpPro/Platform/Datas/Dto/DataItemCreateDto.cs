using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.Platform
{
    public class DataItemCreateDto : DataItemCreateOrUpdateDto
    {
        [Required]
        [DynamicStringLength(typeof(DataItemConsts), nameof(DataItemConsts.MaxNameLength))]
        public string Name { get; set; }
    }
}
