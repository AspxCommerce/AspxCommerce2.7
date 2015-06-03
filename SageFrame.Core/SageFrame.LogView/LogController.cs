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


#endregion

namespace SageFrame.LogView
{
    /// <summary>
    /// Business logic class fog Log
    /// </summary>
    public class LogController
    {
        /// <summary>
        /// Returns log types.
        /// </summary>
        /// <returns>List of LogInfo class containing log types.</returns>
        public List<LogInfo> GetLogType()
        {
            try
            {
                LogProvider objProvider = new LogProvider();
                return objProvider.GetLogType();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Returns log view.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="LogType">logtype</param>
        /// <returns>List of LogInfo class containing log's view</returns>
        public List<LogInfo> GetLogView(int PortalID, string LogType)
        {
            try
            {
                LogProvider objProvider = new LogProvider();
                return objProvider.GetLogView(PortalID, LogType);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
