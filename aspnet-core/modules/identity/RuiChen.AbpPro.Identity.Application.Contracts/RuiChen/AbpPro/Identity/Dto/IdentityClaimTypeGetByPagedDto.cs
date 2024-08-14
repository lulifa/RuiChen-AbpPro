using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Identity
{
    public class IdentityClaimTypeGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
