using AutoMapper;
using FarmerApp.Core.Models;
using FarmerApp.Core.Models.Interfaces;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Entities.Interfaces;
using FarmerApp.Data.Specifications.Common;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.API.Controllers
{
    public class BaseControllerWithExcelExport<TEntity, TModel, TGet, TCreate, TUpdate, TExport> 
        : BaseController<TEntity, TModel, TGet, TCreate, TUpdate>
        where TModel : BaseModel, IHasUserModel
        where TEntity : BaseEntity, IHasUser
    {
        protected BaseControllerWithExcelExport(IBaseService<TModel, TEntity> service, IMapper mapper,
                                  int? depth = null, IEnumerable<string> propertyTypesToExclude = null)
            : base(service, mapper, depth ?? default, propertyTypesToExclude)
        {
        }

        [HttpPost("Export")]
        public virtual async Task<ActionResult<byte[]>> Export([FromBody] BaseQueryModel query)
        {
            var fileBytes = await _service.ExportData<TExport>(new EntityByUserIdSpecification<TEntity>(UserId), query, false, _depth, _propertyTypesToExclude);

            string fileName = $"{typeof(TEntity).Name}_{DateTime.UtcNow.AddHours(4):dd-MM-yyyy_HH-mm}.xlsx";

            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }
    }
}
