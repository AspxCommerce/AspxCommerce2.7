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
    public class ListInfo
    {
        /// <summary>
        /// Gets or sets List name.
        /// </summary>
        public string ListName { get; set; }

        /// <summary>
        /// Gets or sets list value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets list's text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets list's parent ID.
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// Gets or sets list's entries level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        ///  Gets or sets list's currency code.
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets  list's locale display value.
        /// </summary>
        public string DisplayLocale { get; set; }

        /// <summary>
        /// Returns or retains true of list's display order is enabled.
        /// </summary>
        public bool EnableDisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets list's defination ID.
        /// </summary>
        public int DefinitionID { get; set; }

        /// <summary>
        /// Gets or sets list's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Returns or retains true if the list is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets list adding user's name.
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets culture name.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Initializes  an instance of ListInfo class.
        /// </summary>
        public ListInfo() { }


        /// <summary>
        /// Returns or retains true if the list is of system
        /// </summary>
        public bool SystemList { get; set; }

        /// <summary>
        /// Gets or sets the list entries count
        /// </summary>
        public int EntryCount { get; set; }

        /// <summary>
        /// Gets or sets list entry parent if any
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// Gets or sets the parent list
        /// </summary>
        public string ParentList { get; set; }

        /// <summary>
        /// Gets or sets the maximun sorting order
        /// </summary>
        public int MaxSortOrder { get; set; }

        /// <summary>
        /// Gets or sets the list's parents key.
        /// </summary>
        public string ParentKey { get; set; }

        /// <summary>
        /// Gets or sets the list's EntryID.
        /// </summary>
        public int EntryID { get; set; }

        /// <summary>
        /// Gets or sets the list's display order.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the list's children's values.
        /// </summary>
        public int HasChildren { get; set; }

        /// <summary>
        /// Gets or sets the list's added date.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets the list's updated by
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the list's update date.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Initializes an instance of ListInfo class.
        /// </summary>
        /// <param name="_ListName">List name.</param>
        /// <param name="_Value">List value.</param>
        /// <param name="_Text">List text.</param>
        /// <param name="_ParentID">List parent ID.</param>
        /// <param name="_Level">List's level.</param>
        /// <param name="_CurrencyCode"> Curreny code.</param>
        /// <param name="_DisplayLocale">Display locale.</param>
        /// <param name="_EnableDisplayOrder">Enable display order.</param>
        /// <param name="_DefinitionID">Defination ID.</param>
        /// <param name="_Description">List's description.</param>
        /// <param name="_PortalID">Portal ID</param>
        /// <param name="_IsActive">Set true if List is active</param>
        /// <param name="_AddedBy">List adding user's name</param>
        /// <param name="_Culture">Culture name</param>
        public ListInfo(string _ListName, string _Value, string _Text, int _ParentID, int _Level, string _CurrencyCode, string _DisplayLocale, bool _EnableDisplayOrder, int _DefinitionID, string _Description, int _PortalID, bool _IsActive, string _AddedBy, string _Culture)
        {
            this.ListName = _ListName;
            this.Value = _Value;
            this.Text = _Text;
            this.ParentID = _ParentID;
            this.Level = _Level;
            this.CurrencyCode = _CurrencyCode;
            this.DisplayLocale = _DisplayLocale;
            this.EnableDisplayOrder = _EnableDisplayOrder;
            this.DefinitionID = _DefinitionID;
            this.Description = _Description;
            this.PortalID = _PortalID;
            this.IsActive = _IsActive;
            this.AddedBy = _AddedBy;
            this.Culture = _Culture;
        }
    }
}
