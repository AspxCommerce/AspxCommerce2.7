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
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
using SageFrame.Pages;
#endregion

namespace SageFrame.SageMenu
{
    /// <summary>
    ///  Manupulates data for MenuDataProvider.
    /// </summary>
    public class MenuDataProvider
    {
        /// <summary>
        ///  Connects to database and obtain front menu.
        /// </summary>
        /// <param name="PortalID"></param>
        /// <param name="UserName"></param>
        /// <param name="CultureCode"></param>
        /// <returns> Return List of MenuInfo class.</returns>
        public static List<MenuInfo> GetMenuFront(int PortalID, string UserName, string CultureCode)
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            string StoredProcedureName = "[dbo].[usp_SageMenuGetClientView]";
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
                    MenuInfo objMenu = new MenuInfo();
                    objMenu.PageID = int.Parse(SQLReader["PageID"].ToString());
                    objMenu.PageOrder = int.Parse(SQLReader["PageOrder"].ToString());
                    objMenu.ParentID = int.Parse(SQLReader["ParentID"].ToString());
                    objMenu.Level = int.Parse(SQLReader["Level"].ToString());
                    objMenu.LevelPageName = SQLReader["LevelPageName"].ToString();
                    objMenu.SEOName = SQLReader["SEOName"].ToString();
                    objMenu.TabPath = SQLReader["TabPath"].ToString();
                    objMenu.IsVisible = bool.Parse(SQLReader["IsVisible"].ToString());
                    objMenu.ShowInMenu = bool.Parse(SQLReader["ShowInMenu"].ToString());
                    objMenu.IconFile = SQLReader["IconFile"].ToString();
                    lstPages.Add(objMenu);
                }
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
            return lstPages;
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
        /// Connect to database and obtain footer menu.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of MenuInfo class.</returns>
        public static List<MenuInfo> GetFooterMenu(int PortalID, string UserName, string CultureCode)
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            string StoredProcedureName = "[dbo].[usp_SageMenuGetFooter]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
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

