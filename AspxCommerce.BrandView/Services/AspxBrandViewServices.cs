using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AspxCommerce.Core;
using AspxCommerce.BrandView;

/// <summary>
/// Summary description for AspxBrandViewServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class AspxBrandViewServices : System.Web.Services.WebService
{

    public AspxBrandViewServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<BrandViewInfo> GetAllBrandForSlider(AspxCommonInfo aspxCommonObj, int BrandCount)
    {
        try
        {
            AspxBrandViewController objBrand = new AspxBrandViewController();
            List<BrandViewInfo> objAllBrand = objBrand.GetAllBrandForSlider(aspxCommonObj, BrandCount);
            return objAllBrand;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<BrandViewInfo> GetAllBrandForItem(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxBrandViewController objBrand = new AspxBrandViewController();
            List<BrandViewInfo> lstBrand = objBrand.GetAllBrandForItem(aspxCommonObj);
            return lstBrand;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<BrandViewInfo> GetAllFeaturedBrand(AspxCommonInfo aspxCommonObj, int Count)
    {
        try
        {
            AspxBrandViewController objBrand = new AspxBrandViewController();
            List<BrandViewInfo> lstBrand = objBrand.GetAllFeaturedBrand(aspxCommonObj, Count);
            return lstBrand;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public BrandSettingInfo GetBrandSetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxBrandViewController objBrand = new AspxBrandViewController();
            BrandSettingInfo lstBrand = objBrand.GetBrandSetting(aspxCommonObj);
            return lstBrand;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void BrandSettingsUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxBrandViewController objBrand = new AspxBrandViewController();
            objBrand.BrandSettingsUpdate(SettingValues, SettingKeys, aspxCommonObj);   
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

