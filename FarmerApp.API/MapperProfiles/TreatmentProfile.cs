using AutoMapper;
using FarmerApp.Core.Models.Treatment;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class TreatmentProfile : Profile
    {
        public TreatmentProfile()
        {
            CreateMap<TreatmentRequestModel, TreatmentModel>();
            CreateMap<TreatmentModel, TreatmentResponseModel>();
        }
    }
}