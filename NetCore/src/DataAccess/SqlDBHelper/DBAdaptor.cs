using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using DataAccess;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DataAccess.Transactions;

namespace DataAccess.SqlDBHelper
{
    /// <summary>
    /// 数据库访问适配器 默认连接字符串使用【DefaultConnection】
    /// </summary>
    public class DBAdaptor : IDBHelper
    {
        private static IConfiguration Configuration { get; set; }

        private readonly string _ConnectionStringKey = "ConnectionString";

        public DBAdaptor(string connKey = "")
        {
            if (!string.IsNullOrEmpty(connKey))
            {
                _ConnectionStringKey = connKey;
            }
        }


        /// <summary>
        /// 取得数据库连接
        /// </summary>
        /// <param name="DBKey">数据库连接主键</param>
        /// <returns></returns>
        public static SqlConnection GetConnByKey(string connectionStringKey)
        {
            string constr = Configuration[connectionStringKey];
            SqlConnection con = new SqlConnection(constr);
            return con;
        }

        #region 事务



        /// <summary>
        /// 开始一个事务
        /// </summary>
        public IDbTransaction BeginTractionand(IsolationLevel Iso = IsolationLevel.Unspecified)
        {
            SqlConnection con = GetConnByKey(_ConnectionStringKey);
            IDbTransaction transaction = SQLHelper.BeginTransaction(con, Iso);
            return transaction;
        }

