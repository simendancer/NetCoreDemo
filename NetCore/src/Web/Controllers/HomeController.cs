using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Core.Service;
using RedisHelper;
using System.Collections.Specialized;

namespace Web.Controllers
{
    public class HomeController : BaceController
    {
        private UserService UserService { get; set; }

        public HomeController(UserService userService)
        {
            this.UserService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Company = "Welfull";
            ViewBag.Name = "Harry";

            //创建数组
            string[] names = new string[] { "ZeosonY", "XuZ", "Owen", "Actor", "Director" };
            ViewBag.Names = names;

            //创建字典
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ZeosonY", "想做鱼");
            dic.Add("XuZ", "Lave");
            dic.Add("Owen", "Owen");
            dic.Add("Actor", "演员");
            dic.Add("Director", "导演");

            //插入到redis-string
            RedisProvider.StringSetSync(dic);
            //插入到redis-list
            foreach (var item in dic)
            {
                //左侧插入
                await RedisProvider.ListLPush("MyList", item.Value);
                //从右侧删除并获取
                await RedisProvider.ListRightPop("MyList");
            }

            //string对集合
            NameValueCollection list = new NameValueCollection();
            list.Add("XuZ", "username1");
            list.Add("XuZ", "username2");
            list.Add("LiuD", "username3");

            //设置cookie
            Tools.Utility.CookieHelper.SetObj("XXX", 120, list);
            Tools.Utility.CookieHelper.SetObj("YZX", 120, "XZY");

            ViewBag.Dic = dic;
            return View();
        }

        public async Task<IActionResult> Detail()
        {
            //读取redis设置的其中一个string值
            string value1 = await RedisProvider.StringGet("ZeosonY");
            string value2 = await RedisProvider.StringGet("XuZ");
            string value3 = await RedisProvider.StringGet("Owen");
            string value4 = await RedisProvider.StringGet("Actor");
            string value5 = await RedisProvider.StringGet("Director");

            //读取redis设置的list
            List<string> list = await RedisProvider.ListRange("MyList", 0, -1);

            //读取设置的cookie
            string cookie1 = Tools.Utility.CookieHelper.GetValue("YZX");
            string cookie2 = Tools.Utility.CookieHelper.GetValue("XXX");

            //测试分页
            int page = RequestInt("page");
            page = page == 0 ? 1 : page;
            long records = 0;
            int pageSize = 10;
            var userList = Bll.BllUser.Instance.GetPageList(page, pageSize, out records);
            string pageHtml = GetPageHtml(page, pageSize, records, 5, "/Home/Detail?page={0}");

            ViewBag.cookie1 = cookie1;
            ViewBag.cookie2 = cookie2;
            ViewBag.name1 = value1;
            ViewBag.name2 = value2;
            ViewBag.name3 = value3;
            ViewBag.name4 = value4;
            ViewBag.name5 = value5;
            ViewBag.list = list;
            ViewBag.userList = userList;
            ViewBag.pageHtml = pageHtml;
            return View(UserService.CurrentUser);
        }
    }
}
