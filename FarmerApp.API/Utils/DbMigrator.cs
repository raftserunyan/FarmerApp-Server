using FarmerApp.Data.DAO;
using FarmerApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.API.Utils
{
    public static class DbMigrator
    {
        public static async Task MigrateDbAndPopulate(WebApplication app, WebApplicationBuilder builder)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<FarmerDbContext>();
                await context.Database.MigrateAsync();

                if (!(await context.Set<UserEntity>().AnyAsync(x => x.Name == "Admin")))
                {
                    var userSeed = new UserEntity
                    {
                        Name = "Admin",
                        Email = Environment.GetEnvironmentVariable("SEED_USERNAME", EnvironmentVariableTarget.Process) ?? builder.Configuration["SeedUsername"],
                        Password = Environment.GetEnvironmentVariable("SEED_PASS", EnvironmentVariableTarget.Process) ?? builder.Configuration["SeedPass"]
                    };

                    await context.AddAsync(userSeed);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
