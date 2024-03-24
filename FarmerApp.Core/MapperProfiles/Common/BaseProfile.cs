using AutoMapper;
using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Core.MapperProfiles.Common
{
    public class BaseProfile<TEntity> : Profile
        where TEntity : BaseEntity
    {
        public BaseProfile()
        {
            CreateMap<TEntity, TEntity>()
                .ForMember(d => d.CreatedDate, opts => opts.Ignore())
                .ForMember(d => d.LastUpdatedDate, opts => opts.Ignore());

            CreateMap<BaseEntity, BaseEntity>()
                .ForMember(d => d.CreatedDate, opts => opts.Ignore())
                .ForMember(d => d.LastUpdatedDate, opts => opts.Ignore());
        }
    }
}
