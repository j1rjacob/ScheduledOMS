using System;

namespace OMSTaskScheduler.Dtos
{
    public class HydrusData
    {
        public Int64 RowNum { get; set; }
        public string AccountNo { get; set; }
        public string AreaNo { get; set; }
        public string AreaName { get; set; }
        public string MeterAddress { get; set; }
        public string RawTelegram { get; set; }
        public Nullable<DateTime> ReadingDate { get; set; }
    }
}
