using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private FarmerDbContext _dbContext;
        private IMapper _mapper;

        public UserRepository(
            IMapper mapper,
            FarmerDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public void SetUser(int userId)
        {
        }

        public List<User> GetAll() => _dbContext.Users.ToList();

        public int Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public void Remove(int id)
        {
            _dbContext.Users.Remove(_dbContext.Users.SingleOrDefault(x => x.Id == id));
            _dbContext.SaveChanges();
        }

        public User GetById(int id) => _dbContext.Users
            .Include(x => x.Products)
                .ThenInclude(x => x.Sales)
            .Include(x => x.Expenses)
            .Include(x => x.Customers)
                .ThenInclude(x => x.Sales)
            .Include(x => x.Investors)
                .ThenInclude(x => x.Investments)
            .Include(x => x.Treatments)
            .Include(x => x.Sales)
                .ThenInclude(x => x.CurrentCustomer)
            .Include(x => x.Sales)
                .ThenInclude(x => x.CurrentProduct)
            .SingleOrDefault(x => x.Id == id);

        public User Update(User user)
        {
            var userToUpdate = _dbContext.Users.SingleOrDefault(x => x.Id == user.Id);

            _mapper.Map(user, userToUpdate);

            _dbContext.SaveChanges();

            return user;
        }

        public User GetByEmail(string email)
        {
            email = email.ToLower();
            return _dbContext.Users.SingleOrDefault(x => x.Email == email);
        }
    }
}
