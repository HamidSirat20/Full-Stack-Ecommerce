using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using backend.Domain.src.Entities;

namespace backend.WebApi.src.Database;

public class TimestampInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var addedEntries = eventData.Context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added);

            foreach (var entry in addedEntries)
            {
                if (entry.Entity is Timestamp hasTimestamp)
                {
                    hasTimestamp.CreatedAt = new DateTime();
                    hasTimestamp.ModifiedAt = new DateTime();
                }
            }

            var modifiedEntries = eventData.Context.ChangeTracker.Entries()
           .Where(e => e.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is Timestamp hasTimestamp)
                {
                    hasTimestamp.ModifiedAt = new DateTime();
                }
            }
            return base.SavingChanges(eventData, result);
        }
    }
