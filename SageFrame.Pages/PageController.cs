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
    /// Business logic for PageController.
    /// </summary>
    public class PageController
    {
        /// <summary>
        /// Obtain application front menu.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="isAdmin">true for admin.</param>
        /// <returns></returns>
        public List<PageEntity> GetMenuFront(int PortalID, bool isAdmin)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                List<PageEntity> lstPages = objProvider.GetMenuFront(PortalID, isAdmin);
                IEnumerable<PageEntity> lstParent = new List<PageEntity>();
                List<PageEntity> pageList = new List<PageEntity>();
                lstParent = isAdmin ? from pg in lstPages where pg.Level == 1 select pg : from pg in lstPages where pg.Level == 0 select pg;
                foreach (PageEntity parent in lstParent)
                {
                    parent.PageName = parent.PageName.Replace(" ", "-");
                    pageList.Add(parent);
                    GetChildPages(ref pageList, parent, lstPages);
                }
                return pageList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Get child pages.
        /// </summary>
        /// <param name="pageList">List of PageEntity class.</param>
        /// <param name="parent">Object of PageEntity class.</param>
        /// <param name="lstPages">List of PageEntity class.</param>
        public void GetChildPages(ref List<PageEntity> pageList, PageEntity parent, List<PageEntity> lstPages)
        {
            foreach (PageEntity obj in lstPages)
            {
                if (obj.ParentID == parent.PageID)
                {
                    obj.PageNameWithoughtPrefix = obj.PageName;
                    obj.Prefix = GetPrefix(obj.Level);
                    obj.PageName = obj.Prefix + obj.PageName.Replace(" ", "-");
                    pageList.Add(obj);
                    GetChildPages(ref pageList, obj, lstPages);
                }
            }
        }
        /// <summary>
        ///obtain page prefix.
        /// </summary>
        /// <param name="Level">Level</param>
        /// <returns>prefix based on level.</returns>
        public string GetPrefix(int Level)
        {
            string prefix = "";
            for (int i = 0; i < Level; i++)
            {
                prefix += "----";
            }
            return prefix;
        }
        /// <summary>
        /// Add update page.
        /// </summary>
        /// <param name="objPage">Object of PageEntity class.</param>
        public void AddUpdatePages(PageEntity objPage)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.AddUpdatePages(objPage);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtain pages based on PortalID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="isAdmin">true for admin page.</param>
        /// <returns>List of PageEntity class.</returns>
        public List<PageEntity> GetPages(int PortalID, bool isAdmin)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                List<PageEntity> lstPages = objProvider.GetPages(PortalID, isAdmin);
                IEnumerable<PageEntity> lstParent = new List<PageEntity>();
                List<PageEntity> pageList = new List<PageEntity>();
                lstParent = (from pg in lstPages where pg.Level == 0 select pg);
                foreach (PageEntity parent in lstParent)
                {
                    pageList.Add(parent);
                    GetChildPages(ref pageList, parent, lstPages);
                }
                return pageList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Add update page permisiion.
        /// </summary>
        /// <param name="lstPPI">List of PagePermissionInfo class.</param>
        /// <param name="tran">Object of SqlTransaction class.</param>
        /// <param name="pageID">pageID</param>
        /// <param name="portalID">portalID</param>
        /// <param name="addedBy">User nadme.</param>
        /// <param name="isAdmin">true for admin page.</param>
        public void AddUpdatePagePermission(List<PagePermissionInfo> lstPPI, SqlTransaction tran, int pageID, int portalID, string addedBy, bool isAdmin)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.AddUpdatePagePermission(lstPPI, tran, pageID, portalID, addedBy, isAdmin);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Add updated selected menu.
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
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.AddUpdateSelectedMenu(SelectedMenu, tran, pageID, ParentID, Mode, Caption, PortalID, UpdateLabel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Update menu pages.
        /// </summary>
        /// <param name="MenuIDs">Menu's ID with comma separator.</param>
        /// <param name="tran">Object of SqlTransaction class.</param>
        /// <param name="pageID">pageID</param>
        public void MenuPageUpdate(string MenuIDs, SqlTransaction tran, int pageID)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.MenuPageUpdate(MenuIDs, tran, pageID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Obtain page details based on page ID.
        /// </summary>
        /// <param name="pageID">pageID</param>
        /// <returns>Object of PageEntity class.</returns>
        public PageEntity GetPageDetails(int pageID)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                PageEntity objPE = objProvider.GetPageDetails(pageID);
                objPE.PortalID = objPE.PortalID == -1 ? 1 : objPE.PortalID;
                objPE.LstPagePermission = GetPagePermissionDetails(objPE);
                return objPE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtain page permission details.
        /// </summary>
        /// <param name="obj">Object of PageEntity class.</param>
        /// <returns>List of PagePermissionInfo class.</returns>
        public List<PagePermissionInfo> GetPagePermissionDetails(PageEntity obj)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                List<PagePermissionInfo> lst = objProvider.GetPagePermissionDetails(obj);
                return lst;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtain child pages.
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
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                List<PageEntity> lstPageEntry = objProvider.GetChildPage(parentID, isActive, isVisiable, isRequiredPage, userName, portalID);
                return lstPageEntry;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtain page modules.
        /// </summary>
        /// <param name="pageID">PageID</param>
        /// <param name="portalID">PortalID</param>
        /// <returns>List of PageModuleInfo class.</returns>
        public List<PageModuleInfo> GetPageModules(int pageID, int portalID)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                List<PageModuleInfo> objPageModuleLst = objProvider.GetPageModules(pageID, portalID);
                return objPageModuleLst;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Delete page modules.
        /// </summary>
        /// <param name="moduleID">ModuleID</param>
        /// <param name="deletedBY">User name.</param>
        /// <param name="portalID">PortalID</param>
        public void DeletePageModule(int moduleID, string deletedBY, int portalID)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.DeletePageModule(moduleID, deletedBY, portalID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Delete child pages.
        /// </summary>
        /// <param name="pageID">PageID</param>
        /// <param name="deletedBY">User name.</param>
        /// <param name="portalID">PortalID.</param>
        public void DeleteChildPage(int pageID, string deletedBY, int portalID)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.DeleteChildPage(pageID, deletedBY, portalID);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// Update page as context menu.
        /// </summary>
        /// <param name="pageID">PageID</param>
        /// <param name="isVisiable">true for visible.</param>
        /// <param name="isPublished">true for publis.</param>
        /// <param name="portalID">PortalID</param>
        /// <param name="userName">User name.</param>
        /// <param name="updateFor">Update for visible or active.</param>
        public void UpdatePageAsContextMenu(int pageID, bool? isVisiable, bool? isPublished, int portalID, string userName, string updateFor)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.UpdatePageAsContextMenu(pageID, isVisiable, isPublished, portalID, userName, updateFor);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Sort front end menu.
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

            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.SortFrontEndMenu(pageID, parentID, pageName, BeforeID, AfterID, portalID, userName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Sort admin pages.
        /// </summary>
        /// <param name="lstPages">List of PageEntity class.</param>
        public void SortAdminPages(List<PageEntity> lstPages)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.SortAdminPages(lstPages);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
        /// <summary>
        /// Obtain portal pages.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="prefix">Page prefix.</param>
        /// <param name="IsActive">true for active.</param>
        /// <param name="IsDeleted">true for deleated.</param>
        /// <param name="IsVisible">true for visible.</param>
        /// <param name="IsRequiredPage">true for required page.</param>
        /// <returns>List of PageEntity info class.</returns>
        public  List<PageEntity> GetPortalPages(int PortalID, string UserName, string prefix, bool IsActive, bool IsDeleted, object IsVisible, object IsRequiredPage)
        {
           
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                List<PageEntity> objPageEntity = objProvider.GetPortalPages(PortalID, UserName, prefix, IsActive, IsDeleted, IsVisible, IsRequiredPage);
                return objPageEntity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtain active portal page.
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
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                List<PageEntity> objPageEntityLst = objProvider.GetActivePortalPages(PortalID, UserName, prefix, IsActive, IsDeleted, IsVisible, IsRequiredPage);
                return objPageEntityLst;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Update application setting key value.
        /// </summary>
        /// <param name="PageName">Page name.</param>
        /// <param name="PortalID">PortalID</param>
        public void UpdSettingKeyValue(string PageName, int PortalID)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.UpdSettingKeyValue(PageName, PortalID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtain secure page.
        /// </summary>
        /// <param name="portalID">PortalID</param>
        /// <param name="culture">Culture code.</param>
        /// <returns>List of SecurePageInfo class.</returns>
        public List<SecurePageInfo> GetSecurePage(int portalID, string culture)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                List<SecurePageInfo> objSecureLst = objProvider.GetSecurePage(portalID, culture);
                return objSecureLst;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Add update page role permisiion.
        /// </summary>
        /// <param name="pageID">pageID</param>
        /// <param name="portalID">portalID</param>
        /// <param name="addedBy">User name.</param>
        /// <param name="isAdmin">true for admin page.</param>

        public void AddUpdatePageRolePermission(List<PageRoleSettingsInfo> lstPagePermission, int portalID, string addedBy, bool isAdmin)
        {
            try
            {
                PageDataProvider objProvider = new PageDataProvider();
                objProvider.AddUpdatePageRolePermission(lstPagePermission, portalID, addedBy, isAdmin);
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
                PageDataProvider objProvider = new PageDataProvider();
                List<PageRoleSettingsInfo> lstPagePermission = objProvider.GetPagePermissionByRoleID(RoleID, PortalID);
                return lstPagePermission;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Connect to database and obtain superuser roleID
        /// </summary>
        /// <returns>Superuser RoleID</returns>
        public Guid GetSuperRoleID()
        {
            PageDataProvider objProvider = new PageDataProvider();
            return objProvider.GetSuperRoleID();
        }
    }
}
