using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Data.Entities
{
    public class InvestmentEntity : BaseEntity
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public int InvestorId { get; set; }
        public InvestorEntity Investor { get; set; }
    }
}
