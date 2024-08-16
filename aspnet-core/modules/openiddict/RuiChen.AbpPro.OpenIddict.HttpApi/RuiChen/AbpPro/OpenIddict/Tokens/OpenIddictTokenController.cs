using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.OpenIddict
{
    /// <summary>
    /// Openiddict令牌
    /// </summary>
    [Route("api/openiddict/tokens")]
    public class OpenIddictTokenController : OpenIddictControllerBase, IOpenIddictTokenAppService
    {
        private readonly IOpenIddictTokenAppService service;

        public OpenIddictTokenController(IOpenIddictTokenAppService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 删除指定的OpenIddict令牌
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(AbpProOpenIddictPermissions.Tokens.Delete)]
        public Task DeleteAsync(Guid id)
        {
            return service.DeleteAsync(id);
        }

        /// <summary>
        /// 获取指定的OpenIddict令牌
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<OpenIddictTokenDto> GetAsync(Guid id)
        {
            return service.GetAsync(id);
        }

        /// <summary>
        /// 分页获取OpenIddict的令牌列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<OpenIddictTokenDto>> GetListAsync(OpenIddictTokenGetListInput input)
        {
            return service.GetListAsync(input);
        }
    }
}
