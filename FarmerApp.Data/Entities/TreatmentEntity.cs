using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerApp.Data.Entities
{
    public class TreatmentEntity : BaseEntity, IHasUser
    {
		public string DrugName { get; set; }
		public double DrugAmount { get; set; }
        public DateTime? Date { get; set; }

        [NotMapped]
        public string TreatedProductsIds { get; set; }

        public int? MeasurementUnitId { get; set; }
        public MeasurementUnitEntity MeasurementUnit { get; set; }

        public int? UserId { get; set; }
        public UserEntity User { get; set; }

		public ICollection<ProductEntity> Products { get; set; }

    }
}