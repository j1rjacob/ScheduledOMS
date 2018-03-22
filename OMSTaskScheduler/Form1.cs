using Common.Logging;
using DAOms;
using MetroFramework.Forms;
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

namespace OMSTaskScheduler
{
    public partial class FormTaskScheduler : MetroForm
    {
        public FormTaskScheduler()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Console.SetOut(new ConsoleWriter(richTextBoxDebug));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (metroCheckBoxAuto.Checked)
            {
                LoadTaskScheduler();
            }
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
                        s
                        //.WithIntervalInHours(1)
                        .WithIntervalInMinutes(62)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 00))
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
            GetLatest();
        }

        public static void GetLatest()
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
                    Console.WriteLine($"Delete {result} from Latest Table");
                    foreach (var g in gateway)
                    {
                        var x = context.GetLatestMeterReading(g.Name);
                        //Console.WriteLine(x);
                        var extractMeter = context.Database
                            .SqlQuery<CustomLatest>(
                                "SELECT DISTINCT MeterAddress, ReadingDate, RawTelegram FROM tblLatest WHERE GatewayId=@GatewayId",
                                new SqlParameter("@GatewayId", g.Name));
                        var fileName = Timex.CurrentTime();
                        string path2 = @"C:\JizHydrusLatest\" + g.Name + "\\" + fileName + ".csv";
                        if (!Directory.Exists(@"C:\JizHydrusLatest\" + g.Name + "\\"))
                        {
                            Directory.CreateDirectory(@"C:\JizHydrusLatest\" + g.Name + "\\");
                        }
                        using (StreamWriter writer = new StreamWriter(path2, false))
                        {
                            string[] separator = {","};
                            var filecontents = new StringBuilder();
                            filecontents.Append("METER_ADDRESS, READING_DATE, PACKET\r\n");

                            string extract = null;
                            foreach (var em in extractMeter)
                            {
                                extract += em.MeterAddress + ","; //Serial
                                extract += em.ReadingDate
                                               .ToString()
                                               .DBtoCSVDateConvert() + ","; //Date
                                extract += em.RawTelegram; //Packet
                                extract += "\r\n";
                            }
                            filecontents.Append(extract);
                            writer.Write(filecontents.ToString());
                            writer.Flush();
                            writer.Close();
                        }
                    }
                    //MessageBox.Show("Latest record/s extracted.");
                    Console.WriteLine("Latest record/s extracted.");
                    //Console.WriteLine("Extracted");
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        private void ButtonBackup_Click(object sender, EventArgs e)
        {
            try
            {
                ToBackup.MoveMD();
                Console.WriteLine("Backup completed.");
            }
            catch (Exception)
            {
                return;
            }
            
        }

        private void ButtonMigrate_Click(object sender, EventArgs e)
        {
            try
            {
                GetOMS.OMSFolder();
                //MessageBox.Show("Record/s transfer completed.");
                Console.WriteLine("Record/s transfer completed.");
            }
            catch (Exception)
            {
                return;
            }
            
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            ExportHydrusData();
        }

        public static void ExportHydrusData()
        {
            using (var context = new JizanOMSContext())
            {
                try
                {
                    GetOMS.OMSFolder();
                    GetLatest();
                    ToBackup.MoveMD();

                    //string strPath = Environment.GetFolderPath(
                    //    System.Environment.SpecialFolder.DesktopDirectory);
                    string strPath = @"C:\JizHydrusLatest\";

                    var extractMeter1 = context.Database
                        .SqlQuery<HydrusData>("SELECT * FROM viewLatestHydrusData WHERE ReadingDate IS NOT NULL");
                    var extractMeter2 = context.Database
                        .SqlQuery<HydrusData>("SELECT * FROM viewLatestHydrusData WHERE ReadingDate IS NULL");

                    var fileName = Timex.CurrentTime();

                    string path2 = strPath + "\\" + "HYDRUS_DATA_" + fileName + ".csv";

                    using (StreamWriter writer = new StreamWriter(path2, false))
                    {
                        string[] separator = {","};
                        var filecontents = new StringBuilder();
                        filecontents.Append("#, Account No, Area No, Area Name, Serial No, RAW_TELEGRAM, READING_DATE\r\n");

                        string extract = null;
                        foreach (var em in extractMeter1)
                        {
                            extract += em.RowNum + ","; //#
                            extract += em.AccountNo + ","; //AccountNo
                            extract += em.AreaNo + ","; //AreaNo
                            extract += em.AreaName + ","; //AreaName
                            extract += em.MeterAddress + ","; //SerialNo
                            extract += em.RawTelegram + ","; //Packet
                            extract += em.ReadingDate
                                .ToString()
                                .DBtoCSVDateConvert(); //Date
                            extract += "\r\n";
                        }

                        foreach (var em in extractMeter2)
                        {
                            extract += em.RowNum + ","; //#
                            extract += em.AccountNo + ","; //AccountNo
                            extract += em.AreaNo + ","; //AreaNo
                            extract += em.AreaName + ","; //AreaName
                            extract += em.MeterAddress + ","; //SerialNo
                            extract += em.RawTelegram + ","; //Packet
                            extract += em.ReadingDate; //Date
                            extract += "\r\n";
                        }

                        filecontents.Append(extract);
                        writer.Write(filecontents.ToString());
                        writer.Flush();
                        writer.Close();
                    }
                    //Console.WriteLine("Extracted Hydrus Data");
                    Console.WriteLine("Extracted Hydrus Data in JizHydrusLatest Folder.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    //return;
                }
            }
        }
        
        private void metroTileCopy_Click(object sender, EventArgs e)
        {
            GetOMS.OMSFolder();
        }

        private void metroTileLatest_Click(object sender, EventArgs e)
        {
            GetLatest();
        }

        private void metroTileBackup_Click(object sender, EventArgs e)
        {
            ToBackup.MoveMD();
        }

        private void metroTileExport_Click(object sender, EventArgs e)
        {
            ExportHydrusData();
        }

        private void metroCheckBoxAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBoxAuto.Checked)
            {
                LoadTaskScheduler();
            }
        }
    }
}
