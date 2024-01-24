using FarmerApp.Models;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.Services.IServices
{
    public interface ISaleService : IService<Sale>
    {
        Sale GetById(int id);
        IEnumerable<Sale> GetSalesByProductId(int Id);
        IEnumerable<Sale> GetSalesByCustomerId(int Id);
    }
}