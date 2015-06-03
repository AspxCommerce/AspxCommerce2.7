<%@ WebService Language="C#" Class="SiteMapWebService" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.Pages;
using SageFrame.MenuManager;
using SageFrame.Services;
/// <summary>
/// Summary description for SiteMapWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class SiteMapWebService : AuthenticateService
{

    public SiteMapWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public List<MenuManagerInfo> GetSitemap(int PortalID, string UserName, string CultureCode, int UserModuleID, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<MenuManagerInfo> lstMenuItems = MenuManagerDataController.GetSageMenu(UserModuleID, PortalID, UserName);

            IEnumerable<MenuManagerInfo> lstParent = new List<MenuManagerInfo>();
            List<MenuManagerInfo> lstHierarchy = new List<MenuManagerInfo>();
            lstParent = from pg in lstMenuItems
                        where pg.MenuLevel == "0"
                        select pg;
            foreach (MenuManagerInfo parent in lstParent)
            {
                lstHierarchy.Add(parent);
                GetChildPages(ref lstHierarchy, parent, lstMenuItems);
            }

            return (lstHierarchy);
        }
        else
        {
            return null;
        }
    }
    public void GetChildPages(ref List<MenuManagerInfo> lstHierarchy, MenuManagerInfo parent, List<MenuManagerInfo> lstPages)
    {
        foreach (MenuManagerInfo obj in lstPages)
        {
            if (obj.ParentID == parent.MenuItemID)
            {
                lstHierarchy.Add(obj);
                GetChildPages(ref lstHierarchy, obj, lstPages);
            }
        }
    }

}


