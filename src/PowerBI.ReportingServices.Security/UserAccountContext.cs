using System;
using System.Data.Entity;
using System.Linq;

using PowerBI.ReportingServices.Security.Entities;

namespace PowerBI.ReportingServices.Security
{
    public sealed class UserAccountContext : DbContext
    {
        public UserAccountContext() : base("name=cas.useraccounts")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UserAccountContext, Migrations.Configuration>());
        }

        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
           .Entries()
           .Where(e => e.Entity is IBaseEntity && (
                   e.State == EntityState.Added
                   || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((IBaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((IBaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
