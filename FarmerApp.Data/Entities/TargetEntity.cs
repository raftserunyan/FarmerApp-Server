using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Data.Entities
{
    public class TargetEntity : BaseEntity
    {
        public string Name { get; set; }

        public int? UserId { get; set; }
        public UserEntity User { get; set; }

        public List<ExpenseEntity> Expenses { get; set; }
    }
}
