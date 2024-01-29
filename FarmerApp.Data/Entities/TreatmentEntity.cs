using FarmerApp.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerApp.Data.Entities
{
    public class TreatmentEntity : BaseEntity
    {
		public string DrugName { get; set; }
		public string DrugWeight { get; set; }
        public DateTime? Date { get; set; }

        [NotMapped]
        public string TreatedProductsIds { get; set; }

        public int? UserId { get; set; }
        public UserEntity User { get; set; }

		public ICollection<ProductEntity> Products { get; set; }

    }
}