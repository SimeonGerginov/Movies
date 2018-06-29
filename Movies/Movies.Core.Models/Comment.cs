using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Entities;

namespace Movies.Core.Models
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [StringLength(GlobalConstants.MaxCommentLength, MinimumLength = GlobalConstants.MinCommentLength)]
        public string Content { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
