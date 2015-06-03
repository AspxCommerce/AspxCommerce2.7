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
using SageFrame.Pages;
using SageFrame.PagePermission;
using System.Data.SqlClient;
using SageFrame.Web.Utilities;
using System.Data;
using SageFrame.Common;
#endregion

namespace SageFrame.Pages
{
    /// <summary>
    /// Manupulates data for PageDataProvider
    /// </summary>
    public class PageDataProvider
    {
        /// <summary>
        /// Connect to database and obtain front end menu.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="isAdmin">true for admin.</param>
        /// <returns></returns>
        public List<PageEntity> GetMenuFront(int PortalID, bool isAdmin)
        {
            try
            {
                List<PageEntity> lstPages = new List<PageEntity>();
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsAdmin", isAdmin));
                SQLHandler sagesql = new SQLHandler();
                lstPages = sagesql.ExecuteAsList<PageEntity>("[dbo].[usp_GetPages]", ParaMeterCollection);
                return lstPages;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Connect to database and add update pages.
        /// </summary>
        /// <param name="objPage">Object of PageEntity class.</param>
        public void AddUpdatePages(PageEntity objPage)
        {
            SQLHandler sqlH = new SQLHandler();
            SqlTransaction tran = (SqlTransaction)sqlH.GetTransaction();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PageID", objPage.PageID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PageOrder", objPage.PageOrder),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PageName", objPage.PageName.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IsVisible", objPage.IsVisible),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@ParentID", objPage.ParentID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IconFile", objPage.IconFile.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@DisableLink", objPage.DisableLink),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@Title", objPage.Title.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@Description", objPage.Description.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@KeyWords", objPage.KeyWords.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@Url", objPage.Url.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@StartDate", DateTime.Now),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@EndDate", DateTime.Now),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@RefreshInterval",
                                                                                    objPage.RefreshInterval),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PageHeadText",
                                                                                    objPage.PageHeadText.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IsSecure", objPage.IsSecure),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IsActive", objPage.IsActive),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IsShowInFooter",
                                                                                    objPage.IsShowInFooter),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IsRequiredPage",
                                                                                    objPage.IsRequiredPage),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@BeforeID", objPage.BeforeID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AfterID", objPage.AfterID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PortalID", objPage.PortalID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AddedBy", objPage.AddedBy.ToString()),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IsAdmin", objPage.IsAdmin), 
                                                                            };
                int pageID = 0;
                pageID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_AddUpdatePage", ParaMeterCollection, "@InsertedPageID");
                if (pageID > 0)
                {
                    // objPage.PortalID = objPage.PortalID == -1 ? 1 : objPage.PortalID;
                    AddUpdatePagePermission(objPage.LstPagePermission, tran, pageID, objPage.PortalID, objPage.AddedBy, objPage.IsAdmin);
                    if (!objPage.IsAdmin)
                    {
                        if (objPage.Mode == "A")
                        {
                            MenuPageUpdate(objPage.MenuList, tran, pageID);
                        }
                    }
                    if (objPage.MenuList != "0")
                    {
                        AddUpdateSelectedMenu(objPage.MenuList, tran, pageID, objPage.ParentID, objPage.Mode, objPage.Caption, objPage.PortalID, objPage.UpdateLabel);
                    }

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
        /// Connect to data base and obtain pages.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="isAdmin">true for admin pages.</param>
        /// <returns></returns>
        public List<PageEntity> GetPages(int PortalID, bool isAdmin)
        {
            List<PageEntity> lstPages = new List<PageEntity>();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsAdmin", isAdmin));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                lstPages = sagesql.ExecuteAsList<PageEntity>("[dbo].[usp_GetPages]", ParaMeterCollection);
                return lstPages;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Connect to database and add update page permission.
        /// </summary>
        /// <param name="lstPPI">List of PagePermissionInfo class.</param>
        /// <param name="tran">Object of SqlTransaction class.</param>
        /// <param name="pageID">pageID</param>
        /// <param name="portalID">portalID</param>
        /// <param name="addedBy">User name.</param>
        /// <param name="isAdmin">true for admin pages.</param>
        public void AddUpdatePagePermission(List<PagePermissionInfo> lstPPI, SqlTransaction tran, int pageID, int portalID, string addedBy, bool isAdmin)
        {
            try
            {

                List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("@PageID",
                                                                                                           pageID),
                                                                          new KeyValuePair<string, object>("@PortalID",
                                                                                                           portalID),
                                                                          new KeyValuePair<string, object>("@IsAdmin",
                                                                                                           isAdmin)
                                                                      };
                SQLHandler sql = new SQLHandler();
                sql.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[sp_PagePermissionDeleteByPageID]",
                                    ParaMeterColl);

                foreach (PagePermissionInfo objPagePI in lstPPI)
                {
                    if (objPagePI == null) continue;
                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PageID", pageID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@RoleID", objPagePI.RoleID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PermissionID",
                                                                                        objPagePI.PermissionID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AllowAccess",
                                                                                        objPagePI.AllowAccess),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserName", objPagePI.Username),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@IsActive", objPagePI.IsActive),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PortalID", portalID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedBy", addedBy),
                                                                                    new KeyValuePair<string, object>("@IsAdmin",
                                                                                                       isAdmin)
                                                                                };
                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[sp_AddPagePermission]",
                                         ParaMeterCollection);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Connect to database and add updated selected menu.
        /// </summary>
        /// <param name="SelectedMenu">Selected menu with comma separator.</param>
        /// <param name="tran">Object of SqlTransaction class.</param>
        /// <param name="pageID">pageID</param>
        /// <param name="ParentID">ParentID</param>
        /// <param name="Mode">Mode for add or update.</param>
        /// <param name="Caption">Caption.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UpdateLabel">Update label.</param>
        public void AddUpdateSelectedMenu(string SelectedMenu, SqlTransaction tran, int pageID, int ParentID, string Mode, string Caption, int PortalID, string UpdateLabel)
        {
            try
            {
                string[] menuArr = SelectedMenu.Split(',');
                foreach (string menu in menuArr)
                {

                    List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("@MenuID",
                                                                                                           menu),
                                                                          new KeyValuePair<string, object>("@MenuIDs",
                                                                                                           SelectedMenu),
                                                                                                           new KeyValuePair<string, object>("@Mode",
                                                                                                           Mode),
                                                                          new KeyValuePair<string, object>("@PageID",
                                                                                                           pageID),
                                                                          new KeyValuePair<string, object>("@ParentID",
                                                                                                           ParentID),
                                                                        new KeyValuePair<string, object>("@caption",
                                                                                                           Caption),
                                                                        new KeyValuePair<string, object>("@UpdateLabel",
                                                                                                           UpdateLabel)
                                                                        
                                                                      };
                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_PageManagerAddPageToMenu]",
                                        ParaMeterColl);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Connect to database and update menu pages.
        /// </summary>
        /// <param name="MenuIDs">Menu's ID with comma separator.</param>
        /// <param name="tran">Object of SqlTransaction class.</param>
        /// <param name="pageID">pageID</param>
        public void MenuPageUpdate(string MenuIDs, SqlTransaction tran, int pageID)
        {
            try
            {
                string[] menuArr = MenuIDs.Split(',');

                List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("@MenuIDs",
                                                                                                           MenuIDs),
                                                                          new KeyValuePair<string, object>("@PageID",
                                                                                                           pageID)
                                                                      };
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_PageManagerMenuPageUpdate]",
                                    ParaMeterColl);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Connect to database abd obtain page details based on page ID.
        /// </summary>
        /// <param name="pageID">pageID</param>
        /// <returns>Object of PageEntity class.</returns>
        public PageEntity GetPageDetails(int pageID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PageID", pageID)
                                                                            };
                SQLHandler sqlH = new SQLHandler();
                PageEntity objPE = sqlH.ExecuteAsObject<PageEntity>("[dbo].[sp_PagesGetByPageID]", ParaMeterCollection);
                return objPE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connect to database and obtain page permission details.
        /// </summary>
        /// <param name="obj">Object of PageEntity class.</param>
        /// <returns>List of PagePermissionInfo class.</returns>
        public List<PagePermissionInfo> GetPagePermissionDetails(PageEntity obj)
        {
            SqlDataReader reader = null;
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {
                                                                            new KeyValuePair<string, object>("@PageID",
                                                                                                             obj.PageID),
                                                                            new KeyValuePair<string, object>(
                                                                                "@portalID", obj.PortalID)
                                                                        };
                SQLHandler sqlH = new SQLHandler();
                List<PagePermissionInfo> lst = new List<PagePermissionInfo>();
                reader = sqlH.ExecuteAsDataReader("sp_GetPagePermissionByPageID", ParaMeterCollection);

                while (reader.Read())
                {
                    PagePermissionInfo objPP = new PagePermissionInfo
                    {
                        PageID = int.Parse(reader["PageID"].ToString()),
                        PermissionID =
                            int.Parse(reader["PermissionID"].ToString()),
                        RoleID =
                            reader["RoleID"] == DBNull.Value
                                ? ""
                                : reader["RoleID"].ToString(),
                        Username =
                            reader["Username"] == DBNull.Value
                                ? ""
                                : reader["Username"].ToString(),
                        IsActive =
                            bool.Parse(reader["IsActive"].ToString()),
                        AllowAccess =
                            bool.Parse(reader["AllowAccess"].ToString()),
                        AddedBy = reader["AddedBy"].ToString()
                    };
                    lst.Add(objPP);
                }
                return lst;
            }
            catch (Exception e)
            {
                throw e;
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
        /// Connect to database and Obtain child pages.
        /// </summary>
        /// <param name="parentID">Page parent ID.</param>
        /// <param name="isActive">true for active.</param>
        /// <param name="isVisiable">true for visible.</param>
        /// <param name="isRequiredPage">true for required page.</param>
        /// <param name="userName">User name.</param>
        /// <param name="portalID">PortalID</param>
        /// <returns>List of PageEntity class.</returns>
        public List<PageEntity> GetChildPage(int parentID, bool? isActive, bool? isVisiable, bool? isRequiredPage, string userName, int portalID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {
                                                                            new KeyValuePair<string, object>("@ParentID",
                                                                            parentID),
                                                                             new KeyValuePair<string, object>("@IsActive",
                                                                            isActive),
                                                                             new KeyValuePair<string, object>("@IsVisible",
                                                                            isVisiable),
                                                                             new KeyValuePair<string, object>("@IsRequiredPage",
                                                                            isRequiredPage),
                                                                             new KeyValuePair<string, object>("@UserName",
                                                                            userName),
                                                                             new KeyValuePair<string, object>("@PortalID",
                                                                            portalID)
                                                                          
                                                                        };
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<PageEntity>("[dbo].[sp_PageGetByParentID]", ParaMeterCollection);
        }
        /// <summary>
        /// Connect to database and obtain page modules.
        /// </summary>
        /// <param name="pageID">PageID</param>
        /// <param name="portalID">PortalID</param>
        /// <returns>List of PageModuleInfo class.</returns>
        public List<PageModuleInfo> GetPageModules(int pageID, int portalID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {                                                                          
                                                                             new KeyValuePair<string, object>("@PageID",
                                                                            pageID),
                                                                             new KeyValuePair<string, object>("@PortalID",
                                                                            portalID)                                                                          
                                                                        };
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<PageModuleInfo>("[dbo].[usp_GetPageModulesByPageID]", ParaMeterCollection);




        }
        /// <summary>
        /// Connect to database and delete page modules.
        /// </summary>
        /// <param name="moduleID">ModuleID</param>
        /// <param name="deletedBY">User name.</param>
        /// <param name="portalID">PortalID</param>
        public void DeletePageModule(int moduleID, string deletedBY, int portalID)
        {
            List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("@ModuleID",
                                                                                                           moduleID),
                                                                             new KeyValuePair<string, object>("@DeletedBy",
                                                                            deletedBY),
                                                                             new KeyValuePair<string, object>("@PortalID",
                                                                            portalID)                                                                        
                                                                      };
            SQLHandler sql = new SQLHandler();
            sql.ExecuteNonQuery("[dbo].[sp_ModulesDelete]", ParaMeterColl);

        }
        /// <summary>
        /// Connect to database and delete child pages.
        /// </summary>
        /// <param name="pageID">PageID</param>
        /// <param name="deletedBY">User name.</param>
        /// <param name="portalID">PortalID.</param>
        public void DeleteChildPage(int pageID, string deletedBY, int portalID)
        {
            List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("@PageID",
                                                                                                           pageID),
                                                                             new KeyValuePair<string, object>("@DeletedBy",
                                                                            deletedBY),
                                                                             new KeyValuePair<string, object>("@PortalID",
                                                                            portalID)                                                                        
                                                                      };
            SQLHandler sql = new SQLHandler();
            sql.ExecuteNonQuery("[dbo].[sp_PagesDelete]", ParaMeterColl);

        }
        /// <summary>
        /// Connenct to database and update page as context menu.
        /// </summary>
        /// <param name="pageID">PageID</param>
        /// <param name="isVisiable">true for visible.</param>
        /// <param name="isPublished">true for publis.</param>
        /// <param name="portalID">PortalID</param>
        /// <param name="userName">User name.</param>
        /// <param name="updateFor">Update for visible or active.</param>
        public void UpdatePageAsContextMenu(int pageID, bool? isVisiable, bool? isPublished, int portalID, string userName, string updateFor)
        {
            SQLHandler sqlH = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PageID", pageID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IsVisible", isVisiable),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@IsPublish", isPublished),
                                                                                new KeyValuePair<string, object>( 
                                                                                    "@PortalID", portalID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AddedBy", userName),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@updateFor", updateFor)                                                                                 
                                                                            };

                sqlH.ExecuteNonQuery("[dbo].[sp_UpdatePageAsContextMenu]", ParaMeterCollection);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connect to database and sort front end menu.
        /// </summary>
        /// <param name="pageID">pageID</param>
        /// <param name="parentID">parentID</param>
        /// <param name="pageName">Page name.</param>
        /// <param name="BeforeID">BeforeID</param>
        /// <param name="AfterID">AfterID</param>
        /// <param name="portalID">PortalID</param>
        /// <param name="userName">User name.</param>
        public void SortFrontEndMenu(int pageID, int parentID, string pageName, int BeforeID, int AfterID, int portalID, string userName)
        {
            SQLHandler sqlH = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PageID", pageID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@ParentID", parentID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PageName", pageName),                                                                                                                                                      
                                                                                new KeyValuePair<string, object>(
                                                                                    "@BeforeID", BeforeID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AfterID", AfterID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PortalID", portalID),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@AddedBy", userName)
                                                                            };

                sqlH.ExecuteNonQuery("[dbo].[usp_SortPages]", ParaMeterCollection);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connect to database and sort admin pages.
        /// </summary>
        /// <param name="lstPages">List of PageEntity class.</param>
        public void SortAdminPages(List<PageEntity> lstPages)
        {
            SQLHandler sqlH = new SQLHandler();

            try
            {
                foreach (PageEntity obj in lstPages)
                {
                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                            {
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PageID", obj.PageID),                                                                                                                                                                                                                                   
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PageOrder", obj.PageOrder),
                                                                                new KeyValuePair<string, object>(
                                                                                    "@PortalID", obj.PortalID)
                                                                            };
                    sqlH.ExecuteNonQuery("[dbo].[usp_SortAdminPages]", ParaMeterCollection);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connect to database and obtain portal pages.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="prefix">Page prefix.</param>
        /// <param name="IsActive">true for active.</param>
        /// <param name="IsDeleted">true for deleated.</param>
        /// <param name="IsVisible">true for visible.</param>
        /// <param name="IsRequiredPage">true for required page.</param>
        /// <returns>List of PageEntity info class.</returns>
        public List<PageEntity> GetPortalPages(int PortalID, string UserName, string prefix, bool IsActive, bool IsDeleted, object IsVisible, object IsRequiredPage)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {
                                                                            new KeyValuePair<string, object>("@prefix",
                                                                            prefix),
                                                                             new KeyValuePair<string, object>("@IsActive",
                                                                            IsActive),
                                                                             new KeyValuePair<string, object>("@IsDeleted",
                                                                            IsDeleted),
                                                                             new KeyValuePair<string, object>("@PortalID",
                                                                            PortalID),
                                                                             new KeyValuePair<string, object>("@UserName",
                                                                            UserName),
                                                                             new KeyValuePair<string, object>("@IsVisible",
                                                                            IsVisible),
                                                                            new KeyValuePair<string, object>("@IsRequiredPage",
                                                                            IsRequiredPage)
                                                                          
                                                                        };
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<PageEntity>("[dbo].[sp_PagePortalGetByCustomPrefix]", ParaMeterCollection);

        }
        /// <summary>
        /// Connect to database and obtain active portal page.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="prefix">Page prefix.</param>
        /// <param name="IsActive">true for active.</param>
        /// <param name="IsDeleted">true fro deleted.</param>
        /// <param name="IsVisible">true for visible. </param>
        /// <param name="IsRequiredPage">true for required page.</param>
        /// <returns>List of PageEntity class.</returns>
        public List<PageEntity> GetActivePortalPages(int PortalID, string UserName, string prefix, bool IsActive, bool IsDeleted, object IsVisible, object IsRequiredPage)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                        {
                                                                            new KeyValuePair<string, object>("@prefix",
                                                                            prefix),
                                                                             new KeyValuePair<string, object>("@IsActive",
                                                                            IsActive),
                                                                             new KeyValuePair<string, object>("@IsDeleted",
                                                                            IsDeleted),
                                                                             new KeyValuePair<string, object>("@PortalID",
                                                                            PortalID),
                                                                             new KeyValuePair<string, object>("@UserName",
                                                                            UserName),
                                                                             new KeyValuePair<string, object>("@IsVisible",
                                                                            IsVisible),
                                                                            new KeyValuePair<string, object>("@IsRequiredPage",
                                                                            IsRequiredPage)
                                                                          
                                                                        };
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<PageEntity>("[dbo].[usp_PagePortalGetByCustomPrefix]", ParaMeterCollection);

        }
        /// <summary>
        /// Connect to database and update application setting key value.
        /// </summary>
        /// <param name="PageName">Page name.</param>
        /// <param name="PortalID">PortalID</param>
        public void UpdSettingKeyValue(string PageName, int PortalID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Page", PageName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            SQLHandler sageSQL = new SQLHandler();
            sageSQL.ExecuteNonQuery("dbo.usp_PageMgr_UpdSettingKeyValue", ParaMeterCollection);
        }
        /// <summary>
        /// Connect to database and obtain secure page.
        /// </summary>
        /// <param name="portalID">PortalID</param>
        /// <param name="culture">Culture code.</param>
        /// <returns>List of SecurePageInfo class.</returns>
        public List<SecurePageInfo> GetSecurePage(int portalID, string culture)
        {
            try
            {
                SQLHandler sqLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", culture));
                List<SecurePageInfo> portalRoleCollection = new List<SecurePageInfo>();
                portalRoleCollection = sqLH.ExecuteAsList<SecurePageInfo>("usp_GetAllSecurePages", ParaMeter);
                return portalRoleCollection;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connect to database and add update page role permission.
        /// </summary>
        /// <param name="lstPPI">List of PageRoleSettingsInfo class.</param>
        /// <param name="pageID">pageID</param>
        /// <param name="portalID">portalID</param>
        /// <param name="addedBy">User name.</param>
        /// <param name="isAdmin">true for admin pages.</param>
        public void AddUpdatePageRolePermission(List<PageRoleSettingsInfo> lstPPI, int portalID, string addedBy, bool isAdmin)
        {
            try
            {
                SQLHandler sql = new SQLHandler();
                

                foreach (PageRoleSettingsInfo objPagePI in lstPPI)
                {
                    List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("@PageID",
                                                                                                           objPagePI.PageID),
                                                                          new KeyValuePair<string, object>("@RoleID",
                                                                                                           objPagePI.RoleID),
                                                                          new KeyValuePair<string, object>("@PortalID",
                                                                                                           portalID),
                                                                          new KeyValuePair<string, object>("@IsAdmin",
                                                                                                           isAdmin)
                                                                      };

                    sql.ExecuteNonQuery("[dbo].[sp_PagePermissionDeleteByPageRoleID]",
                                        ParaMeterColl);

                }

                foreach (PageRoleSettingsInfo objPagePI in lstPPI)
                {
                    if (objPagePI == null) continue;
                    if (int.Parse(objPagePI.PermissionID) != 0)
                    {
                        List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                                {
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PageID", objPagePI.PageID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@RoleID", objPagePI.RoleID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PermissionID",
                                                                                        objPagePI.PermissionID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AllowAccess",
                                                                                        objPagePI.AllowAccess),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@UserName", objPagePI.Username),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@IsActive", objPagePI.IsActive),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@PortalID", portalID),
                                                                                    new KeyValuePair<string, object>(
                                                                                        "@AddedBy", addedBy),
                                                                                    new KeyValuePair<string, object>("@IsAdmin",
                                                                                                       isAdmin)
                                                                                };
                        SQLHandler sqlH = new SQLHandler();
                        sqlH.ExecuteNonQuery("[dbo].[sp_AddPagePermission]",
                                             ParaMeterCollection);
                    }
                    //tran1.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Connect to database and obtain page permission
        /// </summary>
        /// <param name="RoleID">RoleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of PageRoleSettingsInfo class.</returns>
        public List<PageRoleSettingsInfo> GetPagePermissionByRoleID(Guid RoleID, int PortalID)
        {
            try
            {
                List<PageRoleSettingsInfo> lstPages = new List<PageRoleSettingsInfo>();
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@RoleID", RoleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sagesql = new SQLHandler();
                lstPages = sagesql.ExecuteAsList<PageRoleSettingsInfo>("[dbo].[sp_GetPagePermissionByRoleID]", ParaMeterCollection);
                return lstPages;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Connect to database and obtain superuser roleID
        /// </summary>
        /// <returns>Superuser RoleID</returns>
        public Guid GetSuperRoleID()
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                return SQLH.ExecuteAsScalar<Guid>("[dbo].[sp_GetSuperRoleID]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
