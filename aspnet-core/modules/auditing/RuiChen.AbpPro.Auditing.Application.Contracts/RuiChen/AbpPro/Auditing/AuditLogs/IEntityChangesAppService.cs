using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.Auditing
{
    public interface IEntityChangesAppService : IApplicationService
    {
        Task<EntityChangeDto> GetAsync(Guid id);

        Task<EntityChangeWithUsernameDto> GetWithUsernameAsync(Guid id);

        Task<PagedResultDto<EntityChangeDto>> GetListAsync(EntityChangeGetByPagedDto input);

        Task<ListResultDto<EntityChangeWithUsernameDto>> GetWithUsernameAsync(EntityChangeGetWithUsernameDto input);
    }
}
