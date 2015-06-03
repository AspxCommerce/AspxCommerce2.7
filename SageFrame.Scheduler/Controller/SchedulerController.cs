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
using SageFrame.Scheduler;
using System.IO;
using System.Web.Hosting;
#endregion

namespace SageFrame.Scheduler
{
    /// <summary>
    /// Business logic for SchedulerController.
    /// </summary>
    public class SchedulerController
    {
        /// <summary>
        /// Reload schedule.
        /// </summary>
        public static void ReloadSchedule()
        {
            Scheduler.ReloadSchedule();
        }
        /// <summary>
        /// Add new schedule.
        /// </summary>
        /// <param name="objSchedule"></param>
        /// <returns></returns>
        public static Schedule AddNewSchedule(Schedule objSchedule)
        {
            try
            {
                objSchedule.ScheduleID = SchedulerDataProvider.AddNewSchedule(objSchedule);

                //  RunScheduleItemNow(objSchedule);
            }
            catch (Exception e)
            {
                ErrorLogger.log(objSchedule, e, "AddNewSchedule");
                string fileName = objSchedule.AssemblyFileName;
                string filepath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin\\" + fileName);
                DeleteFile(filepath);
                throw;
            }

            return objSchedule;
        }
        /// <summary>
        /// Add scheduler exception
        /// </summary>
        /// <param name="LogTypeID">Log type ID.</param>
        /// <param name="Severity">Severity</param>
        /// <param name="Message">Message</param>
        /// <param name="Exception">Exception</param>
        /// <param name="ClientIPAddress">Client IP address.</param>
        /// <param name="PageURL">Page URL.</param>
        /// <param name="IsActive">True if active.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="AddedBy">User name.</param>

        public static void AddSchedulerException(int LogTypeID, int Severity, string Message, string Exception, string ClientIPAddress, string PageURL, bool IsActive,
     int PortalID, string AddedBy)
        {
            try
            {
                SchedulerDataProvider.AddSchedulerException(LogTypeID, Severity, Message, Exception, ClientIPAddress, PageURL, IsActive, PortalID, AddedBy);
            }
            catch (Exception)
            {


            }


        }
        /// <summary>
        /// Obtain next start time.
        /// </summary>
        /// <param name="objScheduleHistory">Object of Schedule class.</param>
        /// <returns>Next start.</returns>
        public static string GetNextStart(Schedule objScheduleHistory)
        {
            if (objScheduleHistory.RunningMode != RunningMode.Once)
            {
                if (string.IsNullOrEmpty(objScheduleHistory.NextStart))
                {
                    objScheduleHistory.NextStart = Convert.ToString(DateTime.Parse(objScheduleHistory.StartDate).ToShortDateString() + " " + objScheduleHistory.StartHour + ":" + objScheduleHistory.StartMin + ":00");

                }
                else if (!objScheduleHistory.CatchUpEnabled)
                {
                    objScheduleHistory.NextStart = Convert.ToString(DateTime.Now);

                }


            }
            switch (objScheduleHistory.RunningMode)
            {

                case RunningMode.Hourly:

                    objScheduleHistory.NextStart = Convert.ToString(DateTime.Parse(objScheduleHistory.NextStart).AddHours(objScheduleHistory.EveryHours).AddMinutes(objScheduleHistory.EveryMin));
                    break;

                case RunningMode.Daily:
                    objScheduleHistory.NextStart = Convert.ToString(DateTime.Parse(objScheduleHistory.NextStart).AddDays(objScheduleHistory.RepeatDays));
                    break;
                case RunningMode.Weekly:
                    int day = GetNextScheduleWeekDay(objScheduleHistory.ScheduleID, (int)DateTime.Parse(objScheduleHistory.NextStart).DayOfWeek + 1);
                    DateTime now = DateTime.Parse(DateTime.Now.ToShortDateString() + "  " + objScheduleHistory.StartHour + ":" + objScheduleHistory.StartMin + ":00");

                    int nextday = 0;
                    int currentstartday = (int)now.DayOfWeek + 1;

                    if (day > currentstartday) nextday = day - currentstartday;
                    else nextday = 7 - currentstartday + day;

                    objScheduleHistory.NextStart = Convert.ToString(now.AddDays(nextday));
                    break;

                case RunningMode.WeekNumber:

                    day = GetNextScheduleWeekDay(objScheduleHistory.ScheduleID, (int)DateTime.Parse(objScheduleHistory.NextStart).DayOfWeek + 1);
                    now = DateTime.Parse(DateTime.Now.ToShortDateString() + "  " + objScheduleHistory.StartHour + ":" + objScheduleHistory.StartMin + ":00");

                    nextday = 0;
                    currentstartday = (int)now.DayOfWeek + 1;
                    int nextMonth = now.Month;
                    if (day > currentstartday)
                    {
                        nextday = now.Day + day - currentstartday;
                    }
                    else
                    {
                        nextMonth = SchedulerController.ScheduleDateGetNextMonth(objScheduleHistory.ScheduleID, DateTime.Parse(objScheduleHistory.NextStart).Month);

                        nextday = objScheduleHistory.RepeatWeeks * 7 - 7 + day;
                    }

                    DateTime nextDate = new DateTime(now.Year, nextMonth, nextday);

                    objScheduleHistory.NextStart = Convert.ToString(nextDate);
                    break;

                case RunningMode.Calendar:

                    objScheduleHistory.NextStart = Convert.ToString(GetNextScheduleDate(objScheduleHistory.ScheduleID, objScheduleHistory.NextStart));
                    //objScheduleHistory.NextStart = Convert.ToString(DateTime.Parse(objScheduleHistory.NextStart).AddDays(objScheduleHistory.RepeatDays));                       

                    break;

                case RunningMode.Once:
                    objScheduleHistory.NextStart = string.Empty;
                    break;



            }
            if (objScheduleHistory.NextStart != string.Empty && DateTime.Parse(objScheduleHistory.NextStart) > DateTime.Parse(objScheduleHistory.EndDate))
                objScheduleHistory.NextStart = string.Empty;
            return objScheduleHistory.NextStart;
        }

