using FarmerApp.Core.Models;
using FarmerApp.Core.Query;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Specifications.Common;

namespace FarmerApp.Core.Services.Common
{
    public interface ICommonService<TModel, TEntity>
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        Task<TModel> GetById(int id, bool includeDeleted = false);
        Task<TModel> GetFirstBySpecification(ISpecification<TEntity> specification, bool includeDeleted = false);
        Task<PagedResult<TModel>> GetAll(BaseQueryModel query = null, bool includeDeleted = false);
        Task<PagedResult<TModel>> GetAll(ISpecification<TEntity> specification, BaseQueryModel query = null, bool includeDeleted = false);
        Task<TModel> Add(TModel model);
        Task<TModel> Update(TModel model);
        Task Delete(TModel model);
        Task Delete(int id);
    }
}
