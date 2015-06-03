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

namespace SageFrame.SageMenu
{
    /// <summary>
    /// Business logic for menu.
    /// </summary>
    public class MenuController
    {
        /// <summary>
        /// Obtain front menu.
        /// </summary>
        /// <param name="PortalID"></param>
        /// <param name="UserName"></param>
        /// <param name="CultureCode"></param>
        /// <returns> Return List of MenuInfo class.</returns>
        public static List<MenuInfo> GetMenuFront(int PortalID, string UserName, string CultureCode)
        {
            try
            {
                return (MenuDataProvider.GetMenuFront(PortalID, UserName, CultureCode));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain footer menu.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of MenuInfo class.</returns>
        public static List<MenuInfo> GetFooterMenu(int PortalID, string UserName, string CultureCode)
        {
            try
            {
                return (MenuDataProvider.GetFooterMenu(PortalID, UserName, CultureCode));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        ///  Obtain side menu.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="menuID">MenuID</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of MenuInfo class.</returns>
        public static List<MenuInfo> GetSideMenu(int PortalID, string UserName, int menuID, string CultureCode)
        {
            try
            {
                return (MenuDataProvider.GetSideMenu(PortalID, UserName, menuID, CultureCode));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update menu settings.
        /// </summary>
        /// <param name="lstSetting">List of SageMenuSettingInfo clsass.</param>
        public static void UpdateSageMenuSettings(List<SageMenuSettingInfo> lstSetting)
        {
            try
            {
                MenuDataProvider.UpdateSageMenuSettings(lstSetting);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain menu settings.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <returns>Object of SageMenuSettingInfo.</returns>
        public static SageMenuSettingInfo GetMenuSetting(int PortalID, int UserModuleID)
        {
            try
            {
                return (MenuDataProvider.GetMenuSetting(PortalID, UserModuleID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain admin menu.
        /// </summary>
        /// <returns>List of MenuInfo class.</returns>
        public static List<MenuInfo> GetAdminMenu()
        {
            try
            {
                return (MenuDataProvider.GetAdminMenu());
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
        /// <returns>List of MenuInfo class.</returns>
        public static List<MenuInfo> GetAdminPages(int PortalID, string UserName, string CultureCode)
        {
            try
            {
                return (MenuDataProvider.GetAdminPages(PortalID, UserName, CultureCode));
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// Obtain back end menu.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">Culture code</param>
        /// <param name="UserMode">User mode.</param>
        /// <returns>List of MenuInfo class.</returns>
        public static List<MenuInfo> GetBackEndMenu(string UserName, int PortalID, string CultureCode, int UserMode)
        {
            try
            {
                List<MenuInfo> AdminMenu = MenuDataProvider.GetBackEndMenu(2, UserName, PortalID, CultureCode);
                if (UserMode == 1)
                {
                    List<MenuInfo> SuperUserMenu = MenuDataProvider.GetBackEndMenu(3, UserName, PortalID, CultureCode);
                    AdminMenu.AddRange(SuperUserMenu);
                }
                return AdminMenu;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
