<%@ WebService Language="C#" Class="CDNWebService" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.CDN;
using SageFrame.Services;

/// <summary>
/// Summary description for CDNWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class CDNWebService : AuthenticateService
{
    /// <summary>
    /// Default Constructor
    /// </summary>
    public CDNWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    /// <summary>
    /// Saves CDNInfo links
    /// </summary>
    /// <param name="CDNInfo">CDNInfo Objects</param>
    /// <param name="portalID">Portal</param>
    /// <param name="userModuleID"></param>
    /// <param name="userName"></param>
    /// <param name="secureToken"></param>
    [WebMethod]
    public void SaveLinks(List<CDNInfo> CDNInfo, int portalID, int userModuleID, string userName, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            CDNController obj = new CDNController();
            obj.SaveLinks(CDNInfo);
        }
    }
    [WebMethod]
    public List<CDNInfo> GetCDNLinks(int portalID, int userModuleID, string userName, string secureToken)
    {

        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            CDNController obj = new CDNController();
            return obj.GetCDNLinks(portalID);
        }
        else
        {
            return null;
        }

    }
    [WebMethod]
    public void SaveOrder(List<CDNInfo> objOrder, int portalID, int userModuleID, string userName, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            CDNController objController = new CDNController();
            objController.SaveOrder(objOrder);
        }
    }
    [WebMethod]
    public void DeleteURL(int UrlID, int portalID, int userModuleID, string userName, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            CDNController objController = new CDNController();
            objController.DeleteURL(UrlID, portalID);
        }
    }
}


