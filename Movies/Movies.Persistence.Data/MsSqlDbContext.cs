﻿using System;
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

        public virtual IDbSet<Movie> Movies { get; set; }

        public virtual IDbSet<Genre> Genres { get; set; }

        public virtual IDbSet<Person> People { get; set; }

        public virtual IDbSet<MovieRole> MovieRoles { get; set; }

        public virtual IDbSet<MovieRating> MovieRatings { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Configurations.Add(new MovieConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new MovieRoleConfiguration());
            modelBuilder.Configurations.Add(new MovieRatingConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());

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
