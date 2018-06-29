using System.Data.Entity.ModelConfiguration;
using Movies.Core.Models;

namespace Movies.Persistence.Data.Configurations
{
    public class MovieRatingConfiguration : EntityTypeConfiguration<MovieRating>
    {
        public MovieRatingConfiguration()
        {
            this.Property(mr => mr.UserId).IsRequired();
            this.Property(mr => mr.MovieId).IsRequired();
        }
    }
}
