using AutoMapper;
using FarmerApp.Core.Models.Sale;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;
using FarmerApp.Data.UnitOfWork;

namespace FarmerApp.Core.Services.Sale
{
    internal class SaleService : CommonService<SaleModel, SaleEntity>, ISaleService
    {
        public SaleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}