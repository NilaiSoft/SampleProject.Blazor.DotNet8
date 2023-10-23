using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Server.Data;
using SampleProject.Server.Factory;
using System.Collections.Generic;

namespace SampleProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavMenuController : BaseController<NavMenu, NavMenuModel>
    {
        private readonly INavMenuService _navMenuService;
        private readonly ICacheManager<NavMenu> _cache;
        private readonly INavMenuModelFactory _navMenuModelFactory;
        private readonly IMapper _mapper;
        public NavMenuController(IEntityRepository<NavMenu, NavMenuModel> repository,
            INavMenuService navMenuService,
            IMapper mapper,
            ICacheManager<NavMenu> memoryCache,
            INavMenuModelFactory navMenuModelFactory)
            : base(repository, mapper, memoryCache)
        {
            _navMenuService = navMenuService;
            _cache = memoryCache;
            _mapper = mapper;
            _navMenuModelFactory = navMenuModelFactory;
        }

        [HttpGet]
        [Route(nameof(GetMenuList))]
        public async Task<IActionResult> GetMenuList()
        {
            //var model = await _cacheManager.GetAsync($"{typeof(NavMenu).Name.ToLower()}-List"
            //    , async Task<IList<NavMenu>> () =>
            //    {
            //        return await _navMenuService.GetAllAsync(x => true);
            //    });

            var products = await _navMenuService.GetAllAsync(x => x.IsActive ?? false);

            return Ok(await _navMenuModelFactory.PrepareNavMenuModelAsync(products));
        }
    }
}
