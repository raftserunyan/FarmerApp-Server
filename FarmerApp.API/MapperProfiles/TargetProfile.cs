using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;
using AutoMapper;
using FarmerApp.Core.Models.Target;

namespace FarmerApp.MapperProfiles
{
    public class TargetProfile : Profile
    {
        public TargetProfile()
        {
            CreateMap<TargetRequestModel, TargetModel>();
            CreateMap<TargetModel, TargetResponseModel>();
        }
    }
}
