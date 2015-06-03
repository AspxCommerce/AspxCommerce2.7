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
    ///  Entites class for log.
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// Gets or sets logTyeID.
        /// </summary>
        public int LogTypeID { get; set; }

        /// <summary>
        /// Gets or sets Log name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets log.
        /// </summary>
        public int LogID { get; set; }

        /// <summary>
        /// Gets or sets log added date 
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets log type name
        /// </summary>
        public string LogTypeName { get; set; }

        /// <summary>
        /// Gets or sets portal name 
        /// </summary>
        public string PortalName { get; set; }

        /// <summary>
        /// Gets or sets clients' IP address 
        /// </summary>
        public string ClientIPAddress { get; set; }

        /// <summary>
        /// Gets or sets log page URL 
        /// </summary>
        public string PageURL { get; set; }

        /// <summary>
        /// Gets or sets log  in exception
        /// </summary>
        public string Exception { get; set; }

    }
}
