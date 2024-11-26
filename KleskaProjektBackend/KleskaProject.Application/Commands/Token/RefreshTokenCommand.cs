using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using MediatR;

namespace KleskaProject.Application.Commands
{
    public record RefreshTokenCommand(TokenDto token) : IRequest<Result<TokenDto>>;
}
