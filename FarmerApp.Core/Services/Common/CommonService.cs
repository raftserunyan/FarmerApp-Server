﻿using AutoMapper;
using FarmerApp.Core.Models;
using FarmerApp.Core.Query;
using FarmerApp.Core.Query.DynamicFilterBuilder.Builder;
using FarmerApp.Core.Query.DynamicSortingBuilder;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Specifications.Common;
using FarmerApp.Data.UnitOfWork;
using FarmerApp.Shared.Exceptions;
using System.Linq.Expressions;

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

        public virtual async Task<PagedResult<TModel>> GetAll(BaseQueryModel query = null, bool includeDeleted = false)
        {
            return await GetAll(null, query, includeDeleted);
        }
        public virtual async Task<PagedResult<TModel>> GetAll(ISpecification<TEntity> specification = null, BaseQueryModel query = null, bool includeDeleted = false)
        {
            if (specification is null)
                specification = new EmptySpecification<TEntity>();

            FilterResults(specification, query);

            var total = await _uow.Repository<TEntity>().Count(specification, includeDeleted);

            OrderResults(specification, query.Orderings);
            ApplyPaging(specification, query);

            var entities = await _uow.Repository<TEntity>().GetAllBySpecification(specification, includeDeleted);

            return new PagedResult<TModel>
            {
                Results = _mapper.Map<List<TModel>>(entities),
                Total = total,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public virtual async Task<TModel> GetById(int id, bool includeDeleted = false)
        {
            var entity = await GetEntityById(id, includeDeleted);

            return _mapper.Map<TModel>(entity);
        }
        public virtual async Task<TModel> GetSingleBySpecification(ISpecification<TEntity> specification, bool includeDeleted = false)
        {
            var entity = await _uow.Repository<TEntity>().GetSingleBySpecification(specification, includeDeleted);

            return _mapper.Map<TModel>(entity);
        }
        
        public virtual async Task Delete(TModel model)
        {
            var entity = ValidateAndMap(model, "Model to delete cannot be null");
            _uow.Repository<TEntity>().Delete(entity);
            await _uow.SaveChangesAsync();
        }
        public virtual async Task Delete(int id)
        {
            var entity = await GetEntityById(id);
            _uow.Repository<TEntity>().Delete(entity);
            await _uow.SaveChangesAsync();
        }

        public virtual async Task<int> Add(TModel model)
        {
            var entity = ValidateAndMap(model, "Model to be added was null");

            await _uow.Repository<TEntity>().Add(entity);
            await _uow.SaveChangesAsync();

            return entity.Id;
        }

        public virtual async Task<TModel> Update(TModel model)
        {
            var entity = ValidateAndMap(model, "Model to be updated was null");
            if (entity.Id == default(int))
                throw BadRequest("Model must have an Id for updating");

            var existingEntity = await GetEntityById(entity.Id);
            _mapper.Map(entity, existingEntity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<TModel>(existingEntity);
        }

        #endregion

        #region Private methods

        protected static void ApplyPaging(ISpecification<TEntity> specification, IPaging query)
        {
            if (query is null || query.PageNumber < 1 || query.PageSize < 1)
                return;

            specification.ApplyPaging(query.PageNumber, query.PageSize);
        }

        protected static void FilterResults(ISpecification<TEntity> specification, IFilterable query)
        {
            ApplyFilters(specification, query.OrFilters, true);
            ApplyFilters(specification, query.AndFilters, false);
        }

        protected static void OrderResults(ISpecification<TEntity> specification, IEnumerable<OrderingItem> orderings)
        {
            if (orderings == default || !orderings.Any())
                return;

            var orderingExpressionsFromQuery = DynamicSortingBuilder.BuildOrderingFunc<TEntity>(orderings);

            if (specification.OrderBy.Any() || specification.OrderByDescending.Any())
            {
                if (specification.OrderBy.Any())
                    specification.OrderBy.AddRange(orderingExpressionsFromQuery);
                else
                    specification.OrderByDescending.AddRange(orderingExpressionsFromQuery);
            }
            else
            {
                if (orderings.First().IsAscending)
                    specification.OrderBy.AddRange(orderingExpressionsFromQuery);
                else
                    specification.OrderByDescending.AddRange(orderingExpressionsFromQuery);
            }
        }

        protected TEntity ValidateAndMap(TModel model, string errorMessage)
        {
            if (model is null)
                throw BadRequest(errorMessage);

            return _mapper.Map<TEntity>(model);
        }

        protected async Task<TEntity> GetEntityById(int id, bool includeDeleted = false)
        {
            var entity = await _uow.Repository<TEntity>().GetById(id, includeDeleted);
            EnsureExists(entity, $"Entity with id {id} was not found");
            return entity;
        }

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

        private static void ApplyFilters(ISpecification<TEntity> specification, IEnumerable<FilteringItem> filters, bool isOrFilter)
        {
            if (filters == null || !filters.Any())
                return;

            var filterExpression = DynamicFilterBuilder<TEntity>.Instance.BuildFilter(filters, isOrFilter);

            Expression filter = filterExpression?.Body;

            if (specification.Criteria is not null)
            {
                var replacer = new ParameterReplacer(specification.Criteria.Parameters[0], filterExpression.Parameters[0]);
                var newCriteriaBody = replacer.Visit(specification.Criteria.Body);

                filter = Expression.AndAlso(newCriteriaBody, filterExpression.Body);
            }

            specification.Criteria = Expression.Lambda<Func<TEntity, bool>>(filter, filterExpression.Parameters);
        }
        #endregion

        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ParameterReplacer(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _oldParameter ? _newParameter : base.VisitParameter(node);
            }
        }
    }
}
