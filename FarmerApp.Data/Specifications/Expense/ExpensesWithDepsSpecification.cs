using FarmerApp.Data.Entities;
using FarmerApp.Data.Specifications.Common;

namespace FarmerApp.Data.Specifications.Expense
{
    public class ExpensesWithDepsSpecification : BaseSpecification<ExpenseEntity>
    {
        public ExpensesWithDepsSpecification()
        {
            AddInclude(x => x.Target);
        }
    }
}
