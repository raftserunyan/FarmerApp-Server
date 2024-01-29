using AutoMapper;
using FarmerApp.Core.Models.Customer;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, CustomerResponseModel>();
            CreateMap<CustomerRequestModel, CustomerModel>();
        }
    }
}