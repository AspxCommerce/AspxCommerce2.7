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
using System.Text;
using SageFrame.Localization;
using SageFrame.Common;
#endregion

public partial class Modules_Language_LangSwitch : BaseAdministrationUserControl
{
    public string ContainerClientID = string.Empty;
    public string CultureCode = string.Empty;
    public int LangUserModuleID = 0, PortalID = 0, UserModuleID = 0;
    public string modulePath,switchType,dropDownType;
       
    protected void Page_Load(object sender, EventArgs e)
    {
        Initialize();
        PortalID = GetPortalID;       
        PortalID = GetPortalID;
        UserModuleID = int.Parse(SageUserModuleID);
        LangUserModuleID = int.Parse(SageUserModuleID);
        modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LanguageSwitchGlobalVariable1", " var LanguageSwitchFilePath='" + ResolveUrl(modulePath) + "';", true);
        string flagPath = ResolveUrl(Request.ApplicationPath);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LanguageSwitchGlobalVariable2", " var LanguageSwitchFlagPath='" + ResolveUrl(flagPath) + "';", true);
        GetAvailableLanguages();
        GetLanguageSettings();
       // CreateDynamicNav();
    }

    public void Initialize()
    {
        IncludeCss("LanguageSwitch", "/Modules/LanguageSwitcher/css/carousel.css", "/Modules/LanguageSwitcher/css/module.css", "/Modules/LanguageSwitcher/css/dd.css");
        IncludeJs("LanguageSwitch", "/Modules/LanguageSwitcher/js/jquery.dd.js");//, "/Modules/LanguageSwitcher/js/jquery.tools.min.js"
        IncludeJs("LanguageSwitch", "/Modules/LanguageSwitcher/js/LangSwitch.js");
    }

    public void CreateDynamicNav()
    {
        ContainerClientID = "divNav_" + SageUserModuleID;
        StringBuilder sb = new StringBuilder();
        sb.Append("<div id='");
        sb.Append(ContainerClientID );
        sb.Append("'>");
        sb.Append("<div id='divFlagButton_");
        sb.Append(SageUserModuleID );
        sb.Append("' class='FlagButtonWrapper'> </div>");
        sb.Append("<div id='divFlagDDL_");
        sb.Append(SageUserModuleID );
        sb.Append("'> </div>");
        sb.Append("<div id='divPlainDDL_" );
        sb.Append(SageUserModuleID );
        sb.Append("'> </div>");
        sb.Append("<div id='carousel_container_");
        sb.Append(SageUserModuleID );
        sb.Append("' class='CssClassLanguageWrapper'>");
        sb.Append("<div class='CssClassLanguageWrapperInside' id='divLangWrap_");
        sb.Append(SageUserModuleID );
        sb.Append("'>");
        sb.Append("<div id='left_scroll_");
        sb.Append(SageUserModuleID );
        sb.Append("' class='cssClassLeftScroll'><img class='imgLeftScroll' /></div>");
        sb.Append("<div id='carousel_inner_");
        sb.Append(SageUserModuleID );
        sb.Append("' class='cssClassCarousel'>");
        sb.Append("<ul id='carousel_ul_");
        sb.Append(SageUserModuleID );
        sb.Append("'></ul></div>");
        sb.Append("<div id='right_scroll_" );
        sb.Append(SageUserModuleID );
        sb.Append("' class='cssClassRightScroll'><img class='imgRightScroll'/></div></div></div>");
        sb.Append("</div>");
        ltrNav.Text = sb.ToString();
    }

    private void GetAvailableLanguages()
    {
        List<Language> lstLanguage = LocalizationSqlDataProvider.GetPortalLanguages(PortalID);
        if (lstLanguage.Count < 1)
        {
            ltrNav.Visible = false;
        }
    }
    private void GetLanguageSettings()
    {
        List<LanguageSwitchKeyValue> lstLanguageSetting = LocaleController.GetLanguageSwitchSettings(PortalID,UserModuleID);
        LoadFlags(lstLanguageSetting);

 
    }
   
