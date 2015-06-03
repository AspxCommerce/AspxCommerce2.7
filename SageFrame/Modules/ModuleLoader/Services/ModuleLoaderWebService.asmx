<%@ WebService Language="C#" Class="ModuleLoaderWebService" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.ContactUs;
using System.Web.UI;
using System.IO;
using SageFrame.Web;
using SageFrame.ModuleLoader;


/// <summary>
/// Summary description for ModuleLoaderWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ModuleLoaderWebService : System.Web.Services.WebService
{

    public ModuleLoaderWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod(EnableSession = true)]
    public string ModuleLoaderWithDynamicControl(string ModuleName, string ControlTypeName, string UserModuleID)
    {
        SageUserControl obj = new SageUserControl();
        return obj.ModuleLoaderForJsWithDynamicControl(ModuleName, ControlTypeName, UserModuleID);
    }

    [WebMethod(EnableSession = true)]
    public string ModuleLoaderWithStaticControl(string ControlPath, string UserModuleID)
    {
        SageUserControl obj = new SageUserControl();
        return obj.ModuleLoaderForJsWithStaticControl(ControlPath, UserModuleID);
    }
    
}

