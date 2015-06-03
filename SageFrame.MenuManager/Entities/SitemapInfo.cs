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

namespace SageFrame.Pages
{
    public class SitemapInfo
    {
        /// <summary>
        /// Get or set page name.
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Get or set TabPath.
        /// </summary>
        public string TabPath { get; set; }
        /// <summary>
        /// Get or set portal name.
        /// </summary>
        public string PortalName { get; set; }
        /// <summary>
        /// Get or set PortalID. 
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Get or set updated date.
        /// </summary>
        public string  UpdatedOn { get; set; }
        /// <summary>
        /// Get or set added date.
        /// </summary>
        public string AddedOn { get; set; }
        /// <summary>
        /// Initializes a new instance of the SitemapInfo class.
        /// </summary>
        public SitemapInfo() { }

    }
}
