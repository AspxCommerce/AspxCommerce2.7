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
#endregion

namespace SageFrame.MenuManager
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuManagerDataController
    {
        /// <summary>
        /// Obtain menu list based on user name and PortalID.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetMenuList(string UserName, int PortalID)
        {
            try
            {
                return (MenuManagerDataProvider.GetMenuList(UserName, PortalID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain application menu list based on user name , UserModuleID and PortalID.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetSageMenuList(string UserName, int UserModuleID, int PortalID)
        {
            try
            {
                return (MenuManagerDataProvider.GetSageMenuList(UserName, UserModuleID, PortalID));
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
            try
            {
                MenuManagerDataProvider.UpdateMenu(lstMenuPermissions, MenuID, MenuName, MenuType, IsDefault, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Check for default menu.
        /// </summary>
        /// <param name="MenuID">MenuID</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> CheckDefaultMenu(int MenuID)
        {
            try
            {
                return (MenuManagerDataProvider.CheckDefaultMenu(MenuID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Add new menu.
        /// </summary>
        /// <param name="lstMenuPermissions">List of menu permission.</param>
        /// <param name="MenuName">Menu name.</param>
        /// <param name="MenuType">Menu type.</param>
        /// <param name="IsDefault">true for default menu.</param>
        /// <param name="PortalID">PortalID</param>
        public static void AddNewMenu(List<MenuPermissionInfo> lstMenuPermissions, string MenuName, string MenuType, bool IsDefault, int PortalID)
        {
            try
            {
                MenuManagerDataProvider.AddNewMenu(lstMenuPermissions, MenuName, MenuType, IsDefault, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Add sub text.
        /// </summary>
        /// <param name="PageID">PageID</param>
        /// <param name="SubText">Sub text contain.</param>
        /// <param name="IsActive"> true for active.</param>
        /// <param name="IsVisible">true for visibility.</param>
        public static void AddSubText(int PageID, string SubText, bool IsActive, bool IsVisible)
        {
            try
            {
                MenuManagerDataProvider.AddSubText(PageID, SubText, IsActive, IsVisible);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Delete existing menubased on MenuID .
        /// </summary>
        /// <param name="MenuID">MenuID</param>
        public static void DeleteMenu(int MenuID)
        {
            try
            {
                MenuManagerDataProvider.DeleteMenu(MenuID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain pages beside admin pages.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of PageEntity class. </returns>
        public static List<PageEntity> GetNormalPage(int PortalID, string UserName, string CultureCode)
        {
            try
            {
                return (MenuManagerDataProvider.GetNormalPage(PortalID, UserName, CultureCode));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain admin pages.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetAdminPage(int PortalID, string UserName, string CultureCode)
        {
            try
            {
                return (MenuManagerDataProvider.GetAdminPage(PortalID, UserName, CultureCode));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Add menu item.
        /// </summary>
        /// <param name="MenuItems">Object of MenuManagerInfo.</param>
        public static void AddMenuItem(MenuManagerInfo MenuItems)
        {
            try
            {
                MenuManagerDataProvider.AddMenuItem(MenuItems);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Add external link.
        /// </summary>
        /// <param name="MenuItems">Object of MenuManagerInfo.</param>
        public static void AddExternalLink(MenuManagerInfo MenuItems)
        {
            try
            {
                MenuManagerDataProvider.AddExternalLink(MenuItems);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Add html content.
        /// </summary>
        /// <param name="MenuItems">Object of MenuManagerInfo.</param>
        public static void AddHtmlContent(MenuManagerInfo MenuItems)
        {
            try
            {
                MenuManagerDataProvider.AddHtmlContent(MenuItems);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain all menu items based on MenuID.
        /// </summary>
        /// <param name="MenuID">MenuID</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetAllMenuItem(int MenuID)
        {
            try
            {
                return (MenuManagerDataProvider.GetAllMenuItem(MenuID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Sort menu.
        /// </summary>
        /// <param name="MenuItemID">MenuItemID</param>
        /// <param name="ParentID">Menu item parent ID.</param>
        /// <param name="BeforeID">Previous MenuItemID.</param>
        /// <param name="AfterID">Next MenuItemID.</param>
        /// <param name="PortalID">PortalID</param>
        public static void SortMenu(int MenuItemID, int ParentID, int BeforeID, int AfterID, int PortalID)
        {
            try
            {
                MenuManagerDataProvider.SortMenu(MenuItemID, ParentID, BeforeID, AfterID, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain menu item details.
        /// </summary>
        /// <param name="MenuItemID">MenuItemID</param>
        /// <returns>Object of MenuManagerInfo.</returns>
        public static MenuManagerInfo GetMenuItemDetails(int MenuItemID)
        {
            try
            {
                return (MenuManagerDataProvider.GetMenuItemDetails(MenuItemID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Add menu settings.
        /// </summary>
        /// <param name="objInfo">List of MenuManagerInfo class.</param>
        public static void AddSetting(List<MenuManagerInfo> objInfo)
        {
            try
            {
                MenuManagerDataProvider.AddSetting(objInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain menu setting based on PortalID and MenuID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="MenuID">MenuID</param>
        /// <returns>Object of MenuManagerInfo.</returns>
        public static MenuManagerInfo GetMenuSetting(int PortalID, int MenuID)
        {
            try
            {
                return (MenuManagerDataProvider.GetMenuSetting(PortalID, MenuID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain menu permission based on PortalID and MenuID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="MenuID">MenuID</param>
        /// <returns>List of MenuPermissionInfo class.</returns>
        public static List<MenuPermissionInfo> GetMenuPermission(int PortalID, int MenuID)
        {
            try
            {
                return (MenuManagerDataProvider.GetMenuPermission(PortalID, MenuID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain menu items based on UserModuleID and PortalID.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetAllMenuItems(int UserModuleID, int PortalID)
        {
            try
            {
                return (MenuManagerDataProvider.GetAllMenuItems(UserModuleID, PortalID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain application menu.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <returns>List of MenuManagerInfo class.</returns>
        public static List<MenuManagerInfo> GetSageMenu(int UserModuleID, int PortalID, string UserName)
        {
            try
            {
                return (MenuManagerDataProvider.GetSageMenu(UserModuleID, PortalID, UserName));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain application localized menu.
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
                return MenuManagerDataProvider.GetSageMenu_Localized(UserModuleID, PortalID, UserName, CultureName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Add menu permissions.
        /// </summary>
        /// <param name="lstMenuPermissions">List of MenuPermissionInfo class.</param>
        /// <param name="MenuID">MenuID</param>
        /// <param name="PortalID">PortalID</param>
        public static void AddMenuPermission(List<MenuPermissionInfo> lstMenuPermissions, int MenuID, int PortalID)
        {
            try
            {
                MenuManagerDataProvider.AddMenuPermission(lstMenuPermissions, MenuID, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Delete menu link based on MenuItemID.
        /// </summary>
        /// <param name="MenuItemID">MenuItemID</param>
        public static void DeleteLink(int MenuItemID)
        {
            try
            {
                MenuManagerDataProvider.DeleteLink(MenuItemID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update application selected menu.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="SettingKey">Application setting key.</param>
        /// <param name="SettingValue">Application setting value.</param>
        public static void UpdateSageMenuSelected(int UserModuleID, int PortalID, string SettingKey, string SettingValue)
        {
            try
            {
                MenuManagerDataProvider.UpdateSageMenuSelected(UserModuleID, PortalID, SettingKey, SettingValue);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain pages for sitemap.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of SitemapInfo class.</returns>
        public static List<SitemapInfo> GetSiteMapPages(string UserName, string CultureCode)
        {
            try
            {
                return MenuManagerDataProvider.GetSiteMapPages(UserName, CultureCode);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
