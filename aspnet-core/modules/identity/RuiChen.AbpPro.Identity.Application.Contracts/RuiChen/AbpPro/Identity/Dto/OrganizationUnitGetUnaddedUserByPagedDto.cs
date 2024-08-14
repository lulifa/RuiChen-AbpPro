using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Identity
{
    public class OrganizationUnitGetUnaddedUserByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
