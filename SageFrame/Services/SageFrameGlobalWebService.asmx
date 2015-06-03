<%@ WebService Language="C#" Class="SageFrameGlobalWebService" %>
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SageFrame.Web;

/// <summary>
/// Summary description for SageFrameGlobalWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class SageFrameGlobalWebService : System.Web.Services.WebService
{

    public SageFrameGlobalWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetLocalizedMessage(string CultureCode, string ModuleName, string MessageType)
    {
        return (SageMessage.ProcessSageMessage(CultureCode, ModuleName, MessageType));
    }
    //[WebMethod]
    //public void ActivateDefaultTemplate(int PortalID)
    //{
    //    SageFrame.Templating.TemplateController.ActivateTemplate("default", PortalID);
    //}
}