using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.LocalizationManagement
{
    /// <summary>
    /// 文本管理
    /// </summary>
    [Route("api/localization/texts")]
    public class LanguageTextController : LocalizationControllerBase, ILanguageTextAppService
    {
        private readonly ILanguageTextAppService _service;

        public LanguageTextController(ILanguageTextAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("by-culture-key")]
        public virtual Task<LanguageTextDto> GetByCultureKeyAsync(GetLanguageTextByKeyInput input)
        {
            return _service.GetByCultureKeyAsync(input);
        }

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual Task<ListResultDto<LanguageTextDifferenceDto>> GetListAsync(GetLanguageTextsInput input)
        {
            return _service.GetListAsync(input);
        }


        [HttpPut]
        public virtual Task SetLanguageTextAsync(SetLanguageTextInput input)
        {
            return _service.SetLanguageTextAsync(input);
        }

        [HttpDelete]
        [Route("restore-to-default")]
        public virtual Task RestoreToDefaultAsync(RestoreDefaultLanguageTextInput input)
        {
            return _service.RestoreToDefaultAsync(input);
        }

    }
}
