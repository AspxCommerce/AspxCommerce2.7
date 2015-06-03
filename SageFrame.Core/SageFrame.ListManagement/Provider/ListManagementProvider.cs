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
using System.Data.SqlClient;
using System.Data;
using SageFrame.ListManagement;

#endregion


namespace SageFrame.Core.ListManagement
{
    /// <summary>
    /// Manupulates data for ListManagementController.
    /// </summary>
    public class ListManagementProvider
    {
        /// <summary>
        /// Connects to database and returns list of object of  ListManagementInfo class.
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
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ListName", ListName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentKey", ParentKey));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));
                return SQLH.ExecuteAsList<ListManagementInfo>("[dbo].[sp_GetListEntriesByNameParentKeyAndPortalID]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and returns list of entries by  list name, parent key , portalID and cultureCode.
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
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ListName", ListName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentKey", ParentKey));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));
                return SQLH.ExecuteAsList<ListManagementInfo>("[dbo].[sp_GetListEntriesByNameParentKeyAndPortalID]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and adds new list.
        /// </summary>
        /// <param name="objList">Object of class ListInfo.</param>
        /// <returns>Returns entry ID if the list is insterted successfully.</returns>
        public int AddNewList(ListInfo objList)
        {
            string sp = "[dbo].[usp_ListEntryAdd]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ListName", objList.ListName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Value", objList.Value));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Text", objList.Text));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentID", objList.ParentID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Level", objList.Level));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CurrencyCode", objList.CurrencyCode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayLocale", objList.DisplayLocale));
                ParamCollInput.Add(new KeyValuePair<string, object>("@EnableDisplayOrder", objList.EnableDisplayOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DefinitionID", objList.DefinitionID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Description", objList.Description));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", objList.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", objList.IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", objList.AddedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", objList.Culture));

                int ListID = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@ListID");
                return ListID;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Connects to database and updates list entry
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

            string sp = "[dbo].[sp_ListEntryUpdate]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@EntryID", entryId));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Value", value));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Text", text));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CurrencyCode", currencyCode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayLocale", displayLocale));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Description", Description));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", createdBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", CurrentCultureName));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and deletes list entry.
        /// </summary>
        /// <param name="EntryId">List entry ID.</param>
        /// <param name="DeleteChild">Set true if have to delete child entries.</param>
        /// <param name="Culture">Current culture.</param>
        /// <returns>Returns true if list is deleted</returns>
        public bool DeleteListEntry(int EntryId, bool DeleteChild, string Culture)
        {
            string sp = "[dbo].[sp_ListEntryDeleteByID]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@EntryID", EntryId));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DeleteChild", DeleteChild));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));

                return sagesql.ExecuteNonQueryAsBool(sp, ParamCollInput, "@IsExist");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Connects to a database and sorts list's order.
        /// </summary>
        /// <param name="EntryId">List EntryID</param>
        /// <param name="MoveUp">Set true if move up is true.</param>
        /// <param name="Culture">Current culture.</param>
        public void SortList(int EntryId, bool MoveUp, string Culture)
        {

            string sp = "[dbo].[sp_ListSortOrderUpdate]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@EntryID", EntryId));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MoveUp", MoveUp));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to a database and returns list entry detais by list name, value and entryID.
        /// </summary>
        /// <param name="ListName">List name.</param>
        /// <param name="Value">Value</param>
        /// <param name="EntryID">List entryID.</param>
        /// <param name="Culture">Current culture name</param>
        /// <returns>returns list detais</returns>
        public ListManagementInfo GetListEntryDetails(string ListName, string Value, int EntryID, string Culture)
        {
            SqlDataReader reader = null;
            try
            {

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ListName", ListName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Value", Value));
                ParamCollInput.Add(new KeyValuePair<string, object>("@EntryID", EntryID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));


                reader = SQLH.ExecuteAsDataReader("[dbo].[sp_GetListEntrybyNameValueAndEntryID]", ParamCollInput);
                ListManagementInfo objList = new ListManagementInfo();

                while (reader.Read())
                {

                    objList.ListName = reader["ListName"].ToString();
                    objList.Value = reader["Value"].ToString();
                    objList.CurrencyCode = reader["CurrencyCode"].ToString();
                    objList.DisplayLocale = reader["DisplayLocale"].ToString();
                    objList.IsActive = bool.Parse(reader["IsActive"].ToString());
                    objList.Text = reader["Text"].ToString();

                }
                return objList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

        }

        /// <summary>
        /// Connects to a database and returns list of entries by list name ,value and entryID.
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

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ListName", ListName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Value", Value));
                ParamCollInput.Add(new KeyValuePair<string, object>("@EntryID", EntryID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));


                return SQLH.ExecuteAsList<ListManagementInfo>("[dbo].[sp_GetListEntrybyNameValueAndEntryID]", ParamCollInput);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// Connects to database and returns entries by list name , parentkey, portalID and culture.
        /// </summary>
        /// <param name="ListName">List name.</param>
        /// <param name="ParentKey">Parent list key.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="Culture">Current culture.</param>
        /// <returns>Returns list of  ListManagementInfo object containing the details</returns>
        public List<ListManagementInfo> GetEntriesByNameParentKeyAndPortalID(string ListName, string ParentKey, int PortalID, string Culture)
        {
            SqlDataReader reader = null;
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ListName", ListName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentKey", ParentKey));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));
                reader = SQLH.ExecuteAsDataReader("[dbo].[sp_GetListEntriesByNameParentKeyAndPortalID]", ParamCollInput);
                List<ListManagementInfo> lstList = new List<ListManagementInfo>();
                while (reader.Read())
                {
                    ListManagementInfo objList = new ListManagementInfo();
                    objList.ParentID = int.Parse(reader["ParentID"].ToString());
                    objList.Level = int.Parse(reader["Level"].ToString());
                    objList.DefinitionID = int.Parse(reader["DefinitionID"].ToString());
                    objList.PortalID = int.Parse(reader["PortalID"].ToString());
                    lstList.Add(objList);
                }
                return lstList; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Connects to database and returns list of list's entries .
        /// </summary>
        /// <param name="ListName">List name.</param>
        /// <param name="EntryID">List's entry ID.</param>
        /// <param name="Culture">Current culture.</param>
        /// <returns>List of list's entries</returns>
        public List<ListManagementInfo> GetListEntrybyNameAndID(string ListName, int EntryID, string Culture)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ListName", ListName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@EntryID", EntryID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));
                return SQLH.ExecuteAsList<ListManagementInfo>("[dbo].[sp_GetListEntrybyNameAndID]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        /// <summary>
        /// Connects to database and inserts form values.
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
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();

                ParamCollInput.Add(new KeyValuePair<string, object>("@FormInfo", _FormInformation));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FormTitle", _FormTitle));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Name", _Name));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PermanentCountry", _PermanentCountry));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PermanentState", _PermanentState));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PermanentCity", _PermanentCity));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PermanentZipCode", _PermanentZipCode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PermanentPostCode", _PermanentPostCode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PermanentAddress", _PermanentAddress));
                ParamCollInput.Add(new KeyValuePair<string, object>("@TemporaryCountry", _TemporaryCountry));
                ParamCollInput.Add(new KeyValuePair<string, object>("@TemporaryState", _TemporaryState));
                ParamCollInput.Add(new KeyValuePair<string, object>("@TemporaryCity", _TemporaryCity));
                ParamCollInput.Add(new KeyValuePair<string, object>("@TemporaryZipCode", _TemporaryZipCode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@TemporaryPostCode", _TemporaryPostalCode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@TemporaryAddress", _TemporaryAddress));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Email1", _Email1));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Email2", _Email2));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Phone1", _Phone1));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Phone2", _Phone2));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Mobile", _Mobile));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Company", _Company));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Website", _Website));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Message", _Message));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Attachment", _Attachment));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", status));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", date));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", username));
                SQLH.ExecuteNonQuery("[dbo].[sp_FeedbackFormAdd]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and returns feedback of the item
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="CultureName">Current culture name</param>
        /// <returns>Feedback item</returns>
        public DataTable FeedbackItemGet(int PortalID, string CultureName)
        {

            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
                return SQLH.ExecuteAsDataSet("[dbo].[sp_FeedbackItemGet]", ParamCollInput).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and returns feedback setting value by portal ID.
        /// </summary>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>Feedback setting value</returns>
        public string GetFeedbackSettingValueList(int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsObject<FeedbackSettingInfo>("[dbo].[sp_FeedbackSettingValueList]", ParamCollInput).SettingValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns list of defult list
        /// </summary>
        /// <param name="CultureName">Current culture name</param>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>List of list's default values</returns>
        public List<ListInfo> GetDefaultList(string CultureName, int PortalID)
        {

            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", CultureName));

                SQLHandler sqlH = new SQLHandler();
                List<ListInfo> defaultList = sqlH.ExecuteAsList<ListInfo>("dbo.sp_GetDefaultList", ParaMeterCollection);
                return defaultList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and returns list of list by portal ID and culture name.
        /// </summary>
        /// <param name="CultureName">culture name.</param>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>List of list</returns>
        public List<ListInfo> GetListByPortalID(string CultureName, int PortalID)
        {

            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", CultureName));

                SQLHandler sqlH = new SQLHandler();
                List<ListInfo> defaultList = sqlH.ExecuteAsList<ListInfo>("dbo.sp_GetListsByPortalID", ParaMeterCollection);
                return defaultList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        /// <summary>
        /// Connects to database and returns list of  list entries.
        /// </summary>
        /// <param name="listName">List name.</param>
        /// <param name="parentId">List's parent ID.</param>
        /// <param name="portalID">Portal ID</param>
        /// <param name="cultureName">Culture name</param>
        /// <returns>List of List's entities</returns>
        public List<ListInfo> GetListInfo(string listName, string parentId,int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@ListName", listName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@ParentKey", parentId));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", cultureName));

                SQLHandler sqlH = new SQLHandler();
                List<ListInfo> listParentEntry = new List<ListInfo>();
                listParentEntry = sqlH.ExecuteAsList<ListInfo>("dbo.sp_GetListEntriesByNameParentKeyAndPortalID", ParaMeterCollection);
                return listParentEntry;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connect to database and returns the list of list entries.
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
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@ListName", listName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@ParentKey", parentId));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", cultureName));

                SQLHandler sqlH = new SQLHandler();
                DataSet ds = new DataSet();
                ds = sqlH.ExecuteAsDataSet("dbo.sp_GetListEntriesByNameParentKeyAndPortalID", ParaMeterCollection);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and returns list of list entries 
        /// </summary>
        /// <param name="entryId">List's Entry ID</param>
        /// <param name="cultureName">Culture name</param>
        /// <returns>List of list's entities</returns>
        public List<ListManagementInfo> GetListEntryByParentID(int entryId, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@EntryID", entryId));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", cultureName));
                SQLHandler sqlH = new SQLHandler();
                List<ListManagementInfo> objMngInfoLst = new List<ListManagementInfo>();
                objMngInfoLst = sqlH.ExecuteAsList<ListManagementInfo>("dbo.sp_GetListEntrybyParentId", ParaMeterCollection);
                return objMngInfoLst;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and returns list of unique list name.
        /// </summary>
        /// <param name="listName">List's name.</param>
        /// <param name="culture">Culture name.</param>
        /// <param name="parentId">List's parent ID</param>
        /// <returns>list of unique list name.</returns>
        public List<ListInfo> GetListInfo(string listName, string culture, int parentId)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ListName", listName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", culture));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ParentId", parentId));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ListInfo>("sp_GetListForUniqueness", ParaMeterCollection);
        }
    }
}
