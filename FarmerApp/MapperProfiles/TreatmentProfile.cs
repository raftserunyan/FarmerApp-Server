using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class TreatmentProfile : Profile
    {
        public TreatmentProfile()
        {
            CreateMap<TreatmentRequestModel, Treatment>();
            CreateMap<Treatment, TreatmentResponseModel>();
            CreateMap<Treatment, Treatment>();
        }
    }
}