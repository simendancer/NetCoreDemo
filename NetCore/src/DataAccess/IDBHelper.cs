using System.Data.Common;
using System.Data;
using System.Collections.Generic;

namespace DataAccess
{ /// <summary>
  /// 提供对数据库的基本操作，连接字符串需要在数据库配置。
  /// </summary>
    public interface IDBHelper
    {
        /// <summary>
        /// 生成分页SQL语句
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="selectSql"></param>
        /// <param name="sqlCount"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        string GetPagingSql(int pageIndex, int pageSize, string selectSql, string sqlCount, string orderBy);

        #region 事务
        /// <summary>
        /// 开始一个事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTractionand(IsolationLevel Iso = IsolationLevel.Unspecified);


        /// <summary>
        /// 开始一个事务
        /// </summary>
        /// <param name="connKey">数据库连接字符key</param>
        IDbTransaction BeginTractionand(string connKey, IsolationLevel Iso = IsolationLevel.Unspecified);

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="dbTransaction">要回滚的事务</param>
        void RollbackTractionand(IDbTransaction dbTransaction);

        /// <summary>
        /// 结束并确认事务
        /// </summary>
        /// <param name="dbTransaction">要结束的事务</param>
        void CommitTractionand(IDbTransaction dbTransaction);

        #endregion

        //#region DataSet

        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        //DataSet ExecuteDataSet(string commandText, CommandType commandType);


        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="trans">事务</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        //DataSet ExecuteDataSet(IDbTransaction trans, string commandText, CommandType commandType);


        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="conn">IDbConnection</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        //DataSet ExecuteDataSet(IDbConnection conn, string commandText, CommandType commandType);


        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="connKey">数据库连接字符key</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        //DataSet ExecuteDataSet(string connKey, string commandText, CommandType commandType);


        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        ///// <param name="parameterValues">参数</param>
        //DataSet ExecuteDataSet(string commandText, CommandType commandType, params IDataParameter[] parameterValues);


        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="conn">IDbConnection</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        ///// <param name="parameterValues">参数</param>
        //DataSet ExecuteDataSet(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues);


        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="trans">事务</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        ///// <param name="parameterValues">参数</param>
        //DataSet ExecuteDataSet(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues);


        ///// <summary>
        ///// 执行sql语句,ExecuteDataSet 返回DataSet
        ///// </summary>
        ///// <param name="connKey">数据库连接字符key</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        ///// <param name="parameterValues">参数</param>
        //DataSet ExecuteDataSet(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues);



        //#endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        int ExecuteNonQuery(string commandText, CommandType commandType);


        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="connKey">数据库连接字符key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        int ExecuteNonQuery(string connKey, string commandText, CommandType commandType);

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        int ExecuteNonQuery(IDbTransaction trans, string commandText, CommandType commandType);

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="IDbConnection">IDbConnection</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        int ExecuteNonQuery(IDbConnection conn, string commandText, CommandType commandType);


        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        int ExecuteNonQuery(string commandText, CommandType commandType, params IDataParameter[] parameterValues);

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="connKey">数据库连接字符key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        int ExecuteNonQuery(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues);

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        int ExecuteNonQuery(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues);


        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="IDbConnection">IDbConnection</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        int ExecuteNonQuery(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues);

        #endregion

        #region IDataReader

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>   
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        IDataReader ExecuteReader(string commandText, CommandType commandType);

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>   
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        IDataReader ExecuteReader(IDbTransaction trans, string commandText, CommandType commandType);


        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>   
        /// <param name="IDbConnection">conn</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        IDataReader ExecuteReader(IDbConnection conn, string commandText, CommandType commandType);

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary> 
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        IDataReader ExecuteReader(string commandText, CommandType commandType, params IDataParameter[] parameterValues);



        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>   
        /// <param name="IDbConnection">conn</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        IDataReader ExecuteReader(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues);


        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>   
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        IDataReader ExecuteReader(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues);

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>
        /// <param name="connKey">数据库连接字符key</param>        
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        IDataReader ExecuteReader(string connKey, string commandText, CommandType commandType);

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>
        /// <param name="connKey">数据库连接字符key</param>        
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        IDataReader ExecuteReader(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues);

