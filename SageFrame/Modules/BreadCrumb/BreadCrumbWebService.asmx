<%@ WebService Language="C#" Class="BreadCrumbWebService" %>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SageFrame.BreadCrum;

/// <summary>
/// Summary description for BreadCrumbWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class BreadCrumbWebService : System.Web.Services.WebService
{

    public BreadCrumbWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<BreadCrumInfo> GetBreadCrumb(int PortalID, string PageName, int MenuId, string CultureCode)
    {
        string breadcrumb = string.Empty;
        List<BreadCrumInfo> obj = new List<BreadCrumInfo>();
        BreadCrumDataProvider dp = new BreadCrumDataProvider();
       obj = dp.GetBreadCrumb(PageName, PortalID, MenuId, CultureCode);
        //return (obj.TabPath != string.Empty ? obj.TabPath : string.Empty);
        return obj;
    }

}


