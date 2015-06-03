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

namespace SageFrame.Core.ListManagement
{
    /// <summary>
    /// List management entity class
    /// </summary>
    public class ListManagementInfo
    {
        /// <summary>
        /// Gets or sets the list's entry ID
        /// </summary>
        public int EntryID { get; set; }

        /// <summary>
        /// Gets or sets list's name
        /// </summary>
        public string ListName { get; set; }

        /// <summary>
        /// Gets or sets list's value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets list's text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets list's level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets list's currency code
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets list's localize value
        /// </summary>
        public string DisplayLocale { get; set; }

        /// <summary>
        /// Gets or sets list's display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets list's defination ID
        /// </summary>
        public int DefinitionID { get; set; }

        /// <summary>
        /// Gets or sets list's parent ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// Gets or sets list's decription
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets portalID
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Returns or retains true if the list of system
        /// </summary>
        public bool SystemList { get; set; }

        /// <summary>
        /// Gets or sets list's culture name
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets list's parent key
        /// </summary>
        public string ParentKey { get; set; }

        /// <summary>
        /// Gets or sets list's parent
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// Gets or sets list's parent list
        /// </summary>
        public string ParentList { get; set; }

        /// <summary>
        /// Gets or sets list's maimum order
        /// </summary>
        public int MaxSortOrder { get; set; }

        /// <summary>
        /// Gets or sets list's entry count
        /// </summary>
        public int EntryCount { get; set; }

        /// <summary>
        /// Gets or sets list's children name
        /// </summary>
        public int HasChildren { get; set; }

        /// <summary>
        /// Returns or retains true if the list is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets list's added user's name
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets list added date
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets list's updated user's name
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets list updated date
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Returns or retains true if the display order is enabled
        /// </summary>
        public bool EnableDisplayOrder { get; set; }   
    }
}
