#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Common;
using SageFrame.Templating;
using System.IO;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Threading;
using SageFrame.Web.Utilities;
using SageFrame.CDN;
using SageFrame.Web;

#endregion

namespace SageFrame.Core
{
    [Serializable]
    public class CoreJs
    {
        /// <summary>
        /// Get the list of Core js files to be included by default
        /// </summary>
        /// <param name="IsAdmin">Include only those files required for the admin mode i.e. the dashboard</param>
        /// <param name="IsUserLoggedIn">Scipts required for the top sticky bar and the edit buttons and popup</param>
        /// <returns>List of scripts to be included</returns>
        public static List<CssScriptInfo> GetList(bool IsAdmin, bool IsUserLoggedIn, bool IsHandheld, int PortalID)
        {
            List<CssScriptInfo> jsList = new List<CssScriptInfo>();
            SageFrameConfig sageConfig = new SageFrameConfig();
            bool EnableCDN = sageConfig.GetSettingBollByKey(SageFrameSettingKeys.EnableCDN);
            if (EnableCDN)
            {
                CDNController objCDN = new CDNController();
                List<CDNInfo> obj = objCDN.GetCDNLinks(PortalID);
                // public CssScriptInfo(string _ModuleName,  string _Path, int _Position)
                List<CssScriptInfo> lstJS = new List<CssScriptInfo>();
                int GetCDNPosition = 0;
                foreach (CDNInfo objInfo in obj)
                {
                    if (GetCDNPosition < 2)

                        lstJS.Add(new CssScriptInfo("Core", objInfo.URL, 0, true, objInfo.IsJS));

                    else
                        lstJS.Add(new CssScriptInfo("Core", objInfo.URL, objInfo.URLOrder, true, objInfo.IsJS));

                    GetCDNPosition++;
                    if (GetCDNPosition == 2)
                    {
                        if (!IsHandheld)
                        {
                            lstJS.Add(new CssScriptInfo("Core", "~/js/SageFrameCorejs/sageframecore.min.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/js/JSON2.min.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/js/default.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/jQuery-browser.js", 0, true, true));
                        }
                        if (IsAdmin && IsUserLoggedIn)//Mode=1
                        {

                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/jquery.tooltip.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/js/superfish.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/jquery.uniform.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/dashboardjs.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/saginjs.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/sidebar_accordian.js", 1, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/jquery.jcarousel.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/jquery.qtip-1.0.0-rc3.js", 1, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/js/jquery.dialogextend.js", 1, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/message.js", 1, true, true));


                        }
                        if (!IsAdmin && IsUserLoggedIn)//Mode=2
                        {
                            //lstJS.Add(new CssScriptInfo("Core", "jquery-ui-1.8.14.custom.min.js", "/js/jquery-ui-1.8.14.custom/js/", 0,false));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/max-z-index.js", 1, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/jquery.tooltip.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/dashboardjs.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/saginjs.js", 0, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/js/jquery.dialogextend.js", 1, true, true));
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/message.js", 1, true, true));//added by shyam
                            lstJS.Add(new CssScriptInfo("Core", "~/Administrator/Templates/Default/js/jquery.uniform.js", 0, true, true));
                        }

                        jsList = lstJS;

                    }
                }

