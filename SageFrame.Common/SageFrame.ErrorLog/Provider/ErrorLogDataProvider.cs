/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;
using SageFrame.Web.Utilities;

namespace SageFrame.ErrorLog
{
    /// <summary>
    /// Manupulates data for ErrorLogController.
    /// </summary>
    public class ErrorLogDataProvider
    {
        /// <summary>
        /// Connects to database and clears error log based on portal ID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        public void ClearLog(int PortalID)
        {
            string sp = "[dbo].[sp_LogClear]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Connects to database and delete individual log based on portal ID and LogID.
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">UserName</param>
        public void DeleteLogByLogID(int ID, int PortalID, string UserName)
        {
            string sp = "[dbo].[sp_LogDeleteByLogID]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@LogID", ID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DeletedBy", UserName));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Connects to database and save log.
        /// </summary>
        /// <param name="logTypeID">logTypeID</param>
        /// <param name="severity">severity</param>
        /// <param name="message">message</param>
        /// <param name="exception">exception</param>
        /// <param name="clientIPAddress">clientIPAddress</param>
        /// <param name="pageURL">pageURL</param>
        /// <param name="isActive">isActive</param>
        /// <param name="portalID">portalID</param>
        /// <param name="addedBy">addedBy</param>
        /// <returns></returns>
        public int InsertLog(int logTypeID, int severity, string message, string exception, string clientIPAddress, string pageURL, bool isActive, int portalID, string addedBy)
        {
            string sp = "[dbo].[sp_LogInsert]";
            SQLHandler sagesql = new SQLHandler();
            try
            {

                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@LogTypeID", logTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Severity", severity));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Message", message));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Exception", exception));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ClientIPAddress", clientIPAddress));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PageURL", pageURL));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", addedBy));
                return sagesql.ExecuteNonQuery(sp, ParamCollInput, "@LogID");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
