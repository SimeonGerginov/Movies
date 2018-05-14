using System.Data.Entity.ModelConfiguration;
using Movies.Core.Models;

namespace Movies.Persistence.Data.Configurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            this.HasKey(g => g.Id);
            this.Property(g => g.Name).IsRequired();
        }
    }
}
