using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.Saas
{
    public class EditionDto : ExtensibleAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string DisplayName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
