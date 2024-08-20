using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.PermissionManagement
{
    /// <summary>
    /// 权限定义
    /// </summary>
    [Route("api/permission-management/definitions")]
    public class PermissionDefinitionController : PermissionManagementControllerBase, IPermissionDefinitionAppService
    {
        private readonly IPermissionDefinitionAppService service;

        public PermissionDefinitionController(IPermissionDefinitionAppService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize(PermissionManagementPermissionNames.Definition.Create)]
        public virtual Task<PermissionDefinitionDto> CreateAsync(PermissionDefinitionCreateDto input)
        {
            return service.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{name}")]
        [Authorize(PermissionManagementPermissionNames.GroupDefinition.Delete)]
        public virtual Task DeleteAsync(string name)
        {
            return service.DeleteAsync(name);
        }

        [HttpGet]
        [Route("{name}")]
        [Authorize(PermissionManagementPermissionNames.Definition.Delete)]
        public virtual Task<PermissionDefinitionDto> GetAsync(string name)
        {
            return service.GetAsync(name);
        }

        [HttpGet]
        public virtual Task<ListResultDto<PermissionDefinitionDto>> GetListAsync(PermissionDefinitionGetListInput input)
        {
            return service.GetListAsync(input);
        }

        [HttpPut]
        [Route("{name}")]
        [Authorize(PermissionManagementPermissionNames.Definition.Update)]
        public virtual Task<PermissionDefinitionDto> UpdateAsync(string name, PermissionDefinitionUpdateDto input)
        {
            return service.UpdateAsync(name, input);
        }

    }
}
