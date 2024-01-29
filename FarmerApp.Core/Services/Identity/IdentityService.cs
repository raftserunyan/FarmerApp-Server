using AutoMapper;
using FarmerApp.Data.UnitOfWork;

namespace FarmerApp.Core.Services.Identity
{
    internal class IdentityService : IIdentityService
    {
        public IdentityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
        }
    }
}