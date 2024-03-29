using FarmerApp.Core.MapperProfiles.Common;
using FarmerApp.Core.Models.Treatment;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Core.MapperProfiles.Treatment
{
    public class TreatmentProfile : BaseProfile<TreatmentEntity>
    {
        public TreatmentProfile()
        {
            CreateMap<TreatmentModel, TreatmentEntity>().ReverseMap();
            CreateMap<TreatmentEntity, TreatmentEntity>()
                .IncludeBase<BaseEntity, BaseEntity>()
                .ForMember(d => d.Products, opts => opts.Ignore());
        }
    }
}