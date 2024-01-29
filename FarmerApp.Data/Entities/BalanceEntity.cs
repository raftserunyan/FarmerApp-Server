using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Data.Entities
{
    public class BalanceEntity : BaseEntity
    {
        public int Leftover { get; set; }
        public int Debt { get; set; }
    }
}