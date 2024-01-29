using AutoMapper;
using FarmerApp.Core.Models.Target;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;
using FarmerApp.Data.UnitOfWork;

namespace FarmerApp.Core.Services.Target
{
    internal class TargetService : CommonService<TargetProfile, TargetEntity>, ITargetService
    {
        public TargetService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}