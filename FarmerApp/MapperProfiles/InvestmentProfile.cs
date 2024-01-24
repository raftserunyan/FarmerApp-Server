using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class InvestmentProfile : Profile
    {
        public InvestmentProfile()
        {
            CreateMap<InvestmentRequestModel, Investment>()
                .ForMember(x => x.Date, opts => opts.MapFrom(y => DateTime.Now));

            CreateMap<InvestmentUpdateRequestModel, Investment>();
            CreateMap<Investment, Investment>();
            CreateMap<Investment, InvestmentResponseModel>();
        }
    }
}
