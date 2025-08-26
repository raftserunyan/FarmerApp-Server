using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using FarmerApp.Core.Models;
using FarmerApp.Core.Query;
using FarmerApp.Core.Query.DynamicDepthBuilder;
using FarmerApp.Core.Query.DynamicFilterBuilder.Builder;
using FarmerApp.Core.Query.DynamicSortingBuilder;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Specifications.Common;
using FarmerApp.Data.UnitOfWork;
using FarmerApp.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace FarmerApp.Core.Services.Common
{
    internal abstract class BaseService<TModel, TEntity> : IBaseService<TModel, TEntity>
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;

        protected BaseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        #region Public Methods        
        public virtual async Task<PagedResult<TModel>> GetAll(BaseQueryModel query = null, bool includeDeleted = false,
                                                            int depth = 1, IEnumerable<string> propertyTypesToExclude = default)
        {
            return await GetAll(null, query, includeDeleted, depth, propertyTypesToExclude);
        }
        public virtual async Task<PagedResult<TModel>> GetAll(ISpecification<TEntity> specification = null, BaseQueryModel query = null,
                                                    bool includeDeleted = false, int depth = 1, IEnumerable<string> propertyTypesToExclude = default)
        {
            specification ??= new EmptySpecification<TEntity>();

            int? total = await ApplyQueryToSpecificationAndReturnTotal(query, specification, includeDeleted, depth, propertyTypesToExclude);

            var entities = await _uow.Repository<TEntity>().GetAllBySpecification(specification, includeDeleted);

            return GetPagedResult(total, entities, query);
        }

        public virtual async Task<byte[]> ExportData<TExport>(ISpecification<TEntity> specification, BaseQueryModel query = null, bool includeDeleted = false, int depth = 1, IEnumerable<string> propertyTypesToExclude = null)
        {
            specification ??= new EmptySpecification<TEntity>();

            int? total = await ApplyQueryToSpecificationAndReturnTotal(query, specification, includeDeleted, depth, propertyTypesToExclude, applyPaging: false);

            var mappedEntities = _uow.Repository<TEntity>()
                .GetAllBySpecificationQueryable(specification, includeDeleted)
                .ProjectTo<TExport>(_mapper.ConfigurationProvider);

            var file = await GetExcelFileAsync(mappedEntities);

            return file;
        }

        public virtual async Task<TModel> GetById(int id, bool includeDeleted = false,
                                                int depth = 1, IEnumerable<string> propertyTypesToExclude = default)
        {
            var entity = await GetEntityById(id, includeDeleted, depth, propertyTypesToExclude);

            return _mapper.Map<TModel>(entity);
        }
        public virtual async Task<TModel> GetFirstBySpecification(ISpecification<TEntity> specification, bool includeDeleted = false,
                                                                    int depth = 1, IEnumerable<string> propertyTypesToExclude = default)
        {
            // Apply the includes by depth if they are not set explicitly
            if (!specification.Includes.Any() && !specification.IncludeStrings.Any())
            {
                var propertiesToInclude = new DynamicDepthBuilder<TEntity>().Build(depth, propertyTypesToExclude);
                specification.IncludeStrings.AddRange(propertiesToInclude);
            }

            var entity = await _uow.Repository<TEntity>().GetFirstBySpecification(specification, includeDeleted);

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

        public virtual async Task<TModel> Add(TModel model, int depth = 1, IEnumerable<string> propertyTypesToExclude = default)
        {
            var entity = ValidateAndMap(model, "Model to be added was null");

            await _uow.Repository<TEntity>().Add(entity);
            await _uow.SaveChangesAsync();

            return await GetById(entity.Id, false, depth, propertyTypesToExclude);
        }

        public virtual async Task<TModel> Update(TModel model, int depth = 1, IEnumerable<string> propertyTypesToExclude = default)
        {
            var entity = ValidateAndMap(model, "Model to be updated was null");
            if (entity.Id == default)
                throw BadRequest("Model must have an Id for updating");

            var existingEntity = await GetEntityById(entity.Id);
            _mapper.Map(entity, existingEntity);
            await _uow.SaveChangesAsync();

            return await GetById(entity.Id, false, depth, propertyTypesToExclude);
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

        protected static void IncludeDependenciesByDepth(ISpecification<TEntity> specification, int depth, IEnumerable<string> _propertyTypesToExclude)
        {
            // Apply the includes by depth if they are not set explicitly
            if (!specification.Includes.Any() && !specification.IncludeStrings.Any())
            {
                var propertiesToInclude = new DynamicDepthBuilder<TEntity>().Build(depth, _propertyTypesToExclude);

                specification.IncludeStrings.AddRange(propertiesToInclude);
            }
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

        protected async Task<TEntity> GetEntityById(int id, bool includeDeleted = false, int depth = 1, IEnumerable<string> propertyTypesToExclude = default)
        {
            var propertiesToInclude = new DynamicDepthBuilder<TEntity>().Build(depth, propertyTypesToExclude);

            var entity = await _uow.Repository<TEntity>().GetById(id, includeDeleted, propertiesToInclude);
            EnsureExists(entity, $"Entity with id {id} was not found");

            return entity;
        }

        protected async Task<TEntity> GetEntityBySpecification(ISpecification<TEntity> specification, bool includeDeleted = false)
        {
            var entity = await _uow.Repository<TEntity>().GetFirstBySpecification(specification, includeDeleted);
            EnsureExists(entity, $"Entity was not found");
            return entity;
        }

        protected static void EnsureExists<TObject>(TObject entity, string message = "Not found") where TObject : class
        {
            if (entity is null)
            {
                throw new NotFoundException(message);
            }
        }

        protected static BadRequestException BadRequest(string message = "Bad Request")
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
        
        protected async Task<int?> ApplyQueryToSpecificationAndReturnTotal(BaseQueryModel query,
            ISpecification<TEntity> specification,
            bool includeDeleted = false,
            int depth = 1,
            IEnumerable<string> propertyTypesToExclude = null,
            bool applyPaging = true,
            bool applyOrdering = true)
        {
            int? total = null;
            if (query is not null)
            {
                FilterResults(specification, query);

                total = await _uow.Repository<TEntity>().Count(specification, includeDeleted);

                // Exclude .User if it isn't already excluded
                propertyTypesToExclude = (propertyTypesToExclude ?? Enumerable.Empty<string>())
                    .Concat(new[] { nameof(UserEntity) })
                    .Distinct();

                IncludeDependenciesByDepth(specification, depth, propertyTypesToExclude);

                if (applyOrdering)
                    OrderResults(specification, query.Orderings);

                if (applyPaging)
                    ApplyPaging(specification, query);
            }

            return total;
        }

        protected PagedResult<TModel> GetPagedResult(int? total, List<TEntity> entities, BaseQueryModel query)
        {
            return new PagedResult<TModel>
            {
                Results = _mapper.Map<List<TModel>>(entities),
                Total = total ?? entities.Count,
                PageNumber = query?.PageNumber ?? 1,
                PageSize = query?.PageSize ?? (total ?? entities.Count)
            };
        }

        protected static async Task<byte[]> GetExcelFileAsync<T>(IQueryable<T> query)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            using (var stream = new MemoryStream())
            {
                using (var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
                {
                    var workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    var sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
                    sheets.Append(new Sheet()
                    {
                        Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = "Data"
                    });

                    // Header row
                    var headerRow = new Row();
                    foreach (var prop in properties)
                    {
                        headerRow.Append(new Cell() { DataType = CellValues.String, CellValue = new CellValue(prop.Name) });
                    }
                    sheetData.AppendChild(headerRow);

                    // Data rows (streamed)
                    await foreach (var item in query.AsAsyncEnumerable())
                    {
                        var newRow = new Row();
                        foreach (var prop in properties)
                        {
                            var value = prop.GetValue(item)?.ToString() ?? "";
                            newRow.Append(new Cell() { DataType = CellValues.String, CellValue = new CellValue(value) });
                        }
                        sheetData.AppendChild(newRow);
                    }

                    workbookPart.Workbook.Save();
                }

                return stream.ToArray();
            }
        }

        #endregion

        #region Private classes
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
        #endregion 
    }
}
