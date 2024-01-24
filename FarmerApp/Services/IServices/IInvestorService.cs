using FarmerApp.Models;

namespace FarmerApp.Services.IServices
{
    public interface IInvestorService : IService<Investor>
    {
        Investor GetById(int id);
    }
}