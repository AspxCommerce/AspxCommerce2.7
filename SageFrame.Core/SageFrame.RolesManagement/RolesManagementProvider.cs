/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using System.Data.SqlClient;

namespace SageFrame.RolesManagement
{
    /// <summary>
    /// Manipulates the data for roles.
    /// </summary>
    public class RolesManagementProvider
    {
        /// <summary>
        /// Connects to database and returns roles details by role name.
        /// </summary>
        /// <param name="RoleName">Role name.</param>
        /// <returns>Role details.</returns>
        public RolesManagementInfo GetRoleIDByRoleName(string RoleName)
        {
            SqlDataReader reader = null;
            try
            {

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@RoleName", RoleName));
                reader = SQLH.ExecuteAsDataReader("[dbo].[sp_GetRoleIDByRoleName]", ParamCollInput);
                RolesManagementInfo objList = new RolesManagementInfo();
                while (reader.Read())
                {
                    objList.ApplicationId = new Guid(reader["ApplicationId"].ToString());
                    objList.RoleId = new Guid(reader["RoleId"].ToString());
                    objList.RoleName = reader["RoleName"].ToString();
                    objList.LoweredRoleName = reader["LoweredRoleName"].ToString();
                    objList.Description = reader["Description"].ToString();
                }
                reader.Close();
                return objList;
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// Connects to database and returns all the portal role list.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="IsAll">set true if all the role is to be return.</param>
        /// <param name="Username">User's name.</param>
        /// <returns></returns>
        public List<RolesManagementInfo> PortalRoleList(int PortalID, bool IsAll, string Username)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsAll", IsAll));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                return SQLH.ExecuteAsList<RolesManagementInfo>("[dbo].[sp_PortalRoleList]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and returns portal selected roles.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="Username">User's name.</param>
        /// <returns>List of roles details</returns>
        public List<RolesManagementInfo> GetPortalRoleSelectedList(int PortalID, string Username)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                return SQLH.ExecuteAsList<RolesManagementInfo>("[dbo].[usp_GetPortalRoleSelectedList]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and returns list of portal roles.
        /// </summary>
        /// <returns>List of portal roles</returns>
        public List<RolesManagementInfo> GetSageFramePortalList()
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                return SQLH.ExecuteAsList<RolesManagementInfo>("[dbo].[usp_GetSageFramePortalList]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Returns list of dashboard roles.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <returns>List of dashboard roles</returns>
        public List<RolesManagementInfo> DashboardRoleList(int portalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                return SQLH.ExecuteAsList<RolesManagementInfo>("[dbo].[usp_Dashboard_GetAssignedRoles]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
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
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@RoleID", roleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLH.ExecuteNonQuery("[dbo].[usp_Dashboard_InsertRoles]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
