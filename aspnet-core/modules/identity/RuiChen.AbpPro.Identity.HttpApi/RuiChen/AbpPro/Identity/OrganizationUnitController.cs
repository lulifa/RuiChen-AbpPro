using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace RuiChen.AbpPro.Identity
{
    /// <summary>
    /// 组织单元
    /// </summary>
    [Route("api/identity/organization-units")]
    public class OrganizationUnitController : IdentityControllerBase, IOrganizationUnitAppService
    {
        private readonly IOrganizationUnitAppService organizationUnitAppService;

        public OrganizationUnitController(IOrganizationUnitAppService organizationUnitAppService)
        {
            this.organizationUnitAppService = organizationUnitAppService;
        }

        /// <summary>
        /// 创建新的组织单元
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            return await organizationUnitAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除指定的组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async virtual Task DeleteAsync(Guid id)
        {
            await organizationUnitAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取根组织单元列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("root-node")]
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetRootAsync()
        {
            return await organizationUnitAppService.GetRootAsync();
        }

        /// <summary>
        /// 获取指定的组织单元子集列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("find-children")]
        public async virtual Task<ListResultDto<OrganizationUnitDto>> FindChildrenAsync(OrganizationUnitGetChildrenDto input)
        {
            return await organizationUnitAppService.FindChildrenAsync(input);
        }

        /// <summary>
        /// 根据唯一标识符获取组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async virtual Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return await organizationUnitAppService.GetAsync(id);
        }


        /// <summary>
        /// 获取组织单元子集中的最后一个
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("last-children")]
        public async virtual Task<OrganizationUnitDto> GetLastChildOrNullAsync(Guid? parentId)
        {
            return await organizationUnitAppService.GetLastChildOrNullAsync(parentId);
        }

        /// <summary>
        /// 获取所有的组织单元列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync()
        {
            return await organizationUnitAppService.GetAllListAsync();
        }

        /// <summary>
        /// 分页获取组织单元类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async virtual Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(OrganizationUnitGetByPagedDto input)
        {
            return await organizationUnitAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取组织单元角色名称列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/role-names")]
        public async virtual Task<ListResultDto<string>> GetRoleNamesAsync(Guid id)
        {
            return await organizationUnitAppService.GetRoleNamesAsync(id);
        }

        /// <summary>
        /// 获取组织单元未添加的角色列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/unadded-roles")]
        public async virtual Task<PagedResultDto<IdentityRoleDto>> GetUnaddedRolesAsync(Guid id, OrganizationUnitGetUnaddedRoleByPagedDto input)
        {
            return await organizationUnitAppService.GetUnaddedRolesAsync(id, input);
        }

        /// <summary>
        /// 分页获取组织单元的角色列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/roles")]
        public async virtual Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid id, PagedAndSortedResultRequestDto input)
        {
            return await organizationUnitAppService.GetRolesAsync(id, input);
        }

        /// <summary>
        /// 分页获取组织单元未添加的用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/unadded-users")]
        public async virtual Task<PagedResultDto<IdentityUserDto>> GetUnaddedUsersAsync(Guid id, OrganizationUnitGetUnaddedUserByPagedDto input)
        {
            return await organizationUnitAppService.GetUnaddedUsersAsync(id, input);
        }

        /// <summary>
        /// 分页获取组织单元的用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/users")]
        public async virtual Task<PagedResultDto<IdentityUserDto>> GetUsersAsync(Guid id, GetIdentityUsersInput input)
        {
            return await organizationUnitAppService.GetUsersAsync(id, input);
        }

        /// <summary>
        /// 移动组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/move")]
        public async virtual Task MoveAsync(Guid id, OrganizationUnitMoveDto input)
        {
            await organizationUnitAppService.MoveAsync(id, input);
        }

        /// <summary>
        /// 更新指定的组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async virtual Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            return await organizationUnitAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 新增组织单元用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/users")]
        public async virtual Task AddUsersAsync(Guid id, OrganizationUnitAddUserDto input)
        {
            await organizationUnitAppService.AddUsersAsync(id, input);
        }

        /// <summary>
        /// 新增组织单元角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/roles")]
        public async virtual Task AddRolesAsync(Guid id, OrganizationUnitAddRoleDto input)
        {
            await organizationUnitAppService.AddRolesAsync(id, input);
        }

    }
}
