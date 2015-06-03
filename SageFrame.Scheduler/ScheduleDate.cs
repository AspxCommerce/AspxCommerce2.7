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
#endregion

namespace SageFrame.Scheduler
{
    public class ScheduleDate
    {
        public int ScheduleID { get; set; }
        public DateTime Schedule_Date { get; set; }
        public bool IsExecuted { get; set; }
    }
}
