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
    /// This class holds the properties for Roles class.
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// Gets or sets ApplicationID.
        /// </summary>
        public int ApplicationID { get; set; }
        /// <summary>
        /// Gets or sets RoleID.
        /// </summary>
        public Guid RoleID { get; set; }
        /// <summary>
        /// Gets or sets RoleName.
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// Initializes a new instance of the Roles class.
        /// </summary>
        public Roles() { }
    }
}
