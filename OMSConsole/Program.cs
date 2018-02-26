using Common.Logging;
using OMSConsole.Core;
using OMSConsole.Util;
using Quartz;
using Quartz.Impl;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace OMSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(SmartDB.ConnectionString());
            if (connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Open");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            GetOMS.OMSFolder();
            GetLatest();
            ToBackup.MoveMD();

            Console.ReadLine();
        }
        private static ILog Log = LogManager.GetCurrentClassLogger();
        private void LoadTaskScheduler()
        {

            try
            {
                // construct a scheduler factory
                ISchedulerFactory schedFact = new StdSchedulerFactory();

                // get a scheduler
                IScheduler sched = schedFact.GetScheduler();
                sched.Start();

                IJobDetail job = JobBuilder.Create<SaveDBJob>()
                    .WithIdentity("myJob", "group1")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s.WithIntervalInHours(1)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 30))
                    )
                    .Build();

                sched.ScheduleJob(job, trigger);
            }
            catch (ArgumentException e)
            {
                Log.Error(e);
            }
        }

        private static void GetLatest()
        {
            if (!Directory.Exists(@"C:\JizHydrusLatest\"))
            {
                Directory.CreateDirectory(@"C:\JizHydrusLatest\");
            }
            
            //try
            //{
                var gateway = Gateway.GatewayList();

                //delete all latest here
                var result = Delete.LatestReading();
                Console.WriteLine($"Delete {result}");

                foreach (var g in gateway)
                {
                    //var x = context.GetLatestMeterReading(g.Name);
                    Exec.LatestReading(g);
                    //Console.WriteLine(x);
                    //var extractMeter = context.Database
                    //    .SqlQuery<CustomLatest>("SELECT DISTINCT MeterAddress, ReadingDate, RawTelegram FROM tblLatest WHERE GatewayId=@GatewayId",
                    //        new SqlParameter("@GatewayId", g.Name));

                    var extractMeter = Get.LatestReading(g);
                    var fileName = Timex.CurrentTime();
                    string path2 = @"C:\JizHydrusLatest\" + g + "\\" + fileName + ".csv";
                    if (!Directory.Exists(@"C:\JizHydrusLatest\" + g + "\\"))
                    {
                        Directory.CreateDirectory(@"C:\JizHydrusLatest\" + g + "\\");
                    }
                    using (StreamWriter writer = new StreamWriter(path2, false))
                    {
                        string[] separator = { "," };
                        var filecontents = new StringBuilder();
                        filecontents.Append("METER_ADDRESS, READING_DATE, PACKET\r\n");

                        string extract = null;
                        foreach (DataRow em in extractMeter.Tables["MeterReading"].Rows)
                        {
                            extract += em["MeterAddress"] + ","; //Serial
                            extract += em["ReadingDate"]
                                           .ToString()
                                           .DBtoCSVDateConvert() + ",";//Date
                            extract += em["RawTelegram"];  //Packet
                            extract += "\r\n";
                        }
                        filecontents.Append(extract);
                        writer.Write(filecontents.ToString());
                        writer.Flush();
                        writer.Close();
                    }
                }
                Console.WriteLine("Extracted");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //    throw;
            //}
        }
    }
}
