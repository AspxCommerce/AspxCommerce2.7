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
    /// 
    /// </summary>
    public class LayoutMgrInfo
    {
        /// <summary>
        /// Get or set ModuleID.
        /// </summary>
        public string ModuleID { get; set; }
        /// <summary>
        /// Get or setfriendly name.
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// Get or set module order.
        /// </summary>
        public string ModuleOrder { get; set; }
        /// <summary>
        /// Get or set PortelID.
        /// </summary>
        public int PortelID { get; set; }
        /// <summary>
        /// Get or set ModuleDefID.
        /// </summary>
        public int  ModuleDefID { get; set; }
        /// <summary>
        /// Get or set UserModuleID.
        /// </summary>
        public int UserModuleID { get; set; }
        /// <summary>
        /// Get or set module name.
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// Get or set
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Get or set module description.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Get or set module version.
        /// </summary>
        public string PaneName { get; set; }
        /// <summary>
        /// Get or set new module id.
        /// </summary>
        public string NewModuleID { get; set; }
        /// <summary>
        /// Initializes a new instance of the LayoutMgrInfo class.
        /// </summary>
        public LayoutMgrInfo() { }
    }
}
