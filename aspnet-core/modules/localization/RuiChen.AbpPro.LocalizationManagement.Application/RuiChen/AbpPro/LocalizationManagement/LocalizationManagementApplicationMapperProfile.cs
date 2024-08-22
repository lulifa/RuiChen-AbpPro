using AutoMapper;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public class LocalizationManagementApplicationMapperProfile : Profile
    {
        public LocalizationManagementApplicationMapperProfile()
        {
            CreateMap<Language, LanguageDto>();
            CreateMap<Resource, ResourceDto>();
        }
    }
}
