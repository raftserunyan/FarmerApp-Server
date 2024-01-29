using AutoMapper;
using FarmerApp.Core.Models.Customer;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Customer
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, CustomerEntity>().ReverseMap();
            CreateMap<CustomerEntity, CustomerEntity>();
        }
    }
}
