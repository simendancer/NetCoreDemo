using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Transactions
{
    public class TransactionScope : IDisposable
    {

        private Transaction transaction = Transaction.Current;
        public bool Completed { get; private set; }

        public TransactionScope(string connKey = "ConnectionString", IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (null == transaction)
            {
                IDbConnection connection = SqlConnectionFactory.CreateSqlConnection(connKey);
                connection.Open();
                IDbTransaction dbTransaction = connection.BeginTransaction(isolationLevel);
                Transaction.Current = new CommittableTransaction(dbTransaction);
            }
            else
            {
                Transaction.Current = transaction.DependentClone();
            }
        }


        public TransactionScope(IDBSession dbSession, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (null == transaction)
            {
                IDbConnection connection = dbSession.Connection;
                connection.Open();
                IDbTransaction dbTransaction = connection.BeginTransaction(isolationLevel);
                Transaction.Current = new CommittableTransaction(dbTransaction);
            }
            else
            {
                Transaction.Current = transaction.DependentClone();
            }
        }

        public void Complete()
        {
            this.Completed = true;
        }
        public void Dispose()
        {
            Transaction current = Transaction.Current;
            Transaction.Current = transaction;
            if (!this.Completed)
            {
                current.Rollback();
            }
            CommittableTransaction committableTransaction = current as CommittableTransaction;
            if (null != committableTransaction)
            {
                if (this.Completed)
                {
                    committableTransaction.Commit();
                }
                committableTransaction.Dispose();
            }
        }
    }

}
