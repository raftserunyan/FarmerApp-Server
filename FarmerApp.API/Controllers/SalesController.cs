using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.API.Models.ViewModels.ResponseModels.Sale;
using FarmerApp.Core.Models.Sale;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Sale;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Specifications.Sale;
using FarmerApp.Models.ViewModels.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FarmerApp.Controllers
{
    public class SalesController : BaseController<SaleEntity, SaleModel, SaleResponseModel, SaleRequestModel, SaleRequestModel>
    {
        public SalesController(IMapper mapper, ISaleService saleService)
           : base(saleService, mapper)
        {
        }

        public override async Task<ActionResult<PagedResult<SaleResponseModel>>> Read([FromBody] BaseQueryModel query)
        {
            var sales = await _service.GetAll(new AllSalesWithDepsSpecification(), query);

            return _mapper.Map<PagedResult<SaleResponseModel>>(sales);
        }

        public override async Task<ActionResult<SaleResponseModel>> Create([BindRequired, FromBody] SaleRequestModel model)
        {
            var businessModel = _mapper.Map<SaleModel>(model);
            businessModel.UserId = UserId;

            var data = await _service.Add(businessModel);
            var entity = await _service.GetFirstBySpecification(new SaleByIdWithDepsSpecification((int)data.Id));

            // Fix this when depth builder is available
            return _mapper.Map<SaleResponseModel>(entity);
        }
    }
}