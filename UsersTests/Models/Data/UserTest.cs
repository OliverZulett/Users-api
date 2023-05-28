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

        [Theory]
        [InlineData(null)]
        //[InlineData("")]
        //[InlineData("   ")]
        public void User_Address_AllowsNullAndEmptyValues(string address)
        {
            // Arrange
            var user = new User { Address = address };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            Assert.Equal(0, validationResults.Count);
        }

        //[Theory]
        //[InlineData(null)]
        //[InlineData("")]
        //[InlineData("   ")]
        //public void User_Email_AllowsNullAndEmptyValues(string email)
        //{
        //    // Arrange
        //    var user = new User { Email = email };

        //    // Act
        //    var validationResults = ValidateModel(user);

        //    // Assert
        //    Assert.Empty(validationResults);
        //}

        //[Theory]
        //[InlineData(null)]
        //[InlineData("")]
        //[InlineData("   ")]
        //public void User_Phone_AllowsNullAndEmptyValues(string phone)
        //{
        //    // Arrange
        //    var user = new User { Phone = phone };

        //    // Act
        //    var validationResults = ValidateModel(user);

        //    // Assert
        //    Assert.Empty(validationResults);
        //}

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
