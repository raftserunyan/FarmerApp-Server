using AutoMapper;
using FarmerApp.Core.Models.Investment;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;
using FarmerApp.Data.UnitOfWork;

namespace FarmerApp.Core.Services.Investment
{
    internal class InvestorService : CommonService<InvestorModel, InvestorEntity>, IInvestorService
    {
        public InvestorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}