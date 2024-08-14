using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Identity
{
    public class OrganizationUnitGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