        /// <summary>
        /// 开始一个事务
        /// </summary>
        public IDbTransaction BeginTractionand(string connKey, IsolationLevel Iso = IsolationLevel.Unspecified)
        {
            SqlConnection con = GetConnByKey(connKey);
            IDbTransaction transaction = SQLHelper.BeginTransaction(con, Iso);
            return transaction;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTractionand(IDbTransaction dbTransaction)
        {
            SQLHelper.endTransactionRollback(dbTransaction);
        }

        /// <summary>
        /// 结束并确认事务
        /// </summary>
        public void CommitTractionand(IDbTransaction dbTransaction)
        {
            SQLHelper.endTransactionCommit(dbTransaction);
        }
        #endregion

        //#region DataSet


        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="commandText">sql语句</param>
        //public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        //{
        //    if (null != DataAccess.Transactions.Transaction.Current)
        //    {
        //        DataSet ds = SQLHelper.ExecuteDataset(Transaction.Current.DbTransactionWrapper.DbTransaction, commandType, commandText);
        //        return ds;
        //    }
        //    else
        //    {
        //        SqlConnection con = GetConnByKey(_ConnectionStringKey);
        //        DataSet ds = SQLHelper.ExecuteDataset(con, commandType, commandText);
        //        return ds;
        //    }
        //}

        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="connectionStringKey">数据库连接字符串的Key</param>
        ///// <param name="commandText">sql语句</param>
        //public DataSet ExecuteDataSet(string connKey, string commandText, CommandType commandType)
        //{
        //    SqlConnection con = GetConnByKey(connKey);
        //    DataSet ds = SQLHelper.ExecuteDataset(con, commandType, commandText);
        //    return ds;
        //}

        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="parameterValues">参数</param>
        //public DataSet ExecuteDataSet(string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        //{
        //    if (null != DataAccess.Transactions.Transaction.Current)
        //    {
        //        DataSet ds = SQLHelper.ExecuteDataset(Transaction.Current.DbTransactionWrapper.DbTransaction, commandType, commandText, parameterValues);
        //        return ds;
        //    }
        //    else
        //    {
        //        SqlConnection con = GetConnByKey(_ConnectionStringKey);
        //        DataSet ds = SQLHelper.ExecuteDataset(con, commandType, commandText, parameterValues);
        //        return ds;
        //    }
        //}

        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="connectionStringKey">数据库连接字符串的Key</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="parameterValues">参数</param>
        //public DataSet ExecuteDataSet(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        //{
        //    SqlConnection con = GetConnByKey(connKey);
        //    DataSet ds = SQLHelper.ExecuteDataset(con, commandType, commandText, parameterValues);
        //    return ds;
        //}


        //public DataSet ExecuteDataSet(IDbTransaction trans, string commandText, CommandType commandType)
        //{
        //    DataSet ds = SQLHelper.ExecuteDataset(trans, commandType, commandText);
        //    return ds;
        //}

        //public DataSet ExecuteDataSet(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        //{
        //    DataSet ds = SQLHelper.ExecuteDataset(trans, commandType, commandText, parameterValues);
        //    return ds;
        //}

        //public DataSet ExecuteDataSet(IDbConnection conn, string commandText, CommandType commandType)
        //{
        //    DataSet ds = SQLHelper.ExecuteDataset((SqlConnection)conn, commandType, commandText);
        //    return ds;
        //}

        //public DataSet ExecuteDataSet(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        //{
        //    DataSet ds = SQLHelper.ExecuteDataset((SqlConnection)conn, commandType, commandText, parameterValues);
        //    return ds;
        //}

        //#endregion

        #region ExecuteNonQuery


        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            if (null != DataAccess.Transactions.Transaction.Current)
            {
                int result = SQLHelper.ExecuteNonQuery(Transaction.Current.DbTransactionWrapper.DbTransaction, commandType, commandText);
                return result;
            }
            else
            {
                SqlConnection con = GetConnByKey(_ConnectionStringKey);
                int result = SQLHelper.ExecuteNonQuery(con, commandType, commandText);
                return result;
            }
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery(string connKey, string commandText, CommandType commandType)
        {
            SqlConnection con = GetConnByKey(connKey);
            int result = SQLHelper.ExecuteNonQuery(con, commandType, commandText);
            return result;
        }




        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery(IDbTransaction trans, string commandText, CommandType commandType)
        {
            int result = SQLHelper.ExecuteNonQuery(trans, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery(string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            if (null != DataAccess.Transactions.Transaction.Current)
            {
                int result = SQLHelper.ExecuteNonQuery(Transaction.Current.DbTransactionWrapper.DbTransaction, commandType, commandText, parameterValues);
                return result;
            }
            else
            {
                SqlConnection con = GetConnByKey(_ConnectionStringKey);
                int result = SQLHelper.ExecuteNonQuery(con, commandType, commandText, parameterValues);
                return result;
            }
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            SqlConnection con = GetConnByKey(connKey);
            int result = SQLHelper.ExecuteNonQuery(con, commandType, commandText, parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            int result = SQLHelper.ExecuteNonQuery(trans, commandType, commandText, parameterValues);
            return result;
        }



        public int ExecuteNonQuery(IDbConnection conn, string commandText, CommandType commandType)
        {
            int result = SQLHelper.ExecuteNonQuery((SqlConnection)conn, commandType, commandText);
            return result;
        }

        public int ExecuteNonQuery(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            int result = SQLHelper.ExecuteNonQuery((SqlConnection)conn, commandType, commandText, parameterValues);
            return result;
        }


        #endregion

        #region IDataReader

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>   
        /// <param name="commandText">sql语句</param>
        public IDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            if (null != DataAccess.Transactions.Transaction.Current)
            {
                IDataReader dr = SQLHelper.ExecuteReader(Transaction.Current.DbTransactionWrapper.DbTransaction, commandType, commandText);
                return dr;
            }
            else
            {
                SqlConnection con = GetConnByKey(_ConnectionStringKey);
                IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText);
                return dr;
            }
        }

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary> 
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public IDataReader ExecuteReader(string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            if (null != DataAccess.Transactions.Transaction.Current)
            {
                IDataReader dr = SQLHelper.ExecuteReader(Transaction.Current.DbTransactionWrapper.DbTransaction, commandType, commandText, parameterValues);
                return dr;
            }
            else
            {
                SqlConnection con = GetConnByKey(_ConnectionStringKey);
                IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText, parameterValues);
                return dr;
            }
        }

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>        
        /// <param name="commandText">sql语句</param>
        public IDataReader ExecuteReader(string connKey, string commandText, CommandType commandType)
        {
            SqlConnection con = GetConnByKey(connKey);
            IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText);
            return dr;
        }

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>        
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public IDataReader ExecuteReader(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            SqlConnection con = GetConnByKey(connKey);
            IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText, parameterValues);
            return dr;
        }

        public IDataReader ExecuteReader(IDbTransaction trans, string commandText, CommandType commandType)
        {
            IDataReader dr = SQLHelper.ExecuteReader(trans, commandType, commandText);
            return dr;
        }

