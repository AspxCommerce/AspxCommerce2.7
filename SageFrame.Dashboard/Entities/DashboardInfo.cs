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

namespace SageFrame.Dashboard
{
    /// <summary>
    /// This class holds the properties for DashboardInfo
    /// </summary>
    public class DashboardInfo
    {
        /// <summary>
        /// Gets or sets PageID
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// Gets or sets PageOrder
        /// </summary>
        public int PageOrder { get; set; }
        /// <summary>
        /// Gets or sets PageName
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Gets or sets Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets IconFile
        /// </summary>
        public string IconFile { get; set; }
        /// <summary>
        /// Gets or sets Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets KeyWords
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// Gets or sets tab path
        /// </summary>
        public string TabPath { get; set; }
        /// <summary>
        /// Initializes a new instance of the DashboardInfo class.
        /// </summary>
        public DashboardInfo() { }

       
    }
}
