using FarmerApp.Models;

namespace FarmerApp.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetCustomersByLocation(string address);
    }
}