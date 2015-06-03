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

#endregion


namespace SageFrame.ProfileManagement
{
    /// <summary>
    /// Manipulates data to manage user profile.
    /// </summary>
    public class ProfileManagementProvider
    {

        /// <summary>
        /// Connects to database and returns list of profile.
        /// </summary>
        /// <returns>List of profile details</returns>
        public List<ProfileManagementInfo> GetPropertyTypeList()
        {
            string sp = "sp_PropertyTypeList";
            SQLHandler sageSql = new SQLHandler();
            try
            {
                return sageSql.ExecuteAsList<ProfileManagementInfo>(sp);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        
        /// <summary>
        /// Connects to database and returns list of profile details.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of profile details.</returns>
        public List<ProfileManagementInfo> GetProfileList(int PortalID)
        {
            try
            {

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsList<ProfileManagementInfo>("[dbo].[sp_ProfileList]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
      
        /// <summary>
        /// Connects to database and adds user's profile.
        /// </summary>
        /// <param name="Name">User's name.</param>
        /// <param name="PropertyTypeID">Property type ID.</param>
        /// <param name="DataType">Data type.</param>
        /// <param name="IsRequired">Set true if profile is required.</param>
        /// <param name="IsActive">Set true if profile is active.</param>
        /// <param name="AddedOn">Profile added date.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="AddedBy">Profile added user's name.</param>
        /// <returns>Returns ProfileID</returns>
        public int AddProfile(string Name, int PropertyTypeID, string DataType, bool IsRequired, bool IsActive, DateTime AddedOn, int PortalID, string AddedBy)
        {
            try
            {
                string sp = "[dbo].[sp_ProfileAdd]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();

                ParamCollInput.Add(new KeyValuePair<string, object>("@Name", Name));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PropertyTypeID", PropertyTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DataType", DataType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsRequired", IsRequired));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));

                int PID = SQLH.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@ProfileID");
                return PID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database  and adds profile details.
        /// </summary>
        /// <param name="ProfileID">Profile ID.</param>
        /// <param name="Name">Profile user's name.</param>
        /// <param name="IsActive">Set true if profile is active.</param>
        /// <param name="AddedOn">Profile added user's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="AddedBy">Profile added user's name.</param>
        /// <returns>Returns ProfileValueID.</returns>
        public int AddProfileValue(int? ProfileID, string Name, bool IsActive, DateTime? AddedOn, int? PortalID, string AddedBy)
        {
            try
            {
                string sp = "[dbo].[sp_ProfileAdd]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ProfileID", ProfileID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Name", Name));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));

                int PVID = SQLH.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@ProfileValueID");
                return PVID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        /// <summary>
        /// Connects to database and deletes profile by profile ID.
        /// </summary>
        /// <param name="ProfileID">Profile ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UserName">User's name.</param>
        public void DeleteProfileValueByProfileID(int ProfileID, int PortalID, string UserName)
        {
            try
            {
                string sp = "[dbo].[sp_ProfileValueDeleteByProfileID]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();

                ParamCollInput.Add(new KeyValuePair<string, object>("@ProfileID", ProfileID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DeletedBy", UserName));
                SQLH.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and deletes profile
        /// </summary>
        /// <param name="DeleteID">Profile ID</param>
        /// <param name="UserName">User's name.</param>
        public void DeleteProfileByProfileID(int DeleteID,  string UserName)
        {
            try
            {
                string sp = "[dbo].[sp_ProfileDeleteByProfileID]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();

                ParamCollInput.Add(new KeyValuePair<string, object>("@ProfileID", DeleteID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DeletedBy", UserName));
               
                SQLH.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }
        
        /// <summary>
        /// Connects to database and returns profile by profile ID.
        /// </summary>
        /// <param name="EditID">Profile ID.</param>
        /// <returns>Profile detail</returns>
        public ProfileManagementInfo GetProfileByProfileID(int EditID)
        {
            SqlDataReader reader = null;
            try
            {
                string sp = "[dbo].[sp_ProfileGetByProfileID]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ProfileID", EditID));

                reader = SQLH.ExecuteAsDataReader(sp, ParamCollInput);
                ProfileManagementInfo objList = new ProfileManagementInfo();

                while (reader.Read())
                {

                    objList.ProfileID = int.Parse(reader["ProfileID"].ToString());
                    objList.Name = reader["Name"].ToString();
                    objList.PropertyTypeID = int.Parse(reader["PropertyTypeID"].ToString());
                    objList.DataType = reader["DataType"].ToString();
                    objList.IsRequired = bool.Parse(reader["IsRequired"].ToString());
                    objList.DisplayOrder = int.Parse(reader["DisplayOrder"].ToString());
                    objList.IsActive = bool.Parse(reader["IsActive"].ToString());
                    objList.IsDeleted = bool.Parse(reader["IsDeleted"].ToString());
                    objList.IsDeleted = bool.Parse(reader["IsModified"].ToString());
                    objList.UpdatedOn = DateTime.Parse(reader["UpdatedOn"].ToString());
                    objList.PortalID = int.Parse(reader["PortalID"].ToString());
                    objList.UpdatedBy = reader["UpdatedBy"].ToString();

                }
                return objList;


            }
            catch (Exception)
            {

                throw;
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
        /// Connects to database and updates profile.
        /// </summary>
        /// <param name="ProfileID">Profile ID.</param>
        /// <param name="Name">Profile name.</param>
        /// <param name="PropertyTypeID">Property type ID.</param>
        /// <param name="DataType">Data type.</param>
        /// <param name="IsRequired">Set true if profile is required.</param>
        /// <param name="IsActive">Set true if profile is active.</param>
        /// <param name="IsModified">Set true if profile is modified.</param>
        /// <param name="UpdatedOn">Profile updated date.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UpdatedBy">Profile updated user's name.</param>
        public void UpdateProfile(int ProfileID, string Name, int PropertyTypeID, string DataType, bool IsRequired, bool IsActive, bool IsModified, DateTime UpdatedOn, int PortalID, string UpdatedBy)
        {
            try
            {
                string sp = "[dbo].[sp_ProfileUpdate]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ProfileID", ProfileID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Name", Name));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PropertyTypeID", PropertyTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DataType", DataType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsRequired", IsRequired));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsModified", IsModified));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedOn", UpdatedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));
                SQLH.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
            
        /// <summary>
        /// Connects to database and returns active profile value by profile ID.
        /// </summary>
        /// <param name="ProfileID">Profile ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of profile management.</returns>
        public List<ProfileManagementInfo> GetActiveProfileValueByProfileID(int ProfileID ,int PortalID)
        {
            try
            {

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ProfileID", ProfileID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsList<ProfileManagementInfo>("[dbo].[sp_ProfileValueGetActiveByProfileID]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and updates profile display order only if it is active .
        /// </summary>
        /// <param name="ProfileID">Profile ID.</param>
        /// <param name="DisplayOrder">Display order.</param>
        /// <param name="IsActive">Set true if profile is active.</param>
        /// <param name="UpdatedOn">Profile updated date.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="Username">User's name.</param>
        public void UpdateProfileDisplayOrderAndIsActiveOnly(int ProfileID, int DisplayOrder, bool IsActive, DateTime UpdatedOn, int PortalID, string Username)
        {
            try
            {
                string sp = "[dbo].[sp_ProfileUpdateDisplayOrderAndIsActiveOnly]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ProfileID", ProfileID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayOrder", DisplayOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedOn", UpdatedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", Username));

                SQLH.ExecuteNonQuery(sp, ParamCollInput);
       
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        
        /// <summary>
        /// Connects to database and returns active profile list by portal ID.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of active profile.</returns>
        public List<ProfileManagementInfo> GetActiveProfileList(int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsList<ProfileManagementInfo>("[dbo].[sp_ProfileListActive]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns list of profile details.
        /// </summary>
        /// <param name="ListName">List name.</param>
        /// <param name="EntryID">Entry ID.</param>
        /// <param name="Culture">Culture name.</param>
        /// <returns>List of profile.</returns>
        public List<ProfileManagementInfo> GetListEntrybyNameAndID(string ListName,int EntryID, string Culture )
        {
            try
            {

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ListName", ListName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@EntryID", EntryID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));
                return SQLH.ExecuteAsList<ProfileManagementInfo>("[dbo].[sp_GetListEntrybyNameAndID]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and returns active profile users by username.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of profile details.</returns>
        public List<ProfileManagementInfo> GetUserProfileActiveListByUsername(string Username, int PortalID)
        {
            try
            {

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                return SQLH.ExecuteAsList<ProfileManagementInfo>("[dbo].[sp_UserProfileActiveListByUsername]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and returns list entries by name and parerent key.
        /// </summary>
        /// <param name="ListName">Name list.</param>
        /// <param name="ParentKey">Parent key.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="Culture">Culture name.</param>
        /// <returns>List of profile</returns>
        public List<ProfileManagementInfo> GetListEntriesByNameParentKeyAndPortalID(string ListName, string ParentKey, int PortalID, string Culture)
        {
            try
            {

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ListName", ListName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentKey", ParentKey));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));

                return SQLH.ExecuteAsList<ProfileManagementInfo>("[dbo].[sp_GetListEntriesByNameParentKeyAndPortalID]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns list of profile image folder.
        /// </summary>
        /// <param name="EditUserName">User's name.</param>
        /// <param name="ProfileID">Profile ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Profile folder name.</returns>
        public List<ProfileManagementInfo> GetProfileImageFolders(string EditUserName, int ProfileID, int PortalID)
        {
            try
            {

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@EditUserName", EditUserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ProfileID", ProfileID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsList<ProfileManagementInfo>("[dbo].[sp_ProfileImageFoldersGet]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and adds user profile detail.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="ProfileID">Profile ID.</param>
        /// <param name="Value">Value.</param>
        /// <param name="IsActive">Set true if the profile is active.</param>
        /// <param name="AddedOn">Profile added user's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="AddedBy">Added user's name.</param>
        /// <returns>Returns UserProfileID</returns>
        public int AddUserProfile(string Username, int ProfileID, string Value, bool IsActive, DateTime AddedOn, int PortalID, string AddedBy)
        {
            try
            {
                string sp = "[dbo].[sp_UserProfileAdd]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ProfileID", ProfileID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Value", Value));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));

                int PVID = SQLH.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@UserProfileID");
                return PVID;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
