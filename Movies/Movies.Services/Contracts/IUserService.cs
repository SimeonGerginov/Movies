using Movies.Core.Models;

namespace Movies.Services.Contracts
{
    public interface IUserService
    {
        User GetUser(string username);

        void EditUser(string userId, User userModel);
    }
}
