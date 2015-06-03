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
using SageFrame.Security.Entities;
using SageFrame.Security.Providers;
using System.Web.Security;
using SageFrame.Security.Helpers;
using SageFrame.Security.Enums;
#endregion

namespace SageFrame.Security
{
    /// <summary>
    /// Business logic for MembershipController inherited from SageFrameMembershipProvider.
    /// </summary>
    public class MembershipController : SageFrameMembershipProvider
    {
        # region Public Properties
        /// <summary>
        /// Get true if required unique email for registration.
        /// </summary>
        public override bool RequireUniqueEmail
        {
            get
            {
                bool _RequireUniqueEmail = ((int)EmailConfig.UNIQUE_EMAIL) == 1 ? true : false;
                foreach (SettingInfo obj in MembershipDataProvider.GetSettings())
                {
                    switch (obj.SettingKey)
                    {
                        case "DUPLICATE_EMAIL_ALLOWED":
                            _RequireUniqueEmail = int.Parse(obj.SettingValue) == 1 ? true : false;
                            break;
                    }
                }
                return _RequireUniqueEmail;
            }
        }
        /// <summary>
        /// Get password format from database else return the default format.
        /// </summary>
        public override int PasswordFormat
        {
            get
            {

                int _PasswordFormat = (int)SettingsEnum.DEFAULT_PASSWORD_FORMAT;
                foreach (SettingInfo obj in MembershipDataProvider.GetSettings())
                {
                    switch (obj.SettingKey)
                    {
                        case "SELECTED_PASSWORD_FORMAT":
                            _PasswordFormat = int.Parse(obj.SettingValue);
                            break;
                    }
                }
                return _PasswordFormat;
            }
        }
        /// <summary>
        /// Get true if enable captche for registration.
        /// </summary>
        public override bool EnableCaptcha
        {
            get
            {
                bool _EnableCaptcha = (int)SettingsEnum.DEFAULT_CAPTCHA_STATUS == 1 ? true : false;
                foreach (SettingInfo obj in MembershipDataProvider.GetSettings())
                {
                    switch (obj.SettingKey)
                    {
                        case "ENABLE_CAPTCHA":
                            _EnableCaptcha = int.Parse(obj.SettingValue) == 1 ? true : false;
                            break;
                    }
                }
                return _EnableCaptcha;

            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <param name="status">User creation status. <see cref="T:SageFrame.Security.Helpers.UserCreationStatus"/></param>
        /// <param name="mode">User creation mode.<see cref="T:SageFrame.Security.Helpers.UserCreationMode"/></param>
        /// <returns>True for create successfully.</returns>
        public override bool CreateUser(UserInfo user, out UserCreationStatus status, UserCreationMode mode)
        {
            try
            {
                return (MembershipDataProvider.CreatePortalUser(user, out status, mode));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <returns>True for deleted successfully.</returns>
        public override bool DeleteUser(UserInfo user)
        {
            return (MembershipDataProvider.DeleteUser(user));
        }
        /// <summary>
        /// Delete user based on user ID.
        /// </summary>
        /// <param name="UserID">UserID</param>
        /// <returns>True for deleted successfully.</returns>
        public override bool DeleteUser(Guid UserID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Delete user based on portal ID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>True for deleted successfully.</returns>
        public override bool DeleteUser(int PortalID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Delete user based on PortalID and user name.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <returns>True for deleted successfully.</returns>
        public override bool DeleteUser(int PortalID, string UserName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Delete user based on user name.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <returns>True for deleted successfully.</returns>
        public override bool DeleteUser(string UserName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Obtain users based on PortalID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of UserInfo class.</returns>
        public override List<UserInfo> GetUsers(int PortalID)
        {
            return (MembershipDataProvider.GetPortalUsers(PortalID));
        }
        /// <summary>
        /// Obtain registered unapproved user.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of UserInfo class.</returns>
        public override List<UserInfo> GetUnApprovedUsers(int PortalID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Obtain users.
        /// </summary>
        /// <returns>List of UserInfo class.</returns>
        public override List<UserInfo> GetUsers()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            List<UserInfo> lstUsers = new List<UserInfo>();
            foreach (MembershipUser user in users)
            {

                lstUsers.Add(new UserInfo(user.UserName, "", user.Email, 1, Guid.NewGuid()));
            }
            return lstUsers;
        }
        /// <summary>
        /// Obtain user details based on UserID.
        /// </summary>
        /// <param name="UserID">UserID</param>
        /// <returns>Object of UserInfo class.</returns>
        public override UserInfo GetUserDetails(Guid UserID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///  Obtain user details based on PortalID and user name.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <returns>Object of UserInfo class.</returns>
        public override UserInfo GetUserDetails(int PortalID, string UserName)
        {
            return (MembershipDataProvider.GetUserDetails(UserName, PortalID));
        }

        #region "For Mobile"
        /// <summary>
        /// Obtain user details for mobile based on PortalID and user name.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">user name.</param>
        /// <returns>Object of UserInfoMob class.</returns>
        public UserInfoMob GetUserDetailsMob(int PortalID, string UserName)
        {
            return (MembershipDataProvider.GetUserDetailsMob(UserName, PortalID));

        }
        #endregion
        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <param name="status">User creation status. <see cref="T:SageFrame.Security.Helpers.UserCreationStatus"/></param>
        /// <returns>True for update sucessfully. </returns>
        public override bool UpdateUser(UserInfo user, out UserCreationStatus status)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Obtain all application users.
        /// </summary>
        /// <returns>Object of SageFrameUserCollection class.</returns>
        public override SageFrameUserCollection GetAllUsers()
        {
            return (MembershipDataProvider.GetAllUsers());
        }
        /// <summary>
        /// Change user password.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <returns>True for change password sucessfully.</returns>
        public override bool ChangePassword(UserInfo user)
        {
            return (MembershipDataProvider.ChangePassword(user));
        }
        /// <summary>
        /// Search application user.
        /// </summary>
        /// <param name="RoleID">RoleID</param>
        /// <param name="SearchText">Search text.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <returns>Object of SageFrameUserCollection class.</returns>
        public override SageFrameUserCollection SearchUsers(string RoleID, string SearchText, int PortalID, string UserName)
        {
            return (MembershipDataProvider.SearchUsers(RoleID, SearchText, PortalID, UserName));
        }
        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <param name="status">Update user status.</param>
        /// <returns>True for update user information sucessfully.</returns>
        public override bool UpdateUser(UserInfo user, out UserUpdateStatus status)
        {
            return (MembershipDataProvider.UpdateUser(user, out status));
        }
        /// <summary>
        /// Activate registered user.
        /// </summary>
        /// <param name="ActivationCode">Activation code.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>User name.</returns>
        public override string ActivateUser(string ActivationCode, int PortalID)
        {
            return (MembershipDataProvider.ActivateUser(ActivationCode, PortalID));
        }
        /// <summary>
        /// Activate registered user. 
        /// </summary>
        /// <param name="ActivationCode">Activation code.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="StoreID">StoreID</param>
        /// <returns>User name.</returns>
        public override string ActivateUser(string ActivationCode, int PortalID, int StoreID)
        {
            return (MembershipDataProvider.ActivateUser(ActivationCode, PortalID, StoreID));
        }
        #endregion
        /// <summary>
        /// Edit user permission.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <returns>True for update user information sucessfully.</returns>
        public override bool EditPermissionExists(int UserModuleID, int PortalID, string UserName)
        {
            return (MembershipDataProvider.EditPermissionExists(UserModuleID, PortalID, UserName));
        }
        /// <summary>
        /// Obtain user information based on email and PortalID.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="portalID">PortalID</param>
        /// <returns>Object of UserInfo class.</returns>
        public UserInfo GerUserByEmail(string email, int portalID)
        {
            return (MembershipDataProvider.GerUserByEmail(email, portalID));
        }
    }
}
