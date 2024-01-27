namespace FarmerApp.Models.ViewModels.RequestModels
{
    public class ExpenseRequestModel
    {
		public string ExpenseName { get; set; }
		public int ExpenseAmount { get; set; }
        public DateTime? Date { get; set; }

        public int TargetId { get; set; }
    }
}