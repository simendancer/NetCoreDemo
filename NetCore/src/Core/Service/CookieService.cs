using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Core.Service
{
    public class CookieService
    {
        private IConfiguration Configuration { get; set; }
        private HttpContext HttpContext { get; set; }

        public CookieService(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void Init(HttpContext context)
        {
            this.HttpContext = context;
        }
    }
}
