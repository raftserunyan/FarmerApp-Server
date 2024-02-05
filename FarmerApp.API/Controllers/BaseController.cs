using AutoMapper;
using FarmerApp.Core.Models;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Common;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Specifications.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TEntity, TModel, TGet, TCreate, TUpdate> : ControllerBase 
        where TModel : BaseModel
        where TEntity : BaseEntity
    {
        protected readonly int _depth;
        protected readonly int _userId;
        protected readonly IMapper _mapper;
        protected readonly ICommonService<TModel, TEntity> _service;

        public abstract ISpecification<TEntity> GetSpecification { get; }

        protected BaseController(ICommonService<TModel, TEntity> service, IMapper mapper, int depth = 1)
        {
            _service = service;
            _depth = depth;
            _mapper = mapper;            
            //_userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value);
        }

        [HttpPost("Get")]
        public virtual async Task<ActionResult<PagedResult<TGet>>> Read([FromBody] BaseQueryModel query)
        {
            var data = await _service.GetAll(GetSpecification);

            return Ok(_mapper.Map<PagedResult<TGet>>(data));
        }

        //[HttpPost("{id:int}")]
        //public virtual async Task<IActionResult<TGet>> ReadById([FromRoute][BindRequired] int id)
        //{
        //    var data = await _service.ReadById(id, _depth);

        //    return Ok(_mapper.Map<TGet>(data));
        //}

        //[HttpPost]
        //public virtual async Task<IActionResult<TGet>> Create([FromBody][BindRequired] TCreate model)
        //{
        //    var obj = _mapper.Map<TModel>(model);

        //    var data = await _service.Create(obj, _depth);

        //    return Ok(_mapper.Map<TGet>(data));
        //}

        //[HttpPut("{id:int}")]
        //public virtual async Task<IActionResult<TGet>> Update([FromRoute][BindRequired] int id,
        //    [FromBody][BindRequired] TUpdate model)
        //{
        //    var obj = _mapper.Map<TModel>(model);
        //    obj.Id = id;

        //    var data = await _service.Update(id, obj, _depth);

        //    return Ok(_mapper.Map<TGet>(data));
        //}

        //[HttpDelete("{id:int}")]
        //public virtual async Task<IActionResult> Delete([FromRoute][BindRequired] int id)
        //{
        //    await _service.Delete(id);

        //    return Ok();
        //}
    }
}
