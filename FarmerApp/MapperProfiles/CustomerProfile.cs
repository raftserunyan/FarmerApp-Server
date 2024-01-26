using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class CustomerProfile : Profile
    {
        // private readonly ISaleService _saleService;
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerResponseModel>();
            CreateMap<CustomerRequestModel, Customer>();
            //Map for Update
            CreateMap<Customer, Customer>();
        }
    }
}