using FarmerApp.Core.Models.Expense;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.Services.Expense
{
    public interface IExpenseService : ICommonService<ExpenseModel, ExpenseEntity>
    {
    }
}
