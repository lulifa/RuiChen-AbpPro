using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Platform
{
    public class MenuCreateDto : MenuCreateOrUpdateDto
    {
        [Required]
        public Guid LayoutId { get; set; }
    }
}
