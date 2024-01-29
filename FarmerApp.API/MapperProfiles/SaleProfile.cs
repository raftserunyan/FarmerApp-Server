using AutoMapper;
using FarmerApp.Core.Models.Sale;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleRequestModel, SaleModel>();
            CreateMap<SaleModel, SaleResponseModel>();
        }
    }
}