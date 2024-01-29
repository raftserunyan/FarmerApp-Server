namespace FarmerApp.Core.Models.Investment
{
    public class InvestmentModel : BaseModel
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public int InvestorId { get; set; }
        public InvestorModel Investor { get; set; }
    }
}
