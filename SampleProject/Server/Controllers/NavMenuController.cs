using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            //var model = await _cacheManager.GetAsync($"{typeof(NavMenu).Name.ToLower()}-List"
            //    , async Task<IList<NavMenu>> () =>
            //    {
            //        return await _navMenuService.GetAllAsync(x => true);
            //    });

            var model = await _navMenuService.GetAllAsync(x => x.IsActive ?? false);

            var res = _mapper.Map<IList<NavMenuModel>>(model);

            foreach (var item in res)
            {
                item.IconComponent.Name = model.First(x => x.Id == item.Id).IconComponent_Name;
                item.Badge.text = model.First(x => x.Id == item.Id).BadgeText;
                item.Badge.Color = model.First(x => x.Id == item.Id).BadgeColor;
            }

            foreach (var item in res)
            {
                item.Children.AddRange(res.Where(x => x.ParentId == item.Id && x.ParentId is not null)
                    .ToList());
            }

            return Ok(res.Where(x => x.ParentId is null));
        }
    }
}
