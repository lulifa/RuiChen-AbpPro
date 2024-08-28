using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Platform
{
    public class UserFavoriteMenuRemoveInput
    {
        [Required]
        public Guid MenuId { get; set; }
    }
}
