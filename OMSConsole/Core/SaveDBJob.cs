using Quartz;
using System;

namespace OMSConsole.Core
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
