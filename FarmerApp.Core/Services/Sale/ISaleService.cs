using FarmerApp.Core.Models.Sale;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.Services.Sale
{
    public interface ISaleService : ICommonService<SaleProfile, SaleEntity>
    {
    }
}
