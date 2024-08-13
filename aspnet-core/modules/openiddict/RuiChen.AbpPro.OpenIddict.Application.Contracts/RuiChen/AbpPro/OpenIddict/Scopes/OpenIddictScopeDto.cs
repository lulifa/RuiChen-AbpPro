using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.OpenIddict
{
    [Serializable]
    public class OpenIddictScopeDto : ExtensibleAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Description { get; set; }

        public Dictionary<string, string> Descriptions { get; set; } = new Dictionary<string, string>();


        public string DisplayName { get; set; }

        public Dictionary<string, string> DisplayNames { get; set; } = new Dictionary<string, string>();

        public string Name { get; set; }

        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public List<string> Resources { get; set; }

        public string ConcurrencyStamp { get; set; }

    }
}
