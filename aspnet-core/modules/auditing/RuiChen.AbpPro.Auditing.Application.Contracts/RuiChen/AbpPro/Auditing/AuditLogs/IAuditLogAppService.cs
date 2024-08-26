using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogDto>> GetListAsync(AuditLogGetByPagedDto input);

        Task<AuditLogDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);
    }
}
