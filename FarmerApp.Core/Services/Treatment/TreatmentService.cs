using AutoMapper;
using FarmerApp.Core.Models.Treatment;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;
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

        public override async Task<TreatmentModel> Add(TreatmentModel model)
        {          
            var entity = ValidateAndMap(model, "Model to be added was null");

            if (model.TreatedProductsIds is null || !model.TreatedProductsIds.Any())
                BadRequest("No treated products specified");

            var products = await _uow.Repository<ProductEntity>().GetAllBySpecification(new ProductsByGivenIdsSpecification(model.TreatedProductsIds));

            entity.Products = products; 

            await _uow.Repository<TreatmentEntity>().Add(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<TreatmentModel>(entity);
        }

        public override async Task<TreatmentModel> Update(TreatmentModel model)
        {
            var entity = ValidateAndMap(model, "Model to be updated was null");
            if (entity.Id == default)
                throw BadRequest("Model must have an Id for updating");

            var existingEntity = await GetEntityBySpecification(new TreatmentWithDepsByIdSpecification(entity.Id));

            #region Add/Remove new/old products

            var newProductIds = model.TreatedProductsIds.Except(existingEntity.Products.Select(p => p.Id));
            var removedProductIds = existingEntity.Products.Select(p => p.Id).Except(model.TreatedProductsIds);

            // Remove olds
            foreach (var productId in removedProductIds)
                existingEntity.Products.Remove(existingEntity.Products.First(p => p.Id == productId));

            // Add news
            var newProducts = await _uow.Repository<ProductEntity>().GetAllBySpecification(new ProductsByGivenIdsSpecification(newProductIds));
            newProducts.ForEach(x => existingEntity.Products.Add(x));

            #endregion

            _mapper.Map(entity, existingEntity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<TreatmentModel>(existingEntity);
        }
    }
}