using FarmerApp.Data.Entities;

namespace FarmerApp.Models.ViewModels.RequestModels
{
    public class InvestmentRequestModel
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int InvestorId { get; set; }
        public int? TargetId { get; set; }
    }
}
