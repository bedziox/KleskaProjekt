using KleskaProject.Domain.UserAggregate;

namespace KleskaProject.Domain.Tests.UserAggregate;
public class UserTests
{
    [Theory]
    [InlineData("user@example.com", true)]
    [InlineData("invalid-email", false)]
    public void ValidateEmail_ShouldReturnExpectedResult(string email, bool expected)
    {
        // Act
        var result = UserValidation.ValidateEmail(email);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Password123!", true)]
    [InlineData("weakpass", false)]
    public void ValidatePassword_ShouldReturnExpectedResult(string password, bool expected)
    {
        // Act
        var result = UserValidation.ValidatePassword(password);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void BuildUser_ShouldReturnUser_WhenDataIsValid()
    {
        var number = new PhoneNumber("48", "500000000");
        // Arrange
        var userDto = new UserDto()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "user@example.com",
            Password = "Strong#123",
            PhoneNumber = number
        };

        // Act
        var user = UserBuilder.BuildUser(userDto);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(userDto.FirstName, user.FirstName);
        Assert.Equal(userDto.LastName, user.LastName);
        Assert.Equal(userDto.Email, user.Email);
        Assert.True(BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash));
    }

    [Fact]
    public void Deactivate_ShouldSetUserDetailsToInactive()
    {
        // Arrange
        var phoneNumber = new PhoneNumber("48", "500000000");
        var userDetails = new UserDetails(phoneNumber) { IsActive = true };

        var user = new User
        {
            UserDetails = userDetails
        };

        // Act
        user.Deactivate();

        // Assert
        Assert.False(user.UserDetails.IsActive);
    }

    [Fact]
    public void UserDetails_ShouldInitializeCorrectly_WithValidInput()
    {
        // Arrange
        var phoneNumber = new PhoneNumber("48", "500000000");

        // Act
        var userDetails = new UserDetails(phoneNumber);

        // Assert
        Assert.NotNull(userDetails);
        Assert.Equal(phoneNumber, userDetails.phoneNumber);
        Assert.True(userDetails.IsActive);
        Assert.True((DateTime.UtcNow - userDetails.CreationTime).TotalSeconds < 1);
    }

    [Theory]
    [InlineData("0", "")]
    [InlineData("", "500000000")]
    public void UserDetails_ShouldThrowException_WhenPhoneNumberIsNull(string code, string number)
    {

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new UserDetails(new PhoneNumber(code, number)));
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var phoneNumber = new PhoneNumber("48", "500000000");
        var userDetails = new UserDetails(phoneNumber);

        // Act
        userDetails.Deactivate();

        // Assert
        Assert.False(userDetails.IsActive);
    }
}