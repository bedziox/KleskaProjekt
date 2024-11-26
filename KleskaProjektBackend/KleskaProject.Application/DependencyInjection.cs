using KleskaProject.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace KleskaProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppliaction(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;

        }
    }
}
