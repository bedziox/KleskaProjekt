using KleskaProject.Domain.UserAggregate;

namespace KleskaProject.Domain.Tests.UserAggregate;
public class PhoneNumberTests
{
    [Fact]
    public void PhoneNumber_ShouldInitializeCorrectly_WithValidInput()
    {
        // Arrange
        var countryCode = "48";
        var number = "500000000";

        // Act
        var phoneNumber = new PhoneNumber(countryCode, number);

        // Assert
        Assert.Equal(countryCode, phoneNumber.CountryCode);
        Assert.Equal(number, phoneNumber.Number);
    }

    [Theory]
    [InlineData(null, "500000000")]
    [InlineData("48", null)]
    public void PhoneNumber_ShouldThrowArgumentException_WhenRequiredFieldsAreNullOrEmpty(
        string countryCode, string number)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new PhoneNumber(countryCode, number));
    }

    [Theory]
    [InlineData("48", "12345")]      // Too short
    [InlineData("48", "abcdefghij")] // Not a number
    public void PhoneNumber_ShouldThrowArgumentException_WhenPhoneNumberIsInvalid(
        string countryCode, string number)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new PhoneNumber(countryCode, number));
        Assert.Equal("Invalid phone number.", exception.Message);
    }

    [Fact]
    public void ToString_ShouldReturnFormattedPhoneNumber()
    {
        // Arrange
        var phoneNumber = new PhoneNumber("48", "500000000");

        // Act
        var result = phoneNumber.ToString();

        // Assert
        Assert.Equal("+48 500000000", result);
    }
}