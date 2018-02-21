using Quartz;
using System;

namespace OMSTaskScheduler
{
    public class SaveDBJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            //GetOMS
            //MultipleSave
            //ProcessLatestReadings
            //Move CSV to backup directory
            throw new NotImplementedException();
        }
    }
}
