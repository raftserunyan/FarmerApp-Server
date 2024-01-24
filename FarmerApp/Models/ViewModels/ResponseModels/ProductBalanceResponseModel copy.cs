namespace FarmerApp.Models.ViewModels.ResponseModels
{
    public class ProductBalanceResponseModel
    {
        public double Weight { get; set; }
        public int Leftover { get; set; }
        public int Debt { get; set; }
    }
}