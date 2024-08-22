using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public class LanguageText : Entity<int>
    {
        public virtual string CultureName { get; protected set; }
        public virtual string Key { get; protected set; }
        public virtual string Value { get; protected set; }
        public virtual string ResourceName { get; protected set; }
        protected LanguageText() { }
        public LanguageText(
            [NotNull] string resourceName,
            [NotNull] string cultureName,
            [NotNull] string key,
            [CanBeNull] string value)
        {
            ResourceName = Check.NotNull(resourceName, nameof(resourceName), ResourceConsts.MaxNameLength);
            CultureName = Check.NotNullOrWhiteSpace(cultureName, nameof(cultureName), LanguageConsts.MaxCultureNameLength);
            Key = Check.NotNullOrWhiteSpace(key, nameof(key), LanguageTextConsts.MaxKeyLength);

            Value = !value.IsNullOrWhiteSpace()
                ? Check.NotNullOrWhiteSpace(value, nameof(value), LanguageTextConsts.MaxValueLength)
                : "";
        }

        public void SetValue(string value)
        {
            Value = !value.IsNullOrWhiteSpace()
                ? Check.NotNullOrWhiteSpace(value, nameof(value), LanguageTextConsts.MaxValueLength)
                : Value;
        }
    }
}
