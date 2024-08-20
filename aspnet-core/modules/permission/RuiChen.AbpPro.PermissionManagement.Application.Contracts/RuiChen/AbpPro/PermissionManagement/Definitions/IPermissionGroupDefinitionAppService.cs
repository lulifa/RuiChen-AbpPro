using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.PermissionManagement
{
    public interface IPermissionGroupDefinitionAppService : IApplicationService
    {
        Task<PermissionGroupDefinitionDto> GetAsync(string name);

        Task DeleteAsync(string name);

        Task<PermissionGroupDefinitionDto> CreateAsync(PermissionGroupDefinitionCreateDto input);

        Task<PermissionGroupDefinitionDto> UpdateAsync(string name, PermissionGroupDefinitionUpdateDto input);

        Task<ListResultDto<PermissionGroupDefinitionDto>> GetListAsync(PermissionGroupDefinitionGetListInput input);
    }
}
