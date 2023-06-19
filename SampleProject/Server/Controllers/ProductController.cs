﻿using AutoMapper;
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
        private readonly IRelatedProductService _relatedProductService;
        private readonly IMapper _mapper;
        public ProductController(IEntityRepository<Product, ProductModel> repository, IMapper mapper, IProductService productService, IRelatedProductService relatedProductService)
            : base(repository, mapper)
        {
            _productService = productService;
            _relatedProductService = relatedProductService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route($"{nameof(SearchProduct)}/{{searchString}}")]
        public virtual async Task<IActionResult> SearchProduct(string searchString)
        {
            var model = await _productService.GetAllAsync(
                x => (x.Name.Contains(searchString)
                || x.ShortDescription.Contains(searchString)
                || x.FullDescription.Contains(searchString)));

            return Ok(model);
        }

        [HttpGet]
        [Route($"{nameof(RelatedProducts)}/{{productId}}/{{pageIndex}}/{{pageSize}}")]
        public virtual async Task<IActionResult> RelatedProducts(int productId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var model = await _relatedProductService
                .GetAllAsync(x => x.ProductId1 == productId
                , x => new RelatedProduct
                {
                    ProductId1 = x.ProductId1,
                    ProductId2 = x.ProductId2,
                    Product2 = x.Product2,
                    Id = x.Id,
                    DisplayOrder = x.DisplayOrder
                }, pageIndex, pageSize);
            return Ok(new Tuple<IPagedList<RelatedProduct>, int>(model, model.TotalCount));
        }

        [HttpGet]
        [Route($"{nameof(Edit)}/{{id}}")]
        public override async Task<IActionResult> Edit(int id)
        {
            var model = await _productService.FindAsync(id);

            return Ok(model);
        }

        [HttpPost]
        [Route(nameof(Create))]
        public override async Task<IActionResult> Create(ProductModel entity)
        {
            var product = _mapper.Map<Product>(entity);

            var result = await _productService.AddProductAsync(product);

            return Ok(result);
        }
    }
}
