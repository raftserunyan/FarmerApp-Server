using FarmerApp.Exceptions;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
    public class ExpenseService : IExpenseService
    {
        private IExpenseRepository _expenseRepository;
        private ITargetRepository _targetRepository;

        public ExpenseService(IExpenseRepository expenseRepository, ITargetRepository targetRepository)
        {
            _expenseRepository = expenseRepository;
            _targetRepository = targetRepository;
        }

        public void SetUser(int userId)
        {
            _expenseRepository.SetUser(userId);
        }

        public List<Expense> GetAll() => _expenseRepository.GetAll();

        public int Add(Expense expense)
        {
            if (_targetRepository.GetById(expense.TargetId) is null)
                throw new NotFoundException($"Target with id {expense.TargetId} not found");

            return _expenseRepository.Add(expense);
        }

        public void Remove(int id) => _expenseRepository.Remove(id);

        public Expense GetById(int id) => _expenseRepository.GetById(id);

        public Expense Update(Expense expense) => _expenseRepository.Update(expense);
    }
}