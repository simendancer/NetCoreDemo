using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Service;

namespace Core.Middlewares
{
    public class UserMiddleWare
    {
        private readonly RequestDelegate _next;

        public UserMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.RequestServices.GetService<UserService>().Init(httpContext);
            return _next(httpContext);
        }
    }
}
