using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class InvestorProfile : Profile
    {
        // private readonly ISaleService _saleService;
        public InvestorProfile()
        {
            CreateMap<InvestorRequestModel, Investor>();

            //Map for Update
            CreateMap<Investor, Investor>();
            
            CreateMap<Investor, InvestorResponseModel>();
        }
    }
}