namespace FarmerApp.Core.Models.Balance
{
    public class BalanceModel : BaseModel
    {
        public int Leftover { get; set; }
        public int Debt { get; set; }
    }
}