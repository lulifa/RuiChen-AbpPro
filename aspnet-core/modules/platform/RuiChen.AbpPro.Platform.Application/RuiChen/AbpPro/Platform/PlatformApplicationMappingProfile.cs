using AutoMapper;

namespace RuiChen.AbpPro.Platform
{
    public class PlatformApplicationMappingProfile : Profile
    {
        public PlatformApplicationMappingProfile()
        {

            CreateMap<DataItem, DataItemDto>();
            CreateMap<Data, DataDto>();
            CreateMap<Menu, MenuDto>()
                .ForMember(dto => dto.Meta, map => map.MapFrom(src => src.ExtraProperties))
                .ForMember(dto => dto.Startup, map => map.Ignore());
            CreateMap<Layout, LayoutDto>()
                .ForMember(dto => dto.Meta, map => map.MapFrom(src => src.ExtraProperties));
            CreateMap<UserFavoriteMenu, UserFavoriteMenuDto>();
        }
    }
}
