using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.OpenIddict
{
    [Serializable]
    public class OpenIddictAuthorizationDto : ExtensibleAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid? ApplicationId { get; set; }
        public DateTime? CreationDate { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public List<string> Scopes { get; set; } = new List<string>();
        public string Status { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
