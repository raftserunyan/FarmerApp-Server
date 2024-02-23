using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.API.Models.ViewModels.ResponseModels.Investment;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Investment;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Specifications.Investments;
using FarmerApp.Models.ViewModels.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    public class InvestmentsController : BaseController<InvestmentEntity, InvestmentModel, InvestmentResponseModel, InvestmentRequestModel, InvestmentRequestModel>
    {
        public InvestmentsController(IMapper mapper, IInvestmentService investmentService)
           : base(investmentService, mapper)
        {
        }

        public override async Task<ActionResult<PagedResult<InvestmentResponseModel>>> Read([FromBody] BaseQueryModel query)
        {
            var investments = await _service.GetAll(new InvestmentWithDepsSpecification(), query);

            return _mapper.Map<PagedResult<InvestmentResponseModel>>(investments);
        }
    }
}