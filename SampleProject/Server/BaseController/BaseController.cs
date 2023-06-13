using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Core;
using SampleProject.Server.Data;
using SampleProjects.Server.Services;

namespace SampleProject.Server.BaseController
{
    public class BaseController<TEntity, TVModel> : Controller,
            IBaseController<TEntity, TVModel> where TEntity : BaseEntity
    {
        private readonly IEntityRepository<TEntity, TVModel> _repository;
        private readonly IMapper _mapper;

        public BaseController(IEntityRepository<TEntity, TVModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route($"{nameof(Index)}/{{pageIndex}}/{{pageSize}}")]
        public virtual async Task<IActionResult> Index(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var model = await _repository.GetAllAsync(pageIndex, pageSize);
            //var model = _mapper.Map<IList<TVModel>>(entity);
            return Ok(new Tuple<IPagedList<TEntity>, int>(model, model.TotalCount));
        }

        [HttpGet]
        [Route(nameof(Create))]
        public virtual async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(Create))]
        public virtual async Task<IActionResult> Create(TVModel entity)
        {
            var model = _mapper.Map<TEntity>(entity);

            var result = await _repository.AddAndSaveChangesAsync(model);

            return Json(result);
        }

        [HttpGet]
        [Route($"{nameof(Edit)}/{{id}}")]
        public virtual async Task<IActionResult> Edit(int id)
        {
            var model = await _repository.GetAsync(x => x.Id == id);
            var model2 = _mapper.Map<TVModel>(model);
            return Ok(model2);
        }

        [HttpPost]
        [Route(nameof(Edit))]
        public virtual async Task<IActionResult> Edit(TVModel entity)
        {
            var model = _mapper.Map<TEntity>(entity);
            var result = await _repository.EditAsync(model);
            return Ok(result);
        }

        [HttpDelete($"{nameof(Delete)}/{{id}}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(x => x.Id == id);
            return Ok(result);
        }

        public virtual async Task<IActionResult> Details(int id)
        {
            var model = await _repository.GetAsync(x => x.Id == id);
            return View(model);
        }
    }
}
