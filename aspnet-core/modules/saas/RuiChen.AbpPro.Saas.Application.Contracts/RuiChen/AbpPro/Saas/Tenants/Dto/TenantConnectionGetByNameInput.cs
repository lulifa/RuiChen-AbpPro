using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.Saas
{
    public class TenantConnectionGetByNameInput
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [DynamicStringLength(typeof(TenantConnectionStringConsts), nameof(TenantConnectionStringConsts.MaxNameLength))]
        public string Name { get; set; }
    }
}
