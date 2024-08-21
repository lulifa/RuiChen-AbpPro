using RuiChen.AbpPro.Saas.Localization;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace RuiChen.AbpPro.Saas
{
    public class SaasFeatureDefinitionProvider : FeatureDefinitionProvider
    {
        public override void Define(IFeatureDefinitionContext context)
        {
            var saas = context.AddGroup(
                name: SaasFeatureNames.GroupName,
                displayName: L("Features:Saas"));

            var selectionValueType = new SelectionStringValueType
            {
                ItemSource = new StaticSelectionStringValueItemSource(
                    new LocalizableSelectionStringValueItem
                    {
                        Value = RecycleStrategy.Reserve.ToString(),
                        DisplayText = new LocalizableStringInfo(
                        LocalizationResourceNameAttribute.GetName(typeof(AbpProSaasResource)),
                        "RecycleStrategy:Reserve")
                    },
                    new LocalizableSelectionStringValueItem
                    {
                        Value = RecycleStrategy.Recycle.ToString(),
                        DisplayText = new LocalizableStringInfo(
                    LocalizationResourceNameAttribute.GetName(typeof(AbpProSaasResource)),
                    "RecycleStrategy:Recycle")
                    })
            };
            saas.AddFeature(
                name: SaasFeatureNames.Tenant.RecycleStrategy,
                defaultValue: RecycleStrategy.Recycle.ToString(),
                displayName: L("Features:RecycleStrategy"),
                description: L("Features:RecycleStrategyDesc"),
                valueType: selectionValueType,
                isAvailableToHost: false);
            saas.AddFeature(
                name: SaasFeatureNames.Tenant.ExpirationReminderDays,
                defaultValue: 15.ToString(),
                displayName: L("Features:ExpirationReminderDays"),
                description: L("Features:ExpirationReminderDaysDesc"),
                valueType: new FreeTextStringValueType(new NumericValueValidator(1, 30)),
                isAvailableToHost: false);
            saas.AddFeature(
                name: SaasFeatureNames.Tenant.ExpiredRecoveryTime,
                defaultValue: 15.ToString(),
                displayName: L("Features:ExpiredRecoveryTime"),
                description: L("Features:ExpiredRecoveryTimeDesc"),
                valueType: new FreeTextStringValueType(new NumericValueValidator(1, 30)),
                isAvailableToHost: false);
        }

        protected ILocalizableString L(string name)
        {
            return LocalizableString.Create<AbpProSaasResource>(name);
        }
    }
}
