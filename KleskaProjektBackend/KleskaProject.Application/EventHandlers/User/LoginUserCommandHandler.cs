using KleskaProject.Application.Commands;
using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace KleskaProject.Application.EventHandlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<TokenDto>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginUserCommandHandler(IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
        {
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<TokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.LoginUserAsync(request.email, request.password);
            if (result.IsSuccess)
            {
                _authenticationService.SetTokensInsideCookie(result.Value, _httpContextAccessor.HttpContext);
            }
            return result;
        }
    }
}
