using Microsoft.Extensions.Localization;
using Volo.Abp.Localization;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public interface ILocalizationStoreCache
    {
        string CacheStamp { get; set; }

        SemaphoreSlim SyncSemaphore { get; }

        DateTime? LastCheckTime { get; set; }

        Task InitializeAsync(LocalizationStoreCacheInitializeContext context);

        LocalizationResourceBase GetResourceOrNull(string resourceName);

        LocalizedString GetLocalizedStringOrNull(string resourceName, string cultureName, string name);

        IReadOnlyList<LocalizationResourceBase> GetResources();
        IReadOnlyList<LanguageInfo> GetLanguages();

        IDictionary<string, LocalizationDictionary> GetAllLocalizedStrings(string cultureName);
    }
}
