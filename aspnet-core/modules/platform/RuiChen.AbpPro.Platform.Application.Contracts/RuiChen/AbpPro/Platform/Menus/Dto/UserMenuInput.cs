using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.Platform
{
    public class UserMenuInput
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public List<Guid> MenuIds { get; set; } = new List<Guid>();
    }
}
