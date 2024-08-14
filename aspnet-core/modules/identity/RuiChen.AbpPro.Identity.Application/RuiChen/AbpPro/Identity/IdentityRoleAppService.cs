using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace RuiChen.AbpPro.Identity
{
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

        [Authorize(IdentityPermissions.Roles.ManageClaims)]
        public async virtual Task DeleteClaimAsync(Guid id, IdentityRoleClaimDeleteDto input)
        {
            var role = await identityRoleRepository.GetAsync(id);

            role.RemoveClaim(new Claim(input.ClaimType, input.ClaimValue));

            await identityRoleRepository.UpdateAsync(role);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

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

        public async virtual Task<ListResultDto<IdentityClaimDto>> GetClaimsAsync(Guid id)
        {
            var role = await identityRoleRepository.GetAsync(id);

            return new ListResultDto<IdentityClaimDto>(ObjectMapper.Map<ICollection<IdentityRoleClaim>, List<IdentityClaimDto>>(role.Claims));
        }

        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
        {
            var origanizationUnits = await identityRoleRepository.GetOrganizationUnitsAsync(id);

            var items = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnits);

            return new ListResultDto<OrganizationUnitDto>(items);
        }

        public async virtual Task RemoveOrganizationUnitsAsync(Guid id, Guid ouId)
        {
            await organizationUnitManager.RemoveRoleFromOrganizationUnitAsync(id, ouId);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

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
