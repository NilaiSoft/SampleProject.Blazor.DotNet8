using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Core;
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
        private readonly IProductService _productService;
        public ProductController(IEntityRepository<Product, ProductModel> repository, IMapper mapper, IProductService productService)
            : base(repository, mapper)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route($"{nameof(SearchProduct)}/{{searchString}}")]
        public virtual async Task<IActionResult> SearchProduct(string searchString)
        {
            var model = await _productService.GetAllAsync(
                x => (x.Name.Contains(searchString)
                || x.ShortDescription.Contains(searchString)
                || x.FullDescription.Contains(searchString))
                && x.Enable);

            return Ok(model);
        }

        [HttpGet]
        [Route($"{nameof(Edit)}/{{id}}")]
        public override async Task<IActionResult> Edit(int id)
        {
            var model = await _productService.FindAsync(id);

            return Ok(model);
        }
    }
}
