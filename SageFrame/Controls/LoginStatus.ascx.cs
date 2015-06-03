/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SageFrame.Web;
using SageFrame.Framework;
using SageFrame.Security;
using SageFrame.Common;
using System.Web.SessionState;

public partial class LoginStatus : BaseAdministrationUserControl
{
    string Extension;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            IncludeLanguageJS();
            Extension = SageFrameSettingKeys.PageExtension;
            SecurityPolicy objSecurity = new SecurityPolicy();
            FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
            if (ticket != null)
            {
                int LoggedInPortalID = int.Parse(ticket.UserData.ToString());
                if (ticket.Name != ApplicationKeys.anonymousUser)
                {
                    string[] sysRoles = SystemSetting.SUPER_ROLE;
                    if (GetPortalID == LoggedInPortalID || Roles.IsUserInRole(ticket.Name, sysRoles[0]))
                    {
                        RoleController _role = new RoleController();
                        string userinroles = _role.GetRoleNames(GetUsername, LoggedInPortalID);
                        if (userinroles != string.Empty || userinroles != null)
                        {

                        }
                        else
                        {
                            lnkloginStatus.Text = SageLogInText;
                            lnkloginStatus.CommandName = "LOGIN";
                        }
                    }
                    else
                    {
                        lnkloginStatus.Text = SageLogInText;
                        lnkloginStatus.CommandName = "LOGIN";
                    }
                    lnkloginStatus.Text = SageLogOutText;
                    lnkloginStatus.CommandName = "LOGOUT";
                }
                else
                {
                    lnkloginStatus.Text = SageLogInText;
                    lnkloginStatus.CommandName = "LOGIN";
                }
            }
            else
            {
                lnkloginStatus.Text = SageLogInText;
                lnkloginStatus.CommandName = "LOGIN";
            }
        }
        catch
        {
        }
    }
    private string strLogOut = string.Empty;
    private string strLogIn = string.Empty;
    public string SageLogInText
    {
        get
        {
            if (strLogIn == string.Empty)
            {
                strLogIn = GetSageMessage("LoginStatus", "Login");
            }
            return strLogIn;
        }
    }
    public string SageLogOutText
    {
        get
        {
            if (strLogOut == string.Empty)
            {
                strLogOut = GetSageMessage("LoginStatus", "Logout");
            }
            return strLogOut;
        }
    }

    protected void lnkloginStatus_Click(object sender, EventArgs e)
    {
        try
        {
   
            SageFrameConfig SageConfig = new SageFrameConfig();
            SageFrameSettingKeys.PageExtension = SageConfig.GetSettingsByKey(SageFrameSettingKeys.SettingPageExtension);
            bool EnableSessionTracker = bool.Parse(SageConfig.GetSettingsByKey(SageFrameSettingKeys.EnableSessionTracker));

            SessionTracker sessionTrackerNew = new SessionTracker();
            if (EnableSessionTracker)
            {
                string sessionID = HttpContext.Current.Session.SessionID;
                SageFrame.Web.SessionLog sLogNew = new SageFrame.Web.SessionLog();
                sLogNew.SessionLogStart(sessionTrackerNew, sessionID);
            }           
            string ReturnUrl = string.Empty;
            string RedUrl = string.Empty;
            SageFrameConfig sfConfig = new SageFrameConfig();
            if (lnkloginStatus.CommandName == "LOGIN")
            {

                if (Request.QueryString["ReturnUrl"] == null)
                {
                    ReturnUrl = Request.RawUrl.ToString();
                    if (!(ReturnUrl.ToLower().Contains(SageFrameSettingKeys.PageExtension)))
                    {
                        //ReturnUrl = ReturnUrl.Remove(strURL.LastIndexOf('/'));
                        if (ReturnUrl.EndsWith("/"))
                        {
                            ReturnUrl += sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage).Replace(" ", "-") + SageFrameSettingKeys.PageExtension;
                        }
                        else
                        {
                            ReturnUrl += '/' + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage).Replace(" ", "-") + SageFrameSettingKeys.PageExtension;
                        }
                    }
                }
                else
                {
                    ReturnUrl = Request.QueryString["ReturnUrl"].ToString();
                }
                if (!IsParent)
                {
                    RedUrl = GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + SageFrameSettingKeys.PageExtension;

                }
                else
                {
                    RedUrl = GetParentURL + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + SageFrameSettingKeys.PageExtension;
                }

            }
            else
            {

                if (EnableSessionTracker)
                {
                    SageFrame.Web.SessionLog sLog = new SageFrame.Web.SessionLog();
                    sLog.SessionLogEnd(GetPortalID);
                }

                SecurityPolicy objSecurity = new SecurityPolicy();
                HttpCookie authenticateCookie = new HttpCookie(objSecurity.FormsCookieName(GetPortalID));
                authenticateCookie.Expires = DateTime.Now.AddYears(-1);
                string randomCookieValue = GenerateRandomCookieValue();
                HttpContext.Current.Session[SessionKeys.RandomCookieValue] = randomCookieValue;
                Response.Cookies.Add(authenticateCookie);
                lnkloginStatus.Text = "Login";
                SetUserRoles(string.Empty);
                //create new sessionID
                SessionIDManager manager = new SessionIDManager();
                manager.RemoveSessionID(System.Web.HttpContext.Current);
                var newId = manager.CreateSessionID(System.Web.HttpContext.Current);
                var isRedirected = true;
                var isAdded = true;
                manager.SaveSessionID(System.Web.HttpContext.Current, newId, out isRedirected, out isAdded);

                if (!IsParent)
                {
                    RedUrl = GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage).Replace(" ", "-") + SageFrameSettingKeys.PageExtension;
                }
                else
                {
                    RedUrl = GetParentURL + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage).Replace(" ", "-") + SageFrameSettingKeys.PageExtension;
                }
            }
            CheckOutHelper cHelper = new CheckOutHelper();
            cHelper.ClearSessions();            

            FormsAuthentication.SignOut();
            Response.Redirect(RedUrl, false);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private string GenerateRandomCookieValue()
    {
        Random random = new Random();
        string s = "";
        string[] CapchaValue = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        for (int i = 0; i < 10; i++)
            s = String.Concat(s, CapchaValue[random.Next(36)]);
        return s;
    }
    public void SetUserRoles(string strRoles)
    {
        Session[SessionKeys.SageUserRoles] = strRoles;
        HttpCookie cookie = HttpContext.Current.Request.Cookies[CookiesKeys.SageUserRolesCookie];
        if (cookie == null)
        {
            cookie = new HttpCookie(CookiesKeys.SageUserRolesCookie);
        }
        cookie[CookiesKeys.SageUserRolesProtected] = strRoles;
        HttpContext.Current.Response.Cookies.Add(cookie);
    }
}
