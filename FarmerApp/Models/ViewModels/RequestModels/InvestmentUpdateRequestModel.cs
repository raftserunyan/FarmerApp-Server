namespace FarmerApp.Models.ViewModels.RequestModels
{
    public class InvestmentUpdateRequestModel
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public int InvestorId { get; set; }
    }
}
