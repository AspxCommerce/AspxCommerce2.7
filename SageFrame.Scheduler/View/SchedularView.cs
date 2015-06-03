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
    public class SchedularView
    {

        [DataMember(Name = "_RowTotal", Order = 1)]
        private System.Nullable<int> _RowTotal;

        [DataMember(Name = "_ScheduleID", Order = 2)]
        private System.Nullable<int> _ScheduleID;

        [DataMember(Name = "_ScheduleName", Order = 3)]
        private string _ScheduleName;
        [DataMember(Name = "_FullNamespace", Order = 4)]
        private string _FullNameSpace;
        [DataMember(Name = "_StartDate", Order = 5)]
        private System.Nullable<System.DateTime> _StartDate;
        [DataMember(Name = "_EndDate", Order = 6)]
        private System.Nullable<System.DateTime> _EndDate;
        [DataMember(Name = "_StartHour", Order = 7)]
        private System.Nullable<short> _StartHour;
        [DataMember(Name = "_StartMin", Order = 8)]
        private System.Nullable<short> _StartMin;

        [DataMember(Name = "_RepeatWeeks", Order = 9)]
        private System.Nullable<short> _RepeatWeeks;

        [DataMember(Name = "_RepeatDays", Order = 10)]
        private System.Nullable<int> _RepeatDays;

        [DataMember(Name = "_WeekOfMonth", Order = 11)]
        private System.Nullable<int> _WeekOfMonth;

        [DataMember(Name = "_EveryHours", Order = 12)]
        private System.Nullable<int> _EveryHours;

        [DataMember(Name = "_EveryMins", Order = 13)]
        private System.Nullable<short> _EveryMin;

        [DataMember(Name = "_ObjecDependencies", Order = 14)]
        private string _ObjectDependencies;

        [DataMember(Name = "_RetryTimeLapse", Order = 15)]
        private System.Nullable<int> _RetryTimeLapse;

        [DataMember(Name = "_RetryFrequencyUnit", Order = 16)]
        private System.Nullable<int> _RetryFrequencyUnit;

        [DataMember(Name = "_AttachToEvent", Order = 17)]
        private string _AttachToEvent;

        [DataMember(Name = "_CatchUpEnabled", Order = 18)]
        private System.Nullable<bool> _CatchUpEnabled;

        [DataMember(Name = "_Servers", Order = 19)]
        private string _Servers;

        [DataMember(Name = "_CreatedByUserID", Order = 20)]
        private string _CreatedByUserID;

        [DataMember(Name = "_CreatedOnDate", Order = 21)]
        private System.Nullable<System.DateTime> _CreatedOnDate;

        [DataMember(Name = "_LastModifiedbyUserID", Order = 22)]
        private System.Nullable<int> _LastModifiedbyUserID;

        [DataMember(Name = "_LastModifiedDate", Order = 23)]
        private System.Nullable<System.DateTime> _LastModifiedDate;

        [DataMember(Name = "_IsEnable", Order = 24)]
        private System.Nullable<bool> _IsEnable;

        [DataMember(Name = "_ScheduleHistoryID", Order = 25)]
        private System.Nullable<int> _ScheduleHistoryID;

        [DataMember(Name = "_NextStart", Order = 26)]
        private System.Nullable<System.DateTime> _NextStart;

        [DataMember(Name = "_HistoryStartDate", Order = 27)]
        private System.Nullable<System.DateTime> _HistoryStartDate;

        [DataMember(Name = "_HistoryEndDate", Order = 28)]
        private System.Nullable<System.DateTime> _HistoryEndDate;

        [DataMember(Name = "_RunningMode", Order = 29)]
        private System.Nullable<int> _RunningMode;

        [DataMember(Name = "_AssemblyFileName", Order = 30)]
        private string _AssemblyFileName;
        //[DataMember(Name = "_ScheduleDays", Order = 30)]
        //public List<ScheduleDay> _ScheduleDays { get; set; }

        //[DataMember(Name = "_ScheduleMonths", Order = 31)]
        //public List<ScheduleMonth> _ScheduleMonths { get; set; }

        //[DataMember(Name = "_ScheduleDates", Order = 32)]
        //public List<ScheduleDate> _ScheduleDates { get; set; }

        //[DataMember(Name = "_ScheduleWeeks", Order = 33)]
        //public List<ScheduleWeek> _ScheduleWeeks { get; set; }


        public SchedularView()
        {
        }


        public SchedularView(Schedule objSchedule)
        {
            this.ScheduleID = objSchedule.ScheduleID;

            this.AttachToEvent = objSchedule.AttachToEvent;
            this.CatchUpEnabled = objSchedule.CatchUpEnabled;
            this.EveryMin = (short)objSchedule.EveryMin;
            this.NextStart = Convert.ToDateTime(objSchedule.NextStart);
            this.ObjectDependencies = objSchedule.ObjectDependencies;
            this.RetryTimeLapse = objSchedule.RetryTimeLapse;
            this.RetryFrequencyUnit = objSchedule.RetryFrequencyUnit;
            this.RepeatDays = objSchedule.RepeatDays;
            this.RepeatWeeks = (short)objSchedule.RepeatWeeks;
            this.RunningMode = (short)objSchedule.RunningMode;
            this.WeekOfMonth = objSchedule.WeekOfMonth;


            //this.TimeLapse = objSchedule.TimeLapse;
            //this.TimeLapseMeasurement = objSchedule.TimeLapseMeasurement;
            this.StartDate = Convert.ToDateTime(objSchedule.StartDate);
            this.StartHour = (short)objSchedule.StartHour;
            this.StartMin = (short)objSchedule.StartMin;
            this.EndDate = Convert.ToDateTime(objSchedule.EndDate);
            this.IsEnable = objSchedule.IsEnable;
            this._FullNameSpace = objSchedule.FullNamespace;
            this.Servers = objSchedule.Servers;
            this.ScheduleName = objSchedule.ScheduleName;



        }


        public System.Nullable<int> RowTotal
        {
            get
            {
                return this._RowTotal;
            }
            set
            {
                if ((this._RowTotal != value))
                {
                    this._RowTotal = value;
                }
            }
        }


        public System.Nullable<int> ScheduleID
        {
            get
            {
                return this._ScheduleID;
            }
            set
            {
                if ((this._ScheduleID != value))
                {
                    this._ScheduleID = value;
                }
            }
        }


        public string ScheduleName
        {
            get
            {
                return this._ScheduleName;
            }
            set
            {
                if ((this._ScheduleName != value))
                {
                    this._ScheduleName = value;
                }
            }
        }


        public string FullNameSpace
        {
            get
            {
                return this._FullNameSpace;
            }
            set
            {
                if ((this._FullNameSpace != value))
                {
                    this._FullNameSpace = value;
                }
            }
        }


        public System.Nullable<System.DateTime> StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }


        public System.Nullable<System.DateTime> EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                if ((this._EndDate != value))
                {
                    this._EndDate = value;
                }
            }
        }


        public System.Nullable<short> StartHour
        {
            get
            {
                return this._StartHour;
            }
            set
            {
                if ((this._StartHour != value))
                {
                    this._StartHour = value;
                }
            }
        }


        public System.Nullable<short> StartMin
        {
            get
            {
                return this._StartMin;
            }
            set
            {
                if ((this._StartMin != value))
                {
                    this._StartMin = value;
                }
            }
        }


        public System.Nullable<short> RepeatWeeks
        {
            get
            {
                return this._RepeatWeeks;
            }
            set
            {
                if ((this._RepeatWeeks != value))
                {
                    this._RepeatWeeks = value;
                }
            }
        }


        public System.Nullable<int> RepeatDays
        {
            get
            {
                return this._RepeatDays;
            }
            set
            {
                if ((this._RepeatDays != value))
                {
                    this._RepeatDays = value;
                }
            }
        }


        public System.Nullable<int> WeekOfMonth
        {
            get
            {
                return this._WeekOfMonth;
            }
            set
            {
                if ((this._WeekOfMonth != value))
                {
                    this._WeekOfMonth = value;
                }
            }
        }


        public System.Nullable<int> EveryHours
        {
            get
            {
                return this._EveryHours;
            }
            set
            {
                if ((this._EveryHours != value))
                {
                    this._EveryHours = value;
                }
            }
        }


        public System.Nullable<short> EveryMin
        {
            get
            {
                return this._EveryMin;
            }
            set
            {
                if ((this._EveryMin != value))
                {
                    this._EveryMin = value;
                }
            }
        }


        public string ObjectDependencies
        {
            get
            {
                return this._ObjectDependencies;
            }
            set
            {
                if ((this._ObjectDependencies != value))
                {
                    this._ObjectDependencies = value;
                }
            }
        }


        public System.Nullable<int> RetryTimeLapse
        {
            get
            {
                return this._RetryTimeLapse;
            }
            set
            {
                if ((this._RetryTimeLapse != value))
                {
                    this._RetryTimeLapse = value;
                }
            }
        }


        public System.Nullable<int> RetryFrequencyUnit
        {
            get
            {
                return this._RetryFrequencyUnit;
            }
            set
            {
                if ((this._RetryFrequencyUnit != value))
                {
                    this._RetryFrequencyUnit = value;
                }
            }
        }


        public string AttachToEvent
        {
            get
            {
                return this._AttachToEvent;
            }
            set
            {
                if ((this._AttachToEvent != value))
                {
                    this._AttachToEvent = value;
                }
            }
        }


        public System.Nullable<bool> CatchUpEnabled
        {
            get
            {
                return this._CatchUpEnabled;
            }
            set
            {
                if ((this._CatchUpEnabled != value))
                {
                    this._CatchUpEnabled = value;
                }
            }
        }


        public string Servers
        {
            get
            {
                return this._Servers;
            }
            set
            {
                if ((this._Servers != value))
                {
                    this._Servers = value;
                }
            }
        }


        public string CreatedByUserID
        {
            get
            {
                return this._CreatedByUserID;
            }
            set
            {
                if ((this._CreatedByUserID != value))
                {
                    this._CreatedByUserID = value;
                }
            }
        }


        public System.Nullable<System.DateTime> CreatedOnDate
        {
            get
            {
                return this._CreatedOnDate;
            }
            set
            {
                if ((this._CreatedOnDate != value))
                {
                    this._CreatedOnDate = value;
                }
            }
        }


        public System.Nullable<int> LastModifiedbyUserID
        {
            get
            {
                return this._LastModifiedbyUserID;
            }
            set
            {
                if ((this._LastModifiedbyUserID != value))
                {
                    this._LastModifiedbyUserID = value;
                }
            }
        }


        public System.Nullable<System.DateTime> LastModifiedDate
        {
            get
            {
                return this._LastModifiedDate;
            }
            set
            {
                if ((this._LastModifiedDate != value))
                {
                    this._LastModifiedDate = value;
                }
            }
        }


        public System.Nullable<bool> IsEnable
        {
            get
            {
                return this._IsEnable;
            }
            set
            {
                if ((this._IsEnable != value))
                {
                    this._IsEnable = value;
                }
            }
        }


        public System.Nullable<int> ScheduleHistoryID
        {
            get
            {
                return this._ScheduleHistoryID;
            }
            set
            {
                if ((this._ScheduleHistoryID != value))
                {
                    this._ScheduleHistoryID = value;
                }
            }
        }


        public System.Nullable<System.DateTime> NextStart
        {
            get
            {
                return this._NextStart;
            }
            set
            {
                if ((this._NextStart != value))
                {
                    this._NextStart = value;
                }
            }
        }


        public System.Nullable<System.DateTime> HistoryStartDate
        {
            get
            {
                return this._HistoryStartDate;
            }
            set
            {
                if ((this._HistoryStartDate != value))
                {
                    this._HistoryStartDate = value;
                }
            }
        }


        public System.Nullable<System.DateTime> HistoryEndDate
        {
            get
            {
                return this._HistoryEndDate;
            }
            set
            {
                if ((this._HistoryEndDate != value))
                {
                    this._HistoryEndDate = value;
                }
            }
        }

        public System.Nullable<int> RunningMode
        {
            get
            {
                return this._RunningMode;
            }
            set
            {
                if ((this._RunningMode != value))
                {
                    this._RunningMode = value;
                }
            }
        }

        public string AssemblyFileName
        {
            get
            {
                return this._AssemblyFileName;
            }
            set
            {
                if ((this._AssemblyFileName != value))
                {
                    this._AssemblyFileName = value;
                }
            }
        }

    }
}
