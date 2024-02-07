using AutoMapper;
using FarmerApp.Core.Models.Expense;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;
using FarmerApp.Data.UnitOfWork;

namespace FarmerApp.Core.Services.Expense
{
    internal class ExpenseService : CommonService<ExpenseModel, ExpenseEntity>, IExpenseService
    {
        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}