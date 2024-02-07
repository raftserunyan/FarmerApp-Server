using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Entities.Interfaces;

namespace FarmerApp.Data.Entities
{
	public class ProductEntity : BaseEntity, IHasUser
    {
		public string Name { get; set; }
		public int PriceKG { get; set; }

        public int? UserId { get; set; }
        public UserEntity User { get; set; }
	
		public ICollection<SaleEntity> Sales { get; set; }
		public ICollection<TreatmentEntity> Treatments { get; set; }
    }
}

