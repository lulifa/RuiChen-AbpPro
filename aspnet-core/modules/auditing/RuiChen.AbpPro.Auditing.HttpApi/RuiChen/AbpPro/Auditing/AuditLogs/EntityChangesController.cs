using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace RuiChen.AbpPro.Auditing
{
    /// <summary>
    /// 实体更改管理
    /// </summary>
    [Authorize(AuditingPermissionNames.AuditLog.Default)]
    [Route("api/auditing/entity-changes")]
    public class EntityChangesController : AbpControllerBase, IEntityChangesAppService
    {
        protected IEntityChangesAppService EntityChangeAppService { get; }

        public EntityChangesController(
            IEntityChangesAppService entityChangeAppService)
        {
            EntityChangeAppService = entityChangeAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<EntityChangeDto> GetAsync(Guid id)
        {
            return EntityChangeAppService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<EntityChangeDto>> GetListAsync(EntityChangeGetByPagedDto input)
        {
            return EntityChangeAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-username/{id}")]
        public Task<EntityChangeWithUsernameDto> GetWithUsernameAsync(Guid id)
        {
            return EntityChangeAppService.GetWithUsernameAsync(id);
        }

        [HttpGet]
        [Route("with-username")]
        public Task<ListResultDto<EntityChangeWithUsernameDto>> GetWithUsernameAsync(EntityChangeGetWithUsernameDto input)
        {
            return EntityChangeAppService.GetWithUsernameAsync(input);
        }
    }
}
