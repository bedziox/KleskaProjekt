using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using MediatR;

namespace KleskaProject.Application.Commands.User
{
    public class ValidateTokenCommandHandler : IRequestHandler<ValidateTokenCommand, Result<string>>
    {
        private readonly UserService _userService;

        public ValidateTokenCommandHandler(UserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<string>> Handle(ValidateTokenCommand request, CancellationToken cancellationToken)
        {
            return _userService.ValidateUser(request.token);
        }
    }
}
