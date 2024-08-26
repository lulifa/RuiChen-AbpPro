using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.CachingManagement
{
    public class CacheKeyInput
    {
        [Required]
        public string Key { get; set; }
    }
}
