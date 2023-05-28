using AutoMapper;
using SampleProject.Server.Data;
using SampleProject.Server.VModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<Product, ProductModel>().ReverseMap();
    }
}