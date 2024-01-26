namespace FarmerApp.Models
{
    public class Investment
    {
        public int Id { get; set; }

        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public int InvestorId { get; set; }
        public Investor Investor { get; set; }
    }
}
