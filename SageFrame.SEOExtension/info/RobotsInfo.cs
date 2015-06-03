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

namespace SageFrame.SEOExtension
{    
    /// <summary>
    /// This class holds the properties for robots.
    /// </summary>
    public class RobotsInfo
    {
        /// <summary>
        /// Gets or sets PageName.
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Gets or sets robots path.
        /// </summary>
        public string TabPath { get; set; }
        /// <summary>
        /// Gets or sets SEOName(page name).
        /// </summary>
        public string SEOName { get; set; }
        /// <summary>
        /// Gets or sets Description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets UserAgent(search engines).
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// Gets or sets PortalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Gets or set PagePath.
        /// </summary>
        public string PagePath { get; set; }  
        /// <summary>
        /// Initializes a new instance of the RobotsInfo class.
        /// </summary>
   
        public RobotsInfo() { }
        
        /// <summary>
        /// Parameterize constructor which assigns robots path(tab path).
        /// </summary>
        /// <param name="PageName">PageName</param>
        /// <param name="TabPath">TabPath</param>
        /// <param name="SEOName">SEOName</param>
        /// <param name="Description">Description</param>
        public RobotsInfo(string PageName, string TabPath, string SEOName, string Description) 
        {
            this.TabPath = TabPath;

        }

        /// <summary>
        /// Parameterize constructor which assigns portalid,user agent(search engines) and page path.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserAgent">UserAgent</param>
        /// <param name="PagePath">PagePath</param>
        public RobotsInfo(int PortalID, string UserAgent, string PagePath) 
        {
            this.PortalID = PortalID;
            this.UserAgent = UserAgent;
            this.PagePath = PagePath;

        }
    }
}
