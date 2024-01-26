using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
    public class ExpenseService : IExpenseService
    {
        private IExpenseRepository _expenseRepository;
        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public void SetUser(int userId)
        {
            _expenseRepository.SetUser(userId);
        }

        public List<Expense> GetAll() => _expenseRepository.GetAll();

        public int Add(Expense expense) => _expenseRepository.Add(expense);

        public void Remove(int id) => _expenseRepository.Remove(id);

        public IEnumerable<Expense> GetByPurpose(string purpose) => _expenseRepository.GetByPurpose(purpose);

        public Expense GetById(int id) => _expenseRepository.GetById(id);

        public Expense Update(Expense expense) => _expenseRepository.Update(expense);
    }
}