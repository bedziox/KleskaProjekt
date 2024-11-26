using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using Microsoft.AspNetCore.Http;

namespace KleskaProject.Application.Services
{
    public interface IAuthenticationService
    {
        public Task<Result<Guid>> RegisterUserAsync(UserDto request);
        public Task<Result<TokenDto>> LoginUserAsync(string email, string password);
        public Result<TokenDto> ValidateUser(TokenDto TokenDto);
        public Task<TokenDto> CreateToken(User user, bool populateExp);
        public Task<Result<TokenDto>> RefreshTokenAsync(TokenDto token);
        public void SetTokensInsideCookie(TokenDto tokenDto, HttpContext context);

    }
}
