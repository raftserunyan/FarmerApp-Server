using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Core.Services.Investment;
using FarmerApp.Data.Entities;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.Controllers
{
    public class InvestmentsController : BaseController<InvestmentEntity, InvestmentModel, InvestmentResponseModel, InvestmentRequestModel, InvestmentRequestModel>
    {
        public InvestmentsController(IMapper mapper, IInvestmentService investmentService)
           : base(investmentService, mapper)
        {
        }
    }
}