/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace SageFrame.Dashboard
{
    /// <summary>
    /// This class holds the properties for DashbordSettingInfo
    /// </summary>
    public class DashbordSettingInfo
    {
        /// <summary>
        /// Gets or sets DashboardSettingKeyID
        /// </summary>
        public int DashboardSettingKeyID { get; set; }
        /// <summary>
        /// Gets or sets SettingKey
        /// </summary>
        public string SettingKey { get; set; }
        /// <summary>
        /// Gets or sets SettingValue
        /// </summary>
        public string SettingValue { get; set; }
        /// <summary>
        /// Gets or sets UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets PortalID
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Initializes a new instance of the DashbordSettingInfo class.
        /// </summary>
        public DashbordSettingInfo() { }
        /// <summary>
        /// Initializes a new instance of the DashbordSettingInfo class.
        /// </summary>
        /// <param name="_SettingKey">Setting key.</param>
        /// <param name="_UserName">User name.</param>
        /// <param name="_PortalID">Portal id.</param>
        public DashbordSettingInfo(string _SettingKey, string _UserName, int _PortalID)
        {
            this.SettingKey = _SettingKey;
            this.UserName = _UserName;
            this.PortalID = _PortalID;
        }
    }
}
