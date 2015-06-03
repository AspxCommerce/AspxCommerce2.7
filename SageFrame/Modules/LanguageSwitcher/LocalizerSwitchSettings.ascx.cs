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
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Collections;
#endregion 

public partial class Modules_Language_LocalizerSwitchSettings :BaseAdministrationUserControl
{
    public string ImagePath = string.Empty;
    public int UserModuleID = 0;
    public int PortalID = 0;
    public string appPath = string.Empty;
    public void Initialize()
    {        
       IncludeJs("LanguageSwitch","/Modules/LanguageSwitcher/js/jquery.dd.js");
       IncludeCss("LanguageSwitch", "/Modules/LanguageSwitcher/css/carousel.css");
       IncludeCss("LanguageSwitch", "/Modules/LanguageSwitcher/css/module.css");
       IncludeCss("LanguageSwitch", "/Modules/LanguageSwitcher/css/dd.css");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        appPath = GetApplicationName;
        Initialize();
        PortalID = GetPortalID;
        UserModuleID = int.Parse(SageUserModuleID);
        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);        
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LocalizationLangSwitchGlobalVariable1", " var LanguageSwitchSettingFilepath='" + ResolveUrl(modulePath) + "';", true);
        ImagePath = ResolveUrl(modulePath);       
    }
}
