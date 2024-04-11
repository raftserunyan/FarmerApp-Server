using FarmerApp.Core.MapperProfiles.Common;
using FarmerApp.Core.Models.Sale;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Sale
{
    public class SaleProfile : BaseProfile<SaleEntity>
    {
        public SaleProfile()
        {
            CreateMap<SaleModel, SaleEntity>().ReverseMap();
            CreateMap<SaleEntity, SaleEntity>()
                .ForMember(d => d.User, opts => opts.Ignore());
        }
    }
}

