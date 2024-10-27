using KleskaProject.Application.Commands;
using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using MediatR;

namespace KleskaProject.Application.EventHandlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly UserService _userService;

        public LoginUserCommandHandler(UserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginUserAsync(request.user.Email, request.user.Password);
            return result;
        }
    }
}
