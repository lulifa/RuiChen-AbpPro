using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.Auditing
{
    public interface ILogAppService : IApplicationService
    {
        Task<LogDto> GetAsync(string id);

        Task<PagedResultDto<LogDto>> GetListAsync(LogGetByPagedDto input);
    }
}
