using FarmerApp.Models;

namespace FarmerApp.Repository.IRepository
{
    public interface ISaleRepository : IRepository<Sale>
    {
        IEnumerable<Sale> GetSalesByProductId(int Id);
        IEnumerable<Sale> GetSalesByCustomerId(int Id);
    }
}