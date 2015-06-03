using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace SageFrame.Scheduler
{
     [DataContract]
     [Serializable]
   public class ScheduleHistory :Schedule
   {
   

       [DataMember(Name = "_ScheduleHistoryID", Order = 2)]
        public int? ScheduleHistoryID { get; set; }

       [DataMember(Name = "_HistoryStartDate", Order = 3)]
        public DateTime? HistoryStartDate { get; set; }

       [DataMember(Name = "_HistoryEndDate", Order = 4)]
        public DateTime? HistoryEndDate { get; set; }

       [DataMember(Name = "_Status", Order = 5)]
        public bool? Status{ get; set; }

       [DataMember(Name = "_ReturnText", Order = 6)]
        public String ReturnText{ get; set; }

       [DataMember(Name = "_RowTotal", Order = 7)]
        public string Server { get; set; }
  
        public ScheduleHistory() { }
        public ScheduleHistory(Schedule objSchedule)
        {
            this.AttachToEvent = objSchedule.AttachToEvent;
            this.CatchUpEnabled = objSchedule.CatchUpEnabled;
            this.EveryMin = objSchedule.EveryMin;
            this.EveryHours = objSchedule.EveryHours;
            this.RunningMode = objSchedule.RunningMode;
            this.NextStart = objSchedule.NextStart;
            this.ObjectDependencies = objSchedule.ObjectDependencies;
            this.ProcessGroup = objSchedule.ProcessGroup;
            //this.RetainHistoryNum = objSchedule.RetainHistoryNum;
            this.RetryTimeLapse = objSchedule.RetryTimeLapse;
            this.RetryFrequencyUnit = objSchedule.RetryFrequencyUnit;
            this.ScheduleID = objSchedule.ScheduleID;
            //this.ScheduleSource = objSchedule.ScheduleSource;
            this.ThreadID = objSchedule.ThreadID;
            //this.TimeLapse = objSchedule.TimeLapse;
            //this.TimeLapseMeasurement = objSchedule.TimeLapseMeasurement;
            this.RepeatDays = objSchedule.RepeatDays;
            this.StartDate = objSchedule.StartDate;
            this.StartHour = objSchedule.StartHour;
            this.StartMin = objSchedule.StartMin;
            this.EndDate = objSchedule.EndDate;
            this.IsEnable = objSchedule.IsEnable;
            this.FullNamespace = objSchedule.FullNamespace;
            this.Servers = objSchedule.Servers;
            this.ScheduleName= objSchedule.ScheduleName;
            this.ScheduleHistoryID = null;
            this.HistoryStartDate = null;
            this.HistoryEndDate = null;
            this.Status = null;
            this.ReturnText = string.Empty;
            this.Server = string.Empty;
           

        }


    }
}
