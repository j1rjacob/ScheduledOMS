using DAOms;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OMSTaskScheduler.Util
{
    public static class ToBackup
    {
        public static void MoveMD()
        {
            if (!Directory.Exists(@"C:\JizHydrusBackup\"))
            {
                Directory.CreateDirectory(@"C:\JizHydrusBackup\");
            }

            using (var context = new JizanOMSContext())
            {
                List<tblGateway> gateway = new List<tblGateway>();

                var y = context.tblGateways;
                gateway = y.ToList();
                
                foreach (var g in gateway)
                {
                    if (Directory.Exists(@"C:\JizHydrusBackup\" + g.Name))
                    {   //Overwrite files if exist.
                        string[] files = System.IO.Directory.GetFiles(@"C:\JizFTP\" + g.Name);

                        foreach (string s in files)
                        {
                            var destFile = Path.Combine(@"C:\JizHydrusBackup\" + g.Name, Path.GetFileName(s));
                            File.Copy(s, destFile, true);
                        }
                    }
                    else
                    {   //Move Directory.
                        Directory.Move(@"C:\JizFTP\" + g.Name, @"C:\JizHydrusBackup\" + g.Name);
                    }
                }
            }
        }
    }
}
