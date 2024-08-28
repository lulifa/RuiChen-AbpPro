using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.Platform
{
    public interface ILayoutAppService :
        ICrudAppService<
            LayoutDto,
            Guid,
            GetLayoutListInput,
            LayoutCreateDto,
            LayoutUpdateDto>
    {
        Task<ListResultDto<LayoutDto>> GetAllListAsync();
    }
}
