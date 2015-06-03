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

namespace SageFrame.ExtractTemplate
{
    /// <summary>
    /// Class that contains properties related to extracting page information.
    /// </summary>
    public class ExtractPageInfo
    {
        /// <summary>
        /// Gets or sets page ID.
        /// </summary>
        public int PageID { get; set; }

        /// <summary>
        /// Gets or sets page order.
        /// </summary>
        public int PageOrder { get; set; }

        /// <summary>
        /// Gets or sets numerical value for visible.
        /// </summary>
        public int Isvisible { get; set; }

        /// <summary>
        /// Gets or sets numeric value for parent ID.
        /// </summary>
        public int ParentID { get; set; }
        
        /// <summary>
        /// Gets or sets numerical value for level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Returns or retains true if the link  is disable
        /// </summary>
        public bool DisableLink { get; set; }

        /// <summary>
        /// Returns or retains true if the page is secure.
        /// </summary>
        public bool IsSecure { get; set; }

        /// <summary>
        /// Returns or retains true if the page is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Returns or retains true if the page is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Returns or retains true if the page is modified.
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// Returns or retains true if the page is to be shown in footer.
        /// </summary>
        public bool IsShowInFooter { get; set; }

        /// <summary>
        /// Returns or retains true if the page is required.
        /// </summary>
        public bool IsRequiredPage { get; set; }

        /// <summary>
        /// Gets or sets page name.
        /// </summary>
        public string PageName { get; set; }

        /// <summary>
        /// Gets or sets icon file name.
        /// </summary>
        public string IconFile { get; set; }

        /// <summary>
        /// Gets or sets page head text.
        /// </summary>
        public string PageHeadText { get; set; }

        /// <summary>
        /// Gets or sets page description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets page keywords.
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// Gets or sets page URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets page tab path.
        /// </summary>
        public string TabPath { get; set; }

        /// <summary>
        /// Gets or sets page title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets page added user's name.
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets page updated user's name.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets page deleted user's name.
        /// </summary>
        public string DeletedBy { get; set; }

        /// <summary>
        /// Gets or sets page SEO name.
        /// </summary>
        public string SEOName { get; set; }

        /// <summary>
        /// Gets or sets start datetime. 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets end datetime.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets page added datetime.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets page updated datetime.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets page deleted datatime.
        /// </summary>
        public DateTime DeletedOn { get; set; }

        /// <summary>
        /// Gets or sets  list of ExtractModuleInfo class.
        /// </summary>
        public List<ExtractModuleInfo> ModuleList { get; set; }

        /// <summary>
        /// Gets or sets list of PagePermission class.
        /// </summary>
        public List<PagePermission> PagePermissionList { get; set; }

        /// <summary>
        /// Gets or sets page refresh interval.
        /// </summary>
        public float RefreshInterval { get; set; }

        /// <summary>
        /// Initializes an instance of ExtractPageInfo object.
        /// </summary>
        public ExtractPageInfo()
        {
        }
    }
}
