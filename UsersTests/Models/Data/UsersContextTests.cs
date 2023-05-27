using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Users.Models.Data.Tests
{
    public class UsersContextTests
    {
        [Fact]
        public void UsersContext_WithValidOptions_CreatesDbContext()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            using (var context = new UsersContext(options))
            {
                // Assert
                Assert.NotNull(context); // Context should not be null
                Assert.IsType<UsersContext>(context); // Context should be of type UsersContext
            }
        }

        [Fact]
        public void UsersContext_WithNullOptions_ThrowsArgumentNullException()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new UsersContext(null));
        }

        [Fact]
        public void UsersContext_UsersDbSet_IsNotNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            using (var context = new UsersContext(options))
            {
                // Assert
                Assert.NotNull(context.Users); // Users DbSet should not be null
                _ = Assert.IsAssignableFrom<DbSet<User>>(context.Users); // Users DbSet should be of type DbSet<User>
            }
        }
    }
}