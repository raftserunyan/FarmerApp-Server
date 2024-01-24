namespace FarmerApp.Models
{
	public class Sale
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

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}

