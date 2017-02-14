using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
using DapperExtensions.Mapper;
using System.Reflection;
using System.Data.SqlClient;

namespace DapperExtensions
{
    public class RepositoryServiceBase : IDataServiceRepository
    {
        public RepositoryServiceBase()
        {
        }
        public RepositoryServiceBase(IDBSession dbSession)
        {
            DBSession = dbSession;
        }


        public IDBSession DBSession { get; private set; }

        public void SetDBSession(IDBSession dbSession)
        {
            DBSession = dbSession;
        }


        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        public T GetById<T>(dynamic primaryId) where T : class
        {
            return DBSession.Connection.Get<T>(primaryId as object);
        }


        /// <summary>
        /// 根据多个Id获取多个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IEnumerable<T> GetByIds<T>(IList<dynamic> ids) where T : class
        {
            var tblName = string.Format("dbo.{0}", typeof(T).Name.Replace("Model", ""));
            var idsin = string.Join(",", ids.ToArray<dynamic>());
            var sql = "SELECT * FROM @table WHERE Id in (@ids)";
            IEnumerable<T> dataList = DBSession.Connection.Query<T>(sql, new { table = tblName, ids = idsin });
            return dataList;
        }


        /// <summary>
        /// 获取全部数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>() where T : class
        {
            return DBSession.Connection.GetList<T>();
        }


        /// <summary>
        /// 统计记录总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public int Count<T>(object predicate, bool buffered = false) where T : class
        {
            return DBSession.Connection.Count<T>(predicate);
        }

        /// <summary>
        /// 查询列表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(object predicate = null, IList<ISort> sort = null,
            bool buffered = false) where T : class
        {
            return DBSession.Connection.GetList<T>(predicate, sort, null, null, buffered);
        }


