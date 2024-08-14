using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Identity
{
    public class OrganizationUnitGetUnaddedRoleByPagedDto : PagedAndSortedResultRequestDto
    {

        public string Filter { get; set; }
    }
}
