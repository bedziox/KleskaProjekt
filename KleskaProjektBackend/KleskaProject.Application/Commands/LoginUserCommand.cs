using KleskaProject.Domain.Common.Shared;
using MediatR;

namespace KleskaProject.Application.Commands
{
    public record LoginUserCommand(string email, string password) : IRequest<Result<string>>;
}
