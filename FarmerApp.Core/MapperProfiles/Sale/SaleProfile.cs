using FarmerApp.Core.MapperProfiles.Common;
using FarmerApp.Core.Models.Sale;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Core.MapperProfiles.Sale
{
    public class SaleProfile : BaseProfile<SaleEntity>
    {
        public SaleProfile()
        {
            CreateMap<SaleModel, SaleEntity>().ReverseMap();
            CreateMap<SaleEntity, SaleEntity>()
                .IncludeBase<BaseEntity, BaseEntity>()
                .ForMember(d => d.User, opts => opts.Ignore())
                .ForMember(d => d.Product, opts => opts.Ignore())
                .ForMember(d => d.Customer, opts => opts.Ignore());
        }
    }
}

