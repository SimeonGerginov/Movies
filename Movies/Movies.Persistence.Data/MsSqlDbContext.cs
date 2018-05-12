using System;
using System.Data.Entity;
using System.Linq;

using Microsoft.AspNet.Identity.EntityFramework;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Persistence.Data.Configurations;

namespace Movies.Persistence.Data
{
    public class MsSqlDbContext : IdentityDbContext<User>
    {
        public MsSqlDbContext()
            : base("LocalConnection", throwIfV1Schema: false)
        {
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BaseEntityConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAuditInfoRules()
        {
            var entries = this.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditable && 
                ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in entries)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime?))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
