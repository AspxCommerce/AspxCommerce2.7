using System;
using System.Text;
using System.Collections.Generic;
using SageFrame.Web;
using SageFrame.TaskToDo;


public partial class Modules_TaskToDo_TaskToDoView : BaseUserControl
{
    public string BaseUrl;
    public int UserModuleId; 
   
    protected void Page_Load(object sender, EventArgs e)
    {
        BaseUrl = ResolveUrl(this.AppRelativeTemplateSourceDirectory);       
        UserModuleId = int.Parse(SageUserModuleID);             
        IncludeLanguageJS();
        IncludeCss("TaskToDo", "/css/jquery.alerts.css", "/Modules/Admin/TaskToDo/css/module.css");
        IncludeJs("TaskToDo", "/Modules/Admin/TaskToDo/js/TaskToDo.js", "/js/jquery.alerts.js", "/js/jquery.validate.js",
            "/Modules/Admin/TaskToDo/js/quickpager.jquery.js");
     
    } 
    
}