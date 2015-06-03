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
    /// Class that contains properties for template permission.
    /// </summary>
    public class TemplatePermission
    {
        /// <summary>
        /// Gets or sets user module ID.
        /// </summary>
        public int UserModuleID { get; set; }

        /// <summary>
        /// Gets or sets numeric value for template access.
        /// </summary>
        public int AllowAccess { get; set; }

        /// <summary>
        /// Gets or sets role name.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets permission ID.
        /// </summary>
        public int PermissionID { get; set; }

        /// <summary>
        /// Initializes an instance of template permission.
        /// </summary>
        public TemplatePermission() { }
        
    }
}
