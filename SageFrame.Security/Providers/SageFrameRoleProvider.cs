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
using System.Collections;
#endregion

namespace SageFrame.Security
{
    /// <summary>
    /// Abstract modifier for SageFrameRoleProvider.
    /// </summary>
    public abstract class SageFrameRoleProvider
    {
        public abstract void CreateRole(RoleInfo role, out RoleCreationStatus status);
        public abstract void DeleteRole(string RoleName);
        public abstract void DeleteRole(string RoleName, int PortalID);
        public abstract bool DeleteRole(Guid RoleID, int PortalID);
        public abstract void DeleteRole(Guid RoleID);        
		public abstract RoleInfo GetRole(Guid RoleID);
        public abstract RoleInfo GetRole(int PortalRoleID);
        public abstract List<RoleInfo> GetPortalRoles(int PortalID, int IsAll, string UserName);
		public abstract string[] GetRoleNames(int PortalID);
		public abstract string[] GetRoleNames(int PortalID, int UserID);
        public abstract string GetRoleNames(string UserName, int PortalID);
		public abstract void UpdateRole(RoleInfo role);		
		public abstract bool AddUserToRole(int PortalID, UserInfo user, UserRoleInfo UserRole);
        public abstract bool AddUserToRoles(UserInfo user);
		public abstract UserRoleInfo GetUserRole(int PortalID, int UserID, int RoleID);		
		public abstract ArrayList GetUsersByRoleName(int PortalID, string RoleName);	
		public abstract void RemoveUserFromRole(int PortalID, UserInfo user, UserRoleInfo userRole);
		public abstract void UpdateUserRole(UserRoleInfo userRole);
        public abstract bool DeleteUserInRoles(UserInfo user);
        public abstract bool ChangeUserInRoles(string ApplicationName, Guid UserID, string RoleNamesUnselected, string RoleNamesSelected, int PortalID);
       // public abstract void CreateRoleImport(RoleInfo role, out RoleCreationStatus status);
       
    }
}
