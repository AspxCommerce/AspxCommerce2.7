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

namespace SageFrame.Security.Entities
{
    /// <summary>
    /// This class holds the properties of RoleInfo.
    /// </summary>
    public class RoleInfo
    {
        /// <summary>
        /// Get or set ID.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Get or set RoleID.
        /// </summary>
        public Guid RoleID { get; set; }
        /// <summary>
        /// Get or set role name.
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// Get or set application name.
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// Get or set PortalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Get or set true for active.
        /// </summary>
        public int IsActive { get; set; }
        /// <summary>
        /// Get or set added date time.
        /// </summary>
        public DateTime AddedOn { get; set; }
        /// <summary>
        /// Get or set added user name.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Initializes a new instance of the RoleInfo class.
        /// </summary>
        public RoleInfo() { }
    }
}
