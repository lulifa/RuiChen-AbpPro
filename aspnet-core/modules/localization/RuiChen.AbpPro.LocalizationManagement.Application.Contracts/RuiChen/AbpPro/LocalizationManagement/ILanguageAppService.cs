using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public interface ILanguageAppService : IApplicationService
    {
        Task<LanguageDto> GetByNameAsync(string name);

        Task<LanguageDto> CreateAsync(LanguageCreateDto input);

        Task<LanguageDto> UpdateAsync(string name, LanguageUpdateDto input);

        Task DeleteAsync(string name);

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<LanguageDto>> GetListAsync(GetLanguageWithFilterDto input);

    }
}
