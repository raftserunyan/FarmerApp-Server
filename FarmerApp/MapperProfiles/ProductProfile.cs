using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class ProductProfile : Profile
    {
        // private readonly ISaleService _saleService;
        public ProductProfile()
        {
            CreateMap<ProductRequestModel, Product>();
            
            CreateMap<Product, ProductResponseModel>();

            //Map for Update
            CreateMap<Product, Product>();
        }
    }
}