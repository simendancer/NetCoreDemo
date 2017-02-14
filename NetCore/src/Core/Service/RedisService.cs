using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Core.Service
{
    public class RedisService
    {
        private IConfiguration Configuration { get; set; }
        private HttpContext HttpContext { get; set; }

        public RedisService(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void Init(HttpContext context)
        {
            this.HttpContext = context;
        }
    }
}
