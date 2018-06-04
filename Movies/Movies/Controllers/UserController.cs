using System.Web.Mvc;
using AutoMapper;
using Bytes2you.Validation;

using Movies.Services.Contracts;
using Movies.ViewModels;

namespace Movies.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IFileConverter fileConverter;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IFileConverter fileConverter, IMapper mapper)
        {
            Guard.WhenArgument(userService, "User Service").IsNull().Throw();
            Guard.WhenArgument(fileConverter, "File Converter").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.userService = userService;
            this.fileConverter = fileConverter;
            this.mapper = mapper;
        }

        public ActionResult UserProfile(string username)
        {
            var user = this.userService.GetUser(username);
            var userDetails = this.mapper.Map<UserDetailsViewModel>(user);

            return this.View(userDetails);
        }

        public FileContentResult UserPhoto(string username)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = this.userService.GetUser(username);
                var userProfilePicture = user.ProfilePicture;

                if (userProfilePicture == null)
                {
                    var defaultImage = this.fileConverter.GetDefaultPicture();
                    var file = this.File(defaultImage, "image/png");

                    return file;
                }
                else
                {
                    var file = this.File(userProfilePicture, "image/jpeg");
                    
                    return file;
                }
            }
            else
            {
                var defaultImage = this.fileConverter.GetDefaultPicture();
                var file = this.File(defaultImage, "image/png");

                return file;
            }
        }
    }
}
