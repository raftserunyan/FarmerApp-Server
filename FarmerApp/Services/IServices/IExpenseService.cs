using FarmerApp.Models;

namespace FarmerApp.Services.IServices
{
    public interface IExpenseService : IService<Expense>
    {
        Expense GetById(int id);
        IEnumerable<Expense> GetByPurpose(string purpose);
    }
}