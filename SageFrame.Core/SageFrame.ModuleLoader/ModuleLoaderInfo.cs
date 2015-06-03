using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageFrame.ModuleLoader
{
    /// <summary>
    /// Entities class for ModuleLoader.
    /// </summary>
    public class ModuleLoaderInfo
    {
        /// <summary>
        /// Get or set module name.
        /// </summary>
        public string ModuleName { get; set; }
         /// <summary>
        /// Get or set control type.
        /// </summary>
        public int ControlType { get; set; }
        /// <summary>
        /// Get or set control source.
        /// </summary>
        public string ControlSrc { get; set; }
        /// <summary>
        /// Get or set control source.
        /// </summary>
        public string ModuleID { get; set; }
        /// <summary>
        /// Initializes a new instance of ModuleLoaderInfo class.
        /// </summary>
        public ModuleLoaderInfo() { }
    }
}
