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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Framework;
using System.Web.Security;
using SageFrame.Web;
using SageFrame.RolesManagement;
using SageFrame.Web.Utilities;
using SageFrame.Security.Crypto;
using SageFrame.Security.Helpers;
using SageFrame.Security.Entities;
using SageFrame.Security;
//added for OpenID
using System.Text;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using SageFrame.Security.Providers;
using SageFrame.OauthID;
using System.Json;
using SageFrame.Common;
using SageFrame.Core;
using System.Net;
using SageFrame.Security.Controllers;
#endregion

namespace SageFrame.Modules.Admin.LoginControl
{

    public partial class Login : BaseAdministrationUserControl
    {
        string ipAddress = string.Empty;
        string Extension;
        int loginhit;
        string strRoles = string.Empty;
        SageFrameConfig pagebase = new SageFrameConfig();
        public bool RegisterURL = true;
        private Random random = new Random();
        protected void Page_Init(object sender, EventArgs e)
        {
            HttpRequest request = base.Request;
            this.Page.EnableViewState = true;
            ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            SuspendedIPController objSuspendedIP = new SuspendedIPController();
            bool IsSuspended = objSuspendedIP.IsSuspendedIP(ipAddress);
            if (IsSuspended)
            {
                AlreadySuspendedIPAddress();
                MultiView1.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            IncludeLanguageJS();
            Extension = SageFrameSettingKeys.PageExtension;
            if (!IsPostBack)
            {
                int logHit = Convert.ToInt32(Session[SessionKeys.LoginHitCount]);
                if (logHit >= 3)
                {
                    dvCaptchaField.Visible = true;
                    InitializeCaptcha();
                    GenerateCaptchaImage();
                }
                else
                {
                    dvCaptchaField.Visible = false;
                }

                Refresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
                Password.Attributes.Add("onkeypress", "return clickButton(event,'" + LoginButton.ClientID + "')");
                if (!IsParent)
                {
                    hypForgotPassword.NavigateUrl =
                       GetParentURL + "/portal/" + GetPortalSEOName + "/" +
                                   pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalForgotPassword) + Extension;
                }
                else
                {
                    hypForgotPassword.NavigateUrl =
                        GetParentURL + "/" + pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalForgotPassword) +
                                   Extension;
                }
                string registerUrl =
                    GetParentURL + "/" + pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalUserRegistration) +
                               Extension;

                if (pagebase.GetSettingBoolValueByIndividualKey(SageFrameSettingKeys.RememberCheckbox))
                {
                    chkRememberMe.Visible = true;
                    lblrmnt.Visible = true;
                }
                else
                {
                    chkRememberMe.Visible = false;
                    lblrmnt.Visible = false;
                }
            }
            SecurityPolicy objSecurity = new SecurityPolicy();
            FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
            if (ticket != null && ticket.Name != ApplicationKeys.anonymousUser)
            {
                int LoggedInPortalID = int.Parse(ticket.UserData.ToString());
                string[] sysRoles = SystemSetting.SUPER_ROLE;
                if (GetPortalID == LoggedInPortalID || Roles.IsUserInRole(ticket.Name, sysRoles[0]))
                {
                    RoleController _role = new RoleController();
                    string userinroles = _role.GetRoleNames(GetUsername, LoggedInPortalID);
                    if (userinroles != "" || userinroles != null)
                    {
                        MultiView1.ActiveViewIndex = 1;
                    }
                    else
                    {
                        MultiView1.ActiveViewIndex = 0;
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                }

            }
            else
            {
                MultiView1.ActiveViewIndex = 0;
            }
            // Added For openID services
            divOpenIDProvider.Visible = false;
            if (AllowRegistration())
            {
                if (pagebase.GetSettingBoolValueByIndividualKey(SageFrameSettingKeys.ShowOpenID) == true)
                {
                    divOpenIDProvider.Visible = true;
                    CheckOpenID();
                }
            }
        }

        private bool AllowRegistration()
        {
            int UserRegistrationType = pagebase.GetSettingIntValueByIndividualKey(SageFrameSettingKeys.PortalUserRegistration);
            RegisterURL = UserRegistrationType > 0 ? true : false;
            return RegisterURL;
        }

