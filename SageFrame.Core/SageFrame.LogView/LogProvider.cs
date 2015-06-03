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

#endregion


namespace SageFrame.LogView
{
    /// <summary>
    /// Manupulates data for Log.
    /// </summary>
    public class LogProvider
    {
        /// <summary>
        /// Connects to database and returns log types.
        /// </summary>
        /// <returns>List of LogInfo class containing log types.</returns>
        public List<LogInfo> GetLogType()
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                return SQLH.ExecuteAsList<LogInfo>("[dbo].[sp_LogType]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns log view.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="LogType">logtype</param>
        /// <returns>List of LogInfo class containing log's view</returns>
        public List<LogInfo> GetLogView(int PortalID, string LogType)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LogType", LogType));
                return SQLH.ExecuteAsList<LogInfo>("[dbo].[sp_LogView]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
