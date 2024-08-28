using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.Platform
{
    public class UserFavoriteMenuCreateDto : UserFavoriteMenuCreateOrUpdateDto
    {
        [Required]
        [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]

        public string Framework { get; set; }
    }
}
