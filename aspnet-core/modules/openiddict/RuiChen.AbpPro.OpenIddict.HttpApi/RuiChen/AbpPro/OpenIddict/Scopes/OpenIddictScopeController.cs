using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.OpenIddict
{
    /// <summary>
    /// Openiddict作用域
    /// </summary>
    [Route("api/openiddict/scopes")]
    public class OpenIddictScopeController : OpenIddictControllerBase, IOpenIddictScopeAppService
    {
        private readonly IOpenIddictScopeAppService service;

        public OpenIddictScopeController(IOpenIddictScopeAppService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 创建新的OpenIddict作用域
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<OpenIddictScopeDto> CreateAsync(OpenIddictScopeCreateDto input)
        {
            return service.CreateAsync(input);
        }

        /// <summary>
        /// 删除指定的OpenIddict作用域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return service.DeleteAsync(id);
        }

        /// <summary>
        /// 更新指定的OpenIddict作用域
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<OpenIddictScopeDto> UpdateAsync(Guid id, OpenIddictScopeUpdateDto input)
        {
            return service.UpdateAsync(id, input);
        }

        /// <summary>
        /// 获取指定的OpenIddict作用域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<OpenIddictScopeDto> GetAsync(Guid id)
        {
            return service.GetAsync(id);
        }

        /// <summary>
        /// 分页获取OpenIddict的作用域列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<OpenIddictScopeDto>> GetListAsync(OpenIddictScopeGetListInput input)
        {
            return service.GetListAsync(input);
        }
    }
}
