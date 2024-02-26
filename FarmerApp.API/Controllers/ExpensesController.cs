using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.API.Models.ViewModels.ResponseModels.Expense;
using FarmerApp.Core.Models.Expense;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Expense;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Specifications.Expense;
using FarmerApp.Models.ViewModels.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FarmerApp.Controllers
{
    public class ExpensesController : BaseController<ExpenseEntity, ExpenseModel, ExpenseResponseModel, ExpenseRequestModel, ExpenseRequestModel>
    {
        public ExpensesController(IMapper mapper, IExpenseService expenseService)
           : base(expenseService, mapper)
        {
        }

        public override async Task<ActionResult<PagedResult<ExpenseResponseModel>>> Read([FromBody] BaseQueryModel query)
        {
            var expenses = await _service.GetAll(new AllExpensesWithDepsSpecification(), query);

            return _mapper.Map<PagedResult<ExpenseResponseModel>>(expenses);
        }

        public override async Task<ActionResult<ExpenseResponseModel>> Create([BindRequired, FromBody] ExpenseRequestModel model)
        {
            var businessModel = _mapper.Map<ExpenseModel>(model);
            businessModel.UserId = UserId;

            var data = await _service.Add(businessModel);
            var entity = await _service.GetFirstBySpecification(new ExpenseByIdWithDepsSpecification((int)data.Id));

            // Fix this when depth builder is available
            return _mapper.Map<ExpenseResponseModel>(entity);
        }
    }
}