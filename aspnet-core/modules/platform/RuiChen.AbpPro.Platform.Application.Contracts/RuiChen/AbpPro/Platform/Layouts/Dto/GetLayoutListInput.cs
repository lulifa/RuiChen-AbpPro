using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.Platform
{
    public class GetLayoutListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
        public string Framework { get; set; }
    }
}
