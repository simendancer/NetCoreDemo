using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Service
{
    public class DataAccessService
    {
        private IConfiguration Configuration { get; set; }
        private HttpContext HttpContext { get; set; }

        public DataAccessService(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void Init(HttpContext context)
        {
            this.HttpContext = context;
        }
    }
}
