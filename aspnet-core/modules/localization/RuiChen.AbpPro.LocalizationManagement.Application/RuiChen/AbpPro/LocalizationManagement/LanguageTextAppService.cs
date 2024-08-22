using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Localization;
using Volo.Abp.Localization.External;

namespace RuiChen.AbpPro.LocalizationManagement
{
    [Authorize(LocalizationManagementPermissions.LanguageText.Default)]
    public class LanguageTextAppService : LocalizationAppServiceBase, ILanguageTextAppService
    {
        private readonly AbpLocalizationOptions _localizationOptions;
        private readonly IStringLocalizerFactory _localizerFactory;
        private readonly IExternalLocalizationStore _externalLocalizationStore;
        private readonly ILanguageTextRepository _textRepository;

        public LanguageTextAppService(
            IStringLocalizerFactory stringLocalizerFactory,
            IExternalLocalizationStore externalLocalizationStore,
            IOptions<AbpLocalizationOptions> localizationOptions,
            ILanguageTextRepository repository)
        {
            _localizerFactory = stringLocalizerFactory;
            _externalLocalizationStore = externalLocalizationStore;
            _localizationOptions = localizationOptions.Value;
            _textRepository = repository;
        }

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<LanguageTextDto> GetByCultureKeyAsync(GetLanguageTextByKeyInput input)
        {
            var localizer = await _localizerFactory.CreateByResourceNameAsync(input.ResourceName);

            using (CultureHelper.Use(input.CultureName, input.CultureName))
            {
                var result = new LanguageTextDto
                {
                    Key = input.Key,
                    CultureName = input.CultureName,
                    ResourceName = input.ResourceName,
                    Value = localizer[input.Key]?.Value
                };

                return result;
            }
        }

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<ListResultDto<LanguageTextDifferenceDto>> GetListAsync(GetLanguageTextsInput input)
        {
            var result = new List<LanguageTextDifferenceDto>();

            if (input.ResourceName.IsNullOrWhiteSpace())
            {
                var filterResources = _localizationOptions.Resources
                    .Select(r => r.Value)
                    .Union(await _externalLocalizationStore.GetResourcesAsync())
                    .DistinctBy(r => r.ResourceName)
                    .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.ResourceName.Contains(input.Filter))
                    .OrderBy(r => r.ResourceName);

                foreach (var resource in filterResources)
                {
                    result.AddRange(
                        await GetLanguageTextDifferences(resource, input.CultureName, input.TargetCultureName, input.Filter, input.OnlyNull));
                }
            }
            else
            {
                var resource = _localizationOptions.Resources
                    .Select(r => r.Value)
                    .Union(await _externalLocalizationStore.GetResourcesAsync())
                    .DistinctBy(r => r.ResourceName)
                    .Where(l => l.ResourceName.Equals(input.ResourceName))
                    .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.ResourceName.Contains(input.Filter))
                    .FirstOrDefault();
                if (resource != null)
                {
                    result.AddRange(
                        await GetLanguageTextDifferences(resource, input.CultureName, input.TargetCultureName, input.Filter, input.OnlyNull));
                }
            }

            return new ListResultDto<LanguageTextDifferenceDto>(result);
        }

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="cultureName"></param>
        /// <param name="targetCultureName"></param>
        /// <param name="filter"></param>
        /// <param name="onlyNull"></param>
        /// <returns></returns>
        protected async virtual Task<IEnumerable<LanguageTextDifferenceDto>> GetLanguageTextDifferences(
            LocalizationResourceBase resource,
            string cultureName,
            string targetCultureName,
            string filter = null,
            bool? onlyNull = null)
        {
            var result = new List<LanguageTextDifferenceDto>();

            IEnumerable<LocalizedString> localizedStrings = new List<LocalizedString>();
            IEnumerable<LocalizedString> targetLocalizedStrings = new List<LocalizedString>();
            var localizer = await _localizerFactory.CreateByResourceNameAsync(resource.ResourceName);

            using (CultureHelper.Use(cultureName, cultureName))
            {
                localizedStrings = (await localizer.GetAllStringsAsync(true))
                    .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Name.Contains(filter))
                    .OrderBy(l => l.Name);
            }

            if (Equals(cultureName, targetCultureName))
            {
                targetLocalizedStrings = localizedStrings;
            }
            else
            {
                using (CultureHelper.Use(targetCultureName, targetCultureName))
                {
                    targetLocalizedStrings = (await localizer.GetAllStringsAsync(true))
                        .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Name.Contains(filter))
                        .OrderBy(l => l.Name);
                }
            }

            foreach (var localizedString in localizedStrings)
            {
                var targetLocalizedString = targetLocalizedStrings.FirstOrDefault(l => l.Name.Equals(localizedString.Name));
                if (onlyNull == true)
                {
                    if (targetLocalizedString == null || targetLocalizedString.Value.IsNullOrWhiteSpace())
                    {
                        result.Add(new LanguageTextDifferenceDto
                        {
                            CultureName = cultureName,
                            TargetCultureName = targetCultureName,
                            Key = localizedString.Name,
                            Value = localizedString.Value,
                            TargetValue = null,
                            ResourceName = resource.ResourceName
                        });
                    }
                }
                else
                {
                    result.Add(new LanguageTextDifferenceDto
                    {
                        CultureName = cultureName,
                        TargetCultureName = targetCultureName,
                        Key = localizedString.Name,
                        Value = localizedString.Value,
                        TargetValue = targetLocalizedString?.Value,
                        ResourceName = resource.ResourceName
                    });
                }
            }

            return result;
        }


        public async virtual Task SetLanguageTextAsync(SetLanguageTextInput input)
        {
            var text = await _textRepository.GetByCultureKeyAsync(input.ResourceName, input.CultureName, input.Key);
            if (text == null)
            {
                await AuthorizationService.CheckAsync(LocalizationManagementPermissions.LanguageText.Create);

                text = new LanguageText(
                    input.ResourceName,
                    input.CultureName,
                    input.Key,
                    input.Value);

                await _textRepository.InsertAsync(text);
            }
            else
            {
                await AuthorizationService.CheckAsync(LocalizationManagementPermissions.LanguageText.Update);

                text.SetValue(input.Value);

                await _textRepository.UpdateAsync(text);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(LocalizationManagementPermissions.LanguageText.Delete)]
        public async virtual Task RestoreToDefaultAsync(RestoreDefaultLanguageTextInput input)
        {
            var text = await _textRepository.GetByCultureKeyAsync(input.ResourceName, input.CultureName, input.Key);
            if (text != null)
            {
                await _textRepository.DeleteAsync(text);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }
    }
}
