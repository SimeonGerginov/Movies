using Movies.Core.Models.Enums;

namespace Movies.Core.Models
{
    public class MovieRole
    {
        public int Id { get; set; }
        
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public Role Role { get; set; }
        
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
