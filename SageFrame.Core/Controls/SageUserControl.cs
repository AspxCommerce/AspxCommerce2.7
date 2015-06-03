#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using SageFrame.ErrorLog;
using SageFrame.Web;
using SageFrame.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using SageFrame.Web.Common.SEO;
using SageFrame.Web.Utilities;
using System.Collections;
using SageFrame.Common;
using SageFrame.Templating;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using SageFrame.UserAgent;
using SageFrame.Core;
using SageFrame.PortalSetting;
using SageFrame.ModuleLoader;


#endregion

namespace SageFrame.Web
{
    /// <summary>
    /// SageUserControl is the base class of SageFrame which gives all the information related to registered userControl
    /// </summary>
    [Serializable]
    public partial class SageUserControl : System.Web.UI.UserControl
    {
        #region "Properties"

        int PortalID = 1;
        int StoreID = 1;
        int CustomerID = 0;
        string PortalSEOName = string.Empty;
        private static string _PagePath = string.Empty;
        /// <summary>
        /// Returns current redirectURL.
        /// </summary>
        public string aspxRedirectPath
        {
            get
            {
                string sageRedirectPath = string.Empty;
                if (!IsParent)
                {
                    SageFrameConfig sfConfig = new SageFrameConfig();
                    sageRedirectPath = ResolveUrl("~/portal/" + GetPortalSEOName + "/");
                }
                else
                {
                    SageFrameConfig sfConfig = new SageFrameConfig();
                    sageRedirectPath = ResolveUrl("~/");
                }
                return sageRedirectPath;
            }
        }

        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the SageUserControl class.
        /// </summary>
        public SageUserControl()
        {

        }

        #endregion

        #region "Protected Methods"

        /// <summary>
        /// Displays the message with provided message, title, complete message and message type.
        /// </summary>
        /// <param name="MessageTitle">Message title to be display</param>
        /// <param name="Message">Message to be display</param>
        /// <param name="CompleteMessage">Complete message to be display</param>
        /// <param name="MessageType">alert , error or success</param>
        protected void ShowMessage(string MessageTitle, string Message, string CompleteMessage, SageMessageType MessageType)
        {
            ScriptManager scp = (ScriptManager)this.Page.FindControl("ScriptManager1");
            if (scp != null)
            {
                bool isSageAsyncPostBack = false;
                if (scp.IsInAsyncPostBack)
                {
                    isSageAsyncPostBack = true;
                }
                if (this.Page == null)
                    return;
                Page SagePage = this.Page;
                if (SagePage == null)
                    return;

                PageBase mSagePage = SagePage as PageBase;
                if (mSagePage != null)
                    mSagePage.ShowMessage(MessageTitle, Message, CompleteMessage, isSageAsyncPostBack, MessageType);
            }
        }

        /// <summary>
        /// Sets Current Culture.
        /// </summary>
        /// <param name="name">UI Culture Name</param>
        /// <param name="locale">Culture Name</param>
        protected void SetCulture(string name, string locale)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);
            Session[SessionKeys.SageUICulture] = Thread.CurrentThread.CurrentUICulture;
            Session[SessionKeys.SageCulture] = Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        /// Returns Current UI Culture.
        /// </summary>
        /// <returns></returns>
        protected string GetCurrentUICulture()
        {
            return Thread.CurrentThread.CurrentUICulture.ToString();
        }

