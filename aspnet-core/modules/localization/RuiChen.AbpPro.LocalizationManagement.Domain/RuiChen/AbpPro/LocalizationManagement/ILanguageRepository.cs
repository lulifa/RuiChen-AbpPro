using Volo.Abp.Domain.Repositories;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public interface ILanguageRepository : IRepository<Language, Guid>
    {
        Task<Language> FindByCultureNameAsync(string cultureName, CancellationToken cancellationToken = default);

        Task<List<Language>> GetActivedListAsync(CancellationToken cancellationToken = default);
    }
}
