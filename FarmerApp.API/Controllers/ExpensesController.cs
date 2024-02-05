using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.Core.Models.Expense;
using FarmerApp.Core.Services.Expense;
using FarmerApp.Data.Entities;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.Controllers
{
    public class ExpensesController : BaseController<ExpenseEntity, ExpenseModel, ExpenseResponseModel, ExpenseRequestModel, ExpenseRequestModel>
    {
        public ExpensesController(IMapper mapper, IExpenseService expenseService)
           : base(expenseService, mapper)
        {
        }
    }
}