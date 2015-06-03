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
using SageFrame.Security.Helpers;
using SageFrame.Security.Providers;
#endregion

namespace SageFrame.Security
{
    /// <summary>
    ///  Business logic for RoleController inherited from SageFrameRoleProvider.
    /// </summary>
    public class RoleController : SageFrameRoleProvider
    {
        /// <summary>
        /// Create role.
        /// </summary>
        /// <param name="role">Object of RoleInfo class.</param>
        /// <param name="status">Role creation status.<see cref="T: SageFrame.Security.Helpers.RoleCreationStatus"/></param>
        public override void CreateRole(RoleInfo role, out RoleCreationStatus status)
        {
            MembershipDataProvider.CreateRole(role, out status);
        }
        /// <summary>
        /// Delete role based on role name..
        /// </summary>
        /// <param name="RoleName">Role name.</param>
        public override void DeleteRole(string RoleName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Delete role base on role name and PortalID. 
        /// </summary>
        /// <param name="RoleName"></param>
        /// <param name="PortalID"></param>
        public override void DeleteRole(string RoleName, int PortalID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Delete role base on RoleID.
        /// </summary>
        /// <param name="RoleID"></param>
        public override void DeleteRole(Guid RoleID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Obtain application role.
        /// </summary>
        /// <param name="RoleID">RoleID</param>
        /// <returns>Object of RoleInfo class.</returns>
        public override RoleInfo GetRole(Guid RoleID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Obtain application role.
        /// </summary>
        /// <param name="PortalRoleID">PortalRoleID</param>
        /// <returns>Object of RoleInfo class.</returns>
        public override RoleInfo GetRole(int PortalRoleID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Obtain role name based on PortalID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Array of string  of role name.</returns>
        public override string[] GetRoleNames(int PortalID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///  Obtain role name based on PortalID and UserID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserID">UserID</param>
        /// <returns>Array of string  of role name.</returns>
        public override string[] GetRoleNames(int PortalID, int UserID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Update role.
        /// </summary>
        /// <param name="role">Object of RoleInfo class.</param>
        public override void UpdateRole(RoleInfo role)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Assigne role to user.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="user">Object of UserInfo class.</param>
        /// <param name="UserRole">Object of UserRoleInfo class.</param>
        /// <returns>True for assigne role to user successfully.</returns>
        public override bool AddUserToRole(int PortalID, UserInfo user, UserRoleInfo UserRole)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Obtain user role.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserID">UserID</param>
        /// <param name="RoleID">RoleID</param>
        /// <returns>Object of UserRoleInfo class.</returns>
        public override UserRoleInfo GetUserRole(int PortalID, int UserID, int RoleID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Obtain user based on PortalID and role name.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="RoleName">Role name.</param>
        /// <returns>Array list of user name. </returns>
        public override System.Collections.ArrayList GetUsersByRoleName(int PortalID, string RoleName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Remove user from application role.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="user">User name.</param>
        /// <param name="userRole">User role.</param>
        public override void RemoveUserFromRole(int PortalID, UserInfo user, UserRoleInfo userRole)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Update user role.
        /// </summary>
        /// <param name="userRole">Object of UserRoleInfo class.</param>
        public override void UpdateUserRole(UserRoleInfo userRole)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Obtain application portal roles.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="IsAll">1 for all roles.</param>
        /// <param name="UserName">User name.</param>
        /// <returns>List of RoleInfo class.</returns>
        public override List<RoleInfo> GetPortalRoles(int PortalID, int IsAll, string UserName)
        {
            return (MembershipDataProvider.GetPortalRoles(PortalID, IsAll, UserName));
        }
        /// <summary>
        /// Delete roles.
        /// </summary>
        /// <param name="RoleID">RoleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>True for deleted successfully.</returns>
        public override bool DeleteRole(Guid RoleID, int PortalID)
        {
            return (MembershipDataProvider.DeletePortalRole(RoleID, PortalID));
        }
        /// <summary>
        /// Obtain roles name.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Role names with comma separator.</returns>
        public override string GetRoleNames(string UserName, int PortalID)
        {
            return (MembershipDataProvider.GetRoleNames(UserName, PortalID));
        }
        /// <summary>
        /// Delete user from roles.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <returns>True for deleted successfully.</returns>
        public override bool DeleteUserInRoles(UserInfo user)
        {
            return (MembershipDataProvider.DeleteUserInRoles(user));
        }
        /// <summary>
        /// Add user in role.
        /// </summary>
        /// <param name="user">Object of UserInfo class.</param>
        /// <returns>True for add user in role successfully.</returns>
        public override bool AddUserToRoles(UserInfo user)
        {
            return (MembershipDataProvider.AddUserInRoles(user));
        }
        /// <summary>
        /// Change user in role.
        /// </summary>
        /// <param name="ApplicationName">Application name.</param>
        /// <param name="UserID">UserID</param>
        /// <param name="RoleNamesUnselected">Unselected role name.</param>
        /// <param name="RoleNamesSelected">Selected role name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>True for change user role successfully.</returns>
        public override bool ChangeUserInRoles(string ApplicationName, Guid UserID, string RoleNamesUnselected, string RoleNamesSelected, int PortalID)
        {
            return (MembershipDataProvider.ChangeUserInRoles(ApplicationName, UserID, RoleNamesUnselected, RoleNamesSelected, PortalID));
        }
        /// <summary>
        /// Connect to the database and check condition for dashboard access.
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// /// <param name="PortalID">PortalID</param>
        /// <returns></returns>
        public bool IsDashboardAccesible(string userName, int portalID)
        {
            MembershipDataProvider objProvider = new MembershipDataProvider();
            return objProvider.IsDashboardAccesible(userName, portalID);
        }
    }
}
