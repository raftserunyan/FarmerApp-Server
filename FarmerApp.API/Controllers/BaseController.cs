﻿using AutoMapper;
using FarmerApp.Core.Models;
using FarmerApp.Core.Models.Interfaces;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Common;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Entities.Interfaces;
using FarmerApp.Data.Specifications.Common;
using FarmerApp.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Drawing.Text;

namespace FarmerApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TEntity, TModel, TGet, TCreate, TUpdate> : ControllerBase
        where TModel : BaseModel, IHasUserModel
        where TEntity : BaseEntity, IHasUser
    {
        protected readonly int _depth;
        protected readonly IMapper _mapper;
        protected readonly ICommonService<TModel, TEntity> _service;

        protected int UserId => GetCurrentUserId();

        protected BaseController(ICommonService<TModel, TEntity> service, IMapper mapper, int depth = 1)
        {
            _service = service;
            _depth = depth;
            _mapper = mapper;
        }

        [HttpPost("Get")]
        public virtual async Task<ActionResult<PagedResult<TGet>>> Read([FromBody] BaseQueryModel query)
        {
            var data = await _service.GetAll(new EntityByUserIdSpecification<TEntity>(UserId), query);

            return Ok(_mapper.Map<PagedResult<TGet>>(data));
        }

        [HttpGet("{id:int}")]
        public virtual async Task<ActionResult<TGet>> ReadById([FromRoute][BindRequired] int id)
        {
            var data = await _service.GetById(id);
            EnsureUserHasAccess(data);

            return Ok(_mapper.Map<TGet>(data));
        }

        [HttpPost]
        public virtual async Task<ActionResult<TGet>> Create([FromBody][BindRequired] TCreate model)
        {
            var businessModel = _mapper.Map<TModel>(model);
            businessModel.UserId = UserId;

            var data = await _service.Add(businessModel);

            return Ok(_mapper.Map<TGet>(data));
        }

        [HttpPut("{id:int}")]
        public virtual async Task<ActionResult<TGet>> Update([FromRoute][BindRequired] int id,
            [FromBody][BindRequired] TUpdate model)
        {
            var businessModel = _mapper.Map<TModel>(model);
            businessModel.Id = id;
            businessModel.UserId = UserId;

            // For checking if user has access to this record
            await ReadById(id);

            var data = await _service.Update(businessModel);

            return Ok(_mapper.Map<TGet>(data));
        }

        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete([FromRoute][BindRequired] int id)
        {
            var modelToBeDeleted = await _service.GetById(id);
            EnsureUserHasAccess(modelToBeDeleted);

            await _service.Delete(id);

            return Ok();
        }

        private void EnsureUserHasAccess(IHasUserModel model, Exception exception = null)
        {
            if (model.UserId != UserId)
            {
                throw exception ?? new NotFoundException($"Entity with id {((BaseModel)model).Id} was not found");                
            }
        }

        private int GetCurrentUserId()
        {
            try
            {
                return int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value);
            }
            catch
            {
                throw new AccessDeniedException();
            }
        }
    }
}
