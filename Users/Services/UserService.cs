using Users.Models.Data;
using Users.Models.Repository;

namespace Users.Services
{
    public class UserService : IUserService
    {
        protected readonly UserRepository _repository;
        public UserService(UserRepository userRepository) => _repository = userRepository;
        public Task<User> CreateUserAsync(User user)
        {
            return _repository.CreateUserAsync(user);
        }

        public Task<bool> DeleteUserAsync(User user)
        {
            return _repository.DeleteUserAsync(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            return _repository.updateUserAsync(user);
        }
    }
}
