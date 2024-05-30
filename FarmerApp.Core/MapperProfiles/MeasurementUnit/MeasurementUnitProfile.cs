using FarmerApp.Core.MapperProfiles.Common;
using FarmerApp.Core.Models.MeasurementUnit;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Core.MapperProfiles.MeasurementUnit
{
    public class MeasurementUnitProfile : BaseProfile<MeasurementUnitEntity>
    {
        public MeasurementUnitProfile()
        {
            CreateMap<MeasurementUnitModel, MeasurementUnitEntity>().ReverseMap();
            CreateMap<MeasurementUnitEntity, MeasurementUnitEntity>()
                .IncludeBase<BaseEntity, BaseEntity>()
                .ForMember(d => d.User, opts => opts.Ignore());
        }
    }
}
