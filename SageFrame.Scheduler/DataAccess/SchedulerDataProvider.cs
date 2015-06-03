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
using SageFrame.Web.Utilities;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
using System.Data.Common;
#endregion

namespace SageFrame.Scheduler
{
   public class SchedulerDataProvider
    {
        static SQLHandler sagesql = new SQLHandler();

        public static int AddNewSchedule(Schedule objSchedule)
        {
            int id = 0;
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleName", objSchedule.ScheduleName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FullNameSpace", objSchedule.FullNamespace));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StartDate", objSchedule.StartDate));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@EndDate", objSchedule.EndDate));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StartHour", objSchedule.StartHour));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StartMin", objSchedule.StartMin));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RepeatWeeks", objSchedule.RepeatWeeks));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RepeatDays", objSchedule.RepeatDays));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@WeekOfMonth", objSchedule.WeekOfMonth));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@EveryHour", objSchedule.EveryHours));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@EveryMin", objSchedule.EveryMin));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ObjectDependencies", objSchedule.ObjectDependencies));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RetryTimeLapse", objSchedule.RetryTimeLapse));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RetryFrequencyUnit", objSchedule.RetryFrequencyUnit));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AttachToEvent", objSchedule.AttachToEvent));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CatchUpEnabled", objSchedule.CatchUpEnabled));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Servers", objSchedule.Servers));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsEnable", objSchedule.IsEnable));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RunningMode", (int)objSchedule.RunningMode));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AssemblyFileName", objSchedule.AssemblyFileName));
            try
            {
                SQLHandler sagesql = new SQLHandler();
                id = sagesql.ExecuteNonQuery("usp_SchedulerAddJob", ParaMeterCollection, "@ScheduleID");
            }
            catch (Exception)
            {
                throw;
            }
            return id;
        }


        public static void UpdateSchedule(Schedule objSchedule)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleName", objSchedule.ScheduleName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", objSchedule.ScheduleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StartDate", Convert.ToDateTime(objSchedule.StartDate)));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@EndDate", Convert.ToDateTime(objSchedule.EndDate)));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StartHour", objSchedule.StartHour));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StartMin", objSchedule.StartMin));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RepeatWeeks", objSchedule.RepeatWeeks));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RepeatDays", objSchedule.RepeatDays));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@WeekOfMonth", objSchedule.WeekOfMonth));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@EveryHour", objSchedule.EveryHours));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@EveryMin", objSchedule.EveryMin));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ObjectDependencies", objSchedule.ObjectDependencies));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RetryTimeLapse", objSchedule.RetryTimeLapse));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RetryFrequencyUnit", objSchedule.RetryFrequencyUnit));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AttachToEvent", objSchedule.AttachToEvent));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CatchUpEnabled", objSchedule.CatchUpEnabled));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Servers", objSchedule.Servers));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsEnable", objSchedule.IsEnable));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RunningMode", (int)objSchedule.RunningMode));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("usp_ScheduleUpdate", ParaMeterCollection);

            }
            catch (Exception)
            {

                throw;
            }


        }

        public static void AddSchedulerException(int LogTypeID, int Severity, string Message, string Exception, string ClientIPAddress, string PageURL, bool IsActive,
         int PortalID, string AddedBy)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@LogTypeID", LogTypeID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Severity", Severity));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Message", Message));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Exception", Exception));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ClientIPAddress", ClientIPAddress));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PageURL", PageURL));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("sp_LogInsert", ParaMeterCollection, "@LogID");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AddNewScheduleDay(int ScheduleID, List<string> WeekDays)
        {
            string days = string.Join(",", WeekDays.ToArray());
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@DayIds", days));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("usp_ScheduleDayAdd", ParaMeterCollection);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void AddNewScheduleDate(int ScheduleID, string Dates)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Dates", Dates));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("usp_ScheduleDateAdd", ParaMeterCollection);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AddNewScheduleMonth(int ScheduleID, List<string> Months)
        {
            string monthString = string.Join(",", Months.ToArray());

            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Months", monthString));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("usp_ScheduleMonthAdd", ParaMeterCollection);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AddNewScheduleWeek(int ScheduleID, List<string> Weeks)
        {
            string dayString = string.Join(",", Weeks.ToArray());

            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Weeks", dayString));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("usp_ScheduleWeekAdd", ParaMeterCollection);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void ScheduleRepeatOptionDelete(int ScheduleID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));


            try
            {
                SQLHandler sagesql = new SQLHandler();

                sagesql.ExecuteNonQuery("usp_ScheduleRepeatOptionsDelete", ParaMeterCollection);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Schedule> GetTasks()
        {
            List<Schedule> lstTasks = new List<Schedule>();
            string StoredProcedureName = "usp_SchedulesGetCurrent";
            SqlDataReader SQLReader = null;
            SqlConnection SQLConn;
            using (SQLConn = new SqlConnection(SystemSetting.SageFrameConnectionString))
            {
                try
                {
                    SqlCommand SQLCmd = new SqlCommand();
                    SQLCmd.Connection = SQLConn;
                    SQLCmd.CommandText = StoredProcedureName;
                    SQLCmd.CommandType = CommandType.StoredProcedure;
                    SQLConn.Open();
                    SQLReader = SQLCmd.ExecuteReader();

                    while (SQLReader.Read())
                    {
                        Schedule obj = new Schedule();
                        obj.ScheduleName = SQLReader["ScheduleName"].ToString();
                        obj.ScheduleID = int.Parse(SQLReader["ScheduleID"].ToString());
                        obj.FullNamespace = SQLReader["FullNamespace"].ToString();
                        obj.NextStart = SQLReader["NextStart"].ToString();
                        obj.EveryMin = int.Parse(SQLReader["EveryMin"].ToString());
                        obj.IsEnable = (Boolean)SQLReader["IsEnable"];
                        obj.StartDate = SQLReader["StartDate"].ToString();
                        obj.EndDate = SQLReader["EndDate"].ToString();
                        obj.RunningMode = (RunningMode)(Convert.ToInt32(SQLReader["RunningMode"].ToString()));
                        lstTasks.Add(obj);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    if (SQLReader != null)
                    {
                        SQLReader.Close();
                    }
                }
            }

            return lstTasks;


        }

        public static List<SchedularView> GetAllTasks(int offset, int limit)
        {
            string StoredProcedureName = "usp_ScheduleGetAll";

            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            try
            {
                SQLHandler handler = new SQLHandler();
                return handler.ExecuteAsList<SchedularView>(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<SchedularView> GetAllActiveTasks(int offset, int limit)
        {
            string StoredProcedureName = "usp_ScheduleGetAllActive";

            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            try
            {
                SQLHandler handler = new SQLHandler();
                return handler.ExecuteAsList<SchedularView>(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }


        }
        public static List<ScheduleMonth> GetScheduleMonths(int ScheduleID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                return sagesql.ExecuteAsList<ScheduleMonth>("usp_GetScheduleMonths", ParaMeterCollection);
            }
            catch (Exception) { throw; }

        }


        public static List<ScheduleWeek> GetScheduleWeeks(int ScheduleID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                return sagesql.ExecuteAsList<ScheduleWeek>("usp_GetScheduleWeek", ParaMeterCollection);
            }
            catch (Exception) { throw; }

        }


        public static List<ScheduleDate> GetScheduleDates(int ScheduleID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                return sagesql.ExecuteAsList<ScheduleDate>("usp_GetScheduleDates", ParaMeterCollection);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static DateTime? GetNextScheduleDate(int ScheduleID, string CurrentStartDate)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CurrentCalendarDate ", CurrentStartDate));

            DateTime? nextStartDate = null;
            SqlDataReader reader = null;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                reader = sagesql.ExecuteAsDataReader("usp_ScheduleDateGetNextCalendarDate", ParaMeterCollection);

                while (reader.Read())
                {
                    nextStartDate = DBNull.Value.Equals(reader["ScheduleDate"]) ? null : (DateTime?)(reader["ScheduleDate"]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return nextStartDate;
        }

        public static int GetNextScheduleWeekDay(int ScheduleID, int weekday)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CurrentWeeknum", weekday));

            int nextStartDate = 0;
            SqlDataReader reader = null;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                reader = sagesql.ExecuteAsDataReader("usp_ScheduleWeekGetNextWeek", ParaMeterCollection);

                while (reader.Read())
                {
                    nextStartDate = (int)reader["weekDay"];
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return nextStartDate;
        }


        public static int ScheduleDateGetNextMonth(int ScheduleID, int month)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@NextMonth", month));

            int nextStartDate = 0;
            SqlDataReader reader = null;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                reader = sagesql.ExecuteAsDataReader("usp_ScheduleDateGetNextMonth", ParaMeterCollection);

                while (reader.Read())
                {
                    nextStartDate = (int)reader["NextMonth"];
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return nextStartDate;
        }

        public static List<ScheduleDay> GetScheduleDays(int ScheduleID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                return sagesql.ExecuteAsList<ScheduleDay>("usp_GetScheduleDays", ParaMeterCollection);
            }
            catch (Exception) { throw; }

        }

        public static Schedule GetSchedule(int ScheduleID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));

            try
            {
                Schedule s = sagesql.ExecuteAsObject<SageFrame.Scheduler.Schedule>("usp_ScheduleGet", ParaMeterCollection);
                return s;
            }
            catch (Exception) { throw; }

        }

        public static void DeleteTask(int ScheduleID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));

            try
            {
                SQLHandler sagesql = new SQLHandler();

                sagesql.ExecuteNonQuery("usp_ScheduleDeleteTask", ParaMeterCollection);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdateScheduleStatus(int scheduleId, bool isEnable)
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();

            parameterCollection.Add(new KeyValuePair<string, object>("@scheduleId", scheduleId));
            parameterCollection.Add(new KeyValuePair<string, object>("@isEnable", isEnable));
            try
            {
                sagesql.ExecuteNonQuery("sp_UpdateScheduleEnableStatus", parameterCollection);
            }
            catch (Exception)
            {
                throw;
            }

        }



        public static int AddTaskHistory(ScheduleHistory scheduleHistory)
        {
            int id = 0;
            List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
            ParameterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", scheduleHistory.ScheduleID));
            ParameterCollection.Add(new KeyValuePair<string, object>("@StartDate", scheduleHistory.HistoryStartDate));
            ParameterCollection.Add(new KeyValuePair<string, object>("@EndDate", scheduleHistory.HistoryEndDate));
            ParameterCollection.Add(new KeyValuePair<string, object>("@Status", scheduleHistory.Status));
            ParameterCollection.Add(new KeyValuePair<string, object>("@NextStart", scheduleHistory.NextStart));
            ParameterCollection.Add(new KeyValuePair<string, object>("@Server", scheduleHistory.Server));

            try
            {
                id = sagesql.ExecuteNonQuery("usp_ScheduleHistoryAdd", ParameterCollection, "@id");

            }
            catch (Exception)
            {

                throw;
            }

            return id;
        }




        public static void UpdateTaskHistory(ScheduleHistory scheduleHistory)
        {
            List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
            ParameterCollection.Add(new KeyValuePair<string, object>("@ScheduleHistoryID", scheduleHistory.ScheduleHistoryID));
            ParameterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", scheduleHistory.ScheduleID));
            ParameterCollection.Add(new KeyValuePair<string, object>("@StartDate", scheduleHistory.HistoryStartDate));
            ParameterCollection.Add(new KeyValuePair<string, object>("@ReturnText", scheduleHistory.ReturnText));
            ParameterCollection.Add(new KeyValuePair<string, object>("@EndDate", scheduleHistory.HistoryEndDate));
            ParameterCollection.Add(new KeyValuePair<string, object>("@Status", scheduleHistory.Status));
            ParameterCollection.Add(new KeyValuePair<string, object>("@NextStart", scheduleHistory.NextStart));
            ParameterCollection.Add(new KeyValuePair<string, object>("@Server", scheduleHistory.Server));

            try
            {
                sagesql.ExecuteNonQuery("usp_ScheduleHistoryUpdate", ParameterCollection);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static string UpdateTaskHistoryNextStartDate(int ScheduleID)
        {
            // DateTime NextStart
            List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
            ParameterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));


            string NextStart;
            try
            {
                SQLHandler sagesql = new SQLHandler();

                NextStart = sagesql.ExecuteNonQueryAsGivenType<string>("usp_ScheduleHistoryNextStartUpdate", ParameterCollection, "@ResultNextStart");
                return NextStart;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static void DeleteTaskHistory(int ScheduleHistoryID)
        {
            List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
            ParameterCollection.Add(new KeyValuePair<string, object>("@ScheduleHistoryID", ScheduleHistoryID));

            try
            {
                sagesql.ExecuteNonQuery("sp_ScheduleHistoryDelete", ParameterCollection);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static List<ScheduleHistoryView> GetAllScheduleHistory(int ScheduleID, int offset, int limit)
        {
            List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
            ParameterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));
            ParameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            ParameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            try
            {
                SQLHandler sagesql = new SQLHandler();
                return sagesql.ExecuteAsList<ScheduleHistoryView>("usp_ScheduleHistoryGet", ParameterCollection);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static ScheduleHistory GetScheduleHistory(int ScheduleHistoryID)
        {
            List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
            ParameterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleHistoryID));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                return sagesql.ExecuteAsObject<ScheduleHistory>("usp_ScheduleHistoryRetrieve", ParameterCollection);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int GetMaxWeekDay(int ScheduleID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleId", ScheduleID));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                int value = sagesql.ExecuteAsScalar<int>("usp_ScheduleHighestWeekGetNextWeek", ParaMeterCollection);

                return value;
            }
            catch (Exception) { throw; }
        }


        public static int GetMaxScheduleHistoryID(int ScheduleID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleID", ScheduleID));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                int value = sagesql.ExecuteAsScalar<int>("usp_ScheduleHistoryGetMax", ParaMeterCollection);

                return value;
            }
            catch (Exception) { throw; }
        }

        public static int GetMinWeekDay(int ScheduleID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ScheduleId", ScheduleID));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                int value = sagesql.ExecuteAsScalar<int>("usp_ScheduleLowestWeekGetNextWeek", ParaMeterCollection);

                return value;
            }
            catch (Exception) { throw; }
        }

    }
}
