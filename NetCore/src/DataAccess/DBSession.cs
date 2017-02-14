using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess
{
    /// <summary>
    /// 数据库连接事务的Session对象
    /// </summary>
    public class DBSessionBase : IDBSession
    {
        private static IDbConnection _connection;
        private static IDbTransaction _transaction;
        private readonly string _connKey;

        public string ConnKey
        {
            get { return _connKey; }
        }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public IDbConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// 数据库事务对象
        /// </summary>
        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        public DBSessionBase(IDatabase Database)
        {
            _connection = Database.Connection;
            _connKey = Database.ConnKey;
        }

        /// <summary>
        /// 开启会话
        /// </summary>
        /// <param name="isolation"></param>
        /// <returns></returns>
        public IDbTransaction Begin(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction(isolation);
            return _transaction;
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
            _transaction = null;
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
            _transaction = null;
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction = null;
                }
                _connection.Close();
                _connection = null;
            }
            GC.SuppressFinalize(this);
        }


    }
}
