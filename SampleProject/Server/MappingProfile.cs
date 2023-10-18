public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<Product, ProductModel>().ReverseMap();
        CreateMap<RelatedProduct, RelatedProductModel>().ReverseMap();
        CreateMap<NavMenu, NavMenuModel>()
            .ForMember(dest => dest.IconComponent_Name, opt => opt.MapFrom(src => src.IconComponent_Name))
            .ForMember(dest => dest.LinkProps_Fragment, opt => opt.MapFrom(src => src.LinkProps_Fragment))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.IsTitle))
            .ReverseMap();
    }
}