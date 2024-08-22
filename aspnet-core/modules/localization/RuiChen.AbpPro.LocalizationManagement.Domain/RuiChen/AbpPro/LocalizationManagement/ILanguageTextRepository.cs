using Volo.Abp.Domain.Repositories;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public interface ILanguageTextRepository : IRepository<LanguageText, int>
    {
        Task<List<string>> GetExistsKeysAsync(
            string resourceName,
            string cultureName,
            IEnumerable<string> keys,
            CancellationToken cancellationToken = default);

        Task<LanguageText> GetByCultureKeyAsync(
            string resourceName,
            string cultureName,
            string key,
            CancellationToken cancellationToken = default
            );

        Task<List<LanguageText>> GetListAsync(
            string resourceName = null,
            string cultureName = null,
            CancellationToken cancellationToken = default);
    }
}
