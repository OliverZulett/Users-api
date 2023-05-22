using Users.Models.Data;

namespace Users.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        Task<bool> UpdateUserAsync(User user);
    }
}
