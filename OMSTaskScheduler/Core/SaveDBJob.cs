using Quartz;
using System;

namespace OMSTaskScheduler.Core
{
    public class SaveDBJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
               FormTaskScheduler.ExportHydrusData();
            }
            catch (Exception)
            {
                return;
            }
            //GetOMS
            //GetLatest
            //ProcessLatestReadings
            //Move CSV to backup directory
            //throw new NotImplementedException();
        }
    }
}