        public IDataReader ExecuteReader(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            IDataReader dr = SQLHelper.ExecuteReader(trans, commandType, commandText, parameterValues);
            return dr;
        }

        public IDataReader ExecuteReader(IDbConnection conn, string commandText, CommandType commandType)
        {
            IDataReader dr = SQLHelper.ExecuteReader((SqlConnection)conn, commandType, commandText);
            return dr;
        }

        public IDataReader ExecuteReader(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            IDataReader dr = SQLHelper.ExecuteReader((SqlConnection)conn, commandType, commandText, parameterValues);
            return dr;
        }

        #endregion

        //#region IEnumerable<T>

        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary>   
        ///// <param name="commandText">sql语句</param>
        //public IEnumerable<T> ExecuteIEnumerable<T>(string commandText, CommandType commandType) where T : class, new()
        //{
        //    if (null != DataAccess.Transactions.Transaction.Current)
        //    {
        //        return ExecuteIEnumerable<T>(Transaction.Current.DbTransactionWrapper.DbTransaction, commandText, commandType);
        //    }
        //    else
        //    {
        //        using (SqlConnection con = GetConnByKey(_ConnectionStringKey))
        //        {
        //            using (IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText))
        //            {
        //                return DataReaderExtensions.DataReaderToList<T>(dr);
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary> 
        ///// <param name="commandText">sql语句</param>
        ///// <param name="parameterValues">参数</param>
        //public IEnumerable<T> ExecuteIEnumerable<T>(string commandText, CommandType commandType, params IDataParameter[] parameterValues) where T : class, new()
        //{
        //    if (null != DataAccess.Transactions.Transaction.Current)
        //    {
        //        return ExecuteIEnumerable<T>(Transaction.Current.DbTransactionWrapper.DbTransaction, commandText, commandType, parameterValues);
        //    }
        //    else
        //    {
        //        using (SqlConnection con = GetConnByKey(_ConnectionStringKey))
        //        {
        //            using (IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText, parameterValues))
        //            {
        //                return DataReaderExtensions.DataReaderToList<T>(dr);
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary>
        ///// <param name="connectionStringKey">数据库连接字符串的Key</param>        
        ///// <param name="commandText">sql语句</param>
        //public IEnumerable<T> ExecuteIEnumerable<T>(string connKey, string commandText, CommandType commandType) where T : class, new()
        //{
        //    using (SqlConnection con = GetConnByKey(_ConnectionStringKey))
        //    {
        //        using (IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText))
        //        {
        //            return DataReaderExtensions.DataReaderToList<T>(dr);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary>
        ///// <param name="connectionStringKey">数据库连接字符串的Key</param>        
        ///// <param name="commandText">sql语句</param>
        ///// <param name="parameterValues">参数</param>
        //public IEnumerable<T> ExecuteIEnumerable<T>(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues) where T : class, new()
        //{
        //    using (SqlConnection con = GetConnByKey(_ConnectionStringKey))
        //    {
        //        using (IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText, parameterValues))
        //        {
        //            return DataReaderExtensions.DataReaderToList<T>(dr);
        //        }
        //    }
        //}

        //public IEnumerable<T> ExecuteIEnumerable<T>(IDbTransaction trans, string commandText, CommandType commandType) where T : class, new()
        //{
        //    using (IDataReader dr = SQLHelper.ExecuteReader(trans, commandType, commandText))
        //    {
        //        return DataReaderExtensions.DataReaderToList<T>(dr);
        //    }

        //}

        //public IEnumerable<T> ExecuteIEnumerable<T>(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues) where T : class, new()
        //{
        //    using (IDataReader dr = SQLHelper.ExecuteReader(trans, commandType, commandText, parameterValues))
        //    {
        //        return DataReaderExtensions.DataReaderToList<T>(dr);
        //    }
        //}


        //public IEnumerable<T> ExecuteIEnumerable<T>(IDbConnection conn, string commandText, CommandType commandType) where T : class, new()
        //{
        //    using (IDataReader dr = SQLHelper.ExecuteReader((SqlConnection)conn, commandType, commandText))
        //    {
        //        return DataReaderExtensions.DataReaderToList<T>(dr);
        //    }
        //}

