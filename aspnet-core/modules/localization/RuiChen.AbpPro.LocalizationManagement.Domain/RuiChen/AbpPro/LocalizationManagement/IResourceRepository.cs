using Volo.Abp.Domain.Repositories;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public interface IResourceRepository : IRepository<Resource, Guid>
    {
        Task<bool> ExistsAsync(
            string name,
            CancellationToken cancellationToken = default);

        Task<Resource> FindByNameAsync(
            string name,
            CancellationToken cancellationToken = default);
    }
}
