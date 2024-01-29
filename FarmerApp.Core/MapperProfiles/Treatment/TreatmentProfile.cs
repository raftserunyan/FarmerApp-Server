using AutoMapper;
using FarmerApp.Core.Models.Treatment;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Treatment
{
    public class TreatmentProfile : Profile
    {
        public TreatmentProfile()
        {
            CreateMap<TreatmentModel, TreatmentEntity>().ReverseMap();
            CreateMap<TreatmentEntity, TreatmentEntity>();
        }
    }
}