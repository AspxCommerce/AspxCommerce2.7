#region "Copyright"
/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2012 by SageFrame
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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
using System.IO;
using System.Text;
using SageFrame.Security;
using System.Web.Services;
using SageFrame.ModuleManager;
using SageFrame.Templating;
using System.Collections.Generic;
#endregion

public partial class Modules_Pages_ManagePages : BaseAdministrationUserControl
{
    public int UserModuleID, PortalID;
    public string ContainerClientID = string.Empty;
    string baseURL = string.Empty;
    public string UserName = string.Empty, PageName = string.Empty, CultureCode = string.Empty, appPath = string.Empty;
    public string StartupPage;
    public string ActiveTemplateName;
    //public string HostURL;
    public string PageExtension;
    public int IsSideBarVisible = 0;
    public string ActiveTemplate = string.Empty, PortalName;
    public string GetURL = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageExtension = SageFrameSettingKeys.PageExtension;
        SageUserControl suc = new SageUserControl();
        SageFrameConfig sfConfig = new SageFrameConfig();

        StartupPage = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);
        ActiveTemplateName = TemplateName;
        InitializeCssJs();
        appPath = Request.ApplicationPath != "/" ? Request.ApplicationPath : "";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuAdminGlobal1", " var ServicePath='" + appPath + "';", true);
        //module manager
        IncludeJs("ModuleManager", false, "/js/jquery.floatobject-1.0.js", "/js/cookie.js");
        IncludeCss("ModuleManager", "/Modules/Pages/css/widget.css", "/Modules/Pages/css/module.css");
        IncludeJs("ModuleManager", false, "/js/jquery.pagination.js");
        IncludeJs("ModuleManager", false, "/js/jquery.validate.js");
        ActiveTemplate = TemplateName;
        PortalName = GetPortalSEOName;
        bool ShowSideBar = sfConfig.GetSettingBoolValueByIndividualKey(SageFrameSettingKeys.ShowSideBar);
        IsSideBarVisible = ShowSideBar ? 1 : 0;
        //end
        UserModuleID = int.Parse(SageUserModuleID);
        PortalID = GetPortalID;
        UserName = GetUsername;
        GetURL = Page.Request.Url.Scheme + "://" + Request.Url.Authority + GetApplicationName;

        if (!IsPostBack)
        {
            BuildAccessControlledSelection();
            AddImageUrls();
            CultureCode = GetCurrentCulture();
            PageName = Path.GetFileNameWithoutExtension(PagePath);
            BindLayout();
        }
        string pagePath = Request.ApplicationPath != "/" ? Request.ApplicationPath : "";
        pagePath = GetPortalID == 1 ? pagePath : pagePath + "/portal/" + GetPortalSEOName;
        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuGlobal", " var Path='" + ResolveUrl(modulePath) + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuGlobal1", " var PagePath='" + pagePath + "';", true);
    }
    private void BindLayout()
    {
        StringBuilder html = new StringBuilder();
        html.Append("<select id='ddlLayouts' class='sfListmenu'>");
        html.Append(LoadLayout());
        html.Append("</select>");
        ltrLayouts.Text = html.ToString();
    }

    public string LoadLayout()
    {
        string filePath = Decide.IsTemplateDefault(TemplateName.Trim()) ? Utils.GetTemplatePath_Default(TemplateName) : Utils.GetTemplatePath(TemplateName);
        DirectoryInfo dir = new DirectoryInfo(filePath + "/layouts");
        StringBuilder html = new StringBuilder();
        foreach (FileInfo layout in dir.GetFiles())
        {
            string layoutName = layout.Name.Replace(".xml", "");
            html.Append("<option");
            html.Append(" value='");
            html.Append(layoutName);
            html.Append("'>");
            html.Append(layoutName);
            html.Append("</option>");
        }
        return html.ToString();
    }

    private void AddImageUrls()
    {
        string imageFolder = "~/Administrator/Templates/Default/images/";
        imgRemove.Src = GetImageUrl(imageFolder, "context-delete.png", true);
        imgAddNew.Src = GetImageUrl(imageFolder, "context-add-page.png", true);
        imgStarterpage.Src = GetImageUrl(imageFolder, "context-startup.png", true);
        imgEditNew.Src = GetImageUrl(imageFolder, "context-add-page.png", true);

    }
    public string GetImageUrl(string _imageFolder, string imageName, bool isServerControl)
    {
        string path = string.Empty;
        if (isServerControl == true)
        {
            path = _imageFolder + imageName;
        }
        return path;
    }
    private void InitializeCssJs()
    {
        IncludeCss("PageManager", "/Administrator/Templates/Default/css/ui.tree.css", "/Modules/Pages/css/module.css", "/Modules/Pages/css/module.css");
        IncludeJs("PageManager", "/js/jquery.validate.js", "/Administrator/Templates/Default/js/ui.tree.js", "/Administrator/Templates/Default/js/contextmenu.js", "/Administrator/Templates/Default/js/ajaxupload.js", "/Modules/Pages/js/PageMgr.js", "/Modules/Pages/js/PageTreeView.js", "/js/jquery.pagination.js", "/js/jquery.validate.js", "/Modules/Pages/js/ModuleManager.js");
    }

    protected void BuildAccessControlledSelection()
    {
        StringBuilder sb = new StringBuilder();
        RoleController _role = new RoleController();
        string[] roles = _role.GetRoleNames(GetUsername, GetPortalID).ToLower().Split(',');
        if (roles.Contains(SystemSetting.SUPER_ROLE[0].ToLower()))
        {
            sb.Append("<div class='sfRadiobutton'>");
            sb.Append("<input type='radio' id='rdbFronMenu' checked='checked' name='PageMode' style='display:none;'/>");
            sb.Append("<label id='portalPages' class='sfActive'>Portal Pages</label>");
            sb.Append("<input type='radio' id='rdbAdmin' name='PageMode' style='display:none;'/><label id='adminPages'>Admin Pages</label></div>");
            sb.Append("<label id='btnAddpage' class='sfAdd icon-addnew'> Create Page</label>");
            sb.Append("<div class='sfRadiobutton' style='display:none;'>");
            sb.Append("<input id='rdbGenralModules' name='ModuleSwitcher' type='radio' checked='checked' value='0'/>");
            sb.Append("<label>General</label>");
            sb.Append("<input id='rdbAdminModules' name='ModuleSwitcher' type='radio' value='1' />");
            sb.Append("<label>Admin</label></div>");
            ltrAdminModules.Text = "<div id='divIncludeModules' class='sfRight'><input type='checkbox' id='chkPortalModules' class='sfCheckbox'><label>Include Portal Modules</label></div>";
        }
        else
        {
            sb.Append("<label id='btnAddpage' class='sfAdd icon-addnew'> Create Page</label>");
        }
        ltrPageRadioButtons.Text = sb.ToString();
    }
    [WebMethod]
    public static int AddUserModule(LayoutMgrInfo layout)
    {
        return (LayoutMgrDataProvider.AddLayOutMgr(layout));
    }
}