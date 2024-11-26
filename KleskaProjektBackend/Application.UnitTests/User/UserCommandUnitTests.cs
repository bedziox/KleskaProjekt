using KleskaProject.Application.Commands;
using KleskaProject.Application.EventHandlers;
using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;

namespace KleskaProject.Application.Tests.Commands;

public class LoginUserCommandHandlerTests
{
    private readonly Mock<IAuthenticationService> _mockAuthenticationService;
    private readonly LoginUserCommandHandler _handler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginUserCommandHandlerTests()
    {
        _mockAuthenticationService = new Mock<IAuthenticationService>();
        _handler = new LoginUserCommandHandler(_mockAuthenticationService.Object, _httpContextAccessor);
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenLoginIsSuccessful()
    {
        // Arrange
        var command = new LoginUserCommand("user@example.com", "Strong#123");
        var expectedToken = new TokenDto("accessToken123", "refreshToken123");
        _mockAuthenticationService.Setup(s => s.LoginUserAsync(command.email, command.password))
            .ReturnsAsync(Result<TokenDto>.Success(expectedToken));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(expectedToken.AccessToken, result.Value?.AccessToken);
        Assert.Equal(expectedToken.RefreshToken, result.Value?.RefreshToken);
        _mockAuthenticationService.Verify(s => s.LoginUserAsync(command.email, command.password), Times.Once); // Verify service call
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenLoginFails()
    {
        // Arrange
        var command = new LoginUserCommand("user@example.com", "WrongPassword");
        _mockAuthenticationService.Setup(s => s.LoginUserAsync(command.email, command.password))
            .ReturnsAsync(Result<TokenDto>.Failure(new Error(HttpStatusCode.BadRequest, "Wrong email or password")));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.BadRequest, result.Error.StatusCode);
        Assert.Equal("Wrong email or password", result.Error.Details);
        _mockAuthenticationService.Verify(s => s.LoginUserAsync(command.email, command.password), Times.Once); // Verify service call
    }
}

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IAuthenticationService> _mockUserService;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _mockUserService = new Mock<IAuthenticationService>();
        _handler = new RegisterUserCommandHandler(_mockUserService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserId_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var userDto = new UserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Password = "Strong#123"
        };
        var command = new RegisterUserCommand(userDto);
        var expectedUserId = Guid.NewGuid();
        _mockUserService.Setup(s => s.RegisterUserAsync(userDto))
            .ReturnsAsync(Result<Guid>.Success(expectedUserId));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedUserId, result.Value);
        _mockUserService.Verify(s => s.RegisterUserAsync(userDto), Times.Once); // Verify service call
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenRegistrationFails()
    {
        // Arrange
        var userDto = new UserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Password = "WeakPassword"
        };
        var command = new RegisterUserCommand(userDto);
        _mockUserService.Setup(s => s.RegisterUserAsync(userDto))
            .ReturnsAsync(Result<Guid>.Failure(new Error(HttpStatusCode.BadRequest, "Registration failed")));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.BadRequest, result.Error.StatusCode);
        Assert.Equal("Registration failed", result.Error.Details);
        _mockUserService.Verify(s => s.RegisterUserAsync(userDto), Times.Once); // Verify service call
    }
}


