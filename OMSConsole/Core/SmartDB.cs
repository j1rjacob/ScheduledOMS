using System.Configuration;

namespace OMSConsole.Core
{
    public static class SmartDB
    {
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
