using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using SageFrame.Framework;

namespace AspxCommerce.Core
{
    public class AspxCommonController
    {
        public AspxCommonController()
        {
        }
        //---------------- Added for Grouped Item check ---------------------
        public static bool CheckIfItemIsGrouped(int itemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isGrouped = AspxCommonProvider.CheckIfItemIsGrouped(itemID, aspxCommonObj);
                return isGrouped;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckSessionActive(AspxCommonInfo aspxCommonObj)
        {
            if (HttpContext.Current.User != null)
            {
                SecurityPolicy objSecurity = new SecurityPolicy();
                FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(aspxCommonObj.PortalID);
                if (ticket != null && ticket.Name != ApplicationKeys.anonymousUser)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static List<SearchTermInfo> GetSearchStatistics(int count, string commandName, AspxCommonInfo aspxCommonObj)
        {
            List<SearchTermInfo> lstSearchTerm = AspxCommonProvider.GetSearchStatistics(count, commandName, aspxCommonObj);
            return lstSearchTerm;
        }

        public static void UpdateItemRating(ItemReviewBasicInfo ratingManageObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCommonProvider.UpdateItemRating(ratingManageObj, aspxCommonObj);

            }

            catch (Exception e)
            {
                throw e;
            }
        }

        //public static List<RatingCriteriaInfo> GetItemRatingCriteria(AspxCommonInfo aspxCommonObj, bool isFlag)
        //{
        //    try
        //    {
        //        List<RatingCriteriaInfo> lstRating = AspxCommonProvider.GetItemRatingCriteria(aspxCommonObj,isFlag);
        //        return lstRating;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public static List<ItemReviewDetailsInfo> GetItemRatingByReviewID(int itemReviewID, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        List<ItemReviewDetailsInfo> lstItemRVDetail = AspxCommonProvider.GetItemRatingByReviewID(itemReviewID, aspxCommonObj);
        //        return lstItemRVDetail;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public static void DeleteSingleItemRating(string itemReviewID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCommonProvider.DeleteSingleItemRating(itemReviewID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<RatingLatestInfo> GetRecentItemReviewsAndRatings(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {

                List<RatingLatestInfo> lstRatingNew = AspxCommonProvider.GetRecentItemReviewsAndRatings(offset, limit, aspxCommonObj);
                return lstRatingNew;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<StatusInfo> GetStatus(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StatusInfo> lstStatus = AspxCommonProvider.GetStatus(aspxCommonObj);
                return lstStatus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessBoolean(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, bool attributeValue)
        {
            try
            {
                bool isUnique = AspxCommonProvider.CheckUniquenessBoolean(flag, storeID, portalID, attributeID, attributeValue);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessDecimal(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, Decimal attributeValue)
        {
            try
            {
                bool isUnique = AspxCommonProvider.CheckUniquenessDecimal(flag, storeID, portalID, attributeID, attributeValue);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessDate(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, DateTime attributeValue)
        {
            try
            {
                bool isUnique = AspxCommonProvider.CheckUniquenessDate(flag, storeID, portalID, attributeID, attributeValue);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessFile(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, string attributeValue)
        {
            try
            {
                bool isUnique = AspxCommonProvider.CheckUniquenessFile(flag, storeID, portalID, attributeID, attributeValue);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static bool CheckUniquenessInt(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, Int32 attributeValue)
        {
            try
            {
                bool isUnique = AspxCommonProvider.CheckUniquenessInt(flag, storeID, portalID, attributeID, attributeValue);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessNvarchar(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, string attributeValue)
        {
            try
            {
                bool isUnique = AspxCommonProvider.CheckUniquenessNvarchar(flag, storeID, portalID, attributeID, attributeValue);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessText(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, string attributeValue)
        {
            try
            {
                bool isUnique = AspxCommonProvider.CheckUniquenessText(flag, storeID, portalID, attributeID, attributeValue);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //---------------- Added for unique name check ---------------------
        public static bool CheckUniqueName(AttributeBindInfo attrbuteUniqueObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isUnique = AspxCommonProvider.CheckUniqueName(attrbuteUniqueObj, aspxCommonObj);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<StatusInfo> GetStatusList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StatusInfo> lstStatus = AspxCommonProvider.GetStatusList(aspxCommonObj);
                return lstStatus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<PortalRole> GetPortalRoles(int portalID, bool isAll, string userName)
        {
            List<PortalRole> lstPortalRole = AspxCommonProvider.GetPortalRoles(portalID, isAll, userName);
            return lstPortalRole;
        }

        //--------------------Store Lists------------------------    

        public static List<StoreInfo> GetAllStores(AspxCommonInfo aspxCommonObj)
        {
            List<StoreInfo> lstStore = AspxCommonProvider.GetAllStores(aspxCommonObj);
            return lstStore;
        }
        //----------------country list------------------------------    

        public static List<CountryInfo> BindCountryList()
        {
            try
            {
                List<CountryInfo> lstCountry = AspxCommonProvider.BindCountryList();
                return lstCountry;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //----------------state list--------------------------     

        public static List<StateInfo> BindStateList(string countryCode)
        {
            try
            {
                List<StateInfo> lstState = AspxCommonProvider.BindStateList(countryCode);
                return lstState;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static ItemCommonInfo GetItemInfoFromSKU(string SKU,AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ItemCommonInfo lstItem = AspxCommonProvider.GetItemInfoFromSKU(SKU, aspxCommonObj);
                return lstItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetUserBillingEmail(int addressID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string userEmail = AspxCommonProvider.GetUserBillingEmail(addressID, aspxCommonObj);
                return userEmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region AddToCart

        public static int AddItemstoCart(int itemID, decimal itemPrice, int itemQuantity, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int retValue = AspxCommonProvider.AddItemstoCart(itemID, itemPrice, itemQuantity, false, aspxCommonObj);
                return retValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<GroupItemsOutOfStockInfo> AddGroupItemsToCart(GroupProductCartInfo itemCartObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string UserName = aspxCommonObj.UserName;
                string[] itemIDs = itemCartObj.CartItemIDs.Split(',');
                string[] itemPrices = itemCartObj.CartItemPrices.Split(',');
                string[] itemQtys = itemCartObj.CartItemQtys.Split(',');
                string[] itemSKUs = itemCartObj.CartItemSKUs.Split(',');
                List<GroupItemsOutOfStockInfo> listItems = new List<GroupItemsOutOfStockInfo>();

                for (int count = 0; count < itemIDs.Length; count++)
                {
                    int ItemID;
                    decimal ItemPrice;
                    int ItemQty;
                    string ItemSKU = string.Empty;
                    string AllowRealTimeNotifications = string.Empty;
                    StoreSettingConfig ssc = new StoreSettingConfig();
                    AllowRealTimeNotifications = ssc.GetStoreSettingsByKey(StoreSetting.AllowRealTimeNotifications, aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName);
                    if ((itemIDs[count].Trim() != "") && (itemPrices[count].Trim() != "") && (itemPrices[count].Trim() != "") && (itemSKUs[count].Trim() != ""))
                    {
                        ItemID = Convert.ToInt32(itemIDs[count]);
                        ItemPrice = Convert.ToDecimal(itemPrices[count]);
                        ItemQty = Convert.ToInt32(itemQtys[count]);
                        ItemSKU = itemSKUs[count];
                        int retValue = AspxCommonProvider.AddItemstoCart(ItemID, ItemPrice, ItemQty, true, aspxCommonObj);
                        if (retValue == 1)
                        {
                            listItems.Add(new GroupItemsOutOfStockInfo(retValue, ItemID, ItemSKU, false));
                        }
                        else if (retValue == 2)
                        {
                            listItems.Add(new GroupItemsOutOfStockInfo(retValue, ItemID, ItemSKU, true));

                        }
                        else
                        {
                            bool isOutOfStock = AspxCommonProvider.CheckItemOutOfStock(ItemID, string.Empty, aspxCommonObj);
                            if (isOutOfStock == true)
                            {
                                listItems.Add(new GroupItemsOutOfStockInfo(retValue, ItemID, ItemSKU, true));
                            }
                            else
                            {
                                listItems.Add(new GroupItemsOutOfStockInfo(retValue, ItemID, ItemSKU, false));
                            }

                        }
                    }
                }

                return listItems;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static int AddItemstoCartFromDetail(AddItemToCartInfo AddItemToCartObj, AspxCommonInfo aspxCommonObj, GiftCard giftCardDetail, CartKit kitInfo)
        {
            try
            {
                int retValue = AspxCommonProvider.AddItemstoCartFromDetail(AddItemToCartObj, aspxCommonObj, giftCardDetail, kitInfo);
                return retValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckItemOutOfStock(int itemID, string costVariantsValueIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool retValue = AspxCommonProvider.CheckItemOutOfStock(itemID, costVariantsValueIDs, aspxCommonObj);
                return retValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //#region MiniCart Display
        ////----------------------Count my cart items--------------------     
        //public static int GetCartItemsCount(AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        int cartItemCount = AspxCommonProvider.GetCartItemsCount(aspxCommonObj);
        //        return cartItemCount;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        //#endregion

        #region StoreSettingImplementation

        public static decimal GetTotalCartItemPrice(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                decimal cartPrice = AspxCommonProvider.GetTotalCartItemPrice(aspxCommonObj);
                return cartPrice;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static int GetCompareItemsCount(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int compCount = AspxCommonProvider.GetCompareItemsCount(aspxCommonObj);
                return compCount;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static bool CheckAddressAlreadyExist(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isExist = AspxCommonProvider.CheckAddressAlreadyExist(aspxCommonObj);
                return isExist;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        public static bool GetModuleInstallationInfo(string moduleFriendlyName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isInstalled = AspxCommonProvider.GetModuleInstallationInfo(moduleFriendlyName, aspxCommonObj);
                return isInstalled;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public StartUpInfoCollection GetStartUpInformation(string moduleFriendlyName1, string moduleFriendlyName2, StoreAccessDetailsInfo objStoreAccess)
        {
            try
            {

                AspxCommonProvider objCommonProvider = new AspxCommonProvider();
                return objCommonProvider.GetStartUpInformation(moduleFriendlyName1, moduleFriendlyName2, objStoreAccess);
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetStateCode(string cCode, string stateName)
        {
            string stateCode = AspxCommonProvider.GetStateCode(cCode, stateName);
            return stateCode;
        }


        #region Aspx BreadCrumb

        public string GetCategoryForItem(int storeID, int portalID, string itemSku, string cultureName)
        {
            try
            {
                string retString = AspxBreadCrumbController.GetCategoryForItem(storeID, portalID, itemSku, cultureName);
                return retString;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BreadCrumInfo> GetBreadCrumb(string name, AspxCommonInfo commonInfo)
        {
            try
            {
                List<BreadCrumInfo> list = AspxBreadCrumbController.GetBreadCrumb(name, commonInfo);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BreadCrumInfo> GetCategoryName(string name, AspxCommonInfo commonInfo)
        {
            try
            {
                List<BreadCrumInfo> list = AspxBreadCrumbController.GetCategoryName(name, commonInfo);
                list.Reverse();
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BreadCrumInfo> GetItemCategories(string itemName, AspxCommonInfo commonInfo)
        {
            try
            {
                List<BreadCrumInfo> list = AspxBreadCrumbController.GetItemCategories(itemName, commonInfo);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public UsersInfo GetUserDetails(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCommonProvider objUser = new AspxCommonProvider();
                UsersInfo userInfo = objUser.GetUserDetails(aspxCommonObj);
                return userInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateCartAnonymoususertoRegistered(int storeID, int portalID, int? customerID, string sessionCode)
        {
            try
            {
                AspxCommonProvider objCommon = new AspxCommonProvider();
                return objCommon.UpdateCartAnonymoususertoRegistered(storeID,portalID,customerID,sessionCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SortOptionTypeInfo> BindItemsSortByList()
        {
            try
            {
                List<SortOptionTypeInfo> bind = AspxSearchController.BindItemsSortByList();
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Search
        //Search Setting
        public SearchSettingInfo GetSearchSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SearchSettingInfo objSearchSetting = AspxSearchController.GetSearchSetting(aspxCommonObj);
                return objSearchSetting;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetSearchSetting(SearchSettingInfo searchSettingObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxSearchController.SetSearchSetting(searchSettingObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Auto Complete search Box

        public List<SearchTermList> GetSearchedTermList(string search, AspxCommonInfo aspxCommonObj)
        {
            List<SearchTermList> srInfo = AspxSearchController.GetSearchedTermList(search, aspxCommonObj);
            return srInfo;
        }

        #region General Search
        //----------------General Search Sort By DropDown Options----------------------------

        public List<ItemBasicDetailsInfo> GetSimpleSearchResult(int offset, int limit, int categoryID, bool isGiftCard, string searchText, int sortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemBasicDetailsInfo> lstItemBasic = AspxSearchController.GetSimpleSearchResult(offset, limit, categoryID, isGiftCard, searchText, sortBy, rowTotal, aspxCommonObj);
                return lstItemBasic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[WebMethod]
        //public List<SortOptionTypeInfo> BindItemsSortByList()
        //{
        //    try
        //    {
        //        List<SortOptionTypeInfo> bind = AspxSearchController.BindItemsSortByList();
        //        return bind;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<ItemBasicDetailsInfo> GetItemsByGeneralSearch(int storeID, int portalID, string nameSearchText, float priceFrom, float priceTo,
                                                                  string skuSearchText, int categoryID, string categorySearchText, bool isByName, bool isByPrice, bool isBySKU, bool isByCategory, string userName, string cultureName)
        {
            try
            {
                List<ItemBasicDetailsInfo> lstItemBasic = AspxSearchController.GetItemsByGeneralSearch(storeID, portalID, nameSearchText, priceFrom, priceTo,
                                                                                                     skuSearchText, categoryID, categorySearchText, isByName, isByPrice, isBySKU, isByCategory, userName, cultureName);
                return lstItemBasic;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryInfo> GetAllCategoryForSearch(string prefix, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CategoryInfo> catList = AspxSearchController.GetAllCategoryForSearch(prefix, isActive, aspxCommonObj);
                return catList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SearchTermList> GetTopSearchTerms(AspxCommonInfo aspxCommonObj, int Count)
        {
            try
            {
                List<SearchTermList> searchList = AspxSearchController.GetTopSearchTerms(aspxCommonObj, Count);
                return searchList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        //#region Advance Search

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

        #endregion

        #region "Header Settings"
        public HeaderSettingInfo GetHeaderSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                return AspxHeaderController.GetHeaderSetting(aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetHeaderSetting(string headerType, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxHeaderController.SetHeaderSetting(headerType, aspxCommonObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "Prevent App domain to restart on Folder deletion"
        /// <summary>
        ///The appdomain restart occurs when there are changes in asp.net filesystem except root.
        ///Could be prevented using reflection
        /// </summary>
        public static void AvoidAppDomainRestartOnFolderDelete()
        {
            PropertyInfo p = typeof(HttpRuntime).GetProperty("FileChangesMonitor",
               BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

            object o = p.GetValue(null, null);

            FieldInfo f = o.GetType().GetField("_dirMonSubdirs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);

            object monitor = f.GetValue(o);

            MethodInfo m = monitor.GetType().GetMethod("StopMonitoring",
                BindingFlags.Instance | BindingFlags.NonPublic);

            m.Invoke(monitor, new object[] { });
        }
        #endregion

        #region "AspxSession"
        public static string GetSessionValue(string dictionaryName, string key)
        {
            string value = string.Empty;
            try
            {
                if (HttpContext.Current.Session[dictionaryName] != null)
                {
                    Dictionary<string, string> form = (Dictionary<string, string>)HttpContext.Current.Session[dictionaryName];
                    if (form.ContainsKey(key))
                    {
                        if (!string.IsNullOrEmpty(key))
                        {
                            value = form[key];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
                //Logger.Error("{0}: Error while checking Session value from Dictionary", ex, "SessionDictionary");
            }
            return value;
        }

        public static void SetSessionValue(string dictionaryName, string key, string value)
        {
            if (!String.IsNullOrEmpty(key))
            {
                try
                {
                    if (HttpContext.Current.Session[dictionaryName] != null)
                    {
                        Dictionary<string, string> form = (Dictionary<string, string>)HttpContext.Current.Session[dictionaryName];
                        if (form.ContainsKey(key))
                        {
                            form[key] = value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    //Logger.Error("{0}: Error while checking Session value from Dictionary", ex, "SessionDictionary");
                }
            }
        }

        public static void RemoveSessionValue(string dictionaryName, string key)
        {
            if (!String.IsNullOrEmpty(key))
            {
                try
                {
                    if (HttpContext.Current.Session[dictionaryName] != null)
                    {
                        Dictionary<string, string> form = (Dictionary<string, string>)HttpContext.Current.Session[dictionaryName];
                        form.Remove(key); // no error if key didn't exist
                    }
                }
                catch
                {
                    // Logger.Error("{0}: Error while checking Session value from Dictionary", ex, "SessionDictionary");
                }
            }
        }
        #endregion

    }
}
