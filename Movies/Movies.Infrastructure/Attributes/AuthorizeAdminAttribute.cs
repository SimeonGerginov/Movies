using System.Web.Mvc;
using Movies.Common;

namespace Movies.Infrastructure.Attributes
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        public AuthorizeAdminAttribute()
        {
            this.Roles = GlobalConstants.AdminRole;
        }
    }
}
