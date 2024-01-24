using System.Net;
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
    public class TreatmentsController : ControllerBase
    {
        private ITreatmentService _treatmentService;
        private IProductService _productService;
        private IMapper _mapper;

        public TreatmentsController(
            IMapper mapper,
            ITreatmentService treatmentService,
            IProductService productService,
            IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _treatmentService = treatmentService;
            _treatmentService.SetUser(int.Parse(httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value));
            _productService = productService;
            _productService.SetUser(int.Parse(httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var treatments = _treatmentService.GetAll();

            var response = new List<TreatmentResponseModel>();

            foreach (var treatment in treatments)
            {
                response.Add(_mapper.Map<TreatmentResponseModel>(treatment));
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(TreatmentRequestModel treatmentRequest)
        {
            var id = _treatmentService.Add(_mapper.Map<Treatment>(treatmentRequest));

            return Ok(id);
        }

        [HttpDelete]
        public IActionResult Remove(int Id)
        {
            _treatmentService.Remove(Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateTreatment(int id, TreatmentRequestModel treatmentRequest)
        {
            var treatmentToUpdate = _mapper.Map<Treatment>(treatmentRequest);
            treatmentToUpdate.Id = id;

            var result = _treatmentService.Update(treatmentToUpdate);
            return Ok(result);
        }
    }
}
