using Microsoft.AspNetCore.Mvc;
using SampleProject.Server.Data;

namespace SampleProject.Server.BaseController
{
    public interface IBaseController<TEntity, TVModel>
            where TEntity : BaseEntity
    {
        Task<IActionResult> Index(int pageIndex = 0, int pageSize = int.MaxValue);
        Task<IActionResult> Create();

        [HttpPost]
        Task<IActionResult> CreateAsync(TVModel entity);
        Task<IActionResult> EditAsync(int id);

        [HttpPost]
        Task<IActionResult> EditAsync(TVModel entity);

        Task<IActionResult> DeleteAsync(int id);

        Task<IActionResult> Details(int id);
    }
}