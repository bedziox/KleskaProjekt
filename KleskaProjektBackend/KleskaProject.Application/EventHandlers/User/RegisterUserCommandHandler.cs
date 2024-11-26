using KleskaProject.Application.Commands;
using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using MediatR;

namespace KleskaProject.Application.EventHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.RegisterUserAsync(request.user);
            return result;
        }
    }
}
