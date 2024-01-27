using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleRequestModel, Sale>();
            CreateMap<Sale, SaleResponseModel>();
            CreateMap<Sale, Sale>();
        }
    }
}