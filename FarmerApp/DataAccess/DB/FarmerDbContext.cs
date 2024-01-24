using FarmerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.DataAccess.DB
{
    public class FarmerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Investor> Investors { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Treatment> Treatments { get; set; }

        public FarmerDbContext(DbContextOptions<FarmerDbContext> options) : base(options)
        {

        }
    }
}