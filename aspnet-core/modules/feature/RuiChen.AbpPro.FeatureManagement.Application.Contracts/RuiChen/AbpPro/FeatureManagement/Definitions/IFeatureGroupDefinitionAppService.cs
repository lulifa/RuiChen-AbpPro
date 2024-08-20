using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.FeatureManagement
{
    public interface IFeatureGroupDefinitionAppService : IApplicationService
    {
        Task<FeatureGroupDefinitionDto> GetAsync(string name);

        Task DeleteAsync(string name);

        Task<FeatureGroupDefinitionDto> CreateAsync(FeatureGroupDefinitionCreateDto input);

        Task<FeatureGroupDefinitionDto> UpdateAsync(string name, FeatureGroupDefinitionUpdateDto input);

        Task<ListResultDto<FeatureGroupDefinitionDto>> GetListAsync(FeatureGroupDefinitionGetListInput input);
    }
}
