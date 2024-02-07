using AutoMapper;
using FarmerApp.Core.Models.Treatment;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;
using FarmerApp.Data.UnitOfWork;

namespace FarmerApp.Core.Services.Treatment
{
    internal class TreatmentService : BaseService<TreatmentModel, TreatmentEntity>, ITreatmentService
    {
        public TreatmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}