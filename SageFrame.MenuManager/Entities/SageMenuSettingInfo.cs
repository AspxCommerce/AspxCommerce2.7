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

namespace SageFrame.SageMenu
{
    /// <summary>
    /// This class holds the properties for SageMenuSettingInfo class.
    /// </summary>
    public class SageMenuSettingInfo
    {
        /// <summary>
        /// Get or set MenuID.
        /// </summary>
        public int MenuID { get; set; }
        /// <summary>
        /// Get or set setting key.
        /// </summary>
        public string SettingKey { get; set; }
        /// <summary>
        /// Get or set setting value.
        /// </summary>
        public string SettingValue { get; set; }
        /// <summary>
        /// Get or set menu type.
        /// </summary>
        public string MenuType { get; set; }
        /// <summary>
        /// Get or set menu sub type.
        /// </summary>
        public string TopMenuSubType { get; set; }
        /// <summary>
        /// Get or set menu header text.
        /// </summary>
        public string MenuHeaderText { get; set; }
        /// <summary>
        /// Get or set saide menu type.
        /// </summary>
        public string SideMenuType { get; set; }
        /// <summary>
        /// Get or set display mode.
        /// </summary>
        public string DisplayMode { get; set; }
        /// <summary>
        /// Get or set menu caption.
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Get or set menu sub title level.
        /// </summary>
        public string SubTitleLevel { get; set; }
        /// <summary>
        /// Get or set PortalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Get or set UserModuleID.
        /// </summary>
        public int UserModuleID { get; set; }
        /// <summary>
        /// Get or set added user name.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Get or set true if active.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Get or set updated user name.
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Initializes a new instance of the SageMenuSettingInfo class.
        /// </summary>
        public SageMenuSettingInfo() { }
    }
}
