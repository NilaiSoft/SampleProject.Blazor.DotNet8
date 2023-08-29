using Microsoft.AspNetCore.Mvc;
using SampleProject.Core;
using SampleProject.Server.Services;

namespace SampleProject.Server.BaseController
{
    public class BaseController<TEntity, TVModel> : Controller,
            IBaseController<TEntity, TVModel> where TEntity : BaseEntity
    {
        private readonly IEntityRepository<TEntity, TVModel> _repository;
        public readonly ICacheManager<TEntity> _cacheManager;
        private readonly IMapper _mapper;

        public BaseController(IEntityRepository<TEntity, TVModel> repository, IMapper mapper, ICacheManager<TEntity> cacheManager)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }


        [HttpGet]
        [Route($"{nameof(Index)}/{{pageIndex}}/{{pageSize}}")]
        public virtual async Task<IActionResult> Index(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var model = await _cacheManager.GetAsync($"{typeof(TEntity).Name.ToLower()}-List-index-{pageIndex}-{pageSize}"
                , async Task<IPagedList<TEntity>> () =>
                {
                    return await _repository.GetAllAsync(pageIndex, pageSize);
                });

            return Ok(model.ToPagedList());
        }

        [HttpGet]
        [Route(nameof(Create))]
        public virtual async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(Create))]
        public virtual async Task<IActionResult> CreateAsync(TVModel entity)
        {
            var model = _mapper.Map<TEntity>(entity);

            var result = await _repository.AddAndSaveChangesAsync(model);

            var cacheKey = _cacheManager.GetCacheName($"{typeof(TEntity).Name.ToLower()}-List-index-");
            _cacheManager.Remove(cacheKey);

            return Json(result);
        }

        [HttpGet]
        [Route($"{nameof(EditAsync)}/{{id}}")]
        public virtual async Task<IActionResult> EditAsync(int id)
        {
            var model = await _repository.FindAsync(x => x.Id == id);

            return Ok(model);
        }

        [HttpPost]
        [Route(nameof(EditAsync))]
        public virtual async Task<IActionResult> EditAsync(TVModel entity)
        {
            var model = _mapper.Map<TEntity>(entity);
            var result = await _repository.EditAsync(model);

            _cacheManager.RemoveRangeByPrefixEntityName($"{typeof(TEntity).Name.ToLower()}");

            return Ok(result);
        }

        [HttpDelete($"{nameof(DeleteAsync)}/{{id}}")]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(x => x.Id == id);

            _cacheManager.RemoveRangeByPrefixEntityName(typeof(TEntity).Name.ToLower());

            return Ok(result);
        }

        public virtual async Task<IActionResult> Details(int id)
        {
            var model = await _repository.GetAsync(x => x.Id == id);
            return View(model);
        }
    }
}
