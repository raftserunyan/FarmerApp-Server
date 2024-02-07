using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.API.Models.ViewModels.ResponseModels.Expense;
using FarmerApp.Core.Models.Expense;
using FarmerApp.Core.Services.Expense;
using FarmerApp.Data.Entities;
using FarmerApp.Models.ViewModels.RequestModels;

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