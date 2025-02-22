using System.Linq.Expressions;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Specifications.Common;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Data.Specifications.Expense
{
    public class ExpensesOfCurrentYearSpecification : BaseSpecification<ExpenseEntity>
    {
        public ExpensesOfCurrentYearSpecification() 
            : base(x => EF.Functions.DateDiffYear(x.Date, DateTime.Now) == 0)
        {
        }
    }
}
