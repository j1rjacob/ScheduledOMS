using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OMSTaskScheduler.Core
{
    public abstract class SqlHelper
    {
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public static string MyConnectionString
        {
            get
            {
                string result;
                for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
                {
                    bool flag = ConfigurationManager.ConnectionStrings[i].Name.Contains("DefaultConnection");
                    if (flag)
                    {
                        result = ConfigurationManager.ConnectionStrings[i].ConnectionString;
                        return result;
                    }
                }
                result = null;
                return result;
            }
        }

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            int result;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlHelper.PrepareCommand(sqlCommand, sqlConnection, null, cmdType, cmdText, commandParameters);
                int num = sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                result = num;
            }
            return result;
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlHelper.PrepareCommand(sqlCommand, connection, null, cmdType, cmdText, commandParameters);
            int result = sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
            return result;
        }

        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlHelper.PrepareCommand(sqlCommand, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int result = sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
            return result;
        }

        public static SqlDataReader ExecuteReader(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlHelper.PrepareCommand(sqlCommand, trans.Connection, trans, cmdType, cmdText, commandParameters);
            SqlDataReader result = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            return result;
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlDataReader result;
            try
            {
                SqlHelper.PrepareCommand(sqlCommand, sqlConnection, null, cmdType, cmdText, commandParameters);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                sqlCommand.Parameters.Clear();
                result = sqlDataReader;
            }
            catch
            {
                sqlConnection.Close();
                throw;
            }
            return result;
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            DataSet dataSet = new DataSet();
            try
            {
                SqlHelper.PrepareCommand(sqlCommand, sqlConnection, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataSet);
                sqlCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
            return dataSet;
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            object result;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlHelper.PrepareCommand(sqlCommand, sqlConnection, null, cmdType, cmdText, commandParameters);
                object obj = sqlCommand.ExecuteScalar();
                sqlCommand.Parameters.Clear();
                result = obj;
            }
            return result;
        }

        public static object ExecuteScalar(SqlTransaction trn, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlHelper.PrepareCommand(sqlCommand, trn.Connection, null, cmdType, cmdText, commandParameters);
            object result = sqlCommand.ExecuteScalar();
            sqlCommand.Parameters.Clear();
            return result;
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlHelper.PrepareCommand(sqlCommand, connection, null, cmdType, cmdText, commandParameters);
            object result = sqlCommand.ExecuteScalar();
            sqlCommand.Parameters.Clear();
            return result;
        }

        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            SqlHelper.parmCache[cacheKey] = commandParameters;
        }

        public static SqlParameter[] GetParameters(string cacheKey)
        {
            SqlConnection sqlConnection = new SqlConnection(SqlHelper.MyConnectionString);
            SqlCommand sqlCommand = new SqlCommand(cacheKey, sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlConnection.Open();
            SqlCommandBuilder.DeriveParameters(sqlCommand);
            SqlParameter[] array = new SqlParameter[sqlCommand.Parameters.Count - 1];
            int i = 1;
            int count = sqlCommand.Parameters.Count;
            while (i < count)
            {
                array[i - 1] = (SqlParameter)((ICloneable)sqlCommand.Parameters[i]).Clone();
                i++;
            }
            SqlHelper.CacheParameters(cacheKey, array);
            return array;
        }

        public static SqlParameter[] GetParameters(SmartDB dbInstance, string cacheKey)
        {
            bool transactionControl = dbInstance.TransactionControl;
            SqlCommand sqlCommand;
            if (transactionControl)
            {
                sqlCommand = new SqlCommand(cacheKey, dbInstance.Connection, dbInstance.Transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };
            }
            else
            {
                sqlCommand = new SqlCommand(cacheKey, dbInstance.Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
            }
            bool flag = dbInstance.Connection.State == ConnectionState.Closed;
            if (flag)
            {
                dbInstance.Connection.Open();
            }
            SqlCommandBuilder.DeriveParameters(sqlCommand);
            SqlParameter[] array = new SqlParameter[sqlCommand.Parameters.Count - 1];
            int i = 1;
            int count = sqlCommand.Parameters.Count;
            while (i < count)
            {
                array[i - 1] = (SqlParameter)((ICloneable)sqlCommand.Parameters[i]).Clone();
                i++;
            }
            SqlHelper.CacheParameters(cacheKey, array);
            return array;
        }

        public static SqlParameter[] GetParameters(string connectionString, string cacheKey)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(cacheKey, sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlConnection.Open();
            SqlCommandBuilder.DeriveParameters(sqlCommand);
            SqlParameter[] array = new SqlParameter[sqlCommand.Parameters.Count - 1];
            int i = 1;
            int count = sqlCommand.Parameters.Count;
            while (i < count)
            {
                array[i - 1] = (SqlParameter)((ICloneable)sqlCommand.Parameters[i]).Clone();
                i++;
            }
            SqlHelper.CacheParameters(cacheKey, array);
            return array;
        }

        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] array = (SqlParameter[])SqlHelper.parmCache[cacheKey];
            bool flag = array == null;
            SqlParameter[] result;
            if (flag)
            {
                result = null;
            }
            else
            {
                SqlParameter[] array2 = new SqlParameter[array.Length];
                int i = 0;
                int num = array.Length;
                while (i < num)
                {
                    array2[i] = (SqlParameter)((ICloneable)array[i]).Clone();
                    i++;
                }
                result = array2;
            }
            return result;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            bool flag = conn.State != ConnectionState.Open;
            if (flag)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            bool flag2 = trans != null;
            if (flag2)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            bool flag3 = cmdParms != null;
            if (flag3)
            {
                for (int i = 0; i < cmdParms.Length; i++)
                {
                    SqlParameter value = cmdParms[i];
                    cmd.Parameters.Add(value);
                }
            }
        }
    }
}
