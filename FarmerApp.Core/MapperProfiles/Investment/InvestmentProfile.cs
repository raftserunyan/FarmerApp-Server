using FarmerApp.Core.MapperProfiles.Common;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Core.MapperProfiles.Investment
{
    public class InvestmentProfile : BaseProfile<InvestmentEntity>
    {
        public InvestmentProfile()
        {
            CreateMap<InvestmentModel, InvestmentEntity>().ReverseMap();
            CreateMap<InvestmentEntity, InvestmentEntity>()
                .IncludeBase<BaseEntity, BaseEntity>()
                .ForMember(d => d.User, opts => opts.Ignore())
                .ForMember(d => d.Investor, opts => opts.Ignore());
        }
    }
}
