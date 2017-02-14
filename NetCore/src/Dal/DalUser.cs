//
//Created: 2016-04-08 10:58:21
//Author: 代码生成
//
using System;
using DataAccess;

namespace Dal
{
    /// <summary>
    /// Blog_Article：数据访问对象
    /// </summary>
    public class DalUser : BaseRepository<Model.User>, IDisposable
    {        
        public static readonly DalUser Instance = new DalUser();

        private DalUser()
        {
        }

        public DalUser(IDBSession dbSession)
            : base(dbSession)
        {
        }
    }
}
