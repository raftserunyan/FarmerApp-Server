using AutoMapper;
using FarmerApp.Core.Models.Product;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Product
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, ProductEntity>().ReverseMap();
            CreateMap<ProductEntity, ProductEntity>();
        }
    }
}

