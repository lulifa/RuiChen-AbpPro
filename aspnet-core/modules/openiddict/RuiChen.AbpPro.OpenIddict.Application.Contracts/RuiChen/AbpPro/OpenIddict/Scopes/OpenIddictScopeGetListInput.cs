using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.OpenIddict
{
    [Serializable]
    public class OpenIddictScopeGetListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
