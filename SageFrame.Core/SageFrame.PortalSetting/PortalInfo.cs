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


namespace SageFrame.PortalSetting
{
    /// <summary>
    /// Enitites class for portal.
    /// </summary>
    public class PortalInfo
    {
        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalID { get; set; }
        
        /// <summary>
        /// Gets or sets portal name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets portal SEO name.
        /// </summary>
        public string SEOName { get; set; }

        /// <summary>
        /// Returns or retains true if the portal is parent.
        /// </summary>
        public bool IsParent { get; set; }

        /// <summary>
        /// Gets or sets portal's default page.
        /// </summary>
        public string DefaultPage { get; set; }
        
        /// <summary>
        /// Gets or sets user's name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Returns or retains true if the portal module is admin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets Module ID.
        /// </summary>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets portal's friendly name.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets portal's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets portal's version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Returns or retains true if the portal module is active.
        /// </summary>
        public bool IsPortalModuleActive { get; set; }

        /// <summary>
        /// Gets or sets parent's portal ID.
        /// </summary>
        public string ParentPortalName { get; set; }

        /// <summary>
        /// Initializes an instance of  PortalInfo class.
        /// </summary>
        public PortalInfo() { }
    }
}
