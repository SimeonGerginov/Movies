using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.ViewModels.UserViewModels
{
    public class UserDetailsViewModel : IMap<User>
    {
        [Required]
        [StringLength(GlobalConstants.MaxUserNameLength, MinimumLength = GlobalConstants.MinUserNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxUserNameLength, MinimumLength = GlobalConstants.MinUserNameLength)]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}
