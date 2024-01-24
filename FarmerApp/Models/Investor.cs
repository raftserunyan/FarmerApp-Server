namespace FarmerApp.Models
{
	public class Investor
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Investment> Investments { get; set; }
    }
}

