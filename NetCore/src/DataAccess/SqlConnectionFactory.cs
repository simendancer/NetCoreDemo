using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DataAccess
{
    public class SqlConnectionFactory
    {
        private static IConfiguration Configuration { get; set; }
        public SqlConnectionFactory(IConfiguration configuration)
        {
            if (configuration == null)
                throw new NullReferenceException("IConfiguration为空");
            Configuration = configuration;
        }

        public static IDbConnection CreateSqlConnection(string strKey)
        {
            IDbConnection connection = null;
            string strConn = Configuration[strKey];
            if (string.IsNullOrEmpty(strConn))
            {
                throw new Exception(strKey + "连接字符串未识别");
            }
            connection = new System.Data.SqlClient.SqlConnection(strConn);
            return connection;
        }

        public static IDBHelper GetDBHelper()
        {
            return new SqlDBHelper.DBAdaptor();
        }

        public static List<IDataParameter> ConvertToDbParameter(Dictionary<string, DbParameter> parmList)
        {
            List<IDataParameter> dbParamList = new List<IDataParameter>();
            foreach (var item in parmList)
            {
                dbParamList.Add(ConvertToIDataParameter(item.Value.ParameterName, item.Value.Value));
            }
            return dbParamList;
        }

        public static IDataParameter ConvertToIDataParameter(string parameterName, object value)
        {
            return new System.Data.SqlClient.SqlParameter(parameterName, value);
        }
    }
}
