using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public class GetLanguageTextByKeyInput
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string CultureName { get; set; }

        [Required]
        public string ResourceName { get; set; }
    }
}
