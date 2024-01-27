using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;
using FarmerApp.Models;
using AutoMapper;

namespace FarmerApp.MapperProfiles
{
    public class TargetProfile : Profile
    {
        public TargetProfile()
        {
            CreateMap<TargetRequestModel, Target>();
            CreateMap<Target, TargetResponseModel>();
            CreateMap<Target, Target>();
        }
    }
}
