using OMSConsole.Core;
using System;
using System.Data.SqlClient;

namespace OMSConsole.Util
{
    public static class Delete
    {
        public static bool LatestReading()
        {
            try
            {
                SqlConnection connection = new SqlConnection(SmartDB.ConnectionString());
                SqlCommand command;
                
                command = new SqlCommand(
                    "DELETE * FROM tblLatest",
                    connection);
                command.ExecuteNonQuery();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
