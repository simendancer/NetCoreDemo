using DapperExtensions;
using DataAccess;
using Dal;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bll
{
    public abstract class ServiceBase : RepositoryServiceBase, IDisposable
    {
        public IList<IDisposable> DisposableObjects { get; private set; }
        public ServiceBase()
        {
            SetDBSession(Helper.CreateDBSession(CommonConfig.DefaultConnKey));
            DisposableObjects = new List<IDisposable>();
        }
        public ServiceBase(IDBSession dbSession)
            : base(dbSession)
        {
            DisposableObjects = new List<IDisposable>();
        }
        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (null != disposable)
            {
                DisposableObjects.Add(disposable);
            }
        }
        public void Dispose()
        {
            foreach (IDisposable obj in DisposableObjects)
            {
                if (null != obj)
                {
                    obj.Dispose();
                }
            }
        }

        /// <summary>
        /// 检测关键词，防止SQL注入
        /// </summary>
        /// <param name="sqlValue">需要检测的关键词</param>
        /// <returns></returns>
        public static string CheckSqlValue(string sqlValue)
        {
            string reStr = sqlValue;
            if (reStr == null)
            {
                reStr = "";
            }
            reStr = reStr.Replace("'", "''");
            return reStr;
        }

        /// <summary>
        /// 检测Like字符串,防止注入和关键词匹配(替换 '_ % )
        /// </summary>
        /// <param name="keyword">需要检测的关键词</param>
        /// <returns></returns>
        public static string CheckSqlKeyword(string keyword)
        {
            string reStr = keyword;
            if (reStr == null)
            {
                reStr = "";
            }
            reStr = reStr.Replace("'", "''");
            reStr = reStr.Replace("[", "[[]");
            reStr = reStr.Replace("%", "[%]");
            reStr = reStr.Replace("_", "[_]");
            return reStr;
        }

        /// <summary>
        /// 检测全文索引,防止注入和关键词匹配(替换 '_ % )
        /// </summary>
        /// <param name="keyword">需要检测的关键词</param>
        /// <returns></returns>
        public static string CheckSqlIndex(string keyword)
        {
            string reStr = keyword;
            if (reStr == null)
            {
                reStr = "";
            }
            reStr = reStr.Replace("'", "''");
            reStr = reStr.Replace("[", "[[]");

            //reStr = reStr.Replace("_", "[_]");
            reStr = reStr.Replace("\"", "''''");
            reStr = reStr.Replace(",", "");
            return reStr;
        }

        /// <summary>
        /// 检测数据库字段名或表名
        /// </summary>
        /// <param name="fieldName">要检测的字段名或表名</param>
        /// <returns></returns>
        public static bool CheckSqlField(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(fieldName, @"^[a-zA-Z0-9_\.\,]+$");
            }
        }



    }
}