        public void SetUserRoles(string strRoles)
        {
            Session[SessionKeys.SageUserRoles] = strRoles;
            Session[SessionKeys.SageRoles] = strRoles;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookiesKeys.SageUserRolesCookie];
            if (cookie == null)
            {
                cookie = new HttpCookie(CookiesKeys.SageUserRolesCookie);
            }
            cookie[CookiesKeys.SageUserRolesCookie] = strRoles;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private bool ValidateCaptcha()
        {

            if (!(cvCaptchaValue.ValueToCompare == CaptchaValue.Text))
            {
                InitializeCaptcha();
                GenerateCaptchaImage();
                FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("UserRegistration", "EnterTheCorrectCapchaCode"));
                CaptchaValue.Text = string.Empty;
                return false;
            }
            return true;
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            SecurityPolicy objSecurity = new SecurityPolicy();
            FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
            if (ticket != null && ticket.Name == ApplicationKeys.anonymousUser)
            {
                int logHit = Convert.ToInt32(Session[SessionKeys.LoginHitCount]);
                if (logHit >= 3)
                {
                    this.Session[SessionKeys.CaptchaImageText] = null;

                    if (ValidateCaptcha())
                    {
                        LoginUser();
                    }
                }
                else
                {
                    LoginUser();
                }
            }
        }
        private void LoginUser()
        {
            MembershipController member = new MembershipController();
            RoleController role = new RoleController();
            SuspendedIPController objSuspendedIP = new SuspendedIPController();
            UserInfo user = member.GetUserDetails(GetPortalID, UserName.Text);
            HttpContext.Current.Session[SessionKeys.IsLoginClick] = false;
            if (user.UserExists && user.IsApproved)
            {
                if (!(string.IsNullOrEmpty(UserName.Text) && string.IsNullOrEmpty(Password.Text)))
                {
                    if (PasswordHelper.ValidateUser(user.PasswordFormat, Password.Text, user.Password, user.PasswordSalt))
                    {
                        SucessFullLogin(user);
                    }
                    else
                    {
                        if (Session[SessionKeys.LoginHitCount] == null)
                        {
                            Session[SessionKeys.LoginHitCount] = 1;
                        }
                        else
                        {
                            loginhit = Convert.ToInt32(Session[SessionKeys.LoginHitCount]);
                            loginhit++;
                            Session[SessionKeys.LoginHitCount] = loginhit;
                        }
                        FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("UserLogin", "UsernameandPasswordcombinationdoesntmatched"));//"Username and Password combination doesn't matched!";
                        CaptchaValue.Text = string.Empty;
                        if (loginhit == 3)
                        {
                            Page.Response.Redirect(Page.Request.Url.ToString(), true);
                        }
                        if (loginhit > 3 && loginhit < 6)
                        {
                            InitializeCaptcha();
                            CaptchaValue.Text = string.Empty;
                        }
                        else if (loginhit >= 6)
                        {
                            objSuspendedIP.SaveSuspendedIP(ipAddress);
                            SuspendedIPAddressException();
                            Session[SessionKeys.LoginHitCount] = 0;
                            MultiView1.Visible = false;
                        }
                    }
                }
            }
            else
            {
                if (Session[SessionKeys.LoginHitCount] == null)
                {
                    Session[SessionKeys.LoginHitCount] = 1;
                }
                else
                {
                    loginhit = Convert.ToInt32(Session[SessionKeys.LoginHitCount]);
                    loginhit++;
                    Session[SessionKeys.LoginHitCount] = loginhit;
                }
                FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("UserLogin", "UserDoesnotExist"));
                CaptchaValue.Text = string.Empty;
                if (loginhit == 3)
                {
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                if (loginhit > 3 && loginhit < 6)
                {
                    InitializeCaptcha();
                    CaptchaValue.Text = string.Empty;
                }
                else if (loginhit >= 6)
                {
                    objSuspendedIP.SaveSuspendedIP(ipAddress);
                    SuspendedIPAddressException();
                    Session[SessionKeys.LoginHitCount] = 0;
                    MultiView1.Visible = false;
                }
            }
        }
        private void SuspendedIPAddressException()
        {
            string ShortAlert = "Malicious activity found, your IP is suspended for 3hrs due to false attempt in login.For instant access, request your superuser.";
            ShortAlert += " Your IP Address: " + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            ShortAlert += " and";
            ShortAlert += " System Name: " + Dns.GetHostName();
            ShortAlert += " is black listed.";
            string FullAllert = string.Empty;
            ShowMessage(SageMessageTitle.Notification.ToString(), ShortAlert, FullAllert, SageMessageType.Alert);
        }
        private void AlreadySuspendedIPAddress()
        {
            string ShortAlert = "Malicious activity found, your IP is in suspension mode due to false attempt in login.For instant access, request your superuser.";
            ShortAlert += " Your IP Address: " + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            ShortAlert += " and";
            ShortAlert += " System Name: " + Dns.GetHostName();
            ShortAlert += " is black listed.";
            string FullAllert = string.Empty;
            ShowMessage(SageMessageTitle.Notification.ToString(), ShortAlert, FullAllert, SageMessageType.Alert);
        }
        protected void SucessFullLogin(UserInfo user)
        {
            RoleController role = new RoleController();
            Session[SessionKeys.LoginHitCount] = null;
            string userRoles = role.GetRoleNames(user.UserName, GetPortalID);
            strRoles += userRoles;
            if (strRoles.Length > 0)
            {
                SetUserRoles(strRoles);
                //SessionTracker sessionTracker = (SessionTracker)Session[SessionKeys.Tracker];
                //sessionTracker.PortalID = GetPortalID.ToString();
                //sessionTracker.Username = UserName.Text;
                //Session[SessionKeys.Tracker] = sessionTracker;
                SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
                SLog.SessionTrackerUpdateUsername(UserName.Text, GetPortalID.ToString());
                StringBuilder redirectURL = new StringBuilder();
                SecurityPolicy objSecurity = new SecurityPolicy();
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        user.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        true,
                        GetPortalID.ToString(),
                        FormsAuthentication.FormsCookiePath);

                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);
                //generate random cookieValue
                string randomCookieValue = GenerateRandomCookieValue();
                Session[SessionKeys.RandomCookieValue] = randomCookieValue;
                //create new cookie with random cookie name and encrypted ticket
                HttpCookie cookie = new HttpCookie(objSecurity.FormsCookieName(GetPortalID), encTicket);
                //get default time from  setting
                SageFrameConfig objConfig = new SageFrameConfig();
                string ServerCookieExpiration = objConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.ServerCookieExpiration);
                int expiryTime = Math.Abs(int.Parse(ServerCookieExpiration));
                expiryTime = expiryTime < 5 ? 5 : expiryTime;
                //set cookie expiry time
                cookie.Expires = DateTime.Now.AddMinutes(expiryTime);
                //add cookie to the browser
                Response.Cookies.Add(cookie);
                ServiceSecurity.IssueToken(GetPortalID);

                if (Request.QueryString["ReturnUrl"] != null)
                {
                    string PageNotFoundPage = PortalAPI.PageNotFoundURLWithRoot;
                    string UserRegistrationPage = PortalAPI.RegistrationURLWithRoot;
                    string PasswordRecoveryPage = PortalAPI.PasswordRecoveryURLWithRoot;
                    string ForgotPasswordPage = PortalAPI.ForgotPasswordURL;
                    string PageNotAccessiblePage = PortalAPI.PageNotAccessibleURLWithRoot;
                    string ReturnUrlPage = string.Empty;
                    if (Request.QueryString["ReturnUrl"].Replace("%2f", "-").ToString().Contains(GetHostURL()))
                    {
                         ReturnUrlPage = Request.QueryString["ReturnUrl"].Replace("%2f", "-").ToString();
                    }
                    else
                    {
                         ReturnUrlPage = GetHostURL() + Request.QueryString["ReturnUrl"].Replace("%2f", "-").ToString();
                    }
                      string RequestURL = Request.Url.ToString();
                    Uri RequestURLPageUri = new Uri(RequestURL);
                    string portalHostURL = RequestURLPageUri.AbsolutePath.TrimStart('/');
                    if (GetApplicationName==string.Empty)
                    {
                    bool IsWellFormedReturnUrlPage = Uri.IsWellFormedUriString(ReturnUrlPage, UriKind.Absolute);

                    
                        if (IsWellFormedReturnUrlPage)
                        {
                            Uri ReturnUrlPageUri = new Uri(ReturnUrlPage);
                            string ReturnURl = ReturnUrlPageUri.Scheme + Uri.SchemeDelimiter + ReturnUrlPageUri.Host + ":" + ReturnUrlPageUri.Port;
                            string HostUrl = GetHostURL();
                            Uri uriHostURL = new Uri(HostUrl);
                            Uri uriReturnURL = new Uri(ReturnURl);
                            var resultCompareURL = Uri.Compare(uriHostURL, uriReturnURL,
                                UriComponents.Host | UriComponents.PathAndQuery,
                                UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase);
                            int resultComparePortalURL = 0;
                            if (portalHostURL.ToLower().Contains("portal") && resultCompareURL == 0)
                            {
                                Uri ReturnUrlPageHostUri = new Uri(ReturnUrlPage);
                                string portalReturnURL = ReturnUrlPageHostUri.AbsolutePath.TrimStart('/');
                                string[] portalReturnURLSplit = portalReturnURL.Split('/');
                                string ReturnURLSplitPortal = portalReturnURLSplit[0];
                                string ReturnURLSplitPortalName = portalReturnURLSplit[1];
                                string ReturnURLWithPortal = ReturnURLSplitPortal + "/" + ReturnURLSplitPortalName;

                                string[] portalHostURLSplit = portalHostURL.Split('/');
                                string HostURLSplitPortal = portalHostURLSplit[0];
                                string HostURLSplitPortalName = portalHostURLSplit[1];
                                string HostURLWithPortal = HostURLSplitPortal + "/" + HostURLSplitPortalName;
                                resultComparePortalURL = string.Compare(ReturnURLWithPortal, HostURLWithPortal);
                            }
                            if (resultCompareURL != 0 || resultComparePortalURL != 0)
                            {
                                PageNotFoundURL();
                            }
                        }
                        else
                        {
                            PageNotFoundURL();
                        }
                    }

                    if (ReturnUrlPage == PageNotFoundPage || ReturnUrlPage == UserRegistrationPage || ReturnUrlPage == PasswordRecoveryPage || ReturnUrlPage == ForgotPasswordPage || ReturnUrlPage == PageNotAccessiblePage)
                    {
                        redirectURL.Append(GetParentURL);
                        redirectURL.Append(PortalAPI.DefaultPageWithExtension);
                    }
                    else
                    {
                        redirectURL.Append(ResolveUrl(Request.QueryString["ReturnUrl"].ToString()));
                    }
                }
                else
                {
                    if (!IsParent)
                    {
                        redirectURL.Append(GetParentURL);
                        redirectURL.Append("/portal/");
                        redirectURL.Append(GetPortalSEOName);
                        redirectURL.Append("/");
                        redirectURL.Append(PortalAPI.DefaultPageWithExtension);
                    }
                    else
                    {
                        redirectURL.Append(GetParentURL);
                        redirectURL.Append("/");
                        redirectURL.Append(PortalAPI.DefaultPageWithExtension);
                    }
                }
                HttpContext.Current.Session[SessionKeys.IsLoginClick] = true;
                if (Session[SessionKeys.LoginHitCount] != null)
                {
                    HttpContext.Current.Session.Remove(SessionKeys.LoginHitCount);
                }
                Response.Redirect(redirectURL.ToString(), false);
            }
            else
            {
                FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("UserLogin", "Youarenotauthenticatedtothisportal"));//"You are not authenticated to this portal!";
            }
        }
        private void PageNotFoundURL()
        {
            StringBuilder redirecPath = new StringBuilder();
            Uri url = HttpContext.Current.Request.Url;
            redirecPath.Append(url.Scheme);
            redirecPath.Append("://");
            redirecPath.Append(url.Authority);
            redirecPath.Append(PortalAPI.PageNotFoundURL);
            Response.Redirect(redirecPath.ToString());
        }
        private void InitializeCaptcha()
        {
            CaptchaValue.Text = "";
            if (this.Session[SessionKeys.CaptchaImageText] == null)
            {
                this.Session[SessionKeys.CaptchaImageText] = GenerateRandomCode();
            }
            cvCaptchaValue.ValueToCompare = this.Session[SessionKeys.CaptchaImageText].ToString();
            CaptchaImage.ImageUrl = "~/CaptchaImageHandler.aspx";
            Refresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
            rfvCaptchaValueValidator.Enabled = true;
        }
        private string GenerateRandomCode()
        {
            string s = "";
            string[] CapchaValue = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, CapchaValue[this.random.Next(36)]);
            return s;
        }

        private string GenerateRandomCookieValue()
        {
            string s = "";
            string[] CapchaValue = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int i = 0; i < 10; i++)
                s = String.Concat(s, CapchaValue[this.random.Next(36)]);
            return s;
        }
        protected void Refresh_Click(object sender, ImageClickEventArgs e)
        {
            CaptchaValue.Text = string.Empty;
            GenerateCaptchaImage();
        }
        private void GenerateCaptchaImage()
        {
            try
            {
                this.Session[SessionKeys.CaptchaImageText] = GenerateRandomCode();
                cvCaptchaValue.ValueToCompare = this.Session[SessionKeys.CaptchaImageText].ToString();
                CaptchaImage.ImageUrl = ResolveUrl("~") + "CaptchaImageHandler.aspx?=dummy" + DateTime.Now.ToLongTimeString();
                CaptchaValue.Text = "";
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        #region "OPenID"

        private string FirstName = "";
        private string LastName = "";
        public void CheckOpenID()
        {
            try
            {
                string email = string.Empty;
                if (Session[SessionKeys.ServiceProvider] != null && Session[SessionKeys.ServiceProvider].ToString() == "FaceBook")
                {
                    CheckFaceBook();// FaceBook
                    email = GetFaceBookEmail();// FaceBook        
                }
                if (Session[SessionKeys.ServiceProvider] != null && Session[SessionKeys.ServiceProvider].ToString() == "YahooGoogle")
                    email = GetEmail();// Yahoo and Google
                if (Session[SessionKeys.ServiceProvider] != null && Session[SessionKeys.ServiceProvider].ToString() == "LinkedIn")
                    email = LinkedInAuth();// LinkedIn
                if (email != null && email != string.Empty)
                {
                    CheckEmail(email);
                }
            }
            catch (Exception Ex)
            {
                ProcessException(Ex);
            }
        }
        protected void imgBtnLinkedIn_Click(object sender, ImageClickEventArgs e)
        {
            oAuthLinkedIn _oauth = new oAuthLinkedIn();
            string authLink = _oauth.AuthorizationLinkGet();
            try
            {
                Session[SessionKeys.ServiceProvider] = "LinkedIn";

                if (_oauth.Token != null && _oauth.TokenSecret != null && _oauth.Token.Length > 0 && _oauth.TokenSecret.Length > 0)
                {
                    Application["reuqestToken"] = _oauth.Token;
                    Application["reuqestTokenSecret"] = _oauth.TokenSecret;
                    Application["oauthLink"] = authLink;
                }
                else
                {
                    lblAlertMsg.Text = " LinkedIn keys Not Provided";
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            if (authLink != null)
            {
                Response.Redirect(authLink);
            }
        }
        protected void imgBtnFacebook_Click(object sender, ImageClickEventArgs e)
        {
            Session[SessionKeys.ServiceProvider] = "FaceBook";
            LoginFaceBook();
        }
        public void CheckFaceBook()
        {
            string redirect = HttpContext.Current.Request.Url.ToString().Split('?')[0];
            if (Request.QueryString["code"] != null)
            {
                string code = Request.QueryString["code"].ToString();
                oAuthFacebooks fbAC = new oAuthFacebooks(); //Standard FB class file available on net in c#
                string respnse = "";
                try
                {
                    fbAC.AccessTokenGet(code, redirect);
                    respnse = fbAC.Token;
                }
                catch (Exception exception)
                {
                    ProcessException(exception);
                }
                Response.Redirect(redirect + "?FaceBooktoken=" + respnse);
            }
        }
        public string GetFaceBookEmail()
        {
            string email = "";
            try
            {
                string token = Request.QueryString["FaceBooktoken"];
                if (token != string.Empty && token != null)
                {
                    string HitURL = string.Format("https://graph.facebook.com/me?access_token={0}", token);
                    oAuthFacebooks objFbCall = new oAuthFacebooks();
                    string JSONInfo = objFbCall.WebRequest(oAuthFacebooks.Method.GET, HitURL, "");

                    var objJson = JsonValue.Parse(JSONInfo);
                    if (objJson.Count > 0)
                    {
                        email = JsonParser.getString(objJson, "email");
                        string firstname = JsonParser.getString(objJson, "first_name");
                        string link = JsonParser.getString(objJson, "link");
                        string username = JsonParser.getString(objJson, "username");
                        string gender = JsonParser.getString(objJson, "gender");
                        string lastname = JsonParser.getString(objJson, "last_name");
                        FirstName = firstname;
                        LastName = lastname;
                        lblAlertMsg.Text = firstname + "Sucessfully Logged In with FaceBook  and the email Address is " + email;
                    }
                }
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
            Session.Remove(SessionKeys.ServiceProvider);
            return email;
        }
        protected void imgbtnFacebook_Click(object sender, ImageClickEventArgs e)
        {
            Session[SessionKeys.ServiceProvider] = "FaceBook";
            LoginFaceBook();
        }
        public void LoginFaceBook()
        {
            string redirectUrl = null;
            try
            {
                string facebookConsumerKey = pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.FaceBookConsumerKey);
                redirectUrl = @"https://graph.facebook.com/oauth/authorize?client_id=";
                redirectUrl += facebookConsumerKey + "&redirect_uri=";
                redirectUrl += HttpContext.Current.Request.UrlReferrer.AbsoluteUri + "&scope=email,publish_stream,offline_access,publish_actions";
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
            if (redirectUrl != null)
            {
                Response.Redirect(redirectUrl);
            }
        }
        protected void OpenLogin_Click(object src, CommandEventArgs e)
        {
            Session[SessionKeys.ServiceProvider] = "YahooGoogle";
            string discoveryUri = e.CommandArgument.ToString();
            ProviverRedirect(discoveryUri);
        }
        public void ProviverRedirect(string discoveryUri)
        {
            IAuthenticationRequest request = null;
            try
            {
                OpenIdRelyingParty openid = new OpenIdRelyingParty();
                var builder = new UriBuilder(Request.Url) { Query = "" };
                request = openid.CreateRequest(discoveryUri, builder.Uri, builder.Uri);
                switch (discoveryUri)
                {
                    case "https://www.google.com/accounts/o8/id":
                        var fetch = new FetchRequest();
                        fetch.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
                        fetch.Attributes.AddRequired(WellKnownAttributes.Name.First);
                        fetch.Attributes.AddRequired(WellKnownAttributes.Name.Last);
                        fetch.Attributes.AddRequired(WellKnownAttributes.Name.Middle);
                        fetch.Attributes.AddRequired(WellKnownAttributes.Person.Gender);
                        request.AddExtension(fetch);
                        break;
                    case "https://me.yahoo.com":
                        request.AddExtension(new ClaimsRequest
                        {

                            Email = DemandLevel.Request,
                            FullName = DemandLevel.Request
                        });
                        break;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            if (request != null)
            {
                request.RedirectToProvider();
            }
        }
        public string GetEmail()
        {
            string email = "";
            try
            {
                OpenIdRelyingParty rp = new OpenIdRelyingParty();
                string sessionid;
                if (Session[SessionKeys.Identifier] != null)
                {
                    sessionid = Convert.ToString(Session[SessionKeys.Identifier]);
                }
                var r = rp.GetResponse();

                if (r != null)
                {
                    switch (r.Status)
                    {
                        case AuthenticationStatus.Authenticated:

                            // this works for Yahoo and google
                            var fetch = r.GetExtension<FetchResponse>();
                            var response2 = fetch as FetchResponse;
                            string fullName;
                            email = response2.GetAttributeValue(WellKnownAttributes.Contact.Email);
                            FirstName = response2.GetAttributeValue(WellKnownAttributes.Name.First);
                            LastName = response2.GetAttributeValue(WellKnownAttributes.Name.Last);
                            fullName = response2.GetAttributeValue(WellKnownAttributes.Name.FullName);

                            if (fullName != null)
                            {
                                string[] name = fullName.Split(' ');
                                FirstName = name[0];
                                LastName = name[1];
                            }
                            break;
                        case AuthenticationStatus.Canceled:
                            lblAlertMsg.Text = "Cancelled.";
                            break;
                        case AuthenticationStatus.Failed:
                            lblAlertMsg.Text = "Login Failed.";
                            break;
                    }
                }
                Session.Remove(SessionKeys.ServiceProvider);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            return email;
        }
        public string LinkedInAuth()
        {
            string email = "";
            try
            {
                oAuthLinkedIn _oauth = new oAuthLinkedIn();
                string oauth_token = Request.QueryString["oauth_token"];
                string oauth_verifier = Request.QueryString["oauth_verifier"];

                if (oauth_token != null && oauth_verifier != null)
                {
                    Application["oauth_token"] = oauth_token;
                    Application["oauth_verifier"] = oauth_verifier;
                    _oauth.Token = oauth_token;
                    _oauth.TokenSecret = Application["reuqestTokenSecret"].ToString();
                    _oauth.Verifier = oauth_verifier;
                    _oauth.AccessTokenGet(oauth_token);

                    string emailResponse = _oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/people/~/email-address", null);
                    email = ParselinkedInXMl(emailResponse, "<email-address>", "</email-address>");
                    string nameResponse = _oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/people/~", null);
                    FirstName = ParselinkedInXMl(nameResponse, "<first-name>", "</first-name>");
                    LastName = ParselinkedInXMl(nameResponse, "<last-name>", "</last-name>");
                    lblAlertMsg.Text = email + " sucessfully Login with LinkedIn" + FirstName + "  " + LastName;
                    Session.Remove(SessionKeys.ServiceProvider);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            return email;
        }
        public string ParselinkedInXMl(string xml, string nodeStart, string nodeLast)
        {
            int start = xml.IndexOf(nodeStart);
            int last = xml.IndexOf(nodeLast);
            int length2 = last - start - nodeStart.Length;
            xml = xml.Substring(start + nodeStart.Length, length2);
            return xml;
        }
        public void CheckEmail(string email)
        {
            try
            {
                MembershipController member = new MembershipController();
                UserInfo objUser = member.GerUserByEmail(email, GetPortalID);
                if (objUser.IsApproved == true)
                {
                    SucessFullLogin(objUser);
                }
                else
                {
                    {
                        int UserRegistrationType = pagebase.GetSettingIntValueByIndividualKey(SageFrameSettingKeys.PortalUserRegistration);
                        bool isUserActive = UserRegistrationType == 2 ? true : false;
                        objUser.ApplicationName = Membership.ApplicationName;
                        objUser.FirstName = FirstName;
                        objUser.UserName = email;
                        objUser.LastName = LastName;
                        string Pwd, PasswordSalt;
                        string newPassword = GenerateRandomPassword();
                        PasswordHelper.EnforcePasswordSecurity(member.PasswordFormat, newPassword, out Pwd, out PasswordSalt);
                        objUser.Password = Pwd;
                        objUser.PasswordSalt = PasswordSalt;
                        objUser.Email = email;
                        objUser.SecurityQuestion = " ";
                        objUser.SecurityAnswer = " ";
                        objUser.IsApproved = true;
                        objUser.CurrentTimeUtc = DateTime.Now;
                        objUser.CreatedDate = DateTime.Now;
                        objUser.UniqueEmail = 0;
                        objUser.StoreID = GetStoreID;
                        objUser.PasswordFormat = member.PasswordFormat;
                        objUser.PortalID = GetPortalID;
                        objUser.AddedOn = DateTime.Now;
                        objUser.AddedBy = GetUsername;
                        objUser.UserID = Guid.NewGuid();
                        objUser.RoleNames = SystemSetting.REGISTER_USER_ROLENAME;
                        UserCreationStatus status = new UserCreationStatus();
                        CheckRegistrationType(UserRegistrationType, ref objUser);
                        MembershipDataProvider.CreatePortalUser(objUser, out status, UserCreationMode.REGISTER);
                        if (status == UserCreationStatus.SUCCESS)
                        {
                            SucessFullLogin(objUser);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private string GenerateRandomPassword()
        {
            Random random = new Random();
            string s = "";
            string[] CapchaValue = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, CapchaValue[random.Next(36)]);
            return s;
        }
        private void CheckRegistrationType(int UserRegistrationType, ref UserInfo user)
        {
            switch (UserRegistrationType)
            {
                case 0:
                    break;
                case 1:
                    user.IsApproved = false;
                    break;
                case 2:
                    user.IsApproved = true;
                    break;
                case 3:
                    user.IsApproved = false;
                    break;
            }
        }
        public void CreateErrorMessage(Exception exception, string message)
        {
            var stringBuilder = new StringBuilder();
            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);
                exception = exception.InnerException;
            }
            stringBuilder.ToString();
            lblAlertMsg.Text = message + " " + stringBuilder;
        }
        #endregion
    }
}
