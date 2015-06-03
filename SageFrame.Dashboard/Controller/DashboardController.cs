/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;
using SageFrame.Web;

namespace SageFrame.Dashboard
{
    /// <summary>
    /// Business logic for Dashboard.
    /// </summary>
    public class DashboardController
    {
        /// <summary>
        /// Add quick links.
        /// </summary>
        /// <param name="linkObj">QuickLink object.</param>
        /// <returns>Returns true if inserted successfully.</returns>
        public static bool AddQuickLink(QuickLink linkObj)
        {
            try
            {
                return (DashboardDataProvider.AddQuickLink(linkObj));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Get admin pages for given PortalID.
        /// </summary>
        /// <param name="PortalID">Portal id.</param>
        /// <returns>Link list.</returns>
        public static List<Link> GetAdminPages(int PortalID)
        {
            try
            {
                return (DashboardDataProvider.GetAdminPages(PortalID));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Get quick links for given PortalID.
        /// </summary>
        /// <param name="UserName">UserName.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>QuickLink list.</returns>
        public static List<QuickLink> GetQuickLinks(string UserName,int PortalID)
        {
            try
            {
                return (DashboardDataProvider.GetQuickLinks(UserName, PortalID));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Get all quick links for given PortalID.
        /// </summary>
        /// <param name="UserName">UserName.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>QuickLink list.</returns>
        public static List<QuickLink> GetQuickLinksAll(string UserName, int PortalID)
        {
            try
            {
                return (DashboardDataProvider.GetQuickLinksAll(UserName, PortalID));
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Delete quick link for given QuickLinkID.
        /// </summary>
        /// <param name="QuickLinkID">Quick link id.</param>
        public static void DeleteQuickLink(int QuickLinkID)
        {
            try
            {
                DashboardDataProvider.DeleteQuickLink(QuickLinkID);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Add side bar.
        /// </summary>
        /// <param name="sbObj">Sidebar object.</param>
        /// <returns>Returns true if inserted successfully.</returns>
        public static bool AddSidebar(Sidebar sbObj)
        {
            try
            {
                return (DashboardDataProvider.AddSidebar(sbObj));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Get side bar for given PortalID.
        /// </summary>
        /// <param name="UserName">UserName.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>Sidebar list.</returns>
        public static List<Sidebar> GetSidebar(string UserName,int PortalID)
        {
            try
            {
                List<Sidebar> lstSidebar = DashboardDataProvider.GetSidebar(UserName, PortalID);
                return lstSidebar;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Get all side bar for given PortalID.
        /// </summary>
        /// <param name="UserName">UserName.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>Sidebar list.</returns>
        public static List<Sidebar> GetSidebarAll(string UserName, int PortalID)
        {
            try
            {
                List<Sidebar> lstSidebar = DashboardDataProvider.GetSidebarAll(UserName, PortalID);
                return lstSidebar;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update quick link.
        /// </summary>
        /// <param name="linkObj">QuickLink objects.</param>
        public static void UpdateLink(QuickLink linkObj)
        {
            try
            {
                DashboardDataProvider.UpdateLink(linkObj);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Get parent links.
        /// </summary>
        /// <param name="SidebarItemID">Sidebar item id.</param>
        /// <returns>Sidebar objects.</returns>
        public static List<Sidebar> GetParentLinks(int SidebarItemID)
        {
            try
            {
                List<Sidebar> lstSidebar = DashboardDataProvider.GetParentLinks(SidebarItemID);
                Sidebar zeroItem = new Sidebar();
                zeroItem.DisplayName = "[None Specified]";
                lstSidebar.Insert(0, zeroItem);
                return lstSidebar;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Delete side bar for given Sidebar item id.
        /// </summary>
        /// <param name="SidebarItemID">Sidebar item id.</param>
        public static void DeleteSidebarItem(int SidebarItemID)
        {
            try
            {
                DashboardDataProvider.DeleteSidebarItem(SidebarItemID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get side bar details for given Sidebar item id.
        /// </summary>
        /// <param name="SidebarItemID">Sidebar item id.</param>
        /// <returns>side bar details for given Sidebar item id.</returns>
        public static Sidebar GetSidebarDetails(int SidebarItemID)
        {
            return (DashboardDataProvider.GetSidebarDetails(SidebarItemID));
        }
        /// <summary>
        /// Obtains quick link details.
        /// </summary>
        /// <param name="SidebarItemID">Sidebar item id.</param>
        /// <returns>Quick link details for given Sidebar item id.</returns>
        public static QuickLink GetQuickLinkDetails(int SidebarItemID)
        {
            return (DashboardDataProvider.GetQuickLinkDetails(SidebarItemID));
        }
        /// <summary>
        /// Reorder side bar link.
        /// </summary>
        /// <param name="lstOrder">List of object of DashboardKeyValue class.</param>
        public static void ReorderSidebarLink(List<DashboardKeyValue> lstOrder)
        {
            try
            {
                DashboardDataProvider.ReorderSidebarLink(lstOrder);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Reorder quick links.
        /// </summary>
        /// <param name="lstOrder">DashboardKeyValue list</param>
        public static void ReorderQuickLinks(List<DashboardKeyValue> lstOrder)
        {
            try
            {
                DashboardDataProvider.ReorderQuickLinks(lstOrder);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update side bar.
        /// </summary>
        /// <param name="sbObj">Sidebar objects</param>
        /// <returns>Returns true if updated successfully</returns>
        public static bool UpdateSidebar(Sidebar sbObj)
        {
            try
            {
                return (DashboardDataProvider.UpdateSidebar(sbObj));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Add dashboard settings.
        /// </summary>
        /// <param name="objSetting">DashbordSettingInfo object</param>
        public static void AddUpdateDashboardSettings(DashbordSettingInfo objSetting)
        {
            try
            {
                DashboardDataProvider.AddUpdateDashboardSettings(objSetting);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtain setting key.
        /// </summary>
        /// <param name="objSetting">DashbordSettingInfo objects</param>
        /// <returns>DashbordSettingInfo object</returns>
        public static DashbordSettingInfo GetSettingByKey(DashbordSettingInfo objSetting)
        {
            try
            {
                return (DashboardDataProvider.GetSettingByKey(objSetting));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtain online user count.
        /// </summary>
        /// <returns>Total online count.</returns>
        public static CountUserInfo GetOnlineUserCount()
        {
            try
            {
                return (DashboardDataProvider.GetOnlineUserCount());
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtain modules.
        /// </summary>
        /// <param name="TotalCount">Total count.</param>
        /// <returns>ModuleInfo list.</returns>
        public static List<ModuleInfo> GetModules(int TotalCount)
        {
            try
            {
                return (DashboardDataProvider.GetModules(TotalCount));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain module web info.
        /// </summary>
        /// <returns>ModuleWebInfo list.</returns>
        public static List<ModuleWebInfo> GetModuleWebInfo()
        {
            try
            {
                return (DashboardDataProvider.GetModuleWebInfo());
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Add module web info.
        /// </summary>
        /// <param name="list"> List of object of ModuleWebInfo class.</param>
        public static void AddModuleWebInfo(List<ModuleWebInfo> list)
        {
            try
            {
                DashboardDataProvider.AddModuleWebInfo(list);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Search modules.
        /// </summary>
        /// <param name="keyword">Keyword.</param>
        /// <returns>ModuleInfo list.</returns>
        public static List<ModuleInfo> SearchModules(string keyword)
        {
            try
            {
                return DashboardDataProvider.SearchModules(keyword);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update blog content.
        /// </summary>
        /// <param name="content">Content.</param>
        public static void UpdateBlogContent(string content)
        {
            try
            {
                DashboardDataProvider.UpdateBlogContent(content);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain blog content.
        /// </summary>
        /// <returns>Content.</returns>
        public static string GetBlogContent()
        {

            try
            {
                return DashboardDataProvider.GetBlogContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update tutorial content.
        /// </summary>
        /// <param name="content">Content.</param>
        public static void UpdateTutorialContent(string content)
        {
            try
            {
                DashboardDataProvider.UpdateTutorialContent(content);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain tutorial content.
        /// </summary>
        /// <returns>Content.</returns>
        public static string GetTutorialContent()
        {
            try
            {
                return DashboardDataProvider.GetTutorialContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Update news content.
        /// </summary>
        /// <param name="content">Content.</param>
        public static void UpdateNewsContent(string content)
        {
            try
            {
                DashboardDataProvider.UpdateNewsContent(content);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Obtain news content.
        /// </summary>
        /// <returns>News content.</returns>
        public static string GetNewsContent()
        {
            try
            {
                return DashboardDataProvider.GetNewsContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Pages visible in dashboard.
        /// </summary>
        /// <param name="PageSEOName">Page seo name.</param>
        /// <param name="UserName">UserName</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>DashboardInfo list</returns>
        public List<DashboardInfo> DashBoardView(string PageSEOName, string UserName, int PortalID)
        {
            try
            {
                DashboardDataProvider objProvider = new DashboardDataProvider();
                List<DashboardInfo> objDashInfoLst = objProvider.DashBoardView(PageSEOName, UserName, PortalID);
                return objDashInfoLst;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
