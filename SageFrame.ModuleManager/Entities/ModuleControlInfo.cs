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

namespace SageFrame.ModuleControls
{
    /// <summary>
    /// This class holds the properties for ModuleControlInfo class.
    /// </summary>
    public class ModuleControlInfo
    {
        /// <summary>
        /// Get or set UserModuleID.
        /// </summary>
        public string UserModuleID { get; set; }
        /// <summary>
        /// Get or set ModuleDefID.
        /// </summary>
        public string ModuleDefID { get; set; }
        /// <summary>
        /// Get or set user control title.
        /// </summary>
        public string ControlTitle { get; set; }
        /// <summary>
        /// Get or set control type.
        /// </summary>
        public string ControlType { get; set; }
        /// <summary>
        /// Get or set control source.
        /// </summary>
        public string ControlSrc { get; set; }
        /// <summary>
        /// Initializes a new instance of ModuleControlInfo class.
        /// </summary>
        public ModuleControlInfo() { }
    }
}
