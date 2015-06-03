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
using SageFrame.Web.Utilities;
using System.Data;

#endregion


namespace SageFrame.Core.ListManagement
{
    /// <summary>
    /// Business logic class for list management
    /// </summary>
    public class ListManagementController
    {

		/// <summary>
        /// Returns list of object of  ListManagementInfo class.
        /// </summary>
        /// <param name="ListName">List name</param>
        /// <param name="ParentKey">Parent key</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="Culture">Culture</param>
        /// <returns>List of List entries </returns>
        public List<ListManagementInfo> GetListEntriesByNameParentKeyAndPortalID(string ListName, string ParentKey, int PortalID, string Culture)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetListEntriesByNameParentKeyAndPortalID(ListName, ParentKey, PortalID, Culture);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
 /// <summary>
        /// Returns list of entries by  list name, parent key , portalID and cultureCode.
        /// </summary>
        /// <param name="ListName">Name of the list</param>
        /// <param name="ParentKey">parent key</param>
        /// <param name="PortalID">portalID</param>
        /// <param name="Culture">culture</param>
        /// <returns>Returns List of ListManagementInfo class containing  list entries.</returns>
        public List<ListManagementInfo> GetListCopyEntriesByNameParentKeyAndPortalID(string ListName, string ParentKey, int PortalID, string Culture)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetListCopyEntriesByNameParentKeyAndPortalID(ListName, ParentKey, PortalID, Culture);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

           /// <summary>
        /// Returns list of entries by list name ,value and entryID.
        /// </summary>
        /// <param name="ListName">List name.</param>
        /// <param name="Value">Value</param>
        /// <param name="EntryID">EntryID</param>
        /// <param name="Culture">Current culture.</param>
        /// <returns>Returns list of  ListManagementInfo object containing the details</returns>
        public List<ListManagementInfo> GetListEntriesByNameValueAndEntryID(string ListName, string Value, int EntryID, string Culture)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetListEntriesByNameValueAndEntryID(ListName, Value, EntryID, Culture);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
		
		  /// <summary>
        /// Returns list entry detais by list name, value and entryID.
        /// </summary>
        /// <param name="ListName">List name.</param>
        /// <param name="Value">Value</param>
        /// <param name="EntryID">List entryID.</param>
        /// <param name="Culture">Current culture name</param>
        /// <returns>returns list detais</returns>
        public ListManagementInfo GetListEntryDetails(string ListName, string Value, int EntryID, string Culture)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetListEntryDetails(ListName, Value, EntryID, Culture);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
		
		/// <summary>
        /// Adds new list.
        /// </summary>
        /// <param name="objList">Object of class ListInfo.</param>
        /// <returns>Returns entry ID if the list is insterted successfully.</returns>
        public int AddNewList(ListInfo objList)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.AddNewList(objList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
		
		   /// <summary>
        /// Updates list entry
        /// </summary>
        /// <param name="entryId">EntryID</param>
        /// <param name="value">list value</param>
        /// <param name="text">text</param>
        /// <param name="currencyCode">Currency code</param>
        /// <param name="displayLocale">Localized value</param>
        /// <param name="Description">Description of list</param>
        /// <param name="isActive">Set true if the list entry is active</param>
        /// <param name="createdBy">User's name who created the list</param>
        /// <param name="CurrentCultureName"> Current culture name</param>
        public void UpdateListEntry(int entryId, string value, string text, string currencyCode, string displayLocale, string Description, bool isActive, string createdBy, string CurrentCultureName)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                objProvider.UpdateListEntry(entryId, value, text, currencyCode, displayLocale, Description, isActive, createdBy, CurrentCultureName);
            }
            catch (Exception)
            {

                throw;
            }
        }

