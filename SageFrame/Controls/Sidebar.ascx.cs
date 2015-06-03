/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
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
using SageFrame.Dashboard;
using System.Collections.Generic;
using System.Text;
using SageFrame.Templating;


public partial class Controls_Sidebar : BaseAdministrationUserControl
{
    public string appPath = string.Empty;
    public int SidebarMode = 0, IsSideBarVisible = 0, PortalID = 0;
    public string UserName = string.Empty, PortalName = string.Empty;
    string Extension;
    string adminSidebarPosition;
    protected void Page_Init(object sedner, EventArgs e)
    {
        SageFrameConfig sfConfig = new SageFrameConfig();
        adminSidebarPosition = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.AdminSidebarPosition);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        appPath = GetApplicationName;
        Extension = SageFrameSettingKeys.PageExtension;
        SageFrameConfig sfConfig = new SageFrameConfig();
        bool ShowSideBar = sfConfig.GetSettingBoolValueByIndividualKey(SageFrameSettingKeys.ShowSideBar);
        IsSideBarVisible = ShowSideBar ? 1 : 0;
        if (ShowSideBar)
            GetSidebarStatus();
        UserName = GetUsername;
        PortalID = GetPortalID;
        PortalName = GetPortalSEOName;
        if (SidebarMode == 0)
        {
            BuildCollapsedSidebar(adminSidebarPosition);
        }
        else
        {
            BuildExpandedSidebar(adminSidebarPosition);
        }
    }

    protected void GetSidebarStatus()
    {
        DashbordSettingInfo objSetting = DashboardController.GetSettingByKey(new DashbordSettingInfo(DashboardSettingKeys.SIDEBAR_MODE.ToString(), GetUsername.ToString(), GetPortalID));
        SidebarMode = objSetting != null ? (objSetting.SettingValue == "open" ? 1 : 0) : 1;
    }

    public void BuildCollapsedSidebar(string adminSidebarPosition)
    {
        List<Sidebar> lstSidebar = DashboardController.GetSidebar(UserName, PortalID);
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class='sfSidebar sfSidebarhide' style='width:45px; float:" + adminSidebarPosition + "' id='sidebar'><ul class='menu'>");
        foreach (Sidebar parent in lstSidebar)
        {
            string image = string.Format("{0}/Modules/Dashboard/Icons/{1}", appPath, parent.ImagePath);
            string url = Utils.BuildURL(parent.URL + Extension, appPath, GetPortalSEOName, GetPortalID);
            string sidebartext = string.Format("<span style='display:none;'>{0}</span>", parent.DisplayName);
            if (parent.ChildCount == 0 && parent.ParentID == 0)
            {
                sb.Append("<li class='sfLevel0'><a href='" + url + "'>" + GetImage(parent.ImagePath, parent.DisplayName.Replace(" ", "-").ToLower()) + sidebartext + "</a></li>");
            }
            else if (parent.ChildCount > 0 && parent.ParentID == 0)
            {
                sb.Append("<li class='Grandparent sfLevel0'><a href='#'>" + GetImage(parent.ImagePath, parent.DisplayName.Replace(" ", "-").ToLower()) + sidebartext + "</a>");
                sb.Append("<ul style='visibility: hidden; display: none;' class='acitem sfCollapsed'>");
                foreach (Sidebar child in lstSidebar)
                {
                    string Childurl = Utils.BuildURL(child.URL + Extension, appPath, GetPortalSEOName, GetPortalID);
                    string sidebarchildtext = string.Format("<span>{0}</span>", child.DisplayName);
                    if (child.ParentID == parent.SidebarItemID && child.ChildCount == 0)
                    {
                        sb.Append("<li class='sfLevel1'><a href='" + Childurl + "'>" + GetImage(child.ImagePath, child.DisplayName.Replace(" ", "-").ToLower()) + sidebarchildtext + "</a></li>");
                    }
                    else if (child.ParentID == parent.SidebarItemID && child.ChildCount > 0)
                    {
                        sb.Append("<li class='parent sfLevel1'><a href='#'>" + GetImage(child.ImagePath, child.DisplayName.Replace(" ", "-").ToLower()) + sidebarchildtext + "</a>");
                        sb.Append("<ul class='acitem' style='display:none'>");
                        foreach (Sidebar grandChild in lstSidebar)
                        {
                            string GrandChildurl = Utils.BuildURL(grandChild.URL + Extension, appPath, GetPortalSEOName, GetPortalID);
                            string sidebargrandchildtext = string.Format("<span>{0}</span>", grandChild.DisplayName);

                            if (grandChild.ParentID == child.SidebarItemID && grandChild.ChildCount == 0)
                            {
                                sb.Append("<li class='child sfLevel2'><a href='" + GrandChildurl + "'>" + GetImage(grandChild.ImagePath, grandChild.DisplayName.Replace(" ", "-").ToLower()) + sidebargrandchildtext + "</a></li>");
                            }
                            else if (grandChild.ParentID == child.SidebarItemID && grandChild.ChildCount > 0)
                            {
                                sb.Append("<li class='child parentchild sfLevel2'><a href='#'>" + GetImage(grandChild.ImagePath, grandChild.DisplayName.Replace(" ", "-").ToLower()) + sidebargrandchildtext + "</a>");
                                sb.Append("<ul class='acitem' style='display:none'>");
                                foreach (Sidebar grandgrandChild in lstSidebar)
                                {
                                    string GrandGrandChildurl = Utils.BuildURL(grandgrandChild.URL + Extension, appPath, GetPortalSEOName, GetPortalID);
                                    string sidebargrandgrandchildtext = string.Format("<span>{0}</span>", grandgrandChild.DisplayName);
                                    if (grandgrandChild.ParentID == grandChild.SidebarItemID)
                                    {
                                        sb.Append("<li class='grandchild sfLevel3'><a href='" + GrandGrandChildurl + "'>" + "<i class='icon-arrow-slim-e'></i>" + sidebargrandgrandchildtext + "</a></li>");
                                    }
                                }
                                sb.Append("</ul>");
                                sb.Append("</li>");
                            }
                        }
                        sb.Append("</ul>");
                        sb.Append("</li>");
                    }
                }
                sb.Append("</ul>");
                sb.Append("</li>");
            }
        }
        sb.Append("</ul>");
        string imagePath = string.Format("{0}/Administrator/Templates/Default/images/show-arrow.png", appPath);
        sb.Append("<div class='sfHidepanel clearfix'><a href='#'><i class='sidebarCollapse'></i><span style='display:none'></span></a></div>");
        sb.Append("</div>");
        ltrSidebar.Text = sb.ToString();
    }
    public void BuildExpandedSidebar(string adminSidebarPosition)
    {
        List<Sidebar> lstSidebar = DashboardController.GetSidebar(UserName, PortalID);
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class='sfSidebar' id='sidebar' style='float:" + adminSidebarPosition + "'><ul class='menu'>");
        foreach (Sidebar parent in lstSidebar)
        {
            string image = string.Format("{0}/Modules/Dashboard/Icons/{1}", appPath, parent.ImagePath);
            string url = Utils.BuildURL(parent.URL + Extension, appPath, GetPortalSEOName, GetPortalID);
            string sidebartext = string.Format("<span>{0}</span>", parent.DisplayName);
            if (parent.ChildCount == 0 && parent.ParentID == 0)
            {
                sb.Append("<li class='sfLevel0'><a href='" + url + "'>");
                sb.Append(GetImage(parent.ImagePath, parent.DisplayName.Replace(" ", "-").ToLower()));
                sb.Append(sidebartext);
                sb.Append("</a></li>");
            }
            else if (parent.ChildCount > 0 && parent.ParentID == 0)
            {
                sb.Append("<li class='Grandparent sfLevel0'><a href='#'>");
                sb.Append(GetImage(parent.ImagePath, parent.DisplayName.Replace(" ", "-")).ToLower());
                sb.Append(string.Format("<span>{0}</span>", parent.DisplayName) + "</a>");
                sb.Append("<ul class='acitem' style='display:none'>");
                foreach (Sidebar child in lstSidebar)
                {
                    string Childurl = Utils.BuildURL(child.URL + Extension, appPath, GetPortalSEOName, GetPortalID);
                    string sidebarchildtext = string.Format("<span>{0}</span>", child.DisplayName);
                    if (child.ParentID == parent.SidebarItemID && child.ChildCount == 0)
                    {
                        sb.Append("<li class='sfLevel1'><a href='" + Childurl + "'>");
                        sb.Append(GetImage(child.ImagePath, child.DisplayName.Replace(" ", "-")).ToLower() + sidebarchildtext + "</a></li>");
                    }
                    else if (child.ParentID == parent.SidebarItemID && child.ChildCount > 0)
                    {
                        sb.Append("<li class='parent sfLevel1'><a href='#'>");
                        sb.Append(GetImage(child.ImagePath, child.DisplayName.Replace(" ", "-").ToLower()) + sidebarchildtext + "</a>");
                        sb.Append("<ul class='acitem' style='display:none'>");
                        foreach (Sidebar grandChild in lstSidebar)
                        {
                            string GrandChildurl = Utils.BuildURL(grandChild.URL + Extension, appPath, GetPortalSEOName, GetPortalID);
                            string sidebargrandchildtext = string.Format("<span>{0}</span>", grandChild.DisplayName);
                            if (grandChild.ParentID == child.SidebarItemID && grandChild.ChildCount == 0)
                            {
                                sb.Append("<li class='child sfLevel2'><a href='" + GrandChildurl + "'>");
                                sb.Append("<i class='icon-arrow-slim-e'></i>" + sidebargrandchildtext + "</a></li>");
                            }
                            else if (grandChild.ParentID == child.SidebarItemID && grandChild.ChildCount > 0)
                            {
                                sb.Append("<li class='child parentchild sfLevel2'><a href='#'>");
                                sb.Append("<i class='icon-arrow-slim-e'></i>" + sidebargrandchildtext + "</a>");
                                sb.Append("<ul class='acitem' style='display:none'>");
                                foreach (Sidebar grandgrandChild in lstSidebar)
                                {
                                    string GrandGrandChildurl = Utils.BuildURL(grandgrandChild.URL + Extension, appPath, GetPortalSEOName, GetPortalID);
                                    string sidebargrandgrandchildtext = string.Format("<span>{0}</span>", grandgrandChild.DisplayName);
                                    if (grandgrandChild.ParentID == grandChild.SidebarItemID)
                                    {
                                        sb.Append("<li class='grandchild sfLevel3'><a href='" + GrandGrandChildurl + "'>" + "<i class='icon-arrow-dash'></i>" + sidebargrandgrandchildtext + "</a></li>");
                                    }
                                }
                                sb.Append("</ul>");
                                sb.Append("</li>");
                            }
                        }
                        sb.Append("</ul>");
                        sb.Append("</li>");
                    }
                }
                sb.Append("</ul>");
                sb.Append("</li>");
            }
        }
        sb.Append("</ul>");
        string imagePath = string.Format("{0}/Administrator/Templates/Default/images/hide-arrow.png", appPath);
        sb.Append("<div class='sfHidepanel clearfix'><a href='#'><i class='sidebarExpand'></i><span></span></a></div>");
        sb.Append("</div>");
        ltrSidebar.Text = sb.ToString();
    }
    private string GetImage(string img, string PageName)
    {
        string image = string.Empty;
        if (img == null || img.Trim().Length == 0)
        {
            image = "<i class='icon-" + PageName + "'></i>";
        }
        else
        {
            image = string.Format("{0}/Modules/Dashboard/Icons/{1}", appPath, img);
            image = "<img src='" + img + "'/>";
        }
        return image;
    }
}

