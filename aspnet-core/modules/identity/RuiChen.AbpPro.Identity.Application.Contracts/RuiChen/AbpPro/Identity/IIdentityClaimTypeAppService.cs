using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.Identity
{
    public interface IIdentityClaimTypeAppService : 
        ICrudAppService<
            IdentityClaimTypeDto,
            Guid,
            IdentityClaimTypeGetByPagedDto,
            IdentityClaimTypeCreateDto,
            IdentityClaimTypeUpdateDto>
    {
        Task<ListResultDto<IdentityClaimTypeDto>> GetAllListAsync();
    }
}
