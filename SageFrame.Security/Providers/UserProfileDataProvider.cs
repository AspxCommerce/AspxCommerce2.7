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
using SageFrame.ExportUser;
#endregion

namespace SageFrame.UserProfile
{
    /// <summary>
    ///  Manupulates data for UserProfileDataProvider.
    /// </summary>
    public class UserProfileDataProvider
    {
        /// <summary>
        /// Connect to database and add update user profile.
        /// </summary>
        /// <param name="objProfile">Object of UserProfileInfo class.</param>
        public static void AddUpdateProfile(UserProfileInfo objProfile)
        {
            try
            {
                string sp = "[dbo].[usp_AddUpdateUserProfile]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();


                ParamCollInput.Add(new KeyValuePair<string, object>("@image", objProfile.Image));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", objProfile.UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FirstName", objProfile.FirstName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LastName", objProfile.LastName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FullName", objProfile.FullName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@BirthDate", objProfile.BirthDate));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Gender", objProfile.Gender));

                ParamCollInput.Add(new KeyValuePair<string, object>("@Location", objProfile.Location));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AboutYou", objProfile.AboutYou));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Email", objProfile.Email));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ResPhone", objProfile.ResPhone));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Mobile", objProfile.MobilePhone));

                ParamCollInput.Add(new KeyValuePair<string, object>("@Others", objProfile.Others));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", objProfile.AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", objProfile.AddedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedOn", objProfile.UpdatedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", objProfile.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", objProfile.UpdatedBy));


                SQLH.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to database and obtain user profile.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Object of UserProfileInfo class.</returns>
        public static UserProfileInfo GetProfile(string UserName, int PortalID)
        {
            string sp = "[dbo].[usp_GetUserProfile]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            try
            {
                return (sagesql.ExecuteAsObject<UserProfileInfo>(sp, ParamCollInput));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to database and delete user profile picture.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        public static void DeleteProfilePic(string UserName, int PortalID)
        {
            try
            {
                string sp = "[dbo].[usp_UserProfilePicDelete]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLH.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to database and update cart from anonymous user to registered user.
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
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsBool("usp_Aspx_UpdateCartAnonymoususertoRegistered", ParaMeter, "@IsUpdate");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Connect to database and obtain user Export List.
        /// </summary>
        /// <returns>List of Export User List</returns>
        public List<ExportUserInfo> GetUserExportList()
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                return SQLH.ExecuteAsList<ExportUserInfo>("[dbo].[sp_UserExportList]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Connect to database and obtain SageFrame User List.
        /// </summary>
        /// <returns>List of SageFrame User List</returns>
        public List<ExportUserInfo> GetSageFrameUserList()
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                return SQLH.ExecuteAsList<ExportUserInfo>("[dbo].[usp_GetSageFrameUserList]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
