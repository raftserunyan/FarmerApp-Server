using FarmerApp.Core.Models.Product;

namespace FarmerApp.Models.ViewModels.ResponseModels
{
	public class SaleResponseModel
	{
		public int Id { get; set; }
		public double Weight { get; set; }
		public int PriceKG { get; set; }
		public int Paid { get; set; }
        public DateTime? Date { get; set; }
        public double Credit { get; set; }

		public ProductResponseModel Product { get; set; }
		public CustomerResponseModel Customer { get; set; }
	}
}

