using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Identity
{
    /// <summary>
    /// 身份用户
    /// </summary>
    [Route("api/identity/users")]
    public class IdentityUserController : IdentityControllerBase, IIdentityUserAppService
    {
        private readonly IIdentityUserAppService userAppService;

        public IdentityUserController(IIdentityUserAppService userAppService)
        {
            this.userAppService = userAppService;
        }

        /// <summary>
        /// 为指定的用户添加声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/claims")]
        public async virtual Task AddClaimAsync(Guid id, IdentityUserClaimCreateDto input)
        {
            await userAppService.AddClaimAsync(id, input);
        }

        /// <summary>
        /// 删除指定用户的声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/claims")]
        public async virtual Task DeleteClaimAsync(Guid id, IdentityUserClaimDeleteDto input)
        {
            await userAppService.DeleteClaimAsync(id, input);
        }

        /// <summary>
        /// 更新指定用户的声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/claims")]
        public async virtual Task UpdateClaimAsync(Guid id, IdentityUserClaimUpdateDto input)
        {
            await userAppService.UpdateClaimAsync(id, input);
        }

        /// <summary>
        /// 获取指定用户的所有声明
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/claims")]
        public async virtual Task<ListResultDto<IdentityClaimDto>> GetClaimsAsync(Guid id)
        {
            return await userAppService.GetClaimsAsync(id);
        }

        /// <summary>
        /// 获取指定用户关联的所有组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/organization-units")]
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
        {
            return await userAppService.GetOrganizationUnitsAsync(id);
        }

        /// <summary>
        /// 从指定的组织单元中移除用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/organization-units/{ouId}")]
        public async virtual Task RemoveOrganizationUnitsAsync(Guid id, Guid ouId)
        {
            await userAppService.RemoveOrganizationUnitsAsync(id, ouId);
        }

        /// <summary>
        /// 设置用户关联的组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/organization-units")]
        public async virtual Task SetOrganizationUnitsAsync(Guid id, IdentityUserOrganizationUnitUpdateDto input)
        {
            await userAppService.SetOrganizationUnitsAsync(id, input);
        }

        /// <summary>
        /// 更改用户密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("change-password")]
        public async virtual Task ChangePasswordAsync(Guid id, IdentityUserSetPasswordInput input)
        {
            await userAppService.ChangePasswordAsync(id, input);
        }

        /// <summary>
        /// 更改双因素
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("change-two-factor")]
        public async virtual Task ChangeTwoFactorEnabledAsync(Guid id, TwoFactorEnabledDto input)
        {
            await userAppService.ChangeTwoFactorEnabledAsync(id, input);
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/lock/{seconds}")]
        public async virtual Task LockAsync(Guid id, int seconds)
        {
            await userAppService.LockAsync(id, seconds);
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/unlock")]
        public async virtual Task UnLockAsync(Guid id)
        {
            await userAppService.UnLockAsync(id);
        }

    }
}
