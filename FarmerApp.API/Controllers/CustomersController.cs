using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.Core.Models.Customer;
using FarmerApp.Core.Services.Customer;
using FarmerApp.Data.Entities;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.Controllers
{
    public class CustomersController : BaseController<CustomerEntity, CustomerModel, CustomerResponseModel, CustomerRequestModel, CustomerRequestModel>
    {
        public CustomersController(IMapper mapper, ICustomerService customerService) 
            : base (customerService, mapper)
        {                        
        }
    }
}