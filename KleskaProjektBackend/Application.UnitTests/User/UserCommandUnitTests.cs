using KleskaProject.Application.Commands;
using KleskaProject.Application.Commands.User;
using KleskaProject.Application.EventHandlers;
using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using Moq;
using System.Net;

namespace KleskaProject.Application.Tests.Commands;

public class LoginUserCommandHandlerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserCommandHandlerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _handler = new LoginUserCommandHandler(_mockUserService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenLoginIsSuccessful()
    {
        // Arrange
        var command = new LoginUserCommand("user@example.com", "Strong#123");
        var expectedToken = "token123";
        _mockUserService.Setup(s => s.LoginUserAsync(command.email, command.password))
            .ReturnsAsync(Result<string>.Success(expectedToken));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedToken, result.Value);
        _mockUserService.Verify(s => s.LoginUserAsync(command.email, command.password), Times.Once); // Verify service call
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenLoginFails()
    {
        // Arrange
        var command = new LoginUserCommand("user@example.com", "WrongPassword");
        _mockUserService.Setup(s => s.LoginUserAsync(command.email, command.password))
            .ReturnsAsync(Result<string>.Failure(new Error(HttpStatusCode.BadRequest, "Wrong email or password")));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.BadRequest, result.Error.StatusCode);
        Assert.Equal("Wrong email or password", result.Error.Details);
        _mockUserService.Verify(s => s.LoginUserAsync(command.email, command.password), Times.Once); // Verify service call
    }
}

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _mockUserService = new Mock<IUserService>();
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

public class ValidateTokenCommandHandlerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly ValidateTokenCommandHandler _handler;

    public ValidateTokenCommandHandlerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _handler = new ValidateTokenCommandHandler(_mockUserService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenTokenIsValid()
    {
        // Arrange
        var token = "validToken";
        var command = new ValidateTokenCommand(token);
        _mockUserService.Setup(s => s.ValidateUser(token))
            .Returns(Result<string>.Success(token));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(token, result.Value);
        _mockUserService.Verify(s => s.ValidateUser(token), Times.Once); // Verify service call
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenTokenIsInvalid()
    {
        // Arrange
        var token = "invalidToken";
        var command = new ValidateTokenCommand(token);
        _mockUserService.Setup(s => s.ValidateUser(token))
            .Returns(Result<string>.Failure(new Error(HttpStatusCode.Unauthorized, "Token not valid")));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.Unauthorized, result.Error.StatusCode);
        Assert.Equal("Token not valid", result.Error.Details);
        _mockUserService.Verify(s => s.ValidateUser(token), Times.Once); // Verify service call
    }
}