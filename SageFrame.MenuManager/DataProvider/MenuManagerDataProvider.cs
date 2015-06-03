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
using SageFrame.Pages;
#endregion

namespace SageFrame.MenuManager
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuManagerDataProvider
    {
        /// <summary>
        /// Connect to database and obtain menu list based on user name and PortalID.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetMenuList(string UserName, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsList<MenuManagerInfo>("[dbo].[usp_MenuManagerGetMenu]", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Update menu.
        /// </summary>
        /// <param name="lstMenuPermissions">List of MenuPermissionInfo class.</param>
        /// <param name="MenuID">MenuID</param>
        /// <param name="MenuName">Menu name.</param>
        /// <param name="MenuType">Menu type.</param>
        /// <param name="IsDefault">true if default menu.</param>
        /// <param name="PortalID">PortalID</param>
        public static void UpdateMenu(List<MenuPermissionInfo> lstMenuPermissions, int MenuID, string MenuName, string MenuType, bool IsDefault, int PortalID)
        {
            string sp = "[dbo].[usp_MenuMgrUpdateMenu]";
            SQLHandler sagesql = new SQLHandler();

            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuName", MenuName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuType", MenuType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsDefault", IsDefault));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuID", MenuID));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to database and obtain application menu list based on user name , UserModuleID and PortalID.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetSageMenuList(string UserName, int UserModuleID, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsList<MenuManagerInfo>("[dbo].[usp_MenuManagerGetSageMenu]", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and check for default menu.
        /// </summary>
        /// <param name="MenuID">MenuID</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> CheckDefaultMenu(int MenuID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuID", MenuID));
                return SQLH.ExecuteAsList<MenuManagerInfo>("[dbo].[usp_MenuMgrSelectIsDefault]", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to datatbase and obtain all menu items based on MenuID.
        /// </summary>
        /// <param name="MenuID">MenuID</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetAllMenuItem(int MenuID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuID", MenuID));
                return SQLH.ExecuteAsList<MenuManagerInfo>("[dbo].[usp_MenuMgrGetMenuItem]", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and obtain menu items based on UserModuleID and PortalID.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetAllMenuItems(int UserModuleID, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsList<MenuManagerInfo>("[dbo].[usp_MenuMgrLoadMenu]", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and obtain application menu.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetSageMenu(int UserModuleID, int PortalID, string UserName)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
                return SQLH.ExecuteAsList<MenuManagerInfo>("[dbo].[usp_MenuMgrLoadSageMenuItems]", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and obtain application localized menu.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="CultureName">Culture name.</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetSageMenu_Localized(int UserModuleID, int PortalID, string UserName, string CultureName)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", CultureName));
                return SQLH.ExecuteAsList<MenuManagerInfo>("[dbo].[usp_MenuMgrLoadSageMenuItems_Localized]", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and add new menu.
        /// </summary>
        /// <param name="lstMenuPermissions">List of menu permission.</param>
        /// <param name="MenuName">Menu name.</param>
        /// <param name="MenuType">Menu type.</param>
        /// <param name="IsDefault">true for default menu.</param>
        /// <param name="PortalID">PortalID</param>
        public static void AddNewMenu(List<MenuPermissionInfo> lstMenuPermissions, string MenuName, string MenuType, bool IsDefault, int PortalID)
        {
            string sp = "[dbo].[usp_MenuMgrAddNewMenu]";
            SQLHandler sagesql = new SQLHandler();
            int MenuID = 0;
            SqlTransaction tran = (SqlTransaction)sagesql.GetTransaction();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();

                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuName", MenuName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuType", MenuType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsDefault", IsDefault));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                MenuID = sagesql.ExecuteNonQuery(sp, ParamCollInput, "@MenuID");

                foreach (MenuPermissionInfo menu in lstMenuPermissions)
                {
                    List<KeyValuePair<string, object>> ParamColl = new List<KeyValuePair<string, object>>();
                    ParamColl.Add(new KeyValuePair<string, object>("@MenuID", MenuID));
                    ParamColl.Add(new KeyValuePair<string, object>("@PermissionID", menu.PermissionID));
                    ParamColl.Add(new KeyValuePair<string, object>("@AllowAccess", menu.AllowAccess));
                    ParamColl.Add(new KeyValuePair<string, object>("@RoleId", menu.RoleID == null ? Guid.Empty : new Guid(menu.RoleID)));
                    ParamColl.Add(new KeyValuePair<string, object>("@UserName", menu.Username));
                    ParamColl.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                    sagesql.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_MenuMgrAddMenuPermission]", ParamColl);
                }

                tran.Commit();


            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to database and add sub text.
        /// </summary>
        /// <param name="PageID">PageID</param>
        /// <param name="SubText">Sub text contain.</param>
        /// <param name="IsActive"> true for active.</param>
        /// <param name="IsVisible">true for visibility.</param>
        public static void AddSubText(int PageID, string SubText, bool IsActive, bool IsVisible)
        {
            string sp = "[dbo].[usp_MenuMgrAddSubText]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PageID", PageID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SubText", SubText));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsVisible", IsVisible));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to database and delete existing menu based on MenuID.
        /// </summary>
        /// <param name="MenuID">MenuID</param>
        public static void DeleteMenu(int MenuID)
        {
            string sp = "[dbo].[usp_MenuMgrDeleteMenu]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuID", MenuID));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to database and obtain pages beside admin pages.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of PageEntity class. </returns>
        public static List<PageEntity> GetNormalPage(int PortalID, string UserName, string CultureCode)
        {
            List<PageEntity> lstPages = new List<PageEntity>();
            bool isAdmin = false;
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsAdmin", isAdmin));
            try
            {
                SQLHandler sagesql = new SQLHandler();
                lstPages = sagesql.ExecuteAsList<PageEntity>("[dbo].[usp_GetPages]", ParaMeterCollection);

                IEnumerable<PageEntity> lstParent = new List<PageEntity>();
                List<PageEntity> pageList = new List<PageEntity>();

                lstParent = isAdmin ? from pg in lstPages where pg.Level == 1 select pg : from pg in lstPages where pg.Level == 0 select pg;
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
        /// Obtain child pages.
        /// </summary>
        /// <param name="pageList">List of PageEntity class.</param>
        /// <param name="parent">Object of PageEntity class. </param>
        /// <param name="lstPages">List of PageEntity class</param>
        public static void GetChildPages(ref List<PageEntity> pageList, PageEntity parent, List<PageEntity> lstPages)
        {
            foreach (PageEntity obj in lstPages)
            {
                if (obj.ParentID == parent.PageID)
                {
                    obj.PageNameWithoughtPrefix = obj.PageName;
                    obj.Prefix = GetPrefix(obj.Level);
                    obj.PageName = obj.Prefix + obj.PageName;
                    pageList.Add(obj);
                    GetChildPages(ref pageList, obj, lstPages);
                }
            }
        }
        /// <summary>
        /// Obtain prefix based on level.
        /// </summary>
        /// <param name="Level">Menu item level.</param>
        /// <returns>Menu items with prefix.</returns>
        public static string GetPrefix(int Level)
        {
            string prefix = "";
            for (int i = 0; i < Level; i++)
            {
                prefix += "----";
            }
            return prefix;
        }
        /// <summary>
        /// Connect to database and obtain admin pages.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetAdminPage(int PortalID, string UserName, string CultureCode)
        {
            List<MenuManagerInfo> lstPages = new List<MenuManagerInfo>();
            string StoredProcedureName = "[dbo].[usp_SageMenuGetAdminView]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@prefix", "---"));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsDeleted", 0));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));
            SqlDataReader SQLReader = null;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
                while (SQLReader.Read())
                {
                    MenuManagerInfo objMenu = new MenuManagerInfo();
                    objMenu.PageID = int.Parse(SQLReader["PageID"].ToString());
                    objMenu.PageOrder = int.Parse(SQLReader["PageOrder"].ToString());
                    objMenu.PageName = SQLReader["PageName"].ToString();
                    objMenu.ParentID = int.Parse(SQLReader["ParentID"].ToString());
                    objMenu.Level = int.Parse(SQLReader["Level"].ToString());
                    objMenu.TabPath = SQLReader["TabPath"].ToString();
                    objMenu.SEOName = SQLReader["SEOName"].ToString();
                    objMenu.IsVisible = bool.Parse(SQLReader["IsVisible"].ToString());
                    objMenu.ShowInMenu = bool.Parse(SQLReader["ShowInMenu"].ToString());
                    objMenu.LevelPageName = SQLReader["LevelPageName"].ToString();

                    lstPages.Add(objMenu);
                }
                return lstPages;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }
        }

        /// <summary>
        /// Connect to database and add menu item.
        /// </summary>
        /// <param name="MenuItems">Object of MenuManagerInfo.</param>
        public static void AddMenuItem(MenuManagerInfo MenuItems)
        {
            string sp = "[dbo].[usp_MenuMgrAddMenuItem]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuID", MenuItems.MenuID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuItemID", MenuItems.MenuItemID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LinkType", MenuItems.LinkType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PageID", MenuItems.PageID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Title", MenuItems.Title));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LinkURL", MenuItems.LinkURL));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ImageIcon", MenuItems.ImageIcon));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Caption", MenuItems.Caption));
                ParamCollInput.Add(new KeyValuePair<string, object>("@HtmlContent", MenuItems.HtmlContent));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentID", MenuItems.ParentID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuLevel", MenuItems.MenuLevel));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuOrder", MenuItems.MenuOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Mode", MenuItems.Mode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PreservePageOrder", MenuItems.PreservePageOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MainParent", MenuItems.MainParent));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", MenuItems.IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsVisible", MenuItems.IsVisible));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and add external link.
        /// </summary>
        /// <param name="MenuItems">Object of MenuManagerInfo.</param>
        public static void AddExternalLink(MenuManagerInfo MenuItems)
        {
            string sp = "[dbo].[usp_MenuMgrAddExternalLink]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuID", MenuItems.MenuID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuItemID", MenuItems.MenuItemID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LinkType", MenuItems.LinkType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Title", MenuItems.Title));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LinkURL", MenuItems.LinkURL));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ImageIcon", MenuItems.ImageIcon));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Caption", MenuItems.Caption));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentID", MenuItems.ParentID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuLevel", MenuItems.MenuLevel));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsVisible", MenuItems.IsVisible));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", MenuItems.IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuOrder", MenuItems.MenuOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Mode", MenuItems.Mode));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        ///Connect to database and add html content.
        /// </summary>
        /// <param name="MenuItems">Object of MenuManagerInfo.</param>
        public static void AddHtmlContent(MenuManagerInfo MenuItems)
        {
            string sp = "[dbo].[usp_MenuMgrAddHtmlContent]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuID", MenuItems.MenuID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuItemID", MenuItems.MenuItemID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@LinkType", MenuItems.LinkType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Title", MenuItems.Title));
                ParamCollInput.Add(new KeyValuePair<string, object>("@HtmlContent", MenuItems.HtmlContent));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ImageIcon", MenuItems.ImageIcon));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Caption", MenuItems.Caption));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentID", MenuItems.ParentID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuLevel", MenuItems.MenuLevel));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsVisible", MenuItems.IsVisible));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Mode", MenuItems.Mode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", MenuItems.IsActive));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and sort menu.
        /// </summary>
        /// <param name="MenuItemID">MenuItemID</param>
        /// <param name="ParentID">Menu item parent ID.</param>
        /// <param name="BeforeID">Previous MenuItemID.</param>
        /// <param name="AfterID">Next MenuItemID.</param>
        /// <param name="PortalID">PortalID</param>
        public static void SortMenu(int MenuItemID, int ParentID, int BeforeID, int AfterID, int PortalID)
        {
            string sp = "[dbo].[usp_MenuMgrSortMenu]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuItemID", MenuItemID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentID", ParentID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@BeforeID", BeforeID));

                ParamCollInput.Add(new KeyValuePair<string, object>("@AfterID", AfterID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and obtain menu item details.
        /// </summary>
        /// <param name="MenuItemID">MenuItemID</param>
        /// <returns>Object of MenuManagerInfo.</returns>
        public static MenuManagerInfo GetMenuItemDetails(int MenuItemID)
        {
            List<MenuManagerInfo> lstPages = new List<MenuManagerInfo>();
            string StoredProcedureName = "[dbo].[usp_MenuMgrGetMenuItemDetails]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@MenuItemID", MenuItemID));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                return (sagesql.ExecuteAsObject<MenuManagerInfo>(StoredProcedureName, ParaMeterCollection));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Connect to database and add menu settings.
        /// </summary>
        /// <param name="objInfo">List of MenuManagerInfo class.</param>
        public static void AddSetting(List<MenuManagerInfo> objInfo)
        {
            foreach (MenuManagerInfo obj in objInfo)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@MenuID", obj.MenuID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingKey", obj.SettingKey));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingValue", obj.SettingValue));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", obj.PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", obj.AddedBy));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", obj.AddedBy));

                try
                {
                    SQLHandler sagesql = new SQLHandler();
                    sagesql.ExecuteNonQuery("[dbo].[usp_MenuMgrAddUpdSetting]", ParaMeterCollection);

                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
        /// <summary>
        /// Connect to database and obtain menu setting based on PortalID and MenuID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="MenuID">MenuID</param>
        /// <returns>Object of MenuManagerInfo.</returns>
        public static MenuManagerInfo GetMenuSetting(int PortalID, int MenuID)
        {
            MenuManagerInfo objSetting = new MenuManagerInfo();
            string StoredProcedureName = "[dbo].[usp_MenuMgrGetSetting]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@MenuID", MenuID));
            try
            {
                objSetting = sagesql.ExecuteAsObject<MenuManagerInfo>(StoredProcedureName, ParaMeterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }


            return objSetting;
        }
        /// <summary>
        /// Connect to database and obtain menu permission based on PortalID and MenuID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="MenuID">MenuID</param>
        /// <returns>List of MenuPermissionInfo class.</returns>
        public static List<MenuPermissionInfo> GetMenuPermission(int PortalID, int MenuID)
        {

            string StoredProcedureName = "[dbo].[usp_MenuMgrGetPermission]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@MenuID", MenuID));
            try
            {
                return sagesql.ExecuteAsList<MenuPermissionInfo>(StoredProcedureName, ParaMeterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// Connect to database and add menu permissions.
        /// </summary>
        /// <param name="lstMenuPermissions">List of MenuPermissionInfo class.</param>
        /// <param name="MenuID">MenuID</param>
        /// <param name="PortalID">PortalID</param>
        public static void AddMenuPermission(List<MenuPermissionInfo> lstMenuPermissions, int MenuID, int PortalID)
        {

            SQLHandler sagesql = new SQLHandler();
            SqlTransaction tran = (SqlTransaction)sagesql.GetTransaction();

            try
            {
                string sp = "[dbo].[usp_MenuMgrMenuPermissionDelete]";

                List<KeyValuePair<string, object>> ParaMeterColl = new List<KeyValuePair<string, object>>
                                                                      {
                                                                          new KeyValuePair<string, object>("@MenuID",
                                                                                                           MenuID),
                                                                          new KeyValuePair<string, object>("@PortalID",
                                                                                                           PortalID)
                                                                      };

                sagesql.ExecuteNonQuery(tran, CommandType.StoredProcedure, sp,
                                    ParaMeterColl);


                foreach (MenuPermissionInfo menu in lstMenuPermissions)
                {
                    List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                    ParamCollInput.Add(new KeyValuePair<string, object>("@MenuID", MenuID));
                    ParamCollInput.Add(new KeyValuePair<string, object>("@PermissionID", menu.PermissionID));
                    ParamCollInput.Add(new KeyValuePair<string, object>("@AllowAccess", menu.AllowAccess));
                    ParamCollInput.Add(new KeyValuePair<string, object>("@RoleId", menu.RoleID == null ? Guid.Empty : new Guid(menu.RoleID)));
                    ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", menu.Username));
                    ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                    sagesql.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_MenuMgrAddMenuPermission]", ParamCollInput);
                }

                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Connect to database and delete menu link based on MenuItemID.
        /// </summary>
        /// <param name="MenuItemID">MenuItemID</param>
        public static void DeleteLink(int MenuItemID)
        {
            string sp = "[dbo].[usp_MenuMgrDeleteLink]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MenuItemID", MenuItemID));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to database and update application selected menu.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="SettingKey">Application setting key.</param>
        /// <param name="SettingValue">Application setting value.</param>
        public static void UpdateSageMenuSelected(int UserModuleID, int PortalID, string SettingKey, string SettingValue)
        {
            string sp = "[dbo].[usp_SageMenuUpdateSelectedMenu]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SettingKey", SettingKey));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SettingValue", SettingValue));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connect to database and obtain pages for sitemap.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of SitemapInfo class.</returns>
        public static List<SitemapInfo> GetSiteMapPages(string UserName, string CultureCode)
        {
            List<SitemapInfo> lstPages = new List<SitemapInfo>();
            try
            {
                SQLHandler sagesql = new SQLHandler();
                lstPages = sagesql.ExecuteAsList<SitemapInfo>("[dbo].[usp_SEOGetPages]");

                return lstPages;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }

}
