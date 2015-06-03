using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_NewsLetter_NewsLetterEdit :BaseAdministrationUserControl
{
    public string ModulePath = "";
    public int UserModuleID = 0;
    public int PortalID = 0;
    public string UserName = "", CultureName = "", resolvedURL = "", PassURL="";
    public string PageExtension = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageExtension = SageFrameSettingKeys.PageExtension;
        UserName = GetUsername;
        UserModuleID = Int32.Parse(SageUserModuleID);
        PortalID = GetPortalID;
        CultureName = GetCurrentCultureName;
        ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        this.resolvedURL = ResolveUrl("~/");
        PassURL = Request.Url.GetLeftPart(UriPartial.Path).Replace("/Sagin/" + GetPageSEOName(Request.Url.GetLeftPart(UriPartial.Path)) +SageFrameSettingKeys.PageExtension, "").ToString();
        IncludeCss("stylesheet", "/Modules/NewsLetter/css/module.css");
        IncludeJs("nledit", false, "/Editors/ckeditor/ckeditor.js", "/Editors/ckeditor/adapters/jquery.js", "/Modules/NewsLetter/js/NewsLetterEdit.js", "/Modules/NewsLetter/js/jquery.validate.js");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + SageUserModuleID + "';", true);

    }
}