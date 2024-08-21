using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace RuiChen.AbpPro.Saas
{
    public class Edition : FullAuditedAggregateRoot<Guid>, IHasEntityVersion
    {
        public virtual string DisplayName { get; protected set; }

        public virtual int EntityVersion { get; protected set; }

        protected Edition()
        {
        }

        protected internal Edition(Guid id, [NotNull] string displayName)
            : base(id)
        {
            SetDisplayName(displayName);
        }

        protected internal virtual void SetDisplayName([NotNull] string displayName)
        {
            DisplayName = Check.NotNullOrWhiteSpace(displayName, nameof(displayName), EditionConsts.MaxDisplayNameLength);
        }
    }
}
