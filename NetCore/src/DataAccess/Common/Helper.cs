using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Common
{
    public static class Helper
    {

        public static IDBSession GetPerHttpRequestDBSession(string connkey = "ConnectionString")
        {
            DataAccess.IDatabase db = new DataAccess.Database(connkey);
            IDBSession dbSession = new DBSessionBase(db);
            return dbSession;
        }


        //public static void DisposePerHttpRequestDBSession()
        //{
        //    string IDBSession_Keys = (HttpContext.Current.Items["IDBSession_Keys"] ?? "").ToString();
        //    if (IDBSession_Keys.Length > 0)
        //    {
        //        foreach (string connkey in IDBSession_Keys.Split(','))
        //        {
        //            if (!string.IsNullOrEmpty(connkey))
        //            {
        //                IDBSession dbSession = HttpContext.Current.Items["IDBSession_" + connkey] as IDBSession;
        //                if (dbSession != null)
        //                {
        //                    dbSession.Dispose();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}

