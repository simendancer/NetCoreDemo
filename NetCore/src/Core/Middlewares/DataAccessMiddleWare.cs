using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class DataAccessMiddleWare
    {
        private readonly RequestDelegate _next;

        private static IConfiguration Configuration { get; set; }

        public DataAccessMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IConfiguration configuration)
        {
            new DataAccess.SqlConnectionFactory(configuration);
            return _next(httpContext);
        }
    }
}