        //public IEnumerable<T> ExecuteIEnumerable<T>(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues) where T : class, new()
        //{
        //    using (IDataReader dr = SQLHelper.ExecuteReader((SqlConnection)conn, commandType, commandText, parameterValues))
        //    {
        //        return DataReaderExtensions.DataReaderToList<T>(dr);
        //    }
        //}


        //#endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            if (null != DataAccess.Transactions.Transaction.Current)
            {
                object result = SQLHelper.ExecuteScalar(Transaction.Current.DbTransactionWrapper.DbTransaction, commandType, commandText);
                return result;
            }
            else
            {
                SqlConnection con = GetConnByKey(_ConnectionStringKey);
                object result = SQLHelper.ExecuteScalar(con, commandType, commandText);
                return result;
            }
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public T ExecuteScalar<T>(string commandText, CommandType commandType)
        {
            if (null != DataAccess.Transactions.Transaction.Current)
            {
                object result = SQLHelper.ExecuteScalar(Transaction.Current.DbTransactionWrapper.DbTransaction, commandType, commandText);
                return (T)result;
            }
            else
            {
                SqlConnection con = GetConnByKey(_ConnectionStringKey);
                object result = SQLHelper.ExecuteScalar(con, commandType, commandText);
                return (T)result;
            }
        }


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar(string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            if (null != DataAccess.Transactions.Transaction.Current)
            {
                object result = SQLHelper.ExecuteScalar(Transaction.Current.DbTransactionWrapper.DbTransaction, commandType, commandText, parameterValues);
                return result;
            }
            else
            {
                SqlConnection con = GetConnByKey(_ConnectionStringKey);
                object result = SQLHelper.ExecuteScalar(con, commandType, commandText, parameterValues);
                return result;
            }
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public T ExecuteScalar<T>(string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            if (null != DataAccess.Transactions.Transaction.Current)
            {
                return (T)ExecuteScalar(Transaction.Current.DbTransactionWrapper.DbTransaction, commandText, commandType, parameterValues);
            }
            else
            {
                return (T)ExecuteScalar(commandText, commandType, parameterValues);
            }
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar(IDbTransaction trans, string commandText, CommandType commandType)
        {
            object result = SQLHelper.ExecuteScalar(trans, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        public T ExecuteScalar<T>(IDbTransaction trans, string commandText, CommandType commandType)
        {
            return (T)ExecuteScalar(trans, commandText, commandType);
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar(string connKey, string commandText, CommandType commandType)
        {
            SqlConnection con = GetConnByKey(connKey);
            object result = SQLHelper.ExecuteScalar(con, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        public T ExecuteScalar<T>(string connKey, string commandText, CommandType commandType)
        {
            return (T)ExecuteScalar(connKey, commandText, commandType);
        }


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            SqlConnection con = GetConnByKey(connKey);
            object result = SQLHelper.ExecuteScalar(con, commandType, commandText, parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public T ExecuteScalar<T>(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            return (T)ExecuteScalar(connKey, commandText, commandType, parameterValues);
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            object result = SQLHelper.ExecuteScalar(trans, commandType, commandText, parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public T ExecuteScalar<T>(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            return (T)ExecuteScalar(trans, commandText, commandType, parameterValues);
        }



        public object ExecuteScalar(IDbConnection conn, string commandText, CommandType commandType)
        {
            object result = SQLHelper.ExecuteScalar((SqlConnection)conn, commandType, commandText);
            return result;
        }
        public object ExecuteScalar(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            object result = SQLHelper.ExecuteScalar((SqlConnection)conn, commandType, commandText, parameterValues);
            return result;
        }



        public T ExecuteScalar<T>(IDbConnection conn, string commandText, CommandType commandType)
        {
            return (T)ExecuteScalar(conn, commandText, commandType);
        }



        public T ExecuteScalar<T>(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues)
        {
            return (T)ExecuteScalar(conn, commandText, commandType, parameterValues);
        }
        #endregion

        /// <summary>
        /// 生成分页SQL语句
        /// </summary>
        /// <param name="pageIndex">page索引</param>
        /// <param name="pageSize">page大小</param>
        /// <param name="selectSql">查询语句</param>
        /// <param name="SqlCount">查询总数的语句</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public string GetPagingSql(int pageIndex, int pageSize, string selectSql, string SqlCount, string orderBy)
        {
            return PageHelper.GetPagingSql(pageIndex, pageSize, selectSql, SqlCount, orderBy);
        }
    }

}
