using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.OpenIddict
{
    /// <summary>
    /// Openiddict授权
    /// </summary>
    [Route("api/openiddict/authorizations")]
    public class OpenIddictAuthorizationController : OpenIddictControllerBase, IOpenIddictAuthorizationAppService
    {
        private readonly IOpenIddictAuthorizationAppService service;

        public OpenIddictAuthorizationController(IOpenIddictAuthorizationAppService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 删除指定的OpenIddict授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(AbpProOpenIddictPermissions.Authorizations.Delete)]
        public Task DeleteAsync(Guid id)
        {
            return service.DeleteAsync(id);
        }

        /// <summary>
        /// 获取指定的OpenIddict授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<OpenIddictAuthorizationDto> GetAsync(Guid id)
        {
            return service.GetAsync(id);
        }

        /// <summary>
        /// 分页获取OpenIddict授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<OpenIddictAuthorizationDto>> GetListAsync(OpenIddictAuthorizationGetListInput input)
        {
            return service.GetListAsync(input);
        }
    }
}
