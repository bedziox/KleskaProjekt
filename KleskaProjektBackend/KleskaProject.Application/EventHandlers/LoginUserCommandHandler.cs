using KleskaProject.Application.Commands;
using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using MediatR;

namespace KleskaProject.Application.EventHandlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginUserAsync(request.email, request.password);
            return result;
        }
    }
}
