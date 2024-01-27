namespace FarmerApp.Models
{
    public class Target
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public List<Expense> Expenses { get; set; }
    }
}
