using System;

namespace OMSConsole.Dtos
{
    public class CustomLatest
    {
        public string MeterAddress { get; set; }
        public DateTime ReadingDate { get; set; }
        public string RawTelegram { get; set; }
    }
}
