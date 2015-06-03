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

#endregion


namespace SageFrame.RolesManagement
{
    /// <summary>
    /// Business logic for roles.
    /// </summary>
    public class RolesManagementController
    {
        /// <summary>
        /// Returns roles details by role name.
        /// </summary>
        /// <param name="RoleName">Role name.</param>
        /// <returns>Role details.</returns>
        public RolesManagementInfo GetRoleIDByRoleName(string RoleName)
        {
            try
            {
                RolesManagementProvider objProvider = new RolesManagementProvider();
                return objProvider.GetRoleIDByRoleName(RoleName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns all the portal role list.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="IsAll">set true if all the role is to be return.</param>
        /// <param name="Username">User's name.</param>
        /// <returns></returns>
        public List<RolesManagementInfo> PortalRoleList(int PortalID, bool IsAll, string Username)
        {
            try
            {
                RolesManagementProvider objProvider = new RolesManagementProvider();
                return objProvider.PortalRoleList(PortalID, IsAll, Username);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns portal selected roles.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="Username">User's name.</param>
        /// <returns>List of roles details</returns>
        public List<RolesManagementInfo> GetPortalRoleSelectedList(int PortalID, string Username)
        {
            try
            {
                RolesManagementProvider objProvider = new RolesManagementProvider();
                return objProvider.GetPortalRoleSelectedList(PortalID, Username);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Returns list of portal roles.
        /// </summary>
        /// <returns>List of portal roles</returns>
        public List<RolesManagementInfo> GetSageFramePortalList()
        {
            try
            {
                RolesManagementProvider objProvider = new RolesManagementProvider();
                return objProvider.GetSageFramePortalList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Returns list of dashboard roles.
        /// </summary>
        /// <returns>List of dashboard roles</returns>
        public List<RolesManagementInfo> DashboardRoleList(int portalID)
        {
            try
            {
                RolesManagementProvider objProvider = new RolesManagementProvider();
                return objProvider.DashboardRoleList(portalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connect to database and add update dashboard role list
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="RoleID">RoleID</param>
        /// <param name="UserName">UserName</param>
        public void UpdateDashboardRoleList(int portalID, string roleID, string userName)
        {
            try
            {
                RolesManagementProvider objProvider = new RolesManagementProvider();
                objProvider.UpdateDashboardRoleList(portalID, roleID, userName);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
