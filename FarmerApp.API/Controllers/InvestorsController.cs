using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.API.Models.ViewModels.ResponseModels.Investment;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Investment;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Specifications.Common;
using FarmerApp.Models.ViewModels.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    public class InvestorsController : BaseController<InvestorEntity, InvestorModel, InvestorResponseModel, InvestorRequestModel, InvestorRequestModel>
    {
        public InvestorsController(IMapper mapper, IInvestorService investorService)
           : base(investorService, mapper)
        {
        }

        public override async Task<ActionResult<PagedResult<InvestorResponseModel>>> Read([FromBody] BaseQueryModel query)
        {
            var data = await _service.GetAll(new EntityByUserIdSpecification<InvestorEntity>(UserId), query, false, 3, _propertyTypesToExclude);

            return Ok(_mapper.Map<PagedResult<InvestorResponseModel>>(data));
        }
    }
}