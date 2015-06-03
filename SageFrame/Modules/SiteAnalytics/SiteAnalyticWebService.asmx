<%@ WebService Language="C#" Class="DashBoardWebService" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DashBoardControl.Info;
using DashBoardControl;
using SageFrame.Framework;
using SageFrame.Services;

/// <summary>
/// Summary description for DashBoardWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class DashBoardWebService : AuthenticateService
{
    IPAddressToCountryResolver objIP = new IPAddressToCountryResolver();
    public DashBoardWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<DashBoardSettingInfo> LoadTopBrowser(string StartDate, string EndDate, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<DashBoardSettingInfo> lstTopBrowser = DashBoardDataProvider.GetTopFiveBrowser(StartDate, EndDate);
            return lstTopBrowser;
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<DashBoardSettingInfo> LoadTopVisitedPage(string StartDate, string EndDate, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<DashBoardSettingInfo> lstTopVisitedPage = DashBoardDataProvider.GetTopFiveVisitedPage(StartDate, EndDate);
            return lstTopVisitedPage;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public List<DashBoardSettingInfo> GetTopFiveCountryName(string StartDate, string EndDate, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<DashBoardSettingInfo> lstTopVisitedCountry = DashBoardDataProvider.GetTopFiveVisitedCountry(StartDate, EndDate);
            string CountryName;
            foreach (DashBoardSettingInfo obj in lstTopVisitedCountry)
            {
                objIP.GetCountry(obj.Country, out CountryName);
                if (CountryName == null)
                {
                    obj.Country = obj.Country;
                }
                else
                {
                    obj.Country = CountryName;
                }
            }
            return lstTopVisitedCountry;
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<DashBoardSettingInfo> GetCountryName(string StartDate, string EndDate, int pageNo, int range, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<DashBoardSettingInfo> lstTopVisitedCountry = DashBoardDataProvider.GetTopVisitedCountry(StartDate, EndDate, pageNo, range);
            string CountryName;
            foreach (DashBoardSettingInfo obj in lstTopVisitedCountry)
            {
                objIP.GetCountry(obj.SessionUserHostAddress, out CountryName);
                if (CountryName == null)
                {
                    obj.Country = obj.SessionUserHostAddress;
                }
                else
                {
                    obj.Country = CountryName;
                }
            }
            return lstTopVisitedCountry;
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<DashBoardSettingInfo> GetVisitedPage(string StartDate, string EndDate, int pageNo, int range, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<DashBoardSettingInfo> lstTopVisitedPage = DashBoardDataProvider.GetTopVisitedPage(StartDate, EndDate, pageNo, range);
            return lstTopVisitedPage;
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<DashBoardSettingInfo> GetBrowser(string StartDate, string EndDate, int pageNo, int range, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<DashBoardSettingInfo> lstTopBrowser = DashBoardDataProvider.GetTopBrowser(StartDate, EndDate, pageNo, range);
            return lstTopBrowser;
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<DashBoardSettingInfo> GetSetting(int UserModuleID, int PortalID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<DashBoardSettingInfo> lstSetting = DashBoardController.ListDashBoardSettingForView(UserModuleID, PortalID);
            return lstSetting;
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public int MonthlyVisitMeterGauge(int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            return DashBoardController.GetMonthlyVisit();
        }
        else
        {
            return 0;
        }
    }
    [WebMethod]
    public List<DashBoardSettingInfo> GetDailyVisit(string StartDate, string EndDate, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<DashBoardSettingInfo> lstDailyVisit = DashBoardController.GetDailyVisit(StartDate, EndDate);
            return lstDailyVisit;
        }
        else
        {
            return null;
        }
    }
    [WebMethod]
    public List<DashBoardSettingInfo> GetRefSite(string StartDate, string EndDate, int pageNo, int range, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<DashBoardSettingInfo> lstDailyVisit = DashBoardController.GetRefSite(StartDate, EndDate, pageNo, range);
            return lstDailyVisit;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public List<DashBoardSettingInfo> GetSearchPage(string StartDate, string EndDate, int PortalID, int UserModuleID, string UserName, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleID, UserName, secureToken))
        {
            List<DashBoardSettingInfo> lstPages = DashBoardController.GetSearchPage(StartDate, EndDate);
            return lstPages;
        }
        else
        {
            return null;
        }
    }
}