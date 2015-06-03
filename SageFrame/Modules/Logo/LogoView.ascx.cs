#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
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
using SageFrame.Web;
using SageFrame.Logo;
using System.Text;
#endregion

public partial class Modules_Logo_LogoView : SageUserControl
{
    public int moduleID, portalID;
    public string culture, userName, resolvedUrl, currentDirectory;
    public string ContainerClientID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        IncludeCss("Logo", "/Modules/Logo/css/module.css");
       
        moduleID = UserModuleID;
        portalID = GetPortalID;
        resolvedUrl = ResolveUrl("~/");
        currentDirectory = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        CreateDynamicNav();
    }

    public int UserModuleID
    {
        get
        {
            if (!string.IsNullOrEmpty(SageUserModuleID))
            {
                moduleID = Int32.Parse(SageUserModuleID);
            }
            return moduleID;
        }
    }

    public void CreateDynamicNav()
    {
        LogoController objController = new LogoController();
        LogoEntity objLogo = objController.GetLogoData(moduleID, portalID, GetCurrentCulture());
        string navUrl = string.Empty;
        string imagePath = string.Empty;
        StringBuilder elem = new StringBuilder();
        if (objLogo != null)
        {
            {
                if (objLogo.url == "http://" || objLogo.url == "")
                {
                    navUrl = "";
                }
                else
                {
                    navUrl = "href= '"+ GetHostURL() +"/"+ objLogo.url + "'";
                }
                if (objLogo.LogoPath != "")
                {
                    imagePath = GetApplicationName + "/" + objLogo.LogoPath;
                }
                ContainerClientID = "divNav_" + moduleID;
                elem.Append("<div class='sfLogo' id='");
                elem.Append(ContainerClientID);
                elem.Append("'>");
                elem.Append("<a ");
                elem.Append(navUrl);
                elem.Append(">");
                if (imagePath != string.Empty)
                {
                    elem.Append("<img id='imgLogo' class='sfLogoimg' src='");
                    elem.Append(imagePath);
                    elem.Append("' alt=''/>");
                }
                elem.Append("<span>");
                elem.Append(objLogo.LogoText);
                elem.Append("</span></a>");
                if (objLogo.Slogan.Trim() != string.Empty)
                {
                    elem.Append("<span>");
                    elem.Append(objLogo.Slogan);
                    elem.Append("</span>");
                }
                elem.Append("</div>");
                ltrLogo.Text = elem.ToString();
            }
        }
    }
}