/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;
using SageFrame.Web.Utilities;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
using System.Data.Common;

namespace SageFrame.Dashboard
{
    /// <summary>
    /// Manipulates data for DashboardController Class
    /// </summary>
    public class DashboardDataProvider
    { 
        /// <summary>
        /// Connects to database and add quick link.
        /// </summary>
        /// <param name="linkObj">QuickLink object.</param>
        /// <returns>Returns true if inserted successfully.</returns>
        public static bool AddQuickLink(QuickLink linkObj)
        {
            string sp = "[dbo].[usp_DashboardQuickLinkAdd]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayName", linkObj.DisplayName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@URL", linkObj.URL));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ImagePath", linkObj.ImagePath));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayOrder",linkObj.DisplayOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PageID", linkObj.PageID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", linkObj.IsActive));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and returns link list.
        /// </summary>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>Link list.</returns>
        public static List<Link> GetAdminPages(int PortalID)
        {
            string sp = "[dbo].[usp_DashboardGetAdminPages]";
            SQLHandler sagesql = new SQLHandler();
            List<Link> lstPages = new List<Link>();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID",PortalID));
            try
            {
                lstPages = sagesql.ExecuteAsList<Link>(sp, ParamCollInput);
                return lstPages;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Connects to database and returns QuickLink list.
        /// </summary>
        /// <param name="UserName">UserName.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>QuickLink list.</returns>
        public static List<QuickLink> GetQuickLinks(string UserName, int PortalID)
        {
            string sp = "[dbo].[usp_DashboardQuickLinkGet]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            List<QuickLink> lstLinks = new List<QuickLink>();
           
            try
            {
                lstLinks = sagesql.ExecuteAsList<QuickLink>(sp,ParamCollInput);
                return lstLinks;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and returns QuickLink list.
        /// </summary>
        /// <param name="UserName">UserName.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>QuickLink list.</returns>
        public static List<QuickLink> GetQuickLinksAll(string UserName, int PortalID)
        {
            string sp = "[dbo].[usp_DashboardQuickLinkGetAll]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            List<QuickLink> lstLinks = new List<QuickLink>();

            try
            {
                lstLinks = sagesql.ExecuteAsList<QuickLink>(sp, ParamCollInput);
                return lstLinks;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and deletequick link for given QuickLinkID.
        /// </summary>
        /// <param name="QuickLinkID">Quick link id.</param>
        public static void DeleteQuickLink(int QuickLinkID)
        {
            string sp = "[dbo].[usp_DashboardQuickLinkDelete]";
            SQLHandler sagesql = new SQLHandler();
           
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@QuickLinkID", QuickLinkID));
            try
            {
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and add side bar.
        /// </summary>
        /// <param name="sbObj">Sidebar object.</param>
        /// <returns>Returns true if inserted successfully.</returns>
        public static bool AddSidebar(Sidebar sbObj)
        {
            string sp = "[dbo].[usp_DashboardSidebarAdd]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayName", sbObj.DisplayName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Depth", sbObj.Depth));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ImagePath", sbObj.ImagePath));
                ParamCollInput.Add(new KeyValuePair<string, object>("@URL", sbObj.URL));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentID",sbObj.ParentID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", sbObj.IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayOrder", sbObj.DisplayOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PageID", sbObj.PageID));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and update side bar.
        /// </summary>
        /// <param name="sbObj">Sidebar object.</param>
        /// <returns>Returns true if updated successfully.</returns>
        public static bool UpdateSidebar(Sidebar sbObj)
        {
            string sp = "[dbo].[usp_DashboardSidebarUpdate]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayName", sbObj.DisplayName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Depth", sbObj.Depth));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ImagePath", sbObj.ImagePath));
                ParamCollInput.Add(new KeyValuePair<string, object>("@URL", sbObj.URL));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentID", sbObj.ParentID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", sbObj.IsActive));               
                ParamCollInput.Add(new KeyValuePair<string, object>("@SidebarItemID", sbObj.SidebarItemID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PageID", sbObj.PageID));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and update quick link.
        /// </summary>
        /// <param name="linkObj">QuickLink object.</param>
        /// <returns>Returns true if updated successfully.</returns>
        public static bool UpdateLink(QuickLink linkObj)
        {
            string sp = "[dbo].[usp_DashboardQuickLinkUpdate]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayName", linkObj.DisplayName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@URL", linkObj.URL));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ImagePath", linkObj.ImagePath));
                ParamCollInput.Add(new KeyValuePair<string, object>("@QuickLinkID", linkObj.QuickLinkID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PageID", linkObj.PageID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", linkObj.IsActive));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and returns sidebar list for given username and portalid.
        /// </summary>
        /// <param name="UserName">UserName.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>Sidebar list.</returns>

        public static List<Sidebar> GetSidebar(string UserName,int PortalID)
        {
            string sp = "[dbo].[usp_DashboardSidebarGet]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

            List<Sidebar> lstLinks = new List<Sidebar>();

            try
            {
                lstLinks = sagesql.ExecuteAsList<Sidebar>(sp,ParamCollInput);
                return lstLinks;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and returns sidebar list for given username and portalid.
        /// </summary>
        /// <param name="UserName">UserName.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>Sidebar list.</returns>
        public static List<Sidebar> GetSidebarAll(string UserName, int PortalID)
        {
            string sp = "[dbo].[usp_DashboardSidebarGetAll]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

            List<Sidebar> lstLinks = new List<Sidebar>();

            try
            {
                lstLinks = sagesql.ExecuteAsList<Sidebar>(sp, ParamCollInput);
                return lstLinks;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and returns sidebar list for given SidebarItemID.
        /// </summary>
        /// <param name="SidebarItemID">Sidebar item id.</param>
        /// <returns>Sidebar list.</returns>
        public static List<Sidebar> GetParentLinks(int SidebarItemID)
        {
            string sp = "[dbo].[usp_DashboardSidebarGetParents]";
            SQLHandler sagesql = new SQLHandler();
            List<Sidebar> lstLinks = new List<Sidebar>();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@SidebarItemID", SidebarItemID));
            try
            {
                lstLinks = sagesql.ExecuteAsList<Sidebar>(sp,ParamCollInput);
                return lstLinks;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and delte sidebar item for given SidebarItemID.
        /// </summary>
        /// <param name="SidebarItemID">Sidebar item id.</param>
        public static void DeleteSidebarItem(int SidebarItemID)
        {
            string sp = "[dbo].[usp_DashboardSidebarDelete]";
            SQLHandler sagesql = new SQLHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@SidebarItemID", SidebarItemID));
            try
            {
                sagesql.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and returns quick link details for given SidebarItemID.
        /// </summary>
        /// <param name="SidebarItemID">SidebarItemID</param>
        /// <returns>Quick link details for given sidebar item id.</returns>
        public static Sidebar GetSidebarDetails(int SidebarItemID)
        {
            string sp = "[dbo].[usp_DashboardSidebarGetDetails]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@SidebarItemID", SidebarItemID));
            try
            {
                return(sagesql.ExecuteAsObject<Sidebar>(sp, ParamCollInput));
                

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and returns quick link details for given QuickLinkItemID.
        /// </summary>
        /// <param name="QuickLinkItemID">Quick link item id.</param>
        /// <returns>Quick link details for given quick link item id.</returns>
        public static QuickLink GetQuickLinkDetails(int QuickLinkItemID)
        {
            string sp = "[dbo].[usp_DashboardQuickLinkGetDetails]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@QuickLinkItemID", QuickLinkItemID));
            try
            {
                return (sagesql.ExecuteAsObject<QuickLink>(sp, ParamCollInput));


            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and reorder side bar link.
        /// </summary>
        /// <param name="lstOrder"> List of object of DashboardKeyValue class.</param>
        public static void ReorderSidebarLink(List<DashboardKeyValue> lstOrder)
        {
            string sp = "[dbo].[usp_DashboardSidebarReorder]";
            SQLHandler sagesql = new SQLHandler();
            foreach (DashboardKeyValue kvp in lstOrder)
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@SidebarItemID", kvp.Key));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayOrder", kvp.Value));
                try
                {
                    sagesql.ExecuteNonQuery(sp, ParamCollInput);

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        /// <summary>
        /// Connects to database and reorder quick links.
        /// </summary>
        /// <param name="lstOrder">List of object of DashboardKeyValue class.</param>
        public static void ReorderQuickLinks(List<DashboardKeyValue> lstOrder)
        {
            string sp = "[dbo].[usp_DashboardQuickLinkReorder]";
            SQLHandler sagesql = new SQLHandler();
            foreach (DashboardKeyValue kvp in lstOrder)
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@QuickLinkID", kvp.Key));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayOrder", kvp.Value));
                try
                {
                    sagesql.ExecuteNonQuery(sp, ParamCollInput);

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        /// <summary>
        /// Connects to database and insert to DashboardSettingsKeyValue.
        /// </summary>
        /// <param name="objSetting">DashbordSettingInfo object.</param>
        public static void AddUpdateDashboardSettings(DashbordSettingInfo objSetting)
        {
            string sp = "[dbo].[usp_DashboardSettingAddUpdate]";
            SQLHandler sagesql = new SQLHandler();
           
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@SettingKey", objSetting.SettingKey));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SettingValue", objSetting.SettingValue));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", objSetting.UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", objSetting.PortalID));
                try
                {
                    sagesql.ExecuteNonQuery(sp, ParamCollInput);

                }
                catch (Exception)
                {

                    throw;
                }
            
        }
        /// <summary>
        /// Connects to database and obtain setting key.
        /// </summary>
        /// <param name="objSetting">DashbordSettingInfo object.</param>
        /// <returns>DashbordSettingInfo object.</returns>
        public static DashbordSettingInfo GetSettingByKey(DashbordSettingInfo objSetting)
        {
            string sp = "[dbo].[usp_DashboardGetSettingByKey]";
            SQLHandler sagesql = new SQLHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@SettingKey", objSetting.SettingKey));           
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", objSetting.UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", objSetting.PortalID));
            try
            {
                return(sagesql.ExecuteAsObject<DashbordSettingInfo>(sp, ParamCollInput));

            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connects to database and obtain online user count.
        /// </summary>
        /// <returns>CountUserInfo object.</returns>
        public static CountUserInfo GetOnlineUserCount()
        {
            try
            {
                SQLHandler Objsql = new SQLHandler();
                return Objsql.ExecuteAsObject<CountUserInfo>("[dbo].[usp_OnlineUserCountGet]");

            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connects to database and get module web info.
        /// </summary>
        /// <returns>ModuleWebInfo list.</returns>
        public static List<ModuleWebInfo> GetModuleWebInfo()
        {
            SQLHandler sageSql = new SQLHandler();
            try
            {

                List<ModuleWebInfo> list = sageSql.ExecuteAsList<ModuleWebInfo>("usp_ModuleWebInfoGetAll");

                //foreach (ModuleWebInfo moduleInfo in list)
                //{
                //    moduleInfo.DownloadUrl = "";
                //}
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Connects to database and get modules.
        /// </summary>
        /// <param name="TotalCount">Total count.</param>
        /// <returns>ModuleInfo list.</returns>
        public static List<ModuleInfo> GetModules(int TotalCount)
        {
            SQLHandler sageSql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModulesCount", TotalCount));

                List<ModuleInfo> list = sageSql.ExecuteAsList<ModuleInfo>("usp_LatestModulesList", ParamCollInput);

                //foreach (ModuleWebInfo moduleInfo in list)
                //{
                //    moduleInfo.DownloadUrl = "";
                //}
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Connects to database and search modules for given keyword.
        /// </summary>
        /// <param name="keyword">Keyword.</param>
        /// <returns>ModuleInfo list.</returns>
        public static List<ModuleInfo> SearchModules(string keyword)
        {
            SQLHandler sageSql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@keyword", keyword));
                List<ModuleInfo> list = sageSql.ExecuteAsList<ModuleInfo>("usp_PackageSearch", ParamCollInput);

                //foreach (ModuleWebInfo moduleInfo in list)
                //{
                //    moduleInfo.DownloadUrl = "";
                //}
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Connects to database and add module web info.
        /// </summary>
        /// <param name="list"> List of object of ModuleWebInfo class.</param>
        public static void AddModuleWebInfo(List<ModuleWebInfo> list)
        {
            string sp = "[dbo].[usp_ModuleWebInfoDelete]";
            SQLHandler sagesql = new SQLHandler();

            DbTransaction transaction = sagesql.GetTransaction();

            try
            {
                sagesql.ExecuteNonQuery(sp);

                sp = "[dbo].[usp_ModuleWebInfoAdd]";
                foreach (ModuleWebInfo Obj in list)
                {
                    List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                    ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", Obj.ModuleID));
                    ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleName", Obj.ModuleName));
                    ParamCollInput.Add(new KeyValuePair<string, object>("@ReleaseDate", Obj.ReleaseDate));
                    ParamCollInput.Add(new KeyValuePair<string, object>("@Description", Obj.Description));
                    ParamCollInput.Add(new KeyValuePair<string, object>("@Version", Obj.Version));
                    ParamCollInput.Add(new KeyValuePair<string, object>("@DownloadUrl", Obj.DownloadUrl));


                    sagesql.ExecuteNonQuery(sp, ParamCollInput);

                }

                sagesql.CommitTransaction(transaction);
            }
            catch (Exception ex)
            {
                sagesql.RollbackTransaction(transaction);
                throw ex;
            }


        }
        /// <summary>
        /// Connects to database and update blog content.
        /// </summary>
        /// <param name="content">Content.</param>

        public static void UpdateBlogContent(string content)
        {
            string sp = "[dbo].[usp_BlogRssContentAdd]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> paramColl = new List<KeyValuePair<string, object>>();
            paramColl.Add(new KeyValuePair<string, object>("@BlogContent", content));

            try
            {
                sagesql.ExecuteNonQuery(sp, paramColl);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Connects to database and obtain blog content.
        /// </summary>
        /// <returns>String.</returns>
        public static string GetBlogContent()
        {
            string sp = "[dbo].[usp_GetBlogRssContent]";
            SQLHandler sagesql = new SQLHandler();
            string content = string.Empty;
            SqlDataReader reader = null;
            try
            {
                reader = sagesql.ExecuteAsDataReader(sp);

                while (reader.Read())
                {
                    content = reader["BlogContent"] as string;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return content;
        }
        /// <summary>
        /// Connects to database and update tutorial content.
        /// </summary>
        /// <param name="content">Content.</param>
        public static void UpdateTutorialContent(string content)
        {
            string sp = "[dbo].[usp_TutorialRssContentUpdate]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> paramColl = new List<KeyValuePair<string, object>>();
            paramColl.Add(new KeyValuePair<string, object>("@TutorialContent", content));

            try
            {
                sagesql.ExecuteNonQuery(sp, paramColl);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Connects to database and obtain tutorial content.
        /// </summary>
        /// <returns>String.</returns>
        public static string GetTutorialContent()
        {
            string sp = "[dbo].[usp_TutorialRssContentGet]";
            SQLHandler sagesql = new SQLHandler();
            string content = "";
            SqlDataReader reader = null;
            try
            {
                reader = sagesql.ExecuteAsDataReader(sp);

                while (reader.Read())
                {
                    content = reader["TutorialContent"] as string;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return content;
        }
        /// <summary>
        /// Connects to database and update news content.
        /// </summary>
        /// <param name="content">Content</param>
        public static void UpdateNewsContent(string content)
        {
            string sp = "[dbo].[usp_NewsRssContentUpdate]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> paramColl = new List<KeyValuePair<string, object>>();
            paramColl.Add(new KeyValuePair<string, object>("@NewsContent", content));

            try
            {
                sagesql.ExecuteNonQuery(sp, paramColl);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Connects to database and obtain news content.
        /// </summary>
        /// <returns>String.</returns>
        public static string GetNewsContent()
        {
            string sp = "[dbo].[usp_NewsRssContentGet]";
            SQLHandler sagesql = new SQLHandler();
            string content = "";
            SqlDataReader reader = null;
            try
            {
                reader = sagesql.ExecuteAsDataReader(sp);

                while (reader.Read())
                {
                    content = reader["NewsContent"] as string;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return content;
        }
        /// <summary>
        /// Connects to database and obtain general snapshot.
        /// </summary>
        /// <param name="PortalID">PortalID.</param>
        /// <param name="IsAdmin">IsAdmin.</param>
        /// <returns>CountUserInfo object.</returns>
        public static CountUserInfo GetGeneralSnapShot(int PortalID, bool IsAdmin)
        {
            SQLHandler sageSql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsAdmin", IsAdmin));
                return sageSql.ExecuteAsObject<CountUserInfo>("[dbo].[usp_DashBoardPageModuleStatistics]", ParamCollInput);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and show pages visible in dashboard.
        /// </summary>
        /// <param name="PageSEOName">Page seo name.</param>
        /// <param name="UserName">UserName.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>DashboardInfo list.</returns>
        public List<DashboardInfo> DashBoardView(string PageSEOName, string UserName, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PageSEOName", PageSEOName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsList<DashboardInfo>("[dbo].[sp_DashBoardView]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
