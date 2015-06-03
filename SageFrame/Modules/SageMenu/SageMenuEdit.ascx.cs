#region "References"
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
using SageFrame.SageMenu;
using System.Collections.Generic;
using SageFrame.Pages;
using System.Text;
using SageFrame.Security;
using SageFrame.Common;
#endregion

public partial class Modules_SageMenu_SageMenuEdit : BaseAdministrationUserControl
{
    string appPath = string.Empty;
    public string CultureCode = string.Empty;
    public int UserModuleID;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserModuleID = int.Parse(SageUserModuleID);
        appPath = GetApplicationName;
        CultureCode = GetCurrentCulture();
        IncludeJs("SageMenu", "/Modules/SageMenu/js/MenuManager.js");
        IncludeJs("SageMenu", false, "/Editors/ckeditor/ckeditor.js", "/Editors/ckeditor/adapters/jquery.js", "/Administrator/Templates/Default/js/ui.tree.js", "/Administrator/Templates/Default/js/contextmenu.js");
        IncludeJs("SageMenu", false, "/Administrator/Templates/Default/js/ajaxupload.js", "/js/jquery.validate.js", "/js/jquery.alerts.js");
        IncludeCss("MenuManager", "/Modules/Admin/MenuManager/css/module.css", "/Administrator/Templates/Default/css/ui.tree.css", "/css/jquery.alerts.css");
        BuildAccessControlledSelection();
        AddImageUrls(); 
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + UserModuleID + "';", true);
      
    }

    protected void BuildAccessControlledSelection()
    {
        StringBuilder sb = new StringBuilder();
        RoleController _role = new RoleController();
        string[] roles = _role.GetRoleNames(GetUsername, GetPortalID).ToLower().Split(',');
        if (roles.Contains(SystemSetting.SUPER_ROLE[0].ToLower()))
        {
            sb.Append("<label class='sfActive'>");
            sb.Append("<input id='rdbPages' type='radio' name='rdbMenuItem' value='0' style='display:none'  />");
            sb.Append("Pages</label>");
            sb.Append("<label>");
            sb.Append("<input id='rdbExternalLink' type='radio' name='rdbMenuItem' value='2' style='display:none' />");
            sb.Append("External Link</label>");
            sb.Append("<label>");
            sb.Append("<input id='rdbHtmlContent' type='radio' name='rdbMenuItem' value='1' style='display:none' />");
            sb.Append("HTML Content</label>");
        }
        else
        {
            sb.Append("<label class='sfActive'>");
            sb.Append("<input id='rdbPages' type='radio' name='rdbMenuItem' value='0' style='display:none' />");
            sb.Append("Pages</label>");
            sb.Append("<label>");
            sb.Append("<input id='rdbExternalLink' type='radio' name='rdbMenuItem' value='2' style='display:none' />");
            sb.Append("External Link</label>");
            sb.Append("<label>");
            sb.Append("<input id='rdbHtmlContent' type='radio' name='rdbMenuItem' value='1' style='display:none' />");
            sb.Append("HTML Content</label>");
        }
        ltrMenuRadioButtons.Text = sb.ToString();
    }

    private void AddImageUrls()
    {
        string imageFolder = "~/Administrator/Templates/Default/images/";
        imgRemove.Src = GetImageUrl(imageFolder, "context-delete.png", true);
    }
    public string GetImageUrl(string _imageFolder, string imageName, bool isServerControl)
    {
        string path = string.Empty;
        if (isServerControl == true)
        {
            path = _imageFolder + imageName;
        }
        return path;
    }
}