using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.API.Models.ViewModels.ResponseModels.Sale;
using FarmerApp.Core.Models.Sale;
using FarmerApp.Core.Services.Sale;
using FarmerApp.Data.Entities;
using FarmerApp.Models.ViewModels.RequestModels;

namespace FarmerApp.Controllers
{
    public class SalesController : BaseControllerWithExcelExport<SaleEntity, SaleModel, SaleResponseModel, SaleRequestModel, SaleRequestModel, SaleExportModel>
    {
        public SalesController(IMapper mapper, ISaleService saleService)
           : base(saleService, mapper)
        {
        }
    }
}