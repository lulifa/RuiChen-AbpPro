using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.LocalizationManagement
{
    /// <summary>
    /// 语言管理
    /// </summary>
    [Route("api/localization/languages")]
    public class LanguageController : LocalizationControllerBase, ILanguageAppService
    {
        private readonly ILanguageAppService _service;

        public LanguageController(ILanguageAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual Task<ListResultDto<LanguageDto>> GetListAsync(GetLanguageWithFilterDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpGet]
        [Route("{name}")]
        public virtual Task<LanguageDto> GetByNameAsync(string name)
        {
            return _service.GetByNameAsync(name);
        }

        [HttpPost]
        [Authorize(LocalizationManagementPermissions.Language.Create)]
        public virtual Task<LanguageDto> CreateAsync(LanguageCreateDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{name}")]
        [Authorize(LocalizationManagementPermissions.Language.Delete)]
        public virtual Task DeleteAsync(string name)
        {
            return _service.DeleteAsync(name);
        }

        [HttpPut]
        [Route("{name}")]
        [Authorize(LocalizationManagementPermissions.Language.Update)]
        public virtual Task<LanguageDto> UpdateAsync(string name, LanguageUpdateDto input)
        {
            return _service.UpdateAsync(name, input);
        }

    }
}
