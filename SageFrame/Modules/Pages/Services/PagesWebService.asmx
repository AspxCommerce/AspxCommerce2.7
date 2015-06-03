<%@ WebService Language="C#" Class="PagesWebService" %>
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SageFrame.Security.Entities;
using SageFrame.Security;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using SageFrame.Pages;
using SageFrame.MenuManager;
using System.IO;
using SageFrame.Templating;
using SageFrame.Common;
using SageFrame.ModuleManager.Controller;
using SageFrame.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class PagesWebService : AuthenticateService
{

    public PagesWebService()
    {
    }
    [WebMethod]
    public List<RoleInfo> GetPortalRoles(int portalID, string userName, int userModuleID, string secureToken)
    {
        List<RoleInfo> objRoleInfos = new List<RoleInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            RoleController _role = new RoleController();
            objRoleInfos = _role.GetPortalRoles(portalID, 1, userName);
        }
        return objRoleInfos;
    }

    [WebMethod]
    public List<MenuManagerInfo> GetAllMenu(string userName, int portalID, int userModuleID, string secureToken)
    {
        List<MenuManagerInfo> minfo = new List<MenuManagerInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            minfo = MenuManagerDataController.GetMenuList(userName, portalID);
        }
        return (minfo);
    }

    [WebMethod]
    public List<UserInfo> SearchUsers(string SearchText, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<UserInfo> lstUsers = new List<UserInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            MembershipController m = new MembershipController();
            lstUsers = m.SearchUsers("", SearchText, portalID, userName).UserList;
        }
        return lstUsers;
    }

    [WebMethod]
    public int AddUpdatePages(PageEntity objPageInfo, string Culture, int portalID, string userName, int userModuleID, string secureToken)
    {
        int addUpdate = 0;
        if (objPageInfo.PageID == 0)
        {
            addUpdate = 2;// Added
        }
        else {
            addUpdate = 3; //Updated
        }
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            PageController objPage = new PageController();
            objPageInfo.IconFile = objPageInfo.IconFile == string.Empty || objPageInfo.IconFile == null ? string.Empty : objPageInfo.IconFile;
            objPage.AddUpdatePages(objPageInfo);
            if (objPageInfo.MenuList != "0")
            {
                ClearMenuCache(Culture, objPageInfo.PortalID);
            }
        }
        return addUpdate;
    }

    public void ClearMenuCache(string CultureCode, int PortalID)
    {
        HttpRuntime.Cache.Remove(CultureCode + ".FrontMenu" + PortalID.ToString());
        HttpRuntime.Cache.Remove(CultureCode + ".SideMenu" + PortalID.ToString());
        HttpRuntime.Cache.Remove(CultureCode + ".FooterMenu" + PortalID.ToString());
    }

    [WebMethod]
    public List<PageEntity> GetPages(bool isAdmin, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<PageEntity> lstPages = new List<PageEntity>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            PageController objPageController = new PageController();
            lstPages = objPageController.GetPages(portalID, isAdmin);
            foreach (PageEntity obj in lstPages)
            {
                obj.ChildCount = lstPages.Count(
                    delegate(PageEntity objPage)
                    {
                        return (objPage.ParentID == obj.PageID);
                    }
                 );
            }
        }
        return lstPages;
    }

    [WebMethod]
    public PageEntity GetPageDetails(int pageID, int portalID, string userName, int userModuleID, string secureToken)
    {
        PageEntity pageObj = new PageEntity();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            PageController objPage = new PageController();
            pageObj = objPage.GetPageDetails(pageID);
            pageObj.IconFile = System.IO.File.Exists(Server.MapPath(string.Format("~/PageImages/{0}", pageObj.IconFile))) ? pageObj.IconFile : string.Empty;
        }
        return pageObj;
    }

    [WebMethod]
    public void DeleteIcon(string IconPath, int portalID, string userName, int userModuleID, string secureToken)
    {
        try
        {
            if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
            {
                string filepath = Utils.GetAbsolutePath(string.Format("PageImages/{0}", IconPath));
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
    public List<PageEntity> GetChildPages(int parentID, bool? isActive, bool? isVisiable, bool? isRequiredPage, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<PageEntity> objPageEntity = new List<PageEntity>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            PageController objPage = new PageController();
            objPageEntity = objPage.GetChildPage(parentID, isActive, isVisiable, isRequiredPage, userName, portalID);
        }
        return objPageEntity;
    }

    [WebMethod]
    public List<PageModuleInfo> GetPageModules(int pageID, int portalID, string userName, int userModuleID, string secureToken)
    {
        try
        {
            List<PageModuleInfo> objPageModules = new List<PageModuleInfo>();
            if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
            {
                PageController objPage = new PageController();
                objPageModules = objPage.GetPageModules(pageID, portalID);
            }
            return objPageModules;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void DeletePageModule(int moduleID, string deletedBY, int portalID, string userName, int userModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            PageController objPage = new PageController();
            objPage.DeletePageModule(moduleID, deletedBY, portalID);
        }
    }

    [WebMethod]
    public List<PageEntity> GetPortalPages(bool IsAdmin, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<PageEntity> lstMenu = new List<PageEntity>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            PageController objPageController = new PageController();
            lstMenu = objPageController.GetMenuFront(portalID, IsAdmin);
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
        return lstMenu;
    }

    [WebMethod]
    public void DeleteChildPages(int pageID, string deletedBY, int portalID, string userName, int userModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            PageController obj = new PageController();
            obj.DeleteChildPage(pageID, deletedBY, portalID);
        }
    }

    [WebMethod]
    public void UpdatePageAsContextMenu(int pageID, bool? isVisiable, bool? showInMenu, string updateFor, int portalID, string userName, int userModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            PageController obj = new PageController();
            obj.UpdatePageAsContextMenu(pageID, isVisiable, showInMenu, portalID, userName, updateFor);
        }
    }
    [WebMethod]
    public void SortFrontEndMenu(int pageID, int parentID, string pageName, int BeforeID, int AfterID, int portalID, string userName, int userModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            PageController obj = new PageController();
            obj.SortFrontEndMenu(pageID, parentID, pageName, BeforeID, AfterID, portalID, userName);
        }
    }
    [WebMethod]
    public void SortAdminPages(List<PageEntity> lstPages, int portalID, string userName, int userModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            PageController obj = new PageController();
            obj.SortAdminPages(lstPages);
        }
    }
    [WebMethod]
    public void UpdSettingKeyValue(string ActiveTemplateName, string PageName, string OldPageName, int portalID, string userName, int userModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            UpdPagePreset(ActiveTemplateName, PageName, OldPageName);
            PageController obj = new PageController();
            obj.UpdSettingKeyValue(PageName, portalID);
            SageFrame.Shared.SettingProvider sep = new SageFrame.Shared.SettingProvider();
            Hashtable hst = new Hashtable();
            hst = (Hashtable)HttpRuntime.Cache[CacheKeys.SageSetting];
            if (HttpRuntime.Cache[CacheKeys.SageSetting] != null)
            {
                string portalname = GetDefaultPortalName(portalID);
                hst = (Hashtable)HttpRuntime.Cache[CacheKeys.SageSetting];
                hst[portalname + ".PortalDefaultPage"] = PageName;
            }
        }
    }
    public string GetDefaultPortalName(int portalID)
    {
        Hashtable hstPortals = (Hashtable)HttpRuntime.Cache["Portals"];
        string portalname = "";
        foreach (string key in hstPortals.Keys)
        {
            if (Int32.Parse(hstPortals[key].ToString()) == portalID)// 1 is default PortalID 
            {
                portalname = key;
            }
        }
        return portalname;
    }

    public void UpdPagePreset(string ActiveTemplateName, string PageName, string OldPageName)
    {
        string presetPath = Decide.IsTemplateDefault(ActiveTemplateName.Trim()) ? Utils.GetPresetPath_DefaultTemplate(ActiveTemplateName) : Utils.GetPresetPath(ActiveTemplateName);
        presetPath += "/" + "pagepreset.xml";
        PresetInfo objPreset = new PresetInfo();
        objPreset = PresetHelper.LoadPresetDetails(presetPath);

        List<KeyValue> lstLayout = new List<KeyValue>();
        foreach (KeyValue kvp in objPreset.lstLayouts)
        {
            string[] arrPage = kvp.Value.Split(',');
            if (arrPage.Contains(OldPageName))
            {
                int index = Array.IndexOf(arrPage, OldPageName);
                arrPage[index] = PageName;
            }
            lstLayout.Add(new KeyValue(kvp.Key, string.Join(",", arrPage)));

        }
        objPreset.lstLayouts = lstLayout;
        PresetHelper.WritePreset(presetPath, objPreset);
    }
    //Modules Management
    [WebMethod]
    public List<SageFrame.ModuleManager.UserModuleInfo> GetPageModules(int pageID, bool IsHandheld, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<SageFrame.ModuleManager.UserModuleInfo> lstUserModule = new List<SageFrame.ModuleManager.UserModuleInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            lstUserModule = (ModuleController.GetPageModules(pageID, portalID, IsHandheld));
        }
        return lstUserModule;
    }
    [WebMethod]
    public bool PublishPage(int pageId, bool isPublish, int portalID, string userName, int userModuleID, string secureToken)
    {
        bool isPublished = false;
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            isPublished = ModuleController.PublishPage(pageId, isPublish);
        }
        return isPublished;
    }

}