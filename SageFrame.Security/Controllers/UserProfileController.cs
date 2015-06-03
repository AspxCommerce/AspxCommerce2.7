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
using SageFrame.ExportUser;
#endregion

namespace SageFrame.UserProfile
{
    /// <summary>
    /// Business logic for UserProfileController.
    /// </summary>
    public class UserProfileController
    {
        /// <summary>
        /// Add update user profile.
        /// </summary>
        /// <param name="objProfile">Object of UserProfileInfo class.</param>
        public static void AddUpdateProfile(UserProfileInfo objProfile)
        {
            try
            {
                UserProfileDataProvider.AddUpdateProfile(objProfile);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtain user profile.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Object of UserProfileInfo class.</returns>
        public static UserProfileInfo GetProfile(string UserName, int PortalID)
        {
            try
            {
                return UserProfileDataProvider.GetProfile(UserName, PortalID);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Delete user profile picture.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        public static void DeleteProfilePic(string UserName, int PortalID)
        {
            try
            {
                UserProfileDataProvider.DeleteProfilePic(UserName, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update cart from anonymous user to registered user.
        /// </summary>
        /// <param name="storeID">StoreID</param>
        /// <param name="portalID">PortalID</param>
        /// <param name="customerID">CustomerID</param>
        /// <param name="sessionCode">Session Code</param>
        /// <returns>True for successfully update.</returns>
        public static bool UpdateCartAnonymoususertoRegistered(int storeID, int portalID, int customerID, string sessionCode)
        {
            try
            {
                return UserProfileDataProvider.UpdateCartAnonymoususertoRegistered(storeID, portalID, customerID, sessionCode);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtain user Export List.
        /// </summary>
        /// <returns>List of Export User List</returns>
        public List<ExportUserInfo> GetUserExportList()
        {
            try
            {
                UserProfileDataProvider objProvider = new UserProfileDataProvider();
                return objProvider.GetUserExportList();
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Obtain SageFrame User List.
        /// </summary>
        /// <returns>List of SageFrame User List</returns>
        public List<ExportUserInfo> GetSageFrameUserList()
        {
            try
            {
                UserProfileDataProvider objProvider = new UserProfileDataProvider();
                return objProvider.GetSageFrameUserList();
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
