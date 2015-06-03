using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using AspxCommerce.Core;
using AspxCommerce.LatestItems;
using System.Collections.Generic;

/// <summary>
/// Summary description for AspxLatestItemService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AspxLatestItemService : System.Web.Services.WebService
{

    public AspxLatestItemService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    } 

    [WebMethod]
    public List<LatestItemsInfo> GetLatestItemsList(AspxCommonInfo aspxCommonObj, int count)
    {
        try
        {
            AspxLatestItemsController objLatestItems = new AspxLatestItemsController();
            List<LatestItemsInfo> LatestItems = objLatestItems.GetLatestItemsByCount(aspxCommonObj, count);
            return LatestItems;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public LatestItemSettingInfo GetLatestItemSetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxLatestItemsController objLatestItems = new AspxLatestItemsController();
            LatestItemSettingInfo objLatestSetting = new LatestItemSettingInfo();
            objLatestSetting = objLatestItems.GetLatestItemSetting(aspxCommonObj);
            return objLatestSetting;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public void LatestItemSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxLatestItemsController objLatestItems = new AspxLatestItemsController();
            objLatestItems.LatestItemSettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
        }

        catch(Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<LatestItemRssInfo> GetLatestRssFeedContent(AspxCommonInfo aspxCommonObj, int count)
    {
        try
        {
            AspxLatestItemsController objLatestItem = new AspxLatestItemsController();
            List<LatestItemRssInfo> itemRss = objLatestItem.GetLatestRssFeedContent(aspxCommonObj, count);
            return itemRss;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

