#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace SageFrame.Common
{
    /// <summary>
    /// This class holds the properties for UserModuleInfo.
    /// </summary>
    public class UserModuleInfo
    {
        public int PageID { get; set; }
        public bool IsPageAvailable { get; set; }
        public bool IsPageAccessible { get; set; }
        public int UserModuleID { get; set; }
        public string PaneName { get; set; }
        public int  ModuleOrder { get; set; }
        public bool IsHandHeld { get; set; }
        public string SuffixClass { get; set; }
        public bool ShowHeaderText { get; set; }
        public string HeaderText { get; set; }
        public string ControlSrc { get; set; }
        public bool SupportsPartialRendering { get; set; }
        public int ControlsCount { get; set; }
        public int PortalID { get; set; }
        public int ModuleDefID { get; set; }
        public bool IsEdit { get; set; }
        public string Title { get; set; }
        public string RefreshInterval { get; set; }
        public string KeyWords { get; set; }
        public string Description { get; set; }
        public string UserModuleTitle { get; set; }
        /// <summary>
        /// Initializes a new instance of the UserModuleInfo.
        /// </summary>
        public UserModuleInfo() { }

    }
}
