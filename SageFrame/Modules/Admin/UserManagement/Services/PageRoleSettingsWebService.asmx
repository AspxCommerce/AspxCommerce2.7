<%@ WebService Language="C#" CodeBehind="~/App_Code/PageRoleSettingsWebService.cs" Class="PageRoleSettingsWebService" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.Services;
using SageFrame.Security.Entities;
using SageFrame.Security;
using SageFrame.Pages;
using SageFrame.PagePermission;
/// <summary>
/// Summary description for PageRoleSettingsWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class PageRoleSettingsWebService : AuthenticateService
{

    public PageRoleSettingsWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public List<RoleInfo> GetPortalRoles(int portalID, string userName, int userModuleID, string secureToken)
    {
        List<RoleInfo> objRoleInfos = new List<RoleInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            RoleController _role = new RoleController();
            objRoleInfos = _role.GetPortalRoles(portalID, 1, userName);
        }
        return objRoleInfos;
    }

    [WebMethod]
    public List<PageEntity> GetNormalPage(int PortalID, string UserName, string CultureCode, string secureToken, int userModuleID)
    {
        List<PageEntity> lstMenu = new List<PageEntity>();
        if (IsPostAuthenticatedView(PortalID, userModuleID, UserName, secureToken))
        {
            PageController objPageController = new PageController();
            lstMenu = objPageController.GetMenuFront(PortalID, false);
            //if (IsPostAuthenticated(PortalID, userModuleId, UserName))
            //{
            foreach (PageEntity obj in lstMenu)
            {
                obj.ChildCount = lstMenu.Count(
                    delegate(PageEntity objMenu)
                    {
                        return (objMenu.ParentID == obj.PageID);
                    }
                    );
            }
        }
        //}
        return lstMenu;
    }

    [WebMethod]
    public List<PageRoleSettingsInfo> GetPageDetailsByRoleID(Guid RoleID, int PortalID, string userName, int userModuleID, string secureToken)
    {
        try
        {
            List<PageRoleSettingsInfo> lstPages = new List<PageRoleSettingsInfo>();
            if (IsPostAuthenticatedView(PortalID, userModuleID, userName, secureToken))
            {
                PageController objPage = new PageController();
                lstPages = objPage.GetPagePermissionByRoleID(RoleID, PortalID);

            }
            return lstPages;
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
        
    }
     
    [WebMethod]
    public void AddUpdatePageRolePermission(List<PageRoleSettingsInfo> lstPagePermission,int portalID, string addedBy, bool isAdmin, string userName, int userModuleID, string secureToken)
    {
        try
        {

            if (IsPostAuthenticated(portalID, userModuleID, userName, secureToken))
            {
                PageController objCon = new PageController();
                objCon.AddUpdatePageRolePermission(lstPagePermission,portalID, addedBy, isAdmin);
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