                jsList = lstJS;
            }
            else
            {
                if (!IsHandheld)
                {
                    //Mode=0
                    List<CssScriptInfo> lstJS = new List<CssScriptInfo>{                
                                                    //new CssScriptInfo("Core","jquery-1.9.1.js","/js/",0,false),                                                    
                                                   // new CssScriptInfo("Core", "dashboardjs.js", "/Administrator/Templates/Default/js/", 0, false),
                                                    new CssScriptInfo("Core","sageframecore.min.js","/js/SageFrameCorejs/",0,false),
                                                    new CssScriptInfo("Core","JSON2.min.js","/js/",0,true) ,
                                                    new CssScriptInfo("Core", "default.js", "/js/", 1, true),
                                                    new CssScriptInfo("Core", "jquery-ui-1.10.3.custom.min.js", "/js/jquery-ui-1.8.14.custom/js/", 0, false),
                                                    new CssScriptInfo("Core", "jQuery-browser.js", "/Administrator/Templates/Default/js/", 0, false)
                                                    //,new CssScriptInfo("Core", "jquery-migrate.js", "/Administrator/Templates/Default/js/", 0, false)
                                };
                    if (IsAdmin && IsUserLoggedIn)//Mode=1
                    {
                        //lstJS.Add(new CssScriptInfo("Core", "jquery-ui-1.8.14.custom.min.js", "/js/jquery-ui-1.8.14.custom/js/", 0, false));
                        lstJS.Add(new CssScriptInfo("Core", "jquery.tooltip.js", "/Administrator/Templates/Default/js/", 0, false));
                        lstJS.Add(new CssScriptInfo("Core", "superfish.js", "/js/", 0, false));
                        lstJS.Add(new CssScriptInfo("Core", "jquery.uniform.js", "/Administrator/Templates/Default/js/", 0, false));
                        //lstJS.Add(new CssScriptInfo("Core", "dashboardjs.js", "/Administrator/Templates/Default/js/", 0, false));
                        lstJS.Add(new CssScriptInfo("Core", "saginjs.js", "/Administrator/Templates/Default/js/", 0, false));
                        lstJS.Add(new CssScriptInfo("Core", "sidebar_accordian.js", "/Administrator/Templates/Default/js/", 1, false));
                        lstJS.Add(new CssScriptInfo("Core", "jquery.jcarousel.js", "/Administrator/Templates/Default/js/", 0, false));
                        lstJS.Add(new CssScriptInfo("Core", "jquery.qtip-1.0.0-rc3.js", "/Administrator/Templates/Default/js/", 1, false));
                        lstJS.Add(new CssScriptInfo("Core", "jquery.dialogextend.js", "/js/", 1, true));
                        lstJS.Add(new CssScriptInfo("Core", "message.js", "/Administrator/Templates/Default/js/", 1, false));

                    }
                    else if (!IsAdmin && IsUserLoggedIn)//Mode=2
                    {
                        //lstJS.Add(new CssScriptInfo("Core", "jquery-ui-1.8.14.custom.min.js", "/js/jquery-ui-1.8.14.custom/js/", 0,false));
                        lstJS.Add(new CssScriptInfo("Core", "max-z-index.js", "/Administrator/Templates/Default/js/", 1));
                        lstJS.Add(new CssScriptInfo("Core", "jquery.tooltip.js", "/Administrator/Templates/Default/js/", 0, false));
                        // lstJS.Add(new CssScriptInfo("Core", "dashboardjs.js", "/Administrator/Templates/Default/js/", 0, false));
                        lstJS.Add(new CssScriptInfo("Core", "saginjs.js", "/Administrator/Templates/Default/js/", 0, false));
                        lstJS.Add(new CssScriptInfo("Core", "jquery.dialogextend.js", "/js/", 1, true));
                        lstJS.Add(new CssScriptInfo("Core", "message.js", "/Administrator/Templates/Default/js/", 1, false));//added by shyam
                        lstJS.Add(new CssScriptInfo("Core", "jquery.uniform.js", "/Administrator/Templates/Default/js/", 0, false));
                    }
                    else
                    {
                        lstJS.Add(new CssScriptInfo("Core", "jquery.dialogextend.js", "/js/", 1, true));
                    }
                    jsList = lstJS;
                }
                else
                {
                    List<CssScriptInfo> lstJS = new List<CssScriptInfo>{                
                                                    new CssScriptInfo("Core","jquery-1.7.js","/js/",0,false),   
                                                    new CssScriptInfo("Core", "jquery-ui-1.8.14.custom.min.js", "/js/jquery-ui-1.8.14.custom/js/", 0, false),
                                                    new CssScriptInfo("Core", "dashboardjs.js", "/Administrator/Templates/Default/js/", 0, false),
                                                    new CssScriptInfo("Core","sageframecore.min.js","/js/SageFrameCorejs/",0,false),
                                                    new CssScriptInfo("Core","JSON2.min.js","/js/",0,true),     
                                                    new CssScriptInfo("Core", "default.js", "/js/", 1, true),                                               
                                                    new CssScriptInfo("Core", "superfish.js", "/js/", 0,false),
                                                    new CssScriptInfo("Core", "jquery.tooltip.js", "/Administrator/Templates/Default/js/", 0, false),                                                    
                                                    new CssScriptInfo("Core", "saginjs.js", "/Administrator/Templates/Default/js/", 0, false),
                                                    new CssScriptInfo("Core", "sidebar_accordian.js", "/Administrator/Templates/Default/js/", 1, false),
                                                    new CssScriptInfo("Core", "jquery.jcarousel.js", "/Administrator/Templates/Default/js/", 0, false),
                                                    new CssScriptInfo("Core", "jquery.uniform.js", "/Administrator/Templates/Default/js/", 0, false),
                                                    new CssScriptInfo("Core", "jquery.qtip-1.0.0-rc3.js", "/Administrator/Templates/Default/js/", 1, false)
                
            };



                    jsList = lstJS;
                }


            }

