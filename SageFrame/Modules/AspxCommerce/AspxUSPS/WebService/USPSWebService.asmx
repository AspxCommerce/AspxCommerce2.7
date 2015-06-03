<%@ WebService Language="C#" Class="USPSWebService" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using AspxCommerce.Core;
using AspxCommerce.USPS.Controller;
using AspxCommerce.USPS.Entity;
using SageFrame.Web.Utilities;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class USPSWebService : System.Web.Services.WebService
{

    [WebMethod]
    public UspsSetting GetUSPSSetting(int providerId, AspxCommonInfo commonInfo)
    {
        try
        {
            var objUsps = new UspsController();
            return objUsps.GetSetting(providerId, commonInfo);

        }
        catch (Exception ex)
        {

            throw ex;
        }


    }
    //[WebMethod]
    //public List<ProviderShippingMethod> GetRemainingShippingMethod(int providerId, int storeId, int portalId)
    //{
    //    var objUsps = new UspsController();
    //    return objUsps.GetRemainingShippingMethod(providerId, storeId, portalId);
    //}

    [WebMethod]
    public void RollBack(string temppath, ArrayList dllFiles, string unistallfile)
    {
        try
        {
            FileHelperController helper = new FileHelperController();
            helper.DeleteTempDirectory(temppath);
        }
        catch (Exception ex)
        {

            throw ex;
        }


    }

    [WebMethod]
    public void SaveUpdateUspsSetting(int providerId, string settingKey, string settingValue, AspxCommonInfo commonInfo)
    {
        try
        {
            AspxShipProviderMgntController objUsps = new AspxShipProviderMgntController();
            objUsps.SaveSetting(providerId, settingKey, settingValue, commonInfo);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}

