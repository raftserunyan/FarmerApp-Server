using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Data.Entities
{
	public class ProductEntity : BaseEntity
    {
		public string Name { get; set; }
		public int PriceKG { get; set; }

        public int? UserId { get; set; }
        public UserEntity User { get; set; }
	
		public ICollection<SaleEntity> Sales { get; set; }
		public ICollection<TreatmentEntity> Treatments { get; set; }
    }
}

