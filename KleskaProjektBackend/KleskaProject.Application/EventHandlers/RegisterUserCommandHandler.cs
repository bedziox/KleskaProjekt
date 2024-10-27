using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using MediatR;

namespace KleskaProject.Application.Commands.User
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        private readonly UserService _userService;

        public RegisterUserCommandHandler(UserService userService)
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
