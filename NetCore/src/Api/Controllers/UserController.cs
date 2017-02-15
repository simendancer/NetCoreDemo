using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string username)
        {
            var model = Bll.BllUser.Instance.GetById<Model.User>(1);
            if (model != null)
            {
                model.UserName = username;
                Bll.BllUser.Instance.Update<Model.User>(model);
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getuser/{id}")]
        public Model.User GetUser(int id)
        {
            var model = Bll.BllUser.Instance.GetById<Model.User>(id);
            string cookie = Tools.Utility.CookieHelper.GetValue("YZX");
            string name = RedisHelper.RedisProvider.StringGetSync("XuZ");

            return model;
        }
    }
}
