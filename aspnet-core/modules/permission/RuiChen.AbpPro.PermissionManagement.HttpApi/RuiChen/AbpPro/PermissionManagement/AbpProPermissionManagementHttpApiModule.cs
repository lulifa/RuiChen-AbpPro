using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.PermissionManagement.Localization;
using Volo.Abp.Validation.Localization;
using VoloAbpPermissionManagementHttpApiModule = Volo.Abp.PermissionManagement.HttpApi.AbpPermissionManagementHttpApiModule;

namespace RuiChen.AbpPro.PermissionManagement
{
    [DependsOn(
        typeof(VoloAbpPermissionManagementHttpApiModule),
        typeof(AbpProPermissionManagementApplicationContractsModule)
        )]
    public class AbpProPermissionManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(options =>
            {
                options.AddApplicationPartIfNotExists(typeof(AbpProPermissionManagementHttpApiModule).Assembly);
            });

            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(AbpPermissionManagementResource), typeof(AbpProPermissionManagementApplicationContractsModule).Assembly);
            });

        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AbpPermissionManagementResource>().AddBaseTypes(typeof(AbpValidationResource));
            });
        }

    }
}
