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
    /// This class holds the properties of UserModuleInfo class. 
    /// </summary>
    public class UserModuleInfo
    {
        /// <summary>
        /// Get or set UserModuleID.
        /// </summary>
        public int UserModuleID { get; set; }
        /// <summary>
        /// Get or set ModuleDefID.
        /// </summary>
        public int ModuleDefID { get; set; }
        /// <summary>
        /// Get or set 
        /// </summary>
        public string UserModuleTitle { get; set; }
        /// <summary>
        /// Get or set user module title. 
        /// </summary>
        public bool AllPages { get; set; }
        /// <summary>
        /// Get or set true for all pages.
        /// </summary>
        public string ShowInPages { get; set; }
        /// <summary>
        /// Get or set true for inherit view permission.
        /// </summary>
        public bool InheritViewPermissions { get; set; }
        /// <summary>
        /// Get or set page SEO name.
        /// </summary>
        public string SEOName { get; set; }
        /// <summary>
        /// Get or set PageID.
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// Get or set pane name.
        /// </summary>
        public string PaneName { get; set; }
        /// <summary>
        /// Get or set module order.
        /// </summary>
        public int ModuleOrder { get; set; }
        /// <summary>
        /// Get or set cache time.
        /// </summary>
        public string CacheTime { get; set; }
        /// <summary>
        /// Get or set alignment.
        /// </summary>
        public string Alignment { get; set; }
        /// <summary>
        /// Get or set color.
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Get or set conFile.
        /// </summary>
        public string conFile { get; set; }
        /// <summary>
        /// Get or set true for visibility.
        /// </summary>
        public bool Visibility { get; set; }
        /// <summary>
        /// Get or set 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Get or set added date.
        /// </summary>
        public DateTime AddedOn { get; set; }
        /// <summary>
        /// Get or set PortalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Get or set added user name.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Get or set error code.
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Get or set page name.
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Get or set true for handheld device.
        /// </summary>
        public bool IsHandheld { get; set; }
        /// <summary>
        /// Get or set 
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// Get or set friendly name.
        /// </summary>
        public string HeaderText { get; set; }
        /// <summary>
        /// Get or set true for showing header text.
        /// </summary>
        public bool ShowHeaderText { get; set; }
        /// <summary>
        /// Get or set controls count.
        /// </summary>
        public int ControlsCount { get; set; }
        /// <summary>
        /// Get or set list of ModulePermissionInfo class.
        /// </summary>
        public List<ModulePermissionInfo> LSTUserModulePermission { get; set; }
        /// <summary>
        /// Get or set true for admin.
        /// </summary>
        public bool IsInAdmin { get; set; }
        /// <summary>
        /// Get or set page module name.
        /// </summary>
        public string PageModuleName
        {
            get { return (PageName + "-->" + UserModuleTitle); }
        }
        /// <summary>
        /// Get or set suffix class.
        /// </summary>
        public string SuffixClass { get; set; }
        /// <summary>
        /// Initializes a new instance of the UserModuleInfo class.
        /// </summary>
        public UserModuleInfo() { }


    }
}
