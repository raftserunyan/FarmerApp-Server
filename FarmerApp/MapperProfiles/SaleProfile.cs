using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleRequestModel, Sale>()
                .ForMember(sale => sale.Date,
                            opts => opts.MapFrom(saleRequest => DateTime.Now));
            // .ForMember(sale => sale.CurrentCustomer,
            //             opts => opts.MapFrom(saleRequest => CustomerService.GetCustomerById(saleRequest.CustomerId)));

            CreateMap<Sale, SaleResponseModel>()
                .ForMember(saleResponse => saleResponse.Credit,
                    opts => opts.MapFrom(sale => sale.Weight * sale.PriceKG - sale.Payed));

            //Map for Update
            CreateMap<Sale, Sale>();
        }
    }
}