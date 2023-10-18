public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<Product, ProductModel>().ReverseMap();
        CreateMap<RelatedProduct, RelatedProductModel>().ReverseMap();
        CreateMap<NavMenu, NavMenuModel>()
            .ForMember(dest => dest.LinkProps, src => src.MapFrom(x => x.LinkProps_Fragment))
            .ForMember(dest => dest.Title, src => src.MapFrom(x => x.IsTitle))
            .ForMember(dest => dest.IconComponent, src => src.MapFrom(x => x.IconComponent_Name));
    }
}