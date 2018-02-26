using OMSConsole.Core;
using System;
using System.Data;
using System.Data.SqlClient;

namespace OMSConsole.Util
{
    public static class Exec
    {
        public static bool LatestReading(string gatewayId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SmartDB.ConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("LATEST_METER_READING", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GatewayId", gatewayId);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
