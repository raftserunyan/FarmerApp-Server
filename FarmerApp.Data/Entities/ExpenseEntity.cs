using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Data.Entities
{
	public class ExpenseEntity : BaseEntity
    {
		public string ExpenseName { get; set; }
		public int ExpenseAmount { get; set; }
        public DateTime? Date { get; set; }

        public int TargetId { get; set; }
        public TargetEntity Target { get; set; }

        public int? UserId { get; set; }
		public UserEntity User { get; set; }
	}
}

