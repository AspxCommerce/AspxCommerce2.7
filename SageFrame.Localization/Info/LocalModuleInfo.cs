using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Localization.Info
{
    /// <summary>
    /// This class holds the properties for LocalModuleInfo.
    /// </summary>
    public class LocalModuleInfo
    {
        /// <summary>
        /// Gets or sets UserModuleID
        /// </summary>
        public int UserModuleID { get; set; }
        /// <summary>
        /// Gets or sets PortalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Gets or sets UserModuleTitle.
        /// </summary>
        public string UserModuleTitle { get; set; }
        /// <summary>
        /// Gets or sets LocalModuleTitle.
        /// </summary>
        public string LocalModuleTitle { get; set; }
        /// <summary>
        /// Gets or sets CultureCode.
        /// </summary>
        public string CultureCode { get; set; }
        /// <summary>
        /// Gets or sets UserModules.
        /// </summary>
        public string UserModules { get; set; }
        /// <summary>
        /// Initializes a new instance of the LocalModuleInfo class.
        /// </summary>
        public LocalModuleInfo() { }
    }
}
