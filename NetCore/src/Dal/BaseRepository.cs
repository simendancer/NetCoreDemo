using DataAccess;
using System;
using DapperExtensions;

namespace Dal
{
    public abstract class BaseRepository<T> : RepositoryBase, IDisposable
 where T : class
    {
        protected BaseRepository()
        {
            SetDBSession(Helper.CreateDBSession(CommonConfig.DefaultConnKey));
        }
        protected BaseRepository(IDBSession dbSession)
            : base(dbSession)
        {
        }
        public void Dispose()
        {
        }
    }

    public class Helper
    {

        public static IDBSession CreateDBSession(string connKey)
        {
            DataAccess.IDatabase db = new DataAccess.Database(connKey);
            IDBSession dbSession = new DBSessionBase(db);
            return dbSession;
        }
    }

    public class CommonConfig {

        public static string DefaultConnKey = "ConnectionString";
    }
}

