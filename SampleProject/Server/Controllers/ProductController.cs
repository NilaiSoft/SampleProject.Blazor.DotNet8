﻿namespace SampleProject.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : BaseController<Product, ProductModel>
{
    private readonly IProductService _productService;
    private readonly IRelatedProductService _relatedProductService;
    private readonly IMapper _mapper;

    public ProductController(IEntityRepository<Product, ProductModel> repository
        , IMapper mapper, IProductService productService
        , IRelatedProductService relatedProductService
        , ICacheManager<Product> memoryCache)
        : base(repository, mapper, memoryCache)
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
    [Route($"{nameof(EditAsync)}/{{id}}")]
    public override async Task<IActionResult> EditAsync(int id)
    {
        var model = await _productService.FindAsync(id);

        return Ok(model);
    }

    [HttpPost]
    [Route(nameof(Create))]
    public override async Task<IActionResult> CreateAsync(ProductModel entity)
    {
        var product = _mapper.Map<Product>(entity);

        var result = await _productService.AddProductAsync(product);

        return Ok(result);
    }

    [HttpPost]
    [Route(nameof(RelatedCreate))]
    public virtual async Task<IActionResult> RelatedCreate(IList<RelatedProductModel> entitys)
    {
        var product = _mapper.Map<IList<RelatedProduct>>(entitys);

        var result = await _relatedProductService.AddRangeAndSaveChangesAsync(product);

        return Ok(result);
    }

    [HttpDelete($"{nameof(DeleteAsync)}/{{id}}")]
    public override async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _productService.DeleteAsync(x => x.Id == id);

        var cacheKey = _cacheManager.GetCacheName($"{(nameof(Product)).ToLower()}List-index-");
        _cacheManager.Remove(cacheKey);

        return Ok(result);
    }
}

