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

namespace SageFrame.ExtractTemplate
{
    /// <summary>
    /// Class that contains the page permission for extract template.
    /// </summary>
    public class PagePermission
    {
        /// <summary>
        /// Gets or sets page ID.
        /// </summary>
        public int PageID { get; set; }

        /// <summary>
        /// Gets or sets permission ID. 
        /// </summary>
        public int PermissionID { get; set; }

        /// <summary>
        /// Returns or retains true if the page permission allow access.
        /// </summary>
        public bool AllowAcess { get; set; }

        /// <summary>
        /// Gets or sets role name.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Returns or retains true if the permission is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
