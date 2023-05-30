using Microsoft.AspNetCore.Mvc;
using Moq;
using Users.Models.Data;
using Users.Models.Repository;
using Users.Services;
using Xunit;

namespace Users.Controllers.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UsersController _userController;

        public UserControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userServiceMock = new Mock<IUserService>();

            _userController = new UsersController(_userRepositoryMock.Object, _userServiceMock.Object);
        }

        [Fact]
        public void GetUserAsync_ReturnsAllUsersFromService()
        {
            // Arrange
            var users = new List<User>
        {
            new User { Id = new Guid(), Name = "John Doe" },
            new User { Id = new Guid(), Name = "Jane Smith" }
        };

            _userServiceMock.Setup(service => service.GetAllUsers()).Returns(users);

            // Act
            var result = _userController.GetUserAsync();

            // Assert
            _userServiceMock.Verify(service => service.GetAllUsers(), Times.Once);
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var user = new User { Id = new Guid(), Name = "John Doe" };

            _userServiceMock.Setup(service => service.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userController.CreateUserAsync(user);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(UsersController.GetUserById), createdAtActionResult.ActionName);
            Assert.Equal(user.Id, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(user, createdAtActionResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ReturnsNoContentResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Name = "John Doe" };

            _userServiceMock.Setup(service => service.UpdateUserAsync(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await _userController.UpdateUser(userId, user);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsBadRequestResult_WhenIdDoesNotMatch()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = Guid.NewGuid(), Name = "John Doe" };

            // Act
            var result = await _userController.UpdateUser(userId, user);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContentResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Name = "John Doe" };

            _userServiceMock.Setup(service => service.GetUserById(userId)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.DeleteUserAsync(user)).ReturnsAsync(true);

            // Act
            var result = await _userController.DeleteUser(userId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFoundResult_WhenUserIsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userServiceMock.Setup(service => service.GetUserById(userId)).Returns((User)null);

            // Act
            var result = await _userController.DeleteUser(userId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}