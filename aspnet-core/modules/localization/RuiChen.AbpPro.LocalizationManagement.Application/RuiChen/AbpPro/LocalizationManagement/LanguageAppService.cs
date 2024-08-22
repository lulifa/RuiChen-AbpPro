using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Localization;

namespace RuiChen.AbpPro.LocalizationManagement
{
    [Authorize(LocalizationManagementPermissions.Language.Default)]
    public class LanguageAppService : LocalizationAppServiceBase, ILanguageAppService
    {
        private readonly ILanguageProvider _languageProvider;

        private readonly ILanguageRepository _repository;

        public LanguageAppService(ILanguageProvider languageProvider,ILanguageRepository repository)
        {
            _languageProvider = languageProvider;

            _repository = repository;
        }

        /// <summary>
        /// abp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<ListResultDto<LanguageDto>> GetListAsync(GetLanguageWithFilterDto input)
        {
            var languages = (await _languageProvider.GetLanguagesAsync())
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.CultureName.IndexOf(input.Filter, StringComparison.OrdinalIgnoreCase) >= 0
                         || x.UiCultureName.IndexOf(input.Filter, StringComparison.OrdinalIgnoreCase) >= 0
                         || x.DisplayName.IndexOf(input.Filter, StringComparison.OrdinalIgnoreCase) >= 0);

            return new ListResultDto<LanguageDto>(
                languages.Select(l => new LanguageDto
                {
                    CultureName = l.CultureName,
                    UiCultureName = l.UiCultureName,
                    DisplayName = l.DisplayName,
                    FlagIcon = l.FlagIcon
                })
                .OrderBy(l => l.CultureName)
                .DistinctBy(l => l.CultureName)
                .ToList());
        }

        public async virtual Task<LanguageDto> GetByNameAsync(string name)
        {
            var language = await InternalGetByNameAsync(name);

            return ObjectMapper.Map<Language, LanguageDto>(language);
        }

        [Authorize(LocalizationManagementPermissions.Language.Create)]
        public async virtual Task<LanguageDto> CreateAsync(LanguageCreateDto input)
        {
            if (_repository.FindByCultureNameAsync(input.CultureName) != null)
            {
                throw new BusinessException(LocalizationErrorCodes.Language.NameAlreadyExists)
                    .WithData(nameof(Language.CultureName), input.CultureName);
            }

            var language = new Language(
                GuidGenerator.Create(),
                input.CultureName,
                input.UiCultureName,
                input.DisplayName,
                input.FlagIcon);

            language = await _repository.InsertAsync(language);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Language, LanguageDto>(language);
        }

        [Authorize(LocalizationManagementPermissions.Language.Delete)]
        public async virtual Task DeleteAsync(string name)
        {
            var language = await InternalGetByNameAsync(name);

            await _repository.DeleteAsync(language);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(LocalizationManagementPermissions.Language.Update)]
        public async virtual Task<LanguageDto> UpdateAsync(string name, LanguageUpdateDto input)
        {
            var language = await InternalGetByNameAsync(name);

            language.SetFlagIcon(input.FlagIcon);
            language.SetDisplayName(input.DisplayName);

            await _repository.UpdateAsync(language);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Language, LanguageDto>(language);
        }

        private async Task<Language> InternalGetByNameAsync(string name)
        {
            var language = await _repository.FindByCultureNameAsync(name);

            return language ?? throw new BusinessException(LocalizationErrorCodes.Language.NameNotFoundOrStaticNotAllowed)
                    .WithData(nameof(Language.CultureName), name);
        }
    }
}
