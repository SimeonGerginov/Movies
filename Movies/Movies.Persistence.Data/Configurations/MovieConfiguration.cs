using System.Data.Entity.ModelConfiguration;
using Movies.Core.Models;

namespace Movies.Persistence.Data.Configurations
{
    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {
            this.HasRequired(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId);

            this.Property(m => m.Name).IsRequired();
            this.Property(m => m.Year).IsRequired();
            this.Property(m => m.RunningTime).IsRequired();
            this.Property(m => m.Description).IsRequired();
            this.Property(m => m.Rating).IsRequired();
        }
    }
}
