using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace RuiChen.AbpPro.Saas
{
    public class EditionManager : DomainService
    {
        protected IEditionRepository EditionRepository { get; }

        public EditionManager(IEditionRepository editionRepository)
        {
            EditionRepository = editionRepository;
        }

        public async virtual Task DeleteAsync(Edition edition)
        {
            if (await EditionRepository.CheckUsedByTenantAsync(edition.Id))
            {
                throw new BusinessException(AbpProSaasErrorCodes.DeleteUsedEdition)
                   .WithData(nameof(Edition.DisplayName), edition.DisplayName);
            }
            await EditionRepository.DeleteAsync(edition);
        }

        public async virtual Task<Edition> CreateAsync(string displayName)
        {
            Check.NotNull(displayName, nameof(displayName));

            await ValidateDisplayNameAsync(displayName);
            return new Edition(GuidGenerator.Create(), displayName);
        }

        public async virtual Task ChangeDisplayNameAsync(Edition edition, string displayName)
        {
            Check.NotNull(edition, nameof(edition));
            Check.NotNull(displayName, nameof(displayName));

            await ValidateDisplayNameAsync(displayName, edition.Id);

            edition.SetDisplayName(displayName);
        }

        protected async virtual Task ValidateDisplayNameAsync(string displayName, Guid? expectedId = null)
        {
            var edition = await EditionRepository.FindByDisplayNameAsync(displayName);
            if (edition != null && edition.Id != expectedId)
            {
                throw new BusinessException(AbpProSaasErrorCodes.DuplicateEditionDisplayName)
                    .WithData(nameof(Edition.DisplayName), displayName);
            }
        }
    }
}
