using AutoMapper;
using FarmerApp.Core.Models.Sale;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Sale
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleModel, SaleEntity>().ReverseMap();
            CreateMap<SaleEntity, SaleEntity>();
        }
    }
}

