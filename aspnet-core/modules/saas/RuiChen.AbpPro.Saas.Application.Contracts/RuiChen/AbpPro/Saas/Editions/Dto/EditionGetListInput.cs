using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Saas
{
    public class EditionGetListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
