using FarmerApp.Models;

namespace FarmerApp.Services.IServices
{
    public interface IInvestmentService : IService<Investment>
    {
        Investment GetById(int id);
    }
}
