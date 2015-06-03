#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SageFrame.Web;
using SageFrame.SageMenu;
using System.IO;
using SageFrame.Common.Shared;
using System.Collections.Generic;
using SageFrame.MenuManager;
using System.Text;
using System.Text.RegularExpressions;
#endregion


public partial class Modules_SageMenu_SageMenu : BaseAdministrationUserControl
{
    public int UserModuleID, PortalID;
    public string ContainerClientID = string.Empty, appPath = string.Empty, DefaultPage = string.Empty;
    public string UserName = string.Empty, PageName = string.Empty, CultureCode = string.Empty;
    public string Extension = string.Empty;
    public string menuType = string.Empty;
    string pagePath = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Initialize();
        Extension = SageFrameSettingKeys.PageExtension;
        CreateDynamicNav();
        UserModuleID = int.Parse(SageUserModuleID);
        PortalID = GetPortalID;
        UserName = GetUsername;
        CultureCode = GetCurrentCulture();
        PageName = Path.GetFileNameWithoutExtension(PagePath);
        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuGlobal", " var Path='" + ResolveUrl(modulePath) + "';", true);
        pagePath = IsParent ? ResolveUrl(GetParentURL) : ResolveUrl(GetParentURL) + "portal/" + GetPortalSEOName;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuGlobal1", " var PagePath='" + pagePath + "';", true);
        SageFrameConfig sfConfig = new SageFrameConfig();
        DefaultPage = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage).ToLower();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuGlobal2", " var SageFrameDefaultPage='" + DefaultPage + "';", true);
        MenuController.GetMenuSetting(GetPortalID, UserModuleID);
        GetAllMenuSettings();
    }

    public void CreateDynamicNav()
    {
        ContainerClientID = "divNav_" + SageUserModuleID;
        ltrNav.Text = "<div id='" + ContainerClientID + "'></div>";
    }

    public void Initialize()
    {
        appPath = GetApplicationName;
        IncludeJs("SageMenu", "/Modules/SageMenu/js/SageMenu.js");
        // IncludeJs("SageMenu", "/Modules/SageMenu/js/hoverIntent.js");
        IncludeJs("SageMenu", "/Modules/SageMenu/js/superfish.js");
        IncludeCss("SageMenu", "/Modules/SageMenu/css/module.css");
    }

    public void GetAllMenuSettings()
    {
        SageMenuSettingInfo objMenuSetting = new SageMenuSettingInfo();
        objMenuSetting = MenuController.GetMenuSetting(PortalID, UserModuleID);
        BuildMenu(objMenuSetting);
    }

    public void BuildMenu(SageMenuSettingInfo objMenuSetting)
    {
        menuType = objMenuSetting.MenuType;
        switch (int.Parse(objMenuSetting.MenuType))
        {
            case 0:
                //LoadTopAdminMenu();
                break;
            case 1:
                GetPages(objMenuSetting);
                break;
            case 2:
                LoadSideMenu(objMenuSetting);
                break;
            case 3:
                LoadFooterMenu(objMenuSetting);
                break;
        }
    }

    #region "GetPages"

    public void GetPages(SageMenuSettingInfo objMenuSetting)
    {
        List<MenuManagerInfo> objMenuInfoList = GetMenuFront(PortalID, UserName, CultureCode, UserModuleID);
        BindPages(objMenuInfoList, objMenuSetting);
    }

    public void BindPages(List<MenuManagerInfo> objMenuInfoList, SageMenuSettingInfo objMenuSetting)
    {
        int pageID = 0;
        int parentID = 0;
        string itemPath = string.Empty; ;
        StringBuilder html = new StringBuilder();
        int rootItemCount = 0;
        foreach (MenuManagerInfo objMenuInfo in objMenuInfoList)
        {
            if (int.Parse(objMenuInfo.MenuLevel) == 0)
            {
                rootItemCount = objMenuInfoList.IndexOf(objMenuInfo);
            }
        }
        html.Append(BuildMenuClassContainer(int.Parse(objMenuSetting.TopMenuSubType)));
        int countMenuInfo = 0;
        foreach (MenuManagerInfo objMenuInfo in objMenuInfoList)
        {
            pageID = objMenuInfo.MenuItemID;
            parentID = objMenuInfo.ParentID;
            if (objMenuInfo.MenuLevel == "0")
            {
                string PageURL = objMenuInfo.URL.Split('/').Last();
                string pageLink = objMenuInfo.LinkType == "0" ? pagePath + PageURL + Extension : objMenuInfo.LinkURL;
                if (objMenuInfo.LinkURL == string.Empty)
                {
                    pageLink = pageLink.IndexOf(Extension) > 0 ? pageLink : pageLink + Extension;
                }
                string firstclass = string.Empty;
                string activeClass = PageURL == PageName ? "sfActive" : "";
                if (objMenuInfo.ChildCount > 0)
                {
                    firstclass = countMenuInfo == 0 ? "class='sfFirst sfParent " + activeClass + "'" : countMenuInfo == rootItemCount ? "class='sfParent sfLast " + activeClass + "'" : "class='sfParent " + activeClass + "'";
                }
                else
                {
                    firstclass = countMenuInfo == 0 ? "class='sfFirst " + activeClass + "'" : countMenuInfo == rootItemCount ? "class='sfLast " + activeClass + "'" : "class='" + activeClass + "'";
                }

                html.Append("<li ");
                html.Append(firstclass);
                html.Append(">");
                string menuItem = BuildMenuItem(int.Parse(objMenuSetting.DisplayMode), objMenuInfo, pageLink, objMenuSetting.Caption);
                html.Append(menuItem);
                if (objMenuInfo.LinkType == "1")
                {
                    html.Append("<ul class='megamenu'><li style=''><div class='megawrapper'>");
                    html.Append(objMenuInfo.HtmlContent);
                    html.Append("</div></li></ul>");
                }
                else
                {
                    if (objMenuInfo.ChildCount > 0)
                    {
                        html.Append("<ul style='display: none; visibility: hidden;'>");
                        itemPath = objMenuInfo.Title;
                        string childCategory = BindChildCategory(objMenuInfoList, pageID, int.Parse(objMenuSetting.DisplayMode), objMenuSetting.Caption);
                        html.Append(childCategory);
                        html.Append("</ul>");
                    }
                }
                html.Append("</li>");
            }
            itemPath = string.Empty;
            countMenuInfo++;
        }
        html.Append("</ul></div>");
        ltrNav.Text = html.ToString();
    }

    #endregion

    #region "LoadSideMenu"

    public void LoadSideMenu(SageMenuSettingInfo objMenuSetting)
    {
        List<MenuInfo> objMenuInfoList = GetSideMenu(PortalID, UserName, objMenuSetting.MenuID, CultureCode);
        BuildSideMenu(objMenuInfoList, objMenuSetting);
    }

    public void BuildSideMenu(List<MenuInfo> objMenuInfoList, SageMenuSettingInfo objMenuSetting)
    {
        int pageID = 0;
        int parentID = 0;
        int categoryLevel = 0;
        string itemPath = string.Empty;
        StringBuilder html = new StringBuilder();
        html.Append("<div class='");
        html.Append(ContainerClientID);
        html.Append("'>");
        if (objMenuInfoList.Count > 0)
        {
            html.Append("<ul class='sfSidemenu'>");
            foreach (MenuInfo objMenu in objMenuInfoList)
            {
                pageID = objMenu.PageID;
                parentID = objMenu.ParentID;
                categoryLevel = objMenu.Level;
                string PageURL = objMenu.TabPath.Split('/').Last();
                string pageLink = GetApplicationName + "/" + PageURL + Extension;
                if (objMenu.Level == 0)
                {
                    html.Append("<li class='sfLevel1'>");
                    if (objMenu.ChildCount > 0)
                    {
                        string sideMenuItem = BuildSideMenuItem(int.Parse(objMenuSetting.DisplayMode), objMenu, pageLink, "");
                        html.Append(sideMenuItem);
                    }
                    else
                    {
                        string sideMenuItem = BuildSideMenuItem(int.Parse(objMenuSetting.DisplayMode), objMenu, pageLink, "");
                        html.Append(sideMenuItem);
                    }
                    if (objMenu.ChildCount > 0)
                    {
                        html.Append("<ul>");
                        string sideMenuChildren = BindSideMenuChildren(objMenuInfoList, pageID, int.Parse(objMenuSetting.DisplayMode));
                        html.Append(sideMenuChildren);
                        html.Append("</ul>");
                    }
                    html.Append("</li>");
                }
                itemPath = string.Empty;
            }
            html.Append("</ul>");
        }
        html.Append("</div>");
        ltrNav.Text = html.ToString();
    }

    public string BindSideMenuChildren(List<MenuInfo> objMenuList, int pageID, int menuDisplayMode)
    {
        StringBuilder html = new StringBuilder();
        string childNodes = string.Empty;
        string path = string.Empty;
        string itemPath = string.Empty;
        foreach (MenuInfo objMenu in objMenuList)
        {
            if (objMenu.Level > 0)
            {
                if (objMenu.ParentID == pageID)
                {
                    itemPath = objMenu.PageName;
                    string PageURL = objMenu.TabPath.Split('/').Last();
                    string pageLink = pagePath + PageURL + Extension;
                    string styleClass = objMenu.ChildCount > 0 ? " class='sfParent'" : "";
                    html.Append("<li ");
                    html.Append(styleClass);
                    html.Append(">");
                    string sideMenuItem = BuildSideMenuItem(menuDisplayMode, objMenu, pageLink, "0");
                    html.Append(sideMenuItem);
                    childNodes = BindSideMenuChildren(objMenuList, objMenu.PageID, menuDisplayMode);
                    if (childNodes != string.Empty)
                    {
                        html.Append("<ul>");
                        html.Append(childNodes);
                        html.Append("</ul>");
                    }
                    html.Append("</li>");
                }
            }
        }
        return html.ToString();
    }


    #endregion

    #region

    public void LoadFooterMenu(SageMenuSettingInfo objMenuSetting)
    {
        List<MenuManagerInfo> objMenuMangerList = GetFooterMenu_Localized(PortalID, UserName, CultureCode, UserModuleID);
        BuildFooterMenu(objMenuMangerList, objMenuSetting);
    }

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

    public void BuildFooterMenu(List<MenuManagerInfo> objMenuManagerInfoList, SageMenuSettingInfo objMenuSetting)
    {
        int pageID = 0;
        int parentID = 0;
        string itemPath = string.Empty;
        StringBuilder html = new StringBuilder();
        html.Append("<div class='" + ContainerClientID + "'> <ul class='sfFootermenu'>");
        foreach (MenuManagerInfo objMenuManagerInfo in objMenuManagerInfoList)
        {
            pageID = objMenuManagerInfo.MenuItemID;
            parentID = objMenuManagerInfo.ParentID;
            if (objMenuManagerInfo.MenuLevel == "0")
            {
                string PageURL = objMenuManagerInfo.URL.Split('/').Last();
                string pageLink = objMenuManagerInfo.LinkType == "0" ? pagePath + PageURL + Extension : "/" + PageURL;


                if (PageURL == string.Empty && objMenuManagerInfo.LinkType == "2")
                {
                    pageLink = objMenuManagerInfo.LinkURL;
                }
                else
                {
                    pageLink = objMenuManagerInfo.LinkType == "0" ? pagePath + PageURL + Extension : "/" + PageURL;
                }


                if (objMenuManagerInfo.LinkURL == string.Empty)
                {
                    pageLink = pageLink.IndexOf(Extension) > 0 ? pageLink : pageLink + Extension;
                }
                if (objMenuManagerInfo.ChildCount > 0)
                {
                    html.Append("<li class='sfParent'>");
                }
                else
                {
                    html.Append("<li>");
                }
                string menuItem = BuildMenuItem(int.Parse(objMenuSetting.DisplayMode), objMenuManagerInfo, pageLink, objMenuSetting.Caption);
                html.Append(menuItem);
                if (objMenuManagerInfo.LinkType == "1")
                {
                    html.Append("<ul class='megamenu'><li><div class='megawrapper'>");
                    html.Append(objMenuManagerInfo.HtmlContent);
                    html.Append("</div></li><ul>");
                }
                else
                {
                    if (objMenuManagerInfo.ChildCount > 0)
                    {
                        html.Append("<ul>");
                        itemPath = objMenuManagerInfo.Title;
                        string footerChild = BuildFooterChildren(objMenuManagerInfoList, pageID, int.Parse(objMenuSetting.DisplayMode), objMenuSetting.Caption);
                        html.Append(footerChild);
                        html.Append("</ul>");
                    }
                }
                html.Append("</li>");
            }
            itemPath = string.Empty;
        }
        html.Append("</div>");
        ltrNav.Text = html.ToString();
    }
    public string BuildFooterChildren(List<MenuManagerInfo> objMenuManagerInfoList, int pageID, int menuDisplayMode, string showCaption)
    {
        StringBuilder html = new StringBuilder();
        string childNodes = string.Empty;
        string path = string.Empty;
        string itemPath = string.Empty;
        foreach (MenuManagerInfo objMenuManagerInfo in objMenuManagerInfoList)
        {
            if (int.Parse(objMenuManagerInfo.MenuLevel) > 0)
            {
                if (objMenuManagerInfo.ParentID == pageID)
                {
                    itemPath = objMenuManagerInfo.Title;
                    string PageURL = objMenuManagerInfo.URL.Split('/').Last();
                    string pageLink = objMenuManagerInfo.LinkType == "0" ? pagePath + PageURL + Extension : "/" + PageURL;

                    if (PageURL == string.Empty && objMenuManagerInfo.LinkType == "2")
                    {
                        pageLink = objMenuManagerInfo.LinkURL;
                    }
                    else
                    {
                        pageLink = objMenuManagerInfo.LinkType == "0" ? pagePath + PageURL + Extension : "/" + PageURL;
                    }
                    //if (objMenuManagerInfo.LinkURL == string.Empty)
                    //{
                    //    pageLink = pageLink.IndexOf(Extension) > 0 ? pageLink : pageLink + Extension;
                    //}
                    html.Append("<li>");
                    string menuItem = BuildMenuItem(menuDisplayMode, objMenuManagerInfo, pageLink, showCaption);
                    html.Append(menuItem);
                    childNodes = BuildFooterChildren(objMenuManagerInfoList, objMenuManagerInfo.PageID, menuDisplayMode, showCaption);
                    if (childNodes != string.Empty)
                    {
                        html.Append("<ul>");
                        html.Append(childNodes);
                        html.Append("</ul>");
                    }
                    html.Append("</li>");
                }
            }
        }
        return html.ToString();
    }

    #endregion

    private List<MenuManagerInfo> GetMenuFront(int PortalID, string UserName, string CultureCode, int UserModuleID)
    {
        try
        {
            List<MenuManagerInfo> lstMenuItems = new List<MenuManagerInfo>();
            if (SageFrameSettingKeys.FrontMenu && UserName == "anonymoususer")
            {
                if (!SageFrame.Common.CacheHelper.Get(CultureCode + ".FrontMenu" + PortalID.ToString(), out lstMenuItems))
                {
                    lstMenuItems = MenuManagerDataController.GetSageMenu_Localized(UserModuleID, PortalID, UserName, CultureCode);
                    SageFrame.Common.CacheHelper.Add(lstMenuItems, CultureCode + ".FrontMenu" + PortalID.ToString());
                }
            }
            else
            {
                lstMenuItems = MenuManagerDataController.GetSageMenu_Localized(UserModuleID, PortalID, UserName, CultureCode);
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

    private void GetChildPages(ref List<MenuManagerInfo> lstHierarchy, MenuManagerInfo parent, List<MenuManagerInfo> lstPages)
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

    private string BuildMenuClassContainer(int MenuMode)
    {
        string html = "<div id='" + ContainerClientID + "'>";
        switch (MenuMode)
        {
            case 1:
                html += "<ul class='sf-menu sf-vertical'>";
                break;
            case 2:
                html += "<ul class='sf-menu sf-navbar'>";
                break;
            case 3:
                html += "<span id='sfResponsiveNavBtn' class='inactive'><span></span></span><ul class='sf-menu sfDropdown clearfix'>";
                break;
            case 4:
                html += "<ul class='sf-menu sfCssmenu'>";
                break;
        }
        return html;
    }

    private string BuildMenuItem(int displayMode, MenuManagerInfo objMenuInfo, string pageLink, string caption)
    {
        StringBuilder html = new StringBuilder();
         if (!objMenuInfo.IsActive)
            {
                pageLink = "#";
            }

        string title = objMenuInfo.PageName;
        pageLink = pageLink.Replace("&", "-and-");
        if (objMenuInfo.LinkType != null)
        {
            title = objMenuInfo.LinkType == "0" ? objMenuInfo.PageName : objMenuInfo.Title;
        }
        string image = appPath + "/PageImages/" + objMenuInfo.ImageIcon;
        string imageTag = objMenuInfo.ImageIcon != string.Empty ? "<img src=" + image + ">" : "";
        string arrowStyle = objMenuInfo.ChildCount > 0 ? "<span class='sf-sub-indicator'></span>" : "";
        switch (displayMode)
        {
            case 0://image only
                if (caption == "1")
                {
                    html.Append("<a  href='");
                    html.Append(pageLink);
                    html.Append("'><span class='sfPageicon'>");
                    html.Append(imageTag);
                    html.Append("<em>");
                    html.Append(objMenuInfo.Caption);
                    html.Append("</em>");
                    html.Append("</span>");
                    html.Append(arrowStyle);
                    html.Append("</a>");
                }
                else
                {
                    html.Append("<a  href='");
                    html.Append(pageLink);
                    html.Append("'><span class='sfPageicon'>");
                    html.Append(imageTag);
                    html.Append("</span>");
                    html.Append(arrowStyle);
                    html.Append("</a>");
                }
                break;
            case 1://text only
                if (caption == "1")
                {
                    html.Append("<a  href='");
                    html.Append(pageLink);
                    html.Append("'><span class='sfPagename'>");
                    html.Append(title);
                    html.Append("<em>");
                    html.Append(objMenuInfo.Caption);
                    html.Append("</em>");
                    html.Append("</span>");
                    html.Append(arrowStyle);
                    html.Append("</a>");
                }
                else
                {
                    html.Append("<a  href='");
                    html.Append(pageLink);
                    html.Append("'><span class='sfPagename'>");
                    html.Append(title);
                    html.Append("</span>");
                    html.Append(arrowStyle);
                    html.Append("</a>");
                }
                break;
            case 2: //text and image both
                if (caption == "1")
                {
                    html.Append("<a  href='");
                    html.Append(pageLink);
                    html.Append("'><span class='sfPageicon'>");
                    html.Append(imageTag);
                    html.Append("</span><span class='sfPagename'>");
                    html.Append(title);
                    html.Append("<em>");
                    html.Append(objMenuInfo.Caption);
                    html.Append("</em>");
                    html.Append("</span>");
                    html.Append(arrowStyle);
                    html.Append("</a>");
                }
                else
                {
                    html.Append("<a  href='");
                    html.Append(pageLink);
                    html.Append("'><span class='sfPageicon'>");
                    html.Append(imageTag);
                    html.Append("</span><span class='sfPagename'>");
                    html.Append(title);
                    html.Append("</span>");
                    html.Append(arrowStyle);
                    html.Append("</a>");
                }
                break;
        }
        return html.ToString();
    }

    private List<MenuInfo> GetSideMenu(int PortalID, string UserName, int MenuID, string CultureCode)
    {
        List<MenuInfo> lstMenu = MenuController.GetSideMenu(PortalID, UserName, MenuID, CultureCode);
        List<MenuInfo> lstNewMenu = new List<MenuInfo>();
        foreach (MenuInfo obj in lstMenu)
        {
            obj.ChildCount = lstMenu.Count(
                delegate(MenuInfo objMenu)
                {
                    return (objMenu.ParentID == obj.PageID);
                }
                );
            obj.Title = obj.PageName;
            // if (obj.Level > 0)
            //{
            lstNewMenu.Add(obj);
            //}
        }
        return lstNewMenu;
    }

    private List<MenuManagerInfo> GetCustomSideMenu(int PortalID, string UserName, string CultureCode, int UserModuleID)
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


    private string BindChildCategory(List<MenuManagerInfo> objMenuInfoList, int pageID, int menuDisplayMode, string ShowCaption)
    {
        string strListmaker = string.Empty;
        string childNodes = string.Empty;
        string path = string.Empty;
        string itemPath = string.Empty;
        StringBuilder html = new StringBuilder();
        foreach (MenuManagerInfo objMenuInfo in objMenuInfoList)
        {
            if (int.Parse(objMenuInfo.MenuLevel) > 0)
            {
                if (objMenuInfo.ParentID == pageID)
                {
                    itemPath = objMenuInfo.Title;
                    string PageURL = objMenuInfo.URL.Split('/').Last();
                    string pageLink = string.Empty;
                    if (PageURL == string.Empty && objMenuInfo.LinkType == "2")
                    {
                        pageLink = objMenuInfo.LinkURL;
                    }
                    else
                    {
                        pageLink = objMenuInfo.LinkType == "0" ? pagePath + PageURL + Extension : "/" + PageURL;
                    }
                    if (objMenuInfo.LinkURL == "0")
                    {
                        pageLink = pageLink.IndexOf(Extension) > 0 ? pageLink : pageLink + Extension;
                    }
                    string styleClass = objMenuInfo.ChildCount > 0 ? "class='sfParent'" : "";
                    html.Append("<li ");
                    html.Append(styleClass);
                    html.Append(">");
                    string buildMenu = BuildMenuItem(menuDisplayMode, objMenuInfo, pageLink, ShowCaption);
                    html.Append(buildMenu);
                    childNodes = BindChildCategory(objMenuInfoList, objMenuInfo.MenuItemID, menuDisplayMode, ShowCaption);
                    if (childNodes != string.Empty)
                    {
                        html.Append("<ul>");
                        html.Append(childNodes);
                        html.Append("</ul>");
                    }
                    if (objMenuInfo.HtmlContent != string.Empty)
                    {
                        html.Append("<ul class='megamenu'><li><div class='megawrapper'>");
                        html.Append(objMenuInfo.HtmlContent);
                        html.Append("</div></li></ul>");
                    }
                    html.Append("</li>");
                }
            }
        }
        return html.ToString();
    }

    private string BuildSideMenuItem(int displayMode, MenuInfo objMenuInfo, string pageLink, string caption)
    {
        StringBuilder html = new StringBuilder();
        string title = objMenuInfo.PageName;
        pageLink = pageLink.Replace("&", "-and-");
        string image = appPath + "/PageImages/" + objMenuInfo.IconFile;
        string imageTag = objMenuInfo.IconFile != string.Empty ? "<img src=" + image + ">" : "";
        string arrowStyle = objMenuInfo.ChildCount > 0 ? "<span class='sf-sub-indicator'></span>" : "";
        string firstclass = string.Empty;
        string activeClass = objMenuInfo.PageName == PageName.Replace("-", " ") ? " sfActive" : "";
        string isParent = objMenuInfo.ChildCount > 0 ? (objMenuInfo.Level == 1 ? "class='sfParent level1 " + activeClass + "'" : "class='sfParent " + activeClass + "'") : (objMenuInfo.Level == 1 ? "class='level1 " + activeClass + "'" : "class='" + activeClass + "'");
        switch (displayMode)
        {
            case 0://image only
                if (caption == "1")
                {
                    html.Append("<a ");
                    html.Append(isParent);
                    html.Append(" href='");
                    html.Append(pageLink);
                    html.Append("'><span class='sfPageicon'>");
                    html.Append(imageTag);
                    html.Append("</span>");
                    html.Append(arrowStyle);
                    html.Append("</a>");
                }
                else
                {
                    html.Append("<a ");
                    html.Append(isParent);
                    html.Append(" href='");
                    html.Append(pageLink);
                    html.Append("'><span class='sfPageicon'>");
                    html.Append(imageTag);
                    html.Append("</span>");
                    html.Append(arrowStyle);
                    html.Append("</a>");
                }
                break;
            case 1://text only
                if (caption == "1")
                {
                    html.Append("<a ");
                    html.Append(isParent);
                    html.Append(" href='");
                    html.Append(pageLink);
                    html.Append("'>");
                    html.Append(arrowStyle);
                    html.Append("<span class='sfPagename'>");
                    html.Append(title);
                    html.Append("</span></a>");
                }
                else
                {
                    html.Append("<a ");
                    html.Append(isParent);
                    html.Append(" href='");
                    html.Append(pageLink);
                    html.Append("'>");
                    html.Append(arrowStyle);
                    html.Append("<span class='sfPagename'>");
                    html.Append(title.Replace("-", " "));
                    html.Append("</span></a>");
                }
                break;
            case 2://text and image both
                if (caption == "1")
                {
                    html.Append("<a ");
                    html.Append(isParent);
                    html.Append(" href='");
                    html.Append(pageLink);
                    html.Append("'>");
                    html.Append(arrowStyle);
                    html.Append("<span class='sfPagename'>");
                    html.Append(title);
                    html.Append("</span></a>");

                }
                else
                {
                    html.Append("<a ");
                    html.Append(isParent);
                    html.Append(" href='");
                    html.Append(pageLink);
                    html.Append("'>");
                    html.Append(arrowStyle);
                    html.Append("<span class='sfPagename'>");
                    html.Append(GetSideMenuPadding(objMenuInfo.Level));
                    html.Append(title.Replace("-", " "));
                    html.Append("</span></a>");
                }
                break;
        }
        return html.ToString();
    }

    public string GetSideMenuPadding(int level)
    {
        string padding = string.Empty;
        for (var i = 0; i < level; i++)
        {
            padding += "&nbsp;&nbsp;";
        }
        return padding;
    }
}
