using AutoMapper;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public class LocalizationManagementDomainMapperProfile : Profile
    {
        public LocalizationManagementDomainMapperProfile()
        {
            CreateMap<LanguageText, LanguageTextEto>();
            CreateMap<Resource, ResourceEto>();
            CreateMap<Language, LanguageEto>();
        }

    }
}
