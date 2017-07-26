using Core.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCustomerServices(this IServiceCollection services)
        {
            services.AddSingleton<DataAccessService>()
                     .AddSingleton<RedisService>()
                    .AddSingleton<CookieService>()
                   .AddScoped<UserService>();

        }
    }
}
