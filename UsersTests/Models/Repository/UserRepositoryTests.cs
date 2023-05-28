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

            // Limpiar y crear la base de datos en memoria antes de cada caso de prueba
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsCreatedUser()
        {
            // Arrange
            var user = new User { Name = "name", Surname = "surname", Address = "address", Email = "email", Phone = "phone" };

            // Act
            var createdUser = await _userRepository.CreateUserAsync(user);

            // Assert
            Assert.Equal(user, createdUser);
            Assert.Contains(user, _context.Users);
        }

        [Fact]
        public async Task DeleteUserAsync_WithNullUser_ReturnsFalse()
        {
            // Act
            var result = await _userRepository.DeleteUserAsync(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserAsync_WithValidUser_ReturnsTrueAndDeletesUser()
        {
            // Arrange
            var user = new User { Name = "name", Surname = "surname", Address = "address", Email = "email", Phone = "phone" };
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
                new User { Name = "name1", Surname = "surname1", Address = "address1", Email = "email1", Phone = "phone1" },
                new User { Name = "name2", Surname = "surname2", Address = "address2", Email = "email2", Phone = "phone2" }
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
            var user = new User { Name = "name1", Surname = "surname1", Address = "address1", Email = "email1", Phone = "phone1" };
            var addedUser = _userRepository.CreateUserAsync(user).GetAwaiter().GetResult(); // Guardar el usuario y obtener el usuario con el ID autogenerado
            var userId = addedUser.Id; // Obtener el ID autogenerado del usuario guardado

            // Act
            var result = _userRepository.GetUserById(userId); // Utilizar el ID autogenerado para buscar el usuario

            // Assert
            Assert.Equal(addedUser, result);
        }

        [Fact]
        public void GetUserById_WithInvalidId_ReturnsNull()
        {
            // Arrange
            var id = new Guid("97199283-3b08-400f-8dc0-79fcd7ba8a64");

            // Act
            var result = _userRepository.GetUserById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdatesUserInContext()
        {
            // Arrange
            var user = new User { Name = "name1", Surname = "surname1", Address = "address1", Email = "email1", Phone = "phone1" };
            var addedUser = await _userRepository.CreateUserAsync(user); // Guardar el usuario y obtener el usuario con el ID autogenerado
            var userId = addedUser.Id; // Obtener el ID autogenerado del usuario guardado

            // Obtener el usuario existente del contexto
            var existingUser = _context.Users.Find(userId);
            existingUser.Name = "name2";
            existingUser.Surname = "surname2";
            existingUser.Address = "address2";
            existingUser.Email = "email2";
            existingUser.Phone = "phone2";

            // Act
            await _userRepository.updateUserAsync(existingUser);

            // Assert
            var updatedUser = _userRepository.GetUserById(userId);
            Assert.Equal(existingUser.Name, updatedUser.Name);
        }
    }

}