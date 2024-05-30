using FarmerApp.Data.Entities;
using FarmerApp.Data.Extensions;
using FarmerApp.Data.Utils;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Data.DAO
{
    public class FarmerDbContext : DbContext
    {
        public FarmerDbContext(DbContextOptions<FarmerDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SaleEntity> Sales { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ExpenseEntity> Expenses { get; set; }
        public DbSet<InvestorEntity> Investors { get; set; }
        public DbSet<InvestmentEntity> Investments { get; set; }
        public DbSet<TreatmentEntity> Treatments { get; set; }
        public DbSet<TargetEntity> Targets { get; set; }
        public DbSet<MeasurementUnitEntity> MeasurementUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new BaseEntityInterceptor());

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyGlobalFilter("IsDeleted", false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
