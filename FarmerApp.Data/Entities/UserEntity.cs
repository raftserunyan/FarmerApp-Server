using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }

        public ICollection<ProductEntity> Products { get; set; }
        public ICollection<CustomerEntity> Customers { get; set; }
        public ICollection<ExpenseEntity> Expenses { get; set; }
        public ICollection<InvestorEntity> Investors { get; set; }
        public ICollection<SaleEntity> Sales { get; set; }
        public ICollection<TreatmentEntity> Treatments { get; set; }
    }
}
