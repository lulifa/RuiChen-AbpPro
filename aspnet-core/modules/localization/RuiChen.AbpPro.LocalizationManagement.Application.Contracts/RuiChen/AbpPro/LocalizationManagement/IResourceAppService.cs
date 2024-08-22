using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public interface IResourceAppService : IApplicationService
    {
        Task<ResourceDto> GetByNameAsync(string name);

        Task<ResourceDto> CreateAsync(ResourceCreateDto input);

        Task<ResourceDto> UpdateAsync(string name, ResourceUpdateDto input);

        Task DeleteAsync(string name);

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<ResourceDto>> GetListAsync(GetResourceWithFilterDto input);
    }
}
