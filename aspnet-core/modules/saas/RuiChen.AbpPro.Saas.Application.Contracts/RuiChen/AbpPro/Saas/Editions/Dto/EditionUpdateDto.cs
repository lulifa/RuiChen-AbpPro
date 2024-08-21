using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.Saas
{
    public class EditionUpdateDto : EditionCreateOrUpdateBase, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
