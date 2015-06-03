<%@ WebService Language="C#" Class="WebService" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.Core.SageFrame.Search;
using SageFrame.Web;
using SageFrame.Search;
using SageFrame.SageFrameClass;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    public WebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<SageFrameSearchInfo> GetSearchResult(int offset, int limit, string Searchword, string SearchBy, string CultureName, bool IsUseFriendlyUrls, int PortalID)
    {
        try
        {
            Searchword = HttpContext.Current.Server.UrlDecode(Searchword);
            SageFrameSearch stb = new SageFrameSearch();
            List<SageFrameSearchInfo> list = new List<SageFrameSearchInfo>();
            list = stb.SageSearchBySearchWord(offset, limit, Searchword, SearchBy, CultureName, IsUseFriendlyUrls, PortalID);
            return list;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}


