using FarmerApp.Models;

namespace FarmerApp.Services.IServices
{
    public interface IProductService : IService<Product>
    {
        Product GetById(int id);
    }
}