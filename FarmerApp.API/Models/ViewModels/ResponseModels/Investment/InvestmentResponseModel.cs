using FarmerApp.Data.Entities;

namespace FarmerApp.API.Models.ViewModels.ResponseModels.Investment
{
    public class InvestmentResponseModel
    {
        public int Id { get; set; }

        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public int? TargetId { get; set; }
        public TargetEntity Target { get; set; }

        public int InvestorId { get; set; }
        public InvestorResponseModel Investor { get; set; }
    }
}
