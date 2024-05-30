using FarmerApp.Core.MapperProfiles.Common;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Core.MapperProfiles.Investment;

public class InvestorProfile : BaseProfile<InvestorEntity>
{
    public InvestorProfile()
    {
        CreateMap<InvestorModel, InvestorEntity>().ReverseMap();
        CreateMap<InvestorEntity, InvestorEntity>()
            .IncludeBase<BaseEntity, BaseEntity>()
            .ForMember(d => d.User, opts => opts.Ignore())
            .ForMember(d => d.Investments, opts => opts.Ignore())
            .ForMember(d => d.Expenses, opts => opts.Ignore());
    }
}

