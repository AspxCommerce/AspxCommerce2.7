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
    /// This class holds the properties for FileManagerSettingInfo.
    /// </summary>
    public class FileManagerSettingInfo
    {
        /// <summary>
        /// Gets or sets FileManagerSettingValueID.
        /// </summary>
        public int FileManagerSettingValueID { get; set; }
        /// <summary>
        /// Gets or sets UserModuleID.
        /// </summary>
        public int UserModuleID { get; set; }
        /// <summary>
        /// Gets or sets SettingKey.
        /// </summary>
        public string SettingKey { get; set; }
        /// <summary>
        /// Gets or sets SettingValue.
        /// </summary>
        public string SettingValue { get; set; }
        /// <summary>
        /// Gets or sets IsActive.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets IsDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Gets or sets AddedOn.
        /// </summary>
        public string AddedOn { get; set; }
        /// <summary>
        /// Gets or sets AddedBy.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Gets or sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets .
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Initializes a new instance of the FileManagerSettingInfo class.
        /// </summary>
        public FileManagerSettingInfo() { }
       
    }
}
