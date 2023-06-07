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
        Task<IActionResult> Create(TVModel entity);
        Task<IActionResult> Edit(int id);

        [HttpPost]
        Task<IActionResult> Edit(TVModel entity);

        Task<IActionResult> Delete(int id);

        Task<IActionResult> Details(int id);
    }
}