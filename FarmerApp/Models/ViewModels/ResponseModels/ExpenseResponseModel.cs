namespace FarmerApp.Models.ViewModels.ResponseModels
{
	public class ExpenseResponseModel
	{
		public int Id { get; set; }
		public string ExpenseName { get; set; }
		public int ExpenseAmount { get; set; }
		public string ExpensePurpose{ get; set; }
		public DateTime ExpenseDate { get; set; }
	}
}

