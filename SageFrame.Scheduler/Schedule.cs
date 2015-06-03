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
    //[KnownType(typeof(ScheduleHistory))]
    [Serializable]
    public class Schedule
    {

        [DataMember(Name="ScheduleID",Order=1)]
        public int ScheduleID { get; set; }

        [DataMember(Name="ScheduleName",Order=2)]
        public string ScheduleName { get; set; }

        [DataMember(Name="FullNamespace",Order=3)]
        public string FullNamespace { get; set; }

        [DataMember(Name="StartDate",Order=4)]
        public string StartDate { get; set; }

        [DataMember(Name="EndDate",Order=5)]
        public string EndDate { get; set; }

        [DataMember(Name="StartHour",Order=6)]
        public int StartHour { get; set; }

        [DataMember(Name="StartMin",Order=7)]
        public int StartMin { get; set; }
        [DataMember]
        public int RepeatWeeks { get; set; }
        [DataMember]
        public int RepeatDays { get; set; }
        [DataMember]
        public int WeekOfMonth { get; set; }

        [DataMember(Name="EveryHours",Order=8)]
        public int EveryHours { get; set; }

        [DataMember(Name="EveryMin",Order=9)]
        public int EveryMin { get; set; }

        [DataMember]
        public List<int> Weekly { get; set; }
        [DataMember]
        public List<int> Monthly { get; set; }
        [DataMember]
        public List<int> Days { get; set; }

        [DataMember(Name="ObjectDependencies",Order=10)]
        public string ObjectDependencies { get; set; }

        [DataMember(Name="RetryTimeLapse",Order=11)]
        public int RetryTimeLapse { get; set; }

        [DataMember(Name="RetryFrequencyUnit",Order=12)]
        public int RetryFrequencyUnit { get; set; }


        [DataMember(Name="AttachToEvent",Order=13)]
        public string AttachToEvent { get; set; }

        [DataMember(Name="CatchUpEnabled",Order=14)]
        public bool CatchUpEnabled { get; set; }

        [DataMember(Name="Servers",Order=15)]
        public string Servers { get; set; }

        [DataMember]
        public string CreatedByUserID { get; set; }
        public string CreatedOnDate { get; set; }
        public int LastModifiedbyUserID { get; set; }
        public string LastModifiedDate { get; set; }
        public int ThreadID { get; set; }
        public int ProcessGroup { get; set; }

        [DataMember]
        public string NextStart { get; set; }

        [DataMember]
        public bool IsEnable { get; set; }

        [DataMember(Name="RunningMode",Order=16)]
        public RunningMode RunningMode { get; set; }

        [DataMember(Name = "AssemblyFileName", Order = 17)]
        public string AssemblyFileName { get; set; }

        public Schedule() { }   


    }
}
