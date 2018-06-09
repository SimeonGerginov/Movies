using System.Data.Entity.ModelConfiguration;
using Movies.Core.Models;

namespace Movies.Persistence.Data.Configurations
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            this.Property(c => c.UserId).IsRequired();
            this.Property(c => c.MovieId).IsRequired();
            this.Property(c => c.Content).IsRequired();
        }
    }
}
