using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Auditing
{
    /// <summary>
    /// 审计日志
    /// </summary>
    [Authorize(AuditingPermissionNames.AuditLog.Default)]
    [Route("api/auditing/audit-log")]
    public class AuditLogController : AuditingControllerBase, IAuditLogAppService
    {
        protected IAuditLogAppService AuditLogAppService { get; }

        public AuditLogController(IAuditLogAppService auditLogAppService)
        {
            AuditLogAppService = auditLogAppService;
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuditingPermissionNames.AuditLog.Delete)]
        public async virtual Task DeleteAsync(Guid id)
        {
            await AuditLogAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public async virtual Task<AuditLogDto> GetAsync(Guid id)
        {
            return await AuditLogAppService.GetAsync(id);
        }

        [HttpGet]
        public async virtual Task<PagedResultDto<AuditLogDto>> GetListAsync(AuditLogGetByPagedDto input)
        {
            return await AuditLogAppService.GetListAsync(input);
        }
    }
}
