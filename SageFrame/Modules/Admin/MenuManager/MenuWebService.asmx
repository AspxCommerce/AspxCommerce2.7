<%@ WebService Language="C#" Class="MenuWebService" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.MenuManager;
using SageFrame.Security.Entities;
using SageFrame.Security;
using SageFrame.Templating;
using System.IO;
using SageFrame.Pages;
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

    }

    [WebMethod]
    public List<MenuManagerInfo> GetAllMenu(string UserName, int PortalID, int userModuleId, string secureToken)
    {
        List<MenuManagerInfo> minfo = null;
        if (IsPostAuthenticatedView(PortalID, userModuleId, UserName, secureToken))
        {
            minfo = MenuManagerDataController.GetMenuList(UserName, PortalID);

        }
        return (minfo);

    }
    [WebMethod]
    public List<MenuManagerInfo> GetSageMenu(string UserName, int UserModuleID, int PortalID, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, UserModuleID, UserName, secureToken))
        {
            return (MenuManagerDataController.GetSageMenuList(UserName, UserModuleID, PortalID));
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<MenuManagerInfo> CheckDefaultMenu(int MenuID, string UserName, int UserModuleID, int PortalID, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, UserModuleID, UserName, secureToken))
        {
            List<MenuManagerInfo> minfo = MenuManagerDataController.CheckDefaultMenu(MenuID);
            return (minfo);
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public void AddNewMenu(List<MenuPermissionInfo> lstMenuPermissions, string MenuName, string MenuType, bool IsDefault, int PortalID, string CultureCode, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, userModuleId, UserName, secureToken))
        {
            MenuManagerDataController.AddNewMenu(lstMenuPermissions, MenuName, MenuType, IsDefault, PortalID);
            ClearCache(CultureCode, PortalID);
        }

    }
    [WebMethod]
    public void UpdateMenu(List<MenuPermissionInfo> lstMenuPermissions, int MenuID, string MenuName, string MenuType, bool IsDefault, int PortalID, string CultureCode, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, userModuleId, UserName, secureToken))
        {
            MenuManagerDataController.UpdateMenu(lstMenuPermissions, MenuID, MenuName, MenuType, IsDefault, PortalID);
            ClearCache(CultureCode, PortalID);
        }
    }
    [WebMethod]
    public void AddSubText(int PageID, string SubText, bool IsActive, bool IsVisible, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, userModuleId, UserName, secureToken))
        {
            MenuManagerDataController.AddSubText(PageID, SubText, IsActive, IsVisible);

        }
    }

    [WebMethod]
    public void DeleteMenu(int MenuID, string CultureCode, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, userModuleId, UserName, secureToken))
        {
            MenuManagerDataController.DeleteMenu(MenuID);
            ClearCache(CultureCode, PortalID);
        }

    }

    [WebMethod]
    public List<PageEntity> GetNormalPage(int PortalID, string UserName, string CultureCode, string secureToken, int userModuleID)
    {
        List<PageEntity> lstMenu = new List<PageEntity>();
        if (IsPostAuthenticatedView(PortalID, userModuleID, UserName, secureToken))
        {
            PageController objPageController = new PageController();
            lstMenu = objPageController.GetMenuFront(PortalID, false);
            //if (IsPostAuthenticated(PortalID, userModuleId, UserName))
            //{
            foreach (PageEntity obj in lstMenu)
            {
                obj.ChildCount = lstMenu.Count(
                    delegate(PageEntity objMenu)
                    {
                        return (objMenu.ParentID == obj.PageID);
                    }
                    );
            }
        }
        //}
        return lstMenu;
    }

    [WebMethod]
    public List<MenuManagerInfo> GetAdminPage(int PortalID, string UserName, string CultureCode)
    {
        //if (IsPostAuthenticated(PortalID, userModuleId, UserName))
        //{
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
        //}
        //else
        //{
        //    return null;
        //}
    }

    [WebMethod]
    public void AddMenuItem(MenuManagerInfo objInfo, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticatedView(objInfo.PortalID, userModuleId, UserName, secureToken))
        {
            objInfo.ImageIcon = objInfo.ImageIcon == "" || objInfo.ImageIcon == null ? "" : objInfo.ImageIcon;
            MenuManagerDataController.AddMenuItem(objInfo);
            ClearCache(objInfo.CultureCode, objInfo.PortalID);
        }
    }


    [WebMethod]
    public List<MenuManagerInfo> GetAllMenuItem(int MenuID, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, userModuleId, UserName, secureToken))
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
    public int GetMenuOrder(string MenuLevel)
    {
        int MenuOrder = 1;
        return MenuOrder;
    }


    [WebMethod]
    public void AddExternalLink(MenuManagerInfo objInfo, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, userModuleId, UserName, secureToken))
        {
            objInfo.ImageIcon = objInfo.ImageIcon == "" || objInfo.ImageIcon == null ? "" : objInfo.ImageIcon;
            MenuManagerDataController.AddExternalLink(objInfo);
            ClearCache(objInfo.CultureCode, objInfo.PortalID);

        }
    }

    [WebMethod]
    public void AddHtmlContent(MenuManagerInfo objInfo, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, userModuleId, UserName, secureToken))
        {
            objInfo.ImageIcon = objInfo.ImageIcon == "" || objInfo.ImageIcon == null ? "" : objInfo.ImageIcon;
            MenuManagerDataController.AddHtmlContent(objInfo);
        }
    }

    [WebMethod]
    public void SortMenu(int MenuItemID, int ParentID, int BeforeID, int AfterID, int PortalID, int userModuleId, string UserName, string secureToken, string cultureCode)
    {
        try
        {
            if (IsPostAuthenticatedView(PortalID, userModuleId, UserName, secureToken))
            {
                MenuManagerDataController.SortMenu(MenuItemID, ParentID, BeforeID, AfterID, PortalID);
                ClearCache(cultureCode, PortalID);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    [WebMethod]
    public MenuManagerInfo GetDetails(int MenuItemID, int PortalID, int userModuleId, string UserName, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, userModuleId, UserName, secureToken))
        {
            return (MenuManagerDataController.GetMenuItemDetails(MenuItemID));
        }
        return null;
    }


    [WebMethod]
    public void AddSetting(List<MenuManagerInfo> lstSettings, string UserName, int UserModuleID, int PortalID, string secureToken, string CultureCode)
    {
        if (IsPostAuthenticatedView(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.AddSetting(lstSettings);
            ClearCache(CultureCode, PortalID);
        }
    }

    [WebMethod]
    public MenuManagerInfo GetMenuSettings(int PortalID, int MenuID, string UserName, int UserModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, UserModuleID, UserName, secureToken))
        {
            return (MenuManagerDataController.GetMenuSetting(PortalID, MenuID));
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<MenuPermissionInfo> GetMenuPermission(int PortalID, int MenuID, string UserName, int UserModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, UserModuleID, UserName, secureToken))
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
        if (IsPostAuthenticatedView(PortalID, UserModuleID, UserName, secureToken))
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
    public void AddMenuPermission(List<MenuPermissionInfo> lstMenuPermissions, int MenuID, int PortalID, string UserName, int UserModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.AddMenuPermission(lstMenuPermissions, MenuID, PortalID);
        }

    }

    [WebMethod]
    public List<UserInfo> SearchUsers(string SearchText, int PortalID, string UserName, int UserModuleID, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            MembershipController msController = new MembershipController();
            List<UserInfo> lstUsers = msController.SearchUsers("", SearchText, PortalID, UserName).UserList;
            return lstUsers;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public void DeleteIcon(string IconPath, int PortalID, string UserName, int UserModuleID, string secureToken)
    {
        try
        {
            if (IsPostAuthenticatedView(PortalID, UserModuleID, UserName, secureToken))
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
    public void DeleteLink(int MenuItemID, int PortalID, string UserName, int UserModuleID, string secureToken, string CultureCode)
    {
        if (IsPostAuthenticatedView(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.DeleteLink(MenuItemID);
            ClearCache(CultureCode, PortalID);
        }
    }

    [WebMethod]
    public void SaveSageMenu(int UserModuleID, int PortalID, string SettingKey, string SettingValue, string UserName, string secureToken, string CultureCode)
    {
        if (IsPostAuthenticatedView(PortalID, UserModuleID, UserName, secureToken))
        {
            MenuManagerDataController.UpdateSageMenuSelected(UserModuleID, PortalID, SettingKey, SettingValue);
            ClearCache(CultureCode, PortalID);
        }
    }

    public void ClearCache(string CultureCode, int PortalID)
    {
        SageFrame.Common.CacheHelper.Clear(CultureCode + ".FrontMenu" + PortalID.ToString());
        SageFrame.Common.CacheHelper.Clear(CultureCode + ".SideMenu" + PortalID.ToString());
        SageFrame.Common.CacheHelper.Clear(CultureCode + ".FooterMenu" + PortalID.ToString());
    }
}