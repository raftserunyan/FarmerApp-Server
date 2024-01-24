namespace FarmerApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Investor> Investors { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Treatment> Treatments { get; set; }
    }
}
