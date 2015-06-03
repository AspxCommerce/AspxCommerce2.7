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
using SageFrame.Templating;
using System.IO;
using System.Collections.Generic;
using SageFrame.Common;
using SageFrame.Security;
using SageFrame.Framework;
using SageFrame.Core;

public partial class Controls_TopStickyBar : BaseAdministrationUserControl
{
    public string appPath = string.Empty;
    public string Extension;
    public string userName = string.Empty;
    public string logoNavigation = string.Empty;
    public bool IsAdmin=false;

    protected void Page_Init(object sender, EventArgs e)
    {
        
        BindThemes();
        BindLayouts();
        BindValues();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IncludeLanguageJS();
        appPath = GetApplicationName;
        SecurityPolicy objSecurity = new SecurityPolicy();
        userName = objSecurity.GetUser(GetPortalID);
        Extension = SageFrameSettingKeys.PageExtension;


        if (!IsPostBack)
        {
            // BindThemes();
            //BindLayouts();
            //BindValues();
            hlnkDashboard.Visible = false;
            SageFrameConfig conf = new SageFrameConfig();
            string ExistingPortalShowProfileLink = conf.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalShowProfileLink);
            lnkAccount.NavigateUrl = GetProfileLink();
            if (ExistingPortalShowProfileLink == "1")
            {
                lnkAccount.Visible = true;
            }
            else
            {
                lnkAccount.Visible = false;
            }
            SageFrame.Application.Application app = new SageFrame.Application.Application();
            lblVersion.Text = string.Format("V {0}", app.FormatShortVersion(app.Version, true));
        }
        hypLogo.NavigateUrl = GetPortalAdminPage();
        hypLogo.ImageUrl = appPath + "/Administrator/Templates/Default/images/sagecomers-logoicon.png";
        RoleController _role = new RoleController();
        string[] roles = _role.GetRoleNames(GetUsername, GetPortalID).ToLower().Split(',');
        if (roles.Contains(SystemSetting.SUPER_ROLE[0].ToLower()) || roles.Contains(SystemSetting.SITEADMIN.ToLower()))
        {
            hlnkDashboard.Visible = true;
            hlnkDashboard.NavigateUrl = GetPortalAdminPage();
            cpanel.Visible = true;
            AspxAdminNotificationView1.Visible = true;
            IsAdmin = true;
        }
        else
        {
            cpanel.Visible = false;
        }
        
    }

    public void BindValues()
    {
        PresetInfo preset = GetPresetDetails;
        if (preset.ActiveTheme == string.Empty)
        {
            preset.ActiveTheme = "default";
        }
        ddlThemes.Items.FindByText(preset.ActiveTheme.ToLower()).Selected = true;
        if (preset.ActiveWidth == string.Empty)
        {
            preset.ActiveWidth = "Wide";
        }
        ddlScreen.Items.FindByText(preset.ActiveWidth.ToLower()).Selected = true;
        string activeLayout = string.Empty;
        string pageName = Request.Url.ToString();
        SageFrameConfig sfConfig = new SageFrameConfig();
        pageName = Path.GetFileNameWithoutExtension(pageName);
        pageName = pageName.ToLower().Equals("default") ? sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) : pageName;
        string tempActiveLayout = string.Empty;
        foreach (KeyValue kvp in preset.lstLayouts)
        {
            string[] arrLayouts = kvp.Value.Split(',');
            if (arrLayouts.Contains(pageName))
            {
                activeLayout = kvp.Key;
            }
            if (kvp.Value.ToLower() == "all")
            {
                tempActiveLayout = kvp.Key;
            }
        }
        if (activeLayout != null && activeLayout != string.Empty)
        {
            if (ddlLayout.Items.FindByText(string.Format("{0}.ascx", activeLayout)) != null)
            {
                ddlLayout.Items.FindByText(string.Format("{0}.ascx", activeLayout)).Selected = true;
            }
        }
        else
        {
            activeLayout = tempActiveLayout;
            if (ddlLayout.Items.FindByText(string.Format("{0}.ascx", activeLayout)) != null)
            {
                ddlLayout.Items.FindByText(string.Format("{0}.ascx", activeLayout)).Selected = true;
            }
        }
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        HttpRuntime.Cache.Remove(CacheKeys.SageFrameJs);
        HttpRuntime.Cache.Remove(CacheKeys.SageFrameCss);
        string optimized_path = Server.MapPath(SageFrameConstants.OptimizedResourcePath);
        IOHelper.DeleteDirectoryFiles(optimized_path, ".js,.css");
        if (File.Exists(Server.MapPath(SageFrameConstants.OptimizedCssMap)))
        {
            XmlHelper.DeleteNodes(Server.MapPath(SageFrameConstants.OptimizedCssMap), "resourcemaps/resourcemap");
        }
        if (File.Exists(Server.MapPath(SageFrameConstants.OptimizedJsMap)))
        {
            XmlHelper.DeleteNodes(Server.MapPath(SageFrameConstants.OptimizedJsMap), "resourcemap/resourcemap");
        }
        PresetInfo preset = new PresetInfo();
        preset = PresetHelper.LoadActivePagePreset(TemplateName, GetPageSEOName(Request.Url.ToString()));
        if (ddlScreen.SelectedItem.ToString() != string.Empty)
        {
            preset.ActiveWidth = ddlScreen.SelectedItem.ToString();
        }
        if (ddlThemes.SelectedItem != null && ddlThemes.SelectedItem.ToString() != string.Empty)
        {
            preset.ActiveTheme = ddlThemes.SelectedItem.ToString();
        }
        if (ddlLayout.SelectedItem != null && ddlLayout.SelectedItem.ToString() != string.Empty)
        {
            preset.ActiveLayout = Path.GetFileNameWithoutExtension(ddlLayout.SelectedItem.ToString());
        }
        List<KeyValue> lstLayouts = preset.lstLayouts;
        string pageName = Request.Url.ToString();
        SageFrameConfig sfConfig = new SageFrameConfig();
        pageName = Path.GetFileNameWithoutExtension(pageName);
        pageName = pageName.ToLower().Equals("default") ? sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) : pageName;
        bool isNewLayout = false;
        int oldPageCount = 0;
        bool isNewPage = false;
        bool deleteRepeat = false;
        bool duplicateLayout = false;
        List<string> pageList = new List<string>();
        foreach (KeyValue kvp in lstLayouts)
        {
            if (kvp.Key == preset.ActiveLayout)
            {
                duplicateLayout = true;
            }
            string[] pages = kvp.Value.Split(',');
            pageList.Add(string.Join(",", pages));
            if (pages.Count() == 1 && pages.Contains(pageName)) // for single pagename and if page = currentpageName
            {
                kvp.Key = preset.ActiveLayout;
            }
            else if (pages.Count() > 1 && pages.Contains(pageName))// for multiple pagename and if page = currentpageName
            {
                isNewLayout = true;                             //its because we have to insert another layout
                List<string> lstnewpage = new List<string>();
                foreach (string page in pages)
                {
                    if (page.ToLower() != pageName.ToLower())
                    {
                        lstnewpage.Add(page);
                    }
                }
                kvp.Value = string.Join(",", lstnewpage.ToArray());
                pageList.Add(kvp.Value);
            }
            else
            {
                oldPageCount++;
            }
            if (kvp.Value == "All" && kvp.Key == preset.ActiveLayout)
            {
                deleteRepeat = true;
            }
        }
        if (lstLayouts.Count == oldPageCount)
        {
            isNewPage = true;
        }
        List<KeyValue> lstNewLayouts = new List<KeyValue>();
        if (isNewPage)
        {
            bool isAppended = false;
            foreach (KeyValue kvp in lstLayouts)
            {
                if (kvp.Key == preset.ActiveLayout)
                {
                    if (kvp.Value.ToLower() != "all")
                    {
                        kvp.Value += "," + pageName;
                    }
                    isAppended = true;
                }
                lstNewLayouts.Add(new KeyValue(kvp.Key, kvp.Value));
            }
            if (!isAppended)
            {
                lstNewLayouts.Add(new KeyValue(preset.ActiveLayout, pageName));
            }
            lstLayouts = lstNewLayouts;
        }
        else if (isNewLayout)
        {
            bool isAppended = false;
            bool isAll = false;
            foreach (KeyValue kvp in lstLayouts)
            {
                if (kvp.Key == preset.ActiveLayout)
                {
                    if (kvp.Value.ToLower() != "all")
                    {
                        kvp.Value += "," + pageName;
                        isAll = true;
                    }
                    isAppended = true;
                }
                lstNewLayouts.Add(new KeyValue(kvp.Key, kvp.Value));
            }
            if (!isAppended && !isAll)
            {
                lstNewLayouts.Add(new KeyValue(preset.ActiveLayout, pageName));
            }
            lstLayouts = lstNewLayouts;
        }
        else if (deleteRepeat)
        {
            foreach (KeyValue kvp in lstLayouts)
            {
                if (kvp.Value.ToLower() != pageName.ToLower())
                {
                    lstNewLayouts.Add(new KeyValue(kvp.Key, kvp.Value));
                }
            }
            lstLayouts = lstNewLayouts;
        }
        else if (duplicateLayout)
        {
            string key = preset.ActiveLayout;
            List<string> pages = new List<string>();
            foreach (KeyValue kvp in lstLayouts)
            {
                if (kvp.Key.ToLower() != preset.ActiveLayout.ToLower())
                {
                    lstNewLayouts.Add(new KeyValue(kvp.Key, kvp.Value));
                }
                else
                {
                    pages.Add(kvp.Value);
                }
            }
            lstNewLayouts.Add(new KeyValue(key, string.Join(",", pages.ToArray())));
            lstLayouts = lstNewLayouts;
        }
        preset.lstLayouts = lstLayouts;
        string presetPath = Decide.IsTemplateDefault(TemplateName.Trim()) ? Utils.GetPresetPath_DefaultTemplate(TemplateName) : Utils.GetPresetPath(TemplateName);
        string pagepreset = presetPath + "/" + TemplateConstants.PagePresetFile;
        presetPath += "/" + "pagepreset.xml";
        AppErazer.ClearSysHash(ApplicationKeys.ActivePagePreset + "_" + GetPortalID);
        PresetHelper.WritePreset(presetPath, preset);
        SageFrame.Common.CacheHelper.Clear("PresetList");
        Response.Redirect(Request.Url.OriginalString);
    }

    public string GetPortalAdminPage()
    {
        string sageNavigateUrl = string.Empty;
        SageFrameConfig sfConfig = new SageFrameConfig();
        if (!IsParent)
        {
            sageNavigateUrl = string.Format("{0}/portal/{1}/Admin/Admin" + Extension, GetParentURL, GetPortalSEOName);
        }
        else
        {
            sageNavigateUrl = GetParentURL + "/Admin/Admin" + Extension;
        }
        return sageNavigateUrl;
    }

    private string GetProfileLink()
    {
        string profileURL = string.Empty;
        SageFrameConfig sfConfig = new SageFrameConfig();
        string profilepage = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalUserProfilePage);
        profilepage = profilepage.ToLower().Equals("user-profile") ? string.Format("/sf/{0}", profilepage) : string.Format("/{0}", profilepage);
        profileURL = !IsParent ? string.Format("{0}/portal/{1}/{2}" + Extension, GetParentURL, GetPortalSEOName, profilepage) : string.Format("{0}/{1}" + Extension, GetParentURL, profilepage);
        return profileURL;
    }

    public void BindThemes()
    {
        string themePath = Decide.IsTemplateDefault(TemplateName) ? Utils.GetThemePath_Default(TemplateName) : Utils.GetThemePath(TemplateName);
        List<KeyValue> lstThemes = new List<KeyValue>();
        if (Directory.Exists(themePath))
        {
            DirectoryInfo dir = new DirectoryInfo(themePath);
            foreach (DirectoryInfo theme in dir.GetDirectories())
            {
                lstThemes.Add(new KeyValue(theme.Name, theme.Name));
            }
        }
        lstThemes.Insert(0, new KeyValue("default", "default"));
        ddlThemes.DataSource = lstThemes;
        ddlThemes.DataTextField = "Key";
        ddlThemes.DataTextField = "Value";
        ddlThemes.DataBind();
    }

    public void BindLayouts()
    {
        string templatePath = Decide.IsTemplateDefault(TemplateName) ? Utils.GetTemplatePath_Default(TemplateName) : Utils.GetTemplatePath(TemplateName);
        List<KeyValue> lstLayouts = new List<KeyValue>();
        int count = 0;
        if (Directory.Exists(templatePath))
        {
            DirectoryInfo dir = new DirectoryInfo(templatePath);
            foreach (FileInfo layout in dir.GetFiles())
            {
                if (layout.Extension.Contains("ascx"))
                {
                    lstLayouts.Add(new KeyValue(count.ToString(), layout.Name));
                    count++;
                }
            }
        }
        ddlLayout.DataSource = lstLayouts;
        ddlLayout.DataValueField = "Key";
        ddlLayout.DataTextField = "Value";
        ddlLayout.DataBind();
    }
}
