using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Identity
{
    /// <summary>
    /// 身份角色
    /// </summary>
    [Route("api/identity/roles")]
    public class IdentityRoleController : IdentityControllerBase, IIdentityRoleAppService
    {
        private readonly IIdentityRoleAppService roleAppService;

        public IdentityRoleController(IIdentityRoleAppService roleAppService)
        {
            this.roleAppService = roleAppService;
        }

        /// <summary>
        /// 为指定的角色添加声明
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/claims")]
        public async virtual Task AddClaimAsync(Guid id, IdentityRoleClaimCreateDto input)
        {
            await roleAppService.AddClaimAsync(id, input);
        }

        /// <summary>
        /// 删除指定角色的声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/claims")]
        public async virtual Task DeleteClaimAsync(Guid id, IdentityRoleClaimDeleteDto input)
        {
            await roleAppService.DeleteClaimAsync(id, input);
        }

        /// <summary>
        /// 更新指定角色的声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/claims")]
        public async virtual Task UpdateClaimAsync(Guid id, IdentityRoleClaimUpdateDto input)
        {
            await roleAppService.UpdateClaimAsync(id, input);
        }

        /// <summary>
        /// 获取指定角色的所有声明
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/claims")]
        public async virtual Task<ListResultDto<IdentityClaimDto>> GetClaimsAsync(Guid id)
        {
            return await roleAppService.GetClaimsAsync(id);
        }

        /// <summary>
        /// 获取指定角色关联的所有组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/organization-units")]
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
        {
            return await roleAppService.GetOrganizationUnitsAsync(id);
        }

        /// <summary>
        /// 从指定的组织单元中移除角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/organization-units/{ouId}")]
        public async virtual Task RemoveOrganizationUnitsAsync(Guid id, Guid ouId)
        {
            await roleAppService.RemoveOrganizationUnitsAsync(id, ouId);
        }

        /// <summary>
        /// 设置角色关联的组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/organization-units")]
        public async virtual Task SetOrganizationUnitsAsync(Guid id, IdentityRoleAddOrRemoveOrganizationUnitDto input)
        {
            await roleAppService.SetOrganizationUnitsAsync(id, input);
        }

    }
}
