using WebAPIAutoLink.DTO;
using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool UserExists(int userId);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
