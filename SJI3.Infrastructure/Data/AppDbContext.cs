using Microsoft.EntityFrameworkCore;
using SJI3.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SJI3.Core.Common.Domain;

namespace SJI3.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<TaskUnit> TaskUnits { get; set; }
    public DbSet<TaskUnitType> TaskUnitTypes { get; set; }
    public DbSet<TaskUnitStatus> TaskUnitStatuses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entity in ChangeTracker.Entries<IAudit>())
        {
            if (entity.State is EntityState.Added or EntityState.Modified)
            {
                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.SetModifiedOn(DateTime.UtcNow);
                }

                if (entity.State == EntityState.Added)
                {
                    entity.Entity.SetCreatedOn(DateTime.UtcNow);
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
            .HasKey(a => a.Id);

        modelBuilder
            .Entity<ApplicationUser>()
            .Property<List<Guid>>("TaskUnitsPrivate")
            .HasConversion(
                ids => JsonSerializer.Serialize(ids.Select(id => id), new JsonSerializerOptions()),
                idValue => JsonSerializer.Deserialize<List<Guid>>(idValue, new JsonSerializerOptions()).Select(id => id).Distinct().ToList(),
                new ValueComparer<List<Guid>>(
                    (roleId1, roleId2) => roleId1.Equals(roleId2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

        modelBuilder
            .Entity<ApplicationUser>()
            .Ignore(a => a.TaskUnits);

        modelBuilder.Entity<TaskUnit>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(s => s.ApplicationUserId);
    }
}