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

namespace SageFrame.Templating
{
    /// <summary>
    /// This class holds the properties of  SettingInfo.
    /// </summary>
    public class SettingInfo
    {
        /// <summary>
        /// Get or set active layout.
        /// </summary>
        public string ActiveLayout { get; set; }
        /// <summary>
        /// Get or set active theme.
        /// </summary>
        public string ActiveTheme { get; set; }
        /// <summary>
        /// Get or set active width.
        /// </summary>
        public string ActiveWidth { get; set; }
        /// <summary>
        /// Get or set astting key.
        /// </summary>
        public string SettingKey { get; set; }
        /// <summary>
        /// Get or set setting value.
        /// </summary>
        public string SettingValue { get; set; }
        /// <summary>
        /// Get or set user name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Get or set portalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Initializes a new instance of the SettingInfo class.
        /// </summary>
        public SettingInfo() { }
        /// <summary>
        /// Initializes a new instance of the SettingInfo class.
        /// </summary>
        /// <param name="_ActiveLayout">Active layout.</param>
        /// <param name="_ActiveTheme">Active theme.</param>
        public SettingInfo(string _ActiveLayout, string _ActiveTheme)
        {
            this.ActiveLayout = _ActiveLayout;
            this.ActiveTheme = _ActiveTheme;
        }
        /// <summary>
        /// Initializes a new instance of the SettingInfo class.
        /// </summary>
        /// <param name="_SettingKey">Setting key.</param>
        /// <param name="_UserName">User name.</param>
        /// <param name="_PortalID">PortalID.</param>
        public SettingInfo(string _SettingKey, string _UserName, int _PortalID)
        {
            this.SettingKey = _SettingKey;
            this.UserName = _UserName;
            this.PortalID = _PortalID;
        }
    }
}
