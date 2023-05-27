using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Users.Models.Data.Tests
{
    public class UsersContextTests : IClassFixture<UsersContextFixture>
    {
        private readonly UsersContextFixture _fixture;

        public UsersContextTests(UsersContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void UsersContext_WithValidOptions_CreatesDbContext()
        {
            // Arrange
            using (var context = _fixture.CreateUsersContext())
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
            Assert.Throws<ArgumentNullException>(() => _fixture.CreateUsersContextWithNullOptions());
        }

        [Fact]
        public void UsersContext_UsersDbSet_IsNotNull()
        {
            // Arrange
            using (var context = _fixture.CreateUsersContext())
            {
                // Assert
                Assert.NotNull(context.Users); // Users DbSet should not be null
                Assert.IsAssignableFrom<DbSet<User>>(context.Users); // Users DbSet should be of type DbSet<User>
            }
        }
    }

    public class UsersContextFixture
    {
        public DbContextOptions<UsersContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<UsersContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        public UsersContext CreateUsersContext()
        {
            var options = CreateOptions();
            return new UsersContext(options);
        }

        public UsersContext CreateUsersContextWithNullOptions()
        {
            return new UsersContext(null);
        }
    }
}