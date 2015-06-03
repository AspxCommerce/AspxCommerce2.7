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

namespace SageFrame.Localization
{
    /// <summary>
    /// This class holds the properties for LocalPageInfo.
    /// </summary>
    public class LocalPageInfo
    {
        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets PageID.
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// Gets or sets LocalPageName.
        /// </summary>
        public string LocalPageName { get; set; }
        /// <summary>
        /// Gets or sets PageName.
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Gets or sets CultureCode.
        /// </summary>
        public string  CultureCode { get; set; }
        /// <summary>
        /// Gets or sets LocalPageCaption.
        /// </summary>
        public string LocalPageCaption { get; set; }
        /// <summary>
        /// Initializes a new instance of the LocalPageInfo class.
        /// </summary>
        public LocalPageInfo() { }
        /// <summary>
        /// Initializes a new instance of the LocalPageInfo class.
        /// </summary>
        /// <param name="PageID">PageID</param>
        /// <param name="LocalPageName">LocalPageName</param>
        /// <param name="CultureCode">CultureCode</param>
        public LocalPageInfo(int PageID, string LocalPageName, string CultureCode)
        {
            this.PageID = PageID;
            this.LocalPageName = LocalPageName;
            this.CultureCode = CultureCode;
        }
    }
}
