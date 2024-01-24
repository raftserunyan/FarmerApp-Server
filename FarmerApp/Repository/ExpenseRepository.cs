using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private FarmerDbContext _dbContext;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private int _userId; //private User _user;

        public ExpenseRepository(//IUserRepository userRepository,
            FarmerDbContext dbContext,
            IMapper mapper
            )
        {
            //_userRepository = userRepository;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void SetUser(int userId)
        {
            _userId = userId; //_user = _userRepository.GetById(userId);
        }

        public List<Expense> GetAll() => _dbContext.Expenses.AsNoTracking().ToList();

        public int Add(Expense expense)
        {
            expense.UserId = _userId; //_user.Id;
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();

            return expense.Id;
        }

        public void Remove(int Id)
        {
            _dbContext.Expenses.Remove(_dbContext.Expenses.SingleOrDefault(x => x.Id == Id));
            _dbContext.SaveChanges();
        }

        public IEnumerable<Expense> GetByPurpose(string purpose) => _dbContext.Expenses.AsNoTracking().Where(x => x.UserId == _userId && x.ExpensePurpose.ToLower().Contains(purpose.ToLower()));

        public Expense GetById(int Id) => _dbContext.Expenses.AsNoTracking().SingleOrDefault(x => x.Id == Id);

        public Expense Update(Expense expense)
        {
            expense.UserId = _userId; //_user.Id;
            var expenseToUpdate = _dbContext.Expenses.SingleOrDefault(x => x.Id == expense.Id);

            _mapper.Map(expense, expenseToUpdate);

            _dbContext.SaveChanges();

            return expense;
        }
    }
}

