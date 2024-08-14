using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace RuiChen.AbpPro.Identity
{
    public class IdentityUserAppService : IdentityAppServiceBase, IIdentityUserAppService
    {
        private readonly IdentityUserManager userManager;
        private readonly IOptions<IdentityOptions> identityOptions;

        public IdentityUserAppService(IdentityUserManager userManager, IOptions<IdentityOptions> identityOptions)
        {
            this.userManager = userManager;
            this.identityOptions = identityOptions;
        }

        
        [Authorize(IdentityPermissions.Users.ManageClaims)]
        public async virtual Task AddClaimAsync(Guid id, IdentityUserClaimCreateDto input)
        {
            var user = await userManager.GetByIdAsync(id);

            var claim = new Claim(input.ClaimType, input.ClaimValue);

            if (user.FindClaim(claim) != null)
            {
                throw new UserFriendlyException(L["UserClaimAlreadyExists"]);
            }

            user.AddClaim(GuidGenerator, claim);

            (await userManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(IdentityPermissions.Users.ManageClaims)]
        public async virtual Task DeleteClaimAsync(Guid id, IdentityUserClaimDeleteDto input)
        {
            var user = await userManager.GetByIdAsync(id);

            user.RemoveClaim(new Claim(input.ClaimType, input.ClaimValue));

            (await userManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(IdentityPermissions.Users.ManageClaims)]
        public async virtual Task UpdateClaimAsync(Guid id, IdentityUserClaimUpdateDto input)
        {
            var user = await userManager.GetByIdAsync(id);

            var oldClaim = new Claim(input.ClaimType, input.ClaimValue);

            var newClaim = new Claim(input.ClaimType, input.NewClaimValue);

            user.ReplaceClaim(oldClaim, newClaim);

            (await userManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async virtual Task<ListResultDto<IdentityClaimDto>> GetClaimsAsync(Guid id)
        {
            var user = await userManager.GetByIdAsync(id);

            var items = ObjectMapper.Map<ICollection<IdentityUserClaim>, List<IdentityClaimDto>>(user.Claims);

            return new ListResultDto<IdentityClaimDto>(items);
        }

  

        [Authorize(IdentityPermissions.Users.ManageOrganizationUnits)]
        public async virtual Task SetOrganizationUnitsAsync(Guid id, IdentityUserOrganizationUnitUpdateDto input)
        {
            var user = await userManager.GetByIdAsync(id);

            await userManager.SetOrganizationUnitsAsync(user, input.OrganizationUnitIds);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(IdentityPermissions.Users.ManageOrganizationUnits)]
        public async virtual Task RemoveOrganizationUnitsAsync(Guid id, Guid ouId)
        {
            await userManager.RemoveFromOrganizationUnitAsync(id, ouId);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(IdentityPermissions.Users.ManageOrganizationUnits)]
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
        {
            var user = await userManager.GetByIdAsync(id);

            var origanizationUnits = await userManager.GetOrganizationUnitsAsync(user);

            var items = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnits);

            return new ListResultDto<OrganizationUnitDto>(items);
        }


        [Authorize(IdentityPermissions.Users.ResetPassword)]
        public async virtual Task ChangePasswordAsync(Guid id, IdentityUserSetPasswordInput input)
        {
            var user = await GetUserAsync(id);

            if (user.IsExternal)
            {
                throw new BusinessException(code: Volo.Abp.Identity.IdentityErrorCodes.ExternalUserPasswordChange);
            }

            if (user.PasswordHash == null)
            {
                (await userManager.AddPasswordAsync(user, input.Password)).CheckErrors();
            }
            else
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                (await userManager.ResetPasswordAsync(user, token, input.Password)).CheckErrors();
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(Volo.Abp.Identity.IdentityPermissions.Users.Update)]
        public async virtual Task ChangeTwoFactorEnabledAsync(Guid id, TwoFactorEnabledDto input)
        {
            var user = await GetUserAsync(id);

            (await userManager.SetTwoFactorEnabledWithAccountConfirmedAsync(user, input.Enabled)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(Volo.Abp.Identity.IdentityPermissions.Users.Update)]
        public async virtual Task LockAsync(Guid id, int seconds)
        {
            var user = await GetUserAsync(id);

            var endDate = new DateTimeOffset(Clock.Now).AddSeconds(seconds);

            (await userManager.SetLockoutEndDateAsync(user, endDate)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(Volo.Abp.Identity.IdentityPermissions.Users.Update)]
        public async virtual Task UnLockAsync(Guid id)
        {
            var user = await GetUserAsync(id);

            (await userManager.SetLockoutEndDateAsync(user, null)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        protected async virtual Task<IdentityUser> GetUserAsync(Guid id)
        {
            await identityOptions.SetAsync();

            var user = await userManager.GetByIdAsync(id);

            return user;
        }

    }
}
