using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public class ResourceCreateDto : ResourceCreateOrUpdateDto
    {
        [Required]
        [DynamicStringLength(typeof(ResourceConsts), nameof(ResourceConsts.MaxNameLength))]
        public string Name { get; set; }
    }
}
