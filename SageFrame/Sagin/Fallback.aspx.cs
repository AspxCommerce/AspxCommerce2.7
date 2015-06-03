#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using SageFrame.Templating;
using SageFrame.Web;
using SageFrame.Framework;
using SageFrame.Common;

#endregion

public partial class Sagin_Fallback : PageBase
{
    #region "Public Variables"

    public int PortalID = 0;
    public string appPath = string.Empty;
    public string fallBackPath = string.Empty;
    public string Extension;
    public string templateFavicon = string.Empty;

    #endregion

    #region "Event Handlers"

    protected void Page_Load(object sender, EventArgs e)
    {
        templateFavicon = SetFavIcon(GetActiveTemplate);
        imgLogo.ImageUrl = ResolveUrl("~/") + "images/sageframe.png";
        Extension = SageFrameSettingKeys.PageExtension;
        SageFrameConfig sfConfig = new SageFrameConfig();
        appPath = GetAppPath();
        string pagePath = ResolveUrl(GetParentURL) + GetReduceApplicationName;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "BreadCrumGlobal1", " var BreadCrumPagePath='" + pagePath + "';", true);
        pagePath = IsParent ? pagePath : pagePath + "portal/" + GetPortalSEOName;
        PortalID = GetPortalID;
        if (!IsParent)
        {
            fallBackPath = GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + Extension;
        }
        else
        {
            fallBackPath = GetParentURL + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + Extension;
        }
        if (Session[SessionKeys.TemplateError] != null)
        {
            Exception ex = Session[SessionKeys.TemplateError] as Exception;
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<h3>{0}</h3>", ex.Message));
            sb.Append(string.Format("<p>{0}</p>", ex.ToString()));
            ltrErrorMessage.Text = sb.ToString();
        }
    }
    #endregion
    protected void btnFallback_Click(object sender, EventArgs e)
    {
        SageFrame.Templating.TemplateController.ActivateTemplate("default", PortalID);
        Response.Redirect(fallBackPath);
    }
}