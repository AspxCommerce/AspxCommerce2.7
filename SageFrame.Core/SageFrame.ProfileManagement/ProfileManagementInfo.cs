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

namespace SageFrame.ProfileManagement
{
    /// <summary>
    /// Enitites class  for profile management.
    /// </summary>
    [Serializable]
    public class ProfileManagementInfo
    {
        /// <summary>
        /// Gets or sets profile's property type ID.
        /// </summary>
        public int PropertyTypeID { get; set; }

        /// <summary>
        /// Gets or sets profile name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets profile ID.
        /// </summary>
        public int ProfileID { get; set; }

        /// <summary>
        /// Gets or sets data type.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Returns or retains true if the  profile is active.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets true if the profile is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets true if the profile is to be modified.
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets updated date.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets deleted date.
        /// </summary>
        public DateTime DeletedOn { get; set; }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Gets or sets profile added user's name.
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets profile updated user's name.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets profile deleted user's name.
        /// </summary>
        public string DeletedBy { get; set; }

        /// <summary>
        /// Gets or sets property type name.
        /// </summary>
        public string PropertyTypeName { get; set; }

        /// <summary>
        /// Gets or sets entty ID.
        /// </summary>
        public int EntryID { get; set; }

        /// <summary>
        /// Gets or sets list of names.
        /// </summary>
        public string ListName { get; set; }

        /// <summary>
        /// Gets or sets values.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets level
        /// </summary>
        public int LEVEL { get; set; }

        /// <summary>
        /// Gets or sets currency code.
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets locale value.
        /// </summary>
        public string DisplayLocale { get; set; }

        /// <summary>
        /// Gets or sets display order.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets definition ID
        /// </summary>
        public int DefinitionID { get; set; }

        /// <summary>
        /// Gets or sets parent ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Returns or retains system list.
        /// </summary>
        public bool SystemList { get; set; }

        /// <summary>
        /// Gets or sets culture name.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets parent key
        /// </summary>
        public string ParentKey { get; set; }

        /// <summary>
        /// Gets or sets parent
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public string ParentList { get; set; }

        /// <summary>
        /// Gets or sets maximum sort order.
        /// </summary>
        public int MaxSortOrder { get; set; }

        /// <summary>
        /// Gets or sets entry count.
        /// </summary>
        public int EntryCount { get; set; }

        /// <summary>
        /// Gets or sets children count.
        /// </summary>
        public int HasChildren { get; set; }

        /// <summary>
        /// Returns or retains true if the profile is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets added date.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets profile value ID.
        /// </summary>
        public int ProfileValueID { get; set; }

        /// <summary>
        /// Gets or sets user profile ID.
        /// </summary>
        public int UserProfileID { get; set; }

        /// <summary>
        /// Gets or sets user's name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets edit user's name.
        /// </summary>
        public string EditUserName { get; set; }

        /// <summary>
        /// Initializes an instance of ProfileManagementInfo class.
        /// </summary>
        public ProfileManagementInfo() { }
    }
}
