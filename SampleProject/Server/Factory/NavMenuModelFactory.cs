namespace SampleProject.Server.Factory
{
    public class NavMenuModelFactory : INavMenuModelFactory
    {
        private readonly INavMenuService _navMenuService;
        private readonly ICacheManager<NavMenu> _cache;
        private readonly IMapper _mapper;
        public NavMenuModelFactory(INavMenuService navMenuService, ICacheManager<NavMenu> cache, IMapper mapper)
        {
            _navMenuService = navMenuService;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<IList<NavMenuModel>> PrepareNavMenuModelAsync(IList<NavMenu> products)
        {
            var res = _mapper.Map<IList<NavMenuModel>>(products);

            foreach (var item in res)
            {
                item.IconComponent.Name = products.First(x => x.Id == item.Id).IconComponent_Name;
                item.Badge.text = products.First(x => x.Id == item.Id).BadgeText;
                item.Badge.Color = products.First(x => x.Id == item.Id).BadgeColor;
            }

            foreach (var item in res)
            {
                item.Children.AddRange(res.Where(x => x.ParentId == item.Id && x.ParentId is not null)
                    .ToList());
            }
            res = res.Where(x => x.ParentId is null).ToList();

            return res;
        }
    }
}
