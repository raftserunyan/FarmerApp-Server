using FarmerApp.Models;

namespace FarmerApp.Repository.IRepository
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        IEnumerable<Expense> GetByPurpose(string purpose);
    }
}