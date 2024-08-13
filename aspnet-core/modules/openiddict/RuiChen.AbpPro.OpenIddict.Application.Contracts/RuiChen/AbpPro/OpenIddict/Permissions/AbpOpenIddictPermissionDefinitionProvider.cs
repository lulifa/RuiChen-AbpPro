using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.OpenIddict.Localization;

namespace RuiChen.AbpPro.OpenIddict
{
    public class AbpOpenIddictPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var openiddictGroup = context.GetGroupOrNull(AbpProOpenIddictPermissions.GroupName);

            if (openiddictGroup == null)
            {
                openiddictGroup = context.AddGroup(AbpProOpenIddictPermissions.GroupName, L("Permissions:OpenIddict"));
            }

            var applications = openiddictGroup.AddPermission(
                AbpProOpenIddictPermissions.Applications.Default,
                L("Permissions:Applications"),
                MultiTenancySides.Host);
            applications.AddChild(
                AbpProOpenIddictPermissions.Applications.Create,
                L("Permissions:Create"),
                MultiTenancySides.Host);
            applications.AddChild(
                AbpProOpenIddictPermissions.Applications.Update,
                L("Permissions:Update"),
                MultiTenancySides.Host);
            applications.AddChild(
                AbpProOpenIddictPermissions.Applications.Delete,
                L("Permissions:Delete"),
                MultiTenancySides.Host);
            applications.AddChild(
                AbpProOpenIddictPermissions.Applications.ManagePermissions,
                L("Permissions:ManagePermissions"),
                MultiTenancySides.Host);
            applications.AddChild(
                AbpProOpenIddictPermissions.Applications.ManageSecret,
                L("Permissions:ManageSecret"),
                MultiTenancySides.Host);

            var authorizations = openiddictGroup.AddPermission(
                AbpProOpenIddictPermissions.Authorizations.Default,
                L("Permissions:Authorizations"),
                MultiTenancySides.Host);
            authorizations.AddChild(
                AbpProOpenIddictPermissions.Authorizations.Delete,
                L("Permissions:Delete"),
                MultiTenancySides.Host);

            var scopes = openiddictGroup.AddPermission(
                AbpProOpenIddictPermissions.Scopes.Default,
                L("Permissions:Scopes"),
                MultiTenancySides.Host);
            scopes.AddChild(
                AbpProOpenIddictPermissions.Scopes.Create,
                L("Permissions:Create"),
                MultiTenancySides.Host);
            scopes.AddChild(
                AbpProOpenIddictPermissions.Scopes.Update,
                L("Permissions:Update"),
                MultiTenancySides.Host);
            scopes.AddChild(
                AbpProOpenIddictPermissions.Scopes.Delete,
                L("Permissions:Delete"),
                MultiTenancySides.Host);

            var tokens = openiddictGroup.AddPermission(
                AbpProOpenIddictPermissions.Tokens.Default,
                L("Permissions:Tokens"),
                MultiTenancySides.Host);
            tokens.AddChild(
                AbpProOpenIddictPermissions.Tokens.Delete,
                L("Permissions:Delete"),
                MultiTenancySides.Host);
        }

        protected virtual LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpOpenIddictResource>(name);
        }
    }
}
