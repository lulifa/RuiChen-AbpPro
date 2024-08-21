using Volo.Abp.ObjectExtending.Modularity;

namespace RuiChen.AbpPro.Saas
{
    public class SaasModuleExtensionConfiguration : ModuleExtensionConfiguration
    {
        public SaasModuleExtensionConfiguration ConfigureTenant(
            Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                SaasModuleExtensionConsts.EntityNames.Edition,
                configureAction
            );
        }
    }
}
