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
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            Guard.WhenArgument(userService, "User Service").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.userService = userService;
            this.mapper = mapper;
        }

        public ActionResult UserProfile(string username)
        {
            var user = this.userService.GetUser(username);
            var userDetails = this.mapper.Map<UserDetailsViewModel>(user);

            return this.View(userDetails);
        }
    }
}
