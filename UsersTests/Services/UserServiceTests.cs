using Moq;
using Users.Models.Data;
using Users.Models.Repository;
using Xunit;

namespace Users.Services.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsCreatedUser()
        {
            // Arrange
            var user = new User { Name = "John", Surname = "Doe" };

            _userRepositoryMock.Setup(repo => repo.CreateUserAsync(user))
                .ReturnsAsync(user);

            // Act
            var createdUser = await _userService.CreateUserAsync(user);

            // Assert
            Assert.Equal(user, createdUser);
            _userRepositoryMock.Verify(repo => repo.CreateUserAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnsTrue_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };

            _userRepositoryMock.Setup(repo => repo.DeleteUserAsync(user))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteUserAsync(user);

            // Assert
            Assert.True(result);
            _userRepositoryMock.Verify(repo => repo.DeleteUserAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnsFalse_WhenUserIsNull()
        {
            // Act
            var result = await _userService.DeleteUserAsync(null);

            // Assert
            Assert.False(result);
            _userRepositoryMock.Verify(repo => repo.DeleteUserAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
        {
            new User { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" },
            new User { Id = Guid.NewGuid(), Name = "Jane", Surname = "Smith" }
        };

            _userRepositoryMock.Setup(repo => repo.GetAllUsers())
                .Returns(users);

            // Act
            var result = _userService.GetAllUsers();

            // Assert
            Assert.Equal(users, result);
            _userRepositoryMock.Verify(repo => repo.GetAllUsers(), Times.Once);
        }

        [Fact]
        public void GetUserById_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Name = "John", Surname = "Doe" };

            _userRepositoryMock.Setup(repo => repo.GetUserById(userId))
                .Returns(user);

            // Act
            var result = _userService.GetUserById(userId);

            // Assert
            Assert.Equal(user, result);
            _userRepositoryMock.Verify(repo => repo.GetUserById(userId), Times.Once);
        }

        [Fact]
        public void GetUserById_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userRepositoryMock.Setup(repo => repo.GetUserById(userId))
                .Returns((User)null);

            // Act
            var result = _userService.GetUserById(userId);

            // Assert
            Assert.Null(result);
            _userRepositoryMock.Verify(repo => repo.GetUserById(userId), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnsTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Name = "John Doe" };
            _userRepositoryMock.Setup(repo => repo.updateUserAsync(user)).ReturnsAsync(true);

            var userService = new UserService(_userRepositoryMock.Object);

            // Act
            var result = await userService.UpdateUserAsync(user);

            // Assert
            Assert.True(result);
            _userRepositoryMock.Verify(repo => repo.updateUserAsync(user), Times.Once);
        }
    }
}