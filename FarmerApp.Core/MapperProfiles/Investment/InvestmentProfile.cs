using AutoMapper;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Investment
{
    public class InvestmentProfile : Profile
    {
        public InvestmentProfile()
        {
            CreateMap<InvestmentModel, InvestmentEntity>().ReverseMap();
            CreateMap<InvestmentEntity, InvestmentEntity>();
        }
    }
}
