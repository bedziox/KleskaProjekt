using KleskaProject.Application.Commands;
using KleskaProject.Application.Services;
using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace KleskaProject.Application.EventHandlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenDto>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _contextAccessor;

        public RefreshTokenCommandHandler(IAuthenticationService authenticationService, IHttpContextAccessor contextAccessor)
        {
            _authenticationService = authenticationService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenDtoResult = await _authenticationService.RefreshTokenAsync(request.token);
            _authenticationService.SetTokensInsideCookie(tokenDtoResult.Value, _contextAccessor.HttpContext);
            return tokenDtoResult;
        }
    }
}
