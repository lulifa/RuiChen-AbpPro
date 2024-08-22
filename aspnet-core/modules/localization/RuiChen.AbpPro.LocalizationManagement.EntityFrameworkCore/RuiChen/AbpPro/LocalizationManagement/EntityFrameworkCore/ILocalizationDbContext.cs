using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace RuiChen.AbpPro.LocalizationManagement
{
    [ConnectionStringName(LocalizationDbProperties.ConnectionStringName)]
    public interface ILocalizationDbContext : IEfCoreDbContext
    {
        DbSet<Resource> Resources { get; }
        DbSet<Language> Languages { get; }
        DbSet<LanguageText> LanguageTexts { get; }
    }
}
