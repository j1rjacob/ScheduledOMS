using OMSConsole.Core;
using System;
using System.Data;
using System.Data.SqlClient;


namespace OMSConsole.Util
{
    public static class Get
    {
        public static DataSet LatestReading(string gatewayId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(SmartDB.ConnectionString());
                SqlCommand cmd = new SqlCommand();
                
                //SqlDataAdapter da = new SqlDataAdapter(
                //    "SELECT DISTINCT MeterAddress, ReadingDate, RawTelegram FROM tblLatest WHERE GatewayId="+ ,+'"
                //    connection);
                cmd.CommandText = "SELECT DISTINCT MeterAddress, ReadingDate, RawTelegram FROM tblLatest WHERE GatewayId=@GatewayId";
                cmd.Parameters.AddWithValue("@GatewayId", gatewayId);
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "MeterReading");
                return ds;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            //DataRelation relation = ds.Relations.Add("CustOrders",
            //    ds.Tables["Customers"].Columns["CustomerID"],
            //    ds.Tables["Orders"].Columns["CustomerID"]);

            //foreach (DataRow pRow in ds.Tables["Customers"].Rows)
            //{
            //    Console.WriteLine(pRow["CustomerID"]);
            //    foreach (DataRow cRow in pRow.GetChildRows(relation))
            //        Console.WriteLine("\t" + cRow["OrderID"]);
            //}
        }
    }
}
