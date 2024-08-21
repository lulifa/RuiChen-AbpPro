using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Saas
{
    /// <summary>
    /// 版本管理
    /// </summary>
    [Route("api/saas/editions")]
    public class EditionController : AbpProSaasControllerBase, IEditionAppService
    {
        private readonly IEditionAppService editionAppService;

        public EditionController(IEditionAppService editionAppService)
        {
            this.editionAppService = editionAppService;
        }

        [HttpPost]
        [Authorize(AbpSaasPermissions.Editions.Create)]
        public virtual Task<EditionDto> CreateAsync(EditionCreateDto input)
        {
            return editionAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AbpSaasPermissions.Editions.Delete)]
        public virtual Task DeleteAsync(Guid id)
        {
            return editionAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EditionDto> GetAsync(Guid id)
        {
            return editionAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<EditionDto>> GetListAsync(EditionGetListInput input)
        {
            return editionAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AbpSaasPermissions.Editions.Update)]
        public virtual Task<EditionDto> UpdateAsync(Guid id, EditionUpdateDto input)
        {
            return editionAppService.UpdateAsync(id, input);
        }


    }
}
