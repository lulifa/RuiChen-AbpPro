using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;

namespace RuiChen.AbpPro.Identity
{
    /// <summary>
    /// 组织单元的应用服务
    /// </summary>
    public class OrganizationUnitAppService : IdentityAppServiceBase, IOrganizationUnitAppService
    {
        private readonly IdentityUserManager userManager;
        private readonly IIdentityRoleRepository roleRepository;
        private readonly IIdentityUserRepository userRepository;
        private readonly OrganizationUnitManager organizationUnitManager;
        private readonly IOrganizationUnitRepository organizationUnitRepository;

        public OrganizationUnitAppService(IdentityUserManager userManager, IIdentityRoleRepository roleRepository, IIdentityUserRepository userRepository, OrganizationUnitManager organizationUnitManager, IOrganizationUnitRepository organizationUnitRepository)
        {
            this.userManager = userManager;
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
            this.organizationUnitManager = organizationUnitManager;
            this.organizationUnitRepository = organizationUnitRepository;
        }

        /// <summary>
        /// 创建新的组织单元
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.Create)]
        public async virtual Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            var origanizationUnit = new OrganizationUnit(GuidGenerator.Create(), input.DisplayName, input.ParentId, CurrentTenant.Id)
            {
                CreationTime = Clock.Now
            };
            input.MapExtraPropertiesTo(origanizationUnit);

            await organizationUnitManager.CreateAsync(origanizationUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(origanizationUnit);
        }

        /// <summary>
        /// 删除指定的组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.Delete)]
        public async virtual Task DeleteAsync(Guid id)
        {
            var origanizationUnit = await organizationUnitRepository.FindAsync(id);

            if (origanizationUnit == null)
            {
                return;
            }
            await organizationUnitManager.DeleteAsync(id);
        }

        /// <summary>
        /// 获取根组织单元列表
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetRootAsync()
        {
            var rootOriganizationUnits = await organizationUnitManager.FindChildrenAsync(null, recursive: false);

            var items = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(rootOriganizationUnits);

            return new ListResultDto<OrganizationUnitDto>(items);
        }

        /// <summary>
        /// 获取指定的组织单元子集列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<ListResultDto<OrganizationUnitDto>> FindChildrenAsync(OrganizationUnitGetChildrenDto input)
        {
            var origanizationUnitChildren = await organizationUnitManager.FindChildrenAsync(input.Id, input.Recursive);

            var items = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnitChildren);

            return new ListResultDto<OrganizationUnitDto>(items);
        }

        /// <summary>
        /// 根据唯一标识符获取组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            var origanizationUnit = await organizationUnitRepository.FindAsync(id);

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(origanizationUnit);
        }

        /// <summary>
        /// 获取组织单元子集中的最后一个
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async virtual Task<OrganizationUnitDto> GetLastChildOrNullAsync(Guid? parentId)
        {
            var origanizationUnitLastChildren = await organizationUnitManager.GetLastChildOrNullAsync(parentId);

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(origanizationUnitLastChildren);
        }

        /// <summary>
        /// 获取所有的组织单元列表
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync(GetOrganizationUnitsAdvancedInput input)
        {

            var origanizationUnits = await organizationUnitRepository.GetListAsync(false);

            origanizationUnits = origanizationUnits.WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                                                             item => item.DisplayName.Contains(input.Filter) || 
                                                             item.Code.Contains(input.Filter)).ToList();

            var items = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnits);

            return new ListResultDto<OrganizationUnitDto>(items);
        }

        /// <summary>
        /// 分页获取组织单元类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(OrganizationUnitGetByPagedDto input)
        {
            var specification = new OrganizationUnitGetListSpecification(input);

            var origanizationUnitCount = await organizationUnitRepository.GetCountAsync(specification);

            var origanizationUnits = await organizationUnitRepository.GetListAsync(specification, input.Sorting, input.MaxResultCount, input.SkipCount, false);

            var items = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnits);

            return new PagedResultDto<OrganizationUnitDto>(origanizationUnitCount, items);
        }

        /// <summary>
        /// 获取组织单元角色名称列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task<ListResultDto<string>> GetRoleNamesAsync(Guid id)
        {
            var inOrignizationUnitRoleNames = await userRepository.GetRoleNamesInOrganizationUnitAsync(id);

            return new ListResultDto<string>(inOrignizationUnitRoleNames);
        }

        /// <summary>
        /// 获取组织单元未添加的角色列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task<PagedResultDto<IdentityRoleDto>> GetUnaddedRolesAsync(Guid id, OrganizationUnitGetUnaddedRoleByPagedDto input)
        {
            var origanizationUnit = await organizationUnitRepository.GetAsync(id);

            var origanizationUnitRoleCount = await organizationUnitRepository.GetUnaddedRolesCountAsync(origanizationUnit, input.Filter);

            var origanizationUnitRoles = await organizationUnitRepository.GetUnaddedRolesAsync(origanizationUnit, input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            var items = ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(origanizationUnitRoles);

            return new PagedResultDto<IdentityRoleDto>(origanizationUnitRoleCount, items);
        }

        /// <summary>
        /// 分页获取组织单元的角色列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid id, PagedAndSortedResultRequestDto input)
        {
            var origanizationUnit = await organizationUnitRepository.GetAsync(id);

            var origanizationUnitRoleCount = await organizationUnitRepository.GetRolesCountAsync(origanizationUnit);

            var origanizationUnitRoles = await organizationUnitRepository.GetRolesAsync(origanizationUnit, input.Sorting, input.MaxResultCount, input.SkipCount);

            var items = ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(origanizationUnitRoles);

            return new PagedResultDto<IdentityRoleDto>(origanizationUnitRoleCount, items);
        }


        /// <summary>
        /// 分页获取组织单元未添加的用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.ManageUsers)]
        public async virtual Task<PagedResultDto<IdentityUserDto>> GetUnaddedUsersAsync(Guid id, OrganizationUnitGetUnaddedUserByPagedDto input)
        {
            var origanizationUnit = await organizationUnitRepository.GetAsync(id);

            var origanizationUnitUserCount = await organizationUnitRepository.GetUnaddedUsersCountAsync(origanizationUnit, input.Filter);

            var origanizationUnitUsers = await organizationUnitRepository.GetUnaddedUsersAsync(origanizationUnit, input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            var items = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(origanizationUnitUsers);

            return new PagedResultDto<IdentityUserDto>(origanizationUnitUserCount, items);
        }

        /// <summary>
        /// 分页获取组织单元的用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.ManageUsers)]
        public async virtual Task<PagedResultDto<IdentityUserDto>> GetUsersAsync(Guid id, GetIdentityUsersInput input)
        {
            var origanizationUnit = await organizationUnitRepository.GetAsync(id);

            var origanizationUnitUserCount = await organizationUnitRepository.GetMembersCountAsync(origanizationUnit, input.Filter);

            var origanizationUnitUsers = await organizationUnitRepository.GetMembersAsync(origanizationUnit, input.Sorting, input.MaxResultCount,
                input.SkipCount, input.Filter);

            var items = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(origanizationUnitUsers);

            return new PagedResultDto<IdentityUserDto>(origanizationUnitUserCount, items);
        }

        /// <summary>
        /// 移动组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.Update)]
        public async virtual Task MoveAsync(Guid id, OrganizationUnitMoveDto input)
        {
            await organizationUnitManager.MoveAsync(id, input.ParentId);
        }

        /// <summary>
        /// 更新指定的组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.Update)]
        public async virtual Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            var origanizationUnit = await organizationUnitRepository.GetAsync(id);

            origanizationUnit.DisplayName = input.DisplayName;

            input.MapExtraPropertiesTo(origanizationUnit);

            await organizationUnitManager.UpdateAsync(origanizationUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(origanizationUnit);
        }

        /// <summary>
        /// 新增组织单元用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.ManageUsers)]
        public async virtual Task AddUsersAsync(Guid id, OrganizationUnitAddUserDto input)
        {
            var origanizationUnit = await organizationUnitRepository.GetAsync(id);

            var users = await userRepository.GetListByIdListAsync(input.UserIds, includeDetails: true);

            // 调用内部方法设置用户组织机构
            foreach (var user in users)
            {
                await userManager.AddToOrganizationUnitAsync(user, origanizationUnit);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 新增组织单元角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task AddRolesAsync(Guid id, OrganizationUnitAddRoleDto input)
        {
            var origanizationUnit = await organizationUnitRepository.GetAsync(id);

            var roles = await roleRepository.GetListByIdListAsync(input.RoleIds, includeDetails: true);

            foreach (var role in roles)
            {
                await organizationUnitManager.AddRoleToOrganizationUnitAsync(role, origanizationUnit);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

    }
}
