using Microsoft.AspNetCore.Hosting;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Web;

namespace Test
{
    public class Program
    {
        private static List<User> alllist = null;
        static Random random = new Random();

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
            host.Start();

            InsertTest();
            SelectTest();
            UpdateTest();
            //DeleteTest();

            Console.WriteLine("进程结束");
            Console.ReadLine();
        }

        private static void InsertTest()
        {
            User user = null;
            Stopwatch watcher = new Stopwatch();
            watcher.Start();
            int count = 0;
            int number = 10000;
            for (int i = 0; i < number; i++)
            {
                user = new User()
                {
                    Active = random.Next() % 2 == 0 ? true : false,
                    IsLock = random.Next() % 2 == 0 ? true : false,
                    CreateDate = DateTime.Now,
                    Email = "Insert@test.com",
                    Password = "123456",
                    Token = Guid.NewGuid().ToString(),
                    UserName = "我是第" + (i + 1) + "条数据",
                    WebSite = "test.insert.com"
                };
                if (Bll.BllUser.Instance.Insert(user) > 0) count++;
            }
            watcher.Stop();
            Console.WriteLine("插入" + number + "条数据，成功：" + count + ",耗时：" + watcher.ElapsedMilliseconds + "ms");
            Console.WriteLine("平均每条" + (watcher.ElapsedMilliseconds * 1.0f / number).ToString("F2") + "ms");
            Console.WriteLine();
        }

        private static void SelectTest()
        {
            Stopwatch watcher = new Stopwatch();
            watcher.Start();
            var list = Bll.BllUser.Instance.GetList<User>();
            watcher.Stop();
            alllist = list.ToList();
            Console.WriteLine("查询" + alllist.Count + "条数据,耗时" + watcher.ElapsedMilliseconds + "ms");
            Console.WriteLine();
        }

        private static void UpdateTest()
        {

            Stopwatch watcher = new Stopwatch();
            watcher.Start();
            int count = 0;
            foreach (var item in alllist)
            {
                item.Active = random.Next() % 2 == 0 ? true : false;
                item.IsLock = random.Next() % 2 == 0 ? true : false;
                item.CreateDate = DateTime.Now;
                item.Email = "Update@test.com";
                item.Password = "654321";
                item.Token = Guid.NewGuid().ToString();
                item.WebSite = "test.update.com";
                if (Bll.BllUser.Instance.Update(item)) count++; //单条更新
            }
            //if (Bll.BllUser.Instance.UpdateBatch(alllist)) count = alllist.Count; //批量更新

            watcher.Stop();
            Console.WriteLine("更新" + alllist.Count + "条数据，成功：" + count + ",耗时：" + watcher.ElapsedMilliseconds + "ms");
            Console.WriteLine("平均每条" + (watcher.ElapsedMilliseconds * 1.0f / alllist.Count).ToString("F2") + "ms");
            Console.WriteLine();
        }

        private static void DeleteTest()
        {
            Stopwatch watcher = new Stopwatch();
            watcher.Start();
            int count = 0;
            foreach (var item in alllist)
                if (Bll.BllUser.Instance.Delete<User>(item.Id) > 0) count++;
            watcher.Stop();
            Console.WriteLine("删除" + alllist.Count + "条数据，成功：" + count + ",耗时：" + watcher.ElapsedMilliseconds + "ms");
            Console.WriteLine("平均每条" + (watcher.ElapsedMilliseconds * 1.0f / alllist.Count).ToString("F2") + "ms");
            Console.WriteLine();
        }
    }
}
