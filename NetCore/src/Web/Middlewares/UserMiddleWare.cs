using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Service;

namespace Web.Middlewares
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
            var user = httpContext.RequestServices.GetService<UserService>();
            user.Init(httpContext);//获取用户信息

            return _next(httpContext);
        }
    }
}
