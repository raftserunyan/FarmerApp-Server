using AutoMapper;
using FarmerApp.Core.Models.Target;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Target
{
    public class TargetProfile : Profile
    {
        public TargetProfile()
        {
            CreateMap<TargetModel, TargetEntity>().ReverseMap();
            CreateMap<TargetEntity, TargetEntity>();
        }
    }
}
