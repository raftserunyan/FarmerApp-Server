using FarmerApp.Core.Models.Interfaces;
using FarmerApp.Core.Models.User;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.Models.Investment
{
    public class InvestmentModel : BaseModel, IHasUserModel
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public int InvestorId { get; set; }
        public InvestorModel Investor { get; set; }

        public int? TargetId { get; set; }
        public TargetEntity Target { get; set; }

        public int? UserId { get; set; }
        public UserModel User { get; set; }
    }
}
