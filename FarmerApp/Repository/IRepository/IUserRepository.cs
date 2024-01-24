using FarmerApp.Models;

namespace FarmerApp.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}
