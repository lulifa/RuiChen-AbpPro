using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.Platform
{
    public class UserFavoriteMenuUpdateDto : UserFavoriteMenuCreateOrUpdateDto, IHasConcurrencyStamp
    {

        public string ConcurrencyStamp { get; set; }
    }
}
