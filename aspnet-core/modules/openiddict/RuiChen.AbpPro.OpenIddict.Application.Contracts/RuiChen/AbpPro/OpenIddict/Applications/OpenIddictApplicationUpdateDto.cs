using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.OpenIddict
{
    [Serializable]
    public class OpenIddictApplicationUpdateDto : OpenIddictApplicationCreateOrUpdateDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
