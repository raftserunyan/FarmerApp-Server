using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private ISaleRepository _saleRepository;
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public CustomerService(
            IMapper mapper,
            ISaleRepository saleRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _saleRepository = saleRepository;
            _userRepository = userRepository;
        }

        public void SetUser(int userId)
        {
            _customerRepository.SetUser(userId);
        }

        public List<Customer> GetAll() => _customerRepository.GetAll();

        public int Add(Customer customer) => _customerRepository.Add(customer);

        public void Remove(int id) => _customerRepository.Remove(id);

        public Customer Update(Customer customer) => _customerRepository.Update(customer);

        public IEnumerable<Customer> GetCustomersByLocation(string address) => _customerRepository.GetCustomersByLocation(address);

        public Customer GetById(int id)
        {
            var customer = _customerRepository.GetById(id);

            // add balance to response
            // var customerSales = _saleRepository.GetSalesByCustomerId(customer.Id);


                // _mapper.Map<CustomerSaleResponseModel>(sale);

            // var customerResponse = _mapper.Map<CustomerResponseModel>(customer);
            // customerResponse.CustomerPurchases = salesResponse;

            return customer;
        }
    }
}