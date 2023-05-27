using Microsoft.EntityFrameworkCore;
using Users.Models.Data;
using Xunit;

namespace Users.Models.Repository.Tests
{
    public class UserRepositoryTests
    {
        private readonly UsersContext _context;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            // Crea un contexto en memoria para las pruebas
            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new UsersContext(options);

            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsCreatedUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John" };

            // Act
            var createdUser = await _userRepository.CreateUserAsync(user);

            // Assert
            Assert.Equal(user, createdUser);
            Assert.Contains(user, _context.Users);
        }

        [Fact]
        public async Task DeleteUserAsync_WithNullUser_ReturnsFalse()
        {1
            // Act
            var result = await _userRepository.DeleteUserAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserAsync_WithValidUser_ReturnsTrueAndDeletesUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John" };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = await _userRepository.DeleteUserAsync(user);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(user, _context.Users);
        }

        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var users = new[]
            {
            new User { Id = 1, Name = "John" },
            new User { Id = 2, Name = "Jane" }
        };
            _context.Users.AddRange(users);
            _context.SaveChanges();

            // Act
            var result = _userRepository.GetAllUsers();

            // Assert
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetUserById_WithValidId_ReturnsUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John" };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = _userRepository.GetUserById(1);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public void GetUserById_WithInvalidId_ReturnsNull()
        {
            // Act
            var result = _userRepository.GetUserById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdatesUserInContext()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John" };
            _context.Users.Add(user);
            _context.SaveChanges();

            var modifiedUser = new User { Id = 1, Name = "Updated John" };

            // Act
            await _userRepository.updateUserAsync(modifiedUser);

            // Assert
            var updatedUser = _context.Users.Find(1);
            Assert.Equal(modifiedUser.Name, updatedUser.Name);
        }
    }

}