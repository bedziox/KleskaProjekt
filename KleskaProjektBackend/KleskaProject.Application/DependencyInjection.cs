using KleskaProject.Application.Services;
using KleskaProject.Domain.UserAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace KleskaProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppliaction(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;

        }
    }
}
