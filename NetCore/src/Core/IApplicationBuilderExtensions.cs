using Core.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UserMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UserMiddleWare>();
        }

        public static IApplicationBuilder RedisMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RedisMiddleWare>();
        }

        public static IApplicationBuilder CookieMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CookieMiddleWare>();
        }
        public static IApplicationBuilder DataAccessMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DataAccessMiddleWare>();
        }
    }
}
