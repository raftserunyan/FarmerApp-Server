using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.Core.Models.Sale;
using FarmerApp.Core.Services.Sale;
using FarmerApp.Data.Entities;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.Controllers
{
    public class SalesController : BaseController<SaleEntity, SaleModel, SaleResponseModel, SaleRequestModel, SaleRequestModel>
    {
        public SalesController(IMapper mapper, ISaleService saleService)
           : base(saleService, mapper)
        {
        }
    }
}