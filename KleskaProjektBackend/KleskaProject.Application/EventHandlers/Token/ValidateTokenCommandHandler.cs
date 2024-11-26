using KleskaProject.Application.Commands;
using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using MediatR;

namespace KleskaProject.Application.EventHandlers
{
    public class ValidateTokenCommandHandler : IRequestHandler<ValidateTokenCommand, Result<TokenDto>>
    {
        private readonly IAuthenticationService _authenticationService;

        public ValidateTokenCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Result<TokenDto>> Handle(ValidateTokenCommand request, CancellationToken cancellationToken)
        {
            return _authenticationService.ValidateUser(request.token);
        }
    }
}
