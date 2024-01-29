using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Data.Entities
{
	public class InvestorEntity : BaseEntity
    {
		public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public int? UserId { get; set; }
        public UserEntity User { get; set; }

        public ICollection<InvestmentEntity> Investments { get; set; }
    }
}

