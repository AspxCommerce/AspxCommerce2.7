using System.Collections;
using System.Text.RegularExpressions;
using System.Web.UI;
using SageFrame.Common;
using SageFrame.Framework;
using SageFrame.Shared;
using SageFrame.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using SageFrame.Web.Utilities;
namespace SageFrame.Core
{
    public class SecurePage : IHttpModule
    {
        public SecurePage() { }

        #region IHttpModule Members

        public void Dispose()
        {

            //throw new NotImplementedException();

        }



        public void Init(HttpApplication context)
        {

            context.BeginRequest += new EventHandler(context_BeginRequest);

        }



        void context_BeginRequest(object sender, EventArgs e)
        {
            string ext = SageFrameSettingKeys.PageExtension;

            HttpApplication AppObject = (HttpApplication)sender;

            if (!(AppObject.Request.CurrentExecutionFilePath.Contains("Templates/") ||

                     AppObject.Request.CurrentExecutionFilePath.Contains(".axd") ||
                      AppObject.Request.CurrentExecutionFilePath.Contains(".ashx") ||
                        AppObject.Request.CurrentExecutionFilePath.Contains(".asmx") ||
                          AppObject.Request.CurrentExecutionFilePath.Contains(".svc") ||
                AppObject.Request.CurrentExecutionFilePath.Contains("fonts") ||
                AppObject.Request.CurrentExecutionFilePath.Contains(".gif") ||
                AppObject.Request.CurrentExecutionFilePath.Contains(".jpg") ||
                AppObject.Request.CurrentExecutionFilePath.Contains(".js") ||
                  AppObject.Request.CurrentExecutionFilePath.Contains(".css") ||
                AppObject.Request.CurrentExecutionFilePath.Contains(".png")))
            {

                  ApplicationController objAppController = new ApplicationController();
                if (objAppController.IsInstalled())
                {

                    SageFrameConfig sfConf = new SageFrameConfig();
                    var _useSsl = sfConf.GetSettingValueByIndividualKey(SageFrameSettingKeys.UseSSL);
                    bool useSSL = string.IsNullOrEmpty(_useSsl) ? false : bool.Parse(_useSsl.ToString().ToLower());
                    string serverName = HttpUtility.UrlEncode(AppObject.Request.ServerVariables["SERVER_NAME"]);

                    string filePath = AppObject.Request.FilePath;
                    string query = AppObject.Request.Url.Query;
                    int portNo = AppObject.Request.Url.Port;
                    string redirectionUrl = "";
                    string pagename = Path.GetFileName(filePath);

                    pagename = pagename == "" ? "Home" : pagename;

                    if (pagename.ToLower().Contains("loadcontrolhandler"))
                        return;
                    if (!AppObject.Request.IsSecureConnection)
                    {
                        if (useSSL)
                        {
                            redirectionUrl = "https://" + serverName + ":" + filePath + query;
                            AppObject.Response.Redirect(redirectionUrl);
                            return;
                        }

                        // send user to SSL 
                        if (portNo != 80)
                            redirectionUrl = "https://" + serverName + ":" + portNo + filePath + query;
                        else
                            redirectionUrl = "https://" + serverName + filePath + query;

                        if (checkIsPageSecure(pagename))
                            AppObject.Response.Redirect(redirectionUrl);

                    }
                    else
                    {
                        if (useSSL)
                            return;

                        if (portNo != 443)
                            redirectionUrl = "http://" + serverName + ":" + portNo + filePath + query;
                        else
                            redirectionUrl = "http://" + serverName + filePath + query;
                        if (!checkIsPageSecure(pagename))
                            AppObject.Response.Redirect(redirectionUrl);
                    }
                }
            }


        }

        private bool checkIsPageSecure(string pageName)
        {
            int portalId = GetPortalID;
            pageName = Path.GetFileNameWithoutExtension(pageName);
            return IsPageSecure(portalId, pageName); // pageName.Contains("Login");
        }

        /// <summary>
        /// Get PortalID And Respective Name List
        /// </summary>
        /// <returns>PortalID And PortalName</returns>
        private Hashtable GetPortals()
        {
            Hashtable hstAll = new Hashtable();
            if (HttpRuntime.Cache[CacheKeys.Portals] != null)
            {
                hstAll = (Hashtable)HttpRuntime.Cache[CacheKeys.Portals];
            }
            else
            {
                SettingProvider objSP = new SettingProvider();
                List<SagePortals> sagePortals = objSP.PortalGetList();
                foreach (SagePortals portal in sagePortals)
                {
                    hstAll.Add(portal.SEOName.Replace("https://", "").Replace("http://", "").ToLower().Trim(), portal.PortalID);
                }
            }
            HttpRuntime.Cache.Insert(CacheKeys.Portals, hstAll);
            return hstAll;
        }
        public int GetPortalID
        {
            get
            {
                try
                {
                    Hashtable hstPortals = GetPortals();
                    string URL = HttpContext.Current.Request.Url.ToString();
                    if (URL.Contains("/portal/"))
                    {
                        var RegMatch = Regex.Matches(URL, @"^http[s]?\s*:.*\/portal\/([^/]+)+\/.*");
                        string PortalName = "";
                        foreach (Match match in RegMatch)
                        {
                            PortalName = match.Groups[1].Value;
                        }
                        int PortalID = Int32.Parse(hstPortals[PortalName].ToString());
                        // HttpContext.Current.Session[SessionKeys.SageFrame_PortalID] = PortalID.ToString();
                        return PortalID;
                    }
                    else if (URL.Contains("/Sagin/HandleModuleControls.aspx"))
                    {
                        int PortalID = int.Parse(HttpContext.Current.Request.QueryString["pid"].ToString());
                        return PortalID;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch
                {
                    return 1;
                }
            }
        }

        private bool IsPageSecure(int portalID, string pageName)
        {
            try
            {
                SQLHandler sqLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParaMeter.Add(new KeyValuePair<string, object>("@PageName", pageName));
                int flag = sqLH.ExecuteAsScalar<int>("usp_IsSecurePage", ParaMeter);
                return flag == 1;
            }
            catch
            {

                return false;
            }
        }

        #endregion

    }


}
