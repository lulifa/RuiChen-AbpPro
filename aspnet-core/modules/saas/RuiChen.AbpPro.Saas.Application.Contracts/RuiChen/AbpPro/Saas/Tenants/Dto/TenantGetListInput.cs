using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Saas
{
    public class TenantGetListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}