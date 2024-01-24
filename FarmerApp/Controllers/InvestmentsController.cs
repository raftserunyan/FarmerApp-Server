using AutoMapper;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;
using FarmerApp.Models;
using FarmerApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class InvestmentsController : ControllerBase
    {
        private IInvestmentService _investmentService;
        private IMapper _mapper;

        public InvestmentsController(
            IMapper mapper,
            IInvestmentService investmentService,
            IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _investmentService = investmentService;
            _investmentService.SetUser(int.Parse(httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var investments = _investmentService.GetAll();

            var response = new List<InvestmentResponseModel>();

            foreach (var investment in investments)
            {
                response.Add(_mapper.Map<InvestmentResponseModel>(investment));
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(InvestmentRequestModel investmentRequest)
        {
            var id = _investmentService.Add(_mapper.Map<Investment>(investmentRequest));

            return Ok(id);
        }

        [HttpDelete]
        public IActionResult Remove(int Id)
        {
            _investmentService.Remove(Id);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetInvestmentById(int id) => Ok(_mapper.Map<InvestmentResponseModel>(_investmentService.GetById(id)));

        [HttpPut]
        public IActionResult UpdateInvestor(int id, InvestmentUpdateRequestModel investmentUpdateRequest)
        {
            var investmentToUpdate = _mapper.Map<Investment>(investmentUpdateRequest);
            investmentToUpdate.Id = id;

            var result = _investmentService.Update(investmentToUpdate);
            return Ok(result);
        }
    }
}
