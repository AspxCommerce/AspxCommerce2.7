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
using System.Collections;
using SageFrame.FileManager;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
#endregion

public partial class Modules_FileManager_FileMgr : BaseAdministrationUserControl
{
    public int UserID = 0;
    public int UserModuleID = 0;
    public string UserName = "";
    public string ImgPath = "";
    public string hostdirName = "";

    public void Initialize()
    {
        IncludeJs("FileManager", "/Modules/TemplateFileManager/js/jqueryFileTree.js", "/Modules/FileManager/js/ajaxupload.js", "/Modules/FileManager/js/jquery.lightbox-0.5.js");
        IncludeJs("FileManager", "/Modules/TemplateFileManager/js/UploadFileJScript.js", "/Modules/FileManager/js/jquery.imgareaselect.js");
        IncludeJs("FileManager", "/Modules/TemplateFileManager/js/jquery.tools.min.js");
        IncludeCss("FileManager", "/Modules/TemplateFileManager/css/module.css", "/Modules/FileManager/css/popup.css", "/Modules/FileManager/css/jqueryFileTree.css", "/Modules/FileManager/css/popup.css", "/Modules/FileManager/css/jquery.lightbox-0.5.css");
        IncludeCss("FileManager", "/Modules/TemplateFileManager/css/imgareaselect-animated.css", "/Modules/FileManager/css/FileUploaderStyleSheet.css");
        IncludeJs("FileManager", "/Modules/TemplateFileManager/JS/jquery.alerts.js");

        IncludeJs("FileManager", false, "/Modules/TemplateFileManager/CodeMirror/codemirror.js");
        IncludeJs("FileManager", "/Modules/TemplateFileManager/CodeMirror/xml.js");
        IncludeJs("FileManager", "/Modules/TemplateFileManager/CodeMirror/css.js");
        IncludeJs("FileManager", "/Modules/TemplateFileManager/CodeMirror/scheme.js");
        IncludeCss("FileManager", "/Modules/TemplateFileManager/CodeMirror/codemirror.css");
        IncludeCss("FileManager", "/Modules/TemplateFileManager/CodeMirror/default.css");
        IncludeCss("FileManager", "/Modules/TemplateFileManager/css/jcrop.css");
        IncludeJs("FileManager", "/Modules/TemplateFileManager/js/jquery.Jcrop.js");
        IncludeJs("FileManager", "/Modules/TemplateFileManager/js/crop.js");
        IncludeJs("FileManager", "/js/jquery.alerts.js");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "templateFileManagerUserModuleID", " var templateFileManagerUserModuleID='" + SageUserModuleID + "';", true);
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (ViewState["UserID"] != null)
        {
            UserID = int.Parse(ViewState["UserID"].ToString());
        }
        else
        {
            UserID = FileManagerController.GetUserID(GetUsername);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        hostdirName = HttpRuntime.AppDomainAppPath;
        hostdirName = new DirectoryInfo(hostdirName).Name;
        Initialize();
        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "FileManagerGlobalVariable1", " var FileManagerPath='" + ResolveUrl(modulePath) + "';", true);
        UserModuleID = int.Parse(SageUserModuleID.ToString());
        UserName = GetUsername;

    }

}
