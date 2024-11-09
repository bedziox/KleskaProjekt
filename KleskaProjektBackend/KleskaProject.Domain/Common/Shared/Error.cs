using System.Net;

namespace KleskaProject.Domain.Common.Shared
{
    public record Error(HttpStatusCode StatusCode, string Details);
}
