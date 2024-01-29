using FarmerApp.Core.Models.Target;
using FarmerApp.Core.Models.User;

namespace FarmerApp.Core.Models.Expense
{
    public class ExpenseModel : BaseModel
    {
        public string ExpenseName { get; set; }
        public int ExpenseAmount { get; set; }
        public DateTime? Date { get; set; }

        public int TargetId { get; set; }
        public TargetModel Target { get; set; }

        public int? UserId { get; set; }
        public UserModel User { get; set; }
    }
}

