using System.Data.Entity.ModelConfiguration;
using Movies.Core.Models;

namespace Movies.Persistence.Data.Configurations
{
    public class MovieRoleConfiguration : EntityTypeConfiguration<MovieRole>
    {
        public MovieRoleConfiguration()
        {
            this.HasKey(mr => mr.Id);
            this.Property(mr => mr.PersonId).IsRequired();
            this.Property(mr => mr.MovieId).IsRequired();
        }
    }
}
