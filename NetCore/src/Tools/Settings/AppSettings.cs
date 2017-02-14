using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tools.Settings
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }

        public string RedisWriteConn { get; set; }

        public string RedisReadConn { get; set; }

        public string RedisPrefix { get; set; }
      
        public string CookieName { get; set; }
    }
}
