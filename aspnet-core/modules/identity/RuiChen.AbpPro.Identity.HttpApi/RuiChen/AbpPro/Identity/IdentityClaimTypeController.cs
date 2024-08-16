using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Identity
{
    /// <summary>
    /// 身份声明类型
    /// </summary>
    [Route("api/identity/claim-types")]
    public class IdentityClaimTypeController : IdentityControllerBase, IIdentityClaimTypeAppService
    {
        private readonly IIdentityClaimTypeAppService identityClaimTypeAppService;

        public IdentityClaimTypeController(IIdentityClaimTypeAppService identityClaimTypeAppService)
        {
            this.identityClaimTypeAppService = identityClaimTypeAppService;
        }

        /// <summary>
        /// 创建新的身份声明类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task<IdentityClaimTypeDto> CreateAsync(IdentityClaimTypeCreateDto input)
        {
            return await identityClaimTypeAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除指定的身份声明类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async virtual Task DeleteAsync(Guid id)
        {
            await identityClaimTypeAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取所有身份声明类型的列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("actived-list")]
        public async virtual Task<ListResultDto<IdentityClaimTypeDto>> GetAllListAsync()
        {
            return await identityClaimTypeAppService.GetAllListAsync();
        }

        /// <summary>
        /// 根据唯一标识符获取身份声明类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async virtual Task<IdentityClaimTypeDto> GetAsync(Guid id)
        {
            return await identityClaimTypeAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页获取身份声明类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async virtual Task<PagedResultDto<IdentityClaimTypeDto>> GetListAsync(IdentityClaimTypeGetByPagedDto input)
        {
            return await identityClaimTypeAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新指定的身份声明类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async virtual Task<IdentityClaimTypeDto> UpdateAsync(Guid id, IdentityClaimTypeUpdateDto input)
        {
            return await identityClaimTypeAppService.UpdateAsync(id, input);
        }

    }
}
