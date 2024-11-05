using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using MediatR;

namespace KleskaProject.Application.Commands.User
{
    public class ValidateTokenCommandHandler : IRequestHandler<ValidateTokenCommand, Result<string>>
    {
        private readonly IUserService _userService;

        public ValidateTokenCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<string>> Handle(ValidateTokenCommand request, CancellationToken cancellationToken)
        {
            return _userService.ValidateUser(request.token);
        }
    }
}
