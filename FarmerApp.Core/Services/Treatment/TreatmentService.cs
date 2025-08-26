using AutoMapper;
using FarmerApp.Core.Models.Treatment;
using FarmerApp.Core.Query;
using FarmerApp.Core.Query.DynamicSortingBuilder;
using FarmerApp.Core.Services.Common;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Specifications.Common;
using FarmerApp.Data.Specifications.Product;
using FarmerApp.Data.Specifications.Treatment;
using FarmerApp.Data.UnitOfWork;

namespace FarmerApp.Core.Services.Treatment
{
    internal class TreatmentService : BaseService<TreatmentModel, TreatmentEntity>, ITreatmentService
    {
        public TreatmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async override Task<PagedResult<TreatmentModel>> GetAll(ISpecification<TreatmentEntity> specification = null, BaseQueryModel query = null, bool includeDeleted = false, int depth = 1, IEnumerable<string> propertyTypesToExclude = null)
        {
            var treatedProductIdsFilter = query.AndFilters?.Where(x => x.Property == "treatedProductIds").FirstOrDefault();
            if (treatedProductIdsFilter != null)
            {
                query.AndFilters.Remove(treatedProductIdsFilter);
            }

            specification ??= new EmptySpecification<TreatmentEntity>();
            int? total = await ApplyQueryToSpecificationAndReturnTotal(
                query,
                specification,
                includeDeleted,
                depth,
                propertyTypesToExclude,
                false,
                false);

            // NOTE: if you use GetAllBySpecificationQueryable here, the filter below won't work...
            var treatments = await _uow.Repository<TreatmentEntity>().GetAllBySpecification(specification, includeDeleted);

            if (treatedProductIdsFilter is not null)
            {
                treatments = treatments
                    .Where(t => t.Products.Any(p => treatedProductIdsFilter.Value.Contains(p.Id.ToString())))
                    .ToList();

                total = treatments.Count;
            }

            treatments = ApplyOrdering(treatments, query.Orderings);

            treatments = treatments
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();

            return GetPagedResult(total, treatments, query);
        }

        public override async Task<TreatmentModel> Add(TreatmentModel model, int depth = 1, IEnumerable<string> propertyTypesToExclude = null)
        {
            var entity = ValidateAndMap(model, "Model to be added was null");

            if (model.TreatedProductsIds is null || !model.TreatedProductsIds.Any())
                BadRequest("No treated products specified");

            var products = await _uow.Repository<ProductEntity>().GetAllBySpecification(new ProductsByGivenIdsSpecification(model.TreatedProductsIds));
            entity.Products = products;

            await _uow.Repository<TreatmentEntity>().Add(entity);
            await _uow.SaveChangesAsync();

            return await GetById(entity.Id, false, depth, propertyTypesToExclude);
        }

        public override async Task<TreatmentModel> Update(TreatmentModel model, int depth = 1, IEnumerable<string> propertyTypesToExclude = null)
        {
            var entity = ValidateAndMap(model, "Model to be updated was null");
            if (entity.Id == default)
                throw BadRequest("Model must have an Id for updating");

            var existingEntity = await GetEntityBySpecification(new TreatmentWithDepsByIdSpecification(entity.Id));

            var products = await _uow.Repository<ProductEntity>().GetAllBySpecification(new ProductsByGivenIdsSpecification(model.TreatedProductsIds));
            existingEntity.Products = products;

            _mapper.Map(entity, existingEntity);
            await _uow.SaveChangesAsync();

            return await GetById(entity.Id, false, depth, propertyTypesToExclude);
        }


        private static List<TreatmentEntity> ApplyOrdering(IEnumerable<TreatmentEntity> list, List<OrderingItem> orderings)
        {
            if (list == null || orderings == null || !list.Any() || !orderings.Any())
                return list.ToList();

            var orderingExpressionsFromQuery = DynamicSortingBuilder.BuildOrderingFunc<TreatmentEntity>(orderings);

            var isAscending = orderings.First().IsAscending;
            var isFirst = true;

            foreach (var item in orderingExpressionsFromQuery.Select(x => x.Compile()))
            {
                if (isAscending)
                    list = isFirst ? list.OrderBy(item) : ((IOrderedEnumerable<TreatmentEntity>)list).ThenBy(item);
                else
                    list = isFirst ? list.OrderByDescending(item) : ((IOrderedEnumerable<TreatmentEntity>)list).ThenByDescending(item);

                isFirst = false;
            }

            return list.ToList();
        }
    }
}