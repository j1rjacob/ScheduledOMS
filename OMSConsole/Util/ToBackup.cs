using OMSConsole.Core;
using System.IO;

namespace OMSConsole.Util
{
    public static class ToBackup
    {
        public static void MoveMD()
        {
            if (!Directory.Exists(@"C:\JizHydrusBackup\"))
            {
                Directory.CreateDirectory(@"C:\JizHydrusBackup\");
            }
            
            var gateway = Gateway.GatewayList();

            foreach (var g in gateway)
                {
                    if (Directory.Exists(@"C:\JizHydrusBackup\" + g))
                    {   //Overwrite files if exist.
                        string[] files = System.IO.Directory.GetFiles(@"C:\JizFTP\" + g);

                        foreach (string s in files)
                        {
                            var destFile = Path.Combine(@"C:\JizHydrusBackup\" + g, Path.GetFileName(s));
                            File.Copy(s, destFile, true);
                        }
                    }
                    else
                    {   //Move Directory.
                        Directory.Move(@"C:\JizFTP\" + g, @"C:\JizHydrusBackup\" + g);
                    }
                }
        }
    }
}
