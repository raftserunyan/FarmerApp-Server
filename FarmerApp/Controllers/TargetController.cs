using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;
using FarmerApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private ITargetService _targetService;
        private IMapper _mapper;

        public TargetController(
            IMapper mapper,
            ITargetService targetService,
            IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _targetService = targetService;
            _targetService.SetUser(int.Parse(httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var targets = _targetService.GetAll();

            var response = new List<TargetResponseModel>();

            foreach (var target in targets)
            {
                response.Add(_mapper.Map<TargetResponseModel>(target));
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(TargetRequestModel targetRequest)
        {
            var id = _targetService.Add(_mapper.Map<Target>(targetRequest));

            return Ok(id);
        }

        [HttpDelete]
        public IActionResult Remove(int Id)
        {
            _targetService.Remove(Id);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetTargetById(int id) => Ok(_mapper.Map<TargetResponseModel>(_targetService.GetById(id)));

        [HttpPut]
        public IActionResult UpdateTarget(int id, TargetRequestModel targetRequest)
        {
            var targetToUpdate = _mapper.Map<Target>(targetRequest);
            targetToUpdate.Id = id;

            var result = _targetService.Update(targetToUpdate);
            return Ok(result);
        }
    }
}