        #endregion

        //#region IEnumerable<T>

        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary>   
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        //IEnumerable<T> ExecuteIEnumerable<T>(string commandText, CommandType commandType) where T : class, new();

        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary>   
        ///// <param name="trans">事务</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        //IEnumerable<T> ExecuteIEnumerable<T>(IDbTransaction trans, string commandText, CommandType commandType) where T : class, new();

        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary>   
        ///// <param name="IDbConnection">conn</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        //IEnumerable<T> ExecuteIEnumerable<T>(IDbConnection conn, string commandText, CommandType commandType) where T : class, new();



        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary> 
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        ///// <param name="parameterValues">参数</param>
        //IEnumerable<T> ExecuteIEnumerable<T>(string commandText, CommandType commandType, params IDataParameter[] parameterValues) where T : class, new();



        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary>   
        ///// <param name="trans">事务</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        ///// <param name="parameterValues">参数</param>
        //IEnumerable<T> ExecuteIEnumerable<T>(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues) where T : class, new();


        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary>   
        ///// <param name="IDbConnection">conn</param>
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        ///// <param name="parameterValues">参数</param>
        //IEnumerable<T> ExecuteIEnumerable<T>(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues) where T : class, new();




        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary>
        ///// <param name="connKey">数据库连接字符key</param>        
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        //IEnumerable<T> ExecuteIEnumerable<T>(string connKey, string commandText, CommandType commandType) where T : class, new();

        ///// <summary>
        ///// 执行sql语句,ExecuteReader 返回IDataReader
        ///// </summary>
        ///// <param name="connKey">数据库连接字符key</param>        
        ///// <param name="commandText">sql语句</param>
        ///// <param name="commandType"></param>
        ///// <param name="parameterValues">参数</param>
        //IEnumerable<T> ExecuteIEnumerable<T>(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues) where T : class, new();



        //#endregion

        #region ExecuteScalar

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        object ExecuteScalar(string commandText, CommandType commandType);



        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="IDbConnection">conn</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        object ExecuteScalar(IDbConnection conn, string commandText, CommandType commandType);


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        object ExecuteScalar(IDbTransaction trans, string commandText, CommandType commandType);


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        T ExecuteScalar<T>(string commandText, CommandType commandType);



        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        T ExecuteScalar<T>(IDbTransaction trans, string commandText, CommandType commandType);



        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="conn">conn</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        T ExecuteScalar<T>(IDbConnection conn, string commandText, CommandType commandType);


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        object ExecuteScalar(string commandText, CommandType commandType, params IDataParameter[] parameterValues);


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        object ExecuteScalar(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues);


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="IDbConnection">conn</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        object ExecuteScalar(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues);


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        T ExecuteScalar<T>(string commandText, CommandType commandType, params IDataParameter[] parameterValues);


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        T ExecuteScalar<T>(IDbTransaction trans, string commandText, CommandType commandType, params IDataParameter[] parameterValues);


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="IDbConnection">conn</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        T ExecuteScalar<T>(IDbConnection conn, string commandText, CommandType commandType, params IDataParameter[] parameterValues);



        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connKey">数据库连接字符key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        object ExecuteScalar(string connKey, string commandText, CommandType commandType);

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connKey">数据库连接字符key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        T ExecuteScalar<T>(string connKey, string commandText, CommandType commandType);


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connKey">数据库连接字符key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        object ExecuteScalar(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues);

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connKey">数据库连接字符key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="parameterValues">参数</param>
        T ExecuteScalar<T>(string connKey, string commandText, CommandType commandType, params IDataParameter[] parameterValues);

        #endregion

    }
}