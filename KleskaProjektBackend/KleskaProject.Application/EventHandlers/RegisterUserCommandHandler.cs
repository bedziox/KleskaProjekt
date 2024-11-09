using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using MediatR;

namespace KleskaProject.Application.Commands.User
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        private readonly IUserService _userService;

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        async public Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.RegisterUserAsync(request.user);
            return result;
        }
    }
}
