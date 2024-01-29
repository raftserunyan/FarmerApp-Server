namespace FarmerApp.Models.ViewModels.ResponseModels
{
    public class CustomerBalanceResponseModel
    {
        public int CustomerId { get; set; }
        public int Leftover { get; set; }
        public int Debt { get; set; }
    }
}