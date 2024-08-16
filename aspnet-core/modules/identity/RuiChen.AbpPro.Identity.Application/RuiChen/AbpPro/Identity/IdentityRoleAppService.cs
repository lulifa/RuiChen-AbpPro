using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace RuiChen.AbpPro.Identity
{
    /// <summary>
    /// 身份角色的应用服务类
    /// </summary>
    [Authorize(Volo.Abp.Identity.IdentityPermissions.Roles.Default)]
    public class IdentityRoleAppService : IdentityAppServiceBase, IIdentityRoleAppService
    {
        private readonly IIdentityRoleRepository identityRoleRepository;
        private readonly OrganizationUnitManager organizationUnitManager;

        public IdentityRoleAppService(IIdentityRoleRepository identityRoleRepository, OrganizationUnitManager organizationUnitManager)
        {
            this.identityRoleRepository = identityRoleRepository;
            this.organizationUnitManager = organizationUnitManager;
        }

        /// <summary>
        /// 为指定的角色添加声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async virtual Task AddClaimAsync(Guid id, IdentityRoleClaimCreateDto input)
        {
            var role = await identityRoleRepository.GetAsync(id);

            var claim = new Claim(input.ClaimType, input.ClaimValue);

            if (role.FindClaim(claim) != null)
            {
                throw new UserFriendlyException(L["RoleClaimAlreadyExists"]);
            }

            role.AddClaim(GuidGenerator, claim);

            await identityRoleRepository.UpdateAsync(role);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 删除指定角色的声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Roles.ManageClaims)]
        public async virtual Task DeleteClaimAsync(Guid id, IdentityRoleClaimDeleteDto input)
        {
            var role = await identityRoleRepository.GetAsync(id);

            role.RemoveClaim(new Claim(input.ClaimType, input.ClaimValue));

            await identityRoleRepository.UpdateAsync(role);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 更新指定角色的声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task UpdateClaimAsync(Guid id, IdentityRoleClaimUpdateDto input)
        {
            var role = await identityRoleRepository.GetAsync(id);

            var oldClaim = role.FindClaim(new Claim(input.ClaimType, input.ClaimValue));

            if (oldClaim != null)
            {
                role.RemoveClaim(oldClaim.ToClaim());

                role.AddClaim(GuidGenerator, new Claim(input.ClaimType, input.NewClaimValue));

                await identityRoleRepository.UpdateAsync(role);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 获取指定角色的所有声明
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<ListResultDto<IdentityClaimDto>> GetClaimsAsync(Guid id)
        {
            var role = await identityRoleRepository.GetAsync(id);

            return new ListResultDto<IdentityClaimDto>(ObjectMapper.Map<ICollection<IdentityRoleClaim>, List<IdentityClaimDto>>(role.Claims));
        }

        /// <summary>
        /// 获取指定角色关联的所有组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
        {
            var origanizationUnits = await identityRoleRepository.GetOrganizationUnitsAsync(id);

            var items = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnits);

            return new ListResultDto<OrganizationUnitDto>(items);
        }

        /// <summary>
        /// 从指定的组织单元中移除角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        public async virtual Task RemoveOrganizationUnitsAsync(Guid id, Guid ouId)
        {
            await organizationUnitManager.RemoveRoleFromOrganizationUnitAsync(id, ouId);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 设置角色关联的组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task SetOrganizationUnitsAsync(Guid id, IdentityRoleAddOrRemoveOrganizationUnitDto input)
        {
            var origanizationUnits = await identityRoleRepository.GetOrganizationUnitsAsync(id, true);

            var notInRoleOuIds = input.OrganizationUnitIds.Where(ouid => !origanizationUnits.Any(ou => ou.Id.Equals(ouid)));

            foreach (var ouId in notInRoleOuIds)
            {
                await organizationUnitManager.AddRoleToOrganizationUnitAsync(id, ouId);
            }

            var removeRoleOriganzationUnits = origanizationUnits.Where(ou => !input.OrganizationUnitIds.Contains(ou.Id));

            foreach (var origanzationUnit in removeRoleOriganzationUnits)
            {
                origanzationUnit.RemoveRole(id);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }


    }
}
