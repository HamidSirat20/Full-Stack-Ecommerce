using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using backend.Domain.src.Entities;

namespace backend.WebApi.src.Database;

public class TimestampInterceptor : SaveChangesInterceptor
{
   public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var addedEntries = eventData.Context!.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            foreach (var trackEntry in addedEntries)
            {
                if(trackEntry.Entity is BaseEntity entity)
                {
                    entity.CreatedAt = DateTime.Now.Date;
                    entity.ModifiedAt = DateTime.Now.Date;
                }
            }

            var updatedEntries = eventData.Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            foreach (var trackEntry in updatedEntries)
            {
                if (trackEntry.Entity is BaseEntity entity)
                {
                    entity.ModifiedAt = DateTime.Now.Date;
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
}
