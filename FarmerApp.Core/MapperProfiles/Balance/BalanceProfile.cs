using AutoMapper;
using FarmerApp.Core.Models.Balance;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Balance
{
    public class BalanceProfile : Profile
    {
        public BalanceProfile()
        {
            CreateMap<BalanceModel, BalanceEntity>().ReverseMap();
            CreateMap<BalanceEntity, BalanceEntity>();
        }
    }
}