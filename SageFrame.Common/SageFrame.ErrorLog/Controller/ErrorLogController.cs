#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data.SqlClient;

#endregion


namespace SageFrame.ErrorLog
{
    /// <summary>
    ///Business logic for error log.
    /// </summary>
    public class ErrorLogController
    {
        /// <summary>
        /// Clears error log based on portal ID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        public void ClearLog(int PortalID)
        {
            try
            {
                ErrorLogDataProvider objProvider = new ErrorLogDataProvider();
                objProvider.ClearLog(PortalID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Delete individual log based on portal ID and LogID.
        /// </summary>
        /// <param name="ID">LogID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">UserName</param>
        public void DeleteLogByLogID(int ID, int PortalID, string UserName)
        {
            try
            {
                ErrorLogDataProvider objProvider = new ErrorLogDataProvider();
                objProvider.DeleteLogByLogID(ID, PortalID, UserName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Save log.
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
        /// <returns>Return LogID</returns>
        public int InsertLog(int logTypeID, int severity, string message, string exception, string clientIPAddress, string pageURL, bool isActive, int portalID, string addedBy)
        {
            try
            {
                ErrorLogDataProvider objProvider = new ErrorLogDataProvider();
                return objProvider.InsertLog(logTypeID, severity, message, exception, clientIPAddress, pageURL, isActive, portalID, addedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
