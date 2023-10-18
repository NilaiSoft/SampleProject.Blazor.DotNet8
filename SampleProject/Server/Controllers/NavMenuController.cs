using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavMenuController : BaseController<NavMenu, NavMenuModel>
    {
        private readonly INavMenuService _navMenuService;
        private readonly ICacheManager<NavMenu> _cache;
        private readonly IMapper _mapper;
        public NavMenuController(IEntityRepository<NavMenu, NavMenuModel> repository,
            INavMenuService navMenuService,
            IMapper mapper,
            ICacheManager<NavMenu> memoryCache)
            : base(repository, mapper, memoryCache)
        {
            _navMenuService = navMenuService;
            _cache = memoryCache;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(GetMenu))]
        public async Task<IActionResult> GetMenu()
        {
            var model = await _cacheManager.GetAsync($"{typeof(NavMenu).Name.ToLower()}-List"
                , async Task<IList<NavMenu>> () =>
                {
                    return await _navMenuService.GetAllAsync(x => true);
                });
            var res = _mapper.Map<IList<NavMenuModel>>(model);
            return Ok(res);
        }
    }
}
