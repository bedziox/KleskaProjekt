using KleskaProject.Domain.UserAggregate;
using MediatR;

namespace KleskaProject.Application.Queries
{
    public record GetUserQuery() : IRequest<User>;


}
