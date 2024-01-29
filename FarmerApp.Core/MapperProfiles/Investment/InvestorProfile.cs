using AutoMapper;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Investment;

public class InvestorProfile : Profile
{
    public InvestorProfile()
    {
        CreateMap<InvestorModel, InvestorEntity>().ReverseMap();
        CreateMap<InvestorEntity, InvestorEntity>();
    }
}

