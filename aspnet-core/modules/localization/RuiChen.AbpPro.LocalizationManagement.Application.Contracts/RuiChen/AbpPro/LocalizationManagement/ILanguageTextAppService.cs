using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public interface ILanguageTextAppService : IApplicationService
    {
        Task SetLanguageTextAsync(SetLanguageTextInput input);

        Task RestoreToDefaultAsync(RestoreDefaultLanguageTextInput input);

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LanguageTextDto> GetByCultureKeyAsync(GetLanguageTextByKeyInput input);

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<LanguageTextDifferenceDto>> GetListAsync(GetLanguageTextsInput input);

    }
}
