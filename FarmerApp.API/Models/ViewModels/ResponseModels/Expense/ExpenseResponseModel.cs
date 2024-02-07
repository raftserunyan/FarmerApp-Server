using FarmerApp.API.Models.ViewModels.ResponseModels.Target;

namespace FarmerApp.API.Models.ViewModels.ResponseModels.Expense
{
    public class ExpenseResponseModel
    {
        public int Id { get; set; }
        public string ExpenseName { get; set; }
        public int ExpenseAmount { get; set; }
        public DateTime? Date { get; set; }

        public TargetResponseModel Target { get; set; }
    }
}

