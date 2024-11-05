using KleskaProject.Domain.UserAggregate;
using KleskaProject.Infrastructure.Persistence;
using KleskaProjekt.Domain.Common.Shared;
using KleskaProjekt.Infrastructure.Extensions;
using KleskaProjekt.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace KleskaProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }
    }
}
