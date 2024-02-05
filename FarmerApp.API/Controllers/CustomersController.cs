using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.Core.Models.Customer;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Customer;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Specifications.Customer;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    //[Authorize]
    public class CustomersController : BaseController<CustomerEntity, CustomerModel, CustomerResponseModel, CustomerRequestModel, CustomerRequestModel>
    {
        public CustomersController(
            IMapper mapper,
            ICustomerService customerService,
            IHttpContextAccessor httpContext) : base (customerService, mapper)
        {                        
        }

        #region Commented out
        //[HttpPost("get")]
        //public async Task<IActionResult> GetAll([FromBody] BaseQueryModel query)
        //{
        //    var customers = await _customerService.GetAll(new CustomersByUserIdSpecification(_userId), query);

        //    return Ok(_mapper.Map<PagedResult<CustomerResponseModel>>(customers));
        //}

        //[HttpGet("GetById")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    return Ok(_mapper.Map<CustomerResponseModel>(await _customerService.GetById(id)));
        //}

        //[HttpPost]
        //public async Task<IActionResult> Add(CustomerRequestModel customerRequest)
        //{
        //    var id = await _customerService.Add(_mapper.Map<CustomerModel>(customerRequest));

        //    return Ok(id);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Remove(int id)
        //{
        //    await _customerService.Delete(id);
        //    return Ok();
        //}        

        //[HttpPut]
        //public async Task<IActionResult> UpdateCustomer(int id, CustomerRequestModel customerRequest)
        //{
        //    var customerToUpdate = _mapper.Map<CustomerModel>(customerRequest);
        //    customerToUpdate.Id = id;

        //    var result = await _customerService.Update(customerToUpdate);

        //    return Ok(_mapper.Map<CustomerResponseModel>(result));
        //}
        #endregion
    }
}