using System.Data.Entity.ModelConfiguration;
using Movies.Core.Models;

namespace Movies.Persistence.Data.Configurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            this.HasMany(p => p.Movies)
                .WithMany(m => m.People)
                .Map(cs =>
                {
                    cs.MapLeftKey("PersonId");
                    cs.MapRightKey("MovieId");
                    cs.ToTable("PersonMovie");
                });

            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.Nationality).IsRequired();
            this.Property(p => p.Role).IsRequired();
            this.Property(p => p.DateOfBirth).IsRequired();
            this.Property(p => p.Age).IsRequired();
            this.Property(p => p.Gender).IsRequired();
            this.Property(p => p.Picture).IsOptional();
        }
    }
}