        /// <summary>
        /// 查询列表数据
        /// 可选择指定列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> GetSelectList<T>(string cols, object predicate = null, IList<ISort> sort = null,
            bool buffered = false) where T : class
        {
            ClassMapper<T> classMap = new ClassMapper<T>();
            List<IPropertyMap> listSelectProperties = new List<IPropertyMap>();
            foreach (string t in cols.Split(',').ToList())
            {
                var temp = classMap.Properties.First(p => p.ColumnName == t);
                if (temp != null)
                    listSelectProperties.Add(temp);
            }
            classMap.Properties = listSelectProperties;
            return DBSession.Connection.GetList<T>(predicate, sort, null, null, buffered);
        }

        /// <summary>
        /// 单表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="top"></param>
        /// <param name="cols"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public IEnumerable<T> GetSelectList<T>(int top, string cols, string where, string orderBy) where T : class
        {
            var tblName = string.Format("dbo.{0}", typeof(T).Name.Replace("Model", ""));
            string selectCols = "*";
            if (cols != "")
                selectCols = cols;

            string topStr = "";
            if (top > 0)
                topStr = "top " + top.ToString();
            string whereStr = "";
            if (where != "")
                whereStr += "where " + where;
            string orderByStr = "";
            if (orderBy != "")
                orderByStr = "order by " + orderBy;

            var sql = string.Format("SELECT  {0} {1} FROM {2} {3} {4}", topStr, selectCols, tblName, whereStr, orderByStr);

            IEnumerable<T> dataList = DBSession.Connection.Query<T>(sql);
            return dataList;
        }

        /// <summary>
        /// 根据指定字段获取内容
        /// by:willian date:2016-3-25
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="top"></param>
        /// <param name="cols"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public T GetById<T>(int id, string cols) where T : class
        {
            string where = "Id=" + id.ToString();
            if (cols == "")
                cols = "*";
            return GetSelectList<T>(1, cols, where, "Id").FirstOrDefault();
        }


        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Update<T>(Dictionary<string, object> keyValue, int id, IDbTransaction transaction = null) where T : class
        {
            string where = " Id=" + id.ToString();
            return Update<T>(keyValue, where, transaction);
        }

        /// <summary>
        /// 更新记录 自定义条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Update<T>(Dictionary<string, object> keyValue, string where, IDbTransaction transaction = null) where T : class
        {
            if (where == "")
                return false;
            var tblName = string.Format("dbo.{0}", typeof(T).Name.Replace("Model", ""));
            string sql = string.Format("update {0} set ", tblName);

            DynamicParameters dynamicParameters = new DynamicParameters();

            SqlParameter[] param = new SqlParameter[keyValue.Count];
            int i = 0;
            foreach (KeyValuePair<string, object> item in keyValue)
            {
                sql += string.Format("{0}=@{0},", item.Key);
                dynamicParameters.Add(item.Key, item.Value);

                //param[i] = new SqlParameter("@" + item.Key, item.Value);
                //i++;
            }
            sql = sql.TrimEnd(',');
            sql += " where " + where;
           // IEnumerable<T> dataList = DBSession.Connection.Query<T>(sql);
            return DBSession.Connection.Execute(sql, dynamicParameters, transaction) > 0;
        }

        /// <summary>
        /// 链表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="top"></param>
        /// <param name="cols"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public IEnumerable<TReturn> GetJoinList<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> map, int top, string tabs, string cols, string where, string orderBy) where TFirst : class
        {
            string selectCols = "*";
            if (cols != "")
                selectCols = cols;

            string topStr = "";
            if (top > 0)
                topStr = "top " + top.ToString();
            string whereStr = "";
            if (where != "")
                whereStr += "where " + where;
            string orderByStr = "";
            if (orderBy != "")
                orderByStr = "order by " + orderBy;

            var sql = string.Format("SELECT  {0} {1} FROM {2} {3} {4}", topStr, selectCols, tabs, whereStr, orderByStr);



            IEnumerable<TReturn> dataList = DBSession.Connection.Query<TFirst, TSecond, TReturn>(sql, map);
            return dataList;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allRowsCount"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> GetPageList<T>(int pageIndex, int pageSize, out long allRowsCount,
            object predicate = null, IList<ISort> sort = null, bool buffered = true) where T : class
        {
            if (sort == null)
            {
                sort = new List<ISort>();
            }
            IEnumerable<T> entityList = DBSession.Connection.GetPage<T>(predicate, sort, pageIndex, pageSize, null, null, buffered);
            allRowsCount = DBSession.Connection.Count<T>(predicate);
            return entityList;
        }




        /// <summary>
        /// 插入单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public dynamic Insert<T>(T entity, IDbTransaction transaction = null) where T : class
        {
            dynamic result = DBSession.Connection.Insert<T>(entity, transaction);
            return result;
        }

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Update<T>(T entity, IDbTransaction transaction = null) where T : class
        {
            bool isOk = DBSession.Connection.Update<T>(entity, transaction);
            return isOk;
        }

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int Delete<T>(dynamic primaryId, IDbTransaction transaction = null) where T : class
        {
            var entity = GetById<T>(primaryId);
            var obj = entity as T;
            int isOk = DBSession.Connection.Delete<T>(obj, transaction);
            return isOk;
        }

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns> 
        public int DeleteList<T>(object predicate = null, IDbTransaction transaction = null) where T : class
        {
            return DBSession.Connection.Delete<T>(predicate, transaction);
        }

        /// <summary>
        /// 批量插入功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        public bool InsertBatch<T>(IEnumerable<T> entityList, IDbTransaction transaction = null) where T : class
        {
            bool isOk = false;
            foreach (var item in entityList)
            {
                Insert<T>(item, transaction);
            }
            isOk = true;
            return isOk;
        }

        /// <summary>
        /// 批量更新（）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool UpdateBatch<T>(IEnumerable<T> entityList, IDbTransaction transaction = null) where T : class
        {
            bool isOk = false;
            foreach (var item in entityList)
            {
                Update<T>(item, transaction);
            }
            isOk = true;
            return isOk;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool DeleteBatch<T>(IEnumerable<dynamic> ids, IDbTransaction transaction = null) where T : class
        {
            bool isOk = false;
            foreach (var id in ids)
            {
                Delete<T>(id, transaction);
            }
            isOk = true;
            return isOk;
        }
    }
}