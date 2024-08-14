using AutoMapper;
using Volo.Abp.Identity;

namespace RuiChen.AbpPro.Identity
{
    public class AbpProIdentityApplicationModuleAutoMapperProfile : Profile
    {
        public AbpProIdentityApplicationModuleAutoMapperProfile()
        {
            CreateMap<IdentityClaimType, IdentityClaimTypeDto>().MapExtraProperties();

            CreateMap<IdentityUserClaim, IdentityClaimDto>();

            CreateMap<IdentityRoleClaim, IdentityClaimDto>();

            CreateMap<IdentityUser, IdentityUserDto>().MapExtraProperties();

            CreateMap<IdentityRole, IdentityRoleDto>().MapExtraProperties();

            CreateMap<OrganizationUnit, OrganizationUnitDto>().MapExtraProperties();
        }
    }
}
