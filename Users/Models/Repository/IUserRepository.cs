using Users.Models.Data;

namespace Users.Models.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        Task<bool> updateUserAsync(User user);
    }
}
