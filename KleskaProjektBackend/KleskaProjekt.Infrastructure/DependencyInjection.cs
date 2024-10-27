using KleskaProject.Domain.UserAggregate;
using KleskaProject.Infrastructure.Persistence;
using KleskaProjekt.Domain.Common.Shared;
using KleskaProjekt.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace KleskaProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }
    }
}
