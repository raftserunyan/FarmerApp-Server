using AutoMapper;
using FarmerApp.Core.Models.Product;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestModel, ProductModel>();            
            CreateMap<ProductModel, ProductResponseModel>();
        }
    }
}