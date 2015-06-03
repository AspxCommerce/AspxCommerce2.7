#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.FileManager;
using SageFrame.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using SageFrame.Localization;
using SageFrame.Framework;
using SageFrame.Web.Utilities;
using System.Globalization;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using SageFrame.Localization.Info;
using System.Web.UI.HtmlControls;
#endregion


public partial class Localization_Language : BaseAdministrationUserControl
{
    public static string path = string.Empty;
    private string languageMode = "Normal";
    public static string CultureCode = string.Empty;
    protected string BaseDir;
    protected string ModuleDir;
    protected  string controlsDir;
    public void Initialize()
    {
        IncludeJs("languageManager", "/Modules/Language/js/jqueryFileTree.js", "/Editors/ckeditor/ckeditor.js", "/Editors/ckeditor/adapters/jquery.js", "js/jquery.dd.js", "js/jquery.pagination.js");
        IncludeCss("Language", "/Modules/Language/css/module.css", "/Modules/Language/css/popup.css");

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        path = modulePath;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LocalizationGlobalVariable1", " var LocalizationFilePath='" + ResolveUrl(modulePath) + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LocalizationGlobalVariable2", " var resourceRootTreePath='/Modules/';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LocalizationGlobalVariable3", " var ImagePath='/SageFrame/Modules/';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + SageUserModuleID + "';", true);
        BaseDir = GetAbsolutePath(HttpContext.Current.Request.PhysicalApplicationPath.ToString());
        controlsDir=Path.Combine(BaseDir,"Controls");   
        ModuleDir = Path.Combine(BaseDir, "Modules");
        Initialize();
        InitializePagetPath();
        HookEventHandlers();


        if (HttpContext.Current.Session!=null && HttpContext.Current.Session["IsCultureChange"] != null &&
           ( (int)HttpContext.Current.Session["IsCultureChange"] == 1 || ((int)HttpContext.Current.Session["IsCultureChange"] == 2 && Page.IsPostBack && this.hdntemp.Value == "")))
        {
            HttpContext.Current.Session["IsCultureChange"] = 2;
            hlRefresh.Visible= true;
            hlRefresh.NavigateUrl = Request.Url.ToString();
            ControlVisibility(false, false, false, false, false, false, false, false);
           
            if (this.hdntemp.Value == "") this.hdntemp.Value="0";
                     
        
        }else if (!Page.IsPostBack)
        {
            this.hdntemp.Value = "1"; 
            CreatePortalLanguageSession();
            GetLanguageList();
            LoadCurrentCulture();
            AddImageUrl();
            AddConfirmationJS();
            LoadSystemDefaultLanguage();
            ControlVisibility(true, false, false, false, false, false, false,false);
            LoadPagerDDL(int.Parse(ViewState["RowCount"].ToString()));
        }
    }

    protected void CreatePortalLanguageSession()
    {
        ViewState["PortalLanguages"] = LocalizationSqlDataProvider.GetPortalLanguages(GetPortalID);
    }
    public void InitializePagetPath()
    {
        path = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
    }
    public void HookEventHandlers()
    {
        ctrl_LanguagePackSetup.CancelButtonClick += new EventHandler(ctrl_AddLanguage_CancelClick);
        CreateLanguagePack1.CancelButtonClick += new EventHandler(ctrl_CreateLanguagePack_CancelClick);
        ctrl_TimeZoneEditor.CancelButtonClick += new EventHandler(ctrl_TimeZoneEditor_CancelClick);
        LanguagePackInstaller1.CancelButtonClick += new EventHandler(ctrl_LanguagePackInstaller_CancelClick);
        ctrl_MenuEditor.CancelButtonClick += new EventHandler(ctrl_MenuEditor_CancelClick);
        ctrl_ModuleTitleEditor.CancelButtonClick += new EventHandler(ctrl_ModuleLocaleTitle_CancelClick);

    }

    protected void ctrl_ModuleLocaleTitle_CancelClick(object sender, EventArgs e)
    {
        ControlVisibility(true, false, false, false, false, false, false, false);
        GetLanguageList();
    }

    private void AddImageUrl()
    {
        //imbAddLanguage.ImageUrl = GetAdminImageUrl("add.png", true);
        //imbInstallLang.ImageUrl = GetAdminImageUrl("imginstall.png", true);
        //imbCreateLangPack.ImageUrl = GetAdminImageUrl("btncreatepackage.png", true);
        //imbEditTimeZone.ImageUrl = GetAdminImageUrl("btnedittimezone.png", true);
        //imbCancel.ImageUrl = GetAdminImageUrl("btnback.png", true);
        imbDeleteResxFile.ImageUrl = GetAdminImageUrl("imgdelete.png", true);
        //imbUpdate.ImageUrl = GetAdminImageUrl("btnSave.png", true);
        imbDeleteResxFile.Visible = false;
        //lblDeleteResx.Visible = false;
        imbUpdate.Visible = false;
        //lblUpdateResxFile.Visible = false;
        //imbLocalizeMenu.ImageUrl = GetTemplateImageUrl("menu.png", true);
        //imbLocalizeModuleTitle.ImageUrl = GetTemplateImageUrl("menu.png", true);

    }

