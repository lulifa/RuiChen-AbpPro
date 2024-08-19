using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.OpenIddict
{
    /// <summary>
    /// Openiddict应用
    /// </summary>
    [Route("api/openiddict/applications")]
    public class OpenIddictApplicationController : OpenIddictControllerBase, IOpenIddictApplicationAppService
    {
        private readonly IOpenIddictApplicationAppService service;

        public OpenIddictApplicationController(IOpenIddictApplicationAppService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 创建新的OpenIddict应用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AbpProOpenIddictPermissions.Applications.Create)]
        public virtual Task<OpenIddictApplicationDto> CreateAsync(OpenIddictApplicationCreateDto input)
        {
            return service.CreateAsync(input);
        }

        /// <summary>
        /// 删除指定的OpenIddict应用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(AbpProOpenIddictPermissions.Applications.Delete)]
        public virtual Task DeleteAsync(Guid id)
        {
            return service.DeleteAsync(id);
        }

        /// <summary>
        /// 更新指定的OpenIddict应用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [Authorize(AbpProOpenIddictPermissions.Applications.Update)]
        public virtual Task<OpenIddictApplicationDto> UpdateAsync(Guid id, OpenIddictApplicationUpdateDto input)
        {
            return service.UpdateAsync(id, input);
        }

        /// <summary>
        /// 获取指定的OpenIddict应用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(AbpProOpenIddictPermissions.Applications.Default)]
        public virtual Task<OpenIddictApplicationDto> GetAsync(Guid id)
        {
            return service.GetAsync(id);
        }

        /// <summary>
        /// 分页获取OpenIddict的应用列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(AbpProOpenIddictPermissions.Applications.Default)]
        [AllowAnonymous]
        public virtual Task<PagedResultDto<OpenIddictApplicationDto>> GetListAsync(OpenIddictApplicationGetListInput input)
        {
            return service.GetListAsync(input);
        }
       
    }
}
