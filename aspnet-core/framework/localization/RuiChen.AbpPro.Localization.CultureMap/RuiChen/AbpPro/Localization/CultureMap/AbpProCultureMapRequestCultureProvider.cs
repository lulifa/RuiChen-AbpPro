using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace RuiChen.AbpPro.Localization
{
    public class AbpProCultureMapRequestCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var option = httpContext.RequestServices.GetRequiredService<IOptions<AbpProLocalizationCultureMapOptions>>().Value;

            var mapCultures = new List<StringSegment>();
            var mapUiCultures = new List<StringSegment>();

            var requestLocalizationOptionsProvider = httpContext.RequestServices.GetRequiredService<IAbpRequestLocalizationOptionsProvider>();
            foreach (var provider in (await requestLocalizationOptionsProvider.GetLocalizationOptionsAsync()).RequestCultureProviders)
            {
                if (provider == this)
                {
                    continue;
                }

                var providerCultureResult = await provider.DetermineProviderCultureResult(httpContext);
                if (providerCultureResult == null)
                {
                    continue;
                }

                mapCultures.AddRange(providerCultureResult.Cultures.Where(x => x.HasValue)
                    .Select(culture =>
                    {
                        var map = option.CulturesMaps.FirstOrDefault(x =>
                            x.SourceCultures.Contains(culture.Value, StringComparer.OrdinalIgnoreCase));
                        return new StringSegment(map?.TargetCulture ?? culture.Value);
                    }));

                mapUiCultures.AddRange(providerCultureResult.UICultures.Where(x => x.HasValue)
                    .Select(culture =>
                    {
                        var map = option.UiCulturesMaps.FirstOrDefault(x =>
                            x.SourceCultures.Contains(culture.Value, StringComparer.OrdinalIgnoreCase));
                        return new StringSegment(map?.TargetCulture ?? culture.Value);
                    }));
            }

            return new ProviderCultureResult(mapCultures, mapUiCultures);
        }
    }
}
