using FarmerApp.Models;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.Services.IServices
{
    public interface ICustomerService : IService<Customer>
    {
        Customer GetById(int id);
        IEnumerable<Customer> GetCustomersByLocation(string address);
    }
}