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
using SageFrame.Framework;
using SageFrame.Common;
using System.Web.SessionState;
#endregion

public partial class Modules_Admin_LoginControl_LoginStatus : SageUserControl
{
    public string RegisterURL = string.Empty;
    public string profileURL = string.Empty;
    public string profileText = string.Empty;
    string Extension;
    protected void Page_Load(object sender, EventArgs e)
    {

        IncludeLanguageJS();
        Extension = SageFrameSettingKeys.PageExtension;
        SageFrameConfig sageConfig = new SageFrameConfig();
        profileText = GetSageMessage("LoginStatus", "MyProfile");
        Literal lnkProfileUrl = (Literal)LoginView1.TemplateControl.FindControl("lnkProfileUrl");
        RegisterURL = sageConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalRegistrationPage) + Extension;
        if (sageConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalShowProfileLink) == "1")
        {
            string profilepage = sageConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalUserProfilePage);
            profilepage = profilepage.ToLower().Equals("user-profile")
                              ? string.Format("/sf/{0}", profilepage)
                              : string.Format("/{0}", profilepage);
            if (!IsParent)
            {
                profileURL = "<a  href='" + ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + profilepage + Extension) + "'>" +
                             profileText + "</a>";
            }
            else
            {
                profileURL = "<a  href='" + ResolveUrl(GetParentURL + profilepage + Extension) + "'>" + profileText + "</a>";
            }
        }
        else
        {
            profileURL = string.Empty;
        }
        string userName = GetUsername;
        if (userName.ToLower() == "anonymoususer")
        {
            divAnonymousTemplate.Visible = true;
            divLoggedInTemplate.Visible = false;
            userName = "Guest";
        }
        else
        {
            divAnonymousTemplate.Visible = false;
            divLoggedInTemplate.Visible = true;
        }
        //  Label lblWelcomeMsg = LoginView1.FindControl("lblWelcomeMsg") as Label;
        // lblWelcomeMsg.Text = "<h2><span onload='GetMyLocale(this)'>Welcome " + userName + "!</span></h2>";

        lblWelcomeMsg.Text = lblWelcomeMsg.Text + " " + userName;

        if (!IsParent)
        {
            RegisterURL = ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/" + sageConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalRegistrationPage) + Extension);
        }
        else
        {
            RegisterURL = ResolveUrl(GetParentURL + "/" + sageConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalRegistrationPage) + Extension);
        }
        int UserRegistrationType = sageConfig.GetSettingIntValueByIndividualKey(SageFrameSettingKeys.PortalUserRegistration);
        if (UserRegistrationType > 0)
        {
            RegisterURL = "<span><a href='" + RegisterURL + "'><i class=\"i-register\"></i>" + GetSageMessage("LoginStatus", "Register") + "</a></span>";
        }
        else
        {
            RegisterURL = "";
        }
    }

    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        SetUserRoles(string.Empty);
        SageFrameConfig sageConfig = new SageFrameConfig();
      
        //create new sessionID
        SessionIDManager manager = new SessionIDManager();
        manager.RemoveSessionID(System.Web.HttpContext.Current);
        var newId = manager.CreateSessionID(System.Web.HttpContext.Current);
        var isRedirected = true;
        var isAdded = true;
        manager.SaveSessionID(System.Web.HttpContext.Current, newId, out isRedirected, out isAdded);
        Session.Remove("Auth_Token");

        //Catch activity log            
        if (!IsParent)
        {
            Response.Redirect(GetParentURL + "/portal/" + GetPortalSEOName + "/" + sageConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + Extension);
        }
        else
        {
            Response.Redirect(GetParentURL + "/" + sageConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + Extension);
        }

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
