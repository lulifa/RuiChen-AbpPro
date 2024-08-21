using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.Saas
{
    public interface ITenantAppService :
            ICrudAppService<
                TenantDto,
                Guid,
                TenantGetListInput,
                TenantCreateDto,
                TenantUpdateDto>
    {
        Task<TenantDto> GetAsync(
            [Required] string name);

        Task<TenantConnectionStringDto> GetConnectionStringAsync(
            Guid id,
            [Required] string connectionName);

        Task<ListResultDto<TenantConnectionStringDto>> GetConnectionStringAsync(
            Guid id);

        Task<TenantConnectionStringDto> SetConnectionStringAsync(
            Guid id,
            TenantConnectionStringCreateOrUpdate input);

        Task DeleteConnectionStringAsync(
            Guid id,
            [Required] string connectionName);
    }
}
