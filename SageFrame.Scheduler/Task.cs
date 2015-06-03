#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
#endregion

namespace SageFrame.Scheduler
{
    public abstract class TaskBase
    {
        public event TaskStarted ScheduleStarted;
        public event TaskProcessing ScheduleProcessing;
        public event TaskCompleted ScheduleCompleted;
        public event TaskErrored ScheduleErrored;
        public ScheduleHistory TaskRecordItem { get; set; }
        public string Status { get; set; }
        public abstract void Execute();

        public TaskBase()
        {
            this.TaskRecordItem = new ScheduleHistory();
        }

        public TaskBase(ScheduleHistory scheduleHistory)
        {
            this.TaskRecordItem = scheduleHistory;
        }

        public void Started()
        {
            if (ScheduleStarted != null)
            {
                try
                {
                    ScheduleStarted(this);
                }
                catch (Exception)
                {

                }
            }
        }


        public void Processing()
        {
            if (ScheduleProcessing != null)
            {
                ScheduleProcessing(this);
            }
        }

        public void Completed()
        {

            if (ScheduleCompleted != null)
            {
                ScheduleCompleted(this);
            }
        }


        public void Error(ref Exception exc)
        {
            if (ScheduleErrored != null)

                ScheduleErrored(this, exc);
        }


    }
}
