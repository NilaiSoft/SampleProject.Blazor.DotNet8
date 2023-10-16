using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavMenuController : BaseController<NavMenu, NavMenuModel>
    {
        public NavMenuController(IEntityRepository<NavMenu, NavMenuModel> repository,
            IMapper mapper,
            ICacheManager<NavMenu> memoryCache)
            : base(repository, mapper, memoryCache)
        {

        }
    }
}
