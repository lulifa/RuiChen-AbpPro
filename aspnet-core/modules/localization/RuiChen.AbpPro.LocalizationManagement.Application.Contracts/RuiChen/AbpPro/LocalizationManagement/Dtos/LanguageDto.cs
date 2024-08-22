using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public class LanguageDto : AuditedEntityDto<Guid>
    {
        public string CultureName { get; set; }
        public string UiCultureName { get; set; }
        public string DisplayName { get; set; }
        public string FlagIcon { get; set; }
    }
}