        public static string GetNextStartHistory(ScheduleHistory objScheduleHistory)
        {
            if (string.IsNullOrEmpty(objScheduleHistory.NextStart) && objScheduleHistory.RunningMode != RunningMode.Once)
            {
                objScheduleHistory.NextStart = Convert.ToString(DateTime.Parse(objScheduleHistory.StartDate).AddHours(objScheduleHistory.StartHour).AddMinutes(objScheduleHistory.StartMin));
            }

            switch (objScheduleHistory.RunningMode)
            {

                case RunningMode.Hourly:

                    objScheduleHistory.NextStart = Convert.ToString(DateTime.Parse(objScheduleHistory.StartDate).AddHours(objScheduleHistory.EveryHours).AddMinutes(objScheduleHistory.EveryMin));
                    break;

                case RunningMode.Daily:
                    objScheduleHistory.NextStart = Convert.ToString(DateTime.Parse(objScheduleHistory.StartDate).AddDays(objScheduleHistory.RepeatDays));
                    break;
                case RunningMode.Weekly:
                    int day = GetNextScheduleWeekDay(objScheduleHistory.ScheduleID, (int)DateTime.Parse(objScheduleHistory.StartDate).DayOfWeek + 1);
                    DateTime now = DateTime.Parse(DateTime.Now.ToShortDateString() + "  " + objScheduleHistory.StartHour + ":" + objScheduleHistory.StartMin + ":00");

                    int nextday = 0;
                    int currentstartday = (int)now.DayOfWeek + 1;

                    if (day > currentstartday) nextday = day - currentstartday;
                    else nextday = 7 - currentstartday + day;

                    objScheduleHistory.NextStart = Convert.ToString(now.AddDays(nextday));
                    break;

                case RunningMode.WeekNumber:

                    day = GetNextScheduleWeekDay(objScheduleHistory.ScheduleID, (int)DateTime.Parse(objScheduleHistory.StartDate).DayOfWeek + 1);
                    now = DateTime.Parse(DateTime.Now.ToShortDateString() + "  " + objScheduleHistory.StartHour + ":" + objScheduleHistory.StartMin + ":00");

                    nextday = 0;
                    currentstartday = (int)now.DayOfWeek + 1;
                    int nextMonth = now.Month;
                    if (day > currentstartday)
                    {
                        nextday = now.Day + day - currentstartday;
                    }
                    else
                    {
                        nextMonth = SchedulerController.ScheduleDateGetNextMonth(objScheduleHistory.ScheduleID, DateTime.Parse(objScheduleHistory.StartDate).Month);

                        nextday = objScheduleHistory.RepeatWeeks * 7 - 7 + day;
                    }

                    DateTime nextDate = new DateTime(now.Year, nextMonth, nextday);

                    objScheduleHistory.NextStart = Convert.ToString(nextDate);
                    break;

                case RunningMode.Calendar:

                    objScheduleHistory.NextStart = Convert.ToString(GetNextScheduleDate(objScheduleHistory.ScheduleID, objScheduleHistory.StartDate));
                    break;

                case RunningMode.Once:
                    //do nothing
                    break;
            }

            return objScheduleHistory.NextStart;
        }


