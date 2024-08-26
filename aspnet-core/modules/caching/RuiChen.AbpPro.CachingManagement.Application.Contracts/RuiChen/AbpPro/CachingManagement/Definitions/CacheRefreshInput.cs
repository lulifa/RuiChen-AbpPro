using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.CachingManagement
{
    public class CacheRefreshInput
    {
        [Required]
        public string Key { get; set; }
        public DateTime? AbsoluteExpiration { get; set; }
        public DateTime? SlidingExpiration { get; set; }
    }
}
