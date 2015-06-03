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
using System.Runtime.Serialization;
#endregion

namespace SageFrame.Scheduler
{
    [DataContract]
    [Serializable]
    public class ScheduleHistoryView
    {
        [DataMember(Name = "RowTotal", Order = 1)]
        public System.Nullable<int> RowTotal { get; set; }



        [DataMember(Name = "ScheduleHistoryID", Order = 2)]
        public System.Nullable<int> ScheduleHistoryID { get; set; }

        [DataMember(Name = "ScheduleID", Order = 3)]
        public System.Nullable<int> ScheduleID { get; set; }

        [DataMember(Name = "StartDate", Order = 4)]
        public System.Nullable<DateTime> StartDate { get; set; }

        [DataMember(Name = "EndDate", Order = 5)]
        public System.Nullable<DateTime> EndDate { get; set; }

        [DataMember(Name = "Status", Order = 6)]
        public System.Nullable<bool> Status { get; set; }

        [DataMember(Name = "ReturnText", Order = 7)]
        public String ReturnText { get; set; }

        [DataMember(Name = "Server", Order = 8)]
        public string Server { get; set; }

        [DataMember(Name = "NextStart", Order = 9)]
        public System.Nullable<DateTime> NextStart { get; set; }
    }
}