                    MenuInfo objMenu = new MenuInfo();
                    objMenu.PageID = int.Parse(SQLReader["PageID"].ToString());
                    objMenu.PageOrder = int.Parse(SQLReader["PageOrder"].ToString());
                    objMenu.ParentID = int.Parse(SQLReader["ParentID"].ToString());
                    objMenu.Level = int.Parse(SQLReader["Level"].ToString());
                    objMenu.LevelPageName = SQLReader["LevelPageName"].ToString();
                    objMenu.SEOName = SQLReader["SEOName"].ToString();
                    objMenu.TabPath = SQLReader["TabPath"].ToString();
                    objMenu.IsVisible = bool.Parse(SQLReader["IsVisible"].ToString());
                    objMenu.ShowInMenu = bool.Parse(SQLReader["ShowInMenu"].ToString());
                    objMenu.PageName = SQLReader["PageName"].ToString();
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
        /// Obtain admin menu.
        /// </summary>
        /// <returns>List of MenuInfo class.</returns>
        public static List<MenuInfo> GetAdminMenu()
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            string StoredProcedureName = "[dbo].[usp_sagemenugetadminmenu]";
            SqlDataReader SQLReader = null;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName);
                while (SQLReader.Read())
                {
                    MenuInfo objMenu = new MenuInfo();
                    objMenu.PageID = int.Parse(SQLReader["PageID"].ToString());
                    objMenu.PageOrder = int.Parse(SQLReader["PageOrder"].ToString());
                    objMenu.ParentID = int.Parse(SQLReader["ParentID"].ToString());
                    objMenu.Level = int.Parse(SQLReader["Level"].ToString());
                    objMenu.LevelPageName = SQLReader["LevelPageName"].ToString();
                    objMenu.SEOName = SQLReader["SEOName"].ToString();
                    objMenu.TabPath = SQLReader["TabPath"].ToString();
                    objMenu.IsVisible = bool.Parse(SQLReader["IsVisible"].ToString());
                    objMenu.ShowInMenu = bool.Parse(SQLReader["ShowInMenu"].ToString());
                    objMenu.PageName = SQLReader["PageName"].ToString();
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
        /// Connect to database and obtain side menu.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="menuID">MenuID</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of MenuInfo class.</returns>
        public static List<MenuInfo> GetSideMenu(int PortalID, string UserName, int menuID, string CultureCode)
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            string StoredProcedureName = "[dbo].[usp_SageMenuGetSideMenu]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@MenuID", menuID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));
            SqlDataReader SQLReader = null;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
                while (SQLReader.Read())
                {
                    MenuInfo objMenu = new MenuInfo();
                    objMenu.PageID = int.Parse(SQLReader["PageID"].ToString());
                    objMenu.PageOrder = int.Parse(SQLReader["PageOrder"].ToString());
                    objMenu.PageName = SQLReader["PageName"].ToString();
                    objMenu.ParentID = int.Parse(SQLReader["ParentID"].ToString());
                    objMenu.Level = int.Parse(SQLReader["Level"].ToString());
                    objMenu.TabPath = SQLReader["TabPath"].ToString();
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
        /// Connect to database and update menu settings.
        /// </summary>
        /// <param name="lstSetting">List of SageMenuSettingInfo clsass.</param>
        public static void UpdateSageMenuSettings(List<SageMenuSettingInfo> lstSetting)
        {
            foreach (SageMenuSettingInfo obj in lstSetting)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", obj.UserModuleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingKey", obj.SettingKey));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingValue", obj.SettingValue));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", obj.IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", obj.PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", obj.AddedBy));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", obj.AddedBy));

                try
                {
                    SQLHandler sagesql = new SQLHandler();
                    sagesql.ExecuteNonQuery("[dbo].[usp_SageMenuSettingAddUpdate]", ParaMeterCollection);

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        /// <summary>
        /// Connect to database and obtain menu settings.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <returns>Object of SageMenuSettingInfo.</returns>
        public static SageMenuSettingInfo GetMenuSetting(int PortalID, int UserModuleID)
        {
            SageMenuSettingInfo objSetting = new SageMenuSettingInfo();
            string StoredProcedureName = "[dbo].[usp_SageMenuSettingGetAll]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

            try
            {
                objSetting = sagesql.ExecuteAsObject<SageMenuSettingInfo>(StoredProcedureName, ParaMeterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }
            return objSetting;
        }
        /// <summary>
        ///Connect to database and obtain back end menu.
        /// </summary>
        /// <param name="UserName">User name.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">Culture code</param>
        /// <param name="UserMode">User mode.</param>
        /// <returns>List of MenuInfo class.</returns>
        public static List<MenuInfo> GetBackEndMenu(int ParentNodeID, string UserName, int PortalID, string CultureCode)
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ParentNodeID", ParentNodeID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));
            string StoredProcedureName = "[usp_SageMenuAdminGet]";
            SqlDataReader SQLReader = null;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
                while (SQLReader.Read())
                {
                    MenuInfo objMenu = new MenuInfo();
                    objMenu.PageID = int.Parse(SQLReader["PageID"].ToString());
                    objMenu.PageOrder = int.Parse(SQLReader["PageOrder"].ToString());
                    objMenu.PageName = SQLReader["PageName"].ToString();
                    objMenu.ParentID = int.Parse(SQLReader["ParentID"].ToString());
                    objMenu.Level = int.Parse(SQLReader["Level"].ToString());
                    objMenu.TabPath = SQLReader["TabPath"].ToString();
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
        /// Connect to database and obtain admin pages.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserName">User name.</param>
        /// <param name="CultureCode">Culture code.</param>
        /// <returns>List of MenuInfo class.</returns>
        public static List<MenuInfo> GetAdminPages(int PortalID, string UserName, string CultureCode)
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            string StoredProcedureName = "[dbo].[usp_GetAdminPages]";
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
                    MenuInfo objMenu = new MenuInfo();
                    objMenu.PageID = int.Parse(SQLReader["PageID"].ToString());
                    objMenu.PageOrder = int.Parse(SQLReader["PageOrder"].ToString());
                    objMenu.PageName = SQLReader["PageName"].ToString();
                    objMenu.ParentID = int.Parse(SQLReader["ParentID"].ToString());
                    objMenu.Level = int.Parse(SQLReader["Level"].ToString());
                    objMenu.TabPath = SQLReader["TabPath"].ToString();
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
    }
}
