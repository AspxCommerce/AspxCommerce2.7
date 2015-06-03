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
using SageFrame.ModuleManager;
using System.Data;
using SageFrame.Common;
#endregion

namespace SageFrame.ModuleManager.DataProvider
{
    /// <summary>
    ///  Manupulates data for ModuleDataProvider.
    /// </summary>
    public class ModuleDataProvider
    {
        /// <summary>
        /// Connect to database and add user module.
        /// </summary>
        /// <param name="module">Object of UserModuleInfo class.</param>
        /// <returns>Newly added user module ID.</returns>
        public string AddUserModule(UserModuleInfo module)
        {
            SageFrameSQLHelper sqlH = new SageFrameSQLHelper();
            SqlTransaction tran = (SqlTransaction)sqlH.GetTransaction();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {                                                                              
                                                                                new KeyValuePair<string, object>(
                                                                                    "@ModuleDefID", module.ModuleDefID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@UserModuleTitle", module.UserModuleTitle),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AllPages", module.AllPages),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@ShowInPages", module.ShowInPages),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@InheritViewPermissions", module.InheritViewPermissions),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IsActive", module.IsActive),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AddedOn", DateTime.Now),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PortalID", module.PortalID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AddedBy", module.PortalID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@SEOName", module.SEOName),
                                                                                new KeyValuePair<string,object>(
                                                                                    "@IsHandheld",module.IsHandheld),
                                                                                new KeyValuePair<string,object>(
                                                                                    "@SuffixClass",module.SuffixClass),
                                                                                new KeyValuePair<string,object>(
                                                                                    "@HeaderText",module.HeaderText),
                                                                                new KeyValuePair<string,object>(
                                                                                    "@ShowHeaderText",module.ShowHeaderText),
                                                                                new KeyValuePair<string,object>(
                                                                                    "@IsInAdmin",module.IsInAdmin) 
                                                                            };

                List<KeyValuePair<string, object>> ParaMeterInputCollection = new List<KeyValuePair<string, object>>
                                                                            {                                                                              
                                                                                new KeyValuePair<string, object>(
                                                                                    "@UserModuleID", 0),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@ControlCount", 0)                                                                                
                                                                            };


