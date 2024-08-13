using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.OpenIddict
{
    [Route("api/openiddict/applications")]
    public class OpenIddictApplicationController : OpenIddictControllerBase, IOpenIddictApplicationAppService
    {
        private readonly IOpenIddictApplicationAppService Service;

        public OpenIddictApplicationController(IOpenIddictApplicationAppService Service)
        {
            this.Service = Service;
        }

        [HttpPost]
        [Authorize(AbpProOpenIddictPermissions.Applications.Create)]
        public virtual Task<OpenIddictApplicationDto> CreateAsync(OpenIddictApplicationCreateDto input)
        {
            return Service.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AbpProOpenIddictPermissions.Applications.Delete)]
        public virtual Task DeleteAsync(Guid id)
        {
            return Service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(AbpProOpenIddictPermissions.Applications.Default)]
        public virtual Task<OpenIddictApplicationDto> GetAsync(Guid id)
        {
            return Service.GetAsync(id);
        }

        [HttpGet]
        [Authorize(AbpProOpenIddictPermissions.Applications.Default)]
        public virtual Task<PagedResultDto<OpenIddictApplicationDto>> GetListAsync(OpenIddictApplicationGetListInput input)
        {
            return Service.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AbpProOpenIddictPermissions.Applications.Update)]
        public virtual Task<OpenIddictApplicationDto> UpdateAsync(Guid id, OpenIddictApplicationUpdateDto input)
        {
            return Service.UpdateAsync(id, input);
        }
    }
}
