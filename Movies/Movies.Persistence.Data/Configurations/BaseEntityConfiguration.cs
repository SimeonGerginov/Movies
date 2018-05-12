using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using Movies.Core.Entities;

namespace Movies.Persistence.Data.Configurations
{
    public class BaseEntityConfiguration : EntityTypeConfiguration<BaseEntity>
    {
        public BaseEntityConfiguration()
        {
            this.HasKey(e => e.Id);
            this.Property(e => e.CreatedOn).HasColumnType("datetime2");
            this.Property(e => e.ModifiedOn).HasColumnType("datetime2");
            this.Property(e => e.DeletedOn).HasColumnType("datetime2");

            var indexAttribute = new IndexAttribute();
            var indexAnnotation = new IndexAnnotation(indexAttribute);

            this.Property(e => e.IsDeleted)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, indexAnnotation);
        }
    }
}
