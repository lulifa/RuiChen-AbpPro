using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Authorization.Permissions;

namespace RuiChen.AbpPro.Authorization
{
    /// <summary>
    /// 基于组织单位的权限管理：当应用程序需要根据用户所属的组织单位来管理权限时，这个类提供了一个灵活的方式来实现这种需求。
    /// 组织单位可以代表部门、团队或其他业务实体，并且每个组织单位可以有不同的权限。多租户应用：在多租户应用中，不同的组织单位可能会有不同的权限需求。
    /// 这个类能够帮助根据用户的组织单位动态检查权限，并提供适当的授权结果。
    /// 动态权限验证：在需要根据用户的动态组织单位信息来决定权限时，比如用户在不同的组织单位间切换，系统能够即时反映这些变化。
    /// 
    /// OrganizationUnitPermissionValueProvider 类扩展了权限检查的功能，允许系统根据用户的组织单位声明来决定权限授予。这对于多组织单位的权限管理和动态权限验证非常有用
    /// </summary>
    public class OrganizationUnitPermissionValueProvider : PermissionValueProvider
    {
        public const string ProviderName = "O";

        public override string Name => ProviderName;

        public OrganizationUnitPermissionValueProvider(IPermissionStore permissionStore) : base(permissionStore)
        {

        }

        public async override Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context)
        {
            var organizationUnits = context.Principal?.FindAll(AbpProOrganizationUnitClaimTypes.OrganizationUnit).Select(item => item.Value).Distinct().ToList();

            if (organizationUnits == null || organizationUnits.Count <= 0)
            {
                return PermissionGrantResult.Undefined;
            }

            foreach (var organizationUnit in organizationUnits)
            {
                if (await PermissionStore.IsGrantedAsync(context.Permission.Name, Name, organizationUnit))
                {
                    return PermissionGrantResult.Granted;
                }
            }

            return PermissionGrantResult.Undefined;
        }

        public async override Task<MultiplePermissionGrantResult> CheckAsync(PermissionValuesCheckContext context)
        {
            var permissionNames = context.Permissions.Select(item => item.Name).Distinct().ToList();

            Check.NotNullOrEmpty(permissionNames, nameof(permissionNames));

            var result = new MultiplePermissionGrantResult(permissionNames.ToArray());

            var organizationUnits = context.Principal?.FindAll(AbpProOrganizationUnitClaimTypes.OrganizationUnit).Select(item => item.Value).Distinct().ToList();

            if (organizationUnits == null || organizationUnits.Count <= 0)
            {
                return result;
            }

            foreach (var organizationUnit in organizationUnits)
            {
                var multipleResult = await PermissionStore.IsGrantedAsync(permissionNames.ToArray(), Name, organizationUnit);

                var grantResults = multipleResult.Result.Where(grantResult => result.Result.ContainsKey(grantResult.Key) &&
                                                                            result.Result[grantResult.Key] == PermissionGrantResult.Undefined &&
                                                                            grantResult.Value != PermissionGrantResult.Undefined).ToList();

                foreach (var grantResult in grantResults)
                {

                    result.Result[grantResult.Key] = grantResult.Value;

                    permissionNames.RemoveAll(items => items == grantResult.Key);

                }

                if (result.AllGranted || result.AllProhibited)
                {
                    break;
                }

                if (permissionNames.IsNullOrEmpty())
                {
                    break;
                }

            }

            return result;

        }
    }
}
