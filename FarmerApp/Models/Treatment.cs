using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerApp.Models
{
    public class Treatment
	{
		public int Id { get; set; }
		public string DrugName { get; set; }
		public string DrugWeight { get; set; }
        public DateTime TreatmentDate { get; set; }

        [NotMapped]
        public string TreatedProductsIds { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

		public ICollection<Product> Products { get; set; }

    }
}