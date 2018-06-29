using System.Linq;
using Bytes2you.Validation;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services.Contracts;

namespace Movies.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            Guard.WhenArgument(userRepository, "User Repository").IsNull().Throw();

            this.userRepository = userRepository;
        }

        public User GetUser(string username)
        {
            var user = this.userRepository
                .GetAllFiltered(u => u.UserName == username)
                .FirstOrDefault();

            Guard.WhenArgument(user, "User").IsNull().Throw();

            return user;
        }

        public void EditUser(string userId, User userModel)
        {
            Guard.WhenArgument(userId, "User Id").IsNull().Throw();

            var user = this.userRepository
                .GetAllFiltered(u => u.Id == userId)
                .FirstOrDefault();

            if (user != null)
            {
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.Email = userModel.Email;
                user.Gender = userModel.Gender;
            }

            if (userModel.ProfilePicture != null)
            {
                user.ProfilePicture = userModel.ProfilePicture;
            }

            this.userRepository.Update(user);
        }
    }
}
