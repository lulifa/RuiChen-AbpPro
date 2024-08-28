using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Platform
{
    public class GetDataListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
