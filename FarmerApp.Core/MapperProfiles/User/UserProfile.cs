using AutoMapper;
using FarmerApp.Core.Models.User;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserEntity>().ReverseMap();
            CreateMap<UserEntity, UserEntity>();
        }
    }
}
