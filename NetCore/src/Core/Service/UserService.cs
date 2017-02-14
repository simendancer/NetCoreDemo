using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;

namespace Core.Service
{
    public class UserService
    {
        private IConfiguration Configuration { get; set; }
        private HttpContext HttpContext { get; set; }
        public User CurrentUser { get; set; }

        public UserService(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void Init(HttpContext context)
        {
            this.HttpContext = context;
            IList<User> list = Bll.BllUser.Instance.GetList<User>() as List<User>;
            if (list != null && list.Count > 0)
            {
                this.CurrentUser = Bll.BllUser.Instance.GetById<User>(list[0].Id);
            }
            else
            {
                this.CurrentUser = new User()
                {
                    Active = true,
                    CreateDate = DateTime.Now,
                    Email = "Test@qq.com",
                    Id = 1,
                    IsLock = false,
                    Password = "123456",
                    Token = Guid.NewGuid().ToString(),
                    UserName = "Test",
                    WebSite = "www.test.com"
                };
            }
        }

        public bool IsLogin
        {
            get
            {
                return this.CurrentUser != null && this.CurrentUser.UserName != "Test";
            }
        }

        public bool IsAdmin
        {
            get
            {
                return this.IsLogin && this.CurrentUser.UserName == "admin";
            }
        }

        public string CookieName
        {
            get
            {
                return this.Configuration["CookieName"];
            }
        }
    }
}
