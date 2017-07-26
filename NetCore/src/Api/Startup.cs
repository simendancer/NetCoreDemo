using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Core;

namespace Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(this.Configuration);
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddOptions();
            services.AddMvc();
            //自定义拓展服务
            services.AddCustomerServices();

            #region 跨域
            var urls = Configuration["AppConfig:Cores"].Split(',');
            services.AddCors(options =>
            options.AddPolicy("AllowDomain", builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials())
            );
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            //注册静态文件访问
            app.UseStaticFiles();

            //数据访问中间件
            app.DataAccessMiddleWare();

            //注册cookie中间件
            app.CookieMiddleWare();

            //注册redis中间件
            app.RedisMiddleWare();

            //注册到用户中心中间件
            app.UserMiddleWare();

            //注册MVC
            app.UseMvc();

            //添加跨域
            app.UseCors("AllowDomain");
        }
    }
}
