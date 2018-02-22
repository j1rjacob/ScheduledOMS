using System;

namespace OMSTaskScheduler.Util
{
    public static class Timex
    {
        public static string DBtoCSVDateConvert(this string DBDate)
        {
            var dbDate = DBDate.Split(null);
            var datex = DateTime.Parse(dbDate[0]);
            var time24 = DateTime.Parse(dbDate[1] + " " + dbDate[2]);
            return time24.ToString("HH:mm:ss") + " " + datex.ToString("dd/MM/yyyy");
        }
        public static DateTime CSVtoDateDateConvert(this string CSVDate)
        {
            var dbDate = CSVDate.Split(null);
            var datex = dbDate[1].Split('/');
            return Convert.ToDateTime(datex[1] + "/" + datex[0] + "/" + datex[2] + " " + dbDate[0]);
        }

        public static string CurrentTime()
        {
            return "GTW_OMS_RAW_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss");
        }
    }
}
