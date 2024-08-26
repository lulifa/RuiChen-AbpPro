using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.Auditing
{
    public interface ISecurityLogAppService : IApplicationService
    {
        Task<PagedResultDto<SecurityLogDto>> GetListAsync(SecurityLogGetByPagedDto input);

        Task<SecurityLogDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);
    }
}
