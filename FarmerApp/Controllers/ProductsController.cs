using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;
using FarmerApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private IMapper _mapper;

        public ProductsController(
            IMapper mapper,
            IProductService productService,
            IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _productService = productService;
            _productService.SetUser(int.Parse(httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();

            var productResponses = new List<ProductResponseModel>();

            foreach(var product in products)
                productResponses.Add(_mapper.Map<ProductResponseModel>(product));

            return Ok(productResponses);
        }

        [HttpPost]
        public IActionResult Add(ProductRequestModel productRequest)
        {
            var id = _productService.Add(_mapper.Map<Product>(productRequest));

            return Ok(id);
        }

        [HttpDelete]
        public IActionResult Remove(int id)
        {
            try
            {
                _productService.Remove(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("GetProductById")]
        public IActionResult GetById(int id) => Ok(_mapper.Map<ProductResponseModel>(_productService.GetById(id)));

        [HttpPut]
        public IActionResult UpdateProduct(int id, ProductRequestModel productRequest){
            var productToUpdate = _mapper.Map<Product>(productRequest);
            productToUpdate.Id = id;

            var result = _productService.Update(productToUpdate);
            return Ok(result);
        }
    }
}
