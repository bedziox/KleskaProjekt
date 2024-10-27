using KleskaProject.Domain.Common.Shared;
using MediatR;

namespace KleskaProject.Application.Commands
{
    public record ValidateTokenCommand(string token) : IRequest<Result<string>>;
}
