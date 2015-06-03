#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
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
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Security.Helpers;
using SageFrame.Security.Enums;
#endregion

namespace SageFrame.Security
{
    /// <summary>
    /// Abstract modifier for SageFrameMembershipProvider.
    /// </summary>
    public abstract class SageFrameMembershipProvider
    {
        public abstract bool RequireUniqueEmail{ get; }
        public abstract int PasswordFormat { get; }
        public abstract bool EnableCaptcha { get; }
        public abstract bool CreateUser(UserInfo user, out UserCreationStatus status,UserCreationMode mode);
        public abstract bool DeleteUser(UserInfo user);
        public abstract bool DeleteUser(Guid UserID);
        public abstract bool DeleteUser(int PortalID);
        public abstract bool DeleteUser(int PortalID, string UserName);
        public abstract bool DeleteUser(string UserName);
        public abstract List<UserInfo> GetUsers(int PortalID);
        public abstract List<UserInfo> GetUnApprovedUsers(int PortalID);
        public abstract List<UserInfo> GetUsers();
        public abstract SageFrameUserCollection GetAllUsers();
        public abstract SageFrameUserCollection SearchUsers(string RoleID, string SearchText, int PortalID, string UserName);
        public abstract UserInfo GetUserDetails(Guid UserID);
        public abstract UserInfo GetUserDetails(int PortalID, string UserName);
        public abstract bool UpdateUser(UserInfo user, out UserCreationStatus status);
        public abstract bool UpdateUser(UserInfo user, out UserUpdateStatus status);
        public abstract bool ChangePassword(UserInfo user);
        public abstract string ActivateUser(string ActivationCode, int PortalID);
        public abstract string ActivateUser(string ActivationCode, int PortalID, int StoreID);
        public abstract bool EditPermissionExists(int UserModuleID, int PortalID, string UserName);
    }
}
