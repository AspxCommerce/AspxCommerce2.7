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
using SageFrame.Localization;
using System.Collections.Generic;
#endregion

public partial class Modules_LocalPage_LocalPage : BaseAdministrationUserControl
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
            LoadPages();
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
        LoadPages();
    }
    protected void GetFlagImage()
    {
        string code = this.ddlAvailableLocales.SelectedValue;
        string resolvedUrl = ResolveUrl("~/");
        imgFlag.ImageUrl = ResolveUrl(resolvedUrl+"images/flags/" + code.Substring(code.IndexOf("-") + 1) + ".png");
    }

    public void LoadPages()
    {
        List<LocalPageInfo> lstPages = LocaleController.GetLocalPageName(GetPortalID, this.ddlAvailableLocales.SelectedValue.ToString());
        gdvLocalPage.DataSource = lstPages;
        gdvLocalPage.DataBind();
    }

    protected void Page_init(object sender, EventArgs e)
    {

        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var aspxservicePath='" + ResolveUrl(modulePath) + "';", true);

    }

    protected void imbUpdate_Click(object sender, EventArgs e)
    {
        List<LocalPageInfo> lstLocalPage = new List<LocalPageInfo>();

        foreach (GridViewRow gvRow in gdvLocalPage.Rows)
        {
            TextBox txtLocalName = (TextBox)gvRow.FindControl("txtLocalPageName");
            TextBox txtLocalPageCaption = (TextBox)gvRow.FindControl("txtLocalPageCaption");
            LocalPageInfo objInfo = new LocalPageInfo();
            objInfo.PageID = int.Parse(gdvLocalPage.DataKeys[int.Parse(gvRow.DataItemIndex.ToString())]["PageID"].ToString());
            objInfo.LocalPageName = txtLocalName.Text;
            objInfo.LocalPageCaption = txtLocalPageCaption.Text;
            objInfo.CultureCode = ddlAvailableLocales.SelectedValue.ToString();
            lstLocalPage.Add(objInfo);
        }

        try
        {
            LocaleController.AddUpdateLocalPage(lstLocalPage);
            LoadPages();
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
    protected void gdvLocalPage_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gdvLocalPage_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void gdvLocalPage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvLocalPage.PageIndex = e.NewPageIndex;
        LoadPages();
    }
    protected void imbCancel_Click(object sender, EventArgs e)
    {
        CancelButtonClick(sender, e);
    }
}
