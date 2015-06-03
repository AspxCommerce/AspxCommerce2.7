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
    /// This class holds the properties for sitemap.
    /// </summary>
    public class SiteMapInfo
    {   
        
        /// <summary>
        /// Gets and sets PageID.
        /// </summary>
        public string PageID { get; set; }
        /// <summary>
        /// Gets and sets PageName.
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Gets and sets sitemap path.
        /// </summary>
        public string TabPath { get; set; }
        /// <summary>
        /// Gets and sets SEOName(pagename).
        /// </summary>
        public string SEOName { get; set; }
        /// <summary>
        /// Gets and sets page hierarchy .
        /// </summary>
        public string LevelPageName { get; set; }
        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets updated date.
        /// </summary>
        public DateTime UpdatedOn { get; set; }
        /// <summary>
        /// Gets or sets added time.
        /// </summary>
        public DateTime AddedOn { get; set; }
        /// <summary>
        /// Gets or sets change frequency.
        /// </summary>
        public string ChangeFreq { get; set; }
       /// <summary>
        /// Initializes a new instance of the SiteMapInfo class.
       /// </summary>
        public SiteMapInfo() { }
        /// <summary>
        /// Initializes a new instance of the SiteMapInfo class.
        /// </summary>
        /// <param name="PageID">PageID</param>
        /// <param name="PageName">PageName</param>
        /// <param name="TabPath">TabPath</param>
        /// <param name="SEOName">SEOName</param>
        /// <param name="LevelPageName">LevelPageName</param>
        /// <param name="Description">Description</param>
        /// <param name="UpdatedOn">UpdatedOn</param>
        /// <param name="AddedOn">AddedOn</param>
        
        public SiteMapInfo(string PageID, string PageName, string TabPath, string SEOName, string LevelPageName, string Description, DateTime UpdatedOn,DateTime AddedOn) 
        {
            this.PageID = PageID;
            this.PageName = PageName;
            this.TabPath = TabPath;
            this.SEOName = SEOName;
            this.LevelPageName = LevelPageName;
            this.Description = Description;
            this.UpdatedOn = UpdatedOn;
            this.AddedOn = AddedOn;
        
        }

    }
}
