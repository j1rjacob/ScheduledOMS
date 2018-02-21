using System.Data;
using System.Data.SqlClient;

namespace OMSTaskScheduler
{
    public class SmartDB
    {
        private SqlConnection sqlConn;

        private SqlTransaction sqlTrans;

        private bool transactionControl = false;

        public SqlTransaction Transaction
        {
            get
            {
                return this.sqlTrans;
            }
            set
            {
                this.sqlTrans = value;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return this.sqlConn;
            }
            set
            {
                this.sqlConn = value;
            }
        }

        public bool TransactionControl
        {
            get
            {
                return this.transactionControl;
            }
            set
            {
                this.transactionControl = value;
            }
        }

        public SmartDB()
        {
            this.sqlConn = new SqlConnection(SqlHelper.MyConnectionString);
        }

        public SmartDB(string server, string database, string uid, string pwd)
        {
            this.sqlConn = new SqlConnection();
            this.sqlConn.ConnectionString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Connect Timeout=6000; pooling=True; Max Pool Size=200", new object[]
            {
                server,
                database,
                uid,
                pwd
            });
        }

        public void BeginTransaction()
        {
            this.TransactionControl = true;
            bool flag = this.sqlTrans == null || this.sqlTrans.Connection == null;
            if (flag)
            {
                bool flag2 = this.sqlConn.State != ConnectionState.Open;
                if (flag2)
                {
                    this.sqlConn = this.Connection;
                    this.sqlConn.Open();
                }
                this.sqlTrans = this.sqlConn.BeginTransaction();
            }
        }

        public void RollbackTransaction()
        {
            bool flag = this.sqlTrans != null && this.sqlTrans.Connection != null;
            if (flag)
            {
                this.sqlTrans.Rollback();
                this.sqlConn.Close();
            }
            this.TransactionControl = false;
        }

        public void CommitTransaction()
        {
            bool flag = this.sqlTrans != null && this.sqlTrans.Connection != null;
            if (flag)
            {
                this.sqlTrans.Commit();
                this.sqlConn.Close();
            }
            this.TransactionControl = false;
        }
    }
}