    private void AddConfirmationJS()
    {
        imbDeleteResxFile.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("Adsense", "AreYouSureToDelete") + "')");
    }
    public void GetLanguageList()
    {
        string mode = languageMode == "Native" ? "NativeName" : "LanguageName";
        List<Language> lstAvailableLocales = LocaleController.AddNativeNamesToList(LocalizationSqlDataProvider.GetAvailableLocales());

        List<Language> sortedLanguage = new List<Language> { };
        foreach (Language item in lstAvailableLocales)
        {
            sortedLanguage.Add(item);
        }
        lstAvailableLocales.Clear();
        foreach (Language lang in sortedLanguage.OrderBy(lang => lang.LanguageName))
        {
            lstAvailableLocales.Add(lang);
        }
        gdvLangList.DataSource = lstAvailableLocales;
        gdvLangList.DataBind();
        ViewState["RowCount"] = lstAvailableLocales.Count;
    }
    protected List<Language> AddSiteDefaultLanguage(List<Language> lstAvailableLocales)
    {
        lstAvailableLocales.Insert(0, new Language(0, Thread.CurrentThread.CurrentCulture.EnglishName.ToString(), Thread.CurrentThread.CurrentCulture.ToString()));
        return lstAvailableLocales;
    }

    public void LoadSystemDefaultLanguage()
    {
        SageFrameConfig sfConf = new SageFrameConfig();
        string cultureCode = sfConf.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalDefaultLanguage);
        lblSystemDefault.Text = LocaleController.GetLanguageNameFromCode(cultureCode);
        imgFlagSystemDefault.ImageUrl = ResolveUrl("~/images/flags/" + Regex.Replace(cultureCode, "[a-z]{2}-", "").ToLower() + ".png");

    }
    protected void gdvLangList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "EditResources":
                string Code = "";
                if (gdvLangList.PageIndex == 0)
                {
                    Code = gdvLangList.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text;
                }
                else
                {
                    //int index=int.Parse(e.CommandArgument.ToString())-gdvLangList.PageSize;
                    //index=index>gdvLangList.PageSize?gdv

                   Code = gdvLangList.Rows[int.Parse(e.CommandArgument.ToString()) - gdvLangList.PageSize*gdvLangList.PageIndex].Cells[1].Text;
                   //Code = e.Row.Cells[1].Text.ToString();
                }
                CultureCode = Code;
                hdnCultureCode.Value = Code;
                InitializeTreeview();
                ControlVisibility(false, true, false, false, false, false, false,false);
                languageContent.Visible = false;
                controlButtons.Visible = false;
                break;
            //case "EditLanguage":
            //    Response.Redirect("LanguageSetup.aspx");
            //break;
            case "DeleteResources":
                if (gdvLangList.PageIndex == 0)
                {
                    Code = gdvLangList.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text;
                    DeleteLanguage(Code);
                }
                else
                {
                    Code = gdvLangList.Rows[int.Parse(e.CommandArgument.ToString()) - gdvLangList.PageSize*gdvLangList.PageIndex].Cells[1].Text;
                    DeleteLanguage(Code);
                }
                break;
        }
    }

    private void DeleteLanguage(string code)
    {
       // UserControl ucControl = (UserControl)this.FindControl("ctrl_LanguagePackSetup");
      
        Page page = this.Page;
        LocaleController lcl = new LocaleController();
        lcl.DeleteLanguage(code);
        ShowMessage("", GetSageMessage("LanguageModule", "LanguageDeletedSuccessfully"), "", SageMessageType.Success);
        GetLanguageList();
        LoadPagerDDL(int.Parse(ViewState["RowCount"].ToString()));
        Modules_Language_LanguageSetUp ctrlB = (Modules_Language_LanguageSetUp)this.FindControl("ctrl_LanguagePackSetup");
        DropDownList ddlLanguage = ctrlB.ControlB_DDL;
        //DropDownList ddlLanguage = (DropDownList)ucLanguageSetUp.FindControl("ddlLanguage");
        LoadAllCultures(ddlLanguage);

        if (code == GetCurrentCulture())
        {
            PageBase.SetCultureInfo("en-US", "en-US");
            Response.Redirect(Request.Url.ToString());
        }
        UpdateLocalizeMenuFields();
        Localization_CreateLanguagePack clp = (Localization_CreateLanguagePack)this.FindControl("CreateLanguagePack1");
        DropDownList ddlResourceLocale = (DropDownList)clp.FindControl("ddlResourceLocale");
        LoadAllCultureInCreatePack(ddlResourceLocale);
        UpdateLocalizeMenuFields();

        DeleteAllResources(code);
    }

    protected void DeleteAllResources(string langCode)
    {
        try
        {
            
            string adminFolder = BaseDir + "Sagin";
            TraverseForDeletion(adminFolder, langCode,false);

            TraverseForDeletion(controlsDir,langCode,false);

            TraverseForDeletion(ModuleDir,langCode,false);

            string XmlrootFolder = Path.Combine(GetAbsolutePath(HttpContext.Current.Request.PhysicalApplicationPath.ToString()), "XMLMessage");
            TraverseForDeletion(XmlrootFolder,langCode,true);

            string clientRescFolder= Path.Combine(GetAbsolutePath(HttpContext.Current.Request.PhysicalApplicationPath.ToString()), "js/SystemLocale");
            TraverseForDeletion(clientRescFolder,langCode,true);

          
        }
        catch
        {

        }
    }
    protected void TraverseForDeletion(string folderPath,string langCode,bool deleteFile)
    {
        
        DirectoryInfo DirInfo = new DirectoryInfo(folderPath);
        if (deleteFile)
        {
            foreach (FileInfo fileInfo in DirInfo.GetFiles())
            {
                if (fileInfo.Name.Contains(langCode))
                {
                    fileInfo.Delete();
                }
            }
        }
         bool flag = false;
            IsValidateTraverse(DirInfo, "App_LocalResources", ref flag);
            if (flag)
            {
                foreach (DirectoryInfo dirInfor in DirInfo.GetDirectories())
                {

                    TraverseChildDirectory(dirInfor.FullName, langCode);
                }

            }
            flag = false;
            IsValidateTraverse(DirInfo, "Language", ref flag);           
                if (flag)
                {
                    foreach (DirectoryInfo dirInfor in DirInfo.GetDirectories())
                    {

                        TraverseChildDirectory(dirInfor.FullName, langCode);
                    }
                }

               
    }
    protected void TraverseChildDirectory(string folderPath, string langCode)
    {
        try
        {
            DirectoryInfo info = new DirectoryInfo(folderPath);
            foreach (FileInfo folderFile in info.GetFiles())
            {
                string name = folderFile.Name;
                if (Path.GetExtension(name).Equals(".resx") || Path.GetExtension(name).Equals(".xml") || Path.GetExtension(name).Equals(".js"))
                {
                    bool status = VerifyFile(ref name, Path.GetExtension(name));
                    if (status)
                    {
                        if (folderFile.Name.Contains(langCode))
                        {
                            folderFile.Delete();
                        }
                    }
                }
            }
            foreach (DirectoryInfo dirInfor in info.GetDirectories())
            {
                TraverseChildDirectory(dirInfor.FullName, langCode);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    protected void UpdateLocalizeMenuFields()
    {
        Modules_LocalPage_LocalPage localPage = (Modules_LocalPage_LocalPage)this.FindControl("ctrl_MenuEditor");
        DropDownList ddlAvailableLocales = (DropDownList)localPage.FindControl("ddlAvailableLocales");
        GridView gdvLocalPage = (GridView)localPage.FindControl("gdvLocalPage");
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

        List<LocalPageInfo> lstPages = LocaleController.GetLocalPageName(GetPortalID,ddlAvailableLocales.SelectedValue.ToString());
        gdvLocalPage.DataSource = lstPages;
        gdvLocalPage.DataBind();
    }

    protected void LoadAllCultureInCreatePack(DropDownList ddl)
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
    public void LoadAllCultures(DropDownList ddl)
    {
        string mode = languageMode == "Native" ? "NativeName" : "LanguageName";
        List<Language> lstAllCultures = LocaleController.GetCultures();
        List<Language> lstAvailableLocales = LocalizationSqlDataProvider.GetAvailableLocales();
        ddl.DataSource = FilterLocales(lstAllCultures, lstAvailableLocales);
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
    protected void gdvLangList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string code = e.Row.Cells[1].Text;
            int index = code.IndexOf("-");
            string shortCode = code.Substring(index + 1);
            Image img = (Image)e.Row.FindControl("imgFlag");
            img.Attributes.Add("src", ResolveUrl("~/images/flags/" + shortCode.ToLower() + ".png"));
            List<Language> lstPortalLanguage = (List<Language>)ViewState["PortalLanguages"];
            if (lstPortalLanguage.Exists(delegate(Language obj) { return obj.LanguageID == int.Parse(e.Row.Cells[4].Text); }))
            {
                ((CheckBox)e.Row.FindControl("chkIsEnabled")).Checked = true;
            }
            SageFrameConfig sfConf = new SageFrameConfig();
            string portalCulture = sfConf.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalDefaultLanguage);
            if (code.Equals(portalCulture))
            {
                ((CheckBox)e.Row.FindControl("chkIsEnabled")).Enabled = false;
                ((CheckBox)e.Row.FindControl("chkIsEnabled")).Checked = true;
                ((LinkButton)e.Row.FindControl("btnLanguageDelete")).Visible = false;
            }
            if (code.Equals("en-US"))
            {
                ((CheckBox)e.Row.FindControl("chkIsEnabled")).Enabled = false;
                ((CheckBox)e.Row.FindControl("chkIsEnabled")).Checked = true;
                ((LinkButton)e.Row.FindControl("btnLanguageDelete")).Visible = false;
            }

            e.Row.Cells[4].Visible = false;
            this.gdvLangList.HeaderRow.Cells[4].Visible = false;

        }

    }

    protected void imbCreateLangPack_Click(object sender, EventArgs e)
    {
        ControlVisibility(false, false, false, true, false, false, false,false);
        ResetChildControls();
    }
    public void ResetChildControls()
    {
        ((TextBox)CreateLanguagePack1.FindControl("txtResourcePackName")).Text = "Core";
        ((RadioButtonList)CreateLanguagePack1.FindControl("rbResourcePackType")).SelectedIndex = 0; ;
        ((DropDownList)CreateLanguagePack1.FindControl("ddlResourceLocale")).SelectedIndex = 0; ;
    }
    protected void imbInstallLang_Click(object sender, EventArgs e)
    {
        ControlVisibility(false, false, true, false, false, false, false,false);
    }
    public void ControlVisibility(bool firstDiv, bool resxeditorDiv, bool installLangDiv, bool createLangPackDiv, bool languageSetup, bool timezoneEditor, bool menueditor,bool moduleTitleEditor)
    {
        langEditFirstDiv.Visible = firstDiv;
        langEditSecondDiv.Visible = resxeditorDiv;
        LanguagePackInstaller1.Visible = installLangDiv;
        CreateLanguagePack1.Visible = createLangPackDiv;
        ctrl_LanguagePackSetup.Visible = languageSetup;
        ctrl_TimeZoneEditor.Visible = timezoneEditor;
        ctrl_MenuEditor.Visible = menueditor;
        ctrl_ModuleTitleEditor.Visible = moduleTitleEditor;
    }
    protected void imbAddLanguage_Click(object sender, EventArgs e)
    {
        ControlVisibility(false, false, false, false, true, false, false,false);
    }
    protected void ctrl_AddLanguage_CancelClick(object sender, EventArgs e)
    {
        ControlVisibility(true, false, false, false, false, false, false,false);
        GetLanguageList();
        LoadPagerDDL(int.Parse(ViewState["RowCount"].ToString()));
    }
    protected void ctrl_CreateLanguagePack_CancelClick(object sender, EventArgs e)
    {
        ControlVisibility(true, false, false, false, false, false, false,false);
        GetLanguageList();
    }
    protected void ctrl_LanguagePackInstaller_CancelClick(object sender, EventArgs e)
    {
        ControlVisibility(true, false, false, false, false, false, false,false);
        GetLanguageList();
    }
    protected void ctrl_ResourceEditor_CancelClick(object sender, ImageClickEventArgs e)
    {
        ControlVisibility(true, false, false, false, false, false, false,false);
        GetLanguageList();
    }
    protected void imbEditTimeZone_Click(object sender, EventArgs e)
    {
        Modules_Language_TimeZoneEditor TimeZoneEditor = (Modules_Language_TimeZoneEditor)this.FindControl("ctrl_TimeZoneEditor");
        if (TimeZoneEditor != null)
        {
            DropDownList ddlAvailableLocales = (DropDownList)TimeZoneEditor.FindControl("ddlAvailableLocales");
            if (ddlAvailableLocales != null)
            {
                List<Language> lstAvailableLocales = LocalizationSqlDataProvider.GetAvailableLocales();
                ddlAvailableLocales.DataSource = lstAvailableLocales;
                ddlAvailableLocales.DataTextField = "LanguageName";
                ddlAvailableLocales.DataValueField = "LanguageCode";
                ddlAvailableLocales.DataBind();
            }

            GridView gdvTimeZoneEditor = (GridView)TimeZoneEditor.FindControl("gdvTimeZoneEditor");
            if (gdvTimeZoneEditor != null)
            {
                BindGridTimeZoneEditor(ddlAvailableLocales.SelectedValue.ToString(), gdvTimeZoneEditor);
            }
        }
        ControlVisibility(false, false, false, false, false, true, false,false);
        GetLanguageList();
    }
    protected void BindGridTimeZoneEditor(string language, GridView gdv)
    {
        DataSet ds = new DataSet();
        bool isExists = File.Exists(Server.MapPath(ResourceFile(Localization.TimezonesFile, language, true)));
        if (isExists)
        {
            ds.ReadXml(Server.MapPath(ResourceFile(Localization.TimezonesFile, language, isExists)));
            gdv.DataSource = ds;
            gdv.DataBind();
        }
    }
    private string ResourceFile(string filename, string language, bool isExists)
    {
        return (isExists ? filename.Substring(0, filename.Length - 4) + "." + language + ".xml" : filename);
    }
    protected void ctrl_TimeZoneEditor_CancelClick(object sender, EventArgs e)
    {
        ControlVisibility(true, false, false, false, false, false, false,false);
        GetLanguageList();
    }
    protected void ctrl_MenuEditor_CancelClick(object sender, EventArgs e)
    {
        ControlVisibility(true, false, false, false, false, false, false,false);
        GetLanguageList();
    }
    protected void RestrictDefaultSiteLanguageEdit()
    {
        CultureInfo ci = Thread.CurrentThread.CurrentCulture;
    }
    protected void chkIsEnabled_CheckedChanged(object sender, EventArgs e)
    {
        int isEnabled = ((CheckBox)sender).Checked ? 1 : 0;
        int isPublished = 0;
        GridViewRow row = (GridViewRow)((CheckBox)sender).Parent.Parent;
        int languageId = int.Parse(gdvLangList.DataKeys[row.RowIndex].Values["LanguageID"].ToString());
        string Code=string.Empty;
          if (gdvLangList.PageIndex == 0)
                {
                    Code = row.Cells[1].Text;
                  // Code = e.Row.Cells[1].Text.ToString();
                }
                else
                {
                    //int index=int.Parse(e.CommandArgument.ToString())-gdvLangList.PageSize;
                    //index=index>gdvLangList.PageSize?gdv

                   Code = row.Cells[1].Text;
                   //Code = e.Row.Cells[1].Text.ToString();
                }
        try
        {
            if (isEnabled == 1)
            {
                if (CheckIfResourceFileExists(Code))
                {
                    LocalizationSqlDataProvider.EnableLanguage(GetPortalID, languageId, GetUsername.ToString(), isEnabled, isPublished);
                   // Response.Redirect(Request.Url.OriginalString);
                    ShowMessage("", GetSageMessage("LanguageModule", "LanguageIsEnabled"), "", SageMessageType.Success);
                }

                else
                {
                    ShowMessage("", GetSageMessage("LanguageModule", "LanguageCannotBeEnabled"), "", SageMessageType.Alert);
                    CheckBox thisCheckbox = (CheckBox)sender;
                    thisCheckbox.Checked = false;
                }
            }
            else {
                LocalizationSqlDataProvider.EnableLanguage(GetPortalID, languageId, GetUsername.ToString(), isEnabled, isPublished);
                //Response.Redirect(Request.Url.OriginalString);
                ShowMessage("", GetSageMessage("LanguageModule", "LanguageIsDisabled"), "", SageMessageType.Success);
            }
            ViewState["PortalLanguages"] = LocalizationSqlDataProvider.GetPortalLanguages(GetPortalID);

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }
    protected bool CheckIfResourceFileExists(string languageCode)
    {
        try
        {
            bool isExists = true;
            if (languageCode == "en-US")
            {
                isExists = true;
            }
            else
            {
                string[] files = Directory.GetFiles(Server.MapPath(ResolveUrl("~") + "Controls/App_LocalResources"));
                foreach (string file in files)
                {
                    if (file.IndexOf(languageCode) > 1)
                    {
                        isExists = true;
                        break;
                    }
                }

            }
            return isExists;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void gdvLangList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvLangList.PageIndex = e.NewPageIndex;
        GetLanguageList();
    }
    protected void imbCancelResxEdit_Click(object sender, EventArgs e)
    {
        ControlVisibility(true, false, false, false, false, false, false,false);
        GetLanguageList();
    }

    protected void ddlPageSize_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlPageSize.SelectedValue != "0")
        {
            gdvLangList.AllowPaging = true;
            gdvLangList.PageSize = int.Parse(ddlPageSize.SelectedValue);
            gdvLangList.PageIndex = 0;
        }
        else
        {
            gdvLangList.AllowPaging = false;
        }
        GetLanguageList();
    }
    public void LoadPagerDDL(int gridRowsCount)
    {
        ddlPageSize.Items.Clear();
        for (int i = 0; i < gridRowsCount; i += 10)
        {
            if (i == 0)
            {
                ddlPageSize.Items.Add(new ListItem("All", i.ToString(), true));
            }
            else
            {
                ddlPageSize.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
            }
        }
        ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue("10"));
    }

    public void LoadCurrentCulture()
    {
        lblCurrentCulture.Text = LocaleController.GetLanguageNameFromCode(GetCurrentCulture());
        imgFlagCurrentCulture.ImageUrl = ResolveUrl("~/images/flags/" + Regex.Replace(GetCurrentCulture(), "[a-z]{2}-", "").ToLower() + ".png");
    }

    #region Resource Editor
    public void InitializeTreeview()
    {
        if (Request.QueryString["d"] != null)
        {
            BindTree();
            tvList.Nodes[0].Selected = true;
        }
        else
        {
            BindTree();
        }
        GetFlagImage();
    }

    protected void GetFlagImage()
    {
        int index = CultureCode.IndexOf("-");
        string shortCode = CultureCode.Substring(index + 1);
        imgSelectedLang.ImageUrl = ResolveUrl("~/images/flags/" + shortCode.ToLower() + ".png");
        imgSelectedLang.AlternateText = shortCode;
        lblSelectedLanguage.Text = LocaleController.GetLanguageNameFromCode(CultureCode);

    }

    public string GetAbsolutePath(string filepath)
    {
        return (FileManagerHelper.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath.ToString(), filepath)));
    }

    private void BindTree()
    {
        tvList.Nodes.Clear();

        string adminFolder =BaseDir+"Sagin";
        TreeNode rootNodeAdmin = new TreeNode();
        rootNodeAdmin.SelectAction = TreeNodeSelectAction.Expand;
        rootNodeAdmin.Text = "Admin Default";
        rootNodeAdmin.Expanded = true;
        rootNodeAdmin.ImageUrl = "images/directory.png";
        rootNodeAdmin.Value = adminFolder.Replace("\\", "~").Replace(" ", "|");
        tvList.Nodes.Add(rootNodeAdmin);
        tvList.ShowLines = false;
        BuildTreeDirectory(adminFolder, rootNodeAdmin, true);

        string controlFolder = controlsDir;
        TreeNode rootNode = new TreeNode();
        rootNode.SelectAction = TreeNodeSelectAction.Expand;
        rootNode.Text = "Root Local Resources";
        rootNode.Expanded = true;
        rootNode.ImageUrl = "images/directory.png";
        rootNode.Value = controlFolder.Replace("\\", "~").Replace(" ", "|");
        tvList.Nodes.Add(rootNode);
        tvList.ShowLines = false;
        BuildTreeDirectory(controlFolder, rootNode,true);

        string ModuleFolder = ModuleDir;
        TreeNode rootModuleNode = new TreeNode();
        rootModuleNode.SelectAction = TreeNodeSelectAction.Expand;
        rootModuleNode.Text = "Module Local Resources";
        rootModuleNode.Expanded = true;
        rootModuleNode.ImageUrl = "images/directory.png";
        rootModuleNode.Value = ModuleFolder.Replace("\\", "~").Replace(" ", "|");
        tvList.Nodes.Add(rootModuleNode);
        tvList.ShowLines = false;
        BuildTreeDirectory(ModuleFolder, rootModuleNode,false);

       string rootFolder = Path.Combine(GetAbsolutePath(HttpContext.Current.Request.PhysicalApplicationPath.ToString()), "XMLMessage");
        TreeNode rootNodeGlobal2 = new TreeNode();
        rootNodeGlobal2.SelectAction = TreeNodeSelectAction.Expand;
        rootNodeGlobal2.Text = "XML Resources";
        rootNodeGlobal2.Expanded = true;
        rootNodeGlobal2.ImageUrl = "images/directory.png";
        rootNodeGlobal2.Value = rootFolder.Replace("\\", "~").Replace(" ", "|");
        tvList.Nodes.Add(rootNodeGlobal2);
        tvList.ShowLines = false;
        BuildTreeDirectory(rootFolder, rootNodeGlobal2,false);

        rootFolder = Path.Combine(GetAbsolutePath(HttpContext.Current.Request.PhysicalApplicationPath.ToString()), "js/SystemLocale");

        TreeNode rootNodeGlobal3 = new TreeNode();
        rootNodeGlobal3.SelectAction = TreeNodeSelectAction.Expand;
        rootNodeGlobal3.Text = "Client Resources";
        rootNodeGlobal3.Expanded = true;
        rootNodeGlobal3.ImageUrl = "images/directory.png";
        rootNodeGlobal3.Value = rootFolder.Replace("\\", "~").Replace(" ", "|");
        tvList.Nodes.Add(rootNodeGlobal3);
        tvList.ShowLines = false;
        BuildTreeDirectory(rootFolder, rootNodeGlobal3,false);




    }

    private void BuildTreeDirectory(string dirPath, TreeNode parentNode,bool isControl)
    {
        string[] subDirectories = Directory.GetDirectories(dirPath);

        AddFiles(dirPath, ref parentNode);


        foreach (string directory in subDirectories)
        {
            string[] parts = directory.Split('\\');
            string name = parts[parts.Length - 1];
            TreeNode node = new TreeNode();
            node.SelectAction = TreeNodeSelectAction.Expand;
            node.Text = name;
            node.ImageUrl = "images/directory.png";
            node.Expanded = false;
            DirectoryInfo dir = new DirectoryInfo(directory);
            bool flag = false;
            IsValidateTraverse(dir, "App_LocalResources", ref flag);

            if (flag || name.Equals("App_LocalResources"))
            {
                parentNode.ChildNodes.Add(node);
            }
            else
            {
                IsValidateTraverse(dir, "Language", ref flag);
                if (flag || name.Equals("Language"))
                {
                    parentNode.ChildNodes.Add(node);
                }
            }
            BuildSubDirectory(directory, node,isControl);
        }
    }

    private void BuildSubDirectory(string dirPath, TreeNode parentNode,bool isControl)
    {
        string[] subDirectories = Directory.GetDirectories(dirPath);
        if (isControl)
        {
            AddFiles(dirPath, ref parentNode);
        }
        else
        {
            foreach (string directory in subDirectories)
            {

            string[] parts = directory.Split('\\');
            string name = parts[parts.Length - 1];
            TreeNode node = new TreeNode();
            node.SelectAction = TreeNodeSelectAction.Expand;
            node.Text = name;
            node.ImageUrl = "images/directory.png";
            DirectoryInfo dir = new DirectoryInfo(directory);
            bool flag = false;
            IsValidateTraverse(dir, "App_LocalResources", ref flag);
            if (flag || name.Equals("App_LocalResources"))
            {
                parentNode.ChildNodes.Add(node);
            }
            else
            {
                IsValidateTraverse(dir, "Language", ref flag);
                if (flag || name.Equals("Language"))
                {
                    parentNode.ChildNodes.Add(node);
                }
            }
            node.Expanded = false;
            AddFiles(directory, ref node);
                BuildSubDirectory(directory, node, isControl);
            }
        }
    }

    public bool IsValidateTraverse(DirectoryInfo dir, string dirName, ref bool flag)
    {

        foreach (System.IO.DirectoryInfo child in dir.GetDirectories())
        {
            if (child.Name.Equals(dirName))
            {
                flag = true;
                break;
            }
            else
            {
                IsValidateTraverse(child, dirName, ref flag);
            }
        }
        return flag;
    }

    private void AddFiles(string dirpath, ref TreeNode parentNode)
    {
        foreach (string file in Directory.GetFiles(dirpath))
        {
            string[] parts = file.Split('\\');
            string name = parts[parts.Length - 1];

            TreeNode fileNode = new TreeNode();
            fileNode.ImageUrl = "images/resx.png";
            if (Path.GetExtension(name).Equals(".resx") || Path.GetExtension(name).Equals(".xml") || Path.GetExtension(name).Equals(".js"))
            {
                bool status = VerifyFile(ref name, Path.GetExtension(name));
                if (status)
                {
                    fileNode.Text = name;
                    parentNode.ChildNodes.Add(fileNode);
                    if (Path.GetExtension(name).Equals(".xml"))
                        fileNode.ImageUrl = "images/code.png";
                }
            }
            fileNode.Expanded = false;
        }
    }

    private List<string> GetInvalidFileFormats()
    {
        List<string> lstFiles = new List<string>();
        lstFiles.Add("TimeZones");
        lstFiles.Add("MessageToken");
        return lstFiles;
    }

    private bool VerifyFile(ref string filePath, string ext)
    {
        bool status = false;
        string actualFileName = Regex.Replace(filePath, @".[a-z]{2}-[A-Z]{2}" + ext + "|" + ext + "|." + @"[a-z]{2}-[A-Z]{1}[a-z]{3}-[A-Z]{2}" + ext, "").Trim();
        if (filePath == actualFileName + "." + CultureCode + ".resx" || filePath == actualFileName + "." + CultureCode + ".xml" || filePath == actualFileName + "." + CultureCode + ".js")
        {
            //localized file is present
            status = true;

        }
        else if (filePath == actualFileName + ".resx" || filePath == actualFileName + ".xml" || filePath == actualFileName + ".js")
        {
            //its the default file
            filePath += "[Default]";
            status = true;
        }
        foreach (string file in GetInvalidFileFormats())
        {
            if (file.Equals(actualFileName))
            {
                status = false;
            }
        }
        return status;

    }

    protected void tvList_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (Path.HasExtension(tvList.SelectedNode.Value))
         {
            languageContent.Visible = true;
             controlButtons.Visible = true;
        }
        string[] parts = tvList.SelectedNode.Value.Replace("[Default]", "").ToString().Split('\\');
        string filePath = tvList.SelectedNode.ValuePath.Replace("[Default]", "").ToString();
        filePath = filePath.Replace('|', ' ');
        List<ResourceDefinition> lstResDef = new List<ResourceDefinition>();
        try
        {
            if (Path.GetExtension(filePath) == ".resx")
            {
                lstResDef = ReadResourceFile(filePath);
            }
            else if (Path.GetExtension(filePath) == ".xml")
            {
                lstResDef = ReadXMLResource(filePath);
            }
            else
            {
                lstResDef = ReadJavaScriptResource(filePath);
            }
            //   lstResDef = Path.GetExtension(filePath) == ".resx" ? ReadResourceFile(filePath) : ReadXMLResource(filePath);
        }
        catch (Exception ex)
        {

            ShowMessage("", GetSageMessage("LanguageModule", "ResourceFileCouldNotBeRead"), "", SageMessageType.Error);
            
            throw ex;
        }


        SetFolderLabel(tvList.SelectedNode.ValuePath.ToString().Replace("/", "\\").Replace("[Default]", ""), '\\');


        gdvResxKeyValue.DataSource = lstResDef;
        gdvResxKeyValue.DataBind();

        imbDeleteResxFile.Visible = true;
        imbUpdate.Visible = true;
        
    }

    public List<ResourceDefinition> ReadJavaScriptResource(string filePath)
    {
        string inputString;

        List<ResourceDefinition> lstResDef = new List<ResourceDefinition>();

        using (StreamReader streamReader = File.OpenText(filePath))
        {
            inputString = streamReader.ReadLine();
            while (inputString != null)
            {

                string regexPattern = "(\"(?<key>[^\"]+)\"\\s*:\\s*\"(?<value>[^\"]+)\")|(\'(?<key>[^\']+)\'\\s*:\\s*\'(?<value>[^\']+)\')";
                Regex regex = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);
                if (regex.IsMatch(inputString))
                {
                    Match match = regex.Match(inputString);

                    string key = match.Groups[3].Value.Trim();
                    string value = match.Groups[4].Value.Trim();
                    ResourceDefinition resDef = new ResourceDefinition();
                    resDef.Key = key;
                    resDef.Value = value;
                    lstResDef.Add(resDef);
                }

                inputString = streamReader.ReadLine();
            }

        }

        return lstResDef;
    }

    public void SetFolderLabel(string filePath, char delimiter)
    {
        string[] parts = filePath.Split(delimiter);
        string fileName = parts[parts.Length - 1];
        filePath = filePath.Replace(fileName, "");
        string folderName = parts.ToString();
        this.lblSelectedFolder.Text = delimiter == '/' ? filePath.Replace(LocalizationHelper.ReplaceBackSlash(Request.PhysicalApplicationPath.ToString()), "") : filePath.Replace(Request.PhysicalApplicationPath.ToString(), "");
        this.lblSelectedFile.Text = fileName;
    }

    public List<ResourceDefinition> ReadResourceFile(string filePath)
    {
        string defaultFile = filePath;
        defaultFile = LocalizationHelper.GetDefaultFilePath(filePath);

        Dictionary<string, string> resxDict = new Dictionary<string, string>();
        Dictionary<string, string> resxDictDef = new Dictionary<string, string>();

        if (resxDictDef.Count == 0)
        {
            //this.divNoData.Visible = true;
        }
        else if (resxDictDef.Count > 0)
        {

            //this.divNoData.Visible = false;
        }
        if (File.Exists(filePath))
        {
            resxDict = ResXHelper.ReadResX(filePath);
            resxDictDef = ResXHelper.ReadResX(defaultFile);
        }
        else
        {
            resxDictDef = ResXHelper.ReadResX(defaultFile);
        }
        List<ResourceDefinition> resxList = new List<ResourceDefinition>();
        foreach (KeyValuePair<string, string> kvp in resxDict)
        {
            if (kvp.Value != "")
            {
                ResourceDefinition obj = new ResourceDefinition();
                obj.Key = kvp.Key;
                obj.Value = kvp.Value;
                resxList.Add(obj);
            }

        }
        List<ResourceDefinition> resxListFinal = new List<ResourceDefinition>();
        foreach (KeyValuePair<string, string> kvp in resxDictDef)
        {
            if (kvp.Value != "")
            {
                int index = resxList.FindIndex(
                    delegate(ResourceDefinition objDef)
                    {
                        return (objDef.Key == kvp.Key);
                    }
                    );
                if (index > -1)
                {
                    ResourceDefinition obj = new ResourceDefinition();
                    obj.Key = kvp.Key;
                    obj.Value = resxList[index].Value;
                    obj.DefaultValue = kvp.Value;
                    resxListFinal.Add(obj);
                }
            }
        }
        return resxListFinal;
    }

    public List<ResourceDefinition> ReadXMLResource(string filePath)
    {
        string defaultFile = filePath;
        defaultFile = LocalizationHelper.GetDefaultFilePath(filePath);

        List<ResourceDefinition> xmlList = new List<ResourceDefinition>();
        List<ResourceDefinition> xmlListDef = new List<ResourceDefinition>();
        List<ResourceDefinition> xmlListFinal = new List<ResourceDefinition>();


        if (File.Exists(filePath))
        {
            xmlList = ReadXml(filePath);
            xmlListDef = ReadXml(defaultFile);
        }
        else
        {
            xmlListDef = ReadXml(defaultFile);
        }
        List<ResourceDefinition> resxListFinal = new List<ResourceDefinition>();
        foreach (ResourceDefinition kvp in xmlListDef)
        {
            if (kvp.Value != "")
            {
                int index = xmlList.FindIndex(
                    delegate(ResourceDefinition objDef)
                    {
                        return (objDef.Key == kvp.Key);
                    }
                    );
                if (index > -1)
                {
                    ResourceDefinition obj = new ResourceDefinition();
                    obj.Key = kvp.Key;
                    obj.Value = xmlList[index].Value;
                    obj.DefaultValue = kvp.Value;
                    xmlListFinal.Add(obj);
                }
            }

        }
        return xmlListFinal;
    }

    public List<ResourceDefinition> ReadXml(string filePath)
    {
         List<ResourceDefinition> resxList = new List<ResourceDefinition>();
        string xmlFile = filePath;
        string defaultFile = LocalizationHelper.GetDefaultFilePath(filePath);

        if (File.Exists(xmlFile))
        {
            XmlDocument currentDocument = new XmlDocument();
            try
            {
                currentDocument.Load(xmlFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            XmlNodeList nodeList = currentDocument.DocumentElement.ChildNodes;
            IDictionary<string, string> keyValuePairList = new Dictionary<string, string>();
            foreach (XmlNode node in nodeList)
            {
                Displaychild(node, currentDocument.DocumentElement, keyValuePairList);
            }

            foreach (KeyValuePair<string, string> kvp in keyValuePairList)
            {

                ResourceDefinition obj = new ResourceDefinition();
                obj.Key = kvp.Key;
                obj.Value = kvp.Value.Replace("\"", "");
                resxList.Add(obj);

            }
        }
        return resxList;

    }

    public static string GetKey(XmlNode node, XmlElement rootnode)
    {
        StringBuilder sb = new StringBuilder();
        string path = "";
        if (node != null)
        {
            XmlNode root = (XmlNode)rootnode;
            while (node != root.ParentNode)
            {
                if (node.NodeType != XmlNodeType.Text)
                    path = node.Name + "|| " + path;
                node = node.ParentNode;
            }
        }
        return path.Substring(0, path.LastIndexOf("||"));
    }

    public static void Displaychild(XmlNode node, XmlElement rootnode, IDictionary<string, string> keyValuePairList)
    {
        if (!node.HasChildNodes || node.NodeType == XmlNodeType.Text)
        {
            keyValuePairList.Add(GetKey(node, rootnode), node.InnerText);
        }
        else
        {
            foreach (XmlNode n in node.ChildNodes)
            {
                Displaychild(n, rootnode, keyValuePairList);
            }
        }

    }

    protected void gdvResxKeyValue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
            if (lblSelectedFile.Text.ToString().Contains("TopStickyBar"))
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    HtmlGenericControl keyvalue = (HtmlGenericControl)e.Row.FindControl("keyvalue");
                    HtmlGenericControl li = (HtmlGenericControl)keyvalue.FindControl("defaultvalue");
                    if (li != null)
                    {
                        string text = li.InnerText.ToString().Trim();
                        if (text.Equals("wide") || text.Equals("fluid") || text.Equals("narrow"))
                        {
                            e.Row.Cells[1].Enabled = false;
                        }
                    }
                }
        }
      
    }

    protected void imbUpdate_Click(object sender, EventArgs e)
    {
        List<ResourceDefinition> lstResDef = new List<ResourceDefinition>();
        GetResxKeyValueList(ref lstResDef);
        string filePath = tvList.SelectedNode.ValuePath.Replace("[Default]", "").ToString();
        filePath = filePath.Replace('|', ' ');
        try
        {
            Save(filePath, lstResDef);
            if (filePath.EndsWith(".resx"))
            {
                ReadResourceFile(filePath);
            }
            else if (filePath.EndsWith(".xml"))
            {
                ReadXml(filePath);
            }
            else if (filePath.EndsWith(".js"))
            {
            }
            SetFolderLabel(ConstructLocalizedFilePath(filePath), '/');
            InitializeTreeview();
            ShowMessage("", GetSageMessage("LanguageModule", "ResourceFileSavedSuccessfully"), "", SageMessageType.Success);
            imbDeleteResxFile.Visible = false;
            imbUpdate.Visible = false;
           

        }
        catch (Exception ex)
        {

            ProcessException(ex);
        }


    }

    public void GetResxKeyValueList(ref List<ResourceDefinition> lstResDef)
    {
        foreach (GridViewRow row in gdvResxKeyValue.Rows)
        {
            ResourceDefinition obj = new ResourceDefinition();
            obj.Key = ((Label)row.FindControl("lblKey")).Text;
            obj.Value = ((TextBox)row.FindControl("txtResxValue")).Text;
            lstResDef.Add(obj);

        }
    }

    public string ConstructLocalizedFilePath(string filePath)
    {
        string defaultfile = filePath;
        string newfilepath = filePath;
        if (filePath.EndsWith(".resx"))
        {
            defaultfile = LocalizationHelper.GetDefaultFilePath(filePath).Replace(".resx", ""); ;
            newfilepath = defaultfile + "." + CultureCode + ".resx";
        }
        else if (filePath.EndsWith(".xml"))
        {
            defaultfile = LocalizationHelper.GetDefaultFilePath(filePath).Replace(".xml", ""); ;
            newfilepath = defaultfile + "." + CultureCode + ".xml";
        }
        else if (filePath.EndsWith(".js"))
        {
            defaultfile = LocalizationHelper.GetDefaultFilePath(filePath).Replace(".js", ""); ;
            newfilepath = defaultfile + "." + CultureCode + ".js";
        }

        return newfilepath;
    }

    public void Save(string filePath, List<ResourceDefinition> lstResDef)
    {
        string destinationPath = filePath;
        string newfilepath = "";
        string defaultfile = filePath;
        CultureCode = CultureCode == ""
                          ? hdnCultureCode.Value
                          : CultureCode;

        if (filePath.EndsWith(".resx"))
        {  SageFrameConfig sfConf = new SageFrameConfig();
        string portalCulture = sfConf.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalDefaultLanguage);

            defaultfile = LocalizationHelper.GetDefaultFilePath(filePath).Replace(".resx", "");
            if (CultureCode != "en-US")
                newfilepath = defaultfile + "." + CultureCode + ".resx";
            else
            {
                newfilepath = defaultfile + ".resx";
            }
        }
        else if (filePath.EndsWith(".xml"))
        {
            defaultfile = LocalizationHelper.GetDefaultFilePath(filePath).Replace(".xml", ""); ;
            if (CultureCode != "en-US")
                newfilepath = defaultfile + "." + CultureCode + ".xml";
            else
            {
                newfilepath = defaultfile + ".xml";
            }
           // newfilepath = defaultfile + "." + CultureCode + ".xml";
        }
        else if (filePath.EndsWith(".js"))
        {
            defaultfile = LocalizationHelper.GetDefaultFilePath(filePath).Replace(".js", ""); ;
            if (CultureCode != "en-US")
                newfilepath = defaultfile + "." + CultureCode + ".js";
            else
            {
                newfilepath = defaultfile + ".js";
            }
        }

        string fileName = newfilepath == destinationPath ? destinationPath : newfilepath;
        if (newfilepath == destinationPath && File.Exists(destinationPath))
        {
            File.Delete(destinationPath);
        }

        if (filePath.EndsWith(".xml"))
        {
            CreateXmlFile(fileName, lstResDef);
        }
        else if (filePath.EndsWith(".resx"))
        {
            ResXHelper.WriteResX(fileName, lstResDef);
        }
        else if (filePath.EndsWith(".js"))
        {
            CreateLocalizedJavascriptFile(fileName, defaultfile + ".js", lstResDef);
        }

    }
    public void CreateLocalizedJavascriptFile(string filePath, string defaultFile, List<ResourceDefinition> lstResDef)
    {
        try
        {
            string inputString = string.Empty;
            FileStream fs = File.Create(filePath);

            fs.Close();
            int count = 0;

            StreamWriter sw = new StreamWriter(filePath);

            using (StreamReader streamReader = File.OpenText(defaultFile))
            {

                inputString = streamReader.ReadLine();
                while (inputString != null)
                {

                    string regexPattern = "(\"(?<key>[^\"]+)\"\\s*:\\s*\"(?<value>[^\"]+)\")|(\'(?<key>[^\']+)\'\\s*:\\s*\'(?<value>[^\']+)\')";
                    Regex regex = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);
                    if (regex.IsMatch(inputString))
                    {
                        Match match = regex.Match(inputString);

                        string key = match.Groups[3].Value.Trim();
                        string value = match.Groups[4].Value.Trim();

                        ResourceDefinition resDef = lstResDef[count];

                        if (inputString.Trim().EndsWith(","))
                        {
                            sw.WriteLine('"' + key.Trim() + '"' + ":" + '"' + resDef.Value.Trim() + '"' + ",");
                        }
                        else
                        {
                            sw.WriteLine('"' + key.Trim() + '"' + ":" + '"' + resDef.Value.Trim() + '"');
                        }

                        //lstResDef.Add(resDef);
                        count++;
                    }
                    else
                    {
                        sw.WriteLine(inputString);
                    }
                    inputString = streamReader.ReadLine();
                }

            }
            sw.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void CreateXmlFile(string filePath, List<ResourceDefinition> keyValuePairList)
    {
        ResourceDefinition kvp = keyValuePairList[0];
        string[] baseXml = kvp.Key.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

        List<string> list = baseXml.ToList();

        XmlDocument doc = new XmlDocument();
        XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(decl);
        XmlElement root = doc.CreateElement(list[0]);
        doc.AppendChild(root);

        foreach (ResourceDefinition entry in keyValuePairList)
        {
            string key = Regex.Replace(entry.Key, @"\s", "");
            baseXml = key.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> listobj = baseXml.ToList();

            string xpath = GetXpath(listobj);

            //find parent node to insert
            XmlNodeList nodeList = null;
            int depth = listobj.Count;
            foreach (string s in listobj)
            {
                nodeList = doc.SelectNodes("//" + xpath);
                xpath = xpath.Substring(0, xpath.LastIndexOf("/") > 0 ? xpath.LastIndexOf("/") : xpath.Length);
                depth--;
                if (nodeList.Count > 0) break;//found parent node to insert so break the loop
            }

            XmlElement foundNode = (XmlElement)nodeList[0];
            if (listobj.Count > 0 && foundNode != null)
            {
                listobj.RemoveRange(0, depth + 1);
                XmlElement childElement = GetChildNode(listobj, doc, entry.Value);

                foundNode.AppendChild(childElement);
            }
        }
        doc.Save(filePath);
    }

    public static string GetXpath(List<string> list)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            sb.Append(list[i] + "/");
        }
        string xpath = sb.ToString();
        return xpath.Substring(0, xpath.LastIndexOf("/"));
    }

    public static XmlElement GetChildNode(List<string> listobj, XmlDocument doc, string textValue)
    {
        XmlElement parentNode = doc.CreateElement(listobj[0]);
        XmlElement tmpNode = null;

        if (listobj.Count < 2)
        {
            parentNode.AppendChild(doc.CreateTextNode(textValue));
        }
        else
        {
            for (int j = 1; j < listobj.Count; j++)
            {
                tmpNode = parentNode.HasChildNodes ? (XmlElement)parentNode.LastChild : parentNode;
                XmlElement element = doc.CreateElement(listobj[j]);

                tmpNode.AppendChild(element);
            }

            tmpNode.LastChild.AppendChild(doc.CreateTextNode(textValue));
        }

        return parentNode;
    }

    protected void imbCancel_Click(object sender, EventArgs e)
    {
        ControlVisibility(true, false, false, false, false, false, false,false);
    }

    protected void imbDeleteResxFile_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string filePath = tvList.SelectedNode.ValuePath.Replace("[Default]", "").ToString();
            filePath = filePath.Replace('|', ' ');
            if (!LocalizationHelper.IsDefaultFile(filePath))
            {
                DeleteFile(tvList.SelectedNode.ValuePath.Replace("[Default]", "").ToString());
                ShowMessage("", GetSageMessage("LanguageModule", "ResourceFileDeletedSuccessfully"), "", SageMessageType.Success);
                languageContent.Visible = false;
                InitializeTreeview();
                string defaultFilePath = LocalizationHelper.GetDefaultFilePath(filePath);
                if (defaultFilePath.EndsWith(".resx"))
                {
                    ReadResourceFile(defaultFilePath);
                }
                else if (defaultFilePath.EndsWith(".xml"))
                {
                    ReadXml(defaultFilePath);
                }

                SetFolderLabel(ConstructLocalizedFilePath(defaultFilePath), '/');
                imbDeleteResxFile.Visible = false;
                languageContent.Visible = false;
                imbUpdate.Visible = false;

            }
            else
            {
                ShowMessage("", GetSageMessage("LanguageModule", "DefaultFileCannotBeDeleted"), "", SageMessageType.Alert);

            }

        }
        catch (Exception ex)
        {

            ProcessException(ex);
        }
    }

    public static void DeleteFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

    }

    #endregion
    protected void imbLocalizeMenu_Click(object sender, EventArgs e)
    {
        ControlVisibility(false, false, false, false, false, false, true,false);
        UpdateLocalizeMenuFields();
    }

    protected void imbLocalizeModuleTitle_Click(object sender, EventArgs e)
    {
        ControlVisibility(false, false, false, false, false, false, false,true);
        UpdateModuleTitleFields();
    }

    protected void UpdateModuleTitleFields()
    {
        Modules_Language_LocalModuleTitle lmt = (Modules_Language_LocalModuleTitle)this.FindControl("ctrl_ModuleTitleEditor");
        DropDownList ddlAvailableLocales = (DropDownList)lmt.FindControl("ddlAvailableLocales");
        GridView gdvLocalModuleTitle = (GridView)lmt.FindControl("gdvLocalModuleTitle");
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

        List<LocalModuleInfo> lstPages = LocaleController.GetLocalModuleTitle(GetPortalID, ddlAvailableLocales.SelectedValue.ToString());
        gdvLocalModuleTitle.DataSource = lstPages;
        gdvLocalModuleTitle.DataBind();
    }
}
