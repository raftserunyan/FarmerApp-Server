using AutoMapper;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class InvestmentProfile : Profile
    {
        public InvestmentProfile()
        {
            CreateMap<InvestmentRequestModel, InvestmentModel>();
            CreateMap<InvestmentModel, InvestmentResponseModel>();
        }
    }
}
