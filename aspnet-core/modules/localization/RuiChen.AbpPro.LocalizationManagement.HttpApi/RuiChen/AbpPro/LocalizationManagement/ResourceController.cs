using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.LocalizationManagement
{
    [Route("api/localization/resources")]
    public class ResourceController : LocalizationControllerBase, IResourceAppService
    {
        private readonly IResourceAppService _service;

        public ResourceController(IResourceAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual Task<ListResultDto<ResourceDto>> GetListAsync(GetResourceWithFilterDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpGet]
        [Route("{name}")]
        public virtual Task<ResourceDto> GetByNameAsync(string name)
        {
            return _service.GetByNameAsync(name);
        }

        [HttpPost]
        [Authorize(LocalizationManagementPermissions.Resource.Create)]
        public virtual Task<ResourceDto> CreateAsync(ResourceCreateDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{name}")]
        [Authorize(LocalizationManagementPermissions.Resource.Delete)]
        public virtual Task DeleteAsync(string name)
        {
            return _service.DeleteAsync(name);
        }

        [HttpPut]
        [Route("{name}")]
        [Authorize(LocalizationManagementPermissions.Resource.Update)]
        public virtual Task<ResourceDto> UpdateAsync(string name, ResourceUpdateDto input)
        {
            return _service.UpdateAsync(name, input);
        }
    }
}
