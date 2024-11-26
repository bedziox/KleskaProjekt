using KleskaProject.Domain.Common.Shared;
using KleskaProject.Domain.UserAggregate;
using MediatR;

namespace KleskaProject.Application.Commands
{
    public record LoginUserCommand(string email, string password) : IRequest<Result<TokenDto>>;
}
