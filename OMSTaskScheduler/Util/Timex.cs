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
    }
}
