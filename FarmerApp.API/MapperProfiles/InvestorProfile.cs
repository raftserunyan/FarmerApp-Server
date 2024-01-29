using AutoMapper;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class InvestorProfile : Profile
    {
        public InvestorProfile()
        {
            CreateMap<InvestorRequestModel, InvestorModel>();           
            CreateMap<InvestorModel, InvestorResponseModel>();
        }
    }
}