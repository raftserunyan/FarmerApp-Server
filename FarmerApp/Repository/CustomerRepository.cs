using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Services
{
    public class CustomerRepository : ICustomerRepository
	{
        private FarmerDbContext _dbContext;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private int _userId; //private User _user;

        public CustomerRepository(//IUserRepository userRepository,
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
            _userId = userId;
            //_userId = userId; //_user = _userRepository.GetById(userId);
        }

        public List<Customer> GetAll() => _dbContext.Customers.AsNoTracking().Include(x => x.Sales).Where(x => x.UserId == _userId).ToList();

        public int Add(Customer customer)
        {
            customer.UserId = _userId;
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return customer.Id;
        }

        //TODO: If exists Sale with this customer.
            //Do the same with Product and return message.
        public void Remove(int id)
        {
            _dbContext.Customers.Remove(_dbContext.Customers.SingleOrDefault(x => x.Id == id));
            _dbContext.SaveChanges();
        }

        public Customer Update(Customer customer)
        {
            customer.UserId = _userId;
            var customerToUpdate = _dbContext.Customers.SingleOrDefault(x => x.Id == customer.Id);

            _mapper.Map(customer, customerToUpdate);

            _dbContext.SaveChanges();

            return customer;
        }

        public IEnumerable<Customer> GetCustomersByLocation(string address) => _dbContext.Customers.AsNoTracking()
            .Where(x => x.UserId == _userId && x.Address.ToLower().Contains(address.ToLower()));

        public Customer GetById(int id) => _dbContext.Customers.AsNoTracking().SingleOrDefault(x => x.Id == id);

    }
}