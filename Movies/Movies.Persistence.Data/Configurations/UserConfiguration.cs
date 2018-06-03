using System.Data.Entity.ModelConfiguration;
using Movies.Core.Models;

namespace Movies.Persistence.Data.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.Property(u => u.FirstName).IsOptional();
            this.Property(u => u.LastName).IsOptional();
            this.Property(u => u.Gender).IsOptional();
            this.Property(u => u.ProfilePicture).IsOptional();
        }
    }
}