        /// <summary>
        /// Returns Current Culture.
        /// </summary>
        /// <returns>Current Culture</returns>
        protected string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.ToString();
        }

        #endregion

        #region "Public Methods"

        /// <summary>
        /// Encodes and fixes the URL.
        /// </summary>
        /// <param name="str">URL</param>
        /// <returns>Encoded URL</returns>
        public string fixedEncodeURIComponent(string str)
        {
            str = HttpUtility.UrlEncode(str);
            str = Regex.Replace(str, "/!/g", "%21");
            str = Regex.Replace(str, "/'/g", "%27");
            str = Regex.Replace(str, @"/\(/g", "%28");
            str = Regex.Replace(str, @"/\)/g", "%29");
            str = Regex.Replace(str, "/-/g", "_");
            str = Regex.Replace(str, @"/\*/g", "%2A");
            str = Regex.Replace(str, "/%26/g", "ampersand");
            str = Regex.Replace(str, "/%20/g", "'-'");
            return str;

        }

        /// <summary>
        /// Sets or gets the page path.
        /// </summary>
        public string PagePath
        {
            get { return _PagePath; }
            set { _PagePath = value; }
        }

        /// <summary>
        /// Sets or gets the UsermoduleID.
        /// </summary>
        public string SageUserModuleID
        {
            get
            {
                if (ViewState["__SageUserModuleID"] != null)
                {
                    return ViewState["__SageUserModuleID"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["__SageUserModuleID"] = value;
            }
        }

        /// <summary>
        /// Checks if the requested request  is from handheld device.
        /// </summary>
        /// <returns>Returns "True" if the device is handheld</returns>
        public bool IsHandheld()
        {
            SageFrameConfig sfConf = new SageFrameConfig();
            string GetMode = sfConf.GetSettingValueByIndividualKey(SageFrameSettingKeys.UserAgentMode);
            bool status = false;
            if (GetMode == "3")
            {
                string strUserAgent = Request.UserAgent.ToString().ToLower();
                if (strUserAgent != null)
                {
                    if (Request.Browser.IsMobileDevice == true
                                || strUserAgent.Contains("mobile"))
                    //if (Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                    //    strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                    //    strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                    //    strUserAgent.Contains("palm"))
                    {
                        status = true;
                    }
                }
            }
            else
            {
                int Mode = 0;
                Mode = int.Parse(GetMode);
                if (Mode == 2) status = true;
                else status = false;
            }
            return status;
        }

        /// <summary>
        /// Returns message for given modulename with node.
        /// </summary>
        /// <param name="ModuleName"> SageFrame module name</param>
        /// <param name="MessageNode"> Message node in XML</param>
        /// <returns>message</returns>
        public string GetSageMessage(string ModuleName, string MessageNode)
        {
            try
            {
                return SageMessage.ProcessSageMessage(CultureInfo.CurrentCulture.Name, ModuleName, MessageNode);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get Current User Profile Image.
        /// </summary>
        public string GetUserImage
        {
            get
            {
                try
                {
                    SecurityPolicy objSecurity = new SecurityPolicy();
                    string userName = objSecurity.GetUser(GetPortalID);
                    string userImage = string.Empty;
                    if (userName != ApplicationKeys.anonymousUser)
                    {
                        if (HttpContext.Current.Session[SessionKeys.SageFrame_UserProfilePic] != null &&
                            HttpContext.Current.Session[SessionKeys.SageFrame_UserProfilePic].ToString() != string.Empty)
                        {
                            userImage = HttpContext.Current.Session[SessionKeys.SageFrame_UserProfilePic].ToString();
                        }
                        return string.Format("~/Modules/Admin/UserManagement/UserPic/{0}", userImage);
                    }
                    else
                    {
                        return "Image is not available";
                    }
                }
                catch
                {
                    return "Image is not available";
                }
            }
        }

        /// <summary>
        /// Checks weather the usercontrol postback is true or false.
        /// </summary>
        public bool IsControlPostBack
        {
            get
            {
                bool retValue = false;
                if (ViewState["IsControlPostBack"] != null)
                {
                    retValue = true;
                }
                return retValue;
            }
            set { ViewState["IsControlPostBack"] = value; }
        }

        /// <summary>
        /// Returns Application Path With "/" For Virtual Directory.
        /// Returns Application Path With "" For Root Domain.
        /// </summary>
        public string GetApplicationName
        {
            get
            {
                return (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath);
            }
        }


        /// <summary>
        /// Returns the host path.
        /// for eg: http://sageframe.com/ or http://localhost:2511/SageFrame/
        /// </summary>
        /// <returns></returns>
        public string GetHostURL()
        {
            return Page.Request.Url.Scheme + "://" + Request.Url.Authority + GetApplicationName;
        }

        /// <summary>
        /// Check and reduce "/" of URL.
        /// </summary>
        public string GetReduceApplicationName
        {
            get
            {
                return (Request.ApplicationPath == "/" ? "" : "/");
            }
        }

        /// <summary>
        /// Returns image URL for client side web controls.
        /// </summary>
        /// <param name="imageName">Image Name.</param>
        /// <param name="isServerControl"> Set True For Server Control.</param>
        /// <returns> Get Image Full URL.</returns>
        public string GetTemplateImageUrl(string imageName, bool isServerControl)
        {
            string path = string.Empty;
            if (isServerControl == true)
            {
                path = string.Format("~/Administrator/Templates/Default/images/{0}", imageName);
            }
            else
            {
                path = this.Page.ResolveUrl(string.Format("~/Administrator/Templates/images/{0}", imageName));
            }
            return path;
        }

        /// <summary>
        /// Returns image URL for admin web controls.
        /// </summary>
        /// <param name="imageName">Image name</param>
        /// <param name="isServerControl">Set true for ASP.NET server control</param>
        /// <returns>Full URL of given image</returns>
        public string GetAdminImageUrl(string imageName, bool isServerControl)
        {
            string path = string.Empty;
            if (isServerControl == true)
            {
                path = string.Format("~/Administrator/Templates/Default/images/{0}", imageName);
            }
            else
            {
                path = this.Page.ResolveUrl(string.Format("~/Administrator/Templates/images/{0}", imageName));
            }
            return path;
        }

        /// <summary>
        /// Gets current portalID.
        /// </summary>
        public Int32 GetPortalID
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.SageFrame_PortalID] != null &&
                    HttpContext.Current.Session[SessionKeys.SageFrame_PortalID].ToString() != string.Empty)
                {
                    PortalID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_PortalID].ToString());
                }
                return PortalID;
            }
        }

        /// <summary>
        /// Checks if the currnt portal is parent or not.
        /// </summary>

        public bool IsParent
        {
            get
            {
                try
                {
                    int portalID = GetPortalID;
                    bool flag = false;
                    if (HttpContext.Current.Session[SessionKeys.IsParent + portalID] != null)
                    {
                        flag = bool.Parse(HttpContext.Current.Session[SessionKeys.IsParent + portalID].ToString()) == true ? true : false;
                    }
                    else
                    {
                        SageFrameConfig obj = new SageFrameConfig();
                        flag = obj.DecideIsParent(portalID);
                        HttpContext.Current.Session[SessionKeys.IsParent + portalID] = flag;
                    }
                    return flag;
                }
                catch
                {
                    return false;
                }

            }

        }

        /// <summary>
        ///Gets parent portal ID.
        /// </summary>
        public string GetParentURL
        {
            get
            {
                try
                {
                    string ParentURL = string.Empty;
                    if (HttpContext.Current.Session[SessionKeys.ParentURL] != null)
                    {
                        ParentURL = HttpContext.Current.Session[SessionKeys.ParentURL].ToString();

                    }
                    else
                    {
                        int PID = GetPortalID;
                        SageFrameConfig obj = new SageFrameConfig();
                        ParentURL = obj.GetPortalParentURL(PID);
                    }
                    if (ParentURL.ToLower() == "default")
                    {
                        ParentURL = "~/";
                    }
                    HttpContext.Current.Session[SessionKeys.ParentURL] = ParentURL;
                    return ParentURL;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Set PortalID.
        /// </summary>
        /// <param name="portalID">PortalID</param>
        public void SetPortalID(int portalID)
        {
            PortalID = portalID;
        }

        /// <summary>
        /// Get Current StoreID for AspxCommerce.
        /// </summary>
        public Int32 GetStoreID
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.SageFrame_StoreID] != null &&
                    HttpContext.Current.Session[SessionKeys.SageFrame_StoreID].ToString() != string.Empty)
                {
                    StoreID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_StoreID].ToString());
                }
                return StoreID;
            }
        }

        /// <summary>
        /// Set StoreID For AspxCommerce.
        /// </summary>
        /// <param name="storeID">StoreID</param>
        public void SetStoreID(int storeID)
        {
            StoreID = storeID;
        }

        /// <summary>
        /// Get Current CustomerID for AspxCommerce.
        /// </summary>
        public Int32 GetCustomerID
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID] != null &&
                    HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID].ToString() != string.Empty)
                {
                    CustomerID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID].ToString());
                }
                return CustomerID;
            }
        }

        /// <summary>
        /// Set Customer ID For AspxCommerce User.
        /// </summary>
        /// <param name="customerID">CustomerID</param>
        public void SetCustomerID(int customerID)
        {
            CustomerID = customerID;
        }

        /// <summary>
        /// Get Current User Name.
        /// </summary>
        public string GetUsername
        {
            get
            {
                try
                {
                    SecurityPolicy objSecurity = new SecurityPolicy();
                    string userName = objSecurity.GetUser(GetPortalID);

                    return userName;

                }
                catch
                {
                    return ApplicationKeys.anonymousUser;
                }
            }
        }

        /// <summary>
        /// Get Active Template Name.
        /// </summary>
        public string TemplateName
        {
            get
            {
                try
                {
                    if (Globals.sysHst[ApplicationKeys.ActiveTemplate + "_" + GetPortalID] != null &&
                        Globals.sysHst[ApplicationKeys.ActiveTemplate + "_" + GetPortalID].ToString() != string.Empty)
                    {
                        return (string)Globals.sysHst[ApplicationKeys.ActiveTemplate + "_" + GetPortalID];
                    }
                    else
                    {
                        return (TemplateController.GetActiveTemplate(GetPortalID).TemplateSeoName);
                    }
                }
                catch
                {
                    return ApplicationKeys.DefaultActiveTemplateName;
                }
            }
        }

        /// <summary>
        /// Get Active Admin Theme.
        /// </summary>
        public string GetActiveAdminTheme
        {
            get
            {
                try
                {
                    if (Session[SessionKeys.SageFrame_AdminTheme] != null)
                    {
                        return Session[SessionKeys.SageFrame_AdminTheme].ToString();
                    }
                    else
                    {
                        return (ThemeHelper.GetAdminTheme(GetPortalID, GetUsername));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// Returns application's secure token value for login user.
        /// </summary>
        public string SageFrameSecureToken
        {
            get
            {
                string authToken = string.Empty;
                SecurityPolicy objSecurity = new SecurityPolicy();
                authToken = objSecurity.FormsCookieName(GetPortalID);
                return authToken;
            }
        }

        /// <summary>
        /// Get Preset Details.
        /// </summary>
        public PresetInfo GetPresetDetails
        {
            get
            {
                try
                {
                    if (Globals.sysHst[ApplicationKeys.ActivePagePreset + "_" + GetPortalID] != null)
                    {
                        return Globals.sysHst[ApplicationKeys.ActivePagePreset + "_" + GetPortalID] as PresetInfo;
                    }
                    else
                    {
                        return (PresetHelper.LoadActivePagePreset(TemplateName, GetPageSEOName(Request.Url.ToString())));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Set Active Theme.
        /// </summary>
        /// <param name="newtheme"> Theme Name</param>
        public void SetActiveTheme(string newtheme)
        {
            PresetInfo preset = GetPresetDetails;
            preset.ActiveTheme = newtheme;
            Globals.sysHst[ApplicationKeys.ActivePagePreset + "_" + GetPortalID] = preset;
            PresetHelper.UpdatePreset(preset, TemplateName);
        }

        /// <summary>
        /// Set Active Width of Preset.
        /// </summary>
        /// <param name="newwidth">Preset Width</param>
        public void SetActiveWidth(string newwidth)
        {
            PresetInfo preset = GetPresetDetails;
            preset.ActiveWidth = newwidth;
            Globals.sysHst[ApplicationKeys.ActivePagePreset + "_" + GetPortalID] = preset;
            PresetHelper.UpdatePreset(preset, TemplateName);
        }

        /// <summary>
        /// Get Page SEO Name By Page Path.
        /// </summary>
        /// <param name="pagePath">Page Path</param>
        /// <returns>Page SEO Name</returns>
        public string GetPageSEOName(string pagePath)
        {
            string SEOName = string.Empty;
            if (string.IsNullOrEmpty(pagePath))
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                SEOName = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);
            }
            else
            {
                string[] pagePaths = pagePath.Split('/');
                SEOName = pagePaths[pagePaths.Length - 1];
                if (string.IsNullOrEmpty(SEOName))
                {
                    SEOName = pagePaths[pagePaths.Length - 2];
                }
                SEOName = SEOName.Replace(SageFrameSettingKeys.PageExtension, string.Empty);
            }
            return SEOName;
        }

        /// <summary>
        /// Reidirects to the url elimenating the querystring.
        /// </summary>
        /// <param name="RedirectUrl"> URL with  querystring</param>
        public void ProcessCancelRequestBase(string RedirectUrl)
        {
            string strURL = string.Empty;
            if (RedirectUrl.Contains("?"))
            {
                string[] d = RedirectUrl.Split('?');
                strURL = d[0];
            }
            HttpContext.Current.Response.Redirect(strURL, false);
        }

        /// <summary>
        /// Get Portal SEO Name.
        /// </summary>
        protected string GetPortalSEOName
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.SageFrame_PortalSEOName] != null &&
                     HttpContext.Current.Session[SessionKeys.SageFrame_PortalSEOName].ToString() != string.Empty)
                {
                    PortalSEOName = HttpContext.Current.Session[SessionKeys.SageFrame_PortalSEOName].ToString();
                }
                return PortalSEOName;
            }
        }

        /// <summary>
        /// Set Portal SEO Name.
        /// </summary>
        /// <param name="portalSEOName"> Portal SEO Name</param>
        public void SetPortalSEOName(string portalSEOName)
        {
            PortalSEOName = portalSEOName;
        }

        /// <summary>
        /// Reduce the rawURL to simple URL, appends the parameter and redirects.
        /// </summary>
        /// <param name="rawUrl">raw URL</param>
        /// <param name="controlPath"> control Path</param>
        /// <param name="parameter">URL parameter to be added</param>
        public void ProcessSourceControlUrlBase(string rawUrl, string controlPath, string parameter)
        {
            //Added For unique Control ID generation
            //int controlUniqueIDPrefix = GetRandomNumber(System.Int32.Parse(DateTime.Now.ToString()), System.Int32.MaxValue);

            string strURL = string.Empty;
            if (rawUrl.Contains("?"))
            {
                string[] d = rawUrl.Split('?');
                strURL = d[0];
                strURL = strURL + "?" + parameter + "=" + controlPath;
            }
            else
            {
                strURL = rawUrl + "?" + parameter + "=" + controlPath;
            }
            HttpContext.Current.Response.Redirect(strURL, false);
        }

        /// <summary>
        /// Get Current Culture Name.
        /// </summary>
        public string GetCurrentCultureName
        {
            get
            {
                return CultureInfo.CurrentCulture.Name;
            }
        }
        public void GetPortalCommonInfo(out int StoreID, out int PortalID, out int CustomerID, out string UserName, out string CultureName, out string SessionCode)
        {
            StoreID = 0;
            if (HttpContext.Current.Session[SessionKeys.SageFrame_StoreID] != null &&
                   HttpContext.Current.Session[SessionKeys.SageFrame_StoreID].ToString() != string.Empty)
            {
                StoreID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_StoreID].ToString());
            }
            PortalID = 0;
            if (HttpContext.Current.Session[SessionKeys.SageFrame_PortalID] != null &&
                   HttpContext.Current.Session[SessionKeys.SageFrame_PortalID].ToString() != string.Empty)
            {
                PortalID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_PortalID].ToString());
            }
            CustomerID = 0;
            if (HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID] != null &&
                   HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID].ToString() != string.Empty)
            {
                CustomerID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID].ToString());
            }
            try
            {
                SecurityPolicy objSecurity = new SecurityPolicy();
                UserName = objSecurity.GetUser(GetPortalID);

            }
            catch
            {
                UserName = ApplicationKeys.anonymousUser;
            }
            CultureName = CultureInfo.CurrentCulture.Name;
            SessionCode = string.Empty;
            if (HttpContext.Current.Session.SessionID != null)
            {
                SessionCode = HttpContext.Current.Session.SessionID.ToString();
            }
        }
        public void GetPortalCommonInfo(out int StoreID, out int PortalID, out int CustomerID, out string UserName, out string CultureName)
        {
            StoreID = 0;
            if (HttpContext.Current.Session[SessionKeys.SageFrame_StoreID] != null &&
                   HttpContext.Current.Session[SessionKeys.SageFrame_StoreID].ToString() != string.Empty)
            {
                StoreID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_StoreID].ToString());
            }
            PortalID = 0;
            if (HttpContext.Current.Session[SessionKeys.SageFrame_PortalID] != null &&
                   HttpContext.Current.Session[SessionKeys.SageFrame_PortalID].ToString() != string.Empty)
            {
                PortalID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_PortalID].ToString());
            }
            CustomerID = 0;
            if (HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID] != null &&
                   HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID].ToString() != string.Empty)
            {
                CustomerID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID].ToString());
            }
            try
            {
                SecurityPolicy objSecurity = new SecurityPolicy();
                UserName = objSecurity.GetUser(GetPortalID);

            }
            catch
            {
                UserName = ApplicationKeys.anonymousUser;
            }
            CultureName = CultureInfo.CurrentCulture.Name;
        }
        public void GetPortalCommonInfo(out int StoreID, out int PortalID, out string UserName, out string CultureName)
        {
            StoreID = 0;
            if (HttpContext.Current.Session[SessionKeys.SageFrame_StoreID] != null &&
                   HttpContext.Current.Session[SessionKeys.SageFrame_StoreID].ToString() != string.Empty)
            {
                StoreID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_StoreID].ToString());
            }
            PortalID = 0;
            if (HttpContext.Current.Session[SessionKeys.SageFrame_PortalID] != null &&
                   HttpContext.Current.Session[SessionKeys.SageFrame_PortalID].ToString() != string.Empty)
            {
                PortalID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_PortalID].ToString());
            }
            try
            {
                SecurityPolicy objSecurity = new SecurityPolicy();
                UserName = objSecurity.GetUser(GetPortalID);

            }
            catch
            {
                UserName = ApplicationKeys.anonymousUser;
            }
            CultureName = CultureInfo.CurrentCulture.Name;
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="ctl">Control</param>
        /// <param name="MessageTitle">Message title</param>
        /// <param name="Message">Message</param>
        /// <param name="CompleteMessage">Complete message</param>
        /// <param name="isSageAsyncPostBack">set true if is post back</param>
        /// <param name="MessageType">messagetype: error, success or alert</param>
        /// <param name="strCssClass">class that wrap the message</param>
        public void Modules_Message_ShowMessage(Control ctl, string MessageTitle, string Message, string CompleteMessage, bool isSageAsyncPostBack, SageMessageType MessageType, string strCssClass)
        {

            Label lblUdpSageMesageTitle = ctl.FindControl("lblUdpSageMesageTitle") as Label;
            Label lblUdpSageMesageCustom = ctl.FindControl("lblUdpSageMesageCustom") as Label;
            Label lblUdpSageMesageDetail = ctl.FindControl("lblUdpSageMesageDetail") as Label;
            System.Web.UI.HtmlControls.HtmlGenericControl divUdpSageMessage = ctl.FindControl("divUdpSageMessage") as System.Web.UI.HtmlControls.HtmlGenericControl;
            isSageAsyncPostBack = true;
            bool isudpSageMessageVisible = false;
            if (isSageAsyncPostBack)
            {
                if (lblUdpSageMesageTitle != null && lblUdpSageMesageCustom != null && lblUdpSageMesageDetail != null && divUdpSageMessage != null)
                {
                    lblUdpSageMesageTitle.Visible = false;
                    lblUdpSageMesageCustom.Text = Message;
                    if (Message == string.Empty)
                        lblUdpSageMesageCustom.Visible = false;

                    lblUdpSageMesageDetail.Text = CompleteMessage;
                    if (CompleteMessage == string.Empty)
                        lblUdpSageMesageDetail.Visible = false;

                    divUdpSageMessage.Attributes.Add("class", strCssClass);
                    isudpSageMessageVisible = true;
                }
            }
            System.Web.UI.HtmlControls.HtmlGenericControl divUdpMessage = ctl.FindControl("divUdpMessage") as System.Web.UI.HtmlControls.HtmlGenericControl;
            if (divUdpMessage != null)
            {
                if (isudpSageMessageVisible == true)
                {
                    divUdpMessage.Style.Add("display", "block");
                }

            }
        }

        /// <summary>
        /// File Url must be in "~/" nature
        /// like ~/js/scriptfile.js
        /// Note: Use only when script is required in head section other wise use IncludeScriptFile function
        /// </summary>
        /// <param name="FileUrl">File Url must be in "~/" nature</param>
        public void IncludeScriptFileInHeadSection(string FileUrl)
        {
            Literal LitSageScript = this.Page.FindControl("SageFrameModuleCSSlinks") as Literal;
            if (LitSageScript != null)
            {
                string strScripts = string.Empty;
                if (FileUrl != string.Empty)
                {
                    strScripts += "<script src=\"" + ResolveUrl(FileUrl) + "\" type=\"text/javascript\"></script>";
                }
                if (!LitSageScript.Text.Contains(strScripts))
                {
                    LitSageScript.Text += strScripts;
                }
            }
        }

        /// <summary>
        /// File Url must be in "~/" nature
        /// like ~/js/scriptfile.js
        /// Note:
        /// For this block of code your page musth have Literal of ID "LitSageScript"
        /// </summary>
        /// <param name="FileUrls"></param>
        public void IncludeScriptFile(ArrayList FileUrls)
        {
            Literal LitSageScript = this.Page.FindControl("LitSageScript") as Literal;
            if (LitSageScript != null)
            {
                if (FileUrls.Count > 0)
                {
                    for (int i = 0; i < FileUrls.Count; i++)
                    {
                        string strScripts = string.Empty;
                        strScripts = "<script src=\"" + ResolveUrl(FileUrls[i].ToString()) + "\" type=\"text/javascript\"></script>";
                        if (!LitSageScript.Text.Contains(strScripts))
                        {
                            LitSageScript.Text += strScripts;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// File Url must be in "~/" nature
        /// like ~/js/scriptfile.js
        /// Note:
        /// For this block of code your page musth have Literal of ID "LitSageScript"
        /// </summary>
        /// <param name="FileUrl"></param>
        public void IncludeScriptFile(string FileUrl)
        {
            Literal LitSageScript = this.Page.FindControl("LitSageScript") as Literal;
            if (LitSageScript != null)
            {
                string strScripts = string.Empty;
                if (FileUrl != string.Empty)
                {
                    strScripts += "<script src=\"" + ResolveUrl(FileUrl) + "\" type=\"text/javascript\"></script>";
                }
                if (!LitSageScript.Text.Contains(strScripts))
                {
                    LitSageScript.Text += strScripts;
                }
            }
        }

        /// <summary>
        /// Include Language JS From Modules.
        /// </summary>
        public void IncludeLanguageJS()
        {
            try
            {
                string strScript = string.Empty;
                string langFolderPath = this.AppRelativeTemplateSourceDirectory + "Language/";
                string modulePath = this.AppRelativeTemplateSourceDirectory.Replace("~", "") + "Language/";
                if (Directory.Exists(Server.MapPath(langFolderPath)))
                {
                    bool isTrue = false;
                    string[] fileList = Directory.GetFiles(Server.MapPath(langFolderPath));

                    string regexPattern = ".*\\\\(?<file>[^\\.]+)(\\.[a-z]{2}-[A-Z]{2})?\\.js";

                    Regex regex = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);

                    Match match = regex.Match(fileList[0]);
                    string languageFile = match.Groups[2].Value;
                    string FileUrl = string.Empty;
                    isTrue = GetCurrentCulture() == "en-US" ? true : false;
                    if (isTrue)
                    {
                        FileUrl = modulePath + languageFile + ".js";
                    }
                    else
                    {
                        FileUrl = modulePath + languageFile + "." + GetCurrentCulture() + ".js";
                    }
                    IncludeJsTop(languageFile, FileUrl);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        ///<summary>
        ///Include All Language JS From Modules.
        ///</summary>
        public void IncludeAllLanguageJS()
        {
            try
            {
                string strScript = string.Empty;
                string langFolderPath = this.AppRelativeTemplateSourceDirectory + "Language/";
                string modulePath = this.AppRelativeTemplateSourceDirectory.Replace("~", "") + "Language/";
                if (Directory.Exists(Server.MapPath(langFolderPath)))
                {
                    bool isTrue = false;
                    string[] fileList = Directory.GetFiles(Server.MapPath(langFolderPath));
                    string regexPattern = ".*\\\\(?<file>[^\\.]+)(\\.[a-z]{2}-[A-Z]{2})?\\.js";

                    Regex regex = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);
                    int i = 0;
                    List<string> listLanguageFile = new List<string>();
                    while(i < fileList.Length)
                    {
                        Match match = regex.Match(fileList[i]);
                        if(match.Success)
                        {
                            listLanguageFile.Add(match.Groups[2].Value);
                        }
                        i++;
                    }
                    listLanguageFile = listLanguageFile.Distinct().ToList();
                    string languageFiles = string.Empty; 
                    string[] FileUrl = new string[listLanguageFile.Count];
                    isTrue = GetCurrentCulture() == "en-US" ? true : false;
                    i=0;
                    foreach(string languageFile in listLanguageFile)
                    {
                        if (isTrue)
                            FileUrl[i++] = modulePath + languageFile + ".js";
                        else
                            FileUrl[i++] = modulePath + languageFile + GetCurrentCulture() + ".js";
                    }
                    IncludeJsTop(listLanguageFile[0], FileUrl);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// File Url must be in "~/" nature
        /// "~/Modules/ModuleName/cssfile.css
        /// Note :
        /// For This block of code your page must have Literal of ID "SageFrameModuleCSSlinks"
        /// Use only css file except having name other than module.css, module.css file will automatically inlcude in the file is in
        /// root of module.
        /// </summary>
        /// <param name="FileUrl"></param>
        public void IncludeCssFile(string FileUrl)
        {
            Literal SageFrameModuleCSSlinks = this.Page.FindControl("SageFrameModuleCSSlinks") as Literal;
            if (SageFrameModuleCSSlinks != null)
            {
                string linkText = "<link href=\"" + Page.ResolveUrl(FileUrl) + "\" id=\"cssLinks\" rel=\"stylesheet\" type=\"text/css\" />";
                if (!SageFrameModuleCSSlinks.Text.Contains(linkText))
                {
                    SageFrameModuleCSSlinks.Text += linkText;
                }
            }
        }

        /// <summary>
        /// Register Client Script To Page.
        /// </summary>
        /// <param name="Key">Key For Script</param>
        /// <param name="Value">Value For The Key</param>
        /// <param name="checkMode">Set True To Check Either The key Exists On The Page</param>
        public void RegisterClientScriptToPage(string Key, string Value, bool checkMode)
        {
            if (checkMode)
            {
                if (!Page.ClientScript.IsClientScriptIncludeRegistered(Key))
                {
                    Page.ClientScript.RegisterClientScriptInclude(Key, Value);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptInclude(Key, Value);
            }
        }

        /// <summary>
        /// Include Js Files in the Page.
        /// </summary>
        /// <param name="ModuleName"> Module Name</param>
        /// <param name="files"> List Of Files</param>
        public void IncludeJs(string ModuleName, params string[] files)
        {
            List<CssScriptInfo> lstResources = new List<CssScriptInfo>();
            foreach (string FilePath in files)
            {
                string folderPath = FilePath.LastIndexOf("/") > -1 ? FilePath.Substring(0, FilePath.LastIndexOf("/") + 1) : "";
                string FileName = Path.GetFileName(FilePath);
                if (HttpContext.Current.Session[SessionKeys.ModuleJs] != null)
                {
                    lstResources = HttpContext.Current.Session[SessionKeys.ModuleJs] as List<CssScriptInfo>;
                }
                bool exists = lstResources.Exists(
                                    delegate(CssScriptInfo obj)
                                    {
                                        return (obj.FileName == FileName && obj.Path == folderPath);
                                    }
                                );
                if (!exists)
                {
                    lstResources.Add(new CssScriptInfo(ModuleName, FileName, folderPath, 1, true));
                }
            }
            HttpContext.Current.Session[SessionKeys.ModuleJs] = lstResources;
        }

        /// <summary>
        /// Include JS Files in the Page.
        /// </summary>
        /// <param name="ModuleName">Module Name</param>
        /// <param name="AllowCompression">Set True For Compression</param>
        /// <param name="files"> List Of Files</param>
        public void IncludeJs(string ModuleName, bool AllowCompression, params string[] files)
        {
            List<CssScriptInfo> lstResources = new List<CssScriptInfo>();
            foreach (string FilePath in files)
            {
                string folderPath = FilePath.LastIndexOf("/") > -1 ? FilePath.Substring(0, FilePath.LastIndexOf("/") + 1) : "";
                string FileName = Path.GetFileName(FilePath);
                if (HttpContext.Current.Session[SessionKeys.ModuleJs] != null)
                {
                    lstResources = HttpContext.Current.Session[SessionKeys.ModuleJs] as List<CssScriptInfo>;
                }
                bool exists = lstResources.Exists(
                                    delegate(CssScriptInfo obj)
                                    {
                                        return (obj.FileName == FileName && obj.Path == folderPath);
                                    }
                                );
                if (!exists)
                {
                    lstResources.Add(new CssScriptInfo(ModuleName, FileName, folderPath, 1, false));
                }
            }
            HttpContext.Current.Session[SessionKeys.ModuleJs] = lstResources;
        }

        /// <summary>
        /// Include JS In The Top Of Page With List Of JS Files.
        /// </summary>
        /// <param name="ModuleName">Module Name</param>
        /// <param name="files"> List Of Files</param>
        public void IncludeJsTop(string ModuleName, params string[] files)
        {
            List<CssScriptInfo> lstResources = new List<CssScriptInfo>();
            foreach (string FilePath in files)
            {
                string folderPath = FilePath.LastIndexOf("/") > -1 ? FilePath.Substring(0, FilePath.LastIndexOf("/") + 1) : "";
                string FileName = Path.GetFileName(FilePath);
                if (HttpContext.Current.Session[SessionKeys.ModuleJs] != null)
                {
                    lstResources = HttpContext.Current.Session[SessionKeys.ModuleJs] as List<CssScriptInfo>;
                }
                bool exists = lstResources.Exists(
                                    delegate(CssScriptInfo obj)
                                    {
                                        return (obj.FileName == FileName && obj.Path == folderPath);
                                    }
                                );
                if (!exists)
                {
                    lstResources.Add(new CssScriptInfo(ModuleName, FileName, folderPath, 0));
                }
            }
            HttpContext.Current.Session[SessionKeys.ModuleJs] = lstResources;
        }

        /// <summary>
        /// Include JS In The Top Of Page With List Of JS Files.
        /// </summary>
        /// <param name="ModuleName">Module Name</param>
        /// <param name="AllowCompression"> Set True For Compression</param>
        /// <param name="files"> List Of Files</param>
        public void IncludeJsTop(string ModuleName, bool AllowCompression, params string[] files)
        {
            List<CssScriptInfo> lstResources = new List<CssScriptInfo>();
            foreach (string FilePath in files)
            {
                string folderPath = FilePath.LastIndexOf("/") > -1 ? FilePath.Substring(0, FilePath.LastIndexOf("/") + 1) : "";
                string FileName = Path.GetFileName(FilePath);
                if (HttpContext.Current.Session[SessionKeys.ModuleJs] != null)
                {
                    lstResources = HttpContext.Current.Session[SessionKeys.ModuleJs] as List<CssScriptInfo>;
                }
                bool exists = lstResources.Exists(
                                    delegate(CssScriptInfo obj)
                                    {
                                        return (obj.FileName == FileName && obj.Path == folderPath);
                                    }
                                );
                if (!exists)
                {
                    lstResources.Add(new CssScriptInfo(ModuleName, FileName, folderPath, 0, false));
                }
            }
            HttpContext.Current.Session[SessionKeys.ModuleJs] = lstResources;
        }

        /// <summary>
        /// Include Css with List of Css Path.
        /// </summary>
        /// <param name="ModuleName">Module Name</param>
        /// <param name="files">List of Files</param>
        public void IncludeCss(string ModuleName, params string[] files)
        {
            List<CssScriptInfo> lstResources = new List<CssScriptInfo>();
            foreach (string FilePath in files)
            {
                string folderPath = FilePath.LastIndexOf("/") > -1 ? FilePath.Substring(0, FilePath.LastIndexOf("/") + 1) : "";
                string FileName = Path.GetFileName(FilePath);
                if (HttpContext.Current.Session[SessionKeys.ModuleCss] != null)
                {
                    lstResources = HttpContext.Current.Session[SessionKeys.ModuleCss] as List<CssScriptInfo>;
                }
                bool exists = lstResources.Exists(
                                    delegate(CssScriptInfo obj)
                                    {
                                        return (obj.FileName == FileName && obj.Path == folderPath);
                                    }
                                );
                if (!exists)
                {
                    lstResources.Add(new CssScriptInfo(ModuleName, FileName, folderPath, 1));
                }
            }
            HttpContext.Current.Session[SessionKeys.ModuleCss] = lstResources;
        }
        /// <summary>
        /// Load client side module control with dynamic Control Path and Returns Loaded Control Code
        /// </summary>
        /// <param name="ModuleName">ModuleName</param>
        /// <param name="ControlTypeName">Name of Control Type</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <returns>Control source code</returns>
        public string ModuleLoaderForJsWithDynamicControl(string ModuleName, string ControlTypeName, string UserModuleID)
        {
            try
            {
                string ControlSrcPath = "";
                string controlCode = "";
                ModuleLoaderInfo ControlSrc = new ModuleLoaderInfo();
                ModuleLoaderController objController = new ModuleLoaderController();
                int controlType = ModuleControlType(ControlTypeName);
                if (ModuleName != string.Empty)
                {
                    List<ModuleLoaderInfo> objControlList = objController.GetControlSrcByModuleName(ModuleName);
                    if (objControlList != null)
                    {
                        foreach (var item in objControlList)
                        {
                            ControlSrc = objControlList.Single(x => x.ControlType == controlType);
                            ControlSrcPath = ControlSrc.ControlSrc;
                        }
                        Page ucPage = new Page();
                        Page virPage = new Page();
                        SageUserControl userControl = (SageUserControl)ucPage.LoadControl(ControlSrcPath);
                        userControl.EnableViewState = true;
                        userControl.SageUserModuleID = UserModuleID.ToString();
                        virPage.Controls.Add(userControl);
                        StringWriter writer = new StringWriter();
                        HttpContext.Current.Server.Execute(virPage, writer, true);
                        controlCode = writer.ToString();
                    }
                    else
                    {
                        controlCode = "Control Not found";
                    }
                }
                else
                {
                    controlCode = "Module Name empty";
                }
                return controlCode;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Load client side module control with static Control Path and Returns Loaded Control Code
        /// </summary>
        /// <param name="ModuleName">ModuleName</param>
        /// <param name="ControlTypeName">Name of Control Type</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <returns>Control source code</returns>
        public string ModuleLoaderForJsWithStaticControl(string ControlPath, string UserModuleID)
        {
            try
            {
                string controlCode = "";
                string ControlSrcPath = ControlPath;
                Page ucPage = new Page();
                Page virPage = new Page();
                SageUserControl userControl = (SageUserControl)ucPage.LoadControl(ControlSrcPath);
                userControl.EnableViewState = true;
                userControl.SageUserModuleID = UserModuleID.ToString();
                virPage.Controls.Add(userControl);
                StringWriter writer = new StringWriter();
                HttpContext.Current.Server.Execute(virPage, writer, true);
                controlCode = writer.ToString();
                return controlCode;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Load server side module control with static control ID and dynamic Control Path
        /// </summary>
        /// <param name="ModuleName">ModuleName</param>
        /// <param name="ControlTypeName">Name of Control Type</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="ControlID">ControlID</param>
        public void ModuleLoaderWithDynamicControl(string ModuleName, string ControlTypeName, string UserModuleID, Control ControlID)
        {
            try
            {
                string ControlSrcPath = "";
                ModuleLoaderInfo ControlSrc = new ModuleLoaderInfo();
                ModuleLoaderController objController = new ModuleLoaderController();
                int controlType = ModuleControlType(ControlTypeName);
                List<ModuleLoaderInfo> objControlList = objController.GetControlSrcByModuleName(ModuleName);
                if (objControlList.Count > 0)
                {
                    foreach (var item in objControlList)
                    {
                        ControlSrc = objControlList.Single(x => x.ControlType == controlType);
                        ControlSrcPath = ControlSrc.ControlSrc;
                    }
                    SageUserControl userControl = (SageUserControl)Page.LoadControl(ControlSrcPath);
                    userControl.SageUserModuleID = SageUserModuleID;
                    ControlID.Controls.Add(userControl);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Load server side module control with dynamic position of control and dynamic Control Path
        /// </summary>
        /// <param name="ModuleName">ModuleName</param>
        /// <param name="ControlTypeName">Name of Control Type</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="IsAtTop">Set true if control need to be set in top of existing module in the page</param>
        public void ModuleLoaderWithDynamicControl(string ModuleName, string ControlTypeName, string UserModuleID, bool IsAtTop)
        {
            try
            {
                string ControlSrcPath = "";
                ModuleLoaderInfo ControlSrc = new ModuleLoaderInfo();
                ModuleLoaderController objController = new ModuleLoaderController();
                int controlType = ModuleControlType(ControlTypeName);
                List<ModuleLoaderInfo> objControlList = objController.GetControlSrcByModuleName(ModuleName);
                if (objControlList.Count > 0)
                {
                    foreach (var item in objControlList)
                    {
                        ControlSrc = objControlList.Single(x => x.ControlType == controlType);
                        ControlSrcPath = ControlSrc.ControlSrc;
                    }
                    PlaceHolder ph = new PlaceHolder();
                    if (IsAtTop)
                        this.Controls.AddAt(1, ph);
                    else
                        this.Controls.Add(ph);
                    SageUserControl userControl = (SageUserControl)Page.LoadControl(ControlSrcPath);
                    userControl.SageUserModuleID = SageUserModuleID;
                    ph.Controls.Add(userControl);
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load server side module control with static control ID and static Control Path
        /// </summary>
        /// <param name="ModuleName">ModuleName</param>
        /// <param name="ControlTypeName">Name of Control Type</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="ControlID">ControlID</param>
        public void ModuleLoaderWithStaticControl(string ControlPath, string UserModuleID, Control ControlID)
        {
            try
            {
                string ControlSrcPath = Path.Combine(GetHostURL(), ControlPath);
                SageUserControl userControl = (SageUserControl)Page.LoadControl(ControlSrcPath);
                userControl.SageUserModuleID = SageUserModuleID;
                ControlID.Controls.Add(userControl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load server side module control with dynamic position of control and static Control Path
        /// </summary>
        /// <param name="ModuleName">ModuleName</param>
        /// <param name="ControlTypeName">Name of Control Type</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="IsAtTop">Set true if control need to be set in top of existing module in the page</param>
        public void ModuleLoaderWithStaticControl(string ControlPath, string UserModuleID, bool IsAtTop)
        {
            try
            {
                string ControlSrcPath = Path.Combine(GetHostURL(), ControlPath);
                PlaceHolder ph = new PlaceHolder();
                if (IsAtTop)
                    this.Controls.AddAt(1, ph);
                else
                    this.Controls.Add(ph);
                SageUserControl userControl = (SageUserControl)Page.LoadControl(ControlSrcPath);
                userControl.SageUserModuleID = SageUserModuleID;
                ph.Controls.Add(userControl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns Control Type for Control Type Name
        /// </summary>
        /// <param name="ControlTypeName">Name of Control Type</param>
        /// <returns>Control Type</returns>
        public int ModuleControlType(string ControlTypeName)
        {
            try
            {
                int controlType = 1;
                switch (ControlTypeName.ToLower())
                {
                    case "view":
                        controlType = 1;
                        break;
                    case "edit":
                        controlType = 2;
                        break;
                    case "setting":
                        controlType = 3;
                        break;
                }
                return controlType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}

