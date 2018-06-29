using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.ViewModels.UserViewModels
{
    public class UserDetailsViewModel : IMap<User>
    {
        [StringLength(GlobalConstants.MaxUserNameLength, MinimumLength = GlobalConstants.MinUserNameLength)]
        public string FirstName { get; set; }

        [StringLength(GlobalConstants.MaxUserNameLength, MinimumLength = GlobalConstants.MinUserNameLength)]
        public string LastName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}
