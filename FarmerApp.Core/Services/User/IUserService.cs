using FarmerApp.Core.Models.User;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.Services.User
{
    public interface IUserService : ICommonService<UserModel, UserEntity>
    {
    }
}
