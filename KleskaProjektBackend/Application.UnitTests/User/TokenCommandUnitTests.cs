using KleskaProject.Application.Commands;
using KleskaProject.Application.EventHandlers;
using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using Moq;
using System.Net;

namespace Application.UnitTests.User
{
    public class RefreshTokenCommandHandlerTests
    {
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly RefreshTokenCommandHandler _handler;

        public RefreshTokenCommandHandlerTests()
        {
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _handler = new RefreshTokenCommandHandler(_mockAuthenticationService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenRefreshTokenIsValid()
        {
            // Arrange
            var token = new TokenDto("expiredAccessToken", "validRefreshToken");
            var command = new RefreshTokenCommand(token);
            var expectedToken = new TokenDto("newAccessToken", "newRefreshToken");
            _mockAuthenticationService.Setup(s => s.RefreshTokenAsync(token))
                .ReturnsAsync(Result<TokenDto>.Success(expectedToken));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(expectedToken.AccessToken, result.Value?.AccessToken);
            Assert.Equal(expectedToken.RefreshToken, result.Value?.RefreshToken);
            _mockAuthenticationService.Verify(s => s.RefreshTokenAsync(token), Times.Once); // Verify service call
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRefreshTokenIsInvalid()
        {
            // Arrange
            var token = new TokenDto("expiredAccessToken", "invalidRefreshToken");
            var command = new RefreshTokenCommand(token);
            _mockAuthenticationService.Setup(s => s.RefreshTokenAsync(token))
                .ReturnsAsync(Result<TokenDto>.Failure(new Error(HttpStatusCode.Unauthorized, "Invalid token")));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.Unauthorized, result.Error.StatusCode);
            Assert.Equal("Invalid token", result.Error.Details);
            _mockAuthenticationService.Verify(s => s.RefreshTokenAsync(token), Times.Once); // Verify service call
        }
    }

    public class ValidateTokenCommandHandlerTests
    {
        private readonly Mock<IAuthenticationService> _mockUserService;
        private readonly ValidateTokenCommandHandler _handler;

        public ValidateTokenCommandHandlerTests()
        {
            _mockUserService = new Mock<IAuthenticationService>();
            _handler = new ValidateTokenCommandHandler(_mockUserService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTokenIsValid()
        {
            // Arrange
            var token = new TokenDto("validAccessToken", "validRefreshToken");
            var command = new ValidateTokenCommand(token);
            _mockUserService.Setup(s => s.ValidateUser(token))
                .Returns(Result<TokenDto>.Success(token));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(token.AccessToken, result.Value?.AccessToken);
            Assert.Equal(token.RefreshToken, result.Value?.RefreshToken);
            _mockUserService.Verify(s => s.ValidateUser(token), Times.Once); // Verify service call
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenTokenIsInvalid()
        {
            // Arrange
            var token = new TokenDto("invalidAccessToken", "invalidRefreshToken");
            var command = new ValidateTokenCommand(token);
            _mockUserService.Setup(s => s.ValidateUser(token))
                .Returns(Result<TokenDto>.Failure(new Error(HttpStatusCode.Unauthorized, "Token not valid")));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.Unauthorized, result.Error.StatusCode);
            Assert.Equal("Token not valid", result.Error.Details);
            _mockUserService.Verify(s => s.ValidateUser(token), Times.Once); // Verify service call
        }
    }
}
