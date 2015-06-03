<%@ WebService Language="C#" Class="Service" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using AspxCommerce.Core;
using System.Collections.Generic;
using AspxCommerce.CompareItem;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Service  : System.Web.Services.WebService {

    [WebMethod]
    public List<ItemsCompareInfo> GetItemCompareList(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            CompareItemController controller = new CompareItemController();
            List<ItemsCompareInfo> lstItemCompare = controller.GetItemCompareList(aspxCommonObj);
            return lstItemCompare;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #region CompareItems
    [WebMethod]
    public int SaveCompareItems(SaveCompareItemInfo saveCompareItemObj, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            CompareItemController controller = new CompareItemController();
            int compareID = controller.SaveCompareItems(saveCompareItemObj, aspxCommonObj);
            return compareID;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    [WebMethod]
    public void DeleteCompareItem(int compareItemID, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            CompareItemController controller = new CompareItemController();
            controller.DeleteCompareItem(compareItemID, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void ClearAll(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            CompareItemController controller = new CompareItemController();
            controller.ClearAll(aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public bool CheckCompareItems(int ID, AspxCommonInfo aspxCommonObj, string costVariantValueIDs)
    {
        try
        {
            CompareItemController controller = new CompareItemController();
            bool isExist = controller.CheckCompareItems(ID, aspxCommonObj, costVariantValueIDs);
            return isExist;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public ItemsCompareInfo GetItemDetailsForCompare(int ItemID, AspxCommonInfo aspxCommonObj, string costVariantValueIDs)
    {
        try
        {
            CompareItemController controller = new CompareItemController();
            ItemsCompareInfo objItemDetails = controller.GetItemDetailsForCompare(ItemID, aspxCommonObj,
                                                                                               costVariantValueIDs);
            return objItemDetails;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region CheckWishItems
    [WebMethod]
    public bool CheckWishItems(int ID, string costVariantValueIDs, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxCommerce.WishItem.WishItemController controller = new AspxCommerce.WishItem.WishItemController();
            bool isExist = controller.CheckWishItems(ID, costVariantValueIDs, aspxCommonObj);
            return isExist;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    //---------------------Compare Items Details view-------------------------------
    [WebMethod]
    public List<ItemBasicDetailsInfo> GetCompareListImage(string itemIDs, string CostVariantValueIDs, AspxCommonInfo aspxCommonObj)
    {
        CompareItemController controller = new CompareItemController();
        List<ItemBasicDetailsInfo> lstItemBasic = controller.GetCompareListImage(itemIDs, CostVariantValueIDs, aspxCommonObj);
        return lstItemBasic;
    }

    [WebMethod]
    public List<CompareItemListInfo> GetCompareList(string itemIDs, string CostVariantValueIDs, AspxCommonInfo aspxCommonObj)
    {
        CompareItemController controller = new CompareItemController();
        List<CompareItemListInfo> lstCompItem = controller.GetCompareList(itemIDs, CostVariantValueIDs, aspxCommonObj);
        return lstCompItem;
    }

    #region RecentlyComparedProducts
    [WebMethod]
    public void AddComparedItems(string IDs, string CostVarinatIds, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            CompareItemController controller = new CompareItemController();
            controller.AddComparedItems(IDs, CostVarinatIds, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<ItemsCompareInfo> GetRecentlyComparedItemList(int count, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            CompareItemController controller = new CompareItemController();
            List<ItemsCompareInfo> lstCompItem = controller.GetRecentlyComparedItemList(count, aspxCommonObj);
            return lstCompItem;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #endregion

        [WebMethod]
    public CompareItemsSettingInfo GetCompareItemsSetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            CompareItemController cic = new CompareItemController();
            CompareItemsSettingInfo compItemSettingInfo = new CompareItemsSettingInfo();
            compItemSettingInfo = cic.GetCompareItemsSetting(aspxCommonObj);
            return compItemSettingInfo;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
        [WebMethod]
    public void SaveAndUpdateCompareItemsSetting(AspxCommonInfo aspxCommonObj, CompareItemsSettingKeyPairInfo compareItemsSettingObj)
    {
        CompareItemController cic = new CompareItemController();
        cic.SaveAndUpdateCompareItemsSetting(aspxCommonObj, compareItemsSettingObj);
    }
    
}