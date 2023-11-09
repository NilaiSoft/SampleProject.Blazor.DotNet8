using IdentityModel;

namespace SampleProject.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
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
    [Route($"{nameof(Index)}/{{pageIndex}}/{{pageSize}}")]
    public override async Task<IActionResult> Index(int pageIndex = 0, int pageSize = int.MaxValue)
    {
        var result = _mapper.Map<IList<ProductModel>>(await _productService.GetAllAsync(pageIndex, pageSize == 0 ? 10 : pageSize));

        result = result.OrderBy(x => x.Name).ToList();
        foreach (var item in result)
        {
            item.Position = result.IndexOf(item) + 1;

        }

        var model = new PageOptions<ProductModel>
        {
            Models = result,
            PageSize = 15,
            PageIndex = 0
        };

        return Ok(model);
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
        var model = await _cacheManager.GetAsync($"{typeof(RelatedProduct).Name.ToLower()}-{productId}-List-index-{pageIndex}-{pageSize}"
            , async Task<IPagedList<RelatedProduct>> () =>
            {
                return await _relatedProductService.GetAllAsync(x => x.ProductId1 == productId
                , x => new RelatedProduct
                {
                    ProductId1 = x.ProductId1,
                    ProductId2 = x.ProductId2,
                    Product2 = x.Product2,
                    Id = x.Id,
                    DisplayOrder = x.DisplayOrder
                }, pageIndex, pageSize);
            });

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
    [Route(nameof(RelatedCreate))]
    public virtual async Task<IActionResult> RelatedCreate(IList<RelatedProductModel> entitys)
    {
        var product = _mapper.Map<IList<RelatedProduct>>(entitys);

        var result = await _relatedProductService.AddRangeAndSaveChangesAsync(product);

        _cacheManager.RemoveRangeByPrefixEntityName(typeof(RelatedProduct).Name.ToLower());

        return Ok(result);
    }

    [HttpDelete($"{nameof(DeleteRelatedProductAsync)}/{{id}}")]
    public async Task<IActionResult> DeleteRelatedProductAsync(int id)
    {
        var result = await _relatedProductService.DeleteAsync(x => x.Id == id);

        _cacheManager.RemoveRangeByPrefixEntityName(typeof(RelatedProduct).Name.ToLower());

        return Ok(result);
    }
}

