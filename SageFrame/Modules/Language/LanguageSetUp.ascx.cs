#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion 

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SageFrame.Web.Utilities;
using SageFrame.Localization;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Threading;
#endregion 

public partial class Modules_Language_LanguageSetUp : BaseAdministrationUserControl
{
    public event EventHandler CancelButtonClick;
   
    private string languageMode = "Normal";
    public string appPath = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        appPath = GetApplicationName;
        if (!Page.IsPostBack)
        {
            LoadAllCultures();           
            GetFlagImage();            
        }
    }

    public DropDownList ControlB_DDL
    {
        get
        {
            return this.ddlLanguage;
        }
    }
    public void LoadAllCultures()
    {
        string mode = languageMode == "Native" ? "NativeName" : "LanguageName";
        List<Language> lstAllCultures = LocaleController.GetCultures();
        List<Language> lstAvailableLocales = LocalizationSqlDataProvider.GetAvailableLocales();
        List<Language> filterLocales = FilterLocales(lstAllCultures, lstAvailableLocales);
        ddlLanguage.DataSource = filterLocales.OrderBy(item => mode);
        ddlLanguage.DataTextField = mode;
        ddlLanguage.DataValueField = "LanguageCode";
        ddlLanguage.DataBind();

        List<ListItem> listCopy = new List<ListItem>();
        foreach (ListItem item in ddlLanguage.Items)
            listCopy.Add(item);
        ddlLanguage.Items.Clear();
        foreach (ListItem item in listCopy.OrderBy(item => item.Text))
            ddlLanguage.Items.Add(item);
    }
    protected List<Language> FilterLocales(List<Language> lstAllCultures, List<Language> lstAvailableLocales)
    {
        List<Language> lstNotAvailableLocales = new List<Language>();
        foreach (Language objLang in lstAllCultures)
        {
            bool isExist = lstAvailableLocales.Exists(
                    delegate(Language obj)
                    {
                        return (obj.LanguageCode == objLang.LanguageCode);
                    }
                );
            if (!isExist)
                lstNotAvailableLocales.Add(objLang);
        }
        return lstNotAvailableLocales;
    }


    protected void imbCancel_Click(object sender, EventArgs e)
    {
        CancelButtonClick(sender,e);
    }
    protected void imbUpdate_Click(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedItem != null)
        {
            AddNewLanguage();
        }
        else
        {
            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("LanguageModule", "LanguageIsNotAvailable"), "", SageMessageType.Alert);            

        }
    }
  
    protected void AddNewLanguage()
    {
        Language objLang = new Language();
        objLang.LanguageName = this.ddlLanguage.SelectedItem.ToString();
        objLang.LanguageCode = this.ddlLanguage.SelectedValue.ToString();
        objLang.FallBackLanguage = "English";
        objLang.FallBackLanguageCode = "en-US";
        try
        {
            LocalizationSqlDataProvider.AddLanguage(objLang);
            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("LanguageModule", "LanguageAddedSuccessfully"), "", SageMessageType.Success);            
            LoadAllCultures();
            GetFlagImage();
            Localization_CreateLanguagePack clp = (Localization_CreateLanguagePack)this.Parent.FindControl("CreateLanguagePack1");
            DropDownList ddlResourceLocale = (DropDownList)clp.FindControl("ddlResourceLocale");
            LoadAllCulturesDropDown(ddlResourceLocale);
            UpdateLocalizeMenuFields();
        }
        catch (Exception ex)
        {

            ProcessException(ex);
        }
        
    }

    protected void UpdateLocalizeMenuFields()
    {
        Modules_LocalPage_LocalPage localPage = (Modules_LocalPage_LocalPage)this.Parent.FindControl("ctrl_MenuEditor");
        DropDownList ddlAvailableLocales = (DropDownList)localPage.FindControl("ddlAvailableLocales");
        ddlAvailableLocales.DataSource = LocalizationSqlDataProvider.GetAvailableLocales();
        ddlAvailableLocales.DataTextField = "LanguageName";
        ddlAvailableLocales.DataValueField = "LanguageCode";
        ddlAvailableLocales.DataBind();

        List<ListItem> listCopy = new List<ListItem>();
        foreach (ListItem item in ddlAvailableLocales.Items)
            listCopy.Add(item);
        ddlAvailableLocales.Items.Clear();
        foreach (ListItem item in listCopy.OrderBy(item => item.Text))
            ddlAvailableLocales.Items.Add(item);

        
    }

    public void LoadAllCulturesDropDown(DropDownList ddl)
    {
        string mode = languageMode == "Native" ? "NativeName" : "LanguageName";
        // List<Language> lstAllCultures = LocaleController.GetCultures();
        List<Language> lstAvailableLocales = LocalizationSqlDataProvider.GetAvailableLocales();
        ddl.DataSource = lstAvailableLocales;
        ddl.DataTextField = mode;
        ddl.DataValueField = "LanguageCode";
        ddl.DataBind();

        List<ListItem> listCopy = new List<ListItem>();
        foreach (ListItem item in ddl.Items)
            listCopy.Add(item);
        ddl.Items.Clear();
        foreach (ListItem item in listCopy.OrderBy(item => item.Text))
            ddl.Items.Add(item);
    }
   
    protected void GetFlagImage()
    {
        string code = this.ddlLanguage.SelectedValue;
        imgFlagLanguage.ImageUrl = ResolveUrl("~/images/flags/" + code.Substring(code.IndexOf("-") + 1) + ".png");
        
    }
   
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFlagImage();
    }
    protected void rbLanguageType_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (rbLanguageType.SelectedIndex)
        {
            case 0:
                LoadAllCultures();                
                GetFlagImage();
                break;
            case 1:
                LoadNativeNames();
                break;
        }
    }

    protected void LoadNativeNames()
    {
        languageMode = "Native";
        LoadAllCultures();
        GetFlagImage();
    }  
}
