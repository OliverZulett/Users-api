using System.Text.Json;

namespace Users.Models.Data
{
    public class Seeder
    {
        private readonly UsersContext _context;

        public Seeder(UsersContext context) {
            _context = context;
            this.loadUsers();
        }

        private void loadUsers()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var jsonFilePath = Path.Combine(currentDirectory, "Models", "Data", "users.json");
            var json = File.ReadAllText(jsonFilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(json);
            if (!_context.Users.Any())
            {
                _context.Users.AddRange(users);
                _context.SaveChanges();
            }
        }
    }
}
