using Volo.Abp.Auditing;
using Volo.Abp.EventBus;

namespace RuiChen.AbpPro.Saas
{
    [Serializable]
    [EventName("abp.saas.edition")]
    public class EditionEto : IHasEntityVersion
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public int EntityVersion { get; set; }
    }
}
