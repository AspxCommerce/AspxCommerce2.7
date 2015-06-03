#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace SageFrame.ErrorLog
{
    /// <summary>
    /// This class holds the properties for ErrorLog.
    /// </summary>
    public class ErrorLogInfo
    {
        /// <summary>
        /// Get or set LogID.
        /// </summary>
        public int LogID { get; set; }
        /// <summary>
        /// Get or set LogTypeID.
        /// </summary>
        public int LogTypeID { get; set; }
        /// <summary>
        /// Get or set Severity.
        /// </summary>
        public int Severity { get; set; }
        /// <summary>
        /// Get or set Message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Get or set Exception.
        /// </summary>
        public string Exception { get; set; }
        /// <summary>
        /// Get or set ClientIPAddress.
        /// </summary>
        public string ClientIPAddress { get; set; }
        /// <summary>
        /// Get or set PageURL.
        /// </summary>
        public string PageURL { get; set; }
        /// <summary>
        /// Get or set IsActive.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Get or set PortalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Get or set AddedBy.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Initializes a new instance of the ErrorLogInfo class.
        /// </summary>
        public ErrorLogInfo() { }

    }
}
