namespace FarmerApp.Models
{
	public class Expense
	{
		public int Id { get; set; }
		public string ExpenseName { get; set; }
		public int ExpenseAmount { get; set; }
		public string ExpensePurpose{ get; set; }
		public DateTime ExpenseDate { get; set; }

		public int? UserId { get; set; }
		public User User { get; set; }
	}
}

