using System;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{

    /// <summary>
    /// 数据库接口
    /// </summary>
    public interface IDatabase
    {
        IDbConnection Connection { get; }
        string ConnKey { get; }
    }

    /// <summary>
    /// 数据库类对象
    /// </summary>
    public class Database : IDatabase
    {
        public IDbConnection Connection { get; private set; }

        public string ConnKey { get; set; }

        public Database(IDbConnection connection)
        {
            Connection = connection;
        }

        public Database(string connKey)
        {
            ConnKey = connKey;
            if (Connection == null)
            {
                Connection = SqlConnectionFactory.CreateSqlConnection(connKey);
            }
            else
            {
                Connection.Close();
                Connection = SqlConnectionFactory.CreateSqlConnection(connKey);
            }
        }

    }


    /// <summary>
    /// 数据连接事务的Session接口
    /// </summary>
    public interface IDBSession : IDisposable
    {
        string ConnKey { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IDbTransaction Begin(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
    }
}
