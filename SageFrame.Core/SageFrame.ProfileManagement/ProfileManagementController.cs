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

#endregion


namespace SageFrame.ProfileManagement
{
    /// <summary>
    /// Business logic for profile management
    /// </summary>
    public class ProfileManagementController
    {
        /// <summary>
        /// Returns list of profile.
        /// </summary>
        /// <returns>List of profile details</returns>
        public List<ProfileManagementInfo> GetPropertyTypeList()
        {
            try
            {
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.GetPropertyTypeList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns list of profile details.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of profile details.</returns>
        public List<ProfileManagementInfo> GetProfileList(int PortalID)
        {
            try
            {
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.GetProfileList(PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds user's profile.
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
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.AddProfile(Name, PropertyTypeID, DataType, IsRequired, IsActive, AddedOn, PortalID, AddedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds profile details.
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
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.AddProfileValue(ProfileID, Name, IsActive, AddedOn, PortalID, AddedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates profile.
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
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                objProvider.UpdateProfile(ProfileID, Name, PropertyTypeID, DataType, IsRequired, IsActive, IsModified, UpdatedOn, PortalID, UpdatedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes profile by profile ID.
        /// </summary>
        /// <param name="ProfileID">Profile ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UserName">User's name.</param>
        public void DeleteProfileValueByProfileID(int ProfileID, int PortalID, string UserName)
        {
            try
            {
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                objProvider.DeleteProfileValueByProfileID(ProfileID, PortalID, UserName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes profile
        /// </summary>
        /// <param name="DeleteID">Profile ID</param>
        /// <param name="UserName">User's name.</param>
        public void DeleteProfileByProfileID(int DeleteID, string UserName)
        {
            try
            {
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                objProvider.DeleteProfileByProfileID(DeleteID, UserName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns profile by profile ID.
        /// </summary>
        /// <param name="EditID">Profile ID.</param>
        /// <returns>Profile detail</returns>
        public ProfileManagementInfo GetProfileByProfileID(int EditID)
        {
            try
            {
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.GetProfileByProfileID(EditID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns active profile value by profile ID.
        /// </summary>
        /// <param name="ProfileID">Profile ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of profile management.</returns>
        public List<ProfileManagementInfo> GetActiveProfileValueByProfileID(int ProfileID, int PortalID)
        {
            try
            {
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.GetActiveProfileValueByProfileID(ProfileID, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates profile display order only if it is active .
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
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                objProvider.UpdateProfileDisplayOrderAndIsActiveOnly(ProfileID, DisplayOrder, IsActive, UpdatedOn, PortalID, Username);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns active profile list by portal ID.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of active profile.</returns>
        public List<ProfileManagementInfo> GetActiveProfileList(int PortalID)
        {
            try
            {
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.GetActiveProfileList(PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns list of profile details.
        /// </summary>
        /// <param name="ListName">List name.</param>
        /// <param name="EntryID">Entry ID.</param>
        /// <param name="Culture">Culture name.</param>
        /// <returns>List of profile.</returns>
        public List<ProfileManagementInfo> GetListEntrybyNameAndID(string ListName, int EntryID, string Culture)
        {
            try
            {
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.GetListEntrybyNameAndID(ListName, EntryID, Culture);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns active profile users by username.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of profile details.</returns>
        public static List<ProfileManagementInfo> GetUserProfileActiveListByUsername(string Username, int PortalID)
        {
            try
            {
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.GetUserProfileActiveListByUsername(Username, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns list entries by name and parerent key.
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
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.GetListEntriesByNameParentKeyAndPortalID(ListName, ParentKey, PortalID, Culture);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns list of profile image folder.
        /// </summary>
        /// <param name="EditUserName">User's name.</param>
        /// <param name="ProfileID">Profile ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Profile folder name.</returns>
        public List<ProfileManagementInfo> GetProfileImageFolders(string EditUserName, int ProfileID, int PortalID)
        {
            try
            {
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.GetProfileImageFolders(EditUserName, ProfileID, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds user profile detail.
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
                ProfileManagementProvider objProvider = new ProfileManagementProvider();
                return objProvider.AddUserProfile(Username, ProfileID, Value, IsActive, AddedOn, PortalID, AddedBy);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
