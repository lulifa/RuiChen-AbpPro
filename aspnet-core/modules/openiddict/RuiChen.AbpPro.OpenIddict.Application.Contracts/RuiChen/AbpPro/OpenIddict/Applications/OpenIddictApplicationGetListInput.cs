using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.OpenIddict
{
    [Serializable]
    public class OpenIddictApplicationGetListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
