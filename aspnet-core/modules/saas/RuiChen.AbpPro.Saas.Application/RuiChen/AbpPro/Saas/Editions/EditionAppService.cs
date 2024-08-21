using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace RuiChen.AbpPro.Saas
{
    [Authorize(AbpSaasPermissions.Editions.Default)]
    public class EditionAppService : AbpProSaasAppServiceBase, IEditionAppService
    {
        private readonly EditionManager editionManager;
        private readonly IEditionRepository editionRepository;

        public EditionAppService(EditionManager editionManager, IEditionRepository editionRepository)
        {
            this.editionManager = editionManager;
            this.editionRepository = editionRepository;
        }

        [Authorize(AbpSaasPermissions.Editions.Create)]
        public async virtual Task<EditionDto> CreateAsync(EditionCreateDto input)
        {
            var edition = await editionManager.CreateAsync(input.DisplayName);
            input.MapExtraPropertiesTo(edition);

            await editionRepository.InsertAsync(edition);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Edition, EditionDto>(edition);
        }

        [Authorize(AbpSaasPermissions.Editions.Delete)]
        public async virtual Task DeleteAsync(Guid id)
        {
            var edition = await editionRepository.GetAsync(id);

            await editionManager.DeleteAsync(edition);
        }

        public async virtual Task<EditionDto> GetAsync(Guid id)
        {
            var edition = await editionRepository.GetAsync(id, false);

            return ObjectMapper.Map<Edition, EditionDto>(edition);
        }

        public async virtual Task<PagedResultDto<EditionDto>> GetListAsync(EditionGetListInput input)
        {
            var totalCount = await editionRepository.GetCountAsync(input.Filter);
            var editions = await editionRepository.GetListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.Filter
            );

            return new PagedResultDto<EditionDto>(
                totalCount,
                ObjectMapper.Map<List<Edition>, List<EditionDto>>(editions)
            );
        }

        [Authorize(AbpSaasPermissions.Editions.Update)]
        public async virtual Task<EditionDto> UpdateAsync(Guid id, EditionUpdateDto input)
        {
            var edition = await editionRepository.GetAsync(id, false);

            if (!string.Equals(edition.DisplayName, input.DisplayName))
            {
                await editionManager.ChangeDisplayNameAsync(edition, input.DisplayName);
            }
            edition.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

            input.MapExtraPropertiesTo(edition);

            await editionRepository.UpdateAsync(edition);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Edition, EditionDto>(edition);
        }

    }
}
