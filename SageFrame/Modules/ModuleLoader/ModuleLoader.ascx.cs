using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_ModuleLoader_ModuleLoader : BaseUserControl
{
    public string userModuleID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        userModuleID = SageUserModuleID;
        IncludeJs("Image", "/Modules/ModuleLoader/js/ModuleLoader.js");
        if (!IsPostBack)
        {
            LoadModules();
        }
    }

    //In presence of Script Manager in UserControl, ModuleLoaderWithControl should be called in Page_Init 
    //and also include ScriptManager in that User Control  where the method is called.
    protected void Page_Init(object sender, EventArgs e)
    {
        //ModuleLoaderWithDynamicControl("Login", "view", userModuleID, false);
        //ModuleLoaderWithDynamicControl("Login", "view", userModuleID, pchUserControl);

    }

    public void LoadModules()
    {
        //ModuleLoaderWithDynamicControl("ModuleLoader", "edit", userModuleID, true);
        //ModuleLoaderWithDynamicControl("ModuleLoader", "edit", userModuleID, pchUserControl);
        //ModuleLoaderWithStaticControl("/Modules/ModuleLoader/ModuleLoaderEdit.ascx", userModuleID, true);
        //ModuleLoaderWithStaticControl("/Modules/ModuleLoader/ModuleLoaderEdit.ascx", userModuleID, pchUserControl);

    }
}