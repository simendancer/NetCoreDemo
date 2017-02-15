using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class CookieMiddleWare
    {
        private readonly RequestDelegate _next;

        private static IConfiguration Configuration { get; set; }

        public CookieMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IConfiguration configuration)
        {
            Tools.Utility.CookieHelper.Instance(httpContext);//实例化cookie
            return _next(httpContext);
        }
    }
}
