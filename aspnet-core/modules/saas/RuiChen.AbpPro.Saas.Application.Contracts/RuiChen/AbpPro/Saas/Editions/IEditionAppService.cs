using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.Saas
{
    public interface IEditionAppService :
        ICrudAppService<
            EditionDto,
            Guid,
            EditionGetListInput,
            EditionCreateDto,
            EditionUpdateDto>
    {
    }
}
