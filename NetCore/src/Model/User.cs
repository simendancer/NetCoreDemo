using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string WebSite { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsLock { get; set; }

        public string Token { get; set; }
    }
}
