using RuiChen.AbpPro.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace RuiChen.AbpPro.Identity
{
    /// <summary>
    /// OrganizationUnitClaimsPrincipalContributor 类扩展了 ClaimsPrincipal，确保用户的组织单位信息被正确添加到声明中。
    /// 它通过获取用户和角色的组织单位信息并动态更新 ClaimsPrincipal，实现了基于组织单位的权限控制。
    /// 这对于需要细粒度权限控制和动态权限更新的应用程序特别有用
    /// 
    /// 基于组织单位的权限控制：如果应用程序需要基于用户的组织单位来控制访问权限（如多租户应用、部门权限管理等）
    /// OrganizationUnitClaimsPrincipalContributor 类可以动态地将组织单位信息添加到用户的 ClaimsPrincipal 中，
    /// 确保在权限检查时能够正确识别用户的组织单位。动态声明生成：在用户登录时，可能没有组织单位声明。
    /// 这个类可以在用户登录后动态生成和添加这些声明，确保用户的权限和组织单位信息是最新的。
    /// 集成与授权策略：可以将组织单位声明与授权策略结合，确保用户在其组织单位内拥有合适的权限。
    /// 例如基于声明的授权策略可以简化权限管理并提高安全性。增强用户体验
    /// 确保用户在访问不同功能或页面时能够基于组织单位获得适当的授权，提升用户体验和应用程序的安全性。
    /// </summary>
    public class OrganizationUnitClaimsPrincipalContributor : IAbpClaimsPrincipalContributor, ITransientDependency
    {

        private readonly IIdentityUserRepository identityUserRepository;
        private readonly IIdentityRoleRepository identityRoleRepository;

        public OrganizationUnitClaimsPrincipalContributor(IIdentityUserRepository identityUserRepository, IIdentityRoleRepository identityRoleRepository)
        {
            this.identityUserRepository = identityUserRepository;
            this.identityRoleRepository = identityRoleRepository;
        }

        public async virtual Task ContributeAsync(AbpClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault();

            if (claimsIdentity == null)
            {
                return;
            }

            if (claimsIdentity.FindAll(item => item.Type == AbpProOrganizationUnitClaimTypes.OrganizationUnit).Any())
            {
                return;
            }

            var userId = claimsIdentity.FindUserId();

            if (userId == null)
            {
                return;
            }

            var userOus = await identityUserRepository.GetOrganizationUnitsAsync(userId.Value);

            foreach (var userOu in userOus)
            {
                if (!claimsIdentity.HasClaim(AbpProOrganizationUnitClaimTypes.OrganizationUnit, userOu.Code))
                {
                    claimsIdentity.AddClaim(new Claim(AbpProOrganizationUnitClaimTypes.OrganizationUnit, userOu.Code));
                }
            }

            var userRoles = claimsIdentity.FindAll(item => item.Type == AbpClaimTypes.Role).Select(item => item.Value).Distinct().ToList();

            var roleOus = await identityRoleRepository.GetOrganizationUnitsAsync(userRoles);

            foreach (var roleOu in roleOus)
            {
                if (!claimsIdentity.HasClaim(AbpProOrganizationUnitClaimTypes.OrganizationUnit, roleOu.Code))
                {
                    claimsIdentity.AddClaim(new Claim(AbpProOrganizationUnitClaimTypes.OrganizationUnit, roleOu.Code));
                }
            }

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

        }
    }
}
