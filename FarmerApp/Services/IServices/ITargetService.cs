using FarmerApp.Models;

namespace FarmerApp.Services.IServices
{
    public interface ITargetService : IService<Target>
    {
        Target GetById(int id);
    }
}
