using AutoMapper;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;
using FarmerApp.Data.UnitOfWork;

namespace FarmerApp.Core.Services.Investment
{
    internal class InvestmentService : CommonService<InvestmentModel, InvestmentEntity>, IInvestmentService
    {
        public InvestmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}