    private void LoadFlags(List<LanguageSwitchKeyValue> lstSetting)
    {
        List<Language> lstLanguage = LocaleController.AddNativeNamesToList(AddFlagPath(LocalizationSqlDataProvider.GetPortalLanguages(PortalID), GetApplicationName));
        BindFlags(lstLanguage, lstSetting);
    }
    public string GetCurrentCultureInfo()
    {
        if (HttpContext.Current.Session != null)
        {
            string code = HttpContext.Current.Session[SessionKeys.SageUICulture] == null ? "en-US" : HttpContext.Current.Session[SessionKeys.SageUICulture].ToString();
            return code;
        }
        return "en-US";
    }
    private List<Language> AddFlagPath(List<Language> lstAvailableLocales, string baseURL)
    {
        List<Language> filtered = new List<Language> { };
        string currentCulture = GetCurrentCultureInfo();
        foreach (Language li in lstAvailableLocales)
        {
            //if (li.LanguageCode != currentCulture)
            //{
            filtered.Add(li);
            //}
        }

        filtered.ForEach(
            delegate(Language obj)
            {
                obj.FlagPath = baseURL + "/images/flags/" + obj.LanguageCode.Substring(obj.LanguageCode.IndexOf("-") + 1).ToLower() + ".png";
            }
            );
        return filtered;
    }
    private void BindFlags(List<Language> lstLanguage, List<LanguageSwitchKeyValue> lstSetting)
    {
        if (lstSetting.Count > 0)
        {
            string _SwitchType = "";
            string _ListTypeFlags = "false";
            string _ListTypeName = "false";
            string _ListTypeBoth = "false";
            string _ListAlign = "H";
            string _EnableCarousel = "false";
            string _DropDownType = "Flag";            
            string cultureCode = GetCurrentCultureInfo();
            foreach (LanguageSwitchKeyValue item in lstSetting)
            {
                if (item.Key == "ListTypeBoth")
                {
                    _ListTypeBoth = item.Value;
                }
                if (item.Key == "ListTypeFlag")
                {
                    _ListTypeFlags = item.Value;
                }
                if (item.Key == "ListTypeName")
                {
                    _ListTypeName = item.Value;
                }
                if (item.Key == "ListAlign")
                {
                    _ListAlign = item.Value;
                }
                if (item.Key == "EnableCarousel")
                {
                    _EnableCarousel = item.Value;
                }
                if (item.Key == "SwitchType")
                {
                    _SwitchType = item.Value;
                }
                if (item.Key == "DropDownType")
                {
                    _DropDownType = item.Value;
                }
            }
            switchType = _SwitchType;
            dropDownType = _DropDownType;
            StringBuilder html = new StringBuilder();
            StringBuilder carousel = new StringBuilder();
            ContainerClientID = "divNav_" + SageUserModuleID;
            html.Append("<div id='");
            html.Append(ContainerClientID);
            html.Append("'>");
            if (_SwitchType.ToLower() == "list")
            {
                html.Append("<div id='divFlagButton_");
                html.Append(SageUserModuleID);
                html.Append("' class='FlagButtonWrapper'>");
                if (_ListAlign.ToLower() == "h")
                {
                    html.Append("<ul id='imgFlagButton_");
                    html.Append(UserModuleID);
                    html.Append("' class='defaultButtonClass'>");
                }
                else
                {
                    html.Append("<ul id='imgFlagButton_");
                    html.Append(UserModuleID);
                    html.Append("'>");
                }
                

                if (_EnableCarousel.ToLower() == "false")
                {
                    if (_ListTypeBoth.ToLower() == "true")
                    {
                        if (_ListAlign.ToLower() == "h")
                        {
                            foreach (Language item in lstLanguage)
                            {
                                if (item.LanguageCode == cultureCode)
                                {
                                    html.Append("<li class='cssClassFlagButtonHor cssClassSelectedFlag'  code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("'><img src='");
                                    html.Append(item.FlagPath);
                                    html.Append("' /><span>");
                                    html.Append(item.LanguageName);
                                    html.Append("</span></li>");
                                }
                                else
                                {
                                    html.Append("<li class='cssClassFlagButtonHor'  code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("'><img src='");
                                    html.Append(item.FlagPath);
                                    html.Append("' /><span>");
                                    html.Append(item.LanguageName);
                                    html.Append("</span></li>");
                                }
                            }
                        }
                        else
                        {
                            foreach (Language item in lstLanguage)
                            {
                                if (item.LanguageCode == cultureCode)
                                {

                                    html.Append("<li class='cssClassFlagButtonVer cssClassSelectedFlag'  code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("'><img src='");
                                    html.Append(item.FlagPath);
                                    html.Append("' /><span>");
                                    html.Append(item.LanguageName);
                                    html.Append("</span></li>");
                                }
                                else
                                {
                                    html.Append("<li class='cssClassFlagButtonVer'  code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("'><img src='");
                                    html.Append(item.FlagPath);
                                    html.Append("' /><span>");
                                    html.Append(item.LanguageName);
                                    html.Append("</span></li>");
                                }
                            }
                        }
                    }
                    else if (_ListTypeName.ToLower() == "true")
                    {
                        if (_ListAlign.ToLower() == "h")
                        {
                            foreach (Language item in lstLanguage)
                            {
                                if (item.LanguageCode == cultureCode)
                                {
                                    html.Append("<li class='cssClassLanguageName'  code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("'><span>");
                                    html.Append(item.LanguageN);
                                    html.Append("<b>(");
                                    html.Append(item.NativeName);
                                    html.Append(")</b></span></li>");
                                }
                                else
                                {

                                    html.Append("<li class='cssClassLanguageName' code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("' ><span>");
                                    html.Append(item.LanguageN);
                                    html.Append("<b>(");
                                    html.Append(item.NativeName);
                                    html.Append(")</b></span></li>");
                                }
                            }
                        }
                        else
                        {
                            foreach (Language item in lstLanguage)
                            {
                                if (item.LanguageCode == cultureCode)
                                {
                                    html.Append("<li class='cssClassSelectedFlag cssClassLanguageNameVer'  code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("'><span>");
                                    html.Append(item.LanguageN);
                                    html.Append("<b>(");
                                    html.Append(item.NativeName);
                                    html.Append(")</b></span></li>");
                                }
                                else
                                {
                                    html.Append("<li class='cssClassLanguageNameVer' code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("' ><span>");
                                    html.Append(item.LanguageN);
                                    html.Append("<b>(");
                                    html.Append(item.NativeName);
                                    html.Append(")</b></span></li>");
                                }
                            }
                        }
                    }
                    else if (_ListTypeFlags.ToLower() == "true")
                    {
                        if (_ListAlign.ToLower() == "h")
                        {
                            foreach (Language item in lstLanguage)
                            {
                                if (item.LanguageCode == cultureCode)
                                {
                                    html.Append("<li class='cssClassSelectedFlag cssClassLanguageFlag flag' title=\"");
                                    html.Append(item.LanguageName);
                                    html.Append("\" code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("'><img src='");
                                    html.Append(item.FlagPath);
                                    html.Append("' /></li>");
                                }
                                else
                                {
                                    html.Append("<li class='cssClassLanguageFlag flag'  code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("' title=\"");
                                    html.Append(item.LanguageName);
                                    html.Append("\"><img src='");
                                    html.Append(item.FlagPath);
                                    html.Append("' /></li>");
                                }
                            }
                        }
                        else
                        {
                            foreach (Language item in lstLanguage)
                            {
                                if (item.LanguageCode == cultureCode)
                                {
                                    html.Append("<li class='cssClassSelectedFlag cssClassLanguageFlagVer flag' title=\"");
                                    html.Append(item.LanguageName);
                                    html.Append("\" code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("'><img src='");
                                    html.Append(item.FlagPath);
                                    html.Append("' /></li>");
                                }
                                else
                                {
                                    html.Append("<li class='cssClassLanguageFlagVer flag'  code='");
                                    html.Append(item.LanguageCode);
                                    html.Append("' title='");
                                    html.Append(item.LanguageName);
                                    html.Append("'><img src='");
                                    html.Append(item.FlagPath);
                                    html.Append("' /></li>");
                                }
                            }
                        }
                    }                    
                }
                else if (_EnableCarousel.ToLower() == "true")
                {                   
                    carousel.Append("<div id='carousel_container_");
                    carousel.Append(SageUserModuleID);
                    carousel.Append("' class='CssClassLanguageWrapper'>");
                    carousel.Append("<div class='CssClassLanguageWrapperInside' id='divLangWrap_");
                    carousel.Append(SageUserModuleID);
                    carousel.Append("'>");
                    carousel.Append("<div id='left_scroll_");
                    carousel.Append(SageUserModuleID);
                    carousel.Append("' class='cssClassLeftScroll'><img class='imgLeftScroll' /></div>");
                    carousel.Append("<div id='carousel_inner_");
                    carousel.Append(SageUserModuleID);
                    carousel.Append("' class='cssClassCarousel'>");
                    carousel.Append("<ul id='carousel_ul_");
                    carousel.Append(SageUserModuleID);
                    carousel.Append("'>");                   
                    foreach (Language item in lstLanguage)
                    {
                        if (item.LanguageCode == cultureCode)
                        {
                            carousel.Append("<li class='cssClassSelectedFlag' title=\"");
                            carousel.Append(item.LanguageName);
                            carousel.Append("\"><a href='#' code='");
                            carousel.Append(item.LanguageCode);
                            carousel.Append("'><img src='");
                            carousel.Append(item.FlagPath);
                            carousel.Append("' /></a></li>");                            
                        }
                        else
                        {
                            carousel.Append("<li title=\"");
                            carousel.Append(item.LanguageName);
                            carousel.Append("\" ><a href='#' code='");
                            carousel.Append(item.LanguageCode);
                            carousel.Append("'><img src='");
                            carousel.Append(item.FlagPath);
                            carousel.Append("' /></a></li>");
                        }
                    }
                    carousel.Append("</ul></div>");
                    carousel.Append("<div id='right_scroll_");
                    carousel.Append(SageUserModuleID);
                    carousel.Append("' class='cssClassRightScroll'><img class='imgRightScroll'/></div></div></div>");
                }
            }
            html.Append("</ul>");          

            if (_EnableCarousel.ToLower() == "true")
            {
                html.Append(carousel.ToString());                
            }
            else if (_SwitchType.ToLower() == "dropdown")
            {
               
                StringBuilder ddl = new StringBuilder();
                if (_DropDownType.ToLower() == "normal")
                {
                    html.Append("<div id='divPlainDDL_");
                    html.Append(SageUserModuleID);
                    html.Append("'>");
                    ddl.Append("<select id='ddlLocales_");
                    ddl.Append(UserModuleID);
                    ddl.Append("'>");
                    foreach (Language item in lstLanguage)
                    {
                        if (item.LanguageCode == cultureCode)
                        {
                            ddl.Append("<option selected='selected'>");
                            ddl.Append(item.LanguageCode);
                            ddl.Append("</option>");
                        }
                        else
                        {
                            ddl.Append("<option>");
                            ddl.Append(item.LanguageCode);
                            ddl.Append("</option>");
                        }
                    }
                    ddl.Append("</select>");
                    html.Append(ddl.ToString());                   
                }
                else if (_DropDownType.ToLower() == "flag")
                {
                    html.Append("<div id='divFlagDDL_");
                    html.Append(SageUserModuleID);
                    html.Append("'>");                   
                    ddl.Append("<select id='ddlFlaggedLocales_");
                    ddl.Append(UserModuleID);
                    ddl.Append("'>");
                    foreach (Language item in lstLanguage)
                    {
                        if (item.LanguageCode == cultureCode)
                        {
                            ddl.Append("<option  title='");
                            ddl.Append(item.FlagPath);
                            ddl.Append("' selected='selected'  value='");
                            ddl.Append(item.LanguageCode);
                            ddl.Append("' >");
                            ddl.Append(item.LanguageCode);
                            ddl.Append("</option>");
                        }
                        else
                        {
                            ddl.Append("<option title='");
                            ddl.Append(item.FlagPath);
                            ddl.Append("'  value='");
                            ddl.Append(item.LanguageCode);
                            ddl.Append("' >");
                            ddl.Append(item.LanguageCode);
                            ddl.Append("</option>");
                        }
                    }
                    ddl.Append("</select>");
                    html.Append(ddl.ToString());                    
                }
            }
           
            html.Append("</div>");
            html.Append("</div>");
            ltrNav.Text = html.ToString();
        }
                           
    }     
    
}


   




