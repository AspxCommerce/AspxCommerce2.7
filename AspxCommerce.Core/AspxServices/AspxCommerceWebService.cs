/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.NewsLetter;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using SageFrame.Security;
using SageFrame.Security.Entities;
using System.Web.Security;
using SageFrame.Security.Helpers;
using AspxCommerce.Core;
using SageFrame.Shared;
using SageFrame.Message;
using SageFrame.Security.Providers;
using SageFrame.NewLetterSubscriber;
using SageFrame.Core;
using SageFrame.Common;
using System.IO;
using SageFrame.Framework;

/// <summary>
/// Summary description for AspxCommerceWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AspxCommerceWebService : System.Web.Services.WebService
{

    public AspxCommerceWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public string GetStateCode(string cCode, string stateName)
    //{
    //    string stateCode = AspxCommonController.GetStateCode(cCode, stateName);
    //    return stateCode;
    //}
    //[WebMethod]
    //public List<CountryList> LoadCountry()
    //{
    //    List<CountryList> lstCountry = AspxShipRateController.LoadCountry();
    //    return lstCountry; ;
    //}

    //[WebMethod]
    //public List<States> GetStatesByCountry(string countryCode)
    //{
    //    List<States> lstState = AspxShipRateController.GetStatesByCountry(countryCode);
    //    return lstState;
    //}


    //[WebMethod]
    //public List<CommonRateList> GetRate(ItemListDetails itemsDetail)
    //{
    //    List<CommonRateList> lstCommonRate = AspxShipRateController.GetRate(itemsDetail);
    //    return lstCommonRate;
    //}

    [WebMethod]
    public void DeactivateShippingMethod(int shippingMethodId, AspxCommonInfo aspxCommonObj, bool isActive)
    {
        AspxShipProviderMgntController.DeactivateShippingMethod(shippingMethodId, aspxCommonObj, isActive);
    }

    [WebMethod]
    public List<ProviderShippingMethod> GetProviderRemainingMethod(int shippingProviderId, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<ProviderShippingMethod> services = AspxShipProviderMgntController.GetProviderRemainingMethod(shippingProviderId, aspxCommonObj);
            return services;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    //[WebMethod]
    //public List<CartTaxInfo> GetCartTax(CartDataInfo cartTaxObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CartTaxInfo> lstCartTax = AspxCartController.GetCartTax(cartTaxObj, aspxCommonObj);
    //        return lstCartTax;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<CartUnitTaxInfo> GetCartUnitTax(CartDataInfo cartUnitTaxObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CartUnitTaxInfo> lstCartUnitTax = AspxCartController.GetCartUnitTax(cartUnitTaxObj, aspxCommonObj);
    //        return lstCartUnitTax;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CartTaxforOrderInfo> GetCartTaxforOrder(CartDataInfo cartTaxOrderObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CartTaxforOrderInfo> lstCartTaxOrder = AspxCartController.GetCartTaxforOrder(cartTaxOrderObj, aspxCommonObj);
    //        return lstCartTaxOrder;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    //#region Ware house

    //[WebMethod]
    //public List<WareHouse> GetAllWareHouse(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    List<WareHouse> wList = AspxWareHouseController.GetAllWareHouse(offset, limit, aspxCommonObj);
    //    return wList;
    //}

    //[WebMethod]
    //public List<WareHouseAddress> GetAllWareHouseById(int wareHouseID, AspxCommonInfo aspxCommonObj)
    //{
    //    List<WareHouseAddress> wList = AspxWareHouseController.GetAllWareHouseById(wareHouseID, aspxCommonObj);
    //    return wList;
    //}



    //[WebMethod]
    //public void DeleteWareHouse(int wareHouseId, AspxCommonInfo aspxCommonObj)
    //{
    //    AspxWareHouseController.DeleteWareHouse(wareHouseId, aspxCommonObj);
    //}

    //[WebMethod]
    //public void AddUpDateWareHouse(WareHouseAddress wareHouse, AspxCommonInfo aspxCommonObj)
    //{
    //    AspxWareHouseController.AddUpDateWareHouse(wareHouse, aspxCommonObj);
    //}


    //#endregion

   // #region GiftCard Method

   //// [WebMethod]
   // //public bool CheckGiftCardUsed(AspxCommonInfo aspxCommonObj, string giftCardCode, decimal amount)
   // //{
   // //    try
   // //    {
   // //        bool allowToCheckout = AspxGiftCardController.CheckGiftCardUsed(aspxCommonObj, giftCardCode, amount);
   // //        return allowToCheckout;
   // //    }
   // //    catch (Exception ex)
   // //    {
   // //        throw ex;
   // //    }
   // //}
   // [WebMethod]
   // public List<GiftCardType> GetGiftCardTypes(AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         List<GiftCardType> lstGiftCard = AspxGiftCardController.GetGiftCardTypes(aspxCommonObj);
   //         return lstGiftCard;
   //     }
   //     catch (Exception ex)
   //     {
   //         throw ex;
   //     }
   // }

   // //[WebMethod]
   // //public int GetGiftCardType(AspxCommonInfo aspxCommonObj, int cartitemId)
   // //{
   // //    try
   // //    {
   // //        int strType = AspxGiftCardController.GetGiftCardType(aspxCommonObj, cartitemId);
   // //        return strType;
   // //    }
   // //    catch (Exception e)
   // //    {
   // //        throw e;
   // //    }
   // //}

   // [WebMethod]
   // public List<GiftCardType> GetGiftCardTypeId(AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         List<GiftCardType> lstGiftCardType = AspxGiftCardController.GetGiftCardTypeId(aspxCommonObj);
   //         return lstGiftCardType;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }

   // [WebMethod]
   // public Vefification VerifyGiftCard(string giftcardCode, string pinCode, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         Vefification objVerify = AspxGiftCardController.VerifyGiftCard(giftcardCode, pinCode, aspxCommonObj);
   //         return objVerify;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }

   // [WebMethod]
   // public List<BalanceInquiry> CheckGiftCardBalance(string giftcardCode, string giftCardPinCode, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         List<BalanceInquiry> lstBalanceInq = AspxGiftCardController.CheckGiftCardBalance(giftcardCode, giftCardPinCode, aspxCommonObj);
   //         return lstBalanceInq;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }

   // [WebMethod]
   // public List<GiftCardHistory> GetGiftCardHistory(int giftcardId, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         List<GiftCardHistory> lstGCHistory = AspxGiftCardController.GetGiftCardHistory(giftcardId, aspxCommonObj);
   //         return lstGCHistory;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }
   // [WebMethod]
   // public List<GiftCard> GetGiftCardDetailById(int giftcardId, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         List<GiftCard> lstGiftCard = AspxGiftCardController.GetGiftCardDetailById(giftcardId, aspxCommonObj);
   //         return lstGiftCard;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }

   // [WebMethod]
   // public void SaveGiftCardByAdmin(int giftCardId, GiftCard giftCardDetail, bool isActive, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         AspxGiftCardController.SaveGiftCardByAdmin(giftCardId, giftCardDetail, isActive, aspxCommonObj);
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }
   // [WebMethod]
   // public List<GiftCardGrid> GetAllPaidGiftCard(int offset, int limit, AspxCommonInfo aspxCommonObj, GiftCardDataInfo giftCardDataObj)
   // {
   //     try
   //     {
   //         List<GiftCardGrid> ii = AspxGiftCardController.GetAllPaidGiftCard(offset, limit, aspxCommonObj, giftCardDataObj);
   //         return ii;

   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }
   // [WebMethod]
   // public void DeleteGiftCard(string giftCardId, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         AspxGiftCardController.DeleteGiftCard(giftCardId, aspxCommonObj);
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }

   // [WebMethod]
   // public void DeleteTempFile(string path)
   // {
   //     if (path.Contains("GiftCard_Graphic"))
   //     {
   //         var filePath = HttpContext.Current.Server.MapPath("~/" + path);
   //         if (File.Exists(filePath))
   //         {
   //             File.Delete(filePath);
   //         }
   //     }

   // }

   // [WebMethod]
   // public bool CheckGiftCardCategory(AspxCommonInfo aspxCommonObj, string giftcardCategoryName)
   // {
   //     try
   //     {
   //         bool isGiftCard = AspxGiftCardController.CheckGiftCardCategory(aspxCommonObj, giftcardCategoryName);
   //         return isGiftCard;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }

   // [WebMethod]
   // public void SaveGiftCardCategory(int giftCardCategoryId, AspxCommonInfo aspxCommonObj, string giftcardCategoryName, bool isActive)
   // {
   //     try
   //     {
   //         AspxGiftCardController.SaveGiftCardCategory(giftCardCategoryId, aspxCommonObj, giftcardCategoryName, isActive);
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }

   // [WebMethod]
   // public void DeleteGiftCardCategory(int giftCardCategoryId, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         AspxGiftCardController.DeleteGiftCardCategory(giftCardCategoryId, aspxCommonObj);
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }
   // [WebMethod]
   // public void DeleteGiftCardThemeImage(int giftCardGraphicId, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         AspxGiftCardController.DeleteGiftCardThemeImage(giftCardGraphicId, aspxCommonObj);
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }
   // [WebMethod]
   // public string SaveGiftCardItemCategory(int itemId, string ids, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         string strValue = AspxGiftCardController.SaveGiftCardItemCategory(itemId, ids, aspxCommonObj);
   //         return strValue;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }


   // [WebMethod]
   // public List<GiftCardInfo> GetGiftCardThemeImagesByItem(int itemId, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         List<GiftCardInfo> lstGiftCard = AspxGiftCardController.GetGiftCardThemeImagesByItem(itemId, aspxCommonObj);
   //         return lstGiftCard;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }
   // [WebMethod]
   // public string GetGiftCardItemCategory(int itemId, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         string strValue = AspxGiftCardController.GetGiftCardItemCategory(itemId, aspxCommonObj);
   //         return strValue;

   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }
   // [WebMethod]
   // public List<GiftCardCategoryInfo> GetAllGiftCardCategoryGrid(int offset, int limit, AspxCommonInfo aspxCommonObj, string categoryName, DateTime? addedon, bool? status)
   // {
   //     try
   //     {
   //         List<GiftCardCategoryInfo> lstGCCat = AspxGiftCardController.GetAllGiftCardCategoryGrid(offset, limit, aspxCommonObj, categoryName, addedon, status);
   //         return lstGCCat;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }
    //[WebMethod]
    //public List<GiftCardInfo> GetAllGiftCardCategory(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<GiftCardInfo> lstGiftCard = AspxGiftCardController.GetAllGiftCardCategory(aspxCommonObj);
    //        return lstGiftCard;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

   // [WebMethod]
   // public List<GiftCardInfo> GetAllGiftCardThemeImageByCategory(int giftCardCategoryId, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         List<GiftCardInfo> lstGiftCard = AspxGiftCardController.GetAllGiftCardThemeImageByCategory(giftCardCategoryId, aspxCommonObj);
   //         return lstGiftCard;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }
   // [WebMethod]
   // public List<GiftCardInfo> GetAllGiftCardThemeImage(AspxCommonInfo aspxCommonObj, int categoryId)
   // {
   //     try
   //     {
   //         List<GiftCardInfo> lstGiftCard = AspxGiftCardController.GetAllGiftCardThemeImage(aspxCommonObj, categoryId);
   //         return lstGiftCard;
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }
   // }
   // [WebMethod]
   // public void SaveGiftCardThemeImage(string graphicThemeName, string graphicImage, int giftCardCategoryId, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         AspxGiftCardController.SaveGiftCardThemeImage(graphicThemeName, graphicImage, giftCardCategoryId, aspxCommonObj);
   //     }
   //     catch (Exception e)
   //     {
   //         throw e;
   //     }

   // }
   // [WebMethod]
   // public List<LatestItemsInfo> GetAllGiftCards(int offset, int limit, int rowTotal, AspxCommonInfo aspxCommonObj)
   // {
   //     try
   //     {
   //         List<LatestItemsInfo> lstGiftItems = AspxItemMgntController.GetAllGiftCards(offset, limit, rowTotal, aspxCommonObj);
   //         return lstGiftItems;
   //     }
   //     catch (Exception ex)
   //     {
   //         throw ex;
   //     }
   // }

   // #endregion


    //#region Testing Method
    //[WebMethod]
    //public string HelloWorld()
    //{
    //    return "Hello World";
    //}
    //[WebMethod(EnableSession = true)]
    //public bool CheckSessionActive(AspxCommonInfo aspxCommonObj)
    //{
    //    if (HttpContext.Current.User != null)
    //    {
    //        SecurityPolicy objSecurity = new SecurityPolicy();
    //        FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(aspxCommonObj.PortalID);
    //        if (ticket != null && ticket.Name != ApplicationKeys.anonymousUser)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    //#endregion

    #region Header Menu category Lister
    [WebMethod]
    public List<CategoryInfo> GetCategoryMenuList(AspxCommonInfo aspxCommonObj)
    {
        List<CategoryInfo> catInfo = AspxCategoryListController.GetCategoryMenuList(aspxCommonObj);
        return catInfo;
    }
    #endregion

  //  #region Aspx BreadCrumb
    //[WebMethod]
    //public string GetCategoryForItem(int storeID, int portalID, string itemSku, string cultureName)
    //{
    //    try
    //    {
    //        string retString = AspxBreadCrumbController.GetCategoryForItem(storeID, portalID, itemSku, cultureName);
    //        return retString;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //[WebMethod]
    //public List<BreadCrumInfo> GetBreadCrumb(string name, AspxCommonInfo commonInfo)
    //{
    //    try
    //    {

    //        List<BreadCrumInfo> list = AspxBreadCrumbController.GetBreadCrumb(name, commonInfo);
    //        return list;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }

    //}

    //[WebMethod]
    //public List<BreadCrumInfo> GetCategoryName(string name, AspxCommonInfo commonInfo)
    //{
    //    try
    //    {

    //        List<BreadCrumInfo> list = AspxBreadCrumbController.GetCategoryName(name, commonInfo);
    //        return list;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }

    //}
    //[WebMethod]
    //public List<BreadCrumInfo> GetItemCategories(string itemName, AspxCommonInfo commonInfo)
    //{

    //    try
    //    {
    //        List<BreadCrumInfo> list = AspxBreadCrumbController.GetItemCategories(itemName, commonInfo);
    //        return list;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}



 //   #endregion

    //#region General Functions
    ////--------------------Roles Lists------------------------    
    //[WebMethod]
    //public List<PortalRole> GetAllRoles(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<PortalRole> lstPortalRole = AspxCommonController.GetPortalRoles(aspxCommonObj.PortalID, true, aspxCommonObj.UserName);
    //        return lstPortalRole;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------------Store Lists------------------------    
    //[WebMethod]
    //public List<StoreInfo> GetAllStores(AspxCommonInfo aspxCommonObj)
    //{
    //    List<StoreInfo> lstStore = AspxCommonController.GetAllStores(aspxCommonObj);
    //    return lstStore;
    //}

    ////----------------country list------------------------------    

    //[WebMethod]
    //public List<CountryInfo> BindCountryList()
    //{
    //    try
    //    {
    //        List<CountryInfo> lstCountry = AspxCommonController.BindCountryList();
    //        return lstCountry;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////----------------state list--------------------------     
    //[WebMethod]
    //public List<StateInfo> BindStateList(string countryCode)
    //{
    //    try
    //    {
    //        List<StateInfo> lstState = AspxCommonController.BindStateList(countryCode);
    //        return lstState;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Status Management
    ////------------------Status DropDown-------------------    
    //[WebMethod]
    //public List<StatusInfo> GetStatus(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<StatusInfo> lstStatus = AspxCommonController.GetStatus(aspxCommonObj);
    //        return lstStatus;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //#endregion

    //#region Bind Users DropDown
    //[WebMethod]
    //public List<UserInRoleInfo> BindRoles(bool isAll, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<UserInRoleInfo> lstUserInRole = AspxItemMgntController.BindRoles(isAll, aspxCommonObj);
    //        return lstUserInRole;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Attributes Management
    //[WebMethod]
    //public List<AttributesInputTypeInfo> GetAttributesInputTypeList()
    //{
    //    try
    //    {
    //        List<AttributesInputTypeInfo> lstAttrInputType = AspxItemAttrMgntController.GetAttributesInputType();
    //        return lstAttrInputType;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<AttributesItemTypeInfo> GetAttributesItemTypeList(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AttributesItemTypeInfo> lstAttrItemType = AspxItemAttrMgntController.GetAttributesItemType(aspxCommonObj);
    //        return lstAttrItemType;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<AttributesValidationTypeInfo> GetAttributesValidationTypeList()
    //{
    //    try
    //    {
    //        List<AttributesValidationTypeInfo> lstAttrValidType = AspxItemAttrMgntController.GetAttributesValidationType();
    //        return lstAttrValidType;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<AttributesBasicInfo> GetAttributesList(int offset, int limit, AttributeBindInfo attrbuteBindObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AttributesBasicInfo> lstAttrBasic = AspxItemAttrMgntController.GetItemAttributes(offset, limit, attrbuteBindObj, aspxCommonObj);
    //        return lstAttrBasic;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<AttributesGetByAttributeIdInfo> GetAttributeDetailsByAttributeID(int attributeId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AttributesGetByAttributeIdInfo> lstAttr = AspxItemAttrMgntController.GetAttributesInfoByAttributeID(attributeId, aspxCommonObj);
    //        return lstAttr;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteMultipleAttributesByAttributeID(string attributeIds, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemAttrMgntController.DeleteMultipleAttributes(attributeIds, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteAttributeByAttributeID(int attributeId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemAttrMgntController.DeleteAttribute(attributeId, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void UpdateAttributeIsActiveByAttributeID(int attributeId, AspxCommonInfo aspxCommonObj, bool isActive)
    //{
    //    try
    //    {
    //        AspxItemAttrMgntController.UpdateAttributeIsActive(attributeId, aspxCommonObj, isActive);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void SaveUpdateAttributeInfo(AttributesGetByAttributeIdInfo attributeInfo)
    //{
    //    try
    //    {
    //        AspxItemAttrMgntController.SaveAttribute(attributeInfo);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //[WebMethod]
    //public AttributeFormInfo SaveUpdateAttributeInfo(AttributesGetByAttributeIdInfo attributeInfo, AttributeConfig config)
    //{
    //    try
    //    {
    //       return AspxItemAttrMgntController.SaveAttribute(attributeInfo, config);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //#endregion

    //#region AttributeSet Management
    //[WebMethod]
    //public List<AttributeSetBaseInfo> GetAttributeSetGrid(int offset, int limit, AttributeSetBindInfo AttributeSetBindObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AttributeSetBaseInfo> lstAttrSet = AspxItemAttrMgntController.GetAttributeSetGrid(offset, limit, AttributeSetBindObj, aspxCommonObj);
    //        return lstAttrSet;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}


    //[WebMethod]
    //public List<AttributeSetInfo> GetAttributeSetList(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {

    //        List<AttributeSetInfo> lstAttrSet = AspxItemAttrMgntController.GetAttributeSet(aspxCommonObj);
    //        return lstAttrSet;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}


    //[WebMethod]
    //public int SaveUpdateAttributeSetInfo(AttributeSetInfo attributeSetObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        int retValue = AspxItemAttrMgntController.SaveUpdateAttributeSet(attributeSetObj, aspxCommonObj);
    //        return retValue;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public int CheckAttributeSetUniqueness(AttributeSaveInfo checkUniqueAttrSet, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        int retValue = AspxItemAttrMgntController.CheckAttributeSetUniqueName(checkUniqueAttrSet, aspxCommonObj);
    //        return retValue;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<AttributeSetGetByAttributeSetIdInfo> GetAttributeSetDetailsByAttributeSetID(int attributeSetId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AttributeSetGetByAttributeSetIdInfo> lstAttrSetDetail = AspxItemAttrMgntController.GetAttributeSetInfoByAttributeSetID(attributeSetId, aspxCommonObj);
    //        return lstAttrSetDetail;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteAttributeSetByAttributeSetID(int attributeSetId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemAttrMgntController.DeleteAttributeSet(attributeSetId, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void UpdateAttributeSetIsActiveByAttributeSetID(int attributeSetId, AspxCommonInfo aspxCommonObj, bool isActive)
    //{
    //    try
    //    {
    //        AspxItemAttrMgntController.UpdateAttributeSetIsActive(attributeSetId, aspxCommonObj, isActive);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void SaveUpdateAttributeGroupInfo(AttributeSaveInfo attributeSaveObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemAttrMgntController.UpdateAttributeGroup(attributeSaveObj, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteAttributeSetGroupByAttributeSetID(AttributeSaveInfo deleteGroupObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemAttrMgntController.DeleteAttributeSetGroup(deleteGroupObj, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<AttributeSetGroupAliasInfo> RenameAttributeSetGroupAliasByGroupID(AttributeSetGroupAliasInfo attributeSetInfoToUpdate)
    //{
    //    try
    //    {
    //        List<AttributeSetGroupAliasInfo> lstAttrSetGroup = AspxItemAttrMgntController.RenameAttributeSetGroupAlias(attributeSetInfoToUpdate);
    //        return lstAttrSetGroup;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteAttributeByAttributeSetID(AttributeSaveInfo deleteGroupObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemAttrMgntController.DeleteAttribute(deleteGroupObj, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //#endregion

    //#region Items Management
    //[WebMethod]
    //public ItemSetting GetItemSetting(int ItemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        ItemSetting lstItemSetting = AspxItemMgntController.GetItemSetting(ItemID, aspxCommonObj);
    //        return lstItemSetting;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //[WebMethod]
    //public List<ItemPriceGroupInfo> GetItemGroupPrices(int ItemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemPriceGroupInfo> lstGroupPrice = AspxItemMgntController.GetItemGroupPrices(ItemID, aspxCommonObj);
    //        return lstGroupPrice;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //[WebMethod]
    //public List<ItemsInfo> GetItemsList(int offset, int limit, GetItemListInfo getItemListObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemsInfo> ml;
    //        ml = AspxItemMgntController.GetAllItems(offset, limit, getItemListObj, aspxCommonObj);
    //        return ml;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteMultipleItemsByItemID(string itemIds, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemMgntController.DeleteMultipleItems(itemIds, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteItemByItemID(string itemId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemMgntController.DeleteSingleItem(itemId, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<AttributeFormInfo> GetItemFormAttributes(int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
    //{
    //    List<AttributeFormInfo> formAttributeList;
    //    formAttributeList = AspxItemMgntController.GetItemFormAttributes(attributeSetID, itemTypeID, aspxCommonObj);
    //    return formAttributeList;
    //}

    ////[WebMethod]
    ////public List<AttributeFormInfo> GetItemFormAttributesByitemSKUOnly(string itemSKU, AspxCommonInfo aspxCommonObj)
    ////{
    ////    List<AttributeFormInfo> frmItemFieldList = AspxItemMgntController.GetItemFormAttributesByItemSKUOnly(itemSKU, aspxCommonObj);
    ////    return frmItemFieldList;
    ////}

    //[WebMethod]
    //public List<AttributeFormInfo> GetItemFormAttributesValuesByItemID(int itemID, int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
    //{
    //    List<AttributeFormInfo> frmItemAttributes = AspxItemMgntController.GetItemAttributesValuesByItemID(itemID, attributeSetID, itemTypeID, aspxCommonObj);
    //    return frmItemAttributes;
    //}

    //[WebMethod]
    //public int SaveItemAndAttributes(ItemsInfo.ItemSaveBasicInfo itemObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        string uplodedDownlodableFormValue = string.Empty;

    //        if (itemObj.ItemTypeId == 2 && itemObj.DownloadItemsValue != "")
    //        {
    //            FileHelperController downLoadableObj = new FileHelperController();
    //            string tempFolder = @"Upload\temp";
    //            uplodedDownlodableFormValue = downLoadableObj.MoveFileToDownlodableItemFolder(tempFolder,
    //                                                                                          itemObj.DownloadItemsValue,
    //                                                                                          @"Modules/AspxCommerce/AspxItemsManagement/DownloadableItems/",
    //                                                                                          itemObj.ItemId, "item_");
    //            itemObj.DownloadItemsValue = uplodedDownlodableFormValue;
    //        }


    //        int itemID = AspxItemMgntController.SaveUpdateItemAndAttributes(itemObj, aspxCommonObj);
    //        //return "({\"returnStatus\":1,\"Message\":'Item saved successfully.'})";
    //        int storeId = aspxCommonObj.StoreID;
    //        int portalId = aspxCommonObj.PortalID;
    //        string culture = aspxCommonObj.CultureName;
    //        // if (itemID > 0 && sourceFileCol != "" && dataCollection != "")
    //        if (itemID > 0 && itemObj.SourceFileCol != "" && itemObj.DataCollection != "")
    //        {
    //            StoreSettingConfig ssc = new StoreSettingConfig();
    //            int itemLargeThumbNailHeight = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageHeight, storeId, portalId, culture));
    //            int itemLargeThumbNailWidth = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageWidth, storeId, portalId, culture));
    //            int itemMediumThumbNailHeight = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageHeight, storeId, portalId, culture));
    //            int itemMediumThumbNailWidth = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageWidth, storeId, portalId, culture));
    //            int itemSmallThumbNailHeight = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageHeight, storeId, portalId, culture));
    //            int itemSmallThumbNailWidth = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageWidth, storeId, portalId, culture));
    //            var dataCollection = itemObj.DataCollection;
    //            dataCollection = dataCollection.Replace("../", "");
    //            SaveImageContents(itemID, @"Modules/AspxCommerce/AspxItemsManagement/uploads/", itemObj.SourceFileCol,
    //                              dataCollection, itemLargeThumbNailHeight, itemLargeThumbNailWidth, itemMediumThumbNailHeight, itemMediumThumbNailWidth,
    //                              itemSmallThumbNailHeight, itemSmallThumbNailWidth, "item_", aspxCommonObj.CultureName, aspxCommonObj.PortalID);
    //        }
    //        else if (itemID > 0 && itemObj.SourceFileCol == "" && itemObj.DataCollection == "")
    //        {
    //            DeleteImageContents(itemID);
    //        }
    //        return itemID;
    //        //if (itemID == 0)
    //        //{
    //        //    //SaveImageContents(itemID, @"Modules/AspxCommerce/AspxItemsManagement/uploads/", sourceFileCol, dataCollection, "item_");
    //        //    //TODO:: DELTE UPLOADED FILE FROM DOWNLOAD FOLDER

    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //        //ErrorHandler errHandler = new ErrorHandler();
    //        //if (errHandler.LogWCFException(ex))
    //        //{
    //        //    return "({\"returnStatus\":-1,\"errorMessage\":'" + ex.Message + "'})";
    //        //}
    //        //else
    //        //{
    //        //    return "({\"returnStatus\":-1,\"errorMessage\":'Error while saving item!'})";
    //        //}
    //    }
    //}

    //[WebMethod]
    //public void UpdateItemIsActiveByItemID(int itemId, AspxCommonInfo aspxCommonObj, bool isActive)
    //{
    //    try
    //    {
    //        AspxItemMgntController.UpdateItemIsActive(itemId, aspxCommonObj, isActive);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////[WebMethod]
    ////public bool CheckUniqueAttributeName(AttributeBindInfo attrbuteUniqueObj, AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        bool isUnique = AspxItemAttrMgntController.CheckUniqueName(attrbuteUniqueObj, aspxCommonObj);
    ////        return isUnique;
    ////    }
    ////    catch (Exception e)
    ////    {
    ////        throw e;
    ////    }
    ////}

    //[WebMethod]
    //public List<CategoryInfo> GetCategoryList(string prefix, bool isActive, AspxCommonInfo aspxCommonObj, int itemId, bool serviceBit)
    //{
    //    List<CategoryInfo> catList = AspxItemMgntController.GetCategoryList(prefix, isActive, aspxCommonObj, itemId, serviceBit);
    //    return catList;
    //}

    //[WebMethod]
    //public bool CheckUniqueItemSKUCode(string SKU, int itemId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        bool isUnique = AspxItemMgntController.CheckUniqueSKUCode(SKU, itemId, aspxCommonObj);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //#region Multiple Image Uploader
    //[WebMethod]
    //public string SaveImageContents(int itemID, string imageRootPath, string sourceFileCol, string dataCollection, int itemLargeThumbNailHeight, int itemLargeThumbNailWidth, int itemMediumThumbNailHeight, int itemMediumThumbNailWidth,
    //                             int itemSmallThumbNailHeight, int itemSmallThumbNailWidth, string imgPreFix, string cultureName, int portalID)
    //{

    //    if (dataCollection.Contains("#"))
    //    {
    //        dataCollection = dataCollection.Remove(dataCollection.LastIndexOf("#"));
    //    }
    //    SQLHandler sageSql = new SQLHandler();
    //    string[] individualRow = dataCollection.Split('#');
    //    string[] words;

    //    StringBuilder sbPathList = new StringBuilder();
    //    StringBuilder sbIsActiveList = new StringBuilder();
    //    StringBuilder sbImageType = new StringBuilder();
    //    StringBuilder sbDescription = new StringBuilder();
    //    StringBuilder sbDisplayOrder = new StringBuilder();
    //    StringBuilder sbSourcePathList = new StringBuilder();
    //    StringBuilder sbItemImageId = new StringBuilder();

    //    foreach (string str in individualRow)
    //    {
    //        words = str.Split('%');
    //        sbPathList.Append(words[0] + "%");
    //        sbIsActiveList.Append(words[1] + "%");
    //        sbImageType.Append(words[2] + "%");
    //        sbDescription.Append(words[3] + "%");
    //        sbDisplayOrder.Append(words[4] + "%");
    //        sbItemImageId.Append(words[5] + "%");
    //    }
    //    string pathList = string.Empty;
    //    string isActive = string.Empty;
    //    string imageType = string.Empty;
    //    string description = string.Empty;
    //    string displayOrder = string.Empty;
    //    string itemImageIds = string.Empty;

    //    pathList = sbPathList.ToString();
    //    isActive = sbIsActiveList.ToString();
    //    imageType = sbImageType.ToString();
    //    description = sbDescription.ToString();
    //    displayOrder = sbDisplayOrder.ToString();
    //    itemImageIds = sbItemImageId.ToString();

    //    if (pathList.Contains("%"))
    //    {
    //        pathList = pathList.Remove(pathList.LastIndexOf("%"));
    //    }
    //    if (isActive.Contains("%"))
    //    {
    //        isActive = isActive.Remove(isActive.LastIndexOf("%"));
    //    }
    //    if (imageType.Contains("%"))
    //    {
    //        imageType = imageType.Remove(imageType.LastIndexOf("%"));
    //    }
    //    if (itemImageIds.Contains("%"))
    //    {
    //        itemImageIds = itemImageIds.Remove(itemImageIds.LastIndexOf("%"));
    //    }

    //    if (sourceFileCol.Contains("%"))
    //    {
    //        sourceFileCol = sourceFileCol.Remove(sourceFileCol.LastIndexOf("%"));
    //    }

    //    try
    //    {
    //        FileHelperController fhc = new FileHelperController();
    //        //TODO:: delete all previous files infos lists
    //        fhc.FileMover(itemID, imageRootPath, sourceFileCol, pathList, isActive, imageType, itemImageIds, description, displayOrder, imgPreFix, itemLargeThumbNailHeight, itemLargeThumbNailWidth, itemMediumThumbNailHeight, itemMediumThumbNailWidth,
    //                              itemSmallThumbNailHeight, itemSmallThumbNailWidth, cultureName, portalID);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    return "Success";

    //}

    //[WebMethod]
    //public List<ItemsInfoSettings> GetImageContents(int itemID, AspxCommonInfo aspxCommonObj)
    //{
    //    List<ItemsInfoSettings> itemsImages = AspxImageGalleryController.GetItemsImageGalleryInfoByItemID(itemID, aspxCommonObj);
    //    return itemsImages;
    //}

    //[WebMethod]
    //public void DeleteImageContents(Int32 itemID)
    //{
    //    try
    //    {
    //        AspxItemMgntController.DeleteItemImageByItemID(itemID);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Related, Cross Sell, Up sell Items
    
    //[WebMethod]
    //public List<ItemsInfo> GetAssociatedItemsList(int offset, int limit, ItemDetailsCommonInfo IDCommonObj,int categoryID,AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemsInfo> ml;
    //        ml = AspxItemMgntController.GetAssociatedItemsByItemID(offset, limit, IDCommonObj, categoryID,aspxCommonObj);
    //        return ml;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    
    
    //[WebMethod]
    //public List<ItemsInfo> GetRelatedItemsList(int offset, int limit, ItemDetailsCommonInfo IDCommonObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemsInfo> ml;
    //        ml = AspxItemMgntController.GetRelatedItemsByItemID(offset, limit, IDCommonObj, aspxCommonObj);
    //        return ml;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ItemsInfo> GetUpSellItemsList(int offset, int limit, ItemDetailsCommonInfo UpSellCommonObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemsInfo> ml;
    //        ml = AspxItemMgntController.GetUpSellItemsByItemID(offset, limit, UpSellCommonObj, aspxCommonObj);
    //        return ml;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ItemsInfo> GetCrossSellItemsList(int offset, int limit, ItemDetailsCommonInfo CrossSellCommonObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemsInfo> ml;
    //        ml = AspxItemMgntController.GetCrossSellItemsByItemID(offset, limit, CrossSellCommonObj, aspxCommonObj);
    //        return ml;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public string GetAssociatedCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        string ml;
    //        ml = AspxItemMgntController.GetAssociatedCheckIDs(ItemID, aspxCommonObj);
    //        return ml;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public string GetRelatedCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        string ml;
    //        ml = AspxItemMgntController.GetRelatedCheckIDs(ItemID, aspxCommonObj);
    //        return ml;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public string GetUpSellCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        string ml;
    //        ml = AspxItemMgntController.GetUpSellCheckIDs(ItemID, aspxCommonObj);
    //        return ml;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public string GetCrossSellCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        string ml;
    //        ml = AspxItemMgntController.GetCrossSellCheckIDs(ItemID, aspxCommonObj);
    //        return ml;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //#endregion

    //#region Item Cost Variants Management
    //[WebMethod]
    //public List<CostVariantInfo> GetCostVariantsOptionsList(int itemId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CostVariantInfo> lstCostVar = AspxItemMgntController.GetAllCostVariantOptions(itemId, aspxCommonObj);
    //        return lstCostVar;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////------------------------ delete Item Cost Variants management------------------------    
    //[WebMethod]
    //public void DeleteSingleItemCostVariant(string itemCostVariantID, int itemId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemMgntController.DeleteSingleItemCostVariant(itemCostVariantID, itemId, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////#region NeW CostVariant Combination
    ////[WebMethod]
    ////public List<VariantCombination> GetCostVariantCombinationbyItemSku(string itemSku, AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        List<VariantCombination> lstVarCom = AspxItemMgntController.GetCostVariantCombinationbyItemSku(itemSku, aspxCommonObj);
    ////        return lstVarCom;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}

    ////[WebMethod]
    ////public List<ItemCostVariantsInfo> GetCostVariantsByItemSKU(string itemSku, AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        List<ItemCostVariantsInfo> lstItemCostVar = AspxItemMgntController.GetCostVariantsByItemSKU(itemSku, aspxCommonObj);
    ////        return lstItemCostVar;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}

    //[WebMethod]
    //public List<CostVariantInfo> GetCostVariantForItem(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CostVariantInfo> lstCostVar = AspxItemMgntController.GetCostVariantForItem(aspxCommonObj);
    //        return lstCostVar;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CostVariantValuesInfo> GetCostVariantValues(int costVariantID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CostVariantValuesInfo> lstCostVarValue = AspxItemMgntController.GetCostVariantValues(costVariantID, aspxCommonObj);
    //        return lstCostVarValue;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteCostVariantForItem(int itemId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemMgntController.DeleteCostVariantForItem(itemId, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<VariantCombination> GetCostVariantsOfItem(int itemId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<VariantCombination> lstVarComb = AspxItemMgntController.GetCostVariantsOfItem(itemId, aspxCommonObj);
    //        return lstVarComb;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void SaveAndUpdateItemCostVariantCombination(CostVariantsCombination itemCostVariants, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        string cvCombinations = string.Empty;
    //        foreach (var objCombination in itemCostVariants.VariantOptions)
    //        {
    //            cvCombinations += objCombination.CombinationIsActive;
    //            cvCombinations += "," + objCombination.ImageFile;
    //            cvCombinations += "," + objCombination.CombinationPriceModifier;
    //            cvCombinations += "," + objCombination.CombinationPriceModifierType;
    //            cvCombinations += "," + objCombination.CombinationQuantity;
    //            cvCombinations += "," + objCombination.CombinationType;
    //            cvCombinations += "," + objCombination.CombinationValues;
    //            cvCombinations += "," + objCombination.CombinationValuesName;
    //            cvCombinations += "," + objCombination.CombinationWeightModifier;
    //            cvCombinations += "," + objCombination.CombinationWeightModifierType;
    //            cvCombinations += "," + objCombination.DisplayOrder;
    //            if (itemCostVariants.VariantOptions.Count - 1 != 0)
    //                cvCombinations += "%";
    //        }
    //        // cvCombinations = cvCombinations.Replace("Upload/temp/", "Modules/AspxCommerce/AspxItemsManagement/uploads/");
    //        FileHelperController Fch = new FileHelperController();
    //        string tempFolder = @"Upload\temp";
    //        string destPath = @"Modules/AspxCommerce/AspxItemsManagement/uploads/";
    //        Fch.MoveVariantsImageFile(tempFolder, destPath, itemCostVariants, aspxCommonObj);
    //        AspxItemMgntController.SaveAndUpdateItemCostVariantCombination(itemCostVariants, aspxCommonObj, cvCombinations);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //#endregion
    //#endregion

    #region Item Tax
    [WebMethod]
    public List<TaxRulesInfo> GetAllTaxRules(int storeID, int portalID, bool isActive)
    {
        List<TaxRulesInfo> lstTaxManageRule = AspxItemMgntController.GetAllTaxRules(storeID, portalID, isActive);
        return lstTaxManageRule;
    }
    #endregion

    //#region Item Tax Class Name
    //[WebMethod]
    //public List<TaxItemClassInfo> GetAllTaxItemClass(AspxCommonInfo aspxCommonObj, bool isActive)
    //{
    //    try
    //    {

    //        List<TaxItemClassInfo> lstTaxItemClass = AspxItemMgntController.GetAllTaxItemClass(aspxCommonObj, isActive);
    //        return lstTaxItemClass;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //#endregion

    //#region Downloadable Item Details
    //[WebMethod]
    //public List<DownLoadableItemInfo> GetDownloadableItem(int itemId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<DownLoadableItemInfo> lstDownItem = AspxItemMgntController.GetDownloadableItem(itemId, aspxCommonObj);
    //        return lstDownItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion


    //#region Front Image Gallery
    //[WebMethod]
    //public List<ItemsInfoSettings> GetItemsImageGalleryInfoBySKU(string itemSKU, AspxCommonInfo aspxCommonObj,
    //                                                                 string combinationId)
    //{
    //    List<ItemsInfoSettings> itemsInfoContainer = AspxImageGalleryController.GetItemsImageGalleryInfoByItemSKU(itemSKU, aspxCommonObj, combinationId);
    //    return itemsInfoContainer;
    //}

    //[WebMethod]
    //public List<ImageGalleryItemsInfo> GetItemsImageGalleryInfo(Int32 storeID, Int32 portalID, string userName, string culture)
    //{
    //    List<ImageGalleryItemsInfo> itemsInfoList = AspxImageGalleryController.GetItemsImageGalleryList(storeID, portalID, userName, culture);
    //    return itemsInfoList;
    //}

    //[WebMethod]
    //public List<ImageGalleryItemsInfo> GetItemsGalleryInfo(Int32 storeID, Int32 portalID, string culture)
    //{
    //    List<ImageGalleryItemsInfo> itemsInfoList = AspxImageGalleryController.GetItemInfoList(storeID, portalID, culture);
    //    return itemsInfoList;
    //}

    //[WebMethod]
    //public ImageGalleryInfo ReturnSettings(Int32 userModuleID, AspxCommonInfo aspxCommonObj)
    //{
    //    ImageGalleryInfo infoObject = AspxImageGalleryController.GetGallerySettingValues(userModuleID, aspxCommonObj);
    //    return infoObject;
    //}

    //[WebMethod]
    //public List<int> ReturnDimension(Int32 userModuleID, AspxCommonInfo aspxCommonObj)
    //{
    //    List<int> param = new List<int>();
    //    ImageGalleryInfo info = new ImageGalleryInfo();
    //    ImageGallerySqlProvider settings = new ImageGallerySqlProvider();

    //    info = AspxImageGalleryController.GetGallerySettingValues(userModuleID, aspxCommonObj);
    //    param.Add(int.Parse(info.ImageWidth));
    //    param.Add(int.Parse(info.ImageHeight));
    //    param.Add(int.Parse(info.ThumbWidth));
    //    param.Add(int.Parse(info.ThumbHeight));
    //    //param.Add(int.Parse(info.ZoomShown));
    //    return param;
    //}
    //#endregion

    //#region Category Management
    //[WebMethod]
    //public bool CheckUniqueCategoryName(string catName, int catId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        bool isUnique = AspxCategoryManageController.CheckUniqueCategoryName(catName, catId, aspxCommonObj);
    //        return isUnique;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public bool IsUnique(Int32 storeID, Int32 portalID, Int32 itemID, Int32 attributeID, Int32 attributeType, string attributeValue)
    //{
    //    try
    //    {
    //        /*
    //    1	TextField
    //    2	TextArea
    //    3	Date
    //    4	Boolean
    //    5	MultipleSelect
    //    6	DropDown
    //    7	Price
    //    8	File
    //    9	Radio
    //    10	RadioButtonList
    //    11	CheckBox
    //    12	CheckBoxList
    //     */
    //        bool isUnique = AspxCategoryManageController.IsUnique(storeID, portalID, itemID, attributeID, attributeType, attributeValue);
    //        return isUnique;
    //    }
    //    catch (Exception e)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        errHandler.LogWCFException(e);
    //        return false;
    //    }
    //}

    //[WebMethod]
    //public List<AttributeFormInfo> GetCategoryFormAttributes(Int32 categoryID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {

    //        List<AttributeFormInfo> frmFieldList = AspxCategoryManageController.GetCategoryFormAttributes(categoryID, aspxCommonObj);
    //        return frmFieldList;
    //    }
    //    catch (Exception e)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        errHandler.LogWCFException(e);
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<CategoryInfo> GetCategoryAll(bool isActive, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CategoryInfo> catList = AspxCategoryManageController.GetCategoryAll(isActive, aspxCommonObj);
    //        return catList;
    //    }
    //    catch (Exception e)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        errHandler.LogWCFException(e);
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<CategoryAttributeInfo> GetCategoryByCategoryID(Int32 categoryID, AspxCommonInfo aspxCommonObj)
    //{
    //    List<CategoryAttributeInfo> catList = AspxCategoryManageController.GetCategoryByCategoryID(categoryID, aspxCommonObj);
    //    return catList;
    //}

    //[WebMethod]
    //public string SaveCategory(CategoryInfo.CategorySaveBasicInfo categoryObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        int categoryId = AspxCategoryManageController.SaveCategory(categoryObj, aspxCommonObj);
    //        CacheHelper.Clear("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID);
    //        CacheHelper.Clear("CategoryForSearch" + aspxCommonObj.StoreID + aspxCommonObj.PortalID);
    //        if (categoryObj.ParentId > 0)
    //        {
    //            return
    //                "({\"returnStatus\":1,\"Message\":\"Sub category has been saved successfully.\",\"categoryID\":" +
    //                categoryId + "})";
    //        }
    //        else
    //        {
    //            return "({\"returnStatus\":1,\"Message\":\"Category has been saved successfully.\",\"categoryID\":" +
    //                   categoryId + "})";
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        if (errHandler.LogWCFException(e))
    //        {
    //            return "({\"returnStatus\":-1,\"errorMessage\":'" + e.Message + "'})";
    //        }
    //        else
    //        {
    //            return "({\"returnStatus\":-1,\"errorMessage\":\"Error while saving category!\"})";
    //        }
    //    }
    //}

    //[WebMethod]
    //public string DeleteCategory(Int32 storeID, Int32 portalID, Int32 categoryID, string userName, string culture)
    //{
    //    try
    //    {
    //        AspxCategoryManageController.DeleteCategory(storeID, portalID, categoryID, userName, culture);
    //        CacheHelper.Clear("CategoryInfo" + storeID.ToString() + portalID.ToString());
    //        CacheHelper.Clear("CategoryForSearch" + storeID.ToString() + portalID.ToString());
    //        return "({ \"returnStatus\" : 1 , \"Message\" : \"Category has been deleted successfully.\" })";

    //    }
    //    catch (Exception e)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        if (errHandler.LogWCFException(e))
    //        {
    //            return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + e.Message + "\" })";
    //        }
    //        else
    //        {
    //            return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting category!\" })";
    //        }
    //    }
    //}

    //[WebMethod]
    //public List<CategoryItemInfo> GetCategoryItems(Int32 offset, System.Nullable<int> limit, GetCategoryItemInfo categoryItemsInfo, AspxCommonInfo aspxCommonObj, bool serviceBit)
    //{
    //    try
    //    {

    //        List<CategoryItemInfo> listCategoryItem = AspxCategoryManageController.GetCategoryItems(offset, limit, categoryItemsInfo, aspxCommonObj, serviceBit);
    //        return listCategoryItem;
    //    }
    //    catch (Exception e)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        errHandler.LogWCFException(e);
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public string GetCategoryCheckedItems(int CategoryID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {

    //        string categoryItem = AspxCategoryManageController.GetCategoryCheckedItems(CategoryID, aspxCommonObj);
    //        return categoryItem;
    //    }
    //    catch (Exception e)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        errHandler.LogWCFException(e);
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public string SaveChangesCategoryTree(string categoryIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {

    //        AspxCategoryManageController.SaveChangesCategoryTree(categoryIDs, aspxCommonObj);
    //        CacheHelper.Clear("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID);
    //        CacheHelper.Clear("CategoryForSearch" + aspxCommonObj.StoreID + aspxCommonObj.PortalID);
    //        return "({ \"returnStatus\" : 1 , \"Message\" : \"Category tree saved successfully.\" })";

    //    }
    //    catch (Exception e)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        if (errHandler.LogWCFException(e))
    //        {
    //            return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + e.Message + "\" })";
    //        }
    //        else
    //        {
    //            return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving category tree!\" })";
    //        }
    //    }
    //}
    //[WebMethod]
    //public void ActivateCategory(int categoryID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCategoryManageController.ActivateCategory(categoryID, aspxCommonObj);
    //        CacheHelper.Clear("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID);
    //        CacheHelper.Clear("CategoryForSearch" + aspxCommonObj.StoreID + aspxCommonObj.PortalID);

    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //[WebMethod]
    //public void DeActivateCategory(int categoryID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCategoryManageController.DeActivateCategory(categoryID, aspxCommonObj);
    //        CacheHelper.Clear("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID);
    //        CacheHelper.Clear("CategoryForSearch" + aspxCommonObj.StoreID + aspxCommonObj.PortalID);

    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //#endregion

    ////---------------- File Uploader --------------
    //    [WebMethod]
    //    public string SaveUploadFiles(string fileList)
    //    {
    //        try
    //        {
    //            string fileName = string.Empty;
    //            //HttpPostedFile ss; 
    //            //string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
    //            //string strExtension = Path.GetExtension(HttpContext.Current.Request.Files[0].FileName).ToLower();
    //            //string strSaveLocation = HttpContext.Current.Server.MapPath("Upload") + "" + strFileName;
    //            //HttpContext.Current.Request.Files[0].SaveAs(strSaveLocation);

    //            ////contentType: "application/json; charset=utf-8",
    //            //// contentType: "multipart/form-data"
    //            ////contentType: "text/html; charset=utf-8"
    //            //HttpContext.Current.Response.ContentType = "text/plain; charset=utf-8";
    //            //HttpContext.Current.Response.Write(strSaveLocation);
    //            //HttpContext.Current.Response.End();

    //            if (HttpContext.Current.Request.Files != null)
    //            {
    //                HttpFileCollection files = HttpContext.Current.Request.Files;
    //                for (int i = 0; i < files.Count; i++)
    //                {
    //                    HttpPostedFile file = files[i];
    //                    if (file.ContentLength > 0)
    //                    {
    //                        fileName = file.FileName;
    //                    }
    //                }
    //            }
    //            ////Code ommited
    //            //string jsonClient = null;
    //            //var j = new { fileName = response.key1 };
    //            //var s = new JavaScriptSerializer();
    //            //jsonClient = s.Serialize(j);
    //            //return jsonClient;

    //            return fileName;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //--------------------CategoryItems------------------------------
    //[WebMethod]
    //public List<ItemsGetCategoryIDInfo> BindCategoryItems(int categoryID, int storeID, int portalID, string userName, string cultureName)
    //{
    //    try
    //    {
    //        List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
    //        parameter.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
    //        parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
    //        parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
    //        parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
    //        parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
    //        SQLHandler sqlH = new SQLHandler();
    //        List<ItemsGetCategoryIDInfo> Bind = sqlH.ExecuteAsList<ItemsGetCategoryIDInfo>("usp_Aspx_ItemsGetAllBycategoryID", parameter);
    //        return Bind;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //----------------General Search View As DropDown Options----------------------------
  

   
    //#region RecentlyViewedItems   

    //[WebMethod]
    //public void AddUpdateRecentlyViewedItems(RecentlyAddedItemInfo addUpdateRecentObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemMgntController.AddUpdateRecentlyViewedItems(addUpdateRecentObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Item Details Module
    //[WebMethod]
    //public ItemBasicDetailsInfo GetItemBasicInfoByitemSKU(string itemSKU, AspxCommonInfo aspxCommonObj)
    //{
    //    ItemBasicDetailsInfo frmItemAttributes = AspxItemMgntController.GetItemBasicInfo(itemSKU, aspxCommonObj);
    //    return frmItemAttributes;
    //}

    //[WebMethod]
    //public List<AttributeFormInfo> GetItemDetailsByitemSKU(string itemSKU, int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
    //{
    //    List<AttributeFormInfo> frmItemAttributes = AspxItemMgntController.GetItemDetailsInfoByItemSKU(itemSKU, attributeSetID, itemTypeID, aspxCommonObj);
    //    return frmItemAttributes;
    //}

    //#endregion

    //#region PopularTags Module
    ////[WebMethod]
    ////public void AddTagsOfItem(string itemSKU, string tags, AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        AspxTagsController.AddTagsOfItem(itemSKU, tags, aspxCommonObj);
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}

    ////[WebMethod]
    ////public List<ItemTagsInfo> GetItemTags(string itemSKU, AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        List<ItemTagsInfo> lstItemTags = AspxTagsController.GetItemTags(itemSKU, aspxCommonObj);
    ////        return lstItemTags;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}

    ////[WebMethod]
    ////public void DeleteUserOwnTag(string itemTagID, AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        AspxTagsController.DeleteUserOwnTag(itemTagID, aspxCommonObj);
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}
    //[WebMethod]
    //public void DeleteMultipleTag(string itemTagIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTagsController.DeleteMultipleTag(itemTagIDs, aspxCommonObj);

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<TagDetailsInfo> GetTagDetailsListPending(int offset, int limit, string tag, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<TagDetailsInfo> lstTagDetail = AspxTagsController.GetTagDetailsListPending(offset, limit, tag, aspxCommonObj);
    //        return lstTagDetail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<TagDetailsInfo> GetTagDetailsList(int offset, int limit, string tag, string tagStatus, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<TagDetailsInfo> lstTagDetail = AspxTagsController.GetTagDetailsList(offset, limit, tag, tagStatus, aspxCommonObj);
    //        return lstTagDetail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////[WebMethod]
    ////public List<TagDetailsInfo> GetAllPopularTags(AspxCommonInfo aspxCommonObj, int count)
    ////{
    ////    try
    ////    {
    ////        List<TagDetailsInfo> lstTagDetail = AspxTagsController.GetAllPopularTags(aspxCommonObj, count);
    ////        return lstTagDetail;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}

    ////[WebMethod]
    ////public List<TagDetailsInfo> GetTagsByUserName(AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        List<TagDetailsInfo> lstTagDetail = AspxTagsController.GetTagsByUserName(aspxCommonObj);
    ////        return lstTagDetail;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}

    //#region Tags Reports
    ////---------------------Customer tags------------
    //[WebMethod]
    //public List<CustomerTagInfo> GetCustomerTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CustomerTagInfo> lstCustomerTag = AspxTagsController.GetCustomerTagDetailsList(offset, limit, aspxCommonObj);
    //        return lstCustomerTag;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------Show Customer tags list------------
    //[WebMethod]
    //public List<ShowCustomerTagsListInfo> ShowCustomerTagList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShowCustomerTagsListInfo> lstCustTag = AspxTagsController.ShowCustomerTagList(offset, limit, aspxCommonObj);
    //        return lstCustTag;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------Item tags details------------
    //[WebMethod]
    //public List<ItemTagsDetailsInfo> GetItemTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemTagsDetailsInfo> lstItemTags = AspxTagsController.GetItemTagDetailsList(offset, limit, aspxCommonObj);
    //        return lstItemTags;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------Show Item tags list------------
    //[WebMethod]
    //public List<ShowItemTagsListInfo> ShowItemTagList(int offset, System.Nullable<int> limit, int itemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShowItemTagsListInfo> lstShowItemTags = AspxTagsController.ShowItemTagList(offset, limit, itemID, aspxCommonObj);
    //        return lstShowItemTags;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------Popular tags details------------
    //[WebMethod]
    //public List<PopularTagsInfo> GetPopularTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<PopularTagsInfo> lstPopTags = AspxTagsController.GetPopularTagDetailsList(offset, limit, aspxCommonObj);
    //        return lstPopTags;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------Show Popular tags list------------
    //[WebMethod]
    //public List<ShowpopulartagsDetailsInfo> ShowPopularTagList(int offset, System.Nullable<int> limit, string tagName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShowpopulartagsDetailsInfo> lstShowPopTag = AspxTagsController.ShowPopularTagList(offset, limit, tagName, aspxCommonObj);
    //        return lstShowPopTag;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////[WebMethod]
    ////public List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset, int limit, string tagIDs, int SortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        List<ItemBasicDetailsInfo> lstItemBasic = AspxTagsController.GetUserTaggedItems(offset, limit, tagIDs, SortBy, rowTotal, aspxCommonObj);
    ////        return lstItemBasic;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}
    //#endregion
    //#endregion

    //#region Tags Management
    //[WebMethod]
    //public void UpdateTag(string itemTagIDs, int? itemId, int statusID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTagsController.UpdateTag(itemTagIDs, itemId, statusID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteTag(string itemTagIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTagsController.DeleteTag(itemTagIDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ItemBasicDetailsInfo> GetItemsByMultipleItemID(string itemIDs, string tagName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemBasicDetailsInfo> lstItemBasic = AspxTagsController.GetItemsByMultipleItemID(itemIDs, tagName, aspxCommonObj);
    //        return lstItemBasic;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    //#endregion    

    //#region Search
    ////Search Setting
    //public SearchSettingInfo GetSearchSetting(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        SearchSettingInfo objSearchSetting = AspxSearchController.GetSearchSetting(aspxCommonObj);
    //        return objSearchSetting;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public void SetSearchSetting(SearchSettingInfo searchSettingObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxSearchController.SetSearchSetting(searchSettingObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    ////Auto Complete search Box
    //[WebMethod]
    //public List<SearchTermList> GetSearchedTermList(string search, AspxCommonInfo aspxCommonObj)
    //{
    //    List<SearchTermList> srInfo = AspxSearchController.GetSearchedTermList(search, aspxCommonObj);
    //    return srInfo;
    //}

    //#region General Search
    ////----------------General Search Sort By DropDown Options----------------------------
    //[WebMethod]
    //public List<ItemBasicDetailsInfo> GetSimpleSearchResult(int offset, int limit, int categoryID,bool isGiftCard, string searchText, int sortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemBasicDetailsInfo> lstItemBasic = AspxSearchController.GetSimpleSearchResult(offset, limit, categoryID,isGiftCard, searchText, sortBy, rowTotal, aspxCommonObj);
    //        return lstItemBasic;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////[WebMethod]
    ////public List<SortOptionTypeInfo> BindItemsSortByList()
    ////{
    ////    try
    ////    {
    ////        List<SortOptionTypeInfo> bind = AspxSearchController.BindItemsSortByList();
    ////        return bind;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}

    //[WebMethod]
    //public List<ItemBasicDetailsInfo> GetItemsByGeneralSearch(int storeID, int portalID, string nameSearchText, float priceFrom, float priceTo,
    //                                                          string skuSearchText, int categoryID, string categorySearchText, bool isByName, bool isByPrice, bool isBySKU, bool isByCategory, string userName, string cultureName)
    //{
    //    try
    //    {
    //        List<ItemBasicDetailsInfo> lstItemBasic = AspxSearchController.GetItemsByGeneralSearch(storeID, portalID, nameSearchText, priceFrom, priceTo,
    //                                                                                             skuSearchText, categoryID, categorySearchText, isByName, isByPrice, isBySKU, isByCategory, userName, cultureName);
    //        return lstItemBasic;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CategoryInfo> GetAllCategoryForSearch(string prefix, bool isActive, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CategoryInfo> catList = AspxSearchController.GetAllCategoryForSearch(prefix, isActive, aspxCommonObj);
    //        return catList;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<SearchTermList> GetTopSearchTerms(AspxCommonInfo aspxCommonObj, int Count)
    //{
    //    try
    //    {
    //        List<SearchTermList> searchList = AspxSearchController.GetTopSearchTerms(aspxCommonObj, Count);
    //        return searchList;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //#endregion

    //#region Advance Search
    //[WebMethod]
    //public List<ItemTypeInfo> GetItemTypeList()
    //{
    //    try
    //    {
    //        List<ItemTypeInfo> lstItemType = AspxSearchController.GetItemTypeList();
    //        return lstItemType;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<AttributeFormInfo> GetAttributeByItemType(int itemTypeID, int storeID, int portalID, string cultureName)
    //{
    //    try
    //    {
    //        List<AttributeFormInfo> lstAttrForm = AspxSearchController.GetAttributeByItemType(itemTypeID, storeID, portalID, cultureName);
    //        return lstAttrForm;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region More Advanced Search
    ////------------------get dyanamic Attributes for serach-----------------------   
    //[WebMethod]
    //public List<AttributeShowInAdvanceSearchInfo> GetAttributes(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AttributeShowInAdvanceSearchInfo> lstAttr = AspxSearchController.GetAttributes(aspxCommonObj);
    //        return lstAttr;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////------------------get items by dyanamic Advance serach-----------------------
    //[WebMethod]
    //public List<ItemBasicDetailsInfo> GetItemsByDyanamicAdvanceSearch(int offset, int limit, AspxCommonInfo aspxCommonObj, System.Nullable<int> categoryID, bool isGiftCard, string searchText, int brandId,
    //                                                                  System.Nullable<float> priceFrom, System.Nullable<float> priceTo, string attributeIds, int rowTotal, int SortBy)
    //{
    //    try
    //    {
    //        List<ItemBasicDetailsInfo> lstItemBasic = AspxSearchController.GetItemsByDyanamicAdvanceSearch(offset, limit, aspxCommonObj, categoryID, isGiftCard, searchText, brandId, priceFrom, priceTo, attributeIds, rowTotal, SortBy);
    //        return lstItemBasic;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<Filter> GetDynamicAttributesForAdvanceSearch(AspxCommonInfo aspxCommonObj, int CategoryID, bool IsGiftCard)
    //{
    //    List<Filter> lstFilter = AspxSearchController.GetDynamicAttributesForAdvanceSearch(aspxCommonObj, CategoryID, IsGiftCard);
    //    return lstFilter;
    //}
    //[WebMethod]
    //public List<BrandItemsInfo> GetAllBrandForSearchByCategoryID(AspxCommonInfo aspxCommonObj, int CategoryID, bool IsGiftCard)
    //{
    //    try
    //    {
    //        List<BrandItemsInfo> lstBrandItem = AspxSearchController.GetAllBrandForSearchByCategoryID(aspxCommonObj, CategoryID, IsGiftCard);
    //        return lstBrandItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#endregion

    #region Category Details
    [WebMethod]
    public static List<CategoryDetailsInfo> BindCategoryDetails(int categoryID, int count, int level, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<CategoryDetailsInfo> lstCatDetail = AspxBrowseCategoryProvider.BindCategoryDetails(categoryID, count, level, aspxCommonObj);
            return lstCatDetail;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<CategoryImage> GetCategoryImageList(string categoryName, int storeID, int portalID, string cultureName)
    {
        try
        {
            var parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@CategoryName", categoryName));
            parameterCollection.Add(new KeyValuePair<string, object>("@Culture", cultureName));
            var sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CategoryImage>("[usp_Aspx_GetImagesByCategory]", parameterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<CategoryDetailsOptionsInfo> GetCategoryDetailsOptions(int offset, int limit, string categorykey, int storeID, int portalID, string userName, string cultureName, int sortBy)
    {
        try
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@categorykey", categorykey));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@Culture", cultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@SortBy", sortBy));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>("usp_Aspx_CategoryDetailsOptions", parameterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<CategoryDetailsInfo> GetBrowseByCategorySetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<CategoryDetailsInfo> lstCatDetail = AspxBrowseCategoryController.GetBrowseByCategorySetting(aspxCommonObj);
            return lstCatDetail;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    [WebMethod]
    public void UpdateBrowseByCategorySetting(string settingKeys, string settingValues, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxBrowseCategoryController.UpdateBrowseByCategorySetting(settingKeys, settingValues, aspxCommonObj);

        }
        catch (Exception e)
        {
            throw e;
        }
    }


    #endregion

    //#region Rating/Reviews

    //#region rating/ review
    //---------------------save rating/ review Items-----------------------
    //[WebMethod]
    //public List<ItemRatingAverageInfo> GetItemAverageRating(string itemSKU, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemRatingAverageInfo> avgRating = AspxRatingReviewController.GetItemAverageRating(itemSKU, aspxCommonObj);
    //        return avgRating;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //---------------------rating/ review Items criteria--------------------------
    //[WebMethod]
    //public List<RatingCriteriaInfo> GetItemRatingCriteria(AspxCommonInfo aspxCommonObj, bool isFlag)
    //{
    //    try
    //    {

    //        List<RatingCriteriaInfo> lstRating = AspxCommonController.GetItemRatingCriteria(aspxCommonObj, isFlag);
    //        return lstRating;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<RatingCriteriaInfo> GetItemRatingCriteriaByReviewID(AspxCommonInfo aspxCommonObj, int itemReviewID, bool isFlag)
    //{
    //    try
    //    {
    //        List<RatingCriteriaInfo> rating = AspxRatingReviewController.GetItemRatingCriteriaByReviewID(aspxCommonObj, itemReviewID, isFlag);
    //        return rating;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------save rating/ review Items-----------------------
    //[WebMethod]
    //public void SaveItemRating(ItemReviewBasicInfo ratingSaveObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxRatingReviewController.SaveItemRating(ratingSaveObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //---------------------update rating/ review Items-----------------------
    //[WebMethod]
    //public void UpdateItemRating(ItemReviewBasicInfo ratingManageObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCommonController.UpdateItemRating(ratingManageObj, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////---------------------Get rating/ review of Item Per User ------------------
    //[WebMethod]
    //public List<ItemRatingByUserInfo> GetItemRatingPerUser(int offset, int limit, string itemSKU, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemRatingByUserInfo> lstItemRating = AspxRatingReviewController.GetItemRatingPerUser(offset, limit, itemSKU, aspxCommonObj);
    //        return lstItemRating;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //---------------------Get rating/ review of Item Per User ------------------
    //[WebMethod]
    //public List<RatingLatestInfo> GetRecentItemReviewsAndRatings(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<RatingLatestInfo> lstRatingNew = AspxCommonController.GetRecentItemReviewsAndRatings(offset, limit, aspxCommonObj);
    //        return lstRatingNew;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////---------------------Get rating/ review of Item Per User ------------------
    //[WebMethod]
    //public List<ItemReviewDetailsInfo> GetItemRatingByReviewID(int itemReviewID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemReviewDetailsInfo> lstItemRVDetail = AspxCommonController.GetItemRatingByReviewID(itemReviewID, aspxCommonObj);
    //        return lstItemRVDetail;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////------------------------Item single rating management------------------------
    //[WebMethod]
    //public void DeleteSingleItemRating(string itemReviewID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCommonController.DeleteSingleItemRating(itemReviewID, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////---------------Delete multiple item rating informations--------------------------
    //[WebMethod]
    //public void DeleteMultipleItemRatings(string itemReviewIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxRatingReviewController.DeleteMultipleItemRatings(itemReviewIDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //---------------------Bind in Item Rating Information in grid-------------------------
    //[WebMethod]
    //public List<UserRatingInformationInfo> GetAllUserReviewsAndRatings(int offset, int limit, UserRatingBasicInfo userRatingObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<UserRatingInformationInfo> bind = AspxRatingReviewController.GetAllUserReviewsAndRatings(offset, limit, userRatingObj, aspxCommonObj);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //-------------------------list item names in dropdownlist/item rating management---------------------
    //[WebMethod]
    //public List<ItemsReviewInfo> GetAllItemList(string searchText, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemsReviewInfo> items = AspxRatingReviewController.GetAllItemList(searchText, aspxCommonObj);
    //        return items;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckReviewByUser(int itemID, AspxCommonInfo aspxCommonObj)
    //{

    //    bool isReviewExist = AspxRatingReviewController.CheckReviewByUser(itemID, aspxCommonObj);
    //    return isReviewExist;

    //}

    //[WebMethod]
    //public bool CheckReviewByIP(int itemID, AspxCommonInfo aspxCommonObj, string userIP)
    //{
    //    bool isReviewExist = AspxRatingReviewController.CheckReviewByIP(itemID, aspxCommonObj, userIP);
    //    return isReviewExist;
    //}

    //#endregion

    //#region Item Rating Criteria Manage/Admin
    //--------------------Item Rating Criteria Manage/Admin--------------------------
    //[WebMethod]
    //public List<ItemRatingCriteriaInfo> ItemRatingCriteriaManage(int offset, int limit, string criteria, System.Nullable<bool> isActive,  AspxCommonInfo aspxCommonObj)
    //{
    //public List<ItemRatingCriteriaInfo> ItemRatingCriteriaManage(int offset, int limit, ItemRatingCriteriaInfo itemCriteriaObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemRatingCriteriaInfo> lstRatingCriteria = AspxRatingReviewController.ItemRatingCriteriaManage(offset, limit, itemCriteriaObj, aspxCommonObj);
    //        return lstRatingCriteria;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //--------------- ItemRating Criteria Manage-------------------------------
    //[WebMethod]
    //public void AddUpdateItemCriteria(ItemRatingCriteriaInfo itemCriteriaObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxRatingReviewController.AddUpdateItemCriteria(itemCriteriaObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //--------------- ItemRating Criteria Manage-------------------------------
    //[WebMethod]
    //public void DeleteItemRatingCriteria(string IDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxRatingReviewController.DeleteItemRatingCriteria(IDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion
    //#endregion

    //#region Cost Variants Management
    ////--------------------bind Cost Variants in Grid--------------------------
    //[WebMethod]
    //public List<CostVariantInfo> GetCostVariants(int offset, int limit, string variantName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CostVariantInfo> bind = AspxCostVarMgntController.GetCostVariants(offset, limit, variantName, aspxCommonObj);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------Delete multiple cost variants --------------------------
    //[WebMethod]
    //public void DeleteMultipleCostVariants(string costVariantIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCostVarMgntController.DeleteMultipleCostVariants(costVariantIDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////------------------------ single Cost Variants management------------------------
    //[WebMethod]
    //public void DeleteSingleCostVariant(string costVariantID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCostVarMgntController.DeleteSingleCostVariant(costVariantID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<AttributesInputTypeInfo> GetCostVariantInputTypeList()
    //{
    //    try
    //    {
    //        List<AttributesInputTypeInfo> ml = AspxCostVarMgntController.GetCostVariantInputTypeList();
    //        return ml;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------- bind (edit) cost Variant management--------------------
    //[WebMethod]
    //public List<CostVariantsGetByCostVariantIDInfo> GetCostVariantInfoByCostVariantID(int costVariantID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CostVariantsGetByCostVariantIDInfo> lstCostVar = AspxCostVarMgntController.GetCostVariantInfoByCostVariantID(costVariantID, aspxCommonObj);
    //        return lstCostVar;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------- bind (edit) cost Variant values for cost variant ID --------------------
    //[WebMethod]
    //public List<CostVariantsvalueInfo> GetCostVariantValuesByCostVariantID(int costVariantID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CostVariantsvalueInfo> lstCVValue = AspxCostVarMgntController.GetCostVariantValuesByCostVariantID(costVariantID, aspxCommonObj);
    //        return lstCVValue;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<CostVariantsvalueInfo> GetCostVariantValuesByCostVariantIDForAdmin(int costVariantID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CostVariantsvalueInfo> lstCVValue = AspxCostVarMgntController.GetCostVariantValuesByCostVariantIDForAdmin(costVariantID, aspxCommonObj);
    //        return lstCVValue;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-----------Save and update Costvariant options-------------------------
    //[WebMethod]
    //public void SaveAndUpdateCostVariant(CostVariantsGetByCostVariantIDInfo variantObj, string variantOptions, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCostVarMgntController.SaveAndUpdateCostVariant(variantObj, variantOptions, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////---------------- Added for unique name check ---------------------
    //[WebMethod]
    //public bool CheckUniqueCostVariantName(string costVariantName, int costVariantId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        bool isUnique = AspxCostVarMgntController.CheckUniqueCostVariantName(costVariantName, costVariantId, aspxCommonObj);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Refer-A-Friend
    ////-------------------------Save AND SendEmail Messages For Refer-A-Friend----------------
    //[WebMethod]
    //public void SaveAndSendEmailMessage(AspxCommonInfo aspxCommonObj, ReferToFriendEmailInfo referToFriendObj, WishItemEmailInfo messageBodyDetail)
    //{
    //    try
    //    {
    //        AspxReferFriendController.SaveAndSendEmailMessage(aspxCommonObj, referToFriendObj, messageBodyDetail);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------------bind Email list in Grid--------------------------
    //[WebMethod]
    //public List<ReferToFriendInfo> GetAllReferToAFriendEmailList(int offset, int limit, string senderName, string senderEmail, string receiverName, string receiverEmail, string subject, int storeID, int portalID, string userName)
    //{
    //    try
    //    {
    //        List<ReferToFriendInfo> bind = AspxReferFriendController.GetAllReferToAFriendEmailList(offset, limit, senderName, senderEmail, receiverName, receiverEmail, subject, storeID, portalID, userName);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-----------------Delete Email list --------------------------------
    //[WebMethod]
    //public void DeleteReferToFriendEmailUser(string emailAFriendIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxReferFriendController.DeleteReferToFriendEmailUser(emailAFriendIDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------Get UserReferred Friends--------------------------
    ////[WebMethod]
    ////public List<ReferToFriendInfo> GetUserReferredFriends(int offset, int limit, AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        List<ReferToFriendInfo> lstReferFriend = AspxReferFriendController.GetUserReferredFriends(offset, limit, aspxCommonObj);
    ////        return lstReferFriend;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}
    //#endregion

    //#region Shipping method management
    ////-----------Bind Shipping methods In grid-----------------------------
    //[WebMethod]
    //public List<ShippingMethodInfo> GetStoreProvidersAvailableMethod(int providerId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShippingMethodInfo> lstShipMethod = AspxShipMethodMgntController.GetStoreProvidersAvailableMethod(providerId, aspxCommonObj);
    //        return lstShipMethod;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<ShippingMethodInfoByProvider> GetShippingMethodListByProvider(int offset, int limit, int shippingProviderId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShippingMethodInfoByProvider> shipping = AspxShipMethodMgntController.GetShippingMethodsByProvider(offset, limit, shippingProviderId, aspxCommonObj);
    //        return shipping;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<ShippingMethodInfo> GetShippingMethodList(int offset, int limit, ShippingMethodInfoByProvider shippingMethodObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShippingMethodInfo> shipping = AspxShipMethodMgntController.GetShippingMethods(offset, limit, shippingMethodObj, aspxCommonObj);
    //        return shipping;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-----------------delete multiple shipping methods----------------------
    //[WebMethod]
    //public void DeleteShippingByShippingMethodID(string shippingMethodIds, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxShipMethodMgntController.DeleteShippings(shippingMethodIds, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////----------------bind shipping service list---------------
    //[WebMethod]
    //public List<ShippingProviderListInfo> GetShippingProviderList(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShippingProviderListInfo> lstShipProvider = AspxShipMethodMgntController.GetShippingProviderList(aspxCommonObj);
    //        return lstShipProvider;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------------------SaveAndUpdate shipping methods-------------------
    //[WebMethod]
    //public void SaveAndUpdateShippingMethods(int shippingMethodID, string shippingMethodName, string prevFilePath, string newFilePath, string alternateText, int displayOrder, string deliveryTime,
    //                                         decimal weightLimitFrom, decimal weightLimitTo, int shippingProviderID, bool isActive, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        FileHelperController fileObj = new FileHelperController();
    //        string uplodedValue = string.Empty;
    //        if (newFilePath != null && prevFilePath != newFilePath)
    //        {
    //            string tempFolder = @"Upload\temp";
    //            uplodedValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, newFilePath, @"Modules\AspxCommerce\AspxShippingManagement\uploads\", shippingMethodID, aspxCommonObj, "ship_");
    //        }
    //        else
    //        {
    //            uplodedValue = prevFilePath;
    //        }
    //        AspxShipMethodMgntController.SaveAndUpdateShippings(shippingMethodID, shippingMethodName, uplodedValue, alternateText, displayOrder, deliveryTime, weightLimitFrom, weightLimitTo, shippingProviderID, isActive, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------------bind Cost dependencies  in Grid--------------------------
    //[WebMethod]
    //public List<ShippingCostDependencyInfo> GetCostDependenciesListInfo(int offset, int limit, AspxCommonInfo aspxCommonObj, int shippingMethodId)
    //{
    //    try
    //    {
    //        List<ShippingCostDependencyInfo> bind = AspxShipMethodMgntController.GetCostDependenciesListInfo(offset, limit, aspxCommonObj, shippingMethodId);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------------bind Weight dependencies  in Grid--------------------------
    //[WebMethod]
    //public List<ShippingWeightDependenciesInfo> GetWeightDependenciesListInfo(int offset, int limit, AspxCommonInfo aspxCommonObj, int shippingMethodId)
    //{
    //    try
    //    {
    //        List<ShippingWeightDependenciesInfo> bind = AspxShipMethodMgntController.GetWeightDependenciesListInfo(offset, limit, aspxCommonObj, shippingMethodId);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------------bind Item dependencies  in Grid--------------------------
    //[WebMethod]
    //public List<ShippingItemDependenciesInfo> GetItemDependenciesListInfo(int offset, int limit, AspxCommonInfo aspxCommonObj, int shippingMethodId)
    //{
    //    try
    //    {
    //        List<ShippingItemDependenciesInfo> bind = AspxShipMethodMgntController.GetItemDependenciesListInfo(offset, limit, aspxCommonObj, shippingMethodId);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------Delete multiple cost Depandencies --------------------------
    //[WebMethod]
    //public void DeleteCostDependencies(string shippingProductCostIds, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxShipMethodMgntController.DeleteCostDependencies(shippingProductCostIds, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------Delete multiple weight Depandencies --------------------------
    //[WebMethod]
    //public void DeleteWeightDependencies(string shippingProductWeightIds, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxShipMethodMgntController.DeleteWeightDependencies(shippingProductWeightIds, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------Delete multiple item Depandencies --------------------------
    //[WebMethod]
    //public void DeleteItemDependencies(string shippingItemIds, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxShipMethodMgntController.DeleteItemDependencies(shippingItemIds, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////------------------save  cost dependencies----------------
    //[WebMethod]
    //public void SaveCostDependencies(int shippingProductCostID, int shippingMethodID, string costDependenciesOptions, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxShipMethodMgntController.AddCostDependencies(shippingProductCostID, shippingMethodID, costDependenciesOptions, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------- save weight dependencies-------------------------------
    //[WebMethod]
    //public void SaveWeightDependencies(int shippingProductWeightID, int shippingMethodID, string weightDependenciesOptions, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxShipMethodMgntController.AddWeightDependencies(shippingProductWeightID, shippingMethodID, weightDependenciesOptions, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------- save item dependencies-------------------------------
    //[WebMethod]
    //public void SaveItemDependencies(int shippingItemID, int shippingMethodID, string itemDependenciesOptions, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxShipMethodMgntController.AddItemDependencies(shippingItemID, shippingMethodID, itemDependenciesOptions, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Shipping Service Providers management
    //[WebMethod]
    //public List<ShippingProviderNameListInfo> GetShippingProviderNameList(int offset, int limit, AspxCommonInfo aspxCommonObj, string shippingProviderName, System.Nullable<bool> isActive)
    //{
    //    try
    //    {
    //        List<ShippingProviderNameListInfo> lstShipProvider = AspxShipProviderMgntController.GetShippingProviderNameList(offset, limit, aspxCommonObj, shippingProviderName, isActive);
    //        return lstShipProvider;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public string LoadProviderSetting(int providerId, AspxCommonInfo aspxCommonObj)
    //{
    //    string retStr = AspxShipProviderMgntController.LoadProviderSetting(providerId, aspxCommonObj);
    //    return retStr;
    //}

    //[WebMethod]
    //public void ShippingProviderAddUpdate(List<ShippingMethod> methods,
    //    ShippingProvider provider, bool isAddedZip, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxShipProviderMgntController.ShippingProviderAddUpdate(methods, provider, isAddedZip, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteShippingProviderByID(int shippingProviderID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxShipProviderMgntController.DeleteShippingProviderByID(shippingProviderID, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteShippingProviderMultipleSelected(string shippingProviderIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxShipProviderMgntController.DeleteShippingProviderMultipleSelected(shippingProviderIDs, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //#endregion

    //#region Coupon Management

    //#region Coupon Type Manage
    //[WebMethod]
    //public List<CouponTypeInfo> GetCouponTypeDetails(int offset, int limit, string couponTypeName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CouponTypeInfo> lstCoupType = AspxCouponManageController.GetCouponTypeDetails(offset, limit, couponTypeName, aspxCommonObj);
    //        return lstCoupType;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void AddUpdateCouponType(CouponTypeInfo couponTypeObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCouponManageController.AddUpdateCouponType(couponTypeObj, aspxCommonObj);
    //    }

    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteCouponType(string IDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCouponManageController.DeleteCouponType(IDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Coupon Manage
    //[WebMethod]
    //public List<CouponInfo> GetCouponDetails(int offset, int limit, GetCouponDetailsInfo couponDetailObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CouponInfo> lstCoupon = AspxCouponManageController.BindAllCouponDetails(offset, limit, couponDetailObj, aspxCommonObj);
    //        return lstCoupon;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckUniqueCouponCode(string couponCode, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        bool isExists = AspxCouponManageProvider.CheckUniqueCouponCode(couponCode, aspxCommonObj);
    //        return isExists;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public string AddUpdateCouponDetails(CouponSaveObj couponSaveObj, CouponEmailInfo couponEmailObj, AspxCommonInfo aspxCommonObj)
    //{
    //    string checkMessage = string.Empty;
    //    try
    //    {
    //        try
    //        {
    //            AspxCouponManageController.AddUpdateCoupons(couponSaveObj, aspxCommonObj);
    //            checkMessage += "dataSave" + ",";
    //        }
    //        catch (Exception)
    //        {
    //            checkMessage += "dataSaveFail" + ",";
    //        }

    //        if (checkMessage == "dataSave,")
    //        {
    //            //if (portalUserEmailID != "")
    //            if (couponEmailObj.ReceiverEmail != "")
    //            {
    //                try
    //                {
    //                    // cmSQLProvider.SendCouponCodeEmail(senderEmail, portalUserEmailID, subject, messageBody);
    //                    AspxCouponManageController.SendCouponCodeEmail(couponEmailObj);
    //                    checkMessage += "emailSend";
    //                }
    //                catch (Exception)
    //                {
    //                    checkMessage += "emailSendFail";
    //                }
    //            }
    //            else
    //            {
    //                checkMessage += "emailIDBlank";
    //            }
    //        }
    //        else
    //        {
    //            checkMessage += "emailSendFail";
    //        }

    //        return checkMessage;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public void AddUpdatePromoCodeDetails(PromoCodeSaveObj promoSaveObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCouponManageController.AddUpdatPromoCode(promoSaveObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<CouponStatusInfo> GetCouponStatus(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CouponStatusInfo> lstCoupStat = AspxCouponManageController.BindCouponStatus(aspxCommonObj);
    //        return lstCoupStat;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CouponSettingKeyValueInfo> GetSettinKeyValueByCouponID(int couponID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CouponSettingKeyValueInfo> lstCoupKeyVal = AspxCouponManageController.GetCouponSettingKeyValueInfo(couponID, aspxCommonObj);
    //        return lstCoupKeyVal;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CouponPortalUserListInfo> GetPortalUsersByCouponID(int offset, int limit, int couponID, string customerName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CouponPortalUserListInfo> lstCoupUser = AspxCouponManageController.GetPortalUsersList(offset, limit, couponID, customerName, aspxCommonObj);
    //        return lstCoupUser;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////----------------delete coupons(admin)-----------
    //[WebMethod]
    //public void DeleteCoupons(string couponIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCouponManageController.DeleteCoupons(couponIDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //////-------------------Verify Coupon Code-----------------------------
    ////[WebMethod]
    ////public CouponVerificationInfo VerifyCouponCode(decimal totalCost, string couponCode, string itemIds, string cartItemIds, AspxCommonInfo aspxCommonObj, int appliedCount)
    ////{
    ////    try
    ////    {
    ////        CouponVerificationInfo objCoupVeri = AspxCouponManageController.VerifyUserCoupon(totalCost, couponCode, itemIds, cartItemIds, aspxCommonObj, appliedCount);
    ////        return objCoupVeri;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}

    ////--------update wherever necessary after coupon verification is successful----------
    //[WebMethod]
    //public void UpdateCouponUserRecord(string couponCode, int storeID, int portalID, string userName, int orderID)
    //{
    //    try
    //    {
    //        AspxCouponManageController.UpdateCouponUserRecord(couponCode, storeID, portalID, userName, orderID);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Coupons Per Sales Management
    //[WebMethod]
    //public List<CouponPerSales> GetCouponDetailsPerSales(int offset, int? limit, string couponCode, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CouponPerSales> lstCoupPerSale = AspxCouponManageController.GetCouponDetailsPerSales(offset, limit, couponCode, aspxCommonObj);
    //        return lstCoupPerSale;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CouponPerSalesViewDetailInfo> GetCouponPerSalesDetailView(int offset, int? limit, CouponPerSalesGetInfo couponPerSaesObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CouponPerSalesViewDetailInfo> lstCoupPSVDetail = AspxCouponManageController.GetCouponPerSalesDetailView(offset, limit, couponPerSaesObj, aspxCommonObj);
    //        return lstCoupPSVDetail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Coupon User Management
    //[WebMethod]
    //public List<CouponUserInfo> GetCouponUserDetails(int offset, int? limit, GetCouponUserDetailInfo couponUserObj, AspxCommonInfo aspxCommonObj, string userName)
    //{
    //    try
    //    {

    //        List<CouponUserInfo> lstCoupUser = AspxCouponManageController.GetCouponUserDetails(offset, limit, couponUserObj, aspxCommonObj, userName);
    //        return lstCoupUser;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CouponUserListInfo> GetCouponUserList(int offset, int limit, CouponCommonInfo bindCouponUserObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CouponUserListInfo> lstCoupUser = AspxCouponManageController.GetCouponUserList(offset, limit, bindCouponUserObj, aspxCommonObj);
    //        return lstCoupUser;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public string GetPromoItemCheckIDs(int CouponID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        string lstItems = AspxCouponManageController.GetPromoItemCheckIDs(CouponID, aspxCommonObj);
    //        return lstItems;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<PromoItemsInfo> GetPromoItemList(int offset, int limit, int couponTypeId, int couponId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<PromoItemsInfo> lstItems = AspxCouponManageController.GetAllPromoItems(offset, limit, couponTypeId, couponId,
    //                                                                                    aspxCommonObj);
    //        return lstItems;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
   
    //[WebMethod]
    //public List<ItemsForPromoInfo> ItemsForPromoCode(int offset, int limit, AspxCommonInfo aspxCommonObj, string itemName, int? couponId)
    //{
    //    try
    //    {
    //        List<ItemsForPromoInfo> lst = AspxCouponManageController.ItemsForPromoCode(offset, limit, aspxCommonObj, itemName, couponId);
    //        return lst;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public void DeleteCouponUser(string couponUserID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCouponManageController.DeleteCouponUser(couponUserID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void UpdateCouponUser(int couponUserID, int couponStatusID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCouponManageController.UpdateCouponUser(couponUserID, couponStatusID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Coupon Status Management
    //[WebMethod]
    //public List<CouponStatusInfo> GetAllCouponStatusList(int offset, int limit, AspxCommonInfo aspxCommonObj, string couponStatusName, System.Nullable<bool> isActive)
    //{
    //    try
    //    {
    //        List<CouponStatusInfo> lstCouponStat = AspxCouponStatusMgmtController.GetAllCouponStatusList(offset, limit, aspxCommonObj, couponStatusName, isActive);
    //        return lstCouponStat;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckCouponStatusUniqueness(AspxCommonInfo aspxCommonObj, int couponStatusId, string couponStatusName)
    //{
    //    try
    //    {
    //        bool isUnique = AspxCouponStatusMgmtController.CheckCouponStatusUniqueness(aspxCommonObj, couponStatusId, couponStatusName);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CouponStatusInfo> AddUpdateCouponStatus(AspxCommonInfo aspxCommonObj, CouponStatusInfo SaveCouponStatusObj)
    //{
    //    try
    //    {
    //        List<CouponStatusInfo> lstCouponStat = AspxCouponStatusMgmtController.AddUpdateCouponStatus(aspxCommonObj, SaveCouponStatusObj);
    //        return lstCouponStat;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //#endregion

    //#region Coupon Setting Manage/Admin
    //[WebMethod]
    //public void DeleteCouponSettingsKey(string settingID, int storeID, int portalID, string userName)
    //{
    //    try
    //    {
    //        AspxCouponManageController.DeleteCouponSettingsKey(settingID, storeID, portalID, userName);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CouponSettingKeyInfo> CouponSettingManageKey()
    //{
    //    try
    //    {
    //        List<CouponSettingKeyInfo> lstCoupSetting = AspxCouponManageController.CouponSettingManageKey();
    //        return lstCoupSetting;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void AddUpdateCouponSettingKey(int ID, string settingKey, int validationTypeID, string isActive, int storeID, int portalID, string cultureName, string userName)
    //{
    //    try
    //    {
    //        AspxCouponManageController.AddUpdateCouponSettingKey(ID, settingKey, validationTypeID, isActive, storeID, portalID, cultureName, userName);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Front Coupon Show
    //[WebMethod]
    //public List<CouponDetailFrontInfo> GetCouponDetailListFront(int count, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CouponDetailFrontInfo> lstCoupDetail = AspxCouponManageController.GetCouponDetailListFront(count, aspxCommonObj);
    //        return lstCoupDetail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#endregion

    //#region Admin DashBoard
    //[WebMethod]
    //public List<CategoryQuantityStatics> GetTopCategoryByItemSell(int top, int day, AspxCommonInfo aspxCommonObj)
    //{

    //    List<CategoryQuantityStatics> lstCat = AspxAdminDashProvider.GetTopCategoryByItemSell(top, day, aspxCommonObj);
    //    return lstCat;
    //}
    //[WebMethod]
    //public List<CategoryRevenueStatics> GetTopCategoryByHighestRevenue(int top, int day, AspxCommonInfo aspxCommonObj)
    //{
    //    List<CategoryRevenueStatics> lstCat = AspxAdminDashProvider.GetTopCategoryByHighestRevenue(top, day, aspxCommonObj);
    //    return lstCat;
    //}
    //[WebMethod]
    //public List<VisitorOrderStatics> GetVisitorsOrder(int day, AspxCommonInfo aspxCommonObj)
    //{
    //    List<VisitorOrderStatics> lstVisitor = AspxAdminDashProvider.GetVisitorsOrder(day, aspxCommonObj);
    //    return lstVisitor;
    //}
    //[WebMethod]
    //public List<VisitorNewAccountStatics> GetVisitorsNewAccount(int day, AspxCommonInfo aspxCommonObj)
    //{
    //    List<VisitorNewAccountStatics> lstVisitor = AspxAdminDashProvider.GetVisitorsNewAccount(day, aspxCommonObj);
    //    return lstVisitor;
    //}
    //[WebMethod]
    //public List<VisitorNewOrderStatics> GetVisitorsNewOrder(int day, AspxCommonInfo aspxCommonObj)
    //{
    //    List<VisitorNewOrderStatics> lstVisitor = AspxAdminDashProvider.GetVisitorsNewOrder(day, aspxCommonObj);
    //    return lstVisitor;
    //}
    //[WebMethod]
    //public List<RefundStatics> GetTotalRefund(int day, AspxCommonInfo aspxCommonObj)
    //{
    //    List<RefundStatics> lstRefund = AspxAdminDashProvider.GetTotalRefund(day, aspxCommonObj);
    //    return lstRefund;
    //}

    //[WebMethod]
    //public List<RefundReasonStatics> GetTopRefundReason(int day, AspxCommonInfo aspxCommonObj)
    //{
    //    List<RefundReasonStatics> lstRefund = AspxAdminDashProvider.GetTopRefundReason(day, aspxCommonObj);
    //    return lstRefund;
    //}
    //[WebMethod]
    //public List<SearchTermInfo> GetSearchStatistics(int count, string commandName, AspxCommonInfo aspxCommonObj)
    //{
    //    List<SearchTermInfo> lstSearchTerm = AspxCommonController.GetSearchStatistics(count, commandName, aspxCommonObj);
    //    return lstSearchTerm;
    //}

    //[WebMethod]
    //public List<LatestOrderStaticsInfo> GetLatestOrderItems(int count, AspxCommonInfo aspxCommonObj)
    //{
    //    List<LatestOrderStaticsInfo> lstLOSI = AspxAdminDashController.GetLatestOrderItems(count, aspxCommonObj);
    //    return lstLOSI;
    //}

    //[WebMethod]
    //public List<MostViewItemInfoAdminDash> GetMostViwedItemAdmindash(int count, AspxCommonInfo aspxCommonObj)
    //{
    //    List<MostViewItemInfoAdminDash> lstMVI = AspxAdminDashController.GetMostViwedItemAdmindash(count, aspxCommonObj);
    //    return lstMVI;
    //}

    //[WebMethod]
    //public List<StaticOrderStatusAdminDashInfo> GetStaticOrderStatusAdminDash(int day, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<StaticOrderStatusAdminDashInfo> lstSOS = AspxAdminDashController.GetStaticOrderStatusAdminDash(day, aspxCommonObj);
    //        return lstSOS;

    //    }

    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<TopCustomerOrdererInfo> GetTopCustomerOrderAdmindash(int count, AspxCommonInfo aspxCommonObj)
    //{
    //    List<TopCustomerOrdererInfo> lstTCO = AspxAdminDashController.GetTopCustomerOrderAdmindash(count, aspxCommonObj);
    //    return lstTCO;
    //}

    //[WebMethod]
    //public List<TotalOrderAmountInfo> GetTotalOrderAmountAdmindash(AspxCommonInfo aspxCommonObj)
    //{
    //    List<TotalOrderAmountInfo> lstTOAmount = AspxAdminDashController.GetTotalOrderAmountAdmindash(aspxCommonObj);
    //    return lstTOAmount;
    //}

    //[WebMethod]
    //public List<InventoryDetailAdminDashInfo> GetInventoryDetails(int count, AspxCommonInfo aspxCommonObj)
    //{
    //    List<InventoryDetailAdminDashInfo> lstInvDetail = AspxAdminDashController.GetInventoryDetails(count, aspxCommonObj);
    //    return lstInvDetail;
    //}

    //[WebMethod]
    //public StoreQuickStaticsInfo GetStoreQuickStatics(AspxCommonInfo aspxCommonObj)
    //{
    //    StoreQuickStaticsInfo lstInvDetail = AspxAdminDashController.GetStoreQuickStatics(aspxCommonObj);
    //    return lstInvDetail;
    //}


    //#endregion

  //  #region For User DashBoard

    //#region Shared Wishlists
    ////--------------------bind ShareWishList Email  in Grid--------------------------
    //[WebMethod]
    //public List<ShareWishListItemInfo> GetAllShareWishListItemMail(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShareWishListItemInfo> lstShareWishItem = AspxUserDashController.GetAllShareWishListItemMail(offset, limit, aspxCommonObj);
    //        return lstShareWishItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ShareWishListItemInfo> GetShareWishListItemByID(int sharedWishID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShareWishListItemInfo> lstShareWishItem = AspxUserDashController.GetShareWishListItemByID(sharedWishID, aspxCommonObj);
    //        return lstShareWishItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-----------------Delete ShareWishList --------------------------------
    //[WebMethod]
    //public void DeleteShareWishListItem(string shareWishListID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.DeleteShareWishListItem(shareWishListID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    ////-------------------------Update Customer Account Information----------------------------------------  
    //[WebMethod]
    //public int UpdateCustomer(AspxCommonInfo aspxCommonObj, string firstName, string lastName, string email)
    //{
    //    try
    //    {
    //        int errorCode = AspxUserDashController.UpdateCustomer(aspxCommonObj, firstName, lastName, email);
    //        return errorCode;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //[WebMethod]
    //public bool ChangePassword(AspxCommonInfo aspxCommonObj, string newPassword, string retypePassword)
    //{
    //    MembershipController m = new MembershipController();
    //    try
    //    {
    //        if (newPassword != "" && retypePassword != "" && newPassword == retypePassword && aspxCommonObj.UserName != "")
    //        {
    //            UserInfo sageUser = m.GetUserDetails(aspxCommonObj.PortalID, aspxCommonObj.UserName);
    //            // Guid userID = (Guid)member.ProviderUserKey;
    //            string password, passwordSalt;
    //            PasswordHelper.EnforcePasswordSecurity(m.PasswordFormat, newPassword, out password, out passwordSalt);
    //            UserInfo user = new UserInfo(sageUser.UserID, password, passwordSalt, m.PasswordFormat);
    //            m.ChangePassword(user);
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    ////---------------User Item Reviews and Ratings-----------------------
    //[WebMethod]
    //public List<UserRatingInformationInfo> GetUserReviewsAndRatings(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<UserRatingInformationInfo> bind = AspxUserDashController.GetUserReviewsAndRatings(offset, limit, aspxCommonObj);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------update rating/ review Items From User DashBoard-----------------------
    //[WebMethod]
    //public void UpdateItemRatingByUser(ItemReviewBasicInfo updateItemRatingObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.UpdateItemRatingByUser(updateItemRatingObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-----------User DashBoard/Recent History-------------------
    //[WebMethod]
    //public List<UserRecentHistoryInfo> GetUserRecentlyViewedItems(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<UserRecentHistoryInfo> lstUserHistory = AspxUserDashController.GetUserRecentlyViewedItems(offset, limit, aspxCommonObj);
    //        return lstUserHistory;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-----------User DashBoard/Recent History-------------------
    //[WebMethod]
    //public List<UserRecentCompareInfo> GetUserRecentlyComparedItems(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<UserRecentCompareInfo> lstUserRCompare = AspxUserDashController.GetUserRecentlyComparedItems(offset, limit, aspxCommonObj);
    //        return lstUserRCompare;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void AddUpdateUserAddress(AddressInfo addressObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.AddUpdateUserAddress(addressObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<AddressInfo> GetAddressBookDetails(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AddressInfo> lstAddress = AspxUserDashController.GetUserAddressDetails(aspxCommonObj);
    //        return lstAddress;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    //[WebMethod]
    //public void DeleteAddressBook(int addressID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.DeleteAddressBookDetails(addressID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<UserProductReviewInfo> GetUserProductReviews(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<UserProductReviewInfo> lstUPReview = AspxUserDashController.GetUserProductReviews(aspxCommonObj);
    //        return lstUPReview;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void UpdateUserProductReview(ItemReviewBasicInfo productReviewObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.UpdateUserProductReview(productReviewObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteUserProductReview(int itemID, int itemReviewID, AspxCommonInfo aspxCommonObj)
    //{
    //    AspxUserDashController.DeleteUserProductReview(itemID, itemReviewID, aspxCommonObj);
    //}

    //---------------userDashBord/My Order List in grid----------------------------
    //[WebMethod]
    //public List<MyOrderListInfo> GetMyOrderList(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<MyOrderListInfo> lstMyOrder = AspxUserDashController.GetMyOrderList(offset, limit, aspxCommonObj);
    //        return lstMyOrder;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ReOrderItemsInfo> GetMyOrdersforReOrder(int orderID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ReOrderItemsInfo> info = AspxUserDashController.GetMyOrdersforReOrder(orderID, aspxCommonObj);
    //        return info;
    //    }

    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#region Return and Policy

    //[WebMethod]
    //public decimal CheckItemQuantity(int itemID, AspxCommonInfo aspxCommonObj, string itemCostVariantIDs)
    //{
    //    try
    //    {
    //        decimal retValue = AspxUserDashController.CheckItemQuantity(itemID, aspxCommonObj, itemCostVariantIDs);
    //        return retValue;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ReturnReasonListInfo> BindReturnReasonList(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ReturnReasonListInfo> lstRRList = AspxUserDashController.BindReturnReasonList(aspxCommonObj);
    //        return lstRRList;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ProductStatusListInfo> BindProductStatusList(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ProductStatusListInfo> lstPdtStatus = AspxUserDashController.BindProductStatusList(aspxCommonObj);
    //        return lstPdtStatus;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ReturnStatusInfo> GetReturnStatusList(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ReturnStatusInfo> lstRetStatus = AspxUserDashController.GetReturnStatusList(aspxCommonObj);
    //        return lstRetStatus;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ReturnActionInfo> GetReturnActionList(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ReturnActionInfo> lstRetAction = AspxUserDashController.GetReturnActionList(aspxCommonObj);
    //        return lstRetAction;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<MyOrderListForReturnInfo> GetMyOrderListForReturn(int orderID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<MyOrderListForReturnInfo> lstMyOrder = AspxUserDashController.GetMyOrderListForReturn(orderID, aspxCommonObj);
    //        return lstMyOrder;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void ReturnSaveUpdate(ReturnSaveUpdateInfo ReturnSaveUpdateObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.ReturnSaveUpdate(ReturnSaveUpdateObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void ReturnUpdate(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.ReturnUpdate(returnDetailObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}

    //[WebMethod]
    //public void ReturnSaveComments(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.ReturnSaveComments(returnDetailObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}

    //[WebMethod]
    //public List<ReturnCommentsInfo> GetMyReturnsComment(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ReturnCommentsInfo> info = AspxUserDashController.GetMyReturnsComment(returnDetailObj, aspxCommonObj);
    //        return info;
    //    }

    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ReturnsShippingInfo> GetMyReturnsShippingMethod(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ReturnsShippingInfo> info;
    //        info = AspxUserDashController.GetMyReturnsShippingMethod(returnDetailObj, aspxCommonObj);
    //        return info;
    //    }

    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<RetunReportInfo> GetReturnReport(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, ReturnReportBasicInfo returnReportObj)
    //{
    //    try
    //    {
    //        List<RetunReportInfo> lstRtnReport = AspxUserDashController.GetReturnReport(offset, limit, aspxCommonObj, returnReportObj);
    //        return lstRtnReport;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void ReturnShippingAddressSaveUpdate(AddressBasicInfo addressObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.ReturnShippingAddressSaveUpdate(addressObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<MyReturnListInfo> GetMyReturnsList(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<MyReturnListInfo> lstMyReturn = AspxUserDashController.GetMyReturnsList(offset, limit, aspxCommonObj);
    //        return lstMyReturn;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<ReturnItemsInfo> GetMyReturnsDetails(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ReturnItemsInfo> info = AspxUserDashController.GetMyReturnsDetails(returnDetailObj, aspxCommonObj);
    //        return info;
    //    }

    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ReturnDetailsInfo> GetReturnDetails(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, RetunDetailsBasicInfo returnDetailObj)
    //{
    //    try
    //    {
    //        List<ReturnDetailsInfo> info = AspxUserDashController.GetReturnDetails(offset, limit, aspxCommonObj, returnDetailObj);
    //        return info;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void ReturnSendEmail(AspxCommonInfo aspxCommonObj, SendEmailInfo sendEmailObj)
    //{
    //    try
    //    {
    //        EmailTemplate.SendEmailForReturns(aspxCommonObj, sendEmailObj);

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void ReturnSaveUpdateSettings(AspxCommonInfo aspxCommonObj, int expiresInDays)
    //{
    //    try
    //    {
    //        AspxReturnRequestMgntController.ReturnSaveUpdateSettings(aspxCommonObj, expiresInDays);

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    ////[WebMethod]
    ////public List<ReturnsSettingsInfo> ReturnGetSettings(AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        List<ReturnsSettingsInfo> info = AspxReturnRequestMgntController.ReturnGetSettings(aspxCommonObj);
    ////        return info;
    ////    }

    ////    catch (Exception e)
    ////    {
    ////        throw e;
    ////    }
    ////}

    //#endregion

    //[WebMethod]
    //public List<AddressInfo> GetAddressBookDetailsByAddressID(int addressID, int storeID, int portalID, int customerID, string userName, string cultureName)
    //{
    //    List<AddressInfo> lstAddress = AspxUserDashController.GetAddressBookDetailsByAddressID(addressID, storeID, portalID, customerID, userName, cultureName);
    //    return lstAddress;
    //}
    ////-----------------------UserDashBoard/ My Orders-------------------
    //[WebMethod]
    //public List<OrderItemsInfo> GetMyOrders(int orderID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<OrderItemsInfo> info = AspxUserDashController.GetMyOrders(orderID, aspxCommonObj);
    //        return info;
    //    }

    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}




    ////-------------------------UserDashBoard/User Downloadable Items------------------------------
    //[WebMethod]
    //public List<DownloadableItemsByCustomerInfo> GetCustomerDownloadableItems(int offset, int limit, string sku, string name, AspxCommonInfo aspxCommonObj, bool isActive)
    //{
    //    try
    //    {
    //        List<DownloadableItemsByCustomerInfo> ml;
    //        ml = AspxUserDashController.GetCustomerDownloadableItems(offset, limit, sku, name, aspxCommonObj, isActive);
    //        return ml;
    //    }

    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteCustomerDownloadableItem(string orderItemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.DeleteCustomerDownloadableItem(orderItemID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void UpdateDownloadCount(int itemID, int orderItemID, string downloadIP, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.UpdateDownloadCount(itemID, orderItemID, downloadIP, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckRemainingDownload(int itemId, int orderItemId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        bool isRemain = AspxUserDashController.CheckRemainingDownload(itemId, orderItemId, aspxCommonObj);
    //        return isRemain;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
 //   #endregion

    #region CartManage
    // NewsLetter Subscriber
    //[WebMethod]
    //public string GetUserBillingEmail(int addressID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        string userEmail = AspxCommonController.GetUserBillingEmail(addressID, aspxCommonObj);
    //        return userEmail;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void SaveEmailSubscriber(string email, int userModuleID, int portalID, string userName, string clientIP)
    //{
    //    try
    //    {
    //        NL_Provider cont = new NL_Provider();
    //        cont.SaveEmailSubscriber(email, userModuleID, portalID, userName, clientIP);

    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<NL_Info> CheckPreviousEmailSubscription(string email)
    //{
    //    try
    //    {
    //        NL_Provider cont = new NL_Provider();
    //        return cont.CheckPreviousEmailSubscription(email);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //------------------------------Check Cart--------------------------
    [WebMethod]
    public bool CheckCart(int itemID, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            bool isExist = AspxCartController.CheckCart(itemID, storeID, portalID, userName, cultureName);
            return isExist;

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //------------------------------Add to Cart--------------------------
    [WebMethod]
    public bool AddtoCart(int itemID, int storeID, int portalID, string userName, string cultureName)
    {

        try
        {
            bool isExist = AspxCartController.AddtoCart(itemID, storeID, portalID, userName, cultureName);
            return isExist;

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    ////------------------------------Cart Details--------------------------
    //[WebMethod]
    //public List<CartInfo> GetCartDetails(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CartInfo> lstCart = AspxCartController.GetCartDetails(aspxCommonObj);
    //        return lstCart;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<CartInfo> GetCartCheckOutDetails(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CartInfo> lstCart = AspxCartController.GetCartCheckOutDetails(aspxCommonObj);
    //        return lstCart;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////Cart Item Qty Discount Calculations
    //[WebMethod]
    //public decimal GetDiscountQuantityAmount(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        decimal qtyDiscount = AspxCartController.GetDiscountQuantityAmount(aspxCommonObj);
    //        return qtyDiscount;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////------------------------------Delete Cart Items--------------------------
    //[WebMethod]
    //public void DeleteCartItem(int cartID, int cartItemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCartController.DeleteCartItem(cartID, cartItemID, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////------------------------Clear My Carts----------------------------
    //[WebMethod]
    //public void ClearAllCartItems(int cartID, AspxCommonInfo aspxCommonObj)
    //{
    //    AspxCartController.ClearAllCartItems(cartID, aspxCommonObj);
    //}

    //[WebMethod]
    //public decimal CheckItemQuantityInCart(int itemID, AspxCommonInfo aspxCommonObj, string itemCostVariantIDs)
    //{
    //    try
    //    {
    //        decimal retValue = AspxCartController.CheckItemQuantityInCart(itemID, aspxCommonObj, itemCostVariantIDs);
    //        return retValue;

    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    [WebMethod]
    public decimal GetCostVariantQuantity(int itemID, int storeID, int portalID, string itemCostVariantIDs)
    {
        try
        {
            decimal cvQty = AspxCartController.GetCostVariantQuantity(itemID, storeID, portalID, itemCostVariantIDs);
            return cvQty;

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public bool CheckOutOfStock(int itemID, int storeID, int portalID, string itemCostVariantIDs)
    {
        try
        {
            bool isOutStock = AspxCartController.CheckOutOfStock(itemID, storeID, portalID, itemCostVariantIDs);
            return isOutStock;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //[WebMethod]
    //public CartExistInfo CheckCustomerCartExist(AspxCommonInfo aspxCommonObj)
    //{

    //    CartExistInfo objCartExist = AspxCartController.CheckCustomerCartExist(aspxCommonObj);
    //    return objCartExist;

    //}

    //------------------------------Get ShippingMethodByTotalItemsWeight--------------------------
    [WebMethod]
    public List<ShippingMethodInfo> GetShippingMethodByWeight(int storeID, int portalID, int customerID, string userName, string cultureName, string sessionCode)
    {
        try
        {
            List<ShippingMethodInfo> lstShipMethod = AspxCartController.GetShippingMethodByWeight(storeID, portalID, customerID, userName, cultureName, sessionCode);
            return lstShipMethod;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<ShippingCostInfo> GetShippingCostByItem(int storeID, int portalID, int customerID, string sessionCode, string userName, string cultureName)
    {
        try
        {
            List<ShippingCostInfo> lstShipCost = AspxCartController.GetShippingCostByItem(storeID, portalID, customerID, sessionCode, userName, cultureName);
            return lstShipCost;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public void UpdateShoppingCart(UpdateCartInfo updateCartInfo, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxCartController.UpdateShoppingCart(updateCartInfo, aspxCommonObj);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public bool UpdateCartAnonymoususertoRegistered(int storeID, int portalID, int customerID, string sessionCode)
    {
        try
        {
            bool isUpdate = AspxCartController.UpdateCartAnonymoususertoRegistered(storeID, portalID, customerID, sessionCode);
            return isUpdate;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    #endregion

    //#region Quantity Discount Management
    //[WebMethod]
    //public List<ItemQuantityDiscountInfo> GetItemQuantityDiscountsByItemID(int itemId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemQuantityDiscountInfo> lstIQtyDiscount = AspxQtyDiscountMgntController.GetItemQuantityDiscountsByItemID(itemId, aspxCommonObj);
    //        return lstIQtyDiscount;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////------------------------save quantity discount------------------
    //[WebMethod]
    //public void SaveItemDiscountQuantity(string discountQuantity, int itemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxQtyDiscountMgntController.SaveItemDiscountQuantity(discountQuantity, itemID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////------------------------delete quantity discount------------------
    //[WebMethod]
    //public void DeleteItemQuantityDiscount(int quantityDiscountID, int itemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxQtyDiscountMgntController.DeleteItemQuantityDiscount(quantityDiscountID, itemID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //////------------------------quantity discount shown in Item deatils ------------------
    ////[WebMethod]
    ////public List<ItemQuantityDiscountInfo> GetItemQuantityDiscountByUserName(AspxCommonInfo aspxCommonObj, string itemSKU)
    ////{
    ////    try
    ////    {
    ////        List<ItemQuantityDiscountInfo> lstIQtyDiscount = AspxQtyDiscountMgntController.GetItemQuantityDiscountByUserName(aspxCommonObj, itemSKU);
    ////        return lstIQtyDiscount;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}
    //#endregion

    //#region Search Term Management
    //[WebMethod]
    //public void AddUpdateSearchTerm(bool? hasData, string searchTerm, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxSearchTermMgntController.AddUpdateSearchTerm(hasData, searchTerm, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<SearchTermInfo> ManageSearchTerms(int offset, int? limit, AspxCommonInfo aspxCommonObj, string searchTerm)
    //{
    //    try
    //    {
    //        List<SearchTermInfo> lstSearchTerm = AspxSearchTermMgntController.ManageSearchTerm(offset, limit, aspxCommonObj, searchTerm);
    //        return lstSearchTerm;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteSearchTerm(string searchTermID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxSearchTermMgntController.DeleteSearchTerm(searchTermID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Tax management
    ////--------------item tax classes------------------
    //[WebMethod]
    //public List<TaxItemClassInfo> GetTaxItemClassDetails(int offset, int limit, string itemClassName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<TaxItemClassInfo> lstTaxItem = AspxTaxMgntController.GetTaxItemClassDetails(offset, limit, itemClassName, aspxCommonObj);
    //        return lstTaxItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-------------------save item tax class--------------------
    //[WebMethod]
    //public void SaveAndUpdateTaxItemClass(int taxItemClassID, string taxItemClassName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTaxMgntController.SaveAndUpdateTaxItemClass(taxItemClassID, taxItemClassName, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckTaxClassUniqueness(AspxCommonInfo aspxCommonObj, string taxItemClassName)
    //{
    //    try
    //    {

    //        bool isUnique = AspxTaxMgntController.CheckTaxClassUniqueness(aspxCommonObj, taxItemClassName);
    //        return isUnique;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////-----------------Delete tax item classes --------------------------------
    //[WebMethod]
    //public void DeleteTaxItemClass(string taxItemClassIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTaxMgntController.DeleteTaxItemClass(taxItemClassIDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------customer tax classes------------------
    //[WebMethod]
    //public List<TaxCustomerClassInfo> GetTaxCustomerClassDetails(int offset, int limit, string className, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<TaxCustomerClassInfo> lstTaxCtClass = AspxTaxMgntController.GetTaxCustomerClassDetails(offset, limit, className, aspxCommonObj);
    //        return lstTaxCtClass;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-------------------save customer tax class--------------------
    //[WebMethod]
    //public void SaveAndUpdateTaxCustmerClass(int taxCustomerClassID, string taxCustomerClassName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTaxMgntController.SaveAndUpdateTaxCustmerClass(taxCustomerClassID, taxCustomerClassName, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-----------------Delete tax customer classes --------------------------------
    //[WebMethod]
    //public void DeleteTaxCustomerClass(string taxCustomerClassIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTaxMgntController.DeleteTaxCustomerClass(taxCustomerClassIDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------tax rates------------------
    //[WebMethod]
    //public List<TaxRateInfo> GetTaxRateDetails(int offset, System.Nullable<int> limit, TaxRateDataTnfo taxRateDataObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<TaxRateInfo> lstTaxRate = AspxTaxMgntController.GetTaxRateDetails(offset, limit, taxRateDataObj, aspxCommonObj);
    //        return lstTaxRate;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////----------------- save and update tax rates--------------------------
    //[WebMethod]
    //public void SaveAndUpdateTaxRates(TaxRateDataTnfo taxRateDataObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTaxMgntController.SaveAndUpdateTaxRates(taxRateDataObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-------------dalete Tax rates-----------------------
    //[WebMethod]
    //public void DeleteTaxRates(string taxRateIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTaxMgntController.DeleteTaxRates(taxRateIDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--------------------------get customer class----------------
    //[WebMethod]
    //public List<TaxManageRulesInfo> GetTaxRules(int offset, int limit, TaxRuleDataInfo taxRuleDataObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<TaxManageRulesInfo> lstTaxManage = AspxTaxMgntController.GetTaxRules(offset, limit, taxRuleDataObj, aspxCommonObj);
    //        return lstTaxManage;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    ////------------------------bind tax customer class name list-------------------------------
    //[WebMethod]
    //public List<TaxCustomerClassInfo> GetCustomerTaxClass(int storeID, int portalID)
    //{
    //    try
    //    {
    //        List<TaxCustomerClassInfo> lstTaxCtClass = AspxTaxMgntController.GetCustomerTaxClass(storeID, portalID);
    //        return lstTaxCtClass;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////------------------------bind tax item class name list-------------------------------
    //[WebMethod]
    //public List<TaxItemClassInfo> GetItemTaxClass(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<TaxItemClassInfo> lstTaxItClass = AspxTaxMgntController.GetItemTaxClass(aspxCommonObj);
    //        return lstTaxItClass;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////------------------------bind tax rate list-------------------------------
    //[WebMethod]
    //public List<TaxRateInfo> GetTaxRate(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<TaxRateInfo> lstTaxRate = AspxTaxMgntController.GetTaxRate(aspxCommonObj);
    //        return lstTaxRate;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-------------------save and update tax rules--------------------------------------
    //[WebMethod]
    //public void SaveAndUpdateTaxRule(TaxRuleDataInfo taxRuleDataObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTaxMgntController.SaveAndUpdateTaxRule(taxRuleDataObj, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    ////-------------- delete Tax Rules----------------------------

    //[WebMethod]
    //public void DeleteTaxManageRules(string taxManageRuleIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxTaxMgntController.DeleteTaxManageRules(taxManageRuleIDs, aspxCommonObj);
    //    }
    //    catch (Exception exe)
    //    {
    //        throw exe;
    //    }
    //}
    //#endregion

    //#region Catalog Pricing Rule

    //[WebMethod]
    //public List<PricingRuleAttributeInfo> GetPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<PricingRuleAttributeInfo> lstPriceRuleAttr = AspxCatalogPriceRuleController.GetPricingRuleAttributes(aspxCommonObj);
    //        return lstPriceRuleAttr;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<CatalogPriceRulePaging> GetPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
    //{
    //    List<CatalogPriceRulePaging> lstCatalogPriceRule = AspxCatalogPriceRuleController.GetCatalogPricingRules(ruleName, startDate, endDate, isActive, aspxCommonObj, offset, limit);
    //    return lstCatalogPriceRule;
    //}


    //[WebMethod]
    //public CatalogPricingRuleInfo GetPricingRule(Int32 catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
    //{
    //    CatalogPricingRuleInfo catalogPricingRuleInfo;
    //    catalogPricingRuleInfo = AspxCatalogPriceRuleController.GetCatalogPricingRule(catalogPriceRuleID, aspxCommonObj);
    //    return catalogPricingRuleInfo;
    //}

    //[WebMethod]
    //public string SavePricingRule(CatalogPricingRuleInfo objCatalogPricingRuleInfo, AspxCommonInfo aspxCommonObj, object parentID)
    //{
    //    try
    //    {
    //        List<KeyValuePair<string, object>> p1 = new List<KeyValuePair<string, object>>();
    //        p1.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
    //        p1.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
    //        SQLHandler sql = new SQLHandler();
    //        int count = sql.ExecuteAsScalar<int>("usp_Aspx_CatalogPriceRuleCount", p1);
    //        int maxAllowed = 3;
    //        int catalogPriceRuleId = objCatalogPricingRuleInfo.CatalogPriceRule.CatalogPriceRuleID;
    //        if (catalogPriceRuleId > 0)
    //        {
    //            maxAllowed++;
    //        }
    //        if (count < maxAllowed)
    //        {
    //            AspxCatalogPriceRuleController.SaveCatalogPricingRule(objCatalogPricingRuleInfo, aspxCommonObj, parentID);
    //            //return "({ \"returnStatus\" : 1 , \"Message\" : \"Saving catalog pricing rule successfully.\" })";
    //            return "success";
    //        }
    //        else
    //        {
    //            //return "({ \"returnStatus\" : -1 , \"Message\" : \"No more than 3 rules are allowed in Free version of AspxCommerce!\" })";
    //            return "notify";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        if (errHandler.LogWCFException(ex))
    //        {
    //            return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
    //        }
    //        else
    //        {
    //            return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving catalog pricing rule!\" })";
    //        }
    //    }
    //}

    //[WebMethod]
    //public string DeletePricingRule(Int32 catalogPricingRuleID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCatalogPriceRuleController.CatalogPriceRuleDelete(catalogPricingRuleID, aspxCommonObj);
    //        return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting catalog pricing rule successfully.\" })";
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        if (errHandler.LogWCFException(ex))
    //        {
    //            return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
    //        }
    //        else
    //        {
    //            return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting catalog pricing rule!\" })";
    //        }
    //    }
    //}

    //[WebMethod]
    //public string DeleteMultipleCatPricingRules(string catRulesIds, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCatalogPriceRuleController.CatalogPriceMultipleRulesDelete(catRulesIds, aspxCommonObj);
    //        return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting multiple catalog pricing rules successfully.\" })";
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        if (errHandler.LogWCFException(ex))
    //        {
    //            return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
    //        }
    //        else
    //        {
    //            return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting pricing rule!\" })";
    //        }
    //    }
    //}
    //#endregion

    //#region Cart Pricing Rule
    //[WebMethod]
    //public List<ShippingMethodInfo> GetShippingMethods(System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShippingMethodInfo> lstShipMethod = AspxCartPriceRuleController.GetShippingMethods(isActive, aspxCommonObj);
    //        return lstShipMethod;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<CartPricingRuleAttributeInfo> GetCartPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CartPricingRuleAttributeInfo> lst = AspxCartPriceRuleController.GetCartPricingRuleAttributes(aspxCommonObj);
    //        return lst;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public string SaveCartPricingRule(CartPricingRuleInfo objCartPriceRule, AspxCommonInfo aspxCommonObj, object parentID)
    //{
    //    try
    //    {
    //        List<KeyValuePair<string, object>> p1 = new List<KeyValuePair<string, object>>();
    //        //P1.Add(new KeyValuePair<string,object>("@StoreID", storeID));
    //        p1.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
    //        SQLHandler sql = new SQLHandler();
    //        int count = sql.ExecuteAsScalar<int>("usp_Aspx_CartPrincingRuleCount", p1);
    //        int maxAllowed = 3;
    //        int cartPriceRuleId = objCartPriceRule.CartPriceRule.CartPriceRuleID;
    //        if (cartPriceRuleId > 0)
    //        {
    //            maxAllowed++;
    //        }
    //        if (count < maxAllowed)
    //        {

    //            AspxCartPriceRuleController.SaveCartPricingRule(objCartPriceRule, aspxCommonObj, parentID);
    //            //return "({ \"returnStatus\" : 1 , \"Message\" : \"Saving cart pricing rule successfully.\" })";
    //            return "success";
    //        }
    //        else
    //        {
    //            //return "({ \"returnStatus\" : -1 , \"Message\" : \"No more than 3 rules are allowed in Free version of AspxCommerce!\" })";
    //            return "notify";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        if (errHandler.LogWCFException(ex))
    //        {
    //            return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
    //        }
    //        else
    //        {
    //            return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving cart pricing rule!\" })";
    //        }
    //    }
    //}

    //[WebMethod]
    //public List<CartPriceRulePaging> GetCartPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
    //{
    //    List<CartPriceRulePaging> lstCartPriceRule = AspxCartPriceRuleController.GetCartPricingRules(ruleName, startDate, endDate, isActive, aspxCommonObj, offset, limit);
    //    return lstCartPriceRule;
    //}


    //[WebMethod]
    //public CartPricingRuleInfo GetCartPricingRule(Int32 cartPriceRuleID, AspxCommonInfo aspxCommonObj)
    //{
    //    CartPricingRuleInfo cartPricingRuleInfo;
    //    cartPricingRuleInfo = AspxCartPriceRuleController.GetCartPriceRules(cartPriceRuleID, aspxCommonObj);
    //    return cartPricingRuleInfo;
    //}

    //[WebMethod]
    //public string DeleteCartPricingRule(Int32 cartPricingRuleID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {

    //        AspxCartPriceRuleController.CartPriceRuleDelete(cartPricingRuleID, aspxCommonObj);
    //        return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting cart pricing rule successfully.\" })";
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        if (errHandler.LogWCFException(ex))
    //        {
    //            return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
    //        }
    //        else
    //        {
    //            return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting cart pricing rule!\" })";
    //        }
    //    }
    //}

    //[WebMethod]
    //public string DeleteMultipleCartPricingRules(string cartRulesIds, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {

    //        AspxCartPriceRuleController.CartPriceMultipleRulesDelete(cartRulesIds, aspxCommonObj);
    //        return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting multiple cart pricing rules successfully.\" })";
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorHandler errHandler = new ErrorHandler();
    //        if (errHandler.LogWCFException(ex))
    //        {
    //            return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
    //        }
    //        else
    //        {
    //            return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting cart pricing rule!\" })";
    //        }
    //    }
    //}
    //#endregion

    //#region AddToCart
    //[WebMethod]
    //public int AddItemstoCart(int itemID, decimal itemPrice, int itemQuantity, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        int retValue = AspxCommonController.AddItemstoCart(itemID, itemPrice, itemQuantity, aspxCommonObj);
    //        return retValue;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public int AddItemstoCartFromDetail(AddItemToCartInfo AddItemToCartObj, AspxCommonInfo aspxCommonObj, GiftCard giftCardDetail)
    //{
    //    try
    //    {
    //        int retValue = AspxCommonController.AddItemstoCartFromDetail(AddItemToCartObj, aspxCommonObj, giftCardDetail);
    //        return retValue;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckItemOutOfStock(int itemID, string costVariantsValueIDs,AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        bool retValue = AspxCommonController.CheckItemOutOfStock(itemID, costVariantsValueIDs, aspxCommonObj);
    //        return retValue;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region MiniCart Display
    ////----------------------Count my cart items--------------------
    //[WebMethod]
    //public int GetCartItemsCount(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        int cartItemCount = AspxCommonController.GetCartItemsCount(aspxCommonObj);
    //        return cartItemCount;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    #region Reporting Module

    ////--------------- New Account Reports--------------------------
    //[WebMethod]
    //public List<NewAccountReportInfo> GetNewAccounts(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, bool monthly, bool weekly, bool hourly)
    //{
    //    try
    //    {
    //        List<NewAccountReportInfo> lstNewAccounts = AspxCustomerMgntController.GetNewAccounts(offset, limit, aspxCommonObj, monthly, weekly, hourly);
    //        return lstNewAccounts;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#region Sales Tax Report
    //[WebMethod]
    //public List<StoreTaxesInfo> GetStoreSalesTaxes(int offset, int limit, TaxDateData taxDataObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<StoreTaxesInfo> lstStoreTax = AspxTaxMgntController.GetStoreSalesTaxes(offset, limit, taxDataObj, aspxCommonObj);
    //        return lstStoreTax;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Items Reporting
    ////----------------------GetMostViewedItems----------------------
    //[WebMethod]
    //public List<MostViewedItemsInfo> GetMostViewedItemsList(int offset, int? limit, string name, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<MostViewedItemsInfo> lstMostView = AspxItemMgntController.GetAllMostViewedItems(offset, limit, name, aspxCommonObj);
    //        return lstMostView;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //// --------------------------Get Low Stock Items----------------------------------------------------
    //[WebMethod]
    //public List<LowStockItemsInfo> GetLowStockItemsList(int offset, int? limit, ItemSmallCommonInfo lowStockObj, AspxCommonInfo aspxCommonObj, int lowStock)
    //{
    //    try
    //    {
    //        List<LowStockItemsInfo> lstLowStockItem = AspxItemMgntController.GetAllLowStockItems(offset, limit, lowStockObj, aspxCommonObj, lowStock);
    //        return lstLowStockItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////------------------------------------Get Ordered Items List-----------------------------------
    //[WebMethod]
    //public List<OrderItemsGroupByItemIDInfo> GetOrderedItemsList(int offset, System.Nullable<int> limit, string name, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<OrderItemsGroupByItemIDInfo> lstOrderItem = AspxItemMgntController.GetOrderedItemsList(offset, limit, name, aspxCommonObj);
    //        return lstOrderItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //// --------------------------Get DownLoadable Items----------------------------------------------------
    //[WebMethod]
    //public List<DownLoadableItemGetInfo> GetDownLoadableItemsList(int offset, int? limit, GetDownloadableItemInfo downloadableObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<DownLoadableItemGetInfo> lstDownItem = AspxItemMgntController.GetDownLoadableItemsList(offset, limit, downloadableObj, aspxCommonObj);
    //        return lstDownItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //// --------------------------Get DownLoadable Items----------------------------------------------------

    //[WebMethod]
    //public List<GiftCardReport> GetGiftCardReport(int offset, int? limit, GiftCardReport objGiftcard, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<GiftCardReport> giftCardReports = AspxGiftCardController.GetGiftCardReport(offset, limit, objGiftcard,
    //                                                                                        aspxCommonObj);
    //        return giftCardReports;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}




    //#endregion

    ////---------------------Shipping Reports--------------------
    //[WebMethod]
    //public List<ShippedReportInfo> GetShippedDetails(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, ShippedReportBasicInfo ShippedReportObj)
    //{
    //    try
    //    {
    //        List<ShippedReportInfo> lstShipReport = AspxShipMethodMgntController.GetShippedDetails(offset, limit, aspxCommonObj, ShippedReportObj);
    //        return lstShipReport;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }

    //}

    //// ShoppingCartManagement ---------------------get Cart details in grid-------------------------------
    //[WebMethod]
    //public List<ShoppingCartInfo> GetShoppingCartItemsDetails(int offset, System.Nullable<int> limit, string itemName, string quantity, AspxCommonInfo aspxCommonObj, decimal timeToAbandonCart)
    //{
    //    // quantity = quantity == "" ? null : quantity;
    //    try
    //    {
    //        List<ShoppingCartInfo> lstShopCart = AspxCartController.GetShoppingCartItemsDetails(offset, limit, itemName, quantity, aspxCommonObj, timeToAbandonCart);
    //        return lstShopCart;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------bind Abandoned cart details-------------------------
    //[WebMethod]
    //public List<AbandonedCartInfo> GetAbandonedCartDetails(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, decimal timeToAbandonCart)
    //{
    //    try
    //    {
    //        List<AbandonedCartInfo> bind = AspxCartController.GetAbandonedCartDetails(offset, limit, aspxCommonObj, timeToAbandonCart);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //// OrderManagement ---------------------get order details in grid-----------------------
    //[WebMethod]
    //public List<MyOrderListInfo> GetOrderDetails(int offset, System.Nullable<int> limit, System.Nullable<int> orderStatusName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<MyOrderListInfo> lstMyOrder = AspxOrderController.GetOrderDetails(offset, limit, orderStatusName, aspxCommonObj);
    //        return lstMyOrder;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    ////-----------------------Get Items Involved In Order----------------------- 
    //[WebMethod]
    //public List<ItemCommonInfo> GetItemsInvolvedInOrder(AspxCommonInfo aspxCommonObj, int orderID)
    //{
    //    try
    //    {
    //        List<ItemCommonInfo> lstMyItems = AspxOrderController.GetItemsInvolvedInOrder(aspxCommonObj, orderID);
    //        return lstMyItems;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //} 

    ////-----------------------Send Email for status update----------------------- 
    //[WebMethod]
    //public void NotifyOrderStatusUpdate(AspxCommonInfo aspxCommonObj,string receiverEmail, string billingShipping, string itemTable, string additionalFields, string templateName)
    //{
    //    try
    //    {
    //        EmailTemplate.SendEmailForOrderStatus(aspxCommonObj, receiverEmail, billingShipping, itemTable, additionalFields, templateName);

    //        if (additionalFields != null)
    //        {               
    //            string[] fields = additionalFields.Split('#');               
    //            int orderID =Int32.Parse(fields[4]);
    //            string orderstatus = (fields[0]);
    //            if (orderstatus == "Complete")
    //            {
    //                AspxGiftCardProvider.NotifyUserForGiftCardActivation(orderID, aspxCommonObj);
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //-----------------------Send Email for invoice----------------------- 
    [WebMethod]
    public void EmailInvoice(AspxCommonInfo aspxCommonObj, string receiverEmail, string billingShipping, string itemTable, string additionalFields, string templateName)
    {
        try
        {
            EmailTemplate.SendEmailForOrderStatus(aspxCommonObj, receiverEmail, billingShipping, itemTable, additionalFields, templateName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //-----------------------Update Order Status by Admin-----------------------   
    //[WebMethod]
    //public bool SaveOrderStatus(AspxCommonInfo aspxCommonObj, int orderStatusID, int orderID)
    //{
    //    try
    //    {
    //        bool chkMsg = AspxOrderController.SaveOrderStatus(aspxCommonObj, orderStatusID, orderID);
    //        return chkMsg;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //// InvoiceListMAnagement -----------------------get invoice details-----------------------
    //[WebMethod]
    //public List<InvoiceDetailsInfo> GetInvoiceDetailsList(int offset, System.Nullable<int> limit, InvoiceBasicInfo invoiceObj, AspxCommonInfo aspxCommonObj)
    //{
    //    //status = status == "" ? null : status;
    //    try
    //    {
    //        List<InvoiceDetailsInfo> lstInvoiceDetail = AspxInvoiceMgntProvider.GetInvoiceDetailsList(offset, limit, invoiceObj, aspxCommonObj);
    //        return lstInvoiceDetail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////Get Invoice Details
    //[WebMethod]
    //public List<InvoiceDetailByorderIDInfo> GetInvoiceDetailsByOrderID(int orderID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<InvoiceDetailByorderIDInfo> info = AspxInvoiceMgntProvider.GetInvoiceDetailsByOrderID(orderID, aspxCommonObj);
    //        return info;
    //    }

    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////--ShipmentsListManagement
    //[WebMethod]
    //public List<ShipmentsDetailsInfo> GetShipmentsDetails(int offset, System.Nullable<int> limit, ShipmentsBasicinfo shipmentObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShipmentsDetailsInfo> lstShipmentDet = AspxShipMethodMgntController.GetShipmentsDetails(offset, limit, shipmentObj, aspxCommonObj);
    //        return lstShipmentDet;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////-----------View Shipments Details--------------------------
    //[WebMethod]
    //public List<ShipmentsDetailsViewInfo> BindAllShipmentsDetails(int orderID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ShipmentsDetailsViewInfo> lstShipDetView = AspxShipMethodMgntController.BindAllShipmentsDetails(orderID, aspxCommonObj);
    //        return lstShipDetView;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //#region Rating Reviews Reporting
    ////--------------------bind Customer Reviews Roports-------------------------
    //[WebMethod]
    //public List<CustomerReviewReportsInfo> GetCustomerReviews(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CustomerReviewReportsInfo> bind = AspxRatingReviewController.GetCustomerReviews(offset, limit, aspxCommonObj);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------Show All Customer Reviews-------------------------
    //[WebMethod]
    //public List<UserRatingInformationInfo> GetAllCustomerReviewsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, UserRatingBasicInfo customerReviewObj)
    //{
    //    try
    //    {
    //        List<UserRatingInformationInfo> bind = AspxRatingReviewController.GetAllCustomerReviewsList(offset, limit, aspxCommonObj, customerReviewObj);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////------------------Bind User List------------------------------
    //[WebMethod]
    //public List<UserListInfo> GetUserList(int portalID)
    //{
    //    try
    //    {
    //        List<UserListInfo> lstUser = AspxRatingReviewController.GetUserList(portalID);
    //        return lstUser;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------Item Reviews Reports-------------------------
    //[WebMethod]
    //public List<ItemReviewsInfo> GetItemReviews(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemReviewsInfo> bind = AspxRatingReviewController.GetItemReviews(offset, limit, aspxCommonObj);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////---------------------Show All Item Reviews-------------------------
    //[WebMethod]
    //public List<UserRatingInformationInfo> GetAllItemReviewsList(int offset, System.Nullable<int> limit, UserRatingBasicInfo itemReviewObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<UserRatingInformationInfo> bind = AspxRatingReviewController.GetAllItemReviewsList(offset, limit, itemReviewObj, aspxCommonObj);
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    #endregion

    //#region RewardPoints
    ////----------------------RewardPoints-------------------
    //[WebMethod]
    //public void RewardPointsSaveByCore(int rewardRuleID, string uName, string email, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxRewardPointsController.RewardPointsSaveByCore(rewardRuleID, uName, email, aspxCommonObj);

    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public static void RewardPointsDeleteByCore(int rewardRuleID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxRewardPointsController.RewardPointsDeleteByCore(rewardRuleID, aspxCommonObj);

    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }

    //}
    //#endregion

    //-----------------------RelatedUPSellANDCrossSellItemsByCartItems-------------------
    //[WebMethod]
    //public List<ItemBasicDetailsInfo> GetYouMayAlsoLikeItems(AspxCommonInfo aspxCommonObj, int count)
    //{
    //    try
    //    {
    //        List<ItemBasicDetailsInfo> lstRelatedItem = AspxRelatedItemController.GetYouMayAlsoLikeItems(aspxCommonObj, count);
    //        return lstRelatedItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}



    ////------------------------bind order status name list-------------------------------
    //[WebMethod]
    //public List<StatusInfo> GetStatusList(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<StatusInfo> lstStatus = AspxCommonController.GetStatusList(aspxCommonObj);
    //        return lstStatus;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
  

    #region Payment Gateway and CheckOUT PROCESS
    [WebMethod]
    public bool CheckDownloadableItemOnly(int storeID, int portalID, int customerID, string sessionCode)
    {
        try
        {
            bool isAllDownload = AspxCartController.CheckDownloadableItemOnly(storeID, portalID, customerID, sessionCode);
            return isAllDownload;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //[WebMethod]
    //public List<PaymentGatewayListInfo> GetPGList(AspxCommonInfo aspxCommonObj)
    //{
    //    List<PaymentGatewayListInfo> pginfo = AspxCartController.GetPGList(aspxCommonObj);
    //    return pginfo;
    //}

    [WebMethod]
    public List<PaymentGateway> GetPaymentGateway(int portalID, string cultureName, string userName)
    {
        try
        {
            List<PaymentGateway> lstPayGateWay = AspxCartController.GetPaymentGateway(portalID, cultureName, userName);
            return lstPayGateWay;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<UserAddressInfo> GetUserAddressForCheckOut(int storeID, int portalID, string userName, string cultureName)
    {

        try
        {
            List<UserAddressInfo> lstUserAddress = AspxCartController.GetUserAddressForCheckOut(storeID, portalID, userName, cultureName);
            return lstUserAddress;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public bool CheckCreditCard(AspxCommonInfo aspxCommonObj, string creditCardNo)
    {
        try
        {
            bool isExist = AspxCartController.CheckCreditCard(aspxCommonObj, creditCardNo);
            return isExist;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    [WebMethod]
    public bool CheckEmailAddress(string email, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            bool isExist = AspxCartController.CheckEmailAddress(email, aspxCommonObj);
            return isExist;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    //[WebMethod(EnableSession = true)]
    //public int SaveOrderDetails(OrderDetailsCollection orderDetail)
    //{
    //    try
    //    {
    //        if (orderDetail.ObjOrderDetails.OrderStatusID == 0)
    //            orderDetail.ObjOrderDetails.OrderStatusID = 7;
    //        int orderID = AddOrderDetails(orderDetail);
    //        return orderID;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //public int AddOrderDetails(OrderDetailsCollection orderData)
    //{
    //    SQLHandler sqlH = new SQLHandler();
    //    SqlTransaction tran;
    //    tran = (SqlTransaction)sqlH.GetTransaction();
    //    //AspxCommerceSession sn = new AspxCommerceSession();
    //    if (orderData.ObjOrderDetails.InvoiceNumber == null || orderData.ObjOrderDetails.InvoiceNumber == "")
    //    {
    //        orderData.ObjOrderDetails.InvoiceNumber = DateTime.Now.ToString("yyyyMMddhhmmss");
    //    }
    //    try
    //    {
    //        int billingAddressID = 0;
    //        int shippingAddressId = 0;
    //        int orderID = 0;
    //        if (orderData.ObjOrderDetails.IsMultipleCheckOut == false)
    //        {
    //            if (orderData.ObjBillingAddressInfo.IsBillingAsShipping == true)
    //            {
    //                if (int.Parse(orderData.ObjBillingAddressInfo.AddressID) == 0 &&
    //                    int.Parse(orderData.ObjShippingAddressInfo.AddressID) == 0)
    //                {
    //                    int addressID = AspxOrderController.AddAddress(orderData, tran);
    //                    billingAddressID = AspxOrderController.AddBillingAddress(orderData, tran, addressID);
    //                    shippingAddressId = AspxOrderController.AddShippingAddress(orderData, tran, addressID);
    //                }
    //            }
    //            else
    //            {
    //                if (int.Parse(orderData.ObjBillingAddressInfo.AddressID) == 0)
    //                    billingAddressID = AspxOrderController.AddBillingAddress(orderData, tran);
    //                //For giftcard un registered user 
    //                if (orderData.ObjShippingAddressInfo.AddressID == null || int.Parse(orderData.ObjBillingAddressInfo.AddressID) == 0)
    //                {
    //                    if (!orderData.ObjOrderDetails.IsDownloadable && !orderData.ObjOrderDetails.IsShippingAddressRequired)
    //                    {
    //                        shippingAddressId = AspxOrderController.AddShippingAddress(orderData, tran);
    //                    }

    //                }
    //            }
    //        }
    //        int paymentMethodID = AspxOrderController.AddPaymentInfo(orderData, tran);

    //        if (billingAddressID > 0)
    //        {
    //            orderID = AspxOrderController.AddOrder(orderData, tran, billingAddressID, paymentMethodID);
    //            //sn.SetSessionVariable("OrderID", orderID);
    //            SetSessionVariable("OrderID", orderID);
    //            orderData.ObjOrderDetails.OrderID = orderID;
    //            SetSessionVariable("OrderCollection", orderData);
    //        }
    //        else
    //        {
    //            orderID = AspxOrderController.AddOrderWithMultipleCheckOut(orderData, tran, paymentMethodID);

    //            //sn.SetSessionVariable("OrderID", orderID);
    //            SetSessionVariable("OrderID", orderID);
    //            orderData.ObjOrderDetails.OrderID = orderID;
    //            SetSessionVariable("OrderCollection", orderData);
    //        }

    //        foreach (OrderTaxInfo item in orderData.ObjOrderTaxInfo)
    //        {
    //            int itemID = item.ItemID;
    //            int taxManageRuleID = item.TaxManageRuleID;
    //            decimal taxSubTotal = item.TaxSubTotal;
    //            int storeID = item.StoreID;
    //            int portalID = item.PortalID;
    //            string addedBy = item.AddedBy;
    //            string costVariantValueIDs = item.CostVariantsValueIDs;
    //            OrderTaxRuleMapping(itemID, orderID, taxManageRuleID, taxSubTotal, storeID, portalID, addedBy, costVariantValueIDs);
    //        }

    //        if (shippingAddressId > 0)
    //            AspxOrderController.AddOrderItems(orderData, tran, orderID, shippingAddressId);
    //        else
    //            AspxOrderController.AddOrderItemsList(orderData, tran, orderID);



    //        //add every paymentgateway
    //        // GiftCardController.IssueGiftCard(orderData.LstOrderItemsInfo, orderData.ObjCommonInfo.StoreID,
    //        //                               orderData.ObjCommonInfo.PortalID,orderData.ObjCommonInfo.AddedBy, orderData.ObjCommonInfo.CultureName);

    //        tran.Commit();
    //        return orderID;
    //    }
    //    catch (SqlException sqlEX)
    //    {

    //        throw new ArgumentException(sqlEX.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        tran.Rollback();
    //        throw ex;
    //    }
    //}
    #endregion

    //#region Payment Gateway Installation

    ////[WebMethod]
    ////public void OrderTaxRuleMapping(int itemID, int orderID, int taxManageRuleID, decimal taxSubTotal, int storeID, int portalID, string addedBy, string costVariantValueIDs)
    ////{
    ////    try
    ////    {
    ////        AspxPaymentController.OrderTaxRuleMapping(itemID, orderID, taxManageRuleID, taxSubTotal, storeID, portalID, addedBy, costVariantValueIDs);
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}

    //[WebMethod]
    //public List<PaymentGateWayInfo> GetAllPaymentMethod(int offset, int limit, AspxCommonInfo aspxCommonObj, PaymentGateWayBasicInfo paymentMethodObj)
    //{
    //    try
    //    {
    //        List<PaymentGateWayInfo> lstPayGateWay = AspxPaymentController.GetAllPaymentMethod(offset, limit, aspxCommonObj, paymentMethodObj);
    //        return lstPayGateWay;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<TransactionInfoList> GetAllTransactionDetail(AspxCommonInfo aspxCommonObj, int paymentGatewayID, System.Nullable<int> orderID)
    //{
    //    try
    //    {
    //        List<TransactionInfoList> lstTransaction = AspxPaymentController.GetAllTransactionDetail(aspxCommonObj, paymentGatewayID, orderID);
    //        return lstTransaction;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeletePaymentMethod(string paymentGatewayID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxPaymentController.DeletePaymentMethod(paymentGatewayID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void UpdatePaymentMethod(AspxCommonInfo aspxCommonObj, PaymentGateWayBasicInfo updatePaymentObj)
    //{
    //    try
    //    {
    //        AspxPaymentController.UpdatePaymentMethod(aspxCommonObj, updatePaymentObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void AddUpdatePaymentGateWaySettings(int paymentGatewaySettingValueID, int paymentGatewayID, string settingKeys, string settingValues, bool isActive, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxPaymentController.AddUpdatePaymentGateWaySettings(paymentGatewaySettingValueID, paymentGatewayID, settingKeys, settingValues, isActive, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<GetOrderdetailsByPaymentGatewayIDInfo> GetOrderDetailsbyPayID(int offset, int limit, PaymentGateWayBasicInfo bindOrderObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<GetOrderdetailsByPaymentGatewayIDInfo> lstOrderDetail = AspxPaymentController.GetOrderDetailsbyPayID(offset, limit, bindOrderObj, aspxCommonObj);
    //        return lstOrderDetail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<OrderDetailsByOrderIDInfo> GetAllOrderDetailsByOrderID(int orderId, int storeId, int portalId)
    //{
    //    try
    //    {
    //        List<OrderDetailsByOrderIDInfo> lstOrderDetail = AspxPaymentController.GetAllOrderDetailsByOrderID(orderId, storeId, portalId);
    //        return lstOrderDetail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ////[WebMethod]
    ////public List<OrderItemsInfo> GetAllOrderDetailsForView(int orderId, AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        List<OrderItemsInfo> lstOrderItem = AspxPaymentController.GetAllOrderDetailsForView(orderId, aspxCommonObj);
    ////        return lstOrderItem;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}
    ////[WebMethod]
    ////public List<OrderItemsTaxInfo> GetTaxDetailsByOrderID(int orderId, AspxCommonInfo aspxCommonObj)
    ////{
    ////    try
    ////    {
    ////        List<OrderItemsTaxInfo> lstOrderItem = AspxPaymentController.GetTaxDetailsByOrderID(orderId, aspxCommonObj);
    ////        return lstOrderItem;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}
    //#endregion

    //#region "StoreSetings"
    //[WebMethod(EnableSession = true)]
    //public StoreSettingInfo GetAllStoreSettings(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        StoreSettingInfo DefaultStoreSettings;
    //        DefaultStoreSettings = AspxStoreSetController.GetAllStoreSettings(aspxCommonObj);
    //        Session["DefaultStoreSettings"] = DefaultStoreSettings;
    //        return DefaultStoreSettings;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void UpdateStoreSettings(string settingKeys, string settingValues, string prevFilePath, string newFilePath, string prevStoreLogoPath, string newStoreLogoPath, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxStoreSetController.UpdateStoreSettings(settingKeys, settingValues, prevFilePath, newFilePath, prevStoreLogoPath, newStoreLogoPath, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<DisplayItemsOptionsInfo> BindItemsViewAsList()
    //{
    //    try
    //    {
    //        List<DisplayItemsOptionsInfo> bind = AspxStoreSetController.BindItemsViewAsList();
    //        return bind;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region CardType_Management
    ////------------------------bind All CardType name list-------------------------------        
    //[WebMethod]
    //public List<CardTypeInfo> GetAllCardTypeList(int offset, int limit, AspxCommonInfo aspxCommonObj, string cardTypeName, bool? isActive)
    //{
    //    try
    //    {
    //        List<CardTypeInfo> lstCardType = AspxCardTypeController.GetAllCardTypeList(offset, limit, aspxCommonObj, cardTypeName, isActive);
    //        return lstCardType;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<CardTypeInfo> AddUpdateCardType(AspxCommonInfo aspxCommonObj, CardTypeSaveInfo cardTypeSaveObj)
    //{

    //    FileHelperController imageObj = new FileHelperController();
    //    string uploadedFile;

    //    if (cardTypeSaveObj.NewFilePath != "" && cardTypeSaveObj.PrevFilePath != cardTypeSaveObj.NewFilePath)
    //    {
    //        string tempFolder = @"Upload\temp";
    //        uploadedFile = imageObj.MoveFileToSpecificFolder(tempFolder, cardTypeSaveObj.PrevFilePath,
    //                                                         cardTypeSaveObj.NewFilePath,
    //                                                         @"Modules\AspxCommerce\AspxCardTypeManagement\uploads\",
    //                                                         cardTypeSaveObj.CardTypeID, aspxCommonObj, "cardType_");

    //    }
    //    else
    //    {
    //        uploadedFile = cardTypeSaveObj.PrevFilePath;
    //    }
    //    try
    //    {
    //        List<CardTypeInfo> lstCardType = AspxCardTypeController.AddUpdateCardType(aspxCommonObj, cardTypeSaveObj, uploadedFile);
    //        return lstCardType;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteCardTypeByID(int cardTypeID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCardTypeController.DeleteCardTypeByID(cardTypeID, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteCardTypeMultipleSelected(string cardTypeIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCardTypeController.DeleteCardTypeMultipleSelected(cardTypeIDs, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //#endregion

    //#region OrderStatusManagement
    ////------------------------bind Allorder status name list-------------------------------    
    //[WebMethod]
    //public List<OrderStatusListInfo> GetAllStatusList(int offset, int limit, AspxCommonInfo aspxCommonObj, string orderStatusName, System.Nullable<bool> isActive)
    //{
    //    try
    //    {
    //        List<OrderStatusListInfo> lstOrderStat = AspxOrderStatusMgntController.GetAllStatusList(offset, limit, aspxCommonObj, orderStatusName, isActive);
    //        return lstOrderStat;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<OrderStatusListInfo> AddUpdateOrderStatus(AspxCommonInfo aspxCommonObj, SaveOrderStatusInfo SaveOrderStatusObj)
    //{
    //    try
    //    {
    //        List<OrderStatusListInfo> lstOrderStat = AspxOrderStatusMgntController.AddUpdateOrderStatus(aspxCommonObj, SaveOrderStatusObj);
    //        return lstOrderStat;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteOrderStatusByID(int orderStatusID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxOrderStatusMgntController.DeleteOrderStatusByID(orderStatusID, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteOrderStatusMultipleSelected(string orderStatusIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxOrderStatusMgntController.DeleteOrderStatusMultipleSelected(orderStatusIDs, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //#endregion

    //#region Admin DashBoard Chart
    ////------------------------bind order Chart by last week-------------------------------

    //[WebMethod]
    //public List<OrderChartInfo> GetOrderChartDetailsByLastWeek(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<OrderChartInfo> lstOrderChart = AspxAdminDashController.GetOrderChartDetailsByLastWeek(aspxCommonObj);
    //        return lstOrderChart;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////------------------------bind order Chart by current month-------------------------------    
    //[WebMethod]
    //public List<OrderChartInfo> GetOrderChartDetailsBycurentMonth(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<OrderChartInfo> lstOrderChart = AspxAdminDashController.GetOrderChartDetailsBycurentMonth(aspxCommonObj);
    //        return lstOrderChart;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////------------------------bind order Chart by one year-------------------------------    
    //[WebMethod]
    //public List<OrderChartInfo> GetOrderChartDetailsByOneYear(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<OrderChartInfo> lstOrderChart = AspxAdminDashController.GetOrderChartDetailsByOneYear(aspxCommonObj);
    //        return lstOrderChart;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    ////------------------------bind order Chart by last 24 hours-------------------------------    
    //[WebMethod]
    //public List<OrderChartInfo> GetOrderChartDetailsBy24Hours(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<OrderChartInfo> lstOrderChart = AspxAdminDashController.GetOrderChartDetailsBy24Hours(aspxCommonObj);
    //        return lstOrderChart;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //#endregion

    //#region Store Locator
    //[WebMethod]
    //public List<StoreLocatorInfo> GetAllStoresLocation(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<StoreLocatorInfo> lstStoreLocate = AspxStoreLocateController.GetAllStoresLocation(aspxCommonObj);
    //        return lstStoreLocate;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<StoreLocatorInfo> GetLocationsNearBy(double latitude, double longitude, double searchDistance, AspxCommonInfo aspxCommonObj)
    //{

    //    try
    //    {
    //        List<StoreLocatorInfo> lstStoreLocate = AspxStoreLocateController.GetLocationsNearBy(latitude, longitude, searchDistance, aspxCommonObj);
    //        return lstStoreLocate;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool UpdateStoreLocation(AspxCommonInfo aspxCommonObj, string storeName, String storeDescription, string streetName, string localityName, string city, string state, string country, string zip, double latitude, double longitude)
    //{
    //    try
    //    {
    //        bool retValue = AspxStoreLocateController.UpdateStoreLocation(aspxCommonObj, storeName, storeDescription, streetName, localityName, city, state, country, zip, latitude, longitude);
    //        return retValue;
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void AddStoreLocatorSettings(string settingKey, string settingValue, string cultureName, int storeID, int portalID, string userName)
    //{
    //    try
    //    {
    //        AspxStoreLocateController.AddStoreLocatorSettings(settingKey, settingValue, cultureName, storeID, portalID, userName);
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Online Users
    //[WebMethod]
    //public List<OnLineUserBaseInfo> GetRegisteredUserOnlineCount(int offset, int limit, string searchUserName, string searchHostAddress, string searchBrowser, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<OnLineUserBaseInfo> lstOnlineUser = AspxCustomerMgntController.GetRegisteredUserOnlineCount(offset, limit, searchUserName, searchHostAddress, searchBrowser, aspxCommonObj);
    //        return lstOnlineUser;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<OnLineUserBaseInfo> GetAnonymousUserOnlineCount(int offset, int limit, string searchHostAddress, string searchBrowser, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<OnLineUserBaseInfo> lst = AspxCustomerMgntController.GetAnonymousUserOnlineCount(offset, limit, searchHostAddress, searchBrowser, aspxCommonObj);
    //        return lst;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Customer Reports By Order Total
    ////--------------------bind Customer Order Total Roports-------------------------    
    //[WebMethod]
    //public List<CustomerOrderTotalInfo> GetCustomerOrderTotal(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, string user)
    //{
    //    try
    //    {
    //        List<CustomerOrderTotalInfo> lstCustOrderTot = AspxCustomerMgntController.GetCustomerOrderTotal(offset, limit, aspxCommonObj, user);
    //        return lstCustOrderTot;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region Store Access Management
    //[WebMethod]
    //public List<StoreAccessAutocomplete> SearchStoreAccess(string text, int keyID)
    //{
    //    try
    //    {
    //        List<StoreAccessAutocomplete> lstStoreAccess = AspxStoreAccessMgntController.SearchStoreAccess(text, keyID);
    //        return lstStoreAccess;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void SaveUpdateStoreAccess(int edit, int storeAccessKeyID, string storeAccessData, string reason, bool isActive, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxStoreAccessMgntController.SaveUpdateStoreAccess(edit, storeAccessKeyID, storeAccessData, reason, isActive, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeletStoreAccess(int storeAccessID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxStoreAccessMgntController.DeletStoreAccess(storeAccessID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<AspxUserList> GetAspxUser(string userName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AspxUserList> lstUser = AspxStoreAccessMgntController.GetAspxUser(userName, aspxCommonObj);
    //        return lstUser;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<AspxUserList> GetAspxUserEmail(string email, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AspxUserList> lstUserEmail = AspxStoreAccessMgntController.GetAspxUserEmail(email, aspxCommonObj);
    //        return lstUserEmail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    //[WebMethod]
    //public List<StoreAccessKey> GetStoreKeyID()
    //{
    //    try
    //    {
    //        List<StoreAccessKey> lstStAccessKey = AspxStoreAccessMgntController.GetStoreKeyID();
    //        return lstStAccessKey;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<StoreAccessInfo> LoadStoreAccessCustomer(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntController.LoadStoreAccessCustomer(offset, limit, search, startDate, endDate, status, aspxCommonObj);
    //        return lstStoreAccess;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<StoreAccessInfo> LoadStoreAccessEmails(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntController.LoadStoreAccessEmails(offset, limit, search, startDate, endDate, status, aspxCommonObj);
    //        return lstStoreAccess;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<StoreAccessInfo> LoadStoreAccessIPs(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntController.LoadStoreAccessIPs(offset, limit, search, startDate, endDate, status, aspxCommonObj);
    //        return lstStoreAccess;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<StoreAccessInfo> LoadStoreAccessDomains(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntController.LoadStoreAccessDomains(offset, limit, search, startDate, endDate, status, aspxCommonObj);
    //        return lstStoreAccess;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<StoreAccessInfo> LoadStoreAccessCreditCards(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntController.LoadStoreAccessCreditCards(offset, limit, search, startDate, endDate, status, aspxCommonObj);
    //        return lstStoreAccess;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //#endregion

    #region Store Close
    [WebMethod]
    public void SaveStoreClose(System.Nullable<bool> temporary, System.Nullable<bool> permanent, System.Nullable<DateTime> closeFrom, System.Nullable<DateTime> closeTill, int storeID, int portalID, string userName)
    {
        try
        {
            AspxStoreCloseController.SaveStoreClose(temporary, permanent, closeFrom, closeTill, storeID, portalID, userName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    //#region CustomerDetails
    //[WebMethod]
    //public List<CustomerDetailsInfo> GetCustomerDetails(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CustomerDetailsInfo> lstCustDetail = AspxCustomerMgntController.GetCustomerDetails(offset, limit, aspxCommonObj);
    //        return lstCustDetail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteMultipleCustomersByCustomerID(string customerIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCustomerMgntController.DeleteMultipleCustomers(customerIDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    ///// <summary>
    ///// Get customer details by it's ID
    ///// </summary>
    ///// <param name="customerId"></param>
    //[WebMethod]
    //public CustomerPersonalInfo GetCustomerDetailsByCustomerID(int customerID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        return AspxCustomerMgntController.GetCustomerDetailsByCustomerID(customerID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void GetCustomerRecentOrdersByCustomerID(int customerId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCustomerMgntController.GetCustomerRecentOrdersByCustomerID(customerId, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void GetCustomerShopingCartByCustomerID(int customerID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCustomerMgntController.GetCustomerShopingCartByCustomerID(customerID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void GetCustomerWishListByCustomerID(int customerId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCustomerMgntController.GetCustomerWishListByCustomerID(customerId, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteMultipleCustomersByCustomerID(string customerIDs, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCustomerMgntController.DeleteMultipleCustomers(customerIDs, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteCustomerByCustomerID(int customerId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCustomerMgntController.DeleteCustomer(customerId, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    ////------------------------Multiple Delete Recently viewed Items-------------------------------    
    //[WebMethod]
    //public void DeleteViewedItems(string viewedItems, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.DeleteViewedItems(viewedItems, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    ////------------------------Multiple Delete Compared viewed Items-------------------------------    
    //[WebMethod]
    //public void DeleteComparedItems(string compareItems, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxUserDashController.DeleteComparedItems(compareItems, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool GetModuleInstallationInfo(string moduleFriendlyName, AspxCommonInfo aspxCommonObj) // NEED TO BE COMMENTED
    //{
    //    try
    //    {
    //        bool isInstalled = AspxCommonController.GetModuleInstallationInfo(moduleFriendlyName, aspxCommonObj);
    //        return isInstalled;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public string GetDiscountPriceRule(int cartID, AspxCommonInfo aspxCommonObj, decimal shippingCost)
    //{

    //    try
    //    {
    //        string discount = AspxCartController.GetDiscountPriceRule(cartID, aspxCommonObj, shippingCost);
    //        return discount;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    [WebMethod]
    public int GetCartId(int storeID, int portalID, int customerID, string sessionCode)
    {
        try
        {
            int cartId = AspxCartController.GetCartId(storeID, portalID, customerID, sessionCode);
            return cartId;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //[WebMethod]
    //public UsersInfo GetUserDetails(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxCommonController objUser = new AspxCommonController();
    //        UsersInfo userInfo = objUser.GetUserDetails(aspxCommonObj);
    //        return userInfo;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    #region GetStoreSetting
    [WebMethod]
    public string GetStoreSettingValueByKey(string settingKey, int storeID, int portalID, string cultureName)
    {
        try
        {
            string setValue = AspxStoreSetController.GetStoreSettingValueByKey(settingKey, storeID, portalID, cultureName);
            return setValue;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    //#region Session Setting/Getting
    //[WebMethod(EnableSession = true)]
    //public void SetSessionVariableCoupon(string key, int value)
    //{
    //    if (System.Web.HttpContext.Current.Session[key] != null)
    //    {
    //        value = int.Parse(System.Web.HttpContext.Current.Session[key].ToString()) + 1;
    //    }
    //    else
    //    {
    //        value = value + 1;
    //    }

    //    System.Web.HttpContext.Current.Session[key] = value;
    //    //  string asdf = System.Web.HttpContext.Current.Session["OrderID"].ToString();
    //    // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
    //}

    //[WebMethod(EnableSession = true)]
    //public void SetSessionVariable(string key, object value)
    //{
    //    HttpContext.Current.Session[key] = value;
    //    //  string asdf = System.Web.HttpContext.Current.Session["OrderID"].ToString();
    //    // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
    //}

    //[WebMethod(EnableSession = true)]
    //public void ClearSessionVariable(string key)
    //{
    //    var keys = key.Split(',');
    //    for (int i = 0; i < keys.Length; i++)
    //    {
    //        var keycode = keys[i];
    //        if (System.Web.HttpContext.Current.Session[keycode] != null)
    //        {
    //            System.Web.HttpContext.Current.Session.Remove(keycode);
    //            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
    //        }
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //public void ClearALLSessionVariable()
    //{
    //    System.Web.HttpContext.Current.Session.Clear();
    //    // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
    //}

    //[WebMethod(EnableSession = true)]
    //public Decimal GetSessionVariable(string key)
    //{
    //    if (System.Web.HttpContext.Current.Session[key] != null)
    //    {
    //        string i = System.Web.HttpContext.Current.Session[key].ToString();
    //        return Convert.ToDecimal(i.ToString());
    //    }
    //    else
    //    {
    //        return 0;
    //    }

    //    // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
    //}
    //[WebMethod(EnableSession = true)]
    //public object GetSessionGiftCard(string key)
    //{
    //    if (System.Web.HttpContext.Current.Session[key] != null)
    //    {
    //        return System.Web.HttpContext.Current.Session[key];

    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}
    //[WebMethod(EnableSession = true)]
    //public string GetSessionCouponCode(string key)
    //{
    //    if (System.Web.HttpContext.Current.Session[key] != null)
    //    {
    //        string i = System.Web.HttpContext.Current.Session[key].ToString();
    //        return i;
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}
    //[WebMethod(EnableSession = true)]
    //public string GetSessionVariableCart(string key)
    //{
    //    string val = string.Empty;
    //    if (System.Web.HttpContext.Current.Session[key] != null)
    //    {
    //        val = System.Web.HttpContext.Current.Session[key].ToString();

    //    }
    //    return val;

    //    // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
    //}
    //#endregion

  //  #region StoreSettingImplementation
    //[WebMethod]
    //public decimal GetTotalCartItemPrice(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        decimal cartPrice = AspxCommonController.GetTotalCartItemPrice(aspxCommonObj);
    //        return cartPrice;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public int GetCompareItemsCount(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        int compCount = AspxCommonController.GetCompareItemsCount(aspxCommonObj);
    //        return compCount;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckAddressAlreadyExist(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        bool isExist = AspxCommonController.CheckAddressAlreadyExist(aspxCommonObj);
    //        return isExist;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
  //  #endregion

    [WebMethod]
    public List<PortalUserRoleListInfo> GetAllPortalUserList(int storeID, int portalID)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));

            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<PortalUserRoleListInfo>("usp_Aspx_GetPortalUserList", parameter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //#region "Currency conversion"

    //[WebMethod]
    //public List<CurrencyInfo> BindCurrencyList(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CurrencyInfo> lstCurrency = AspxCurrencyController.BindCurrencyList(aspxCommonObj);
    //        return lstCurrency;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CurrencyInfo> BindCurrencyAddedLists(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CurrencyInfo> lstCurrency = AspxCurrencyController.BindCurrencyAddedLists(offset, limit, aspxCommonObj);
    //        return lstCurrency;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<CurrencyInfoByCode> GetDetailsByCountryCode(string countryCode, string countryName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CurrencyInfoByCode> lstCountryDetails = AspxCurrencyController.GetDetailsByCountryCode(countryCode, countryName, aspxCommonObj);
    //        return lstCountryDetails;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void InsertNewCurrency(AspxCommonInfo aspxCommonObj, CurrencyInfo currencyInsertObj)
    //{
    //    try
    //    {
    //        AspxCurrencyController.InsertNewCurrency(aspxCommonObj, currencyInsertObj);
    //        HttpContext.Current.Cache.Remove("AspxCurrencyRate" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString());
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public bool CheckUniquenessForDisplayOrderForCurrency(AspxCommonInfo aspxCommonObj, int value, int currencyID)
    //{
    //    try
    //    {
    //        bool isUnique = AspxCurrencyController.CheckUniquenessForDisplayOrderForCurrency(aspxCommonObj, value, currencyID);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]

    //public bool CheckCurrencyCodeUniqueness(AspxCommonInfo aspxCommonObj, string currencyCode, int currencyID)
    //{
    //    try
    //    {
    //        bool isUnique = AspxCurrencyController.CheckCurrencyCodeUniqueness(aspxCommonObj, currencyCode, currencyID);
    //        return isUnique;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //public double GetCurrencyRateOnChange(AspxCommonInfo aspxCommonObj, string from, string to, string region)
    //{
    //    System.Net.ServicePointManager.Expect100Continue = false;
    //    try
    //    {
    //        double result = 0.0;
    //        StoreSettingConfig ssc = new StoreSettingConfig();
    //        string isRealTimeEnabled = ssc.GetStoreSettingsByKey("RealTimeCurrency", aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName);
    //        if (isRealTimeEnabled.ToLower() == "true")
    //        {
    //            try
    //            {
    //                result = AspxCommerce.Core.CurrencyConverter.GetRate(from, to);
    //            }
    //            catch (Exception)
    //            {
    //                return 1;
    //            }
    //        }
    //        else
    //        {
    //            result = AspxCurrencyProvider.GetRatefromTable(aspxCommonObj, to);
    //        }

    //        HttpContext.Current.Session["CurrencyCode"] = to;
    //        HttpContext.Current.Session["CurrencyRate"] = result;
    //        HttpContext.Current.Session["Region"] = region;
    //        return Math.Round(double.Parse(result.ToString()), 4);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;

    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //public double GetCurrencyRate(string from, string to)
    //{
    //    System.Net.ServicePointManager.Expect100Continue = false;
    //    try
    //    {
    //        double result = 0.0;

    //        result = AspxCommerce.Core.CurrencyConverter.GetRate(from, to);
    //        HttpContext.Current.Session["CurrencyRate"] = result;
    //        return result;

    //    }
    //    catch (Exception)
    //    {
    //        return 1;

    //    }
    //}

    //[WebMethod]
    //public void RealTimeUpdate(AspxCommonInfo aspxCommonObj)
    //{
    //    List<CurrencyInfo> lstCurrency = AspxCurrencyController.BindCurrencyList(aspxCommonObj);
    //    int currencyCount = lstCurrency.Count;
    //    CurrencyInfo DefaultStoreCurrency = lstCurrency.SingleOrDefault(x => x.IsPrimaryForStore == true);
    //    for (int i = 0; i < currencyCount; i++)
    //    {
    //        CurrencyInfo info = lstCurrency[i];
    //        if (info.CurrencyCode != DefaultStoreCurrency.CurrencyCode)
    //        {
    //            double result = 0.0;
    //            result = AspxCommerce.Core.CurrencyConverter.GetRate(DefaultStoreCurrency.CurrencyCode, info.CurrencyCode);
    //            AspxCurrencyController.UpdateRealTimeRate(aspxCommonObj, info.CurrencyCode, result);
    //            HttpContext.Current.Cache.Remove("AspxCurrencyRate" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString());
    //        }
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //public void SetStorePrimary(AspxCommonInfo aspxCommonObj, string currencyCode)
    //{
    //    try
    //    {
    //        AspxCurrencyController.SetStorePrimary(aspxCommonObj, currencyCode);
    //        Session["CurrencyCode"] = null;
    //        Session["Region"] = null;
    //        StoreSettingConfig ssc = new StoreSettingConfig();
    //        HttpContext.Current.Cache.Remove("AspxStoreSetting" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString());
    //        HttpContext.Current.Cache.Remove("AspxCurrencyRate" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString());
    //        ssc.ResetStoreSettingKeys(aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteMultipleCurrencyByCurrencyID(string currencyIDs, AspxCommonInfo aspxCommonObj)
    //{

    //    AspxCurrencyController.DeleteMultipleCurrencyByCurrencyID(currencyIDs, aspxCommonObj);

    //}
    //[WebMethod]
    //public List<CurrrencyRateInfo> GetCountryCodeRates(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CurrrencyRateInfo> currencyList = AspxCurrencyController.GetCountryCodeRates(aspxCommonObj);
    //        return currencyList;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //#endregion

    //[WebMethod]
    //public bool CheckCatalogPriorityUniqueness(int catalogPriceRuleID, int priority, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        bool isUnique = AspxCatalogPriceRuleController.CheckCatalogPriorityUniqueness(catalogPriceRuleID, priority, aspxCommonObj);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckCartPricePriorityUniqueness(int cartPriceRuleID, int priority, int portalID)
    //{
    //    try
    //    {
    //        bool isUnique = AspxCartPriceRuleProvider.CheckCartPricePriorityUniqueness(cartPriceRuleID, priority, portalID);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckUniquenessForDisplayOrder(AspxCommonInfo aspxCommonObj, int value, int shippingMethodID)
    //{
    //    try
    //    {
    //        bool isUnique = AspxShipMethodMgntController.CheckUniquenessForDisplayOrder(aspxCommonObj, value, shippingMethodID);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckShippingProviderUniqueness(AspxCommonInfo aspxCommonObj, int shippingProviderId, string shippingProviderName)
    //{
    //    try
    //    {
    //        bool isUnique = AspxShipProviderMgntController.CheckShippingProviderUniqueness(aspxCommonObj, shippingProviderId, shippingProviderName);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    //For Demo Availability Check
    [WebMethod]
    public bool IsStoreExists(string storeName)
    {
        bool i = false;
        var paraMeter = new List<KeyValuePair<string, object>>();
        paraMeter.Add(new KeyValuePair<string, object>("@PortalName", storeName));
        var sqlH = new SQLHandler();
        i = sqlH.ExecuteAsScalar<bool>("usp_Aspx_CheckPortal", paraMeter);
        return i;
    }

    //[WebMethod]
    //public bool CheckOrderStatusUniqueness(AspxCommonInfo aspxCommonObj, int orderStatusId, string orderStatusAliasName)
    //{
    //    try
    //    {
    //        bool isUnique = AspxOrderStatusMgntController.CheckOrderStatusUniqueness(aspxCommonObj, orderStatusId, orderStatusAliasName);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public bool CheckExisting(AspxCommonInfo aspxCommonObj, int storeAccesskeyId, string accessData)
    //{
    //    try
    //    {
    //        bool isUnique = AspxStoreAccessMgntController.CheckExisting(aspxCommonObj, storeAccesskeyId, accessData);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public bool CheckCouponTypeUniqueness(AspxCommonInfo aspxCommonObj, int couponTypeId, string couponType)
    //{
    //    try
    //    {
    //        bool isUnique = AspxCouponManageController.CheckCouponTypeUniqueness(aspxCommonObj, couponTypeId, couponType);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    [WebMethod]
    public bool CheckUniqueness(int storeID, int portalID, int value, int shippingMethodID)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
            Parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            Parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            Parameter.Add(new KeyValuePair<string, object>("@Value", value));
            Parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
            return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckUniquenessForDisplayOrder]", Parameter, "@IsUnique");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //[WebMethod]
    //public bool CheckTaxUniqueness(AspxCommonInfo aspxCommonObj, int value, int taxRuleID)
    //{
    //    try
    //    {
    //        bool isUnique = AspxTaxMgntController.CheckTaxUniqueness(aspxCommonObj, value, taxRuleID);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    #region "For Mobile"
    [WebMethod(EnableSession = true)]
    public string GetSessionCode()
    {
        return HttpContext.Current.Session.SessionID;
    }



    [WebMethod]
    public UrlInfo GetAllRequestUrls()
    {
        try
        {
            UrlInfo info = new UrlInfo();
            info.BaseUrl = "Modules/AspxCommerce/AspxCommerceServices/AspxCommerceWebService.asmx/";
            info.Home = new Home()
            {
                BestSellerItems = "GetBestSoldItems",
                FeaturedItems = "GetFeaturedItemsList",
                PopularTags = "GetAllPopularTags",
                RecentlyAddedItems = "GetLatestItemsList",
                ShoppingOption = "ShoppingOptionsByPrice",
                SpecialItems = "GetSpecialItems",
                CompareItems = "GetItemCompareList"
            };
            info.Item = new Item();
            info.Item.BestSellerItems = new BestSellerItems()
            {
                Title = "Best Seller Items",
                URL = "GetBestSoldItems",
                Count = "NoOfBestSellersItemDisplay",
                State = "EnableBestSellerItems",
            };

            info.Item.RecentlyAddedItems = new RecentlyAddedItems()
            {
                Title = "Recently Added Items",
                URL = "GetLatestItemsList",
                Count = "NoOfLatestItemsDisplay",
                State = "EnableLatestItems",
            };

            info.Item.SpecialItems = new SpecialItems()
            {
                Title = "Special Items",
                URL = "GetSpecialItems",
                Count = "NoOfSpecialItemDisplay",
                State = "EnableSpecialItems",
            };

            info.Item.PopularTags = new PopularTags()
            {
                Title = "Popular Tags",
                URL = "GetAllPopularTags",
                Count = "NoOfPopularTags",
            };

            info.Item.ShoppingOption = new ShoppingOption()
            {
                Title = "Shopping Options",
                URL = "ShoppingOptionsByPrice",
                Range = "ShoppingOptionRange",

            };

            info.Item.FeaturedItem = new FeaturedItem()
            {
                Title = "Featured products",
                URL = "GetFeaturedItemsList",
            };

            info.Item.CompareItems = new CompareItems()
            {
                Title = "My Compared Items",
                URL = "GetItemCompareList",
                Count = "MaxNoOfItemsToCompare",
                State = "EnableCompareItems"
            };
            info.MyAccount = new MyAccount { AccountDashboard = "GetMyOrderList,GetAddressBookDetails,BindCountryList", AccountInformation = "UpdateCustomer", ChangePassword = "ChangePassword", AddressBook = "GetAddressBookDetails,BindCountryList", MyOrders = "GetMyOrderList", MyWishList = "GetWishItemList", SharedWishList = "GetAllShareWishListItemMail", MyDigitalItems = "GetCustomerDownloadableItems", ReferredFriends = "GetUserReferredFriends", RecentHistory = "GetUserRecentlyViewedItems" };
            info.Login = "IsUserValid";
            info.StoreSettings = "GetAllStoreSettings";
            info.AdvanceSearch = "GetAttributes,GetItemsByDyanamicAdvanceSearch,AddUpdateSearchTerm";
            info.RegisterUser = "RegisterUser";
            return info;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod(EnableSession = true)]
    public UserInfoMob IsUserValid(string userName, string password, int portalID, int storeID, bool isChecked, string sessionCode)
    {
        SageFrameConfig pagebase = new SageFrameConfig();
        MembershipController member = new MembershipController();
        RoleController role = new RoleController();
        UserInfo user = member.GetUserDetails(portalID, userName);
        UserInfoMob userinfo = member.GetUserDetailsMob(portalID, userName);
        if (user.UserExists && user.IsApproved)
        {
            if (!(string.IsNullOrEmpty(password) && string.IsNullOrEmpty(password)))
            {
                if (PasswordHelper.ValidateUser(user.PasswordFormat, password, user.Password, user.PasswordSalt))
                {

                    int customerID = GetCustomerID();
                    if (customerID == 0)
                    {
                        CustomerGeneralInfo sageUserCust = CustomerGeneralInfoController.CustomerIDGetByUsername(user.UserName, portalID, storeID);
                        if (sageUserCust != null)
                        {
                            customerID = sageUserCust.CustomerID;
                            userinfo.CustomerID = customerID;
                        }
                    }

                    UpdateCartAnonymoususertoRegistered1(storeID, portalID, customerID, sessionCode);
                    userinfo.Status = 1;


                }
                else
                {
                    userinfo = new UserInfoMob();
                    userinfo.Status = 2;//User and Password Combination Doesnot match
                }
            }
        }
        else
        {
            userinfo = new UserInfoMob();
            userinfo.Status = 3;//User Doesnot Exist

        }
        return userinfo;
    }
    [WebMethod]
    public int GetCustomerIDByUserName(int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsScalar<int>("usp_ASPX_GetCustomerIDByUserName", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public bool UpdateCartAnonymoususertoRegistered1(int storeID, int portalID, int customerID, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsBool("usp_ASPX_UpdateCartAnonymoususertoRegistered", ParaMeter, "@IsUpdate");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod(EnableSession = true)]
    public void SetUserRoles(string strRoles)
    {
        HttpContext.Current.Session["SageUserRoles"] = strRoles;
        HttpCookie cookie = HttpContext.Current.Request.Cookies["SageUserRolesCookie"];
        if (cookie == null)
        {
            cookie = new HttpCookie("SageUserRolesCookie");
        }
        cookie["SageUserRolesProtected"] = strRoles;
        HttpContext.Current.Response.Cookies.Add(cookie);
    }
    [WebMethod(EnableSession = true)]
    public Int32 GetCustomerID()
    {
        int CustomerID = 0;
        {
            if (HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID] != null && HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID].ToString() != "")
            {
                CustomerID = Int32.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_CustomerID].ToString());
            }
            return CustomerID;
        }
    }

    [WebMethod(EnableSession = true)]
    public int RegisterUser(int storeID, int portalID, string firstName, string userName, string lastName, string password, string email)
    {
        SageFrameConfig pagebase = new SageFrameConfig();
        MembershipController _member = new MembershipController();
        int returnValue = 0;
        try
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email))
            {
                returnValue = 1;// ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserRegistration", "PleaseEnterAllRequiredFields"), "", SageMessageType.Alert);
            }
            else
            {
                int UserRegistrationType = pagebase.GetSettingIntByKey(SageFrameSettingKeys.PortalUserRegistration);

                bool isUserActive = UserRegistrationType == 2 ? true : false;

                UserInfo objUser = new UserInfo();
                objUser.ApplicationName = Membership.ApplicationName;
                objUser.FirstName = firstName;
                objUser.UserName = userName;
                objUser.LastName = lastName;
                string Pwd, PasswordSalt;
                PasswordHelper.EnforcePasswordSecurity(_member.PasswordFormat, password, out Pwd, out PasswordSalt);
                objUser.Password = Pwd;
                objUser.PasswordSalt = PasswordSalt;
                objUser.Email = email;
                objUser.SecurityQuestion = "";
                objUser.SecurityAnswer = "";
                objUser.IsApproved = true;
                objUser.CurrentTimeUtc = DateTime.Now;
                objUser.CreatedDate = DateTime.Now;
                objUser.UniqueEmail = 0;
                objUser.PasswordFormat = _member.PasswordFormat;
                objUser.PortalID = portalID;
                objUser.AddedOn = DateTime.Now;
                objUser.AddedBy = "";
                objUser.UserID = Guid.NewGuid();
                objUser.RoleNames = SystemSetting.REGISTER_USER_ROLENAME;
                objUser.StoreID = storeID;
                objUser.CustomerID = 0;

                UserCreationStatus status = new UserCreationStatus();
                //CheckRegistrationType(UserRegistrationType, ref objUser);

                int customerId;
                string sessionCode;
                sessionCode = GetSessionCode();
                MembershipDataProvider.RegisterPortalUser(objUser, out status, out customerId, UserCreationMode.REGISTER);
                if (status == UserCreationStatus.DUPLICATE_USER)
                {
                    returnValue = 2;// ShowMessage(SageMessageTitle.Notification.ToString(), UserName.Text.Trim() + " " + GetSageMessage("UserManagement", "NameAlreadyExists"), "", SageMessageType.Alert);
                    //GenerateCaptchaImage();
                }
                else if (status == UserCreationStatus.DUPLICATE_EMAIL)
                {
                    returnValue = 3; //ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "EmailAddressAlreadyIsInUse"), "", SageMessageType.Alert);
                    //GenerateCaptchaImage();
                }
                else if (status == UserCreationStatus.SUCCESS)
                {
                    UpdateCartAnonymoususertoRegistered(storeID, portalID, customerId, sessionCode);
                    returnValue = 4;

                }
            }
        }

        catch (Exception ex)
        {
            throw ex; // ProcessException(ex);
        }
        return returnValue;
    }
    #endregion

    //#region "For Brand"

    //[WebMethod]
    //public List<BrandInfo> GetAllBrandList(int offset, int limit, AspxCommonInfo aspxCommonObj, string brandName)
    //{
    //    try
    //    {
    //        List<BrandInfo> lstBrand = AspxBrandController.GetAllBrandList(offset, limit, aspxCommonObj, brandName);
    //        return lstBrand;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<BrandInfo> GetAllBrandForItem(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<BrandInfo> lstBrand = AspxBrandController.GetAllBrandForItem(aspxCommonObj);
    //        return lstBrand;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void InsertNewBrand(string prevFilePath, AspxCommonInfo aspxCommonObj, BrandInfo brandInsertObj)
    //{
    //    try
    //    {
    //        FileHelperController fileObj = new FileHelperController();
    //        string uplodedValue = string.Empty;
    //        string imagePath;
    //        if (brandInsertObj.BrandImageUrl != null && prevFilePath != brandInsertObj.BrandImageUrl)
    //        {
    //            string tempFolder = @"Upload\temp";
    //            uplodedValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, brandInsertObj.BrandImageUrl, @"Modules\AspxCommerce\AspxBrandManagement\uploads\", aspxCommonObj.StoreID, aspxCommonObj, "store_");

    //        }
    //        imagePath = uplodedValue.Length > 0 ? uplodedValue : "";
    //        AspxBrandController.InsertNewBrand(prevFilePath, aspxCommonObj, brandInsertObj, imagePath);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteBrand(string BrandID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxBrandController.DeleteBrand(BrandID, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}  
  

    //[WebMethod]
    //public List<BrandInfo> GetBrandByItemID(int ItemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<BrandInfo> lstBrand = AspxBrandController.GetBrandByItemID(ItemID, aspxCommonObj);
    //        return lstBrand;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void ActivateBrand(int brandID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxBrandController.ActivateBrand(brandID, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeActivateBrand(int brandID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxBrandController.DeActivateBrand(brandID, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    
    //[WebMethod]
    //public List<ItemBasicDetailsInfo> GetBrandItemsByBrandID(int offset, int limit, string brandName, int SortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemBasicDetailsInfo> lstItem = AspxBrandController.GetBrandItemsByBrandID(offset, limit, brandName, SortBy, rowTotal, aspxCommonObj);
    //        return lstItem;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<BrandInfo> GetBrandDetailByBrandID(string brandName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<BrandInfo> lstBrand = AspxBrandController.GetBrandDetailByBrandID(brandName, aspxCommonObj);
    //        return lstBrand;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public bool CheckBrandUniqueness(string brandName, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        bool isUnique = AspxBrandController.CheckBrandUniqueness(brandName, aspxCommonObj);
    //        return isUnique;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //#endregion

  //  #region "For Out of Stock Notification"

    //[WebMethod]
    //public List<Notification> GetNotificationList(int offset, int limit, GetAllNotificationInfo getAllNotificationObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<Notification> lstNotification = AspxOutStockNotifyController.GetNotificationList(offset, limit, getAllNotificationObj, aspxCommonObj);
    //        return lstNotification;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteNotification(string notificationID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxOutStockNotifyController.DeleteNotification(notificationID, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<NotifictionMail> GetEmail(string SKU, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<NotifictionMail> lstNotifMail = AspxOutStockNotifyController.GetEmail(SKU, aspxCommonObj);
    //        return lstNotifMail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void SendEmailNotification(SendEmailInfo emailInfo, string VariantId, string VarinatValue, string sku, string ProductUrl, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxOutStockNotifyController.SendEmailNotification(emailInfo, VariantId, VarinatValue, sku, ProductUrl, aspxCommonObj);

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void InsertNotification(AspxCommonInfo aspxCommonObj, InsertNotificationInfo insertNotificationObj)
    //{
    //    AspxOutStockNotifyController.InsertNotification(aspxCommonObj, insertNotificationObj);
    //}

    //[WebMethod]
    //public List<Notification> GetAllNotification(AspxCommonInfo aspxCommonObj, InsertNotificationInfo getNotificationObj)
    //{
    //    List<Notification> lstNotification = AspxOutStockNotifyController.GetAllNotification(aspxCommonObj, getNotificationObj);
    //    return lstNotification;
    //}

   // #endregion

    #region OtherPeopleViewedItems

    [WebMethod]
    public List<OtherViewedItemsInfo> GetOtherViewedItems(int storeID, int portalID, string cultureName, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            List<OtherViewedItemsInfo> view = sqlH.ExecuteAsList<OtherViewedItemsInfo>("[dbo].[usp_Aspx_GetOtherViewedItemList]", parameter);
            return view;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region YouJustExplored

    [WebMethod]
    public YouJustExploredSettingInfo GetYouJustExploredSetting(int storeId, int portalId, string cultureName)
    {
        List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
        parameterCollection.Add(new KeyValuePair<string, object>("@storeId", storeId));
        parameterCollection.Add(new KeyValuePair<string, object>("@PortalId", portalId));
        parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sqlHandle = new SQLHandler();
        return sqlHandle.ExecuteAsObject<YouJustExploredSettingInfo>("usp_Aspx_GetYouJustExploredSetting", parameterCollection);
    }

    [WebMethod]
    public List<YouJustExploredItemsInfo> GetYouJustExploredItems(int storeId, int portalId, string cultureName, string sessionCode, string userName, int count)
    {
        List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
        parameterCollection.Add(new KeyValuePair<string, object>("@storeId", storeId));
        parameterCollection.Add(new KeyValuePair<string, object>("@PortalId", portalId));
        parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        parameterCollection.Add(new KeyValuePair<string, object>("@SessionCOde", sessionCode));
        parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
        parameterCollection.Add(new KeyValuePair<string, object>("@count", count));
        SQLHandler sqlHandel = new SQLHandler();
        return sqlHandel.ExecuteAsList<YouJustExploredItemsInfo>("usp_Aspx_YouJustExplored", parameterCollection);

    }

    #endregion

  //  #region filter

    //[WebMethod]
    //public List<Filter> GetShoppingFilter(AspxCommonInfo aspxCommonObj, string categoryName, bool isByCategory)
    //{
    //    List<Filter> lstFilter = AspxFilterController.GetShoppingFilter(aspxCommonObj, categoryName, isByCategory);
    //    return lstFilter;
    //}

    //[WebMethod]
    //public List<CategoryDetailFilter> GetCategoryDetailFilter(string categorykey, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CategoryDetailFilter> lstCatDetFilter = AspxFilterController.GetCategoryDetailFilter(categorykey, aspxCommonObj);
    //        return lstCatDetFilter;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<ItemBasicDetailsInfo> GetShoppingFilterItemsResult(int offset, int limit, string brandIds, string attributes, decimal priceFrom, decimal priceTo, string categoryName, bool isByCategory, int sortBy, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemBasicDetailsInfo> lstItemBasic = AspxFilterController.GetShoppingFilterItemsResult(offset, limit, brandIds, attributes, priceFrom, priceTo, categoryName, isByCategory, sortBy, aspxCommonObj);
    //        return lstItemBasic;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<CategoryDetailFilter> GetAllSubCategoryForFilter(string categorykey, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<CategoryDetailFilter> lstCatDet = AspxFilterController.GetAllSubCategoryForFilter(categorykey, aspxCommonObj);
    //        return lstCatDet;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<BrandItemsInfo> GetAllBrandForCategory(string categorykey, bool isByCategory, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<BrandItemsInfo> lstBrandItem = AspxFilterController.GetAllBrandForCategory(categorykey, isByCategory, aspxCommonObj);
    //        return lstBrandItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


  //  #endregion

   // #region for Services Details

   
    //[WebMethod]
    //public void SaveServiceItem(AspxCommonInfo aspxCommonObj, int categoryId, List<ServiceItemInfo> serviceInfo)
    //{
    //    SQLHandler sqlH = new SQLHandler();
    //    SqlTransaction tran;
    //    tran = (SqlTransaction)sqlH.GetTransaction();
    //    try
    //    {
    //        AspxServiceController.SaveServiceItem(aspxCommonObj, categoryId, serviceInfo, tran);
    //        tran.Commit();
    //    }
    //    catch (Exception e)
    //    {
    //        tran.Rollback();
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<ServiceItemInfo> GetServiceItemInfo(AspxCommonInfo aspxCommonObj, int categoryId)
    //{
    //    List<ServiceItemInfo> serviceInfo = AspxServiceController.GetServiceItemInfo(aspxCommonObj, categoryId);
    //    return serviceInfo;
    //}

    //[WebMethod]
    //public void DeleteServiceItem(string option, AspxCommonInfo aspxCommonObj, int id)
    //{
    //    AspxServiceController.DeleteServiceItem(option, aspxCommonObj, id);
    //}  
    
  

    //[WebMethod]
    //public List<BookAppointmentGridInfo> GetBookAppointmentList(int offset, int limit, AspxCommonInfo aspxCommonObj, string appointmentStatusName, string branchName, string employeeName)
    //{
    //    try
    //    {
    //        List<BookAppointmentGridInfo> lstBookAppoint = AspxServiceController.GetBookAppointmentList(offset, limit, aspxCommonObj, appointmentStatusName, branchName, employeeName);
    //        return lstBookAppoint;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public void DeleteAppointment(string appointmentID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxServiceController.DeleteAppointment(appointmentID, aspxCommonObj);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<BookAppointmentInfo> GetAppointmentDetailByID(int appointmentID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<BookAppointmentInfo> lstBookAppoint = AspxServiceController.GetAppointmentDetailByID(appointmentID, aspxCommonObj);
    //        return lstBookAppoint;
    //    }
    //    catch (Exception e)
    //    {

    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<AppointmentStatusInfo> GetAppointmentStatusList(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AppointmentStatusInfo> lstAppointStatus = AspxServiceController.GetAppointmentStatusList(aspxCommonObj);
    //        return lstAppointStatus;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //[WebMethod]
    //public List<AppointmentSatusInfoBasic> GetAppointmentStatusListGrid(int limit, int offset, string statusName, bool? isActive, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<AppointmentSatusInfoBasic> lstAppointStatus = AspxServiceController.GetAppointmentStatusListGrid(limit, offset, statusName, isActive, aspxCommonObj);
    //        return lstAppointStatus;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //[WebMethod]
    //public void AddUpdateAppointmentStatus(AspxCommonInfo aspxCommonObj, AppointmentSatusInfoBasic appointmentStatusObj)
    //{
    //    try
    //    {
    //        AspxServiceController.AddUpdateAppointmentStatus(aspxCommonObj, appointmentStatusObj);
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    

    //[WebMethod(EnableSession = true)]
    //public void SetServiceSessionVariable(string key, object value)
    //{
    //    //HttpContext.Current.Session[key] = value;
    //    System.Web.HttpContext.Current.Session[key] = value;
    //}

    //[WebMethod]
    //public void SaveServiceProvider(AspxCommonInfo aspxCommonObj, ServiceProviderInfo providerSaveInfo)
    //{
    //    AspxServiceController.SaveServiceProvider(aspxCommonObj, providerSaveInfo);
    //}

    //[WebMethod]
    //public List<ServiceProviderInfo> GetServiceProviderNameList(AspxCommonInfo aspxCommonObj, int storeBranchId)
    //{
    //    List<ServiceProviderInfo> lstServProv = AspxServiceController.GetServiceProviderNameList(aspxCommonObj, storeBranchId);
    //    return lstServProv;
    //}
    //[WebMethod]
    //public List<ServiceProviderInfo> GetBranchProviderNameList(int offset, int? limit, int storeBranchId, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ServiceProviderInfo> lstServProv = AspxServiceController.GetBranchProviderNameList(offset, limit, storeBranchId, aspxCommonObj);
    //        return lstServProv;

    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}
    //[WebMethod]
    //public void DeleteServiceProvider(AspxCommonInfo aspxCommonObj, string id, int storeBranchId)
    //{
    //    try
    //    {
    //        AspxServiceController.DeleteServiceProvider(aspxCommonObj, id, storeBranchId);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

   
    //[WebMethod]
    //public bool CheckServiceProviderUniqueness(AspxCommonInfo aspxCommonObj, ServiceProviderInfo providerUniqueInfo)
    //{
    //    bool isSPUnique = AspxServiceController.CheckServiceProviderUniqueness(aspxCommonObj, providerUniqueInfo);
    //    return isSPUnique;
    //}
  

    //[WebMethod]
    //public List<OrderServiceDetailInfo> GetServiceDetailsByOrderID(int orderID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<OrderServiceDetailInfo> lstBookAppoint = AspxServiceController.GetServiceDetailsByOrderID(orderID, aspxCommonObj);
    //        return lstBookAppoint;
    //    }
    //    catch (Exception e)
    //    {

    //        throw e;
    //    }
    //}

    //[WebMethod]
    //public List<BookAppointmentInfo> GetAppointmentDetailsForExport(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<BookAppointmentInfo> lstBookAppoint = AspxServiceController.GetAppointmentDetailsForExport(aspxCommonObj);
    //        return lstBookAppoint;
    //    }
    //    catch (Exception e)
    //    {

    //        throw e;
    //    }
    //}
    //[WebMethod]
    //public List<StoreLocatorInfo> GetAllStoresForService(AspxCommonInfo aspxCommonObj, int? serviceCategoryId)
    //{
    //    try
    //    {
    //        List<StoreLocatorInfo> lstStoreLocator = AspxServiceController.GetAllStoresForService(aspxCommonObj, serviceCategoryId);
    //        return lstStoreLocator;
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }
    //}

  //  #endregion

    //#region Branch Management

    //[WebMethod]
    //public bool CheckBranchNameUniqueness(AspxCommonInfo aspxCommonObj, int storeBranchId, string storeBranchName)
    //{
    //    try
    //    {
    //        bool isUnique = AspxStoreBranchMgntController.CheckBranchNameUniqueness(aspxCommonObj, storeBranchId, storeBranchName);
    //        return isUnique;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void SaveAndUpdateStorebranch(string branchName, string branchImage, AspxCommonInfo aspxCommonObj, int storeBranchId)
    //{
    //    try
    //    {
    //        AspxStoreBranchMgntController.SaveAndUpdateStorebranch(branchName, branchImage, aspxCommonObj, storeBranchId);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<BranchDetailsInfo> GetStoreBranchList(int offset, int limit, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<BranchDetailsInfo> lstBrDetail = AspxStoreBranchMgntController.GetStoreBranchList(offset, limit, aspxCommonObj);
    //        return lstBrDetail;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void DeleteStoreBranches(string storeBranchIds, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxStoreBranchMgntController.DeleteStoreBranches(storeBranchIds, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //#endregion
    

    #region ItemViewList

    [WebMethod]
    public List<CategoryDetailsOptionsInfo> GetLatestItemsDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
    {
        try
        {
            List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListController.GetLatestItemsDetails(offset, limit, aspxCommonObj, sortBy, rowTotal);
            return lstCatDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<CategoryDetailsOptionsInfo> GetGiftCardItemsDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
    {
        try
        {
            List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListController.GetGiftCardItemsDetails(offset, limit, aspxCommonObj, sortBy, rowTotal);
            return lstCatDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<CategoryDetailsOptionsInfo> GetBestSoldItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
    {
        try
        {
            List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListController.GetBestSoldItemDetails(offset, limit, aspxCommonObj, sortBy, rowTotal);
            return lstCatDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<CategoryDetailsOptionsInfo> GetRecentlyViewedItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
    {
        try
        {
            List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListController.GetRecentlyViewedItemDetails(offset, limit, aspxCommonObj, sortBy, rowTotal);
            return lstCatDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<CategoryDetailsOptionsInfo> GetSpecialItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
    {
        try
        {
            List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListController.GetSpecialItemDetails(offset, limit, aspxCommonObj, sortBy, rowTotal);
            return lstCatDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<CategoryDetailsOptionsInfo> GetFeatureItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
    {
        try
        {
            List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListController.GetFeatureItemDetails(offset, limit, aspxCommonObj, sortBy, rowTotal);
            return lstCatDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<CategoryDetailsOptionsInfo> GetAllHeavyDiscountItems(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
    {
        try
        {
            List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListController.GetAllHeavyDiscountItems(offset, limit, aspxCommonObj, sortBy, rowTotal);
            return lstCatDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<CategoryDetailsOptionsInfo> GetAllSeasonalItems(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy)
    {
        try
        {
            List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListController.GetAllSeasonalItems(offset, limit, aspxCommonObj, sortBy);
            return lstCatDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion    

    //#region "For Item Videos"

    //[WebMethod]
    //public List<ItemsInfo.ItemSaveBasicInfo> GetItemVideoContents(int ItemID, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<ItemsInfo.ItemSaveBasicInfo> lstItemVideo = AspxItemMgntController.GetItemVideoContents(ItemID, aspxCommonObj);
    //        return lstItemVideo;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion    

    //[WebMethod]
    //public List<PriceHistoryInfo> GetPriceHistoryList(int itemId, AspxCommonInfo aspxCommerceObj)
    //{
    //    try
    //    {
    //        List<PriceHistoryInfo> lstPriceHistory = PriceHistoryController.GetPriceHistory(itemId, aspxCommerceObj);
    //        return lstPriceHistory;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    [WebMethod]
    public List<PriceHistoryInfo> BindPriceHistory(int offset, int limit, AspxCommonInfo aspxCommerceObj, string itemName, string userName)
    {
        try
        {
            List<PriceHistoryInfo> lstPriceHistory = PriceHistoryController.BindPriceHistory(offset, limit, aspxCommerceObj, itemName, userName);
            return lstPriceHistory;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //#region "Service Management"
    //[WebMethod]
    //public List<ServiceManageInfo> GetServiceEmployeeDetails(int offset, int limit, AspxCommonInfo aspxCommonInfo, int serviceId, int employeeId, int branchID, string itemName)
    //{
    //    try
    //    {
    //        List<ServiceManageInfo> list = AspxServiceProvider.GetServiceEmployeeDetails(offset, limit, aspxCommonInfo, serviceId, employeeId, branchID, itemName);
    //        return list;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<ServiceGridListInfo> GetAllServiceList(int offset, int limit, AspxCommonInfo aspxCommonInfo, string serviceName, string branchName)
    //{
    //    try
    //    {
    //        List<ServiceGridListInfo> list = AspxServiceProvider.GetAllServiceList(offset, limit, aspxCommonInfo, serviceName, branchName);
    //        return list;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<ServiceEmpInfo> GetServiceEmployee(int offset, int limit, AspxCommonInfo aspxCommonInfo, int serviceId, int branchID, string serviceEmpName)
    //{
    //    try
    //    {
    //        List<ServiceEmpInfo> list = AspxServiceProvider.GetServiceEmployee(offset, limit, aspxCommonInfo, serviceId, branchID, serviceEmpName);
    //        return list;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public List<BookingDetailsInfo> GetBookingDetails(int offset, int limit, AspxCommonInfo aspxCommonInfo, int employeeId, int? statusId, int branchID)
    //{
    //    try
    //    {
    //        List<BookingDetailsInfo> list = AspxServiceProvider.GetServiceBookingDetails(offset, limit, aspxCommonInfo, employeeId, statusId, branchID);
    //        return list;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region "Header Settings"
    //public string GetHeaderSetting(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        string headerType = AspxHeaderController.GetHeaderSetting(aspxCommonObj);
    //        return headerType;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public void SetHeaderSetting(string headerType, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxHeaderController.SetHeaderSetting(headerType, aspxCommonObj);

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    #region "Shopping Settings"
    public string GetShoppingBagSetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            string bagType = AspxShoppingBagController.GetShoppingBagSetting(aspxCommonObj);
            return bagType;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void SetShoppingBagSetting(string bagType, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AspxShoppingBagController.SetShoppingBagSetting(bagType, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
   
    //#region Import FROM Excel
    //[WebMethod]
    //public List<string> GetExcelConnection(string fileName)
    //{
    //    try
    //    {
    //        var xlsHeader = new List<string>();
    //        if (fileName != "")
    //        {
    //            string xlPath = Server.MapPath("~/" + fileName); //fileName.Replace("/", @"\"); //@"F:\AKBook.xls";    //location of xlsx file
    //            string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xlPath +
    //                            ";Extended Properties=\"Excel 12.0 Xml; HDR=YES; IMEX=1;\"";
    //            OleDbConnection con = new OleDbConnection(constr);
    //            OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", con);
    //            con.Open();
    //            OleDbDataReader dreader = cmd.ExecuteReader();

    //            while (dreader.Read())
    //            {
    //                for (int col = 0; col < dreader.FieldCount; col++)
    //                {

    //                    var columnName = dreader.GetName(col).ToString();
    //                    var columnFieldType = dreader.GetFieldType(col).ToString(); // Gets the column type
    //                    var columnDbType = dreader.GetDataTypeName(col).ToString(); // Gets the column database type
    //                    var columnValue = dreader.GetValue(col).ToString();
    //                    if (!xlsHeader.Contains(columnName))
    //                    {
    //                        xlsHeader.Add(columnName);
    //                    }
    //                }
    //            }
    //            dreader.Close();
    //            return xlsHeader;
    //        }
    //        else
    //        {
    //            return xlsHeader;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public List<AttributeInfo> GetAttributesListForImport()
    //{
    //    try
    //    {
    //        SqlConnection sqlConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
    //        SqlCommand sqlCmd = new SqlCommand();
    //        sqlCmd.Connection = sqlConn;
    //        sqlCmd.CommandText = "[dbo].[usp_Aspx_GetAllAttributes]";
    //        sqlCmd.CommandType = CommandType.StoredProcedure;
    //        sqlConn.Open();
    //        SqlDataReader dr = null;
    //        dr = sqlCmd.ExecuteReader();
    //        List<AttributeInfo> lst = new List<AttributeInfo>();
    //        while (dr.Read())
    //        {
    //            var att = new AttributeInfo();
    //            att.AttributeID = int.Parse(dr[0].ToString());
    //            att.AttributeName = dr[1].ToString();
    //            att.InputTypeID = int.Parse(dr[2].ToString());
    //            att.ValidationTypeID = int.Parse(dr[3].ToString());
    //            lst.Add(att);
    //        }
    //        return lst;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[WebMethod]
    //public void GetExcelData(List<FinalImportDataInfo> finalInfo, AspxCommonInfo aspxCommonObj, string fileName)
    //{
    //    try
    //    {
    //        if (fileName != "")
    //        {
    //            string xlPath = Server.MapPath("~/" + fileName); //@"F:\AKBook.xls"; //location of xlsx file
    //            string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xlPath +
    //                            ";Extended Properties=\"Excel 12.0 Xml; HDR=YES; IMEX=1;\"";
    //            OleDbConnection con = new OleDbConnection(constr);
    //            OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", con);
    //            con.Open();
    //            OleDbDataReader dreader = cmd.ExecuteReader();
    //            while (dreader.Read())
    //            {
    //                var itemValues = new List<ItemsValues>();

    //                for (int col = 0; col < dreader.FieldCount; col++)
    //                {

    //                    var columnName = dreader.GetName(col).ToString();
    //                    var columnFieldType = dreader.GetFieldType(col).ToString(); // Gets the column type
    //                    var columnDbType = dreader.GetDataTypeName(col).ToString(); // Gets the column database type
    //                    var columnValue = dreader.GetValue(col).ToString();
    //                    var id = GetAttrubuteId(finalInfo, columnName);
    //                    var inputId = GetInputTypeId(finalInfo, columnName);
    //                    var validId = GetValidationTypeId(finalInfo, columnName);
    //                    if (id > 0)
    //                    {
    //                        itemValues.Add(new ItemsValues()
    //                                           {
    //                                               AttributeId = id,
    //                                               InputTypeID = inputId,
    //                                               ValidationTypeID = validId,
    //                                               AttributeValue = columnValue

    //                                           });
    //                    }
    //                    if (columnName == "Category")
    //                    {
    //                        itemValues.Add(new ItemsValues()
    //                                           {
    //                                               AttributeId = 0,
    //                                               InputTypeID = 0,
    //                                               ValidationTypeID = 0,
    //                                               AttributeValue = columnValue
    //                                           });
    //                    }
    //                    if (dreader.FieldCount == col + 1)
    //                    {
    //                        int attributeSetId = 2;
    //                        int itemId = SaveItemFromExcel(itemValues, false, 1, attributeSetId, "USD", aspxCommonObj);
    //                        SQLHandler sqlH = new SQLHandler();
    //                        sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_TruncateCategoryForExcel]");
    //                        SaveItemImage(itemId, itemValues, aspxCommonObj);
    //                        SaveItemAttributesFromExcel(itemId, attributeSetId, itemValues, aspxCommonObj);
    //                        CacheHelper.Clear("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID);
    //                        CacheHelper.Clear("CategoryForSearch" + aspxCommonObj.StoreID + aspxCommonObj.PortalID);
    //                    }
    //                }
    //            }
    //            dreader.Close();
    //            GetExcelCostVariantData(aspxCommonObj, fileName);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private int GetAttrubuteId(List<FinalImportDataInfo> collection, string key)
    //{
    //    return (from t in collection where t.Header.ToLower().Trim() == key.ToLower().Trim() select t.AttributeID).FirstOrDefault();
    //}

    //private int GetInputTypeId(List<FinalImportDataInfo> collection, string key)
    //{
    //    return (from t in collection where t.Header.ToLower().Trim() == key.ToLower().Trim() select t.InputTypeID).FirstOrDefault();
    //}

    //private int GetValidationTypeId(List<FinalImportDataInfo> collection, string key)
    //{
    //    return (from t in collection where t.Header.ToLower().Trim() == key.ToLower().Trim() select t.ValidationTypeID).FirstOrDefault();
    //}

    //private object GetValuesByAttirbuteId(List<ItemsValues> itemObj, int attributeId)
    //{
    //    return (from t in itemObj where t.AttributeId == attributeId select t.AttributeValue).FirstOrDefault();
    //}

    //public int SaveItemFromExcel(List<ItemsValues> itemObj, bool isTypeSpecified, int itemTypeId, int attributeSetId, string currencyCode, AspxCommonInfo aspxCommonObj)// bool isActive, bool isModified,
    ////string sku, string activeFrom, string activeTo, string hidePrice, string isHideInRSS, string isHideToAnonymous,
    //// bool updateFlag)
    //{
    //    System.DateTime today = System.DateTime.Now;
    //    DateTime activeFrom = DateTime.Now;
    //    System.TimeSpan day1 = new System.TimeSpan(1, 0, 0, 0);
    //    activeFrom = today.Subtract(day1);
    //    DateTime activeTo = DateTime.Now;
    //    System.TimeSpan duration = new System.TimeSpan(365, 0, 0, 0);
    //    activeTo = today.Add(duration);
    //    bool hasSystemAttributesOnly = true;
    //    foreach (ItemsValues item in itemObj)
    //    {
    //        if (item.AttributeId > 43)
    //        {
    //            hasSystemAttributesOnly = false;
    //            break;
    //        }
    //    }
    //    try
    //    {
    //        string attributeIDs = "1,2,3,4,5,6,7,8,9,10,11,13,14,15,19,20,23,24,25,26,27,28,29,30,31,32,33,34";

    //        List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
    //        parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", 0));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", isTypeSpecified ? itemTypeId : 1));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetId));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@TaxRuleID", 1));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@SKU", GetValuesByAttirbuteId(itemObj, 4)));

    //        parameterCollection.Add(new KeyValuePair<string, object>("@ActiveFrom", activeFrom));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@ActiveTo", activeTo));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@HidePrice", false));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@HideInRSSFeed", false));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@HideToAnonymous", false));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@Name", GetValuesByAttirbuteId(itemObj, 1)));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@Description", GetValuesByAttirbuteId(itemObj, 2)));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@ShortDescription",
    //                                                                 GetValuesByAttirbuteId(itemObj, 3)));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@Weight", GetValuesByAttirbuteId(itemObj, 5)));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@Quantity", GetValuesByAttirbuteId(itemObj, 15)));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@Price", GetValuesByAttirbuteId(itemObj, 8)));

    //        if (GetValuesByAttirbuteId(itemObj, 13).ToString() != "")
    //        {
    //            parameterCollection.Add(new KeyValuePair<string, object>("@ListPrice",
    //                                                                     GetValuesByAttirbuteId(itemObj, 13)));
    //        }
    //        else
    //        {
    //            parameterCollection.Add(new KeyValuePair<string, object>("@ListPrice", null));
    //        }
    //        parameterCollection.Add(new KeyValuePair<string, object>("@NewFromDate", activeFrom));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@NewToDate", activeTo));

    //        parameterCollection.Add(new KeyValuePair<string, object>("@MetaTitle", GetValuesByAttirbuteId(itemObj, 9)));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@MetaKeyword", GetValuesByAttirbuteId(itemObj, 10)));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@MetaDescription",
    //                                                                 GetValuesByAttirbuteId(itemObj, 11)));

    //        //parameterCollection.Add(new KeyValuePair<string, object>("@VisibilityOptionValueID", true));

    //        if (GetValuesByAttirbuteId(itemObj, 26) != null)
    //        {
    //            parameterCollection.Add(new KeyValuePair<string, object>("@IsFeaturedOptionValueID",
    //                                                                     Boolean.Parse(
    //                                                                         GetValuesByAttirbuteId(itemObj, 26).
    //                                                                             ToString())));
    //        }
    //        if (GetValuesByAttirbuteId(itemObj, 29) != null)
    //        {
    //            parameterCollection.Add(new KeyValuePair<string, object>("@IsSpecialOptionValueID",
    //                                                                     Boolean.Parse(
    //                                                                         GetValuesByAttirbuteId(itemObj, 29).
    //                                                                             ToString())));
    //        }
    //        parameterCollection.Add(new KeyValuePair<string, object>("@FeaturedFrom",
    //                                                                 activeFrom));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@FeaturedTo", activeTo));


    //        parameterCollection.Add(new KeyValuePair<string, object>("@SpecialFrom", activeFrom));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@SpecialTo", activeTo));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@Length", GetValuesByAttirbuteId(itemObj, 32)));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@Height", GetValuesByAttirbuteId(itemObj, 33)));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@Width", GetValuesByAttirbuteId(itemObj, 34)));
    //        //parameterCollection.Add(new KeyValuePair<string, object>("@IsPromo", false));
    //        //parameterCollection.Add(new KeyValuePair<string, object>("@ServiceDuration", null));

    //        parameterCollection.Add(new KeyValuePair<string, object>("@HasSystemAttributesOnly",
    //                                                                 hasSystemAttributesOnly));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@AttributeIDs", attributeIDs));

    //        //For Static tabs
    //        parameterCollection.Add(new KeyValuePair<string, object>("@CategoriesIDs",
    //                                                                 GetValuesByAttirbuteId(itemObj, 0)));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@RelatedItemsIDs", "0"));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@UpSellItemsIDs", "0"));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@CrossSellItemsIDs", "0"));

    //        parameterCollection.Add(new KeyValuePair<string, object>("@DownloadInfos", ""));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", true));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", false));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", false));
    //        //parameterCollection.Add(new KeyValuePair<string, object>("@BrandID", '0'));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@CurrencyCode", currencyCode));
    //        //parameterCollection.Add(new KeyValuePair<string, object>("@VideosIDs", '0'));
    //        SQLHandler sqlH = new SQLHandler();
    //        return sqlH.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_ItemAddUpdateFromExcel", parameterCollection,
    //                                                    "@NewItemID");
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public void SaveItemImage(int itemId, List<ItemsValues> imageObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        SQLHandler sageSql = new SQLHandler();
    //        var baseImage = GetValuesByAttirbuteId(imageObj, 12);
    //        if (baseImage != null && baseImage != "")
    //        {
    //            var imagePath = baseImage;
    //            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
    //            parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
    //            parameterCollection.Add(new KeyValuePair<string, object>("@PathList", imagePath));
    //            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", true));
    //            parameterCollection.Add(new KeyValuePair<string, object>("@ImageType", 1));
    //            parameterCollection.Add(new KeyValuePair<string, object>("@AlternateText", GetValuesByAttirbuteId(imageObj, 1)));
    //            parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", "1"));
    //            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
    //            sageSql.ExecuteNonQuery("[dbo].[usp_Aspx_InsertUpdateImageFromExcel]", parameterCollection);
    //        }

    //        //var smallImage = GetValuesByAttirbuteId(imageObj, 21);
    //        //if (smallImage != null && smallImage != "")
    //        //{
    //        //    var imagePath1 = "Modules/AspxCommerce/AspxItemsManagement/uploads + smallImage;
    //        //    List<KeyValuePair<string, object>> parameterCollection1 = new List<KeyValuePair<string, object>>();
    //        //    parameterCollection1.Add(new KeyValuePair<string, object>("@ItemID", itemId));
    //        //    parameterCollection1.Add(new KeyValuePair<string, object>("@PathList", imagePath1));
    //        //    parameterCollection1.Add(new KeyValuePair<string, object>("@IsActive", true));
    //        //    parameterCollection1.Add(new KeyValuePair<string, object>("@ImageType", 2));
    //        //    parameterCollection1.Add(new KeyValuePair<string, object>("@AlternateText", ""));
    //        //    parameterCollection1.Add(new KeyValuePair<string, object>("@DisplayOrder", "3"));
    //        //    parameterCollection1.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
    //        //    sageSql.ExecuteNonQuery("[dbo].[usp_Aspx_InsertUpdateImageFromExcel]", parameterCollection1);
    //        //}

    //        var thumbImage = GetValuesByAttirbuteId(imageObj, 22);
    //        if (thumbImage != null && thumbImage != "")
    //        {
    //            var imagePath2 = thumbImage;
    //            List<KeyValuePair<string, object>> parameterCollection2 = new List<KeyValuePair<string, object>>();
    //            parameterCollection2.Add(new KeyValuePair<string, object>("@ItemID", itemId));
    //            parameterCollection2.Add(new KeyValuePair<string, object>("@PathList", imagePath2));
    //            parameterCollection2.Add(new KeyValuePair<string, object>("@IsActive", true));
    //            parameterCollection2.Add(new KeyValuePair<string, object>("@ImageType", 3));
    //            parameterCollection2.Add(new KeyValuePair<string, object>("@AlternateText", GetValuesByAttirbuteId(imageObj, 1)));
    //            parameterCollection2.Add(new KeyValuePair<string, object>("@DisplayOrder", "2"));
    //            parameterCollection2.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
    //            sageSql.ExecuteNonQuery("[dbo].[usp_Aspx_InsertUpdateImageFromExcel]", parameterCollection2);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public void SaveItemAttributesFromExcel(int itemID, int attributeSetId, List<ItemsValues> itemObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        SQLHandler sqlH = new SQLHandler();
    //        foreach (var itemsValues in itemObj)
    //        {
    //            if (itemsValues.AttributeId > 43)
    //            {
    //                if (itemsValues.InputTypeID > 0 && itemsValues.InputTypeID != 8)
    //                {
    //                    List<KeyValuePair<string, object>> parameterCollection =
    //                        new List<KeyValuePair<string, object>>();
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetId));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", true));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", false));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue",
    //                                                                             itemsValues.AttributeValue));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", itemsValues.AttributeId));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", itemsValues.InputTypeID));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@ValidationTypeID",
    //                                                                             itemsValues.ValidationTypeID));

    //                    int? groupId = GetAttributeSetGroupID(itemsValues.AttributeId, attributeSetId, aspxCommonObj.StoreID, aspxCommonObj.PortalID);

    //                    parameterCollection.Add(new KeyValuePair<string, object>("@GroupID", groupId));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", false));
    //                    parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", 1));
    //                    //inputTypeID //validationTypeID
    //                    string valueType = string.Empty;
    //                    if (itemsValues.InputTypeID == 1)
    //                    {
    //                        if (itemsValues.ValidationTypeID == 3)
    //                        {
    //                            valueType = "DECIMAL";
    //                        }
    //                        else if (itemsValues.ValidationTypeID == 5)
    //                        {
    //                            valueType = "INT";
    //                        }
    //                        else
    //                        {
    //                            valueType = "NVARCHAR";
    //                        }
    //                    }
    //                    else if (itemsValues.InputTypeID == 2)
    //                    {
    //                        valueType = "TEXT";
    //                    }
    //                    else if (itemsValues.InputTypeID == 3)
    //                    {
    //                        valueType = "DATE";
    //                    }
    //                    else if (itemsValues.InputTypeID == 4)
    //                    {
    //                        valueType = "Boolean";
    //                    }
    //                    else if (itemsValues.InputTypeID == 5 || itemsValues.InputTypeID == 6 ||
    //                             itemsValues.InputTypeID == 9 ||
    //                             itemsValues.InputTypeID == 10 ||
    //                             itemsValues.InputTypeID == 11 || itemsValues.InputTypeID == 12)
    //                    {
    //                        valueType = "OPTIONS";

    //                        //TODO: to get attributeValueID from itemsValues.AttributeValue
    //                        List<KeyValuePair<string, object>> paramCol =
    //                        new List<KeyValuePair<string, object>>();
    //                        paramCol.Add(new KeyValuePair<string, object>("@AttributeValue", itemsValues.AttributeValue));
    //                        paramCol.Add(new KeyValuePair<string, object>("@AttributeID", itemsValues.AttributeId));
    //                        paramCol.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
    //                        paramCol.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
    //                        paramCol.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
    //                        paramCol.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
    //                        itemsValues.AttributeValue = sqlH.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_GetAttributesValueIDByValue",
    //                                         paramCol, "@AttributeValueID");
    //                        parameterCollection.RemoveAll(x => x.Key.Equals("@AttributeValue"));
    //                        parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue",
    //                                                                             itemsValues.AttributeValue));
    //                    }
    //                    else if (itemsValues.InputTypeID == 7)
    //                    {
    //                        valueType = "DECIMAL";
    //                    }
    //                    //else if (itemsValues.InputTypeID == 8)
    //                    //{
    //                    //    valueType = "FILE";
    //                    //    valueType.Replace("T, "");
    //                    //    valueType.Replace("P, "");
    //                    //}
    //                    sqlH.ExecuteNonQuery("dbo.usp_Aspx_ItemAttributesValue" + valueType + "AddUpdate",
    //                                         parameterCollection);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public int? GetAttributeSetGroupID(int attributeId, int attributeSetId, int storeId, int portalId)
    //{
    //    try
    //    {
    //        List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
    //        parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeId));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetId));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
    //        parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
    //        SQLHandler sqlH = new SQLHandler();
    //        int? id = sqlH.ExecuteAsScalar<int>("[dbo].[usp_Aspx_GetGroupIDByAttributeSetID]",
    //                                           parameterCollection);
    //        return id;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion

    //#region ImportCostVariants

    //[WebMethod]
    //public void GetExcelCostVariantData(AspxCommonInfo aspxCommonObj, string fileName)
    //{
    //    try
    //    {
    //        if (fileName != "")
    //        {
    //            string xlPath = Server.MapPath("~/" + fileName); //@"F:\optionvariant 022113.xls"; //location of xlsx file
    //            string constr1 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xlPath +
    //                             ";Extended Properties=\"Excel 12.0 Xml; HDR=YES; IMEX=1;\"";
    //            OleDbConnection con1 = new OleDbConnection(constr1);
    //            OleDbCommand cmd1 = new OleDbCommand("select * from [Sheet2$]", con1);
    //            con1.Open();
    //            OleDbDataReader dreader1 = cmd1.ExecuteReader();
    //            while (dreader1.Read())
    //            {
    //                var costVariantOptions = new List<CostVariantsOptions>();

    //                for (int col = 0; col < dreader1.FieldCount; col++)
    //                {

    //                    var columnName = dreader1.GetName(col).ToString();
    //                    var columnFieldType = dreader1.GetFieldType(col).ToString(); // Gets the column type
    //                    var columnDbType = dreader1.GetDataTypeName(col).ToString(); // Gets the column database type
    //                    var columnValue = dreader1.GetValue(col).ToString();
    //                    costVariantOptions.Add(new CostVariantsOptions()
    //                                               {
    //                                                   ColumnName = columnName,
    //                                                   ColumnValue = columnValue

    //                                               });
    //                    if (dreader1.FieldCount == col + 1)
    //                    {
    //                        SaveCostVariantOptionsFromExcel(costVariantOptions, aspxCommonObj);
    //                    }
    //                }
    //            }
    //            dreader1.Close();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public void SaveCostVariantOptionsFromExcel(List<CostVariantsOptions> optionObj, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
    //        parameter.Add(new KeyValuePair<string, object>("@Sku", optionObj[0].ColumnValue));
    //        parameter.Add(new KeyValuePair<string, object>("@CostVariantName", optionObj[4].ColumnValue));
    //        parameter.Add(new KeyValuePair<string, object>("@CostVariantValueName", optionObj[5].ColumnValue));
    //        parameter.Add(new KeyValuePair<string, object>("@CostVariantPriceValue", decimal.Parse(optionObj[3].ColumnValue)));
    //        parameter.Add(new KeyValuePair<string, object>("@CostVariantWeightValue", decimal.Parse(optionObj[1].ColumnValue)));
    //        parameter.Add(new KeyValuePair<string, object>("@Quantity", int.Parse(optionObj[2].ColumnValue)));
    //        parameter.Add(new KeyValuePair<string, object>("@Description", ""));
    //        parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
    //        parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
    //        parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
    //        parameter.Add(new KeyValuePair<string, object>("@IsActive", 1));
    //        parameter.Add(new KeyValuePair<string, object>("@InputTypeID", 6));
    //        parameter.Add(new KeyValuePair<string, object>("@DisplayOrder", optionObj[6].ColumnValue));
    //        SQLHandler sqlH = new SQLHandler();
    //        sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_SaveCostVariantsFromExcel]", parameter);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //#endregion

    //#region Item Management Setting
    //[WebMethod]
    //public ItemTabSettingInfo ItemTabSettingGet(AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        ItemTabSettingInfo lstItem = AspxItemMgntController.ItemTabSettingGet(aspxCommonObj);
    //        return lstItem;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //[WebMethod]
    //public void ItemTabSettingSave(string SettingKeys, string SettingValues, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxItemMgntController.ItemTabSettingSave(SettingKeys, SettingValues, aspxCommonObj);            
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //#endregion


}