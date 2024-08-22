using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public class EfCoreLanguageRepository : EfCoreRepository<LocalizationDbContext, Language, Guid>, ILanguageRepository
    {
        public EfCoreLanguageRepository(IDbContextProvider<LocalizationDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async virtual Task<Language> FindByCultureNameAsync(
            string cultureName,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).Where(x => x.CultureName.Equals(cultureName))
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public async virtual Task<List<Language>> GetActivedListAsync(CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).Where(x => x.Enable)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

    }
}
