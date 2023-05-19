using Microsoft.EntityFrameworkCore;
using Users.Models.Data;

namespace Users.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly UsersContext _context;
        public UserRepository(UsersContext context) => _context = context;

        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            if (user is null)
            {
                return false;
            }
            _context.Set<User>().Remove(user);
            await _context.SaveChangesAsync(true);
            return true;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public async Task<bool> updateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