        public static ScheduleHistory GetScheduleHistory(int ScheduleHistoryID)
        {
            try
            {
                return SchedulerDataProvider.GetScheduleHistory(ScheduleHistoryID);
            }
            catch (Exception e)
            {
                ErrorLogger.log(e, "GetScheduleHistory");
                return null;
            }
        }


        public static List<ScheduleDay> GetScheduleDays(int ScheduleID)
        {
            try
            {
                return SchedulerDataProvider.GetScheduleDays(ScheduleID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<ScheduleMonth> GetScheduleMonths(int ScheduleID)
        {
            try
            {
                return SchedulerDataProvider.GetScheduleMonths(ScheduleID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<ScheduleWeek> GetScheduleWeeks(int ScheduleID)
        {
            try
            {
                return SchedulerDataProvider.GetScheduleWeeks(ScheduleID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<ScheduleDate> GetScheduleDates(int ScheduleID)
        {
            try
            {
                return SchedulerDataProvider.GetScheduleDates(ScheduleID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AddNewScheduleWeeks(int ScheduleID, List<string> Weeks)
        {
            try
            {
                SchedulerDataProvider.AddNewScheduleWeek(ScheduleID, Weeks);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static int GetMaxScheduleHistoryID(int ScheduleID)
        {
            try
            {
                return SchedulerDataProvider.GetMaxScheduleHistoryID(ScheduleID);
            }
            catch (Exception e)
            {
                ErrorLogger.log(e, "GetMaxScheduleHistoryID");
                // throw;
                return 0;
            }
        }
        public static string UpdateTaskHistoryNextStartDate(int ScheduleID)
        {
            try
            {
                return SchedulerDataProvider.UpdateTaskHistoryNextStartDate(ScheduleID);
            }
            catch (Exception e)
            {
                ErrorLogger.log(e, "UpdateTaskHistoryNextStartDate");
                return null;
                // throw;
            }
        }

        public static void AddNewScheduleDays(int ScheduleID, List<string> WeekDays)
        {
            try
            {
                SchedulerDataProvider.AddNewScheduleDay(ScheduleID, WeekDays);
            }
            catch (Exception e)
            {
                ErrorLogger.log(e, "AddNewScheduleDays");
                // throw;
            }
        }

        public static void AddNewScheduleMonths(int ScheduleID, List<string> Months)
        {
            try
            {
                SchedulerDataProvider.AddNewScheduleMonth(ScheduleID, Months);
            }
            catch (Exception e)
            {
                ErrorLogger.log(e, "AddNewScheduleMonths");
                // throw;
            }
        }

        public static void AddNewScheduleDate(int ScheduleID, string Dates)
        {
            try
            {
                SchedulerDataProvider.AddNewScheduleDate(ScheduleID, Dates);
            }
            catch (Exception e)
            {
                ErrorLogger.log(e, "AddNewScheduleDate");
                // throw;
            }
        }


        public static int GetNextScheduleWeekDay(int ScheduleID, int weekday)
        {
            return SchedulerDataProvider.GetNextScheduleWeekDay(ScheduleID, weekday);
        }

        public static int ScheduleDateGetNextMonth(int ScheduleID, int month)
        {
            return SchedulerDataProvider.ScheduleDateGetNextMonth(ScheduleID, month);
        }

        public static void UpdateSchedule(Schedule objSchedule, List<string> WeekDays, List<string> Months, string Dates)
        {
            DateTime currentDate = DateTime.Now;
            DateTime tmpNextStartDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, objSchedule.StartHour, objSchedule.StartMin, 0);

            try
            {

                Scheduler.RemoveFromScheduleQueue(objSchedule);
                SchedulerDataProvider.ScheduleRepeatOptionDelete(objSchedule.ScheduleID);
                if (WeekDays.Count > 0) AddNewScheduleWeeks(objSchedule.ScheduleID, WeekDays);
                if (Months.Count > 0) AddNewScheduleMonths(objSchedule.ScheduleID, Months);
                if (!string.IsNullOrEmpty(Dates) && Dates.Trim().Length > 0) AddNewScheduleDate(objSchedule.ScheduleID, Dates);

                objSchedule.NextStart = UpdateTaskHistoryNextStartDate(objSchedule.ScheduleID);
                SchedulerDataProvider.UpdateSchedule(objSchedule);

                RunScheduleItemNow(objSchedule);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Schedule GetSchedule(int ScheduleID)
        {
            try
            {
                return (SchedulerDataProvider.GetSchedule(ScheduleID));
            }
            catch (Exception e)
            {

                ErrorLogger.log(e, "GetSchedule");

            }
            return null;
        }

        public static List<Schedule> GetTasks()
        {
            try
            {
                return (SchedulerDataProvider.GetTasks());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<SchedularView> GetAllTasks(int offset, int limit)
        {
            try
            {
                return (SchedulerDataProvider.GetAllTasks(offset, limit));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<SchedularView> GetAllActiveTasks(int offset, int limit)
        {
            try
            {
                return (SchedulerDataProvider.GetAllActiveTasks(offset, limit));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DateTime? GetNextScheduleDate(int ScheduleID, string CurrentStartDate)
        {
            return SchedulerDataProvider.GetNextScheduleDate(ScheduleID, CurrentStartDate);
        }
        public static void DeleteTask(int ScheduleID, string physicalPath)
        {
            try
            {
                SchedulerDataProvider.DeleteTask(ScheduleID);
                Scheduler.RemoveFromScheduleQueueByID(ScheduleID);
                //Scheduler.ReloadSchedule();
                DeleteFile(physicalPath);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static void DeleteFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    object obj = new object();
                    lock (obj)
                    {
                        File.Delete(fileName);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void UpdateScheduleEnableStatus(int scheduleId, bool isEnable)
        {
            try
            {
                SchedulerDataProvider.UpdateScheduleStatus(scheduleId, isEnable);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int AddTaskHistory(ScheduleHistory scheduleHistory)
        {
            int Id = 0;
            try
            {
                Id = SchedulerDataProvider.AddTaskHistory(scheduleHistory);
            }
            catch (Exception)
            {

                throw;
            }
            return Id;
        }

        public static void UpdateTaskHistory(ScheduleHistory scheduleHistory)
        {
            try
            {
                SchedulerDataProvider.UpdateTaskHistory(scheduleHistory);
            }
            catch (Exception e)
            {
                ErrorLogger.log(scheduleHistory, e, "UpdateTaskHistory");
                throw;
            }
        }


        public static void DeleteTaskHistory(int ScheduleHistoryID)
        {
            try
            {
                SchedulerDataProvider.DeleteTaskHistory(ScheduleHistoryID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<ScheduleHistoryView> GetAllScheduleHistory(int ScheduleID, int offset, int limit)
        {
            List<ScheduleHistoryView> scheduleHistoryList = null;
            try
            {
                scheduleHistoryList = SchedulerDataProvider.GetAllScheduleHistory(ScheduleID, offset, limit);
            }
            catch (Exception)
            {

                throw;
            }

            return scheduleHistoryList;
        }


        public static void RunScheduleItemNow(Schedule schedule)
        {

            Scheduler.RemoveFromScheduleQueue(schedule);
            ScheduleHistory scheduleHistory = new ScheduleHistory(schedule);


            if (schedule.IsEnable)
                Scheduler.AddToScheduleQueue(scheduleHistory);

        }


        public static void RunScheduleNow(int id)
        {
            Schedule schedule = GetSchedule(id);
            Scheduler.RemoveFromScheduleQueue(schedule);
            ScheduleHistory scheduleHistory = new ScheduleHistory(schedule);
            scheduleHistory.NextStart = DateTime.Now.ToString();
            schedule.IsEnable = true;
            Scheduler.AddToScheduleQueue(scheduleHistory);

        }

    }
}
