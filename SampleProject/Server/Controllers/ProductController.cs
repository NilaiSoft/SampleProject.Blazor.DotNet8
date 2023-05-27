using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Server.BaseController;
using SampleProject.Server.Data;
using SampleProject.Server.VModels;
using SampleProjects.Server.Services;

namespace SampleProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController<Product, ProductModel>
    {
        public ProductController(IEntityRepository<Product, ProductModel> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
