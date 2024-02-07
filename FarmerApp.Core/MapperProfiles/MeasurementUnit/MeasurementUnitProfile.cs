using AutoMapper;
using FarmerApp.Core.Models.MeasurementUnit;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.MeasurementUnit
{
    public class MeasurementUnitProfile : Profile
    {
        public MeasurementUnitProfile()
        {
            CreateMap<MeasurementUnitModel, MeasurementUnitEntity>().ReverseMap();
            CreateMap<MeasurementUnitEntity, MeasurementUnitEntity>();
        }
    }
}
