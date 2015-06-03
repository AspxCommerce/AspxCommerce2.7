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
using SageFrame.Security;
#endregion


public partial class Modules_Scheduler_ScheduleView : BaseAdministrationUserControl
{
    public static string ImagePath = "";
    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageFrameGlobalVar12", " var SchedularModuleID='" + int.Parse(SageUserModuleID) + "';", true);
        ImagePath = this.ResolveUrl(this.AppRelativeTemplateSourceDirectory.ToString());
        Initialize();
    }

    #region Methods

    public void Initialize()
    {
        //IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/admin.css");
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/facebox.css");
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/style.css");
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/module.css");

        IncludeCss("Scheduler", "/Modules/Admin/Scheduler/css/module.css");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "GlobalVariable1", " var SchedularModuleFilePath='" + ResolveUrl(ImagePath) + "';", true);

        //Page.ClientScript.RegisterClientScriptInclude("JQueryFacebox", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/jquery-1.3.2.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryAlertEase", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/facebox.js"));
        Page.ClientScript.RegisterClientScriptInclude("json2", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/json2.js"));
        Page.ClientScript.RegisterClientScriptInclude("JGrid", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/jquery.grid.js"));
        Page.ClientScript.RegisterClientScriptInclude("JSagePaging1", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/SagePaging.js"));
        Page.ClientScript.RegisterClientScriptInclude("JGlobal", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/jquery.global.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryValidater", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/jquery.validate.js"));
        Page.ClientScript.RegisterClientScriptInclude("JdateFormat", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/jquery.dateFormat.js"));
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/jquery.tablesorter.js"));

        Page.ClientScript.RegisterClientScriptInclude("JGrid", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/jquery.grid.js"));
        Page.ClientScript.RegisterClientScriptInclude("JSagePaging", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/SagePaging.js"));
        Page.ClientScript.RegisterClientScriptInclude("JGlobal1", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/jquery.global.js"));
        Page.ClientScript.RegisterClientScriptInclude("JdateFormat", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/jquery.dateFormat.js"));
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/GridView/jquery.tablesorter.js"));

        Page.ClientScript.RegisterClientScriptInclude("JQueryJson", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/json2.js"));
        //Page.ClientScript.RegisterClientScriptInclude("JQueryUI", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/jquery-ui.min.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryToolTip2", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/jquery.datepick.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryMaskValidater", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/jquery.maskedinput-1.3.min.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryMaskValidaterq", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/jquery.easing.1.3.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryMaskValidaterqw", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/alertbox.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryAjaxUpload", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/ajaxupload.js"));


        Page.ClientScript.RegisterClientScriptInclude("JQueryToolTip1", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "Scripts/SchedularForm.js"));

    }
    #endregion



}
