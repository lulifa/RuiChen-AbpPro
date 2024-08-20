using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.FeatureManagement
{
    /// <summary>
    /// 特性定义
    /// </summary>
    [Route("api/feature-management/definitions")]
    public class FeatureDefinitionController : FeatureManagementControllerBase, IFeatureDefinitionAppService
    {
        private readonly IFeatureDefinitionAppService service;

        public FeatureDefinitionController(IFeatureDefinitionAppService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize(FeatureManagementPermissionNames.Definition.Create)]
        public virtual Task<FeatureDefinitionDto> CreateAsync(FeatureDefinitionCreateDto input)
        {
            return service.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{name}")]
        [Authorize(FeatureManagementPermissionNames.GroupDefinition.Delete)]
        public virtual Task DeleteAsync(string name)
        {
            return service.DeleteAsync(name);
        }

        [HttpGet]
        [Route("{name}")]
        [Authorize(FeatureManagementPermissionNames.Definition.Delete)]
        public virtual Task<FeatureDefinitionDto> GetAsync(string name)
        {
            return service.GetAsync(name);
        }

        [HttpGet]
        public virtual Task<ListResultDto<FeatureDefinitionDto>> GetListAsync(FeatureDefinitionGetListInput input)
        {
            return service.GetListAsync(input);
        }

        [HttpPut]
        [Route("{name}")]
        [Authorize(FeatureManagementPermissionNames.Definition.Update)]
        public virtual Task<FeatureDefinitionDto> UpdateAsync(string name, FeatureDefinitionUpdateDto input)
        {
            return service.UpdateAsync(name, input);
        }

    }
}
