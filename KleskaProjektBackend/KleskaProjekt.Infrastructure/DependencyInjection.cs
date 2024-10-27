using KleskaProject.Domain.UserAggregate;
using KleskaProject.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace KleskaProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;

        }
    }
}
