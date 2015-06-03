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

namespace SageFrame.FileManager
{
    /// <summary>
    /// This class holds the properties for FolderPermission class.
    /// </summary>
    public class FolderPermission
    {
        /// <summary>
        /// Gets or sets FolderPermissionID.
        /// </summary>
        public int FolderPermissionID { get; set; }
        /// <summary>
        /// Gets or sets FolderID.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// Gets or sets UserID.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Gets or sets RoleID.
        /// </summary>
        public Guid RoleID { get; set; }
        /// <summary>
        /// Gets or sets PermissionKey.
        /// </summary>
        public string PermissionKey { get; set; }
        /// <summary>
        /// Gets or sets PermissionID.
        /// </summary>
        public int PermissionID { get; set; }
        /// <summary>
        /// Gets or sets UserName.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets IsActive.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets AddedBy.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Initializes a new instance of the FolderPermission class.
        /// </summary>
        public FolderPermission() { }
    }
}
