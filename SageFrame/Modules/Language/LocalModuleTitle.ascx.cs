#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

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
using SageFrame.Localization;
using System.Collections.Generic;
using SageFrame.Localization.Info;

public partial class Modules_Language_LocalModuleTitle : BaseAdministrationUserControl
{
    public static string path = string.Empty;
    public event EventHandler CancelButtonClick;
    protected void Page_Load(object sender, EventArgs e)
    {
        Initialize();
        if (!IsPostBack)
        {
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            path = modulePath;
            LoadModuleTitles();
            BindAvailableLocales();
            GetFlagImage();
            
        }
    }
    public void Initialize()
    {
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/popup.css");
    }

    protected void BindAvailableLocales()
    {
        this.ddlAvailableLocales.DataSource = LocalizationSqlDataProvider.GetAvailableLocales();
        this.ddlAvailableLocales.DataTextField = "LanguageName";
        this.ddlAvailableLocales.DataValueField = "LanguageCode";
        this.ddlAvailableLocales.DataBind();

      
    }

    protected void ddlAvailableLocales_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFlagImage();
        LoadModuleTitles();
    }
    protected void GetFlagImage()
    {
        string code = this.ddlAvailableLocales.SelectedValue;
        string resolvedUrl = ResolveUrl("~/");
        imgFlag.ImageUrl = ResolveUrl(resolvedUrl + "images/flags/" + code.Substring(code.IndexOf("-") + 1) + ".png");
    }
    public void LoadModuleTitles()
    {
        List<LocalModuleInfo> lstModuleTitles = LocaleController.GetLocalModuleTitle(GetPortalID, this.ddlAvailableLocales.SelectedValue.ToString());
        gdvLocalModuleTitle.DataSource = lstModuleTitles;
        gdvLocalModuleTitle.DataBind();
    }

    protected void Page_init(object sender, EventArgs e)
    {

        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var aspxservicePath='" + ResolveUrl(modulePath) + "';", true);

    }

    protected void imbUpdate_Click(object sender, EventArgs e)
    {
        List<LocalModuleInfo> lstLocalModuleTitle = new List<LocalModuleInfo>();

        foreach (GridViewRow gvRow in gdvLocalModuleTitle.Rows)
        {
            TextBox txtLocalModuleTitle = (TextBox)gvRow.FindControl("txtLocalModuleTitle");
            LocalModuleInfo objInfo = new LocalModuleInfo();
            objInfo.UserModuleID = int.Parse(gdvLocalModuleTitle.DataKeys[int.Parse(gvRow.DataItemIndex.ToString())]["UserModuleID"].ToString());
            objInfo.LocalModuleTitle = txtLocalModuleTitle.Text;
            objInfo.CultureCode = ddlAvailableLocales.SelectedValue.ToString();
            lstLocalModuleTitle.Add(objInfo);
        }

        try
        {
            LocaleController.AddUpdateLocalModuleTitle(lstLocalModuleTitle);
            LoadModuleTitles();
            SageFrame.Common.CacheHelper.Clear("MegaMenuNepInd");
            SageFrame.Common.CacheHelper.Clear("MegaMenuNepBuss");
            SageFrame.Common.CacheHelper.Clear("MegaMenuEngInd");
            SageFrame.Common.CacheHelper.Clear("MegaMenuEngBiz");
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void gdvLocalModuleTitle_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gdvLocalModuleTitle_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void gdvLocalModuleTitle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvLocalModuleTitle.PageIndex = e.NewPageIndex;
        LoadModuleTitles();
    }
    protected void imbCancel_Click(object sender, EventArgs e)
    {
        CancelButtonClick(sender, e);
    }
}
