using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Entities.Interfaces;

namespace FarmerApp.Data.Entities
{
	public class ExpenseEntity : BaseEntity, IHasUser
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

