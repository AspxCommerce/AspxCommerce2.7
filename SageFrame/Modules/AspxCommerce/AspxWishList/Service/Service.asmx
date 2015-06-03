<%@ WebService Language="C#" Class="Service" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using AspxCommerce.Core;
using AspxCommerce.WishItem;
using System.Collections.Generic;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class Service  : System.Web.Services.WebService {


    #region WishItems
    [WebMethod]
    public bool CheckWishItems(int ID, string costVariantValueIDs, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            WishItemController controller = new WishItemController();
            bool isExist = controller.CheckWishItems(ID, costVariantValueIDs, aspxCommonObj);
            return isExist;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void SaveWishItems(SaveWishListInfo saveWishListInfo, AspxCommonInfo aspxCommonObj)
    {
        try 
	{	        
	
        WishItemController controller = new WishItemController();
        controller.SaveWishItems(saveWishListInfo, aspxCommonObj);
         }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<WishItemsInfo> GetWishItemList(int offset, int limit, AspxCommonInfo aspxCommonObj, string flagShowAll, int count, int sortBy)
    {
        try
        {
            WishItemController controller = new WishItemController();
            List<WishItemsInfo> lstWishItem = controller.GetWishItemList(offset, limit, aspxCommonObj, flagShowAll, count, sortBy);
            return lstWishItem;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public List<WishItemsInfo> GetRecentWishItemList(AspxCommonInfo aspxCommonObj, string flagShowAll, int count)
    {
        try
        {
            WishItemController controller = new WishItemController();
            List<WishItemsInfo> lstWishItem = controller.GetRecentWishItemList(aspxCommonObj, flagShowAll, count);
            return lstWishItem;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void DeleteWishItem(string wishItemID, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            WishItemController controller = new WishItemController();
            controller.DeleteWishItem(wishItemID, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void UpdateWishList(string wishItemID, string comment, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            WishItemController controller = new WishItemController();
            controller.UpdateWishList(wishItemID, comment, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void ClearWishList(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            WishItemController controller = new WishItemController();
            controller.ClearWishList(aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    //-------------------------Save AND SendEmail Messages For ShareWishList----------------
    [WebMethod]
    public void ShareWishListEmailSend(AspxCommonInfo aspxCommonObj, WishItemsEmailInfo wishlistObj)
    {
        try
        {
            WishItemController controller = new WishItemController();
            controller.SaveShareWishListEmailMessage(aspxCommonObj, wishlistObj);
            controller.SendShareWishItemEmail(aspxCommonObj, wishlistObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public int CountWishItems(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            WishItemController controller = new WishItemController();
            int countWish = controller.CountWishItems(aspxCommonObj);
            return countWish;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    [WebMethod]
    public WishItemsSettingInfo GetWishItemsSetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            WishItemController wic = new WishItemController();
            WishItemsSettingInfo wishSetting = wic.GetWishItemsSetting(aspxCommonObj);
            return wishSetting;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public void SaveAndUpdateWishItemsSetting(AspxCommonInfo aspxCommonObj, WishItemsSettingKeyPairInfo wishlistSettingObj)
    {
        try
        {
            WishItemController wic = new WishItemController();
            wic.SaveAndUpdateWishItemsSetting(aspxCommonObj, wishlistSettingObj);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}