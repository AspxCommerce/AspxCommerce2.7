<%@ WebService Language="C#" Class="MenuWebService" %>
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SageFrame.Framework;
using SageFrame.SageMenu;
using System.Collections.Generic;
using SageFrame.MenuManager;
using SageFrame.Common;
using SageFrame.Security.Entities;
using SageFrame.Security;
using SageFrame.Templating;
using System.IO;
using SageFrame.Pages;
using SageFrame.Web;
using SageFrame.Services;



/// <summary>
/// Summary description for MenuWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class MenuWebService : AuthenticateService
{

    public MenuWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<MenuInfo> GetBackEndMenu(int PortalID, string UserName, string CultureCode, int UserMode, string PortalSEOName, int UserModuleID, string secureToken)
    {
        try
        {

            if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
            {
                List<MenuInfo> lstMenu = MenuController.GetBackEndMenu(UserName, PortalID, CultureCode, UserMode);
                string PortalName = PortalSEOName.ToLower() != "default" ? "/portal/" + PortalSEOName : "";
                lstMenu.Add(new MenuInfo(2, 0, "Admin", 0, PortalName + "/Admin/Admin" + SageFrameSettingKeys.PageExtension));
                if (UserMode == 1)
                {
                    lstMenu.Add(new MenuInfo(3, 0, "SuperUser", 0, PortalName + "/Super-User/Super-User" + SageFrameSettingKeys.PageExtension));
                }
                foreach (MenuInfo obj in lstMenu)
                {
                    obj.ChildCount = lstMenu.Count(
                        delegate(MenuInfo objMenu)
                        {
                            return (objMenu.ParentID == obj.PageID);
                        }
                        );
                }
                return lstMenu;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public List<MenuManagerInfo> GetMenuFront(int PortalID, string UserName, string CultureCode, int UserModuleID)
    {
        try
        {
            List<MenuManagerInfo> lstMenuItems = new List<MenuManagerInfo>();
            if (SageFrameSettingKeys.FrontMenu && UserName == "anonymoususer")
            {
                if (!SageFrame.Common.CacheHelper.Get(CultureCode + ".FrontMenu" + PortalID.ToString(), out lstMenuItems))
                {
                    lstMenuItems = MenuManagerDataController.GetSageMenu(UserModuleID, PortalID, UserName);
                    SageFrame.Common.CacheHelper.Add(lstMenuItems, CultureCode + ".FrontMenu" + PortalID.ToString());
                }
            }
            else
            {
                lstMenuItems = MenuManagerDataController.GetSageMenu(UserModuleID, PortalID, UserName);
            }
            IEnumerable<MenuManagerInfo> lstParent = new List<MenuManagerInfo>();
            List<MenuManagerInfo> lstHierarchy = new List<MenuManagerInfo>();
            lstParent = from pg in lstMenuItems
                        where pg.MenuLevel == "0"
                        select pg;

            foreach (MenuManagerInfo parent in lstParent)
            {

                lstHierarchy.Add(parent);
                GetChildPages(ref lstHierarchy, parent, lstMenuItems);

            }

            return (lstHierarchy);

        }
        catch (Exception)
        {

            throw;
        }

    }



    [WebMethod]
    public List<MenuManagerInfo> GetCustomSideMenu(int PortalID, string UserName, string CultureCode, int UserModuleID)
    {
        List<MenuManagerInfo> lstMenuItems = new List<MenuManagerInfo>();
        if (SageFrameSettingKeys.SideMenu && UserName == "anonymoususer")
        {
            if (!SageFrame.Common.CacheHelper.Get(CultureCode + ".SideMenu" + PortalID.ToString(), out lstMenuItems))
            {
                lstMenuItems = MenuManagerDataController.GetSageMenu(UserModuleID, PortalID, UserName);
                SageFrame.Common.CacheHelper.Add(lstMenuItems, CultureCode + ".SideMenu" + PortalID.ToString());
            }
        }
        else
        {
            lstMenuItems = MenuManagerDataController.GetSageMenu(UserModuleID, PortalID, UserName);
        }
        IEnumerable<MenuManagerInfo> lstParent = new List<MenuManagerInfo>();
        List<MenuManagerInfo> lstHierarchy = new List<MenuManagerInfo>();
        lstParent = from pg in lstMenuItems
                    where pg.MenuLevel == "0"
                    select pg;
        foreach (MenuManagerInfo parent in lstParent)
        {
            lstHierarchy.Add(parent);
            GetChildPages(ref lstHierarchy, parent, lstMenuItems);
        }

        return (lstHierarchy);
    }


    [WebMethod]
    public List<MenuManagerInfo> GetFooterMenu(int PortalID, string UserName, string CultureCode, int UserModuleID)
    {

        List<MenuManagerInfo> lstMenuItems = new List<MenuManagerInfo>();
        if (SageFrameSettingKeys.FooterMenu && UserName == "anonymoususer")
        {
            if (!SageFrame.Common.CacheHelper.Get(CultureCode + ".FooterMenu" + PortalID.ToString(), out lstMenuItems))
            {
                lstMenuItems = MenuManagerDataController.GetSageMenu(UserModuleID, PortalID, UserName);
                SageFrame.Common.CacheHelper.Add(lstMenuItems, CultureCode + ".FooterMenu" + PortalID.ToString());
            }
        }
        else
        {
            lstMenuItems = MenuManagerDataController.GetSageMenu(UserModuleID, PortalID, UserName);
        }

        IEnumerable<MenuManagerInfo> lstParent = new List<MenuManagerInfo>();
        List<MenuManagerInfo> lstHierarchy = new List<MenuManagerInfo>();
        lstParent = from pg in lstMenuItems
                    where pg.MenuLevel == "0"
                    select pg;
        foreach (MenuManagerInfo parent in lstParent)
        {
            lstHierarchy.Add(parent);
            GetChildPages(ref lstHierarchy, parent, lstMenuItems);
        }

        return (lstHierarchy);
    }

    [WebMethod]
    public List<MenuManagerInfo> GetFooterMenu_Localized(int PortalID, string UserName, string CultureCode, int UserModuleID)
    {

        List<MenuManagerInfo> lstMenuItems = new List<MenuManagerInfo>();
        lstMenuItems = MenuManagerDataController.GetSageMenu_Localized(UserModuleID, PortalID, UserName, CultureCode);
        IEnumerable<MenuManagerInfo> lstParent = new List<MenuManagerInfo>();
        List<MenuManagerInfo> lstHierarchy = new List<MenuManagerInfo>();
        lstParent = from pg in lstMenuItems
                    where pg.MenuLevel == "0"
                    select pg;
        foreach (MenuManagerInfo parent in lstParent)
        {
            lstHierarchy.Add(parent);
            GetChildPages(ref lstHierarchy, parent, lstMenuItems);
        }

        return (lstHierarchy);
    }


    public void GetChildPages(ref List<MenuManagerInfo> lstHierarchy, MenuManagerInfo parent, List<MenuManagerInfo> lstPages)
    {
        foreach (MenuManagerInfo obj in lstPages)
        {
            if (obj.ParentID == parent.MenuItemID)
            {
                lstHierarchy.Add(obj);
                GetChildPages(ref lstHierarchy, obj, lstPages);
            }
        }
    }
    [WebMethod]
    public List<MenuInfo> GetPages()
    {
        List<MenuInfo> lstMenu = MenuController.GetMenuFront(1, "superuser", "en-US");
        foreach (MenuInfo obj in lstMenu)
        {
            obj.ChildCount = lstMenu.Count(
                delegate(MenuInfo objMenu)
                {
                    return (objMenu.ParentID == obj.PageID);
                }
                );
        }
        return lstMenu;
    }


    //[WebMethod]
    //public List<MenuInfo> GetSideMenu(int PortalID, string UserName, string PageName, string CultureCode)
    //{
    //    List<MenuInfo> lstMenu = MenuController.GetSideMenu(PortalID, UserName, PageName, CultureCode);
    //    List<MenuInfo> lstNewMenu = new List<MenuInfo>();
    //    foreach (MenuInfo obj in lstMenu)
    //    {
    //        obj.ChildCount = lstMenu.Count(
    //            delegate(MenuInfo objMenu)
    //            {
    //                return (objMenu.ParentID == obj.PageID);
    //            }
    //            );
    //        obj.Title = obj.PageName;
    //        if (obj.Level > 0)
    //        {
    //            lstNewMenu.Add(obj);
    //        }
    //    }
    //    return lstNewMenu;
    //}

    [WebMethod]
    public SageMenuSettingInfo GetMenuSettings1(int PortalID, int UserModuleID)
    {
        return (MenuController.GetMenuSetting(PortalID, UserModuleID));
    }

    [WebMethod]
    public SageMenuSettingInfo GetAllMenuSettings(int PortalID, int UserModuleID)
    {
        return (MenuController.GetMenuSetting(PortalID, UserModuleID));
    }

    [WebMethod]
    public void SaveMenuSetting(List<SageMenuSettingInfo> lstMenuSetting)
    {
        MenuController.UpdateSageMenuSettings(lstMenuSetting);
    }

    [WebMethod]
    public List<MenuInfo> GetAdminPages(int PortalID, string UserName, string CultureCode)
    {
        List<MenuInfo> lstMenu = MenuController.GetAdminPages(PortalID, UserName, CultureCode);
        foreach (MenuInfo obj in lstMenu)
        {
            obj.ChildCount = lstMenu.Count(
                delegate(MenuInfo objMenu)
                {
                    return (objMenu.ParentID == obj.PageID);
                }
                );
        }
        return lstMenu;
    }


    [WebMethod]
    public List<MenuManagerInfo> GetAllMenu(string UserName, int PortalID, int UserModuleID, string secureToken)
    {

        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<MenuManagerInfo> minfo = MenuManagerDataController.GetMenuList(UserName, PortalID);
            return (minfo);
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<MenuManagerInfo> GetSageMenu(string UserName, int UserModuleID, int PortalID, string secureToken)
    {

        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            return (MenuManagerDataController.GetSageMenuList(UserName, UserModuleID, PortalID));
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<MenuManagerInfo> CheckDefaultMenu(int MenuID)
    {
        List<MenuManagerInfo> minfo = MenuManagerDataController.CheckDefaultMenu(MenuID);
        return (minfo);
    }

    [WebMethod]
    public void AddNewMenu(List<MenuPermissionInfo> lstMenuPermissions, string MenuName, string MenuType, bool IsDefault, int PortalID, string CultureCode, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.AddNewMenu(lstMenuPermissions, MenuName, MenuType, IsDefault, PortalID);
            ClearCache(CultureCode, PortalID);
        }

    }
    [WebMethod]
    public void UpdateMenu(List<MenuPermissionInfo> lstMenuPermissions, int MenuID, string MenuName, string MenuType, bool IsDefault, int PortalID, string CultureCode, int UserModuleID, string UserName, string secureToken)
    {

        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.UpdateMenu(lstMenuPermissions, MenuID, MenuName, MenuType, IsDefault, PortalID);
            ClearCache(CultureCode, PortalID);
        }
    }
    [WebMethod]
    public void AddSubText(int PageID, string SubText, bool IsActive, bool IsVisible)
    {

        MenuManagerDataController.AddSubText(PageID, SubText, IsActive, IsVisible);
    }
    [WebMethod]
    public void DeleteMenu(int MenuID, string CultureCode, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.DeleteMenu(MenuID);
            ClearCache(CultureCode, PortalID);
        }
    }
    [WebMethod]
    public List<PageEntity> GetNormalPage(int PortalID, string UserName, string CultureCode, int UserModuleID, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            PageController objPageController = new PageController();
            List<PageEntity> lstMenu = objPageController.GetMenuFront(PortalID, false);
            foreach (PageEntity obj in lstMenu)
            {
                obj.ChildCount = lstMenu.Count(
                    delegate(PageEntity objMenu)
                    {
                        return (objMenu.ParentID == obj.PageID);
                    }
                    );
            }
            return lstMenu;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public List<MenuManagerInfo> GetAdminPage(int PortalID, string UserName, string CultureCode, int UserModuleID, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<MenuManagerInfo> lstMenu = MenuManagerDataController.GetAdminPage(PortalID, UserName, CultureCode);
            foreach (MenuManagerInfo obj in lstMenu)
            {
                obj.ChildCount = lstMenu.Count(
                    delegate(MenuManagerInfo objMenu)
                    {
                        return (objMenu.ParentID == obj.PageID);
                    }
                    );
            }
            return lstMenu;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public void AddMenuItem(MenuManagerInfo objInfo, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(objInfo.PortalID, UserModuleID, UserName, secureToken))
        {
            objInfo.ImageIcon = objInfo.ImageIcon == "" || objInfo.ImageIcon == null ? "" : objInfo.ImageIcon;
            MenuManagerDataController.AddMenuItem(objInfo);
            ClearCache(objInfo.CultureCode, objInfo.PortalID);
        }
    }

    [WebMethod]
    public List<MenuManagerInfo> GetAllMenuItem(int MenuID, int PortalID, int UserModuleID, string UserName, string secureToken)
    {

        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<MenuManagerInfo> lstMenuItems = MenuManagerDataController.GetAllMenuItem(MenuID);

            IEnumerable<MenuManagerInfo> lstParent = new List<MenuManagerInfo>();
            List<MenuManagerInfo> lstHierarchy = new List<MenuManagerInfo>();
            lstParent = from pg in lstMenuItems
                        where pg.MenuLevel == "0"
                        select pg;
            foreach (MenuManagerInfo parent in lstParent)
            {
                lstHierarchy.Add(parent);
                GetChildPages(ref lstHierarchy, parent, lstMenuItems);
            }

            return (lstHierarchy);
        }
        else
        {
            return null;
        }
    }




    [WebMethod]
    public int GetMenuOrder(string MenuLevel)
    {
        int MenuOrder = 1;

        return MenuOrder;
    }


    [WebMethod]
    public void AddExternalLink(MenuManagerInfo objInfo, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            objInfo.ImageIcon = objInfo.ImageIcon == "" || objInfo.ImageIcon == null ? "" : objInfo.ImageIcon;
            MenuManagerDataController.AddExternalLink(objInfo);
        }
    }

    [WebMethod]
    public void AddHtmlContent(MenuManagerInfo objInfo)
    {
        objInfo.ImageIcon = objInfo.ImageIcon == "" || objInfo.ImageIcon == null ? "" : objInfo.ImageIcon;

        MenuManagerDataController.AddHtmlContent(objInfo);
    }

    [WebMethod]
    public void SortMenu(int MenuItemID, int ParentID, int BeforeID, int AfterID, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        try
        {
            if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
            {
                MenuManagerDataController.SortMenu(MenuItemID, ParentID, BeforeID, AfterID, PortalID);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    [WebMethod]
    public MenuManagerInfo GetDetails(int MenuItemID)
    {
        return (MenuManagerDataController.GetMenuItemDetails(MenuItemID));
    }


    [WebMethod]
    public void AddSetting(List<MenuManagerInfo> lstSettings, int PortalID, int UserModuleID, string UserName, string secureToken)
    {

        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.AddSetting(lstSettings);
        }
    }

    [WebMethod]
    public MenuManagerInfo GetMenuSettings(int PortalID, int MenuID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            return (MenuManagerDataController.GetMenuSetting(PortalID, MenuID));
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<MenuPermissionInfo> GetMenuPermission(int PortalID, int MenuID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            return (MenuManagerDataController.GetMenuPermission(PortalID, MenuID));
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<RoleInfo> GetPortalRoles(int PortalID, string UserName, int UserModuleID, string secureToken)
    {

        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            RoleController _role = new RoleController();
            return (_role.GetPortalRoles(PortalID, 1, UserName));
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public void AddMenuPermission(List<MenuPermissionInfo> lstMenuPermissions, int MenuID, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.AddMenuPermission(lstMenuPermissions, MenuID, PortalID);
        }
    }

    [WebMethod]
    public List<UserInfo> SearchUsers(string SearchText, int PortalID, string UserName)
    {
        MembershipController msController = new MembershipController();
        List<UserInfo> lstUsers = msController.SearchUsers("", SearchText, PortalID, UserName).UserList;
        return lstUsers;
    }

    [WebMethod]
    public void DeleteIcon(string IconPath, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        try
        {
            if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
            {
                string filepath = Utils.GetAbsolutePath(string.Format("Modules/Admin/MenuManager/Icons/{0}", IconPath));
                if (File.Exists(filepath))
                {
                    File.SetAttributes(filepath, System.IO.FileAttributes.Normal);
                    File.Delete(filepath);
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    [WebMethod]
    public void DeleteLink(int MenuItemID, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.DeleteLink(MenuItemID);
        }
    }

    [WebMethod]
    public void SaveSageMenu(int UserModuleID, int PortalID, string SettingKey, string SettingValue, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.UpdateSageMenuSelected(UserModuleID, PortalID, SettingKey, SettingValue);
        }
    }

    public void ClearCache(string CultureCode, int PortalID)
    {
        SageFrame.Common.CacheHelper.Clear(CultureCode + ".FrontMenu" + PortalID.ToString());
        SageFrame.Common.CacheHelper.Clear(CultureCode + ".SideMenu" + PortalID.ToString());
        SageFrame.Common.CacheHelper.Clear(CultureCode + ".FooterMenu" + PortalID.ToString());
        //HttpRuntime.Cache.Remove(CultureCode + ".FrontMenu" + PortalID.ToString());
        //HttpRuntime.Cache.Remove(CultureCode + ".SideMenu" + PortalID.ToString());
        //HttpRuntime.Cache.Remove(CultureCode + ".FooterMenu" + PortalID.ToString());
    }
}