                List<KeyValuePair<int, string>> resultColl = new List<KeyValuePair<int, string>>();
                resultColl = sqlH.ExecuteNonQueryWithMultipleOutput(tran, CommandType.StoredProcedure, "[dbo].[usp_UserModulesAdd]", ParaMeterCollection, ParaMeterInputCollection);
                if (int.Parse(resultColl[0].Value) > 0)
                {
                    if (module.InheritViewPermissions)
                    {
                        //if (module.IsInAdmin)
                        //{
                        //    module.PortalID = -1;
                        //}
                        SetUserModuleInheritedPermission(module.PageID, tran, int.Parse(resultColl[0].Value), module.PortalID, module.AddedBy, module.ModuleDefID);
                    }
                    else
                    {
                        SetUserModulePermission(module.LSTUserModulePermission, tran, int.Parse(resultColl[0].Value), module.PortalID, module.AddedBy, module.ModuleDefID);
                    }
                    if (module.IsInAdmin)
                    {
                        module.PortalID = -1;
                    }
                    SetPageModules(module, tran, int.Parse(resultColl[0].Value), module.PortalID, module.AddedBy);

                }
                tran.Commit();
                return (string.Format("{0}_{1}", resultColl[0].Value.ToString(), resultColl[1].Value.ToString()));

            }
            catch (SqlException sqlEx)
            {
                tran.Rollback();
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connect to database and set user module permission.
        /// </summary>
        /// <param name="lstPermission">List of ModulePermissionInfo class.</param>
        /// <param name="tran">Object of SqlTransaction</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="AddedBy">User name.</param>
        /// <param name="ModuleDefID">ModuleDefID</param>
        public void SetUserModulePermission(List<ModulePermissionInfo> lstPermission, SqlTransaction tran, int UserModuleID, int PortalID, string AddedBy, int ModuleDefID)
        {
            try
            {

                foreach (ModulePermissionInfo objPerm in lstPermission)
                {
                    if (objPerm == null) continue;
                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@ModuleDefID", ModuleDefID),                                                                                 
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserModuleID",UserModuleID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AllowAccess",objPerm.AllowAccess),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@RoleID", objPerm.RoleID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserName", objPerm.UserName),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@IsActive", true),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedOn", DateTime.Now),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PortalID", PortalID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedBy", AddedBy),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PermissionID", objPerm.PermissionID)
                                                                                };
                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_UserModulesPermissionAdd]",
                                         ParaMeterCollection);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Connect to database and set user inherite permission.
        /// </summary>
        /// <param name="PageID">PageID</param>
        /// <param name="tran">Object of SqlTransaction</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="AddedBy">User name.</param>
        /// <param name="ModuleDefID">ModuleDefID</param>
        public void SetUserModuleInheritedPermission(int PageID, SqlTransaction tran, int UserModuleID, int PortalID, string AddedBy, int ModuleDefID)
        {
            try
            {

                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@ModuleDefID", ModuleDefID),                                                                                 
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserModuleID",UserModuleID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AllowAccess",true),                                                                                   
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@IsActive", true),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedOn", DateTime.Now),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PortalID", PortalID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedBy", AddedBy),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PageID", PageID)
                                                                                };
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_UserModulesInheritedPermissionAdd]",
                                     ParaMeterCollection);



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Connect to database and update user module inherit permission.
        /// </summary>
        /// <param name="PageID">PageID</param>
        /// <param name="tran">object of SqlTransaction.</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="AddedBy">User name.</param>
        /// <param name="ModuleDefID">ModuleDefID</param>
        public static void UpdateUserModuleInheritedPermission(int PageID, SqlTransaction tran, int UserModuleID, int PortalID, string AddedBy, int ModuleDefID)
        {
            try
            {

                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@ModuleDefID", ModuleDefID),                                                                                 
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserModuleID",UserModuleID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AllowAccess",true),                                                                                   
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@IsActive", true),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedOn", DateTime.Now),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PortalID", PortalID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedBy", AddedBy),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PageID", PageID)
                                                                                };
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_UserModulesInheritedPermissionAdd]",
                                     ParaMeterCollection);



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Connect to database and set page modules.
        /// </summary>
        /// <param name="module">Object of UserModuleInfo class.</param>
        /// <param name="tran">Object of SqlTransaction class.</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="AddedBy">User name.</param>
        public void SetPageModules(UserModuleInfo module, SqlTransaction tran, int UserModuleID, int PortalID, string AddedBy)
        {

            try
            {


                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PageID", module.PageID),                                                                                 
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserModuleID",UserModuleID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PaneName",module.PaneName),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@ModuleOrder", module.ModuleOrder),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@CacheTime",module.CacheTime),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@Alignment", module.Alignment),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@Color", module.Color),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@Border", ""),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@IconFile", "none"),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@Visibility", true),                                                                                       
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@IsActive", true),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedOn", DateTime.Now),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PortalID",PortalID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedBy", AddedBy)

                                                                                };
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_PageModulesAdd]",
                                     ParaMeterCollection);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Connect to database and obtain list of page module list.
        /// </summary>
        /// <param name="PageID">PageID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="IsHandheld">true for handheld devise.</param>
        /// <returns>List of UserModuleInfo class.</returns>
        public static List<UserModuleInfo> GetPageModules(int PageID, int PortalID, bool IsHandheld)
        {

            List<UserModuleInfo> lstUserModules = new List<UserModuleInfo>();

            string StoredProcedureName = "[dbo].[usp_ModuleManagerGetPageModules]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                         {
                                                                             new KeyValuePair<string, object>(
                                                                                 "@PageID", PageID),
                                                                             new KeyValuePair<string, object>(
                                                                                 "@PortalID", PortalID),
                                                                             new KeyValuePair<string, object>(
                                                                                 "@IsHandheld", IsHandheld),
                                                                         };
            try
            {
                SQLHandler sagesql = new SQLHandler();
                lstUserModules = sagesql.ExecuteAsList<UserModuleInfo>(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstUserModules;
        }

        /// <summary>
        /// Connect to database and delete user module based on PortalID.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="DeletedBy">User name.</param>
        public static void DeleteUserModule(int UserModuleID, int PortalID, string DeletedBy)
        {
            List<UserModuleInfo> lstUserModules = new List<UserModuleInfo>();
            string StoredProcedureName = "[dbo].[usp_UserModulesDelete]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                         {
                                                                             new KeyValuePair<string, object>(
                                                                                 "@UserModuleID", UserModuleID),
                                                                             new KeyValuePair<string, object>(
                                                                                 "@PortalID", PortalID),
                                                                             new KeyValuePair<string, object>(
                                                                                 "@DeletedBy", DeletedBy)
                                                                         };

            try
            {
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// Connect to database and update page modules.
        /// </summary>
        /// <param name="lstPageModules">List of PageModuleInfo class.</param>
        public static void UpdatePageModule(List<PageModuleInfo> lstPageModules)
        {

            string StoredProcedureName = "[dbo].[usp_PageModule_Update]";
            foreach (PageModuleInfo pm in lstPageModules)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserModuleID", pm.UserModuleID),                                                                                 
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PaneName",pm.PaneName),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@ModuleOrder",pm.ModuleOrder)
                                                                                };

                try
                {
                    SQLHandler sagesql = new SQLHandler();
                    sagesql.ExecuteNonQuery(StoredProcedureName, ParaMeterCollection);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }
        /// <summary>
        /// Connect to database and obtain page user module .
        /// </summary>
        /// <param name="IsAdmin">true for admin modules.</param>
        /// <returns>List of UserModuleInfo class.</returns>
        public static List<UserModuleInfo> GetPageUserModules(bool IsAdmin)
        {
            List<UserModuleInfo> lstUserModules = new List<UserModuleInfo>();
            string StoredProcedureName = "[dbo].[usp_usermodulesgetpagemodules]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@IsAdmin", IsAdmin)                                                                               
                                                                                    
                                                                                };
            try
            {
                SQLHandler sagesql = new SQLHandler();
                lstUserModules = sagesql.ExecuteAsList<UserModuleInfo>(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }


            return lstUserModules;
        }
        /// <summary>
        /// Connect to database and obtain user module details.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID"></param>
        /// <returns>Object of UserModuleInfo class.</returns>
        public static UserModuleInfo GetUserModuleDetails(int UserModuleID, int PortalID)
        {
            UserModuleInfo objUserModule = new UserModuleInfo();
            string StoredProcedureName = "[dbo].[usp_UserModulesGetDetails]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserModuleID", UserModuleID),                                                                                 
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PortalID",PortalID)
                                                                                };

            try
            {
                SQLHandler sagesql = new SQLHandler();
                objUserModule = sagesql.ExecuteAsObject<UserModuleInfo>(StoredProcedureName, ParaMeterCollection);
                objUserModule.LSTUserModulePermission = GetModulePermission(UserModuleID, PortalID);

            }
            catch (Exception e)
            {
                throw e;
            }


            return objUserModule;
        }
        /// <summary>
        /// Connect to database and obtain module permission.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of ModulePermissionInfo class.</returns>
        public static List<ModulePermissionInfo> GetModulePermission(int UserModuleID, int PortalID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {
                                                                            new KeyValuePair<string, object>("@UserModuleID",
                                                                                                             UserModuleID),
                                                                            new KeyValuePair<string, object>(
                                                                                "@PortalID", PortalID)
                                                                        };
            SQLHandler sqlH = new SQLHandler();
            return (sqlH.ExecuteAsList<ModulePermissionInfo>("[usp_UserModuleGetPermissions]", ParaMeterCollection));


        }
        /// <summary>
        /// Connect to database and update user module.
        /// </summary>
        /// <param name="module">Object of UserModuleInfo class.</param>

        public static void UpdateUserModule(UserModuleInfo module)
        {
            SQLHandler sqlH = new SQLHandler();
            SqlTransaction tran = (SqlTransaction)sqlH.GetTransaction();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                               new KeyValuePair<string, object>(
                                                                                    "@UserModuleID", module.UserModuleID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@ModuleDefID", module.ModuleDefID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@UserModuleTitle", module.UserModuleTitle),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AllPages", module.AllPages),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@ShowInPages", module.ShowInPages),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@InheritViewPermissions", module.InheritViewPermissions),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IsActive", module.IsActive),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AddedOn", DateTime.Now),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PortalID", module.PortalID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AddedBy", module.PortalID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@SEOName", module.SEOName),
                                                                                new KeyValuePair<string,object>(
                                                                                    "@IsHandheld",module.IsHandheld),
                                                                                new KeyValuePair<string,object>(
                                                                                    "@SuffixClass",module.SuffixClass),
                                                                                new KeyValuePair<string,object>(
                                                                                    "@HeaderText",module.HeaderText),
                                                                                new KeyValuePair<string,object>(
                                                                                    "@ShowHeaderText",module.ShowHeaderText) 
                                                                                    
                                                                            };


                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_UserModulesUpdate]", ParaMeterCollection);
                if (module.InheritViewPermissions)
                {
                    UpdateUserModuleInheritedPermission(module.PageID, tran, module.UserModuleID, module.PortalID, module.AddedBy, module.ModuleDefID);

                }
                else
                {
                    UpdateUserModulePermission(module.LSTUserModulePermission, tran, module.UserModuleID, module.PortalID, module.AddedBy, module.ModuleDefID);

                }


                tran.Commit();


            }
            catch (SqlException sqlEx)
            {
                tran.Rollback();
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connect to database and update user module permission.
        /// </summary>
        /// <param name="lstPermission">List of ModulePermissionInfo class.</param>
        /// <param name="tran">Object of SqlTransaction class.</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="AddedBy">User name.</param>
        /// <param name="ModuleDefID">ModuleDefID</param>
        public static void UpdateUserModulePermission(List<ModulePermissionInfo> lstPermission, SqlTransaction tran, int UserModuleID, int PortalID, string AddedBy, int ModuleDefID)
        {


            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamColl = new List<KeyValuePair<string, object>>
                                                                        {
                                                                            new KeyValuePair<string, object>("@UserModuleID",
                                                                                                             UserModuleID),
                                                                            new KeyValuePair<string, object>(
                                                                                "@PortalID", PortalID)
                                                                        };
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_UserModulePermissionDelete]",
                                        ParamColl);


                foreach (ModulePermissionInfo objPerm in lstPermission)
                {
                    if (objPerm == null) continue;
                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@ModuleDefID", ModuleDefID),                                                                                 
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserModuleID",UserModuleID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AllowAccess",objPerm.AllowAccess),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@RoleID", objPerm.RoleID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserName", objPerm.UserName),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@IsActive", true),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedOn", DateTime.Now),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PortalID", PortalID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedBy", AddedBy),
                                                                                         new KeyValuePair<string, object>(
                                                                                        "@PermissionID", objPerm.PermissionID)
                                                                                };

                    sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_UserModulesPermissionAdd]",
                                         ParaMeterCollection);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connect to database and publish page.
        /// </summary>
        /// <param name="pageId">pageId</param>
        /// <param name="isPublish">true for publis</param>
        /// <returns>true for publish.</returns>
        internal static bool PublishPage(int pageId, bool isPublish)
        {
            try
            {
                List<KeyValuePair<string, object>> para = new List<KeyValuePair<string, object>>();
                para.Add(new KeyValuePair<string, object>("@PageId", pageId));
                para.Add(new KeyValuePair<string, object>("@IsPublished", isPublish));
                SQLHandler handler = new SQLHandler();
                handler.ExecuteNonQuery("[dbo].[usp_PagePublish]", para);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
