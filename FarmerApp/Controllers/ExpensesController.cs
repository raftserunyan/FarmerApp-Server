using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;
using FarmerApp.Services;
using FarmerApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private IExpenseService _expenseService;
        private IMapper _mapper;

        public ExpensesController(
            IMapper mapper,
            IExpenseService expenseService,
            IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _expenseService = expenseService;
            _expenseService.SetUser(int.Parse(httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var expenses = _expenseService.GetAll();

            var response = new List<ExpenseResponseModel>();

            foreach (var expense in expenses)
            {
                response.Add(_mapper.Map<ExpenseResponseModel>(expense));
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(ExpenseRequestModel expenseRequest)
        {
            var id = _expenseService.Add(_mapper.Map<Expense>(expenseRequest));

            return Ok(id);
        }

        [HttpDelete]
        public IActionResult Remove(int Id)
        {
            _expenseService.Remove(Id);
            return Ok();
        }

        [HttpGet("GetByPurpose")]
        public IActionResult GetExpenseByPurpose(string purpose) => Ok(_mapper.Map<ExpenseResponseModel>(_expenseService.GetByPurpose(purpose)));

        [HttpPut]
        public IActionResult UpdateExpense(int id, ExpenseRequestModel expenseRequest)
        {
            var expenseToUpdate = _mapper.Map<Expense>(expenseRequest);
            expenseToUpdate.Id = id;

            var result = _expenseService.Update(expenseToUpdate);
            return Ok(result);
        }

    }

}