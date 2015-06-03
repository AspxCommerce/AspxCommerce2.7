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

namespace SageFrame.ModuleManager
{
    /// <summary>
    /// This class holds the properties of ModulePermissionInfo class.
    /// </summary>
    public class ModulePermissionInfo
    {
        /// <summary>
        /// Get or set PermissionID.
        /// </summary>
        public int PermissionID { get; set; }
        /// <summary>
        /// Get or set RoleID.
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// Get or set UserID.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Get or set user name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Get or set true for allow access.
        /// </summary>
        public bool AllowAccess { get; set; }
        /// <summary>
        /// Initializes a new instance of the ModulePermissionInfo class.
        /// </summary>
        public ModulePermissionInfo() { }

    }
}
