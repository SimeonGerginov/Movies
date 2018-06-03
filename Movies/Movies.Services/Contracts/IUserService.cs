using Movies.Core.Models;

namespace Movies.Services.Contracts
{
    public interface IUserService
    {
        User GetUser(string username);
    }
}
