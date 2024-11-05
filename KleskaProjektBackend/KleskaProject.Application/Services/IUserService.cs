using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;

namespace KleskaProject.Application.Services
{
    public interface IUserService
    {
        public Task<Result<Guid>> RegisterUserAsync(UserDto request);
        public Task<Result<string>> LoginUserAsync(string email, string password);

        public Result<string> ValidateUser(string token);

        public string CreateToken(User user);

    }
}
