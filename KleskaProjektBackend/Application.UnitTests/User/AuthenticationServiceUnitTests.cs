using KleskaProject.Application.Services;
using KleskaProject.Domain.UserAggregate;
using KleskaProjekt.Domain.Common.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace KleskaProject.Application.Tests.Commands;

public class AuthenticationServiceUnitTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
    private readonly Mock<IPhoneNumberRepository> _phoneNumberRepository = new Mock<IPhoneNumberRepository>();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
    private readonly Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
    private readonly AuthenticationService _authenticationService;

    public AuthenticationServiceUnitTests()
    {
        _configurationMock.Setup(c => c["JwtSettings:securityKey"]).Returns("KleskaProjektStringTokenThatIsLongEnoughForHmacSha512AlgorithmKeySize");
        _configurationMock.Setup(c => c["JwtSettings:validIssuer"]).Returns("validIssuer");
        _configurationMock.Setup(c => c["JwtSettings:validAudience"]).Returns("validAudience");
        _authenticationService = new AuthenticationService(_userRepositoryMock.Object, _phoneNumberRepository.Object, _unitOfWorkMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldReturnSuccess_WhenUserIsRegistered()
    {
        // Arrange
        var phoneNumber = new PhoneNumber("48", "500000000"); // Example phone number
        var userDto = new UserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "test@example.com",
            Password = "Secure#123",
            PhoneNumber = phoneNumber // Ensure phone number is included
        };

        _userRepositoryMock.Setup(r => r.ExistsByEmailAsync(userDto.Email)).ReturnsAsync(false);
        _userRepositoryMock.Setup(r => r.Add(It.IsAny<User>()));
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _authenticationService.RegisterUserAsync(userDto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.IsType<Guid>(result.Value);
        _userRepositoryMock.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldReturnFailure_WhenEmailAlreadyExists()
    {
        // Arrange
        var userDto = new UserDto { FirstName = "John", LastName = "Doe", Email = "test@example.com", Password = "Secure#123" };
        _userRepositoryMock.Setup(r => r.ExistsByEmailAsync(userDto.Email)).ReturnsAsync(true);

        // Act
        var result = await _authenticationService.RegisterUserAsync(userDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.Conflict, result.Error.StatusCode);
        Assert.Equal("Email already in use. Please use a different email.", result.Error.Details);
    }

    [Fact]
    public async Task LoginUserAsync_ShouldReturnSuccess_WhenCredentialsAreCorrect()
    {
        // Arrange
        var email = "test@example.com";
        var password = "Correct#123";
        var user = new User { Email = email, PasswordHash = BCrypt.Net.BCrypt.HashPassword(password) };
        _userRepositoryMock.Setup(r => r.GetByEmail(email)).ReturnsAsync(user);

        // Act
        var result = await _authenticationService.LoginUserAsync(email, password);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<TokenDto>(result.Value);
    }

    [Fact]
    public async Task LoginUserAsync_ShouldReturnFailure_WhenPasswordIsIncorrect()
    {
        // Arrange
        var email = "test@example.com";
        var incorrectPassword = "WrongPassword123!";
        var user = new User { Email = email, PasswordHash = BCrypt.Net.BCrypt.HashPassword("Correct#123") };
        _userRepositoryMock.Setup(r => r.GetByEmail(email)).ReturnsAsync(user);

        // Act
        var result = await _authenticationService.LoginUserAsync(email, incorrectPassword);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.BadRequest, result.Error.StatusCode);
        Assert.Equal("Wrong email or password", result.Error.Details);
    }

    [Fact]
    public void ValidateUser_ShouldReturnSuccess_WhenTokenIsValid()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        TokenDto validToken = _authenticationService.CreateToken(user, true);

        // Act
        var result = _authenticationService.ValidateUser(validToken);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void ValidateUser_ShouldReturnFailure_WhenTokenIsExpired()
    {
        // Arrange
        var keyToken = "KleskaProjektStringTokenThatIsLongEnoughForHmacSha512AlgorithmKeySize";
        var expiredToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(-1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyToken)),
                SecurityAlgorithms.HmacSha512Signature));
        var tokenString = new JwtSecurityTokenHandler().WriteToken(expiredToken);
        var tokenDto = new TokenDto(tokenString, "refreshToken");

        // Act
        var result = _authenticationService.ValidateUser(tokenDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.Unauthorized, result.Error.StatusCode);
        Assert.Equal("Token not valid, login again", result.Error.Details);
    }

    [Fact]
    public void CreateToken_ShouldReturnTokenDto()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };

        // Act
        var token = _authenticationService.CreateToken(user, true);

        // Assert
        Assert.NotNull(token);
        Assert.IsType<TokenDto>(token);
        Assert.False(string.IsNullOrEmpty(token.AccessToken));
        Assert.False(string.IsNullOrEmpty(token.RefreshToken));
    }

    [Fact]
    public async Task RefreshTokenAsync_ShouldReturnSuccess_WhenTokenIsValid()
    {
        // Arrange
        var user = new User { Email = "test@example.com", RefreshToken = "validRefreshToken", RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1) };
        var tokenDto = new TokenDto("expiredAccessToken", "validRefreshToken");
        _userRepositoryMock.Setup(r => r.GetByEmail(user.Email)).ReturnsAsync(user);

        // Act
        var result = await _authenticationService.RefreshTokenAsync(tokenDto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.IsType<TokenDto>(result.Value);
    }

    [Fact]
    public async Task RefreshTokenAsync_ShouldReturnFailure_WhenTokenIsInvalid()
    {
        // Arrange
        var user = new User { Email = "test@example.com", RefreshToken = "validRefreshToken", RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1) };
        var tokenDto = new TokenDto("expiredAccessToken", "invalidRefreshToken");
        _userRepositoryMock.Setup(r => r.GetByEmail(user.Email)).ReturnsAsync(user);

        // Act
        var result = await _authenticationService.RefreshTokenAsync(tokenDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.Unauthorized, result.Error.StatusCode);
        Assert.Equal("Invalid token", result.Error.Details);
    }
}
