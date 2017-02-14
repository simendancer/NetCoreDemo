using Core.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCustomerServices(this IServiceCollection services)
        {
            services.AddScoped<DataAccessService>()
                     .AddScoped<RedisService>()
                    .AddScoped<CookieService>()
                   .AddScoped<UserService>();
        }
    }
}
