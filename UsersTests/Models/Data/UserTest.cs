using Microsoft.AspNetCore.Routing;
using System.ComponentModel.DataAnnotations;
using Users.Models.Data;
using Xunit;

namespace UsersTests.Models.Data
{
    public class UserTests
    {
        [Fact]
        public void User_Id_HasDefaultValue()
        {
            // Arrange
            var user = new User();

            // Assert
            Assert.Equal(Guid.Empty, user.Id);
        }

        [Fact]
        public void User_Name_IsRequired()
        {
            // Arrange
            var user = new User();

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Name"));
        }

        [Fact]
        public void User_Surname_IsRequired()
        {
            // Arrange
            var user = new User();

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Surname"));
        }

        [Fact]
        public void Address_AllowNull_ShouldAcceptNull()
        {
            // Arrange
            var instance = new User();

            // Act
            instance.Address = null;

            // Assert
            Assert.Null(instance.Address);
        }

        [Fact]
        public void Address_AllowNull_ShouldAcceptNonNull()
        {
            // Arrange
            var instance = new User();
            var address = "123 Main St";

            // Act
            instance.Address = address;

            // Assert
            Assert.Equal(address, instance.Address);
        }

        [Fact]
        public void Email_AllowNull_ShouldAcceptNull()
        {
            // Arrange
            var user = new User();

            // Act
            user.Email = null;

            // Assert
            Assert.Null(user.Email);
        }

        [Fact]
        public void Email_AllowNull_ShouldAcceptNonNull()
        {
            // Arrange
            var user = new User();
            var email = "test@example.com";

            // Act
            user.Email = email;

            // Assert
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void Phone_AllowNull_ShouldAcceptNull()
        {
            // Arrange
            var user = new User();

            // Act
            user.Phone = null;

            // Assert
            Assert.Null(user.Phone);
        }

        [Fact]
        public void Phone_AllowNull_ShouldAcceptNonNull()
        {
            // Arrange
            var user = new User();
            var phone = "1234567890";

            // Act
            user.Phone = phone;

            // Assert
            Assert.Equal(phone, user.Phone);
        }

        // Helper method to validate the model using data annotations
        private List<ValidationResult> ValidateModel(User user)
        {
            var validationContext = new ValidationContext(user, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, validationResults, true);
            return validationResults;
        }
    }

}
