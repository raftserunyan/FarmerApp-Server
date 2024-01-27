namespace FarmerApp.Models
{
	public class Expense
	{
		public int Id { get; set; }
		public string ExpenseName { get; set; }
		public int ExpenseAmount { get; set; }
        public DateTime? Date { get; set; }

        public int TargetId { get; set; }
        public Target Target { get; set; }

        public int? UserId { get; set; }
		public User User { get; set; }
	}
}

