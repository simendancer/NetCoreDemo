using Core.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Core.Middlewares
{
    public class RedisMiddleWare
    {
        private readonly RequestDelegate _next;

        private static IConfiguration Configuration { get; set; }

        public RedisMiddleWare(RequestDelegate next, IConfiguration configuration)
        {
            RedisHelper.RedisProvider.Instance(configuration);//实例化redis
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            return _next(httpContext);
        }
    }
}
