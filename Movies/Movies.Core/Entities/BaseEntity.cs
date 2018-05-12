using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Movies.Core.Contracts;

namespace Movies.Core.Entities
{
    public class BaseEntity : IBaseEntity, IAuditable, IDeletable
    {
        [Key]
        public int Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
