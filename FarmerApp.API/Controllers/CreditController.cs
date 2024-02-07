﻿using AutoMapper;
using FarmerApp.API.Models.ViewModels.ResponseModels.Sale;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Sale;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Specifications.Common;
using FarmerApp.Data.Specifications.Sale;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CreditController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public CreditController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpPost("Get")]
        public async Task<ActionResult<PagedResult<SaleResponseModel>>> Get([FromQuery] bool myCredits, [FromBody] BaseQueryModel query)
        {
            ISpecification<SaleEntity> specification = myCredits ? new SalesWhichAreOverPaidSpecification() : new SalesWhichAreNotFullyPaidSpecification();

            var sales = await _saleService.GetAll(specification, query);

            return Ok(_mapper.Map<PagedResult<SaleResponseModel>>(sales));
        }
    }
}
