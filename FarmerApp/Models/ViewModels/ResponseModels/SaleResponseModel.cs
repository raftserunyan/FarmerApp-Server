namespace FarmerApp.Models.ViewModels.ResponseModels
{
	public class SaleResponseModel
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public Product CurrentProduct { get; set; }
		public double Weight { get; set; }
		public int PriceKG { get; set; }
		public int CustomerId { get; set; }
		public Customer CurrentCustomer { get; set; }
		public int Payed { get; set; }
		public DateTime Date { get; set; }
        public double Credit { get; set; }
	}
}

