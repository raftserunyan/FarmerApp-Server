using FarmerApp.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FarmerApp.Data.Utils
{
    public class BaseEntityInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            SetCreatedAndUpdatedDates(eventData);

            return base.SavingChangesAsync(eventData, result);
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            SetCreatedAndUpdatedDates(eventData);

            return base.SavingChanges(eventData, result);
        }

        private void SetCreatedAndUpdatedDates(DbContextEventData eventData)
        {
            var entries = eventData.Context.ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Property(x => x.LastUpdatedDate).CurrentValue = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedDate).CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}
