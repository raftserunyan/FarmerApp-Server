using FarmerApp.Core.MapperProfiles.Common;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Investment;

public class InvestorProfile : BaseProfile<InvestmentEntity>
{
    public InvestorProfile()
    {
        CreateMap<InvestorModel, InvestorEntity>().ReverseMap();
        CreateMap<InvestorEntity, InvestorEntity>();
    }
}

