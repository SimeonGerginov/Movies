using System;

using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.Areas.Admin.Models
{
    public class GridGenreViewModel : IMap<Genre>
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
        
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}