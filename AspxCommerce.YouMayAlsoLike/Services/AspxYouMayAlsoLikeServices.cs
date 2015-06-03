using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AspxCommerce.YouMayAlsoLike;
using AspxCommerce.Core;

/// <summary>
/// Summary description for AspxYouMayAlsoLikeServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class AspxYouMayAlsoLikeServices : System.Web.Services.WebService
{

    public AspxYouMayAlsoLikeServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Related Items You may also like
    [WebMethod]
    public List<YouMayAlsoLikeInfo> GetYouMayAlsoLikeItemsListByItemSKU(string itemSKU, AspxCommonInfo aspxCommonObj, int count)
    {
        try
        {
            AspxYouMayAlsoLikeController objYouMayLike = new AspxYouMayAlsoLikeController();
            List<YouMayAlsoLikeInfo> lstYouMayLike = objYouMayLike.GetYouMayAlsoLikeItemsListByItemSKU(itemSKU, aspxCommonObj, count);
            return lstYouMayLike;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<YouMayAlsoLikeInfo> GetYouMayAlsoLikeItems(string itemSKU, AspxCommonInfo aspxCommonObj, int count)
    {
        try
        {
            AspxYouMayAlsoLikeController objYouMayLike = new AspxYouMayAlsoLikeController();
            List<YouMayAlsoLikeInfo> lstYouMayLike = objYouMayLike.GetYouMayAlsoLikeItems(itemSKU, aspxCommonObj, count);
            return lstYouMayLike;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public YouMayAlsoLikeSettingInfo GetYouMayAlsoLikeSetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxYouMayAlsoLikeController objYouMayLike = new AspxYouMayAlsoLikeController();
            YouMayAlsoLikeSettingInfo lstYouMayAlsoLike = objYouMayLike.GetYouMayAlsoLikeSetting(aspxCommonObj);
            return lstYouMayAlsoLike;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void YouMayAlsoLikeSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxYouMayAlsoLikeController objYouMayLike = new AspxYouMayAlsoLikeController();
            objYouMayLike.YouMayAlsoLikeSettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }  
    #endregion

}

