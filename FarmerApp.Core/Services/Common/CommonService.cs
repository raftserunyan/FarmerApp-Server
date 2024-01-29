﻿using AutoMapper;
using FarmerApp.Core.Models;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Specifications.Common;
using FarmerApp.Data.UnitOfWork;
using FarmerApp.Shared.Exceptions;

namespace FarmerApp.Core.Services.Common
{
    internal abstract class CommonService<TModel, TEntity> : ICommonService<TModel, TEntity>
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;

        protected CommonService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        #region Public Methods
        public async Task<int> Add(TModel model)
        {
            if (model is null)
                throw BadRequest($"Model to be added was null");

            var entity = _mapper.Map<TEntity>(model);

            await _uow.Repository<TEntity>().Add(entity);
            await _uow.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(TModel model)
        {
            if (model is null)
                throw BadRequest("Model to delete cannot be null");

            var entity = _mapper.Map<TEntity>(model);

            _uow.Repository<TEntity>().Delete(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _uow.Repository<TEntity>().GetById(id, true);
            EnsureExists(entity, $"There's no entity with id {id} to delete. Entity type: {typeof(TEntity).Name}");

            _uow.Repository<TEntity>().Delete(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task<List<TModel>> GetAll(bool includeDeleted = false)
        {
            var entities = await _uow.Repository<TEntity>().GetAll(includeDeleted);

            return _mapper.Map<List<TModel>>(entities);
        }

        public async Task<List<TModel>> GetAll(ICommonSpecification<TEntity> specification, bool includeDeleted = false)
        {
            var entities = await _uow.Repository<TEntity>().GetAllBySpecification(specification, includeDeleted);

            return _mapper.Map<List<TModel>>(entities);
        }

        public async Task<TModel> GetById(int id, bool includeDeleted = false)
        {
            var entity = await _uow.Repository<TEntity>().GetById(id, includeDeleted);
            EnsureExists(entity, $"Entity with id {id} was not found");

            return _mapper.Map<TModel>(entity);
        }

        public async Task<TModel> GetSingleBySpecification(ICommonSpecification<TEntity> specification, bool includeDeleted = false)
        {
            var entity = await _uow.Repository<TEntity>().GetSingleBySpecification(specification, includeDeleted);

            return _mapper.Map<TModel>(entity);
        }

        public async Task Update(TModel model)
        {
            if (model is null)
                throw BadRequest($"Model to be updated was null");

            var entity = _mapper.Map<TEntity>(model);

            if (entity.Id == default(int))
                throw BadRequest("Model must have an Id for updating");

            var existingEntity = await _uow.Repository<TEntity>().GetById(entity.Id, true);
            EnsureExists(existingEntity, $"There's no record with id {entity.Id} to update");

            _mapper.Map(entity, existingEntity);
            await _uow.SaveChangesAsync();
        }
        #endregion

        #region Private methods
        protected void EnsureExists<TObject>(TObject entity, string message = "Not found") where TObject : class
        {
            if (entity is null)
            {
                throw new NotFoundException(message);
            }
        }

        protected BadRequestException BadRequest(string message = "Bad Request")
        {
            throw new BadRequestException(message);
        }
        #endregion
    }
}
