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
using System.Data.SqlClient;
using System.Data;
using SageFrame.Web;
using SageFrame.ModuleManager;
#endregion

namespace SageFrame.ModuleManager
{
    /// <summary>
    /// Manupulates data for LayoutMgrDataProvider.
    /// </summary>
    public class LayoutMgrDataProvider
    {
        /// <summary>
        /// Connect to database and get modules based on PortalID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of LayoutMgrInfo class.</returns>
        public static List<LayoutMgrInfo> GetModules(int PortalID)
        {
            SQLHandler SQLH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            return SQLH.ExecuteAsList<LayoutMgrInfo>("[usp_ModuleManagerGetPortalModules]",ParaMeterCollection);
        }
        /// <summary>
        /// Connect to database and obtain admin modules.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of LayoutMgrInfo.</returns>
        public static List<LayoutMgrInfo> GetAdminModules(int PortalID)
        {
            SQLHandler SQLH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            return SQLH.ExecuteAsList<LayoutMgrInfo>("[dbo].[usp_ModuleManagerGetAdminModules]",ParaMeterCollection);
        }
        /// <summary>
        /// Connect to database and obtain module information based onmodule name.
        /// </summary>
        /// <param name="ModuleName">Module name.</param>
        /// <returns>List of LayoutMgrInfo class.</returns>
        public static List<LayoutMgrInfo> GetModuleInformation(string ModuleName)
        {
            SQLHandler SQLH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FriendlyName", ModuleName));

            return SQLH.ExecuteAsList<LayoutMgrInfo>("[dbo].[usp_GetModuleInformation]", ParaMeterCollection);
        }
        /// <summary>
        /// Connect to database and search module.
        /// </summary>
        /// <param name="search">Searching string.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="IsAdmin">true if admin modules.</param>
        /// <returns>List of LayoutMgrInfo class.</returns>
        public static List<LayoutMgrInfo> SearchModules(string search,int PortalID,bool IsAdmin)
        {
            SQLHandler SQLH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@SearchText", search));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsAdmin", IsAdmin));
            return SQLH.ExecuteAsList<LayoutMgrInfo>("[dbo].[usp_ModuleManagerGetSearchModules]", ParaMeterCollection);
        }
        /// <summary>
        /// Connect to database and obtain sort modules.
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="isAdmin">true for admin.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="IncludePortalModules">IncludePortalModules</param>
        /// <returns>List of LayoutMgrInfo class.</returns>
        public static List<LayoutMgrInfo> GetSortModules(int flag, bool isAdmin, int PortalID, int IncludePortalModules)
        {
            SQLHandler SQLH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@flag", flag));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@isAdmin", isAdmin));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IncludePortalModules", IncludePortalModules));
            try
            {
                return SQLH.ExecuteAsList<LayoutMgrInfo>("[dbo].[usp_ModuleMgrSortModules]", ParaMeterCollection);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and add lay out.
        /// </summary>
        /// <param name="obj">Object of LayoutMgrInfo class.</param>
        /// <returns>Newly added ID.</returns>
        public static int AddLayOutMgr(LayoutMgrInfo obj)
        {
            try
            {

                SqlConnection SQLConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                SqlCommand SQLCmd = new SqlCommand();
                int ReturnValue = -1;
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = "[dbo].[usp_AddModulesOrder]";
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.Parameters.Add(new SqlParameter("@ModuleOrder", obj.ModuleOrder));
                SQLCmd.Parameters.Add(new SqlParameter("@PortelID", obj.PortelID));
                SQLCmd.Parameters.Add(new SqlParameter("@ModuleID", obj.ModuleID));
                SQLCmd.Parameters.Add(new SqlParameter("@ModuleName", obj.ModuleName));
                SQLCmd.Parameters.Add(new SqlParameter("@PaneName", obj.PaneName));
                SQLCmd.Parameters.Add(new SqlParameter("@UserModuleID", obj.UserModuleID));

                SQLCmd.Parameters.Add(new SqlParameter("@NewModuleID", SqlDbType.Int));
                SQLCmd.Parameters["@NewModuleID"].Direction = ParameterDirection.Output;

                SQLConn.Open();
                SQLCmd.ExecuteNonQuery();
                ReturnValue = (int)SQLCmd.Parameters["@NewModuleID"].Value;
                return ReturnValue;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
