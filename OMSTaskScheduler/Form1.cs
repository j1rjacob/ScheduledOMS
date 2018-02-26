using Common.Logging;
using DAOms;
using OMSTaskScheduler.Core;
using OMSTaskScheduler.Dtos;
using OMSTaskScheduler.Util;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OMSTaskScheduler
{
    public partial class FormTaskScheduler : Form
    {
        public FormTaskScheduler()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //LoadTaskScheduler();
            GetOMS.OMSFolder();
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

        private void ButtonLatest_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@"C:\JizHydrusLatest\"))
            {
                Directory.CreateDirectory(@"C:\JizHydrusLatest\");
            }

            using (var context = new JizanOMSContext())
            {
                try
                {
                    List<tblGateway> gateway = new List<tblGateway>();

                    var y = from j in context.tblGateways
                            select j;
                    gateway = y.ToList();
                    //delete all latest here
                    var result = context.Database.ExecuteSqlCommand("delete from tblLatest");
                    Console.WriteLine($"Delete {result}");
                    foreach (var g in gateway)
                    {
                        var x = context.GetLatestMeterReading(g.Name);
                        //Console.WriteLine(x);
                        var extractMeter = context.Database
                            .SqlQuery<CustomLatest>("SELECT DISTINCT MeterAddress, ReadingDate, RawTelegram FROM tblLatest WHERE GatewayId=@GatewayId", 
                                       new SqlParameter("@GatewayId", g.Name));
                        var fileName = Timex.CurrentTime();
                        string path2 = @"C:\JizHydrusLatest\" + g.Name + "\\"+ fileName +".csv";
                        if (!Directory.Exists(@"C:\JizHydrusLatest\" + g.Name + "\\"))
                        {
                            Directory.CreateDirectory(@"C:\JizHydrusLatest\" + g.Name + "\\");
                        }
                        using (StreamWriter writer = new StreamWriter(path2, false))
                        {
                            string[] separator = { "," };
                            var filecontents = new StringBuilder();
                            filecontents.Append("METER_ADDRESS, READING_DATE, PACKET\r\n");

                            string extract = null;
                            foreach (var em in extractMeter)
                            {
                                extract += em.MeterAddress + ","; //Serial
                                extract += em.ReadingDate
                                             .ToString()
                                             .DBtoCSVDateConvert() + ",";//Date
                                extract += em.RawTelegram;  //Packet
                                extract += "\r\n";
                            }
                            filecontents.Append(extract);
                            writer.Write(filecontents.ToString());
                            writer.Flush();
                            writer.Close();
                        }
                    }
                    Console.WriteLine("Extracted");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        private void ButtonBackup_Click(object sender, EventArgs e)
        {
            ToBackup.MoveMD();
        }
    }
}
