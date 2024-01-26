using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;
using FarmerApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class InvestorsController : ControllerBase
    {
        private IInvestorService _investorService;
        private IMapper _mapper;

        public InvestorsController(
            IMapper mapper,
            IInvestorService investorService,
            IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _investorService = investorService;
            _investorService.SetUser(int.Parse(httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var investors = _investorService.GetAll();

            var response = new List<InvestorResponseModel>();

            foreach (var investor in investors)
            {
                response.Add(_mapper.Map<InvestorResponseModel>(investor));
            }

            return  Ok(response);
        }

        [HttpPost]
        public IActionResult Add(InvestorRequestModel investorRequest)
        {
            var id = _investorService.Add(_mapper.Map<Investor>(investorRequest));

            return Ok(id);
        }

        [HttpDelete]
        public IActionResult Remove(int Id)
        {
            _investorService.Remove(Id);
            return Ok();
        }

        [HttpGet("GetInvestorById")]
        public IActionResult GetInvestorById(int Id) => Ok(_mapper.Map<InvestorResponseModel>(_investorService.GetById(Id)));

        [HttpPut]
        public IActionResult UpdateInvestor(int id, InvestorRequestModel investorRequest)
        {
            var investorToUpdate = _mapper.Map<Investor>(investorRequest);
            investorToUpdate.Id = id;

            var result = _investorService.Update(investorToUpdate);
            return Ok(result);
        }
    }
}