		/// <summary>
        /// Deletes list entry.
        /// </summary>
        /// <param name="EntryId">List entry ID.</param>
        /// <param name="DeleteChild">Set true if have to delete child entries.</param>
        /// <param name="Culture">Current culture.</param>
        /// <returns>Returns true if list is deleted</returns>
        public bool DeleteListEntry(int EntryId, bool DeleteChild, string Culture)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.DeleteListEntry(EntryId, DeleteChild, Culture);
            }
            catch (Exception)
            {

                throw;
            }

        }
		   /// <summary>
        /// Sorts list's order.
        /// </summary>
        /// <param name="EntryId">List EntryID</param>
        /// <param name="MoveUp">Set true if move up is true.</param>
        /// <param name="Culture">Current culture.</param>
        public void SortList(int EntryId, bool MoveUp, string Culture)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                objProvider.SortList(EntryId, MoveUp, Culture);
            }
            catch (Exception)
            {

                throw;
            }
        }

/// <summary>
        /// Returns entries by list name , parentkey, portalID and culture.
        /// </summary>
        /// <param name="ListName">List name.</param>
        /// <param name="ParentKey">Parent list key.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="Culture">Current culture.</param>
        /// <returns>Returns list of  ListManagementInfo object containing the details</returns>
        public List<ListManagementInfo> GetEntriesByNameParentKeyAndPortalID(string ListName, string ParentKey, int PortalID, string Culture)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetEntriesByNameParentKeyAndPortalID(ListName, ParentKey, PortalID, Culture);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
		
		/// <summary>
        /// Returns list of list's entries .
        /// </summary>
        /// <param name="ListName">List name.</param>
        /// <param name="EntryID">List's entry ID.</param>
        /// <param name="Culture">Current culture.</param>
        /// <returns>List of list's entries</returns>
        public List<ListManagementInfo> GetListEntrybyNameAndID(string ListName, int EntryID, string Culture)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetListEntrybyNameAndID(ListName, EntryID, Culture);
            }
            catch (Exception)
            {

                throw;
            }
        }

 /// <summary>
        /// Inserts form values.
        /// </summary>
        /// <param name="_FormTitle">Form's title.</param>
        /// <param name="_FormInformation">Form's information.</param>
        /// <param name="_Name">Name</param>
        /// <param name="_PermanentCountry">Permanent country.</param>
        /// <param name="_PermanentState">Permanent state.</param>
        /// <param name="_PermanentCity">Permanent city.</param>
        /// <param name="_PermanentZipCode">Permanent zip code.</param>
        /// <param name="_PermanentPostCode">Permanent post code.</param>
        /// <param name="_PermanentAddress">Permanent addess.</param>
        /// <param name="_TemporaryCountry">Temporary country.</param>
        /// <param name="_TemporaryState">Temporary state</param>
        /// <param name="_TemporaryCity">Temporary city</param>
        /// <param name="_TemporaryZipCode">Temporary zip code</param>
        /// <param name="_TemporaryPostalCode">Temporary postal code</param>
        /// <param name="_TemporaryAddress">Temporary address</param>
        /// <param name="_Email1">Email 1</param>
        /// <param name="_Email2">Email 2</param>
        /// <param name="_Phone1">Phone 1</param>
        /// <param name="_Phone2">Phone 2</param>
        /// <param name="_Mobile">Mobile no.</param>
        /// <param name="_Company">Company name.</param>
        /// <param name="_Website">Website URL.</param>
        /// <param name="_Message">Message.</param>
        /// <param name="_Attachment">Attachment.</param>
        /// <param name="status">Status.</param>
        /// <param name="date">Date.</param>
        /// <param name="PortalID">portalID.</param>
        /// <param name="username">UserName</param>
        public void FeedbackFormAdd(string _FormTitle, string _FormInformation, string _Name, string _PermanentCountry, string _PermanentState,
                  string _PermanentCity, string _PermanentZipCode, string _PermanentPostCode, string _PermanentAddress, string _TemporaryCountry,
                  string _TemporaryState, string _TemporaryCity, string _TemporaryZipCode, string _TemporaryPostalCode,
                  string _TemporaryAddress, string _Email1, string _Email2, string _Phone1, string _Phone2, string _Mobile,
                  string _Company, string _Website, string _Message, string _Attachment, bool status, DateTime date, int PortalID, string username)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                objProvider.FeedbackFormAdd(_FormTitle, _FormInformation, _Name, _PermanentCountry, _PermanentState,
                  _PermanentCity, _PermanentZipCode, _PermanentPostCode, _PermanentAddress, _TemporaryCountry,
                  _TemporaryState, _TemporaryCity, _TemporaryZipCode, _TemporaryPostalCode,
                  _TemporaryAddress, _Email1, _Email2, _Phone1, _Phone2, _Mobile,
                  _Company, _Website, _Message, _Attachment, status, date, PortalID, username);
            }
            catch (Exception)
            {

                throw;
            }
        }

		/// <summary>
        /// Returns feedback of the item
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="CultureName">Current culture name</param>
        /// <returns>Feedback item</returns>
        public DataTable FeedbackItemGet(int PortalID, string CultureName)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.FeedbackItemGet(PortalID, CultureName);
            }
            catch (Exception)
            {

                throw;
            }
        }

		  /// <summary>
        /// Returns feedback setting value by portal ID.
        /// </summary>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>Feedback setting value</returns>
        public string GetFeedbackSettingValueList(int PortalID)
        {
            ListManagementProvider objProvider = new ListManagementProvider();
            return objProvider.GetFeedbackSettingValueList(PortalID);
        }
		
		   /// <summary>
        /// Returns list of defult list
        /// </summary>
        /// <param name="CultureName">Current culture name</param>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>List of list's default values</returns>
        public List<ListInfo> GetDefaultList(string CultureName, int PortalID)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetDefaultList(CultureName, PortalID);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
		
		  /// <summary>
        /// Returns list of list by portal ID and culture name.
        /// </summary>
        /// <param name="CultureName">culture name.</param>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>List of list</returns>
        public List<ListInfo> GetListByPortalID(string CultureName, int PortalID)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetListByPortalID(CultureName, PortalID);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

		   /// <summary>
        /// Returns list of  list entries.
        /// </summary>
        /// <param name="listName">List name.</param>
        /// <param name="parentId">List's parent ID.</param>
        /// <param name="portalID">Portal ID</param>
        /// <param name="cultureName">Culture name</param>
        /// <returns>List of List's entities</returns>
        public List<ListInfo> GetListInfo(string listName, string parentId, int portalID, string cultureName)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetListInfo(listName, parentId, portalID, cultureName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
		
		     /// <summary>
        /// Returns the list of list entries.
        /// </summary>
        /// <param name="listName">List name.</param>
        /// <param name="parentId">List's parent ID.</param>
        /// <param name="portalID">Portal ID</param>
        /// <param name="cultureName">Culture name.</param>
        /// <returns>List of list entries</returns>
        public DataSet GetListInfoInDataSet(string listName, string parentId, int portalID, string cultureName)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetListInfoInDataSet(listName, parentId, portalID, cultureName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
		
		  /// <summary>
        /// Returns list of list entries 
        /// </summary>
        /// <param name="entryId">List's Entry ID</param>
        /// <param name="cultureName">Culture name</param>
        /// <returns>List of list's entities</returns>
        public List<ListManagementInfo> GetListEntryByParentID(int entryId, string cultureName)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetListEntryByParentID(entryId, cultureName);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
		
		  /// <summary>
        /// Returns list of unique list name.
        /// </summary>
        /// <param name="listName">List's name.</param>
        /// <param name="culture">Culture name.</param>
        /// <param name="parentId">List's parent ID</param>
        /// <returns>list of unique list name.</returns>
        public List<ListInfo> GetListInfo(string listName, string culture, int parentId)
        {
            try
            {
                ListManagementProvider objProvider = new ListManagementProvider();
                return objProvider.GetListInfo(listName, culture, parentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
