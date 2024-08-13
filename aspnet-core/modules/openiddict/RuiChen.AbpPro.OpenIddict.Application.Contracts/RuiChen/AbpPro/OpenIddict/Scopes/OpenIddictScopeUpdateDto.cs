using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.OpenIddict
{
    [Serializable]
    public class OpenIddictScopeUpdateDto : OpenIddictScopeCreateOrUpdateDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
