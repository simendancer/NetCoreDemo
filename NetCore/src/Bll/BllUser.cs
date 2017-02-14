//
//Created: 2016-04-08 10:58:21
//Author: 代码生成
//
using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions;
using Model;

namespace Bll
{
    /// <summary>
    /// Blog_Article：服务访问对象
    /// </summary>
    public class BllUser : ServiceBase, IDataServiceRepository, IDisposable
    {
        public BllUser()
        {
        }

        public static BllUser Instance
        {
            get
            {
                return new BllUser();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public IList<User> GetPageList(int page, int pagesize, out long records)
        {
            object predicate = new { IsLock = false };
            IList<ISort> sort = new List<ISort>();
            sort.Add(new Sort { Ascending = false, PropertyName = "Id" });
            var list = GetPageList<User>(page, pagesize, out records, predicate, sort, false).ToList();
            return list;
        }
    }
}
