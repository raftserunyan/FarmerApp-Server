using FarmerApp.Data.Entities;
using FarmerApp.Data.Specifications.Common;

namespace FarmerApp.Data.Specifications.Investments
{
    public class InvestmentWithDepsSpecification : BaseSpecification<InvestmentEntity>
    {
        public InvestmentWithDepsSpecification()
        {
            AddInclude(x => x.Investor);
        }
    }
}
