using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.Saas
{
    public class TenantUpdateDto : TenantCreateOrUpdateBase, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}