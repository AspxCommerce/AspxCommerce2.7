<%@ WebService Language="C#" Class="SchedulerWebService" %>
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SageFrame.Scheduler;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.Security;
using System.Net;
using System.Threading;
using SageFrame.Services;

/// <summary>
/// Summary description for SchedulerWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class SchedulerWebService : AuthenticateService
{

    public SchedulerWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    // Add [WebGet] attribute to use HTTP GET  
    public DateTime? GetDate(string date)
    {
        DateTime? dateTime = null;

        if (string.IsNullOrEmpty(date) && date.Contains("-"))
        {
            date = date.Trim();
            int year = int.Parse(date.Substring(0, 4));
            int month = int.Parse(date.Substring(5, 2));
            int day = int.Parse(date.Substring(8, 2));
            dateTime = new DateTime(year, month, day, 0, 0, 0);
        }
        return dateTime;

    }
    // Add more operations here and mark them with
    [WebMethod]
    public void AddNewSchedule(string ScheduleName, string FullNameSpace, string StartDate, string EndDate, int StartHour, int StartMin, int RepeatWeeks, int RepeatDays,
                                int WeekOfMonth, int EveryHour, int EveryMin, string Dependencies, int RetryTimeLapse, int RetryFrequencyUnit, int AttachToEvent, bool CatchUpEnabled,
                                string Servers, string CreatedByUserID, bool IsEnable, int RunningMode, List<string> WeekDays, List<string> Months, string Dates, string AssemblyFileName, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {

            //MembershipUser user = Membership.GetUser();

            Schedule objSchedule = new Schedule();
            objSchedule.ScheduleName = ScheduleName;
            objSchedule.FullNamespace = FullNameSpace;
            objSchedule.StartDate = StartDate ?? DateTime.Now.ToString();
            objSchedule.EndDate = EndDate ?? DateTime.Now.ToString();
            objSchedule.StartHour = StartHour;
            objSchedule.StartMin = StartMin;
            objSchedule.RepeatWeeks = RepeatWeeks;
            objSchedule.RepeatDays = RepeatDays;
            objSchedule.WeekOfMonth = WeekOfMonth;
            objSchedule.EveryMin = EveryMin;
            objSchedule.EveryHours = EveryHour;
            objSchedule.ObjectDependencies = Dependencies;
            objSchedule.RetryTimeLapse = RetryTimeLapse;
            objSchedule.RetryFrequencyUnit = RetryFrequencyUnit;
            objSchedule.AttachToEvent = AttachToEvent.ToString();
            objSchedule.CatchUpEnabled = CatchUpEnabled;
            objSchedule.Servers = Servers;
            // objSchedule.CreatedByUserID = user.ProviderUserKey.ToString();

            objSchedule.CreatedOnDate = DateTime.Now.ToString();
            objSchedule.IsEnable = IsEnable;
            objSchedule.RunningMode = (RunningMode)RunningMode;
            objSchedule.AssemblyFileName = AssemblyFileName;


            try
            {
                Schedule schedule = SchedulerController.AddNewSchedule(objSchedule);
                if (schedule.ScheduleID < 1)
                {
                    string fileName = objSchedule.AssemblyFileName;
                    string filepath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin\\" + fileName);
                    SchedulerController.DeleteFile(filepath);
                }
                else
                {

                    if (WeekDays.Count > 0) SchedulerController.AddNewScheduleWeeks(schedule.ScheduleID, WeekDays);
                    if (Months.Count > 0) SchedulerController.AddNewScheduleMonths(schedule.ScheduleID, Months);
                    if (!string.IsNullOrEmpty(Dates) && Dates.Trim().Length > 0) SchedulerController.AddNewScheduleDate(schedule.ScheduleID, Dates);

                    SchedulerController.RunScheduleItemNow(schedule);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    [WebMethod]
    public void UpdateSchedule(string ScheduleName, string FullNameSpace, string StartDate, string EndDate, int StartHour, int StartMin, int RepeatWeeks, int RepeatDays,
                               int WeekOfMonth, int EveryHour, int EveryMin, string Dependencies, int RetryTimeLapse, int RetryFrequencyUnit, int AttachToEvent, bool CatchUpEnabled,
                               string Servers, string CreatedByUserID, bool IsEnable, int RunningMode, List<string> WeekDays, List<string> Months, string Dates, int ScheduleID, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            //  MembershipUser user = Membership.GetUser();
            Schedule objSchedule = new Schedule();
            objSchedule.ScheduleID = ScheduleID;
            objSchedule.ScheduleName = ScheduleName;
            objSchedule.StartDate = StartDate ?? DateTime.Now.ToString();
            objSchedule.EndDate = EndDate ?? DateTime.Now.ToString();
            objSchedule.StartHour = StartHour;
            objSchedule.StartMin = StartMin;
            objSchedule.FullNamespace = FullNameSpace;
            objSchedule.RepeatWeeks = RepeatWeeks;
            objSchedule.RepeatDays = RepeatDays;
            objSchedule.WeekOfMonth = WeekOfMonth;
            objSchedule.EveryMin = EveryMin;
            objSchedule.EveryHours = EveryHour;
            objSchedule.ObjectDependencies = Dependencies;
            objSchedule.RetryTimeLapse = RetryTimeLapse;
            objSchedule.RetryFrequencyUnit = RetryFrequencyUnit;
            objSchedule.AttachToEvent = AttachToEvent.ToString();
            objSchedule.CatchUpEnabled = CatchUpEnabled;
            objSchedule.Servers = Servers;

            objSchedule.CreatedOnDate = DateTime.Now.ToString();
            objSchedule.IsEnable = IsEnable;
            objSchedule.RunningMode = (RunningMode)RunningMode;

            try
            {
                SchedulerController.UpdateSchedule(objSchedule, WeekDays, Months, Dates);
                // SchedulerController.UpdateTaskHistoryNextStartDate(objSchedule.ScheduleID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    [WebMethod]
    public void RunScheduleNow(int id, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            SchedulerController.RunScheduleNow(id);
        }
    }

    [WebMethod(EnableSession = true)]
    public List<SchedularView> GetAllTasks(int offset, int limit, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            List<SchedularView> lstSchedule = SchedulerController.GetAllTasks(offset, limit);
            return lstSchedule;
        }
        else
        {
            return null;
        }
    }

    public List<SchedularView> GetAllActiveTasks(int offset, int limit, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            List<SchedularView> lstSchedule = SchedulerController.GetAllActiveTasks(offset, limit);
            return lstSchedule;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public Schedule GetTask(int id, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            Schedule s = SchedulerController.GetSchedule(id);
            return s;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public List<ScheduleHistoryView> GetAllScheduleHistory(int ScheduleID, int offset, int limit, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            List<ScheduleHistoryView> lstSchedule = SchedulerController.GetAllScheduleHistory(ScheduleID, offset, limit);
            return lstSchedule;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public List<ScheduleDay> GetScheduleDays(int ScheduleID, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            List<ScheduleDay> list = SchedulerController.GetScheduleDays(ScheduleID);
            return list;
        }
        else
        {
            return null;
        }

    }

    [WebMethod]
    public List<ScheduleWeek> GetScheduleWeeks(int ScheduleID, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {

            List<ScheduleWeek> list = SchedulerController.GetScheduleWeeks(ScheduleID);

            return list;
        }
        else
        {
            return null;
        }

    }

    [WebMethod]
    public List<ScheduleDate> GetScheduleDates(int ScheduleID, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            List<ScheduleDate> list = SchedulerController.GetScheduleDates(ScheduleID);

            return list;
        }
        else
        {
            return null;
        }


    }

    [WebMethod]
    public List<ScheduleMonth> GetScheduleMonths(int ScheduleID, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            List<ScheduleMonth> list = SchedulerController.GetScheduleMonths(ScheduleID);

            return list;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public void DeleteTask(int ScheduleID, string AssemblyFileName, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            // Schedule schedule = SchedulerController.GetSchedule(ScheduleID);

            if (!string.IsNullOrEmpty(AssemblyFileName))
            {
                string filepath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin\\" + AssemblyFileName);
                SchedulerController.DeleteTask(ScheduleID, filepath);
            }
        }
    }

    [WebMethod]
    public int isFileUniqueTask(string FileName, int portalID, int userModuleId, string userName, string sageFrameSecureToken)
    {
        if (IsPostAuthenticated(portalID, userModuleId, userName, sageFrameSecureToken))
        {
            //bool bIsReady = false;

            //IPHostEntry entries = Dns.GetHostByName(Dns.GetHostName());
            //string server = entries.AddressList[0].ToString();
            ////string UNC = Strings.Space(100);
            //            WNetGetConnection(server.Name.Substring(0, 2), UNC, 100);

            ////            //Presuming the drive is mapped \\servername\share
            ////            //    Parse the servername out of the UNC
            //           string server = UNC.Trim().Substring(2, UNC.Trim().IndexOf("\\", 2) - 2);

            // bIsReady = IsDriveReady(server);

            int returnFlag = 0;
            string path = HostingEnvironment.ApplicationPhysicalPath;
            string filepath = Path.Combine(path, "bin\\" + FileName);

            Thread t = new Thread(
                                  new ThreadStart(delegate()
                                  {
                                      if (!File.Exists(filepath))
                                      {
                                          returnFlag = 1;
                                      }

                                  })
                                 );
            t.Start();
            bool completed = t.Join(500); //half a sec of timeout
            if (!completed) { returnFlag = 0; t.Abort(); }
            return returnFlag;


            //int returnFlag = 0;  

            //     string path = HostingEnvironment.ApplicationPhysicalPath;

            //     string filepath = Path.Combine(path, "bin\\" + FileName);

            //object obj=new  object();

            //  lock(obj)
            //  {

            //     if (!File.Exists(filepath))
            //     {
            //         returnFlag = 1;
            //     }
            ////}

            //return returnFlag;
        }
        else
        {
            return 0;
        }
    }

    [WebMethod]
    public void UpdateScheduleEnableStatus(int scheduleId, bool isEnable, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, userModuleId, UserName, secureToken))
        {
            SchedulerDataProvider.UpdateScheduleStatus(scheduleId, isEnable);
        }
    }

    //     private void IsDriveReady(System.Object sender, System.EventArgs e)
    //{
    //    bool bIsReady = false;


    //    foreach (System.IO.DriveInfo dri in System.IO.DriveInfo.GetDrives()) {
    //        //If the drive is a Network drive only, then ping it to see if it's ready.

    //        if (dri.DriveType == System.IO.DriveType.Network) {
    //            //Get the UNC (\\servername\share) for the 
    //            //    drive letter returned by dri.Name
    //            string UNC = Strings.Space(100);
    //            WNetGetConnection(dri.Name.Substring(0, 2), UNC, 100);

    //            //Presuming the drive is mapped \\servername\share
    //            //    Parse the servername out of the UNC
    //            string server = UNC.Trim().Substring(2, UNC.Trim().IndexOf("\\", 2) - 2);

    //            //Ping the server to see if it is available
    //            bIsReady = IsDriveReady(server);

    //        } else {
    //            bIsReady = dri.IsReady;

    //        }


    //        //Only process drives that are ready
    //        if (bIsReady == true) {
    //            //Process your drive...
    //            Interaction.MsgBox(dri.Name + " is ready:  " + bIsReady);

    //        }

    //    }

    //    Interaction.MsgBox("All drives processed");

    //}


    //private bool IsDriveReady(string serverName)
    //{
    //    bool bReturnStatus = false;

    //    //***  SET YOUR TIMEOUT HERE  ***
    //    int timeout = 5;
    //    //5 seconds


    //    System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
    //    System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();

    //    options.DontFragment = true;

    //    //Enter a valid ip address
    //    string ipAddressOrHostName = serverName;
    //    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    //    byte[] buffer = System.Text.Encoding.ASCII.GetBytes(data);
    //    System.Net.NetworkInformation.PingReply reply = pingSender.Send(ipAddressOrHostName, timeout, buffer, options);

    //    if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
    //    {
    //        bReturnStatus = true;

    //    }

    //    return bReturnStatus;

    //}

}

