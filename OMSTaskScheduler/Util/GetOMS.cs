using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using DAOms;

namespace OMSTaskScheduler.Util
{
    public static class GetOMS
    {
        public static void OMSFolder()
        {
            //string[] folderName = Directory
            //                        .GetDirectories(@"C:\JizFTP",
            //                        "*",
            //                        SearchOption. AllDirectories);
            List<tblGateway> gateway = new List<tblGateway>();
            using (var context = new DAOms.JizanOMSContext())
            {
                var x = from j in context.tblGateways
                        select j;
                gateway = x.ToList();
            }
            foreach (var g in gateway)
            {
                string[] files = Directory
                    .GetFiles(@"C:\JizFTP\" + g.Name);
                foreach (var file in files)
                {
                    using (SqlConnection connection =
                        new SqlConnection(new SmartDB().Connection.ConnectionString))
                    {
                        connection.Open();
                        if (new DirectoryInfo(file).Name.Contains("GTW_OMS_RAW_") &&
                            !new DirectoryInfo(file).Name.Contains("lock"))
                        {
                            //Console.WriteLine(Path.GetFileName(Path.GetDirectoryName(file)));
                            //Console.WriteLine(new DirectoryInfo(file).Name);
                            //Read file
                            System.Data.DataTable newMeter = ReadFile(file);
                            InsertMeterBulkCopy(connection, newMeter);
                        }
                    }
                    Console.WriteLine(file);
                }
            }
            //return folderName;
        }

        private static System.Data.DataTable ReadFile(string filePath)
        {
            System.Data.DataTable newMeter = new System.Data.DataTable("tblRaw");

            DataColumn Id = new DataColumn();
            Id.DataType = Type.GetType("System.Guid");
            Id.ColumnName = "Id";
            newMeter.Columns.Add(Id);

            DataColumn MeterAddress = new DataColumn();
            MeterAddress.DataType = Type.GetType("System.String");
            MeterAddress.ColumnName = "MeterAddress";
            newMeter.Columns.Add(MeterAddress);

            DataColumn ReadingDate = new DataColumn();
            ReadingDate.DataType = Type.GetType("System.DateTime");
            ReadingDate.ColumnName = "ReadingDate";
            newMeter.Columns.Add(ReadingDate);

            DataColumn RawTelegram = new DataColumn();
            RawTelegram.DataType = Type.GetType("System.String");
            RawTelegram.ColumnName = "RawTelegram";
            newMeter.Columns.Add(RawTelegram);

            DataColumn GatewayId = new DataColumn();
            GatewayId.DataType = Type.GetType("System.String");
            GatewayId.ColumnName = "GatewayId";
            newMeter.Columns.Add(GatewayId);

            DataColumn[] keys = new DataColumn[1];
            keys[0] = Id;
            newMeter.PrimaryKey = keys;

            try
            {
                DataAccess.DataTable dt = DataAccess.DataTable.New.ReadCsv(filePath);

                // Query via the DataTable.Rows enumeration.
                var gateway = Path.GetFileName(Path.GetDirectoryName(filePath));
                DataRow rowMeter;
                foreach (Row row in dt.Rows)
                {
                    rowMeter = newMeter.NewRow();
                    rowMeter["Id"] = Guid.NewGuid();
                    rowMeter["MeterAddress"] = row["METER_ADDRESS"];
                    var datetimex = row["READING_DATE"].Split(null);
                    var cdate1 = datetimex[1].Split('/');
                    string neotimex = cdate1[1] + "/" + cdate1[0] + "/" + cdate1[2] + " " + datetimex[0];
                    rowMeter["ReadingDate"] = Convert.ToDateTime(neotimex);
                    rowMeter["RawTelegram"] = row["RAW_TELEGRAM"];
                    rowMeter["GatewayId"] = gateway;
                    newMeter.Rows.Add(rowMeter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Contact Admin: {ex.Message}", "Import");
            }

            newMeter.AcceptChanges();

            return newMeter;
        }

        private static void InsertMeterBulkCopy(SqlConnection connection, System.Data.DataTable meter)
        {
            using (SqlBulkCopy s = new SqlBulkCopy(connection))
            {
                s.DestinationTableName = "tblRaw";
                //s.ColumnMappings.Add("Id", "Id");
                s.ColumnMappings.Add("MeterAddress", "MeterAddress");
                s.ColumnMappings.Add("ReadingDate", "ReadingDate");
                s.ColumnMappings.Add("RawTelegram", "RawTelegram");
                s.ColumnMappings.Add("GatewayId", "GatewayId");
                try
                {
                    s.WriteToServer(meter);
                    Console.WriteLine($"Importing was successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Contact Admin: {ex.Message}", "Import");
                }
            }
        }

    }
}