            return jsList;
        }

        /// <summary>
        /// Returns the list of .js files for given template.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>List of template paths.</returns>
        public static List<CssScriptInfo> GetTemplateJs(string TemplateName)
        {
            string templatePath = TemplateName.ToLower().Equals("default") ? Utils.GetTemplatePath_Default(TemplateName) : Utils.GetTemplatePath(TemplateName);
            string templatePath_rel = TemplateName.ToLower().Equals("default") ? "/Core/Template/js/" : string.Format("/Templates/{0}/js/", TemplateName);
            string templateJsPath = string.Format("{0}/js", templatePath);
            List<CssScriptInfo> lstJs = new List<CssScriptInfo>();
            if (Directory.Exists(templateJsPath))
            {
                DirectoryInfo dirJs = new DirectoryInfo(templateJsPath);
                foreach (FileInfo js in dirJs.GetFiles())
                {
                    if (js.Extension.Equals("js") || js.Extension.Equals(".js"))
                    {
                        lstJs.Add(new CssScriptInfo("TemplateJs", js.Name, templatePath_rel, 1));
                    }
                }
            }
            lstJs.Sort(
                delegate(CssScriptInfo f1, CssScriptInfo f2)
                {
                    return f1.FileName.CompareTo(f2.FileName);
                }
            );
            return lstJs;

        }

        /// <summary>
        /// Writes the list of links of js to the responding page.
        /// </summary>
        /// <param name="sfPage">System.Web.UI.Page object</param>
        public static void IncludeLanguageCoreJs(Page sfPage)
        {
            Literal LitLangResc = sfPage.FindControl("LitLangResc") as Literal;
            string strScript = string.Empty;
            string langFolder = "~/js/SystemLocale/";
            if (Directory.Exists(HttpContext.Current.Server.MapPath(langFolder)))
            {
                bool isTrue = false;
                string[] fileList = Directory.GetFiles(HttpContext.Current.Server.MapPath(langFolder));
                string regexPattern = ".*\\\\(?<file>[^\\.]+)(\\.[a-z]{2}-[A-Z]{2})?\\.js";
                Regex regex = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);
                Match match = regex.Match(fileList[0]);
                string languageFile = match.Groups[2].Value;
                string FileUrl = string.Empty;
                isTrue = Thread.CurrentThread.CurrentCulture.ToString() == "en-US" ? true : false;
                if (isTrue)
                {
                    FileUrl = langFolder + languageFile + ".js";
                    // strScript = "<script src=\"" + ResolveUrl(FileUrl) + "\" type=\"text/javascript\"></script>";
                }
                else
                {
                    FileUrl = langFolder + languageFile + "." + Thread.CurrentThread.CurrentCulture.ToString() + ".js";
                    // strScript = "<script src=\"" + ResolveUrl(FileUrl) + "\" type=\"text/javascript\"></script>";
                }
                string inputString = string.Empty;
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type=\"text/javascript\">\n");

                if (File.Exists(HttpContext.Current.Server.MapPath(FileUrl)))
                {
                    using (StreamReader streamReader = File.OpenText(HttpContext.Current.Server.MapPath(FileUrl)))
                    {
                        inputString = streamReader.ReadLine();
                        while (inputString != null)
                        {
                            sb.Append(inputString + "\n");
                            inputString = streamReader.ReadLine();
                        }
                    }
                }
                else
                {
                    FileUrl = langFolder + languageFile + ".js";
                    using (StreamReader streamReader = File.OpenText(HttpContext.Current.Server.MapPath(FileUrl)))
                    {
                        inputString = streamReader.ReadLine();
                        while (inputString != null)
                        {
                            sb.Append(inputString + "\n");
                            inputString = streamReader.ReadLine();
                        }
                    }
                }
                sb.Append("</script>\n");
                if (!LitLangResc.Text.Contains(sb.ToString()))
                {
                    LitLangResc.Text += sb.ToString();
                }
            }
        }
    }
}
