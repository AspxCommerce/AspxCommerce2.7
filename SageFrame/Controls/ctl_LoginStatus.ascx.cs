/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;
using SageFrame.Common;

namespace SageFrame.Controls
{
    public partial class ctl_LoginStatus : SageUserControl
    {
        public string RegisterURL = string.Empty;
        public string profileURL = string.Empty;
        public string profileText = string.Empty;
        string Extension;
        public string userName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Extension = SageFrameSettingKeys.PageExtension;
            SageFrameConfig sfConfig = new SageFrameConfig();
            SecurityPolicy objSecurity = new SecurityPolicy();
            userName = objSecurity.GetUser(GetPortalID);
            if (!IsPostBack)
            {
                profileText = GetSageMessage("LoginStatus", "MyProfile");
                Literal lnkProfileUrl = (Literal)LoginView1.TemplateControl.FindControl("lnkProfileUrl");
                RegisterURL = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalRegistrationPage) + SageFrameSettingKeys.PageExtension;
                if (sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalShowProfileLink) == "1")
                {
                    if (!IsParent)
                    {
                        profileURL = "<a  href='" + GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalUserProfilePage) + SageFrameSettingKeys.PageExtension + "'>" + profileText + "</a>";
                    }
                    else
                    {
                        profileURL = "<a  href='" + GetParentURL + "/" + sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalUserProfilePage) + SageFrameSettingKeys.PageExtension + "'>" + profileText + "</a>";
                    }

                }
                else
                {
                    profileURL = "";
                }
                if (!IsParent)
                {
                    RegisterURL = GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalRegistrationPage) + SageFrameSettingKeys.PageExtension;
                }
                else
                {
                    RegisterURL = GetParentURL + "/" + sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalRegistrationPage) + SageFrameSettingKeys.PageExtension;
                }

            }
        }

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            SetUserRoles(string.Empty);
            SageFrameConfig sfConfig = new SageFrameConfig();
            if (!IsParent)
            {
                Response.Redirect(GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + SageFrameSettingKeys.PageExtension);
            }
            else
            {
                Response.Redirect(GetParentURL + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + SageFrameSettingKeys.PageExtension);
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
}