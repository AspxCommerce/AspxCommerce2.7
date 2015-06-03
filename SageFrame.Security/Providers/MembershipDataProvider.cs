
#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "references"
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Security.Helpers;
using SageFrame.Web.Utilities;
using SageFrame.Security.Enums;
#endregion

namespace SageFrame.Security.Providers
{
    /// <summary>
    ///  Manupulates data for MembershipDataProvider.
    /// </summary>
    public class MembershipDataProvider
    {
        #region User
        /// <summary>
        /// Connect to the database and create user.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <param name="status">User creation status.<see cref="T:SageFrame.Security.Helpers.UserCreationStatus"/></param>
        /// <param name="mode">User creation mode.</param>
        /// <returns>True for create successfully.</returns>
        public static bool CreatePortalUser(UserInfo obj, out UserCreationStatus status, UserCreationMode mode)
        {
            string sp = "[dbo].[usp_sf_CreateUser]";
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationName", obj.ApplicationName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", obj.UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FirstName", obj.FirstName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LastName", obj.LastName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Password", obj.Password));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordSalt", obj.PasswordSalt));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Email", obj.Email));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordQuestion", obj.SecurityQuestion));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordAnswer", obj.SecurityAnswer));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsApproved", obj.IsApproved));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CurrentTimeUtc", obj.CurrentTimeUtc));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CreateDate", obj.CreatedDate));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UniqueEmail", obj.UniqueEmail));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordFormat", obj.PasswordFormat));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", obj.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", obj.AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", obj.AddedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@RoleNames", obj.RoleNames));
                ParamCollInput.Add(new KeyValuePair<string, object>("@StoreID", obj.StoreID));

                List<KeyValuePair<string, object>> ParamCollOutput = new List<KeyValuePair<string, object>>();
                ParamCollOutput.Add(new KeyValuePair<string, object>("@UserId", obj.UserID));
                ParamCollOutput.Add(new KeyValuePair<string, object>("@ErrorCode", 0));
                ParamCollOutput.Add(new KeyValuePair<string, object>("@CustomerID", obj.CustomerID));

                SageFrameSQLHelper sagesql = new SageFrameSQLHelper();

                List<KeyValuePair<int, string>> OutputValColl = new List<KeyValuePair<int, string>>();
                OutputValColl = sagesql.ExecuteNonQueryWithMultipleOutput(sp, ParamCollInput, ParamCollOutput);
                int CustomerID = int.Parse(OutputValColl[2].Value);
                int ErrorCode = int.Parse(OutputValColl[1].Value);
                Guid UserID = new Guid(OutputValColl[0].Value.ToString());

                switch (ErrorCode)
                {
                    case 3:
                        status = UserCreationStatus.DUPLICATE_EMAIL;
                        break;
                    case 6:
                        status = UserCreationStatus.DUPLICATE_USER;
                        break;
                    default:
                        status = UserCreationStatus.SUCCESS;
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }


        }
        /// <summary>
        /// Connect to database and register portal user.
        /// </summary>
        /// <param name="info">Object of UserInfo class.</param>
        /// <param name="status">User creation status.<see cref="T:SageFrame.Security.Helpers.UserCreationStatus"/></param>
        /// <param name="customerId">customerId</param>
        /// <param name="register">User creation mode.<see cref="T:SageFrame.Security.Helpers.UserCreationMode"/></param>
        /// <returns>True for register successfully.</returns>
        public static bool RegisterPortalUser(UserInfo info, out UserCreationStatus status, out int customerId, UserCreationMode register)
        {
            string sp = "[dbo].[usp_sf_CreateUser]";
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationName", info.ApplicationName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", info.UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FirstName", info.FirstName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LastName", info.LastName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Password", info.Password));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordSalt", info.PasswordSalt));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Email", info.Email));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordQuestion", info.SecurityQuestion));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordAnswer", info.SecurityAnswer));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsApproved", info.IsApproved));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CurrentTimeUtc", info.CurrentTimeUtc));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CreateDate", info.CreatedDate));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UniqueEmail", info.UniqueEmail));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordFormat", info.PasswordFormat));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", info.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", info.AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", info.AddedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@RoleNames", info.RoleNames));
                ParamCollInput.Add(new KeyValuePair<string, object>("@StoreID", info.StoreID));

                List<KeyValuePair<string, object>> ParamCollOutput = new List<KeyValuePair<string, object>>();
                ParamCollOutput.Add(new KeyValuePair<string, object>("@UserId", info.UserID));
                ParamCollOutput.Add(new KeyValuePair<string, object>("@ErrorCode", 0));
                ParamCollOutput.Add(new KeyValuePair<string, object>("@CustomerID", info.CustomerID));

                SageFrameSQLHelper sagesql = new SageFrameSQLHelper();

                List<KeyValuePair<int, string>> OutputValColl = new List<KeyValuePair<int, string>>();
                OutputValColl = sagesql.ExecuteNonQueryWithMultipleOutput(sp, ParamCollInput, ParamCollOutput);
                customerId = int.Parse(OutputValColl[2].Value);
                int ErrorCode = int.Parse(OutputValColl[1].Value);
                Guid UserID = new Guid(OutputValColl[0].Value.ToString());

                switch (ErrorCode)
                {
                    case 3:
                        status = UserCreationStatus.DUPLICATE_EMAIL;
                        break;
                    case 6:
                        status = UserCreationStatus.DUPLICATE_USER;
                        break;
                    default:
                        status = UserCreationStatus.SUCCESS;
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Connect to database and update user.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <param name="status">User creation status. <see cref="T:SageFrame.Security.Helpers.UserCreationStatus"/></param>
        /// <returns>True for update sucessfully. </returns>
        public static bool UpdateUser(UserInfo obj, out UserUpdateStatus status)
        {
            string sp = "[dbo].[usp_sf_UsersUpdate]";
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationName", obj.ApplicationName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", obj.UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserID", obj.UserID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FirstName", obj.FirstName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LastName", obj.LastName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Email", obj.Email));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", obj.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsApproved", obj.IsApproved));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", obj.UpdatedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@StoreID", obj.StoreID));

                List<KeyValuePair<string, object>> ParamCollOutput = new List<KeyValuePair<string, object>>();
                ParamCollOutput.Add(new KeyValuePair<string, object>("@ErrorCode", 0));

                SageFrameSQLHelper sagesql = new SageFrameSQLHelper();

                List<KeyValuePair<int, string>> OutputValColl = new List<KeyValuePair<int, string>>();
                OutputValColl = sagesql.ExecuteNonQueryWithMultipleOutput(sp, ParamCollInput, ParamCollOutput);

                int ErrorCode = int.Parse(OutputValColl[0].Value);

                switch (ErrorCode)
                {
                    case 1:
                        status = UserUpdateStatus.DUPLICATE_EMAIL_NOT_ALLOWED;
                        break;
                    default:
                        status = UserUpdateStatus.USER_UPDATE_SUCCESSFUL;
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Connect to database and obtain users based on PortalID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of UserInfo class.</returns>
        public static List<UserInfo> GetPortalUsers(int PortalID)
        {
            string sp = "[dbo].[usp_PortalUserListGet]";
            SQLHandler sagesql = new SQLHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

            List<UserInfo> lstUsers = new List<UserInfo>();
            SqlDataReader reader = null;
            try
            {

                reader = sagesql.ExecuteAsDataReader(sp, ParamCollInput);
                while (reader.Read())
                {
                    UserInfo obj = new UserInfo();
                    obj.UserID = new Guid(reader["UserID"].ToString());
                    obj.UserName = reader["UserName"].ToString();
                    obj.IsApproved = bool.Parse(reader["IsActive"].ToString());
                    lstUsers.Add(obj);
                }
                reader.Close();
                return lstUsers;

            }
            catch (Exception ex)
            {

                throw (ex);
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
        /// Connect to database and obtain all application users.
        /// </summary>
        /// <returns>Object of SageFrameUserCollection class.</returns>
        public static SageFrameUserCollection GetAllUsers()
        {
            string sp = "[dbo].[usp_UsersGetAll]";
            SQLHandler sagesql = new SQLHandler();

            SageFrameUserCollection userColl = new SageFrameUserCollection();
            List<UserInfo> lstUsers = new List<UserInfo>();
            SqlDataReader reader = null;
            try
            {

                reader = sagesql.ExecuteAsDataReader(sp);
                while (reader.Read())
                {
                    UserInfo obj = new UserInfo();
                    obj.UserID = new Guid(reader["userid"].ToString());
                    obj.UserName = reader["username"].ToString();
                    obj.FirstName = reader["firstname"].ToString();
                    obj.LastName = reader["lastname"].ToString();
                    obj.Email = reader["email"].ToString();
                    lstUsers.Add(obj);
                }
                reader.Close();
                userColl.UserList = lstUsers;
                return userColl;

            }
            catch (Exception ex)
            {

                throw (ex);
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
        /// Connect to database and search application user.
        /// </summary>
        /// <param name="RoleID">RoleID</param>
        /// <param name="SearchText">Search text.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <returns>Object of SageFrameUserCollection class.</returns>
        public static SageFrameUserCollection SearchUsers(string RoleID, string SearchText, int PortalID, string UserName)
        {
            string sp = "[dbo].[usp_SageFrameUserListSearch]";
            SageFrameSQLHelper sagesql = new SageFrameSQLHelper();

            SageFrameUserCollection userColl = new SageFrameUserCollection();
            List<UserInfo> lstUsers = new List<UserInfo>();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@RoleID", RoleID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@SearchText", SearchText));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
            SqlDataReader reader = null;
            try
            {

                reader = sagesql.ExecuteAsDataReader(sp, ParamCollInput);
                while (reader.Read())
                {
                    UserInfo obj = new UserInfo();
                    obj.UserID = new Guid(reader["userid"].ToString());
                    obj.UserName = reader["username"].ToString();
                    obj.FirstName = reader["firstname"].ToString();
                    obj.LastName = reader["lastname"].ToString();
                    obj.Email = reader["email"].ToString();
                    obj.IsActive = bool.Parse(reader["IsActive"].ToString());
                    obj.AddedOn = DateTime.Parse(reader["AddedOn"].ToString());
                    lstUsers.Add(obj);
                }
                reader.Close();
                userColl.UserList = lstUsers;
                return userColl;

            }
            catch (Exception ex)
            {

                throw (ex);
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
        /// Connect to database and delete user.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <returns>True for deleted successfully.</returns>
        public static bool DeleteUser(UserInfo user)
        {
            string sp = "[dbo].[usp_UsersDelete]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationName", user.ApplicationName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", user.UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", user.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@StoreID", user.StoreID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DeletedBy", user.AddedBy));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        ///  Connect to database and obtain user details based on PortalID and user name.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Object of UserInfo class.</returns>
        public static UserInfo GetUserDetails(string UserName, int PortalID)
        {
            string sp = "[dbo].[usp_GetUserDetails]";
            SQLHandler sagesql = new SQLHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

            List<UserInfo> lstUser = new List<UserInfo>();
            SqlDataReader reader = null;
            try
            {

                reader = sagesql.ExecuteAsDataReader(sp, ParamCollInput);
                while (reader.Read())
                {
                    UserInfo obj = new UserInfo();
                    obj.UserID = new Guid(reader["userid"].ToString());
                    obj.UserName = reader["Username"].ToString();
                    obj.Password = reader["Password"].ToString();
                    obj.PasswordSalt = reader["PasswordSalt"].ToString();
                    obj.PasswordFormat = int.Parse(reader["PasswordFormat"].ToString());
                    obj.FirstName = reader["FirstName"].ToString();
                    obj.LastName = reader["LastName"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.CreatedDate = DateTime.Parse(reader["CreateDate"].ToString());
                    obj.LastPasswordChangeDate = DateTime.Parse(reader["LastPasswordChangedDate"].ToString());
                    obj.LastActivityDate = DateTime.Parse(reader["LastActivityDate"].ToString());
                    obj.LastLoginDate = DateTime.Parse(reader["LastLoginDate"].ToString());
                    obj.IsApproved = bool.Parse(reader["IsApproved"].ToString());
                    obj.UserExists = true;
                    lstUser.Add(obj);
                }
                reader.Close();
                UserInfo userObj = lstUser.Count > 0 ? lstUser[0] : new UserInfo(false);
                return userObj;

            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public static bool CreatePortalUserImport(UserInfo obj, out UserCreationStatus status, UserCreationMode mode)
        {
            string sp = "[dbo].[usp_sf_CreateUser]";
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationName", obj.ApplicationName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", obj.UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FirstName", obj.FirstName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LastName", obj.LastName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Password", obj.Password));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordSalt", obj.PasswordSalt));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Email", obj.Email));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordQuestion", obj.SecurityQuestion));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordAnswer", obj.SecurityAnswer));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsApproved", obj.IsApproved));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CurrentTimeUtc", obj.CurrentTimeUtc));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CreateDate", obj.CreatedDate));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UniqueEmail", obj.UniqueEmail));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordFormat", obj.PasswordFormat));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", obj.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", obj.AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", obj.AddedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@RoleNames", obj.RoleNames));
                ParamCollInput.Add(new KeyValuePair<string, object>("@StoreID", obj.StoreID));

                List<KeyValuePair<string, object>> ParamCollOutput = new List<KeyValuePair<string, object>>();
                ParamCollOutput.Add(new KeyValuePair<string, object>("@UserId", obj.UserID));
                ParamCollOutput.Add(new KeyValuePair<string, object>("@ErrorCode", 0));
                ParamCollOutput.Add(new KeyValuePair<string, object>("@CustomerID", obj.CustomerID));

                SageFrameSQLHelper sagesql = new SageFrameSQLHelper();

                List<KeyValuePair<int, string>> OutputValColl = new List<KeyValuePair<int, string>>();
                OutputValColl = sagesql.ExecuteNonQueryWithMultipleOutput(sp, ParamCollInput, ParamCollOutput);
                int CustomerID = int.Parse(OutputValColl[2].Value);
                int ErrorCode = int.Parse(OutputValColl[1].Value);
                Guid UserID = new Guid(OutputValColl[0].Value.ToString());

                switch (ErrorCode)
                {
                    case 3:
                        status = UserCreationStatus.DUPLICATE_EMAIL;
                        break;
                    case 6:
                        status = UserCreationStatus.DUPLICATE_USER;
                        break;
                    default:
                        status = UserCreationStatus.SUCCESS;
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region "For Mobile"
        /// <summary>
        /// Connect to database and obtain user details for mobile based on PortalID and user name.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">user name.</param>
        /// <returns>Object of UserInfoMob class.</returns>
        public static UserInfoMob GetUserDetailsMob(string UserName, int PortalID)
        {
            string sp = "[dbo].[usp_GetUserDetails]";
            SQLHandler sagesql = new SQLHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

            List<UserInfoMob> lstUser = new List<UserInfoMob>();
            SqlDataReader reader = null;
            try
            {

                reader = sagesql.ExecuteAsDataReader(sp, ParamCollInput);
                while (reader.Read())
                {
                    UserInfoMob obj = new UserInfoMob();
                    // obj.UserID = new Guid(reader["userid"].ToString());
                    obj.UserName = reader["Username"].ToString();
                    //obj.Password = reader["Password"].ToString();
                    //obj.PasswordSalt = reader["PasswordSalt"].ToString();
                    //obj.PasswordFormat = int.Parse(reader["PasswordFormat"].ToString());
                    obj.FirstName = reader["FirstName"].ToString();
                    obj.LastName = reader["LastName"].ToString();
                    obj.Email = reader["Email"].ToString();
                    // obj.LastPasswordChangeDate = DateTime.Parse(reader["LastPasswordChangedDate"].ToString());
                    //obj.LastActivityDate = DateTime.Parse(reader["LastActivityDate"].ToString());
                    //obj.LastLoginDate = DateTime.Parse(reader["LastLoginDate"].ToString());
                    //obj.IsApproved = bool.Parse(reader["IsApproved"].ToString());
                    obj.UserExists = true;
                    lstUser.Add(obj);
                }
                reader.Close();
                UserInfoMob userObj = lstUser.Count > 0 ? lstUser[0] : new UserInfoMob(false);
                return userObj;

            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        #endregion
        /// <summary>
        /// Connect to database and activate registered user.
        /// </summary>
        /// <param name="ActivationCode">Activation code.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>User name.</returns>
        public static string ActivateUser(string ActivationCode, int PortalID)
        {
            string sp = "[usp_ActivateUserByUserId]";
            SageFrameSQLHelper sagesql = new SageFrameSQLHelper();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@ActivationCode", ActivationCode));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

            List<KeyValuePair<string, object>> ParamCollOutput = new List<KeyValuePair<string, object>>();
            ParamCollOutput.Add(new KeyValuePair<string, object>("@UserName", "nouser"));

            try
            {
                List<KeyValuePair<int, string>> Output = new List<KeyValuePair<int, string>>();
                Output = sagesql.ExecuteNonQueryWithMultipleOutput(sp, ParamCollInput, ParamCollOutput);
                return Output[0].Value.ToString();

            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
        /// <summary>
        /// Connect tod database and activate registered user. 
        /// </summary>
        /// <param name="ActivationCode">Activation code.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="StoreID">StoreID</param>
        /// <returns>User name.</returns>
        public static string ActivateUser(string ActivationCode, int PortalID, int StoreID)
        {
            string sp = "[usp_ActivateUserByUserId]";
            SageFrameSQLHelper sagesql = new SageFrameSQLHelper();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@ActivationCode", ActivationCode));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@StoreID", StoreID));

            List<KeyValuePair<string, object>> ParamCollOutput = new List<KeyValuePair<string, object>>();
            ParamCollOutput.Add(new KeyValuePair<string, object>("@UserName", "nouser"));

            try
            {
                List<KeyValuePair<int, string>> Output = new List<KeyValuePair<int, string>>();
                Output = sagesql.ExecuteNonQueryWithMultipleOutput(sp, ParamCollInput, ParamCollOutput);
                return Output[0].Value.ToString();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
        /// <summary>
        /// Connect to databse and edit user permission.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <returns>True for update user information sucessfully.</returns>
        public static bool EditPermissionExists(int UserModuleID, int PortalID, string UserName)
        {
            string sp = "[dbo].[usp_UserModuleIsEditAllowed]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
                int count = sagesql.ExecuteAsScalar<int>(sp, ParamCollInput);
                bool status = count > 0 ? true : false;
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Roles
        /// <summary>
        /// Connect to database and create role.
        /// </summary>
        /// <param name="obj">Object of RoleInfo class.</param>
        /// <param name="status">Role creation status.<see cref="T: SageFrame.Security.Helpers.RoleCreationStatus"/></param>

        public static void CreateRole(RoleInfo obj, out RoleCreationStatus status)
        {
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationName", obj.ApplicationName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@RoleName", obj.RoleName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", obj.PortalID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", obj.IsActive));
            ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", obj.AddedOn));
            ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", obj.AddedBy));

            List<KeyValuePair<string, object>> ParamCollOutput = new List<KeyValuePair<string, object>>();
            ParamCollOutput.Add(new KeyValuePair<string, object>("@ErrorCode", 0));

            try
            {
                SageFrameSQLHelper sagesql = new SageFrameSQLHelper();
                List<KeyValuePair<int, string>> OutputValColl = new List<KeyValuePair<int, string>>();
                OutputValColl = sagesql.ExecuteNonQueryWithMultipleOutput("[dbo].[usp_sf_CreateRole]", ParamCollInput, ParamCollOutput);
                int ErrorCode = int.Parse(OutputValColl[0].Value);

                switch (ErrorCode)
                {
                    case 1:
                        status = RoleCreationStatus.DUPLICATE_ROLE;
                        break;
                    default:
                        status = RoleCreationStatus.SUCCESS;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connect to database and obtain application portal roles.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="IsAll">1 for all roles.</param>
        /// <param name="UserName">User name.</param>
        /// <returns>List of RoleInfo class.</returns>
        public static List<RoleInfo> GetPortalRoles(int PortalID, int IsAll, string UserName)
        {
            string sp = "[dbo].[sp_PortalRoleList]";
            SQLHandler sagesql = new SQLHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@IsAll", IsAll));
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));

            List<RoleInfo> lstPortalRoles = new List<RoleInfo>();
            SqlDataReader reader = null;
            try
            {

                reader = sagesql.ExecuteAsDataReader(sp, ParamCollInput);
                while (reader.Read())
                {
                    RoleInfo obj = new RoleInfo();
                    obj.RoleName = reader["RoleName"].ToString();
                    obj.RoleID = new Guid(reader["RoleID"].ToString());
                    lstPortalRoles.Add(obj);
                }
                reader.Close();
                return lstPortalRoles;

            }
            catch (Exception ex)
            {

                throw (ex);
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
        /// Connect to database and delete roles.
        /// </summary>
        /// <param name="RoleID">RoleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>True for deleted successfully.</returns>
        public static bool DeletePortalRole(Guid RoleID, int PortalID)
        {
            string sp = "[dbo].[usp_PortalRoleDelete]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@RoleID", RoleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to the database and obtain roles name.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Role names with comma separator.</returns>
        public static string GetRoleNames(string UserName, int PortalID)
        {
            string sp = "[dbo].[usp_RoleNamesGetByUserName]";
            SQLHandler sagesql = new SQLHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            SqlDataReader reader = null;
            try
            {

                reader = sagesql.ExecuteAsDataReader(sp, ParamCollInput); ;
                List<string> lstRoles = new List<string>();
                while (reader.Read())
                {
                    lstRoles.Add(reader["RoleName"].ToString());

                }
                reader.Close();
                return (String.Join(",", lstRoles.ToArray()));
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        #endregion

        #region Settings
        /// <summary>
        /// Connect to the database and check condition for dashboard access.
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// /// <param name="PortalID">PortalID</param>
        /// <returns></returns>
        public bool IsDashboardAccesible(string UserName, int PortalID)
        {
            string sp = "[dbo].[usp_IsDashboardAccesible]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            SqlDataReader reader = null;
            bool isAccessible = false;
            try
            {
                reader = sagesql.ExecuteAsDataReader(sp, ParamCollInput);
                while (reader.Read())
                {
                    isAccessible = bool.Parse(reader["Accessible"].ToString());
                }
                reader.Close();
                return isAccessible;
            }
            catch (Exception ex)
            {
                throw (ex);
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
        /// Connect to the database and obtain application settings.
        /// </summary>
        /// <returns>List of SettingInfo class.</returns>
        public static List<SettingInfo> GetSettings()
        {
            string sp = "usp_GetMembershipSettings";
            SQLHandler sagesql = new SQLHandler();
            List<SettingInfo> lstSetting = new List<SettingInfo>();
            SqlDataReader reader = null;
            try
            {
                reader = sagesql.ExecuteAsDataReader(sp);
                while (reader.Read())
                {
                    SettingInfo obj = new SettingInfo();
                    obj.SettingKey = reader["SettingKey"].ToString();
                    obj.SettingValue = reader["SettingValue"].ToString();
                    lstSetting.Add(obj);
                }
                reader.Close();
                return lstSetting;

            }
            catch (Exception ex)
            {

                throw (ex);
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
        /// Connect to database and save settings.
        /// </summary>
        /// <param name="lstSetting">List of SettingInfo class.</param>
        /// <returns></returns>
        public static bool SaveSettings(List<SettingInfo> lstSetting)
        {
            string sp = "usp_SaveSettings";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                foreach (SettingInfo obj in lstSetting)
                {
                    List<KeyValuePair<string, object>> ParamCollection = new List<KeyValuePair<string, object>>();
                    ParamCollection.Add(new KeyValuePair<string, object>("@SettingKey", obj.SettingKey));
                    ParamCollection.Add(new KeyValuePair<string, object>("@SettingValue", obj.SettingValue));
                    sagesql.ExecuteNonQuery(sp, ParamCollection);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region UserInRoles
        /// <summary>
        /// Connect to database and add user in role.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <returns>True for add user in role successfully.</returns>
        public static bool AddUserInRoles(UserInfo obj)
        {
            string sp = "[dbo].[usp_sf_UserInRolesAdd]";
            SageFrameSQLHelper sagesql = new SageFrameSQLHelper();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationName", obj.ApplicationName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserID", obj.UserID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@RoleNames", obj.RoleNames));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", obj.PortalID));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);


                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to database and delete user from roles.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <returns>True for deleted successfully.</returns>
        public static bool DeleteUserInRoles(UserInfo obj)
        {
            string sp = "[dbo].[usp_sf_UserInRolesDelete]";
            SageFrameSQLHelper sagesql = new SageFrameSQLHelper();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationName", obj.ApplicationName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserID", obj.UserID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@RoleNames", obj.RoleNames));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", obj.PortalID));

                List<KeyValuePair<string, object>> ParamCollOutput = new List<KeyValuePair<string, object>>();
                ParamCollOutput.Add(new KeyValuePair<string, object>("@ErrorCode", 0));

                List<KeyValuePair<int, string>> OutPutColl = new List<KeyValuePair<int, string>>();
                OutPutColl = sagesql.ExecuteNonQueryWithMultipleOutput(sp, ParamCollInput, ParamCollOutput);

                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and change user in role.
        /// </summary>
        /// <param name="ApplicationName">Application name.</param>
        /// <param name="UserID">UserID</param>
        /// <param name="RoleNamesUnselected">Unselected role name.</param>
        /// <param name="RoleNamesSelected">Selected role name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>True for change user role successfully.</returns>
        public static bool ChangeUserInRoles(string ApplicationName, Guid UserID, string RoleNamesUnselected, string RoleNamesSelected, int PortalID)
        {
            string sp = "[dbo].[usp_ChangeUserInRoles]";
            SageFrameSQLHelper sagesql = new SageFrameSQLHelper();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ApplicationName", ApplicationName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserID", UserID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@RoleNamesUnselected", RoleNamesUnselected));
                ParamCollInput.Add(new KeyValuePair<string, object>("@RoleNamesSelected", RoleNamesSelected));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);

                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region Password
        /// <summary>
        /// Connect to database and change user password.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <returns>True for change password sucessfully.</returns>
        public static bool ChangePassword(UserInfo obj)
        {
            string sp = "[dbo].[usp_sf_ResetPassword]";
            SageFrameSQLHelper sagesql = new SageFrameSQLHelper();

            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserID", obj.UserID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@NewPassword", obj.Password));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordSalt", obj.PasswordSalt));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PasswordFormat", obj.PasswordFormat));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region OpenID
        /// <summary>
        /// Connect to database and obtain user information based on email and PortalID.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="portalID">PortalID</param>
        /// <returns>Object of UserInfo class.</returns>
        public static UserInfo GerUserByEmail(string email, int portalID)
        {
            string sp = "[dbo].[usp_GetUserByEmail]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@email", email));
                ParamCollInput.Add(new KeyValuePair<string, object>("@portalID", portalID));

                UserInfo objUser = sagesql.ExecuteAsObject<UserInfo>(sp, ParamCollInput);
                return objUser;
            }
            catch (Exception)
            {

                throw;
            }

        }


        #endregion


    }
}

