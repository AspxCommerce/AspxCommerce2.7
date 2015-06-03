using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using AspxCommerce.Core;
using SageFrame.Common;
using SageFrame.NewsLetter;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System.Threading.Tasks;
using System.Web.SessionState;
using System.Threading;
using System.Data;

namespace AspxCommerce.Core
{
    public class AspxCoreController
    {
        public static Thread catalogThread;

        #region General Functions
        //--------------------Roles Lists------------------------         
        public List<PortalRole> GetAllRoles(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<PortalRole> lstPortalRole = AspxCommonController.GetPortalRoles(aspxCommonObj.PortalID, true, aspxCommonObj.UserName);
                return lstPortalRole;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------Store Lists------------------------         
        public List<StoreInfo> GetAllStores(AspxCommonInfo aspxCommonObj)
        {
            List<StoreInfo> lstStore = AspxCommonController.GetAllStores(aspxCommonObj);
            return lstStore;
        }

        //----------------country list------------------------------         
        public List<CountryInfo> BindCountryList()
        {
            try
            {
                List<CountryInfo> lstCountry = AspxCommonController.BindCountryList();
                return lstCountry;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------state list--------------------------  
        public List<StateInfo> BindStateList(string countryCode)
        {
            try
            {
                List<StateInfo> lstState = AspxCommonController.BindStateList(countryCode);
                return lstState;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ItemCommonInfo GetItemInfoFromSKU(string SKU,AspxCommonInfo aspxCommonObj)
        {
            ItemCommonInfo lstItem = AspxCommonController.GetItemInfoFromSKU(SKU, aspxCommonObj);
            return lstItem;
        }
        #endregion

        #region Bind Users DropDown

        public List<UserInRoleInfo> BindRoles(bool isAll, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<UserInRoleInfo> lstUserInRole = AspxItemMgntController.BindRoles(isAll, aspxCommonObj);
                return lstUserInRole;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Item Details Module
        public ItemBasicDetailsInfo GetItemBasicInfoByitemSKU(string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            ItemBasicDetailsInfo frmItemAttributes = AspxItemMgntController.GetItemBasicInfo(itemSKU, aspxCommonObj);
            return frmItemAttributes;
        }

        public List<ItemBasicDetailsInfo> GetGroupedItemsByGroupSKU(string groupSKU, AspxCommonInfo aspxCommonObj)
        {
            List<ItemBasicDetailsInfo> groupedProducts = AspxItemMgntController.GetGroupedItemsByGroupSKU(groupSKU, aspxCommonObj);
            return groupedProducts;
        }

        public decimal GetStartPriceOfGroupAfterDeletion(string groupItems, AspxCommonInfo aspxCommonObj)
        {
            decimal startPrice = AspxItemMgntController.GetStartPriceOfGroupAfterDeletion(groupItems, aspxCommonObj);
            return startPrice;
        }


        public List<AttributeFormInfo> GetItemDetailsByitemSKU(string itemSKU, int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeFormInfo> frmItemAttributes = AspxItemMgntController.GetItemDetailsInfoByItemSKU(itemSKU, attributeSetID, itemTypeID, aspxCommonObj);
            return frmItemAttributes;
        }

        //---------------------Get rating/ review of Item Per User ------------------      
        public List<ItemRatingByUserInfo> GetItemRatingPerUser(int offset, int limit, string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemRatingByUserInfo> lstItemRating = AspxRatingReviewController.GetItemRatingPerUser(offset, limit, itemSKU, aspxCommonObj);
                return lstItemRating;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Get rating/ review of Item Per User ------------------       
        public List<ItemReviewDetailsInfo> GetItemRatingByReviewID(int itemReviewID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemReviewDetailsInfo> lstItemRVDetail = AspxCommonProvider.GetItemRatingByReviewID(itemReviewID, aspxCommonObj);
                return lstItemRVDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<RatingCriteriaInfo> GetItemRatingCriteriaByReviewID(AspxCommonInfo aspxCommonObj, int itemReviewID, bool isFlag)
        {
            try
            {
                List<RatingCriteriaInfo> rating = AspxRatingReviewController.GetItemRatingCriteriaByReviewID(aspxCommonObj, itemReviewID, isFlag);
                return rating;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static List<RatingCriteriaInfo> GetItemRatingCriteria(AspxCommonInfo aspxCommonObj, bool isFlag)
        //{
        //    try
        //    {
        //        List<RatingCriteriaInfo> lstRating = AspxCommonProvider.GetItemRatingCriteria(aspxCommonObj, isFlag);
        //        return lstRating;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //------------------------Item single rating management------------------------

        public void DeleteSingleItemRating(string itemReviewID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCommonController.DeleteSingleItemRating(itemReviewID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //---------------Delete multiple item rating informations--------------------------       
        public void DeleteMultipleItemRatings(string itemReviewIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxRatingReviewController.DeleteMultipleItemRatings(itemReviewIDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetItemVideos(string SKU, AspxCommonInfo aspxCommonObj)
        {
            string lstItem = AspxItemMgntController.GetItemVideos(SKU, aspxCommonObj);
            return lstItem;
        }

        #region NeW CostVariant Combination
        public List<VariantCombination> GetCostVariantCombinationbyItemSku(string itemSku, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<VariantCombination> lstVarCom = AspxItemMgntController.GetCostVariantCombinationbyItemSku(itemSku, aspxCommonObj);
                return lstVarCom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemCostVariantsInfo> GetCostVariantsByItemSKU(string itemSku, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemCostVariantsInfo> lstItemCostVar = AspxItemMgntController.GetCostVariantsByItemSKU(itemSku, aspxCommonObj);
                return lstItemCostVar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CostVariantInfo> GetCostVariantForItem(AspxCommonInfo aspxCommonObj) //not used in ItemDetailPage
        {
            try
            {
                List<CostVariantInfo> lstCostVar = AspxItemMgntController.GetCostVariantForItem(aspxCommonObj);
                return lstCostVar;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CostVariantValuesInfo> GetCostVariantValues(int costVariantID, AspxCommonInfo aspxCommonObj) // not ussed in ItemDetailPage
        {
            try
            {
                List<CostVariantValuesInfo> lstCostVarValue = AspxItemMgntController.GetCostVariantValues(costVariantID, aspxCommonObj);
                return lstCostVarValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCostVariantForItem(int itemId, AspxCommonInfo aspxCommonObj) // not used in ItemDetailPage
        {
            try
            {
                AspxItemMgntController.DeleteCostVariantForItem(itemId, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VariantCombination> GetCostVariantsOfItem(int itemId, AspxCommonInfo aspxCommonObj) // not used in ItemDetailPage
        {
            try
            {
                List<VariantCombination> lstVarComb = AspxItemMgntController.GetCostVariantsOfItem(itemId, aspxCommonObj);
                return lstVarComb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveAndUpdateItemCostVariantCombination(CostVariantsCombination itemCostVariants, AspxCommonInfo aspxCommonObj) // not used in ItemDetailPage
        {
            try
            {
                string cvCombinations = string.Empty;
                foreach (var objCombination in itemCostVariants.VariantOptions)
                {
                    cvCombinations += objCombination.CombinationIsActive;
                    cvCombinations += "," + objCombination.ImageFile;
                    cvCombinations += "," + objCombination.CombinationPriceModifier;
                    cvCombinations += "," + objCombination.CombinationPriceModifierType;
                    cvCombinations += "," + objCombination.CombinationQuantity;
                    cvCombinations += "," + objCombination.CombinationType;
                    cvCombinations += "," + objCombination.CombinationValues;
                    cvCombinations += "," + objCombination.CombinationValuesName;
                    cvCombinations += "," + objCombination.CombinationWeightModifier;
                    cvCombinations += "," + objCombination.CombinationWeightModifierType;
                    cvCombinations += "," + objCombination.DisplayOrder;
                    if (itemCostVariants.VariantOptions.Count - 1 != 0)
                        cvCombinations += "%";
                }
                // cvCombinations = cvCombinations.Replace("Upload/temp/", "Modules/AspxCommerce/AspxItemsManagement/uploads/");
                FileHelperController Fch = new FileHelperController();
                string tempFolder = @"Upload\temp";
                string destPath = @"Modules/AspxCommerce/AspxItemsManagement/uploads/";
                Fch.MoveVariantsImageFile(tempFolder, destPath, itemCostVariants, aspxCommonObj);
                AspxItemMgntController.SaveAndUpdateItemCostVariantCombination(itemCostVariants, aspxCommonObj, cvCombinations);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region RecentlyViewedItems
        public void AddUpdateRecentlyViewedItems(RecentlyAddedItemInfo addUpdateRecentObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntController.AddUpdateRecentlyViewedItems(addUpdateRecentObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region PriceList In Item Detail Page
        public List<PriceHistoryInfo> GetPriceHistoryList(int itemId, AspxCommonInfo aspxCommerceObj)
        {
            try
            {
                List<PriceHistoryInfo> lstPriceHistory = PriceHistoryController.GetPriceHistory(itemId, aspxCommerceObj);
                return lstPriceHistory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Item Rating In Item Detail Page
        public List<RatingCriteriaInfo> GetItemRatingCriteria(AspxCommonInfo aspxCommonObj, bool isFlag)
        {
            try
            {

                List<RatingCriteriaInfo> lstRating = AspxCommonProvider.GetItemRatingCriteria(aspxCommonObj, isFlag);
                return lstRating;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //---------------------save rating/ review Items-----------------------        
        public void SaveItemRating(ItemReviewBasicInfo ratingSaveObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxRatingReviewController.SaveItemRating(ratingSaveObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemRatingAverageInfo> GetItemAverageRating(string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemRatingAverageInfo> avgRating = AspxRatingReviewController.GetItemAverageRating(itemSKU, aspxCommonObj);
                return avgRating;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<AttributeFormInfo> GetItemFormAttributesByitemSKUOnly(string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeFormInfo> frmItemFieldList = AspxItemMgntController.GetItemFormAttributesByItemSKUOnly(itemSKU, aspxCommonObj);
            return frmItemFieldList;
        }

        #region User Tags
        public List<ItemTagsInfo> GetItemTags(string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemTagsInfo> lstItemTags = AspxTagsController.GetItemTags(itemSKU, aspxCommonObj);
                return lstItemTags;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUserOwnTag(string itemTagID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTagsController.DeleteUserOwnTag(itemTagID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddTagsOfItem(string itemSKU, string tags, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTagsController.AddTagsOfItem(itemSKU, tags, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TagDetailsInfo> GetTagsByUserName(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<TagDetailsInfo> lstTagDetail = AspxTagsController.GetTagsByUserName(aspxCommonObj);
                return lstTagDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public bool CheckReviewByIP(int itemID, AspxCommonInfo aspxCommonObj, string userIP)
        {
            bool isReviewExist = AspxRatingReviewController.CheckReviewByIP(itemID, aspxCommonObj, userIP);
            return isReviewExist;
        }

        public bool CheckReviewByUser(int itemID, AspxCommonInfo aspxCommonObj)
        {
            bool isReviewExist = AspxRatingReviewController.CheckReviewByUser(itemID, aspxCommonObj);
            return isReviewExist;
        }

        #region Front Image Gallery
        public List<ItemsInfoSettings> GetItemsImageGalleryInfoBySKU(string itemSKU, AspxCommonInfo aspxCommonObj, string combinationId)
        {
            List<ItemsInfoSettings> itemsInfoContainer = AspxImageGalleryController.GetItemsImageGalleryInfoByItemSKU(itemSKU, aspxCommonObj, combinationId);
            return itemsInfoContainer;
        }

        public List<ImageGalleryItemsInfo> GetItemsImageGalleryInfo(Int32 storeID, Int32 portalID, string userName, string culture) //  not used In Item Detail page
        {
            List<ImageGalleryItemsInfo> itemsInfoList = AspxImageGalleryController.GetItemsImageGalleryList(storeID, portalID, userName, culture);
            return itemsInfoList;
        }

        public List<ImageGalleryItemsInfo> GetItemsGalleryInfo(Int32 storeID, Int32 portalID, string culture) // not  used in ItemDetail Page
        {
            List<ImageGalleryItemsInfo> itemsInfoList = AspxImageGalleryController.GetItemInfoList(storeID, portalID, culture);
            return itemsInfoList;
        }

        public ImageGalleryInfo ReturnSettings(Int32 userModuleID, AspxCommonInfo aspxCommonObj) // not Used In ItemDetail page
        {
            ImageGalleryInfo infoObject = AspxImageGalleryController.GetGallerySettingValues(userModuleID, aspxCommonObj);
            return infoObject;
        }

        public List<int> ReturnDimension(Int32 userModuleID, AspxCommonInfo aspxCommonObj)
        {
            List<int> param = new List<int>();
            ImageGalleryInfo info = new ImageGalleryInfo();
            ImageGallerySqlProvider settings = new ImageGallerySqlProvider();

            info = AspxImageGalleryController.GetGallerySettingValues(userModuleID, aspxCommonObj);
            param.Add(int.Parse(info.ImageWidth));
            param.Add(int.Parse(info.ImageHeight));
            param.Add(int.Parse(info.ThumbWidth));
            param.Add(int.Parse(info.ThumbHeight));
            //param.Add(int.Parse(info.ZoomShown));
            return param;
        }
        #endregion

        #region "For Out of Stock Notification"

        public List<Notification> GetNotificationList(int offset, int limit, GetAllNotificationInfo getAllNotificationObj, AspxCommonInfo aspxCommonObj) // not used In Item Detail Page
        {
            try
            {
                List<Notification> lstNotification = AspxOutStockNotifyController.GetNotificationList(offset, limit, getAllNotificationObj, aspxCommonObj);
                return lstNotification;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteNotification(string notificationID, AspxCommonInfo aspxCommonObj) // not used In Item Detail Page
        {
            try
            {
                AspxOutStockNotifyController.DeleteNotification(notificationID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotifictionMail> GetEmail(string SKU, AspxCommonInfo aspxCommonObj) // not used In Item Detail Page
        {
            try
            {
                List<NotifictionMail> lstNotifMail = AspxOutStockNotifyController.GetEmail(SKU, aspxCommonObj);
                return lstNotifMail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendEmailNotification(SendEmailInfo emailInfo, string VariantId, string VarinatValue, string sku, string ProductUrl, AspxCommonInfo aspxCommonObj) // not used In Item Detail Page
        {
            try
            {
                AspxOutStockNotifyController.SendEmailNotification(emailInfo, VariantId, VarinatValue, sku, ProductUrl, aspxCommonObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertNotification(AspxCommonInfo aspxCommonObj, InsertNotificationInfo insertNotificationObj)
        {
            AspxOutStockNotifyController.InsertNotification(aspxCommonObj, insertNotificationObj);
        }

        public List<Notification> GetAllNotification(AspxCommonInfo aspxCommonObj, InsertNotificationInfo getNotificationObj)
        {
            List<Notification> lstNotification = AspxOutStockNotifyController.GetAllNotification(aspxCommonObj, getNotificationObj);
            return lstNotification;
        }

        #endregion

        #endregion

        #region MyCart

        #region Estimate Shipping Rate
        public List<CountryList> LoadCountry()
        {
            List<CountryList> lstCountry = AspxShipRateController.LoadCountry();
            return lstCountry; ;
        }

        public string GetStateCode(string cCode, string stateName)
        {
            string stateCode = AspxCommonController.GetStateCode(cCode, stateName);
            return stateCode;
        }

        public List<States> GetStatesByCountry(string countryCode)
        {
            List<States> lstState = AspxShipRateController.GetStatesByCountry(countryCode);
            return lstState;
        }

        public List<CommonRateList> GetRate(ItemListDetails itemsDetail)
        {
            List<CommonRateList> lstCommonRate = AspxShipRateController.GetRate(itemsDetail);
            return lstCommonRate;
        }
        #endregion

        #region MiniCart Display
        //----------------------Count my cart items--------------------       
        public int GetCartItemsCount(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int cartItemCount = AspxCommonProvider.GetCartItemsCount(aspxCommonObj);
                return cartItemCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal GetTotalCartItemPrice(AspxCommonInfo aspxCommonObj) // on click of check out from shopping bag
        {
            try
            {
                decimal cartPrice = AspxCommonController.GetTotalCartItemPrice(aspxCommonObj);
                return cartPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


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

        public static List<CartInfo> GetCartDetails(AspxCommonInfo aspxCommonObj)
        {
            try
            {

                List<CartInfo> lstCart = AspxCartProvider.GetCartDetails(aspxCommonObj);
                return lstCart;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet GetCartDetailsDataset(AspxCommonInfo aspxCommonObj)
        {
            try
            {

                DataSet lstCart = AspxCartProvider.GetCartDetailsDataSet(aspxCommonObj);
                return lstCart;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //------------------------------Delete Cart Items--------------------------

        public static void DeleteCartItem(int cartID, int cartItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCartProvider.DeleteCartItem(cartID, cartItemID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion


        #region Check Out Page

        public CartExistInfo CheckCustomerCartExist(AspxCommonInfo aspxCommonObj)
        {
            CartExistInfo objCartExist = AspxCartController.CheckCustomerCartExist(aspxCommonObj);
            return objCartExist;

        }
        public List<CartTaxInfo> GetCartTax(CartDataInfo cartTaxObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CartTaxInfo> lstCartTax = AspxCartController.GetCartTax(cartTaxObj, aspxCommonObj);
                return lstCartTax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetDiscountPriceRule(int cartID, AspxCommonInfo aspxCommonObj, decimal shippingCost)
        {

            try
            {
                string discount = AspxCartController.GetDiscountPriceRule(cartID, aspxCommonObj, shippingCost);
                return discount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static decimal GetDiscountQuantityAmount(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                decimal qtyDiscount = AspxCartController.GetDiscountQuantityAmount(aspxCommonObj);
                return qtyDiscount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CartInfo> GetCartCheckOutDetails(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CartInfo> lstCart = AspxCartController.GetCartCheckOutDetails(aspxCommonObj);
                return lstCart;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CartTaxforOrderInfo> GetCartTaxforOrder(CartDataInfo cartTaxOrderObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CartTaxforOrderInfo> lstCartTaxOrder = AspxCartController.GetCartTaxforOrder(cartTaxOrderObj, aspxCommonObj);
                return lstCartTaxOrder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CartUnitTaxInfo> GetCartUnitTax(CartDataInfo cartUnitTaxObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CartUnitTaxInfo> lstCartUnitTax = AspxCartProvider.GetCartUnitTax(cartUnitTaxObj, aspxCommonObj);
                return lstCartUnitTax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrderItemsInfo> GetAllOrderDetailsForView(int orderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OrderItemsInfo> lstOrderItem = AspxPaymentController.GetAllOrderDetailsForView(orderId, aspxCommonObj);
                return lstOrderItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrderItemsTaxInfo> GetTaxDetailsByOrderID(int orderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OrderItemsTaxInfo> lstOrderItem = AspxPaymentController.GetTaxDetailsByOrderID(orderId, aspxCommonObj);
                return lstOrderItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUpdateUserAddress(AddressInfo addressObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxUserDashController.AddUpdateUserAddress(addressObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AddressInfo> GetAddressBookDetails(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AddressInfo> lstAddress = AspxUserDashController.GetUserAddressDetails(aspxCommonObj);
                return lstAddress;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAddressBook(int addressID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxUserDashController.DeleteAddressBookDetails(addressID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckAddressAlreadyExist(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isExist = AspxCommonController.CheckAddressAlreadyExist(aspxCommonObj);
                return isExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaymentGatewayListInfo> GetPGList(AspxCommonInfo aspxCommonObj)
        {
            List<PaymentGatewayListInfo> pginfo = AspxCartController.GetPGList(aspxCommonObj);
            return pginfo;
        }


        #region CheckOUt Gift Card
        //public int GetGiftCardType(AspxCommonInfo aspxCommonObj, int cartitemId)
        //{
        //    try
        //    {
        //        int strType = AspxGiftCardController.GetGiftCardType(aspxCommonObj, cartitemId);
        //        return strType;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public bool CheckGiftCardUsed(AspxCommonInfo aspxCommonObj, string giftCardCode, decimal amount)
        //{
        //    try
        //    {
        //        bool allowToCheckout = AspxGiftCardController.CheckGiftCardUsed(aspxCommonObj, giftCardCode, amount);
        //        return allowToCheckout;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //} 
        #endregion


        #region CheckOut Email Subscribtion

        public string GetUserBillingEmail(int addressID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string userEmail = AspxCommonController.GetUserBillingEmail(addressID, aspxCommonObj);
                return userEmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveEmailSubscriber(string email, int userModuleID, int portalID, string userName, string clientIP)
        {
            try
            {
                NL_Provider cont = new NL_Provider();
                cont.SaveEmailSubscriber(email, userModuleID, portalID, userName, clientIP);

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<NL_Info> CheckPreviousEmailSubscription(string email)
        {
            try
            {
                NL_Provider cont = new NL_Provider();
                return cont.CheckPreviousEmailSubscription(email);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #endregion

        #region Session Setting/Getting

        public void SetSessionVariableCoupon(string key, int value)
        {
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                value = int.Parse(System.Web.HttpContext.Current.Session[key].ToString()) + 1;
            }
            else
            {
                value = value + 1;
            }

            System.Web.HttpContext.Current.Session[key] = value;
            //  string asdf = System.Web.HttpContext.Current.Session["OrderID"].ToString();
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }


        public void SetSessionVariable(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
            //HttpContext.Current.Session[key] = value;
            //  string asdf = System.Web.HttpContext.Current.Session["OrderID"].ToString();
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }


        public void ClearSessionVariable(string key)
        {
            var keys = key.Split(',');
            for (int i = 0; i < keys.Length; i++)
            {
                var keycode = keys[i];
                if (System.Web.HttpContext.Current.Session[keycode] != null)
                {
                    System.Web.HttpContext.Current.Session.Remove(keycode);
                    // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
                }
            }
        }


        public void ClearALLSessionVariable()
        {
            System.Web.HttpContext.Current.Session.Clear();
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }


        public Decimal GetSessionVariable(string key)
        {
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                string i = System.Web.HttpContext.Current.Session[key].ToString();
                return Convert.ToDecimal(i.ToString());
            }
            else
            {
                return 0;
            }

            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }

        public object GetSessionGiftCard(string key)
        {
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                return System.Web.HttpContext.Current.Session[key];

            }
            else
            {
                return "";
            }
        }

        public string GetSessionCouponCode(string key)
        {
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                string i = System.Web.HttpContext.Current.Session[key].ToString();
                return i;
            }
            else
            {
                return "";
            }
        }

        public string GetSessionVariableCart(string key)
        {
            string val = string.Empty;
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                val = System.Web.HttpContext.Current.Session[key].ToString();

            }
            return val;

            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }
        #endregion

        #region Save Order Details From Success Page

        public int SaveOrderDetails(OrderDetailsCollection orderDetail)
        {
            try
            {
                if (orderDetail.ObjOrderDetails.OrderStatusID == 0)
                    orderDetail.ObjOrderDetails.OrderStatusID = 7;
                int orderID = AddOrderDetails(orderDetail);
                return orderID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public int AddOrderDetails(OrderDetailsCollection orderData)
        {
            SQLHandler sqlH = new SQLHandler();
            SqlTransaction tran;
            tran = (SqlTransaction)sqlH.GetTransaction();
            //AspxCommerceSession sn = new AspxCommerceSession();

            orderData.Coupons = CheckOutSessions.Get<List<CouponSession>>("CouponSession");

            if (orderData.ObjOrderDetails.InvoiceNumber == null || orderData.ObjOrderDetails.InvoiceNumber == "")
            {
                orderData.ObjOrderDetails.InvoiceNumber = DateTime.Now.ToString("yyyyMMddhhmmss");
            }
            try
            {
                int billingAddressID = 0;
                int shippingAddressId = 0;
                int orderID = 0;
                if (orderData.ObjOrderDetails.IsMultipleCheckOut == false)
                {
                    if (int.Parse(orderData.ObjBillingAddressInfo.AddressID) == 0 &&
                        int.Parse(orderData.ObjShippingAddressInfo.AddressID) == 0)
                    {

                        billingAddressID = AspxOrderController.AddBillingAddress(orderData, tran);
                        if (!orderData.ObjOrderDetails.IsShippingAddressRequired)
                        {
                            shippingAddressId = AspxOrderController.AddShippingAddress(orderData, tran);
                        }
                    }
                    else
                    {

                        billingAddressID = AspxOrderController.AddBillingAddress(orderData, tran, int.Parse(orderData.ObjBillingAddressInfo.AddressID));
                        if (!orderData.ObjOrderDetails.IsDownloadable && !orderData.ObjOrderDetails.IsShippingAddressRequired)
                        {
                            shippingAddressId = AspxOrderController.AddShippingAddress(orderData, tran, int.Parse(orderData.ObjShippingAddressInfo.AddressID));
                        }
                    }

                }
                int paymentMethodID = AspxOrderController.AddPaymentInfo(orderData, tran);

                if (billingAddressID > 0)
                {
                    orderID = AspxOrderController.AddOrder(orderData, tran, billingAddressID, paymentMethodID);
                    //sn.SetSessionVariable("OrderID", orderID);
                    SetSessionVariable("OrderID", orderID);
                    orderData.ObjOrderDetails.OrderID = orderID;
                    SetSessionVariable("OrderCollection", orderData);
                }
                else
                {
                    orderID = AspxOrderController.AddOrderWithMultipleCheckOut(orderData, tran, paymentMethodID);

                    //sn.SetSessionVariable("OrderID", orderID);
                    SetSessionVariable("OrderID", orderID);
                    orderData.ObjOrderDetails.OrderID = orderID;
                    SetSessionVariable("OrderCollection", orderData);
                }

                foreach (OrderTaxInfo item in orderData.ObjOrderTaxInfo)
                {
                    int itemID = item.ItemID;
                    int taxManageRuleID = item.TaxManageRuleID;
                    decimal taxSubTotal = item.TaxSubTotal;
                    int storeID = item.StoreID;
                    int portalID = item.PortalID;
                    string addedBy = item.AddedBy;
                    string costVariantValueIDs = item.CostVariantsValueIDs;
                    OrderTaxRuleMapping(itemID, orderID, taxManageRuleID, taxSubTotal, storeID, portalID, addedBy, costVariantValueIDs);
                }

                if (shippingAddressId > 0)
                    AspxOrderController.AddOrderItems(orderData, tran, orderID, shippingAddressId);
                else
                    AspxOrderController.AddOrderItemsList(orderData, tran, orderID);



                //add every paymentgateway
                // GiftCardController.IssueGiftCard(orderData.LstOrderItemsInfo, orderData.ObjCommonInfo.StoreID,
                //                               orderData.ObjCommonInfo.PortalID,orderData.ObjCommonInfo.AddedBy, orderData.ObjCommonInfo.CultureName);

                tran.Commit();
                return orderID;
            }
            catch (SqlException sqlEX)
            {

                throw new ArgumentException(sqlEX.Message);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        public void OrderTaxRuleMapping(int itemID, int orderID, int taxManageRuleID, decimal taxSubTotal, int storeID, int portalID, string addedBy, string costVariantValueIDs)
        {
            try
            {
                AspxPaymentController.OrderTaxRuleMapping(itemID, orderID, taxManageRuleID, taxSubTotal, storeID, portalID, addedBy, costVariantValueIDs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "GroupItems"
        public static ItemCartInfo CheckItemQuantityInCart(int itemID, AspxCommonInfo aspxCommonObj, string itemCostVariantIDs)
        {
            try
            {
                ItemCartInfo itemCartObj = AspxCartProvider.CheckItemQuantityInCart(itemID, aspxCommonObj, itemCostVariantIDs);
                return itemCartObj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<GroupItemsQtyInfo> GetGroupItemQuantityInCart(string itemIDs, AspxCommonInfo aspxCommonObj, string itemCostVariantIDs)
        {
            try
            {
                string UserName = aspxCommonObj.UserName;
                string[] itemArray = itemIDs.Split(',');
                string[] itemCostVariantIDsArray = itemCostVariantIDs.Split(',');
                List<GroupItemsQtyInfo> listItems = new List<GroupItemsQtyInfo>();
                for (int count = 0; count < itemArray.Length; count++)
                {
                    int ItemID;
                    string ItemCostVariant;
                    if ((itemArray[count].Trim() != "") && (itemCostVariantIDsArray[count].Trim() != ""))
                    {
                        ItemID = Convert.ToInt32(itemArray[count]);
                        ItemCostVariant = itemCostVariantIDsArray[count].ToString();
                        ItemCartInfo itemCartObj = AspxCartProvider.CheckItemQuantityInCart(ItemID, aspxCommonObj, ItemCostVariant);
                        listItems.Add(new GroupItemsQtyInfo(ItemID, itemCartObj.ItemQuantityInCart, itemCartObj.UserItemQuantityInCart));
                    }
                }

                return listItems;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #region filter

        public List<Filter> GetShoppingFilter(AspxCommonInfo aspxCommonObj, string categoryName, bool isByCategory)
        {
            List<Filter> lstFilter = AspxFilterController.GetShoppingFilter(aspxCommonObj, categoryName, isByCategory);
            return lstFilter;
        }

        public List<CategoryDetailFilter> GetCategoryDetailFilter(string categorykey, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CategoryDetailFilter> lstCatDetFilter = AspxFilterController.GetCategoryDetailFilter(categorykey, aspxCommonObj);
                return lstCatDetFilter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemBasicDetailsInfo> GetShoppingFilterItemsResult(int offset, int limit, string brandIds, string attributes, decimal priceFrom, decimal priceTo, string categoryName, bool isByCategory, int sortBy, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemBasicDetailsInfo> lstItemBasic = AspxFilterController.GetShoppingFilterItemsResult(offset, limit, brandIds, attributes, priceFrom, priceTo, categoryName, isByCategory, sortBy, aspxCommonObj);
                return lstItemBasic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryDetailFilter> GetAllSubCategoryForFilter(string categorykey, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CategoryDetailFilter> lstCatDet = AspxFilterController.GetAllSubCategoryForFilter(categorykey, aspxCommonObj);
                return lstCatDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BrandItemsInfo> GetAllBrandForCategory(string categorykey, bool isByCategory, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<BrandItemsInfo> lstBrandItem = AspxFilterController.GetAllBrandForCategory(categorykey, isByCategory, aspxCommonObj);
                return lstBrandItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Order Management
        public List<StatusInfo> GetStatusList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StatusInfo> lstStatus = AspxCommonController.GetStatusList(aspxCommonObj);
                return lstStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MyOrderListInfo> GetOrderDetails(int offset, System.Nullable<int> limit, System.Nullable<int> orderStatusName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<MyOrderListInfo> lstMyOrder = AspxOrderController.GetOrderDetails(offset, limit, orderStatusName, aspxCommonObj);
                return lstMyOrder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SaveOrderStatus(AspxCommonInfo aspxCommonObj, int orderStatusID, int orderID)
        {
            try
            {
                bool chkMsg = AspxOrderController.SaveOrderStatus(aspxCommonObj, orderStatusID, orderID);
                return chkMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------Send Email for status update----------------------- 

        public void NotifyOrderStatusUpdate(AspxCommonInfo aspxCommonObj, string receiverEmail, string billingShipping, string itemTable, string additionalFields, string templateName)
        {
            try
            {
                EmailTemplate.SendEmailForOrderStatus(aspxCommonObj, receiverEmail, billingShipping, itemTable, additionalFields, templateName);

                if (additionalFields != null)
                {
                    string[] fields = additionalFields.Split('#');
                    int orderID = Int32.Parse(fields[4]);
                    string orderstatus = (fields[0]);
                    if (orderstatus == "Complete")
                    {
                        AspxGiftCardProvider.NotifyUserForGiftCardActivation(orderID, aspxCommonObj);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------Get Items Involved In Order-----------------------        
        public List<ItemCommonInfo> GetItemsInvolvedInOrder(AspxCommonInfo aspxCommonObj, int orderID)
        {
            try
            {
                List<ItemCommonInfo> lstMyItems = AspxOrderController.GetItemsInvolvedInOrder(aspxCommonObj, orderID);
                return lstMyItems;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region OrderStatusManagement
        //------------------------bind Allorder status name list-------------------------------    

        public List<OrderStatusListInfo> GetAllStatusList(int offset, int limit, AspxCommonInfo aspxCommonObj, string orderStatusName, System.Nullable<bool> isActive)
        {
            try
            {
                List<OrderStatusListInfo> lstOrderStat = AspxOrderStatusMgntController.GetAllStatusList(offset, limit, aspxCommonObj, orderStatusName, isActive);
                return lstOrderStat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrderStatusListInfo> AddUpdateOrderStatus(AspxCommonInfo aspxCommonObj, SaveOrderStatusInfo SaveOrderStatusObj)
        {
            try
            {
                List<OrderStatusListInfo> lstOrderStat = AspxOrderStatusMgntController.AddUpdateOrderStatus(aspxCommonObj, SaveOrderStatusObj);
                return lstOrderStat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrderStatusListInfo GetOrderStatusDetailByOrderStatusID(AspxCommonInfo aspxCommonObj,int OrderStatusID)
        {
            try
            {
                OrderStatusListInfo lstOrderStat = AspxOrderStatusMgntController.GetOrderStatusDetailByOrderStatusID(aspxCommonObj, OrderStatusID);
                return lstOrderStat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOrderStatusByID(int orderStatusID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxOrderStatusMgntController.DeleteOrderStatusByID(orderStatusID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteOrderStatusMultipleSelected(string orderStatusIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxOrderStatusMgntController.DeleteOrderStatusMultipleSelected(orderStatusIDs, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckOrderStatusUniqueness(AspxCommonInfo aspxCommonObj, int orderStatusId, string orderStatusAliasName)
        {
            try
            {
                bool isUnique = AspxOrderStatusMgntController.CheckOrderStatusUniqueness(aspxCommonObj, orderStatusId, orderStatusAliasName);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Invoice Management
        // InvoiceListMAnagement -----------------------get invoice details-----------------------

        public List<InvoiceDetailsInfo> GetInvoiceDetailsList(int offset, System.Nullable<int> limit, InvoiceBasicInfo invoiceObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<InvoiceDetailsInfo> lstInvoiceDetail = AspxInvoiceMgntProvider.GetInvoiceDetailsList(offset, limit, invoiceObj, aspxCommonObj);
                return lstInvoiceDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get Invoice Details      
        public List<InvoiceDetailByorderIDInfo> GetInvoiceDetailsByOrderID(int orderID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<InvoiceDetailByorderIDInfo> info = AspxInvoiceMgntProvider.GetInvoiceDetailsByOrderID(orderID, aspxCommonObj);
                return info;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------Send Email for invoice----------------------- 
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
        #endregion

        #region Shipment Management
        //--ShipmentsListManagement     
        public List<ShipmentsDetailsInfo> GetShipmentsDetails(int offset, System.Nullable<int> limit, ShipmentsBasicinfo shipmentObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShipmentsDetailsInfo> lstShipmentDet = AspxShipMethodMgntController.GetShipmentsDetails(offset, limit, shipmentObj, aspxCommonObj);
                return lstShipmentDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------View Shipments Details--------------------------

        public List<ShipmentsDetailsViewInfo> BindAllShipmentsDetails(int orderID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShipmentsDetailsViewInfo> lstShipDetView = AspxShipMethodMgntController.BindAllShipmentsDetails(orderID, aspxCommonObj);
                return lstShipDetView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Return and Policy

        public decimal CheckItemQuantity(int itemID, AspxCommonInfo aspxCommonObj, string itemCostVariantIDs)
        {
            try
            {
                decimal retValue = AspxUserDashController.CheckItemQuantity(itemID, aspxCommonObj, itemCostVariantIDs);
                return retValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReturnReasonListInfo> BindReturnReasonList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ReturnReasonListInfo> lstRRList = AspxUserDashController.BindReturnReasonList(aspxCommonObj);
                return lstRRList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductStatusListInfo> BindProductStatusList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ProductStatusListInfo> lstPdtStatus = AspxUserDashController.BindProductStatusList(aspxCommonObj);
                return lstPdtStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReturnStatusInfo> GetReturnStatusList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ReturnStatusInfo> lstRetStatus = AspxUserDashController.GetReturnStatusList(aspxCommonObj);
                return lstRetStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReturnActionInfo> GetReturnActionList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ReturnActionInfo> lstRetAction = AspxUserDashController.GetReturnActionList(aspxCommonObj);
                return lstRetAction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MyOrderListForReturnInfo> GetMyOrderListForReturn(int orderID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<MyOrderListForReturnInfo> lstMyOrder = AspxUserDashController.GetMyOrderListForReturn(orderID, aspxCommonObj);
                return lstMyOrder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReturnSaveUpdate(ReturnSaveUpdateInfo ReturnSaveUpdateObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxUserDashController.ReturnSaveUpdate(ReturnSaveUpdateObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReturnUpdate(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxUserDashController.ReturnUpdate(returnDetailObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ReturnSaveComments(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxUserDashController.ReturnSaveComments(returnDetailObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ReturnCommentsInfo> GetMyReturnsComment(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ReturnCommentsInfo> info = AspxUserDashController.GetMyReturnsComment(returnDetailObj, aspxCommonObj);
                return info;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReturnsShippingInfo> GetMyReturnsShippingMethod(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ReturnsShippingInfo> info;
                info = AspxUserDashController.GetMyReturnsShippingMethod(returnDetailObj, aspxCommonObj);
                return info;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RetunReportInfo> GetReturnReport(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, ReturnReportBasicInfo returnReportObj)
        {
            try
            {
                List<RetunReportInfo> lstRtnReport = AspxUserDashController.GetReturnReport(offset, limit, aspxCommonObj, returnReportObj);
                return lstRtnReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReturnShippingAddressSaveUpdate(AddressBasicInfo addressObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxUserDashController.ReturnShippingAddressSaveUpdate(addressObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MyReturnListInfo> GetMyReturnsList(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<MyReturnListInfo> lstMyReturn = AspxUserDashController.GetMyReturnsList(offset, limit, aspxCommonObj);
                return lstMyReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReturnItemsInfo> GetMyReturnsDetails(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ReturnItemsInfo> info = AspxUserDashController.GetMyReturnsDetails(returnDetailObj, aspxCommonObj);
                return info;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReturnDetailsInfo> GetReturnDetails(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, RetunDetailsBasicInfo returnDetailObj)
        {
            try
            {
                List<ReturnDetailsInfo> info = AspxUserDashController.GetReturnDetails(offset, limit, aspxCommonObj, returnDetailObj);
                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReturnSendEmail(AspxCommonInfo aspxCommonObj, SendEmailInfo sendEmailObj)
        {
            try
            {
                EmailTemplate.SendEmailForReturns(aspxCommonObj, sendEmailObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReturnSaveUpdateSettings(AspxCommonInfo aspxCommonObj, int expiresInDays)
        {
            try
            {
                AspxReturnRequestMgntController.ReturnSaveUpdateSettings(aspxCommonObj, expiresInDays);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReturnsSettingsInfo> ReturnGetSettings(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ReturnsSettingsInfo> info = AspxReturnRequestMgntController.ReturnGetSettings(aspxCommonObj);
                return info;
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region "For Brand"

        public List<BrandInfo> GetAllBrandList(int offset, int limit, AspxCommonInfo aspxCommonObj, string brandName)
        {
            try
            {
                List<BrandInfo> lstBrand = AspxBrandController.GetAllBrandList(offset, limit, aspxCommonObj, brandName);
                return lstBrand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BrandInfo> GetAllBrandForItem(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<BrandInfo> lstBrand = AspxBrandController.GetAllBrandForItem(aspxCommonObj);
                return lstBrand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void InsertNewBrand(string prevFilePath, AspxCommonInfo aspxCommonObj, BrandInfo brandInsertObj)
        {
            try
            {
                FileHelperController fileObj = new FileHelperController();
                string uplodedValue = string.Empty;
                string imagePath;
                if (brandInsertObj.BrandImageUrl != null && prevFilePath != brandInsertObj.BrandImageUrl)
                {
                    string tempFolder = @"Upload\temp";
                    uplodedValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, brandInsertObj.BrandImageUrl, @"Modules\AspxCommerce\AspxBrandManagement\uploads\", aspxCommonObj.StoreID, aspxCommonObj, "store_");

                }
                imagePath = uplodedValue.Length > 0 ? uplodedValue : "";
                AspxBrandController.InsertNewBrand(prevFilePath, aspxCommonObj, brandInsertObj, imagePath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteBrand(string BrandID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxBrandController.DeleteBrand(BrandID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BrandInfo> GetBrandByItemID(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<BrandInfo> lstBrand = AspxBrandController.GetBrandByItemID(ItemID, aspxCommonObj);
                return lstBrand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ActivateBrand(int brandID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxBrandController.ActivateBrand(brandID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeActivateBrand(int brandID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxBrandController.DeActivateBrand(brandID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ItemBasicDetailsInfo> GetBrandItemsByBrandID(int offset, int limit, string brandName, int SortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemBasicDetailsInfo> lstItem = AspxBrandController.GetBrandItemsByBrandID(offset, limit, brandName, SortBy, rowTotal, aspxCommonObj);
                return lstItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BrandInfo> GetBrandDetailByBrandID(string brandName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<BrandInfo> lstBrand = AspxBrandController.GetBrandDetailByBrandID(brandName, aspxCommonObj);
                return lstBrand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckBrandUniqueness(string brandName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isUnique = AspxBrandController.CheckBrandUniqueness(brandName, aspxCommonObj);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Search Term Management
        public void AddUpdateSearchTerm(bool? hasData, string searchTerm, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxSearchTermMgntController.AddUpdateSearchTerm(hasData, searchTerm, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SearchTermInfo> ManageSearchTerms(int offset, int? limit, AspxCommonInfo aspxCommonObj, string searchTerm)
        {
            try
            {
                List<SearchTermInfo> lstSearchTerm = AspxSearchTermMgntController.ManageSearchTerm(offset, limit, aspxCommonObj, searchTerm);
                return lstSearchTerm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSearchTerm(string searchTermID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxSearchTermMgntController.DeleteSearchTerm(searchTermID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GiftCard Method


        public bool CheckGiftCardUsed(AspxCommonInfo aspxCommonObj, string giftCardCode, decimal amount)
        {
            try
            {
                bool allowToCheckout = AspxGiftCardController.CheckGiftCardUsed(aspxCommonObj, giftCardCode, amount);
                return allowToCheckout;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GiftCardType> GetGiftCardTypes(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardType> lstGiftCard = AspxGiftCardController.GetGiftCardTypes(aspxCommonObj);
                return lstGiftCard;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetGiftCardType(AspxCommonInfo aspxCommonObj, int cartitemId)
        {
            try
            {
                int strType = AspxGiftCardController.GetGiftCardType(aspxCommonObj, cartitemId);
                return strType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<GiftCardType> GetGiftCardTypeId(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardType> lstGiftCardType = AspxGiftCardController.GetGiftCardTypeId(aspxCommonObj);
                return lstGiftCardType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Vefification VerifyGiftCard(string giftcardCode, string pinCode, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                Vefification objVerify = AspxGiftCardController.VerifyGiftCard(giftcardCode, pinCode, aspxCommonObj);
                return objVerify;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<BalanceInquiry> CheckGiftCardBalance(string giftcardCode, string giftCardPinCode, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<BalanceInquiry> lstBalanceInq = AspxGiftCardController.CheckGiftCardBalance(giftcardCode, giftCardPinCode, aspxCommonObj);
                return lstBalanceInq;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<GiftCardHistory> GetGiftCardHistory(int giftcardId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardHistory> lstGCHistory = AspxGiftCardController.GetGiftCardHistory(giftcardId, aspxCommonObj);
                return lstGCHistory;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GiftCard> GetGiftCardDetailById(int giftcardId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCard> lstGiftCard = AspxGiftCardController.GetGiftCardDetailById(giftcardId, aspxCommonObj);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void SaveGiftCardByAdmin(int giftCardId, GiftCard giftCardDetail, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxGiftCardController.SaveGiftCardByAdmin(giftCardId, giftCardDetail, isActive, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GiftCardGrid> GetAllPaidGiftCard(int offset, int limit, AspxCommonInfo aspxCommonObj, GiftCardDataInfo giftCardDataObj)
        {
            try
            {
                List<GiftCardGrid> ii = AspxGiftCardController.GetAllPaidGiftCard(offset, limit, aspxCommonObj, giftCardDataObj);
                return ii;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteGiftCard(string giftCardId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxGiftCardController.DeleteGiftCard(giftCardId, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteTempFile(string path)
        {
            if (path.Contains("GiftCard_Graphic"))
            {
                var filePath = HttpContext.Current.Server.MapPath("~/" + path);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

        }

        public bool CheckGiftCardCategory(AspxCommonInfo aspxCommonObj, string giftcardCategoryName)
        {
            try
            {
                bool isGiftCard = AspxGiftCardController.CheckGiftCardCategory(aspxCommonObj, giftcardCategoryName);
                return isGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveNewGiftCardCategory(string giftCardGraphicId, AspxCommonInfo aspxCommonObj, string giftcardCategoryName, bool isActive)
        {
            try
            {
                AspxGiftCardController.SaveNewGiftCardCategory(giftCardGraphicId, aspxCommonObj, giftcardCategoryName, isActive);
            }
            catch(Exception e)
            { throw e; }
        }

        public void SaveGiftCardCategory(int giftCardCategoryId, AspxCommonInfo aspxCommonObj, string giftcardCategoryName, bool isActive)
        {
            try
            {
                AspxGiftCardController.SaveGiftCardCategory(giftCardCategoryId, aspxCommonObj, giftcardCategoryName, isActive);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteGiftCardCategory(int giftCardCategoryId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxGiftCardController.DeleteGiftCardCategory(giftCardCategoryId, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteGiftCardThemeImage(int giftCardGraphicId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxGiftCardController.DeleteGiftCardThemeImage(giftCardGraphicId, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string SaveGiftCardItemCategory(int itemId, string ids, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string strValue = AspxGiftCardController.SaveGiftCardItemCategory(itemId, ids, aspxCommonObj);
                return strValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public List<GiftCardInfo> GetGiftCardThemeImagesByItem(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardInfo> lstGiftCard = AspxGiftCardController.GetGiftCardThemeImagesByItem(itemId, aspxCommonObj);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetGiftCardItemCategory(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string strValue = AspxGiftCardController.GetGiftCardItemCategory(itemId, aspxCommonObj);
                return strValue;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GiftCardCategoryInfo> GetAllGiftCardCategoryGrid(int offset, int limit, AspxCommonInfo aspxCommonObj, string categoryName, DateTime? addedon, bool? status)
        {
            try
            {
                List<GiftCardCategoryInfo> lstGCCat = AspxGiftCardController.GetAllGiftCardCategoryGrid(offset, limit, aspxCommonObj, categoryName, addedon, status);
                return lstGCCat;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GiftCardCategoryInfo> GetGiftCardCategoryDetailByID(int categoryID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardCategoryInfo> lstGCCat = AspxGiftCardController.GetGiftCardCategoryDetailByID(categoryID, aspxCommonObj);
                return lstGCCat;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GiftCardInfo> GetAllGiftCardCategory(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardInfo> lstGiftCard = AspxGiftCardController.GetAllGiftCardCategory(aspxCommonObj);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<GiftCardInfo> GetAllGiftCardThemeImageByCategory(int giftCardCategoryId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardInfo> lstGiftCard = AspxGiftCardController.GetAllGiftCardThemeImageByCategory(giftCardCategoryId, aspxCommonObj);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GiftCardInfo> GetAllGiftCardThemeImage(AspxCommonInfo aspxCommonObj, int categoryId)
        {
            try
            {
                List<GiftCardInfo> lstGiftCard = AspxGiftCardController.GetAllGiftCardThemeImage(aspxCommonObj, categoryId);
                return lstGiftCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveGiftCardThemeImage(string graphicThemeName, string graphicImage, int giftCardCategoryId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxGiftCardController.SaveGiftCardThemeImage(graphicThemeName, graphicImage, giftCardCategoryId, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int SaveGiftCardThemeImageReturnGiftCardGraphicId(string graphicThemeName, string graphicImage, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                return AspxGiftCardController.SaveGiftCardThemeImageReturnGiftCardGraphicId(graphicThemeName, graphicImage, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<LatestItemsInfo> GetAllGiftCards(int offset, int limit, int rowTotal, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<LatestItemsInfo> lstGiftItems = AspxItemMgntController.GetAllGiftCards(offset, limit, rowTotal, aspxCommonObj);
                return lstGiftItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Index Management

        public List<IndexManagement> GetIndexedTables(int offset, int limit)
        {
            try
            {
                List<IndexManagement> lstCustDetail = AspxIndexManagement.GetIndexedTables(offset, limit);
                return lstCustDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReBuild(string tableName)
        {
            try
            {
                AspxIndexManagement.ReOrganizeTable(tableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReIndex(string tableName)
        {
            try
            {
                AspxIndexManagement.ReIndexTable(tableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReIndexAllTables()
        {
            try
            {
                AspxIndexManagement.ReIndexAllTables();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CustomerDetails

        public List<CustomerDetailsInfo> GetCustomerDetails(string customerName, int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CustomerDetailsInfo> lstCustDetail = AspxCustomerMgntController.GetCustomerDetails(customerName, offset, limit, aspxCommonObj);
                return lstCustDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CustomerPersonalInfo GetCustomerDetailsByCustomerID(int customerID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                return AspxCustomerMgntController.GetCustomerDetailsByCustomerID(customerID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomerRecentOrders> GetCustomerRecentOrdersByCustomerID(int offset, int limit, int customerID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                return AspxCustomerMgntController.GetCustomerRecentOrdersByCustomerID(offset, limit, customerID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustCartInfo> GetCustomerShoppingCartByCustomerID(int customerID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                return AspxCustomerMgntController.GetCustomerShopingCartByCustomerID(customerID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCustShoppingCartByShopID(string shoppingID, int customerID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCustomerMgntController.DeleteCustShoppingCartByShopID(shoppingID, customerID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomerWishList> GetCustomerWishListByCustomerID(int offset, int limit, int customerID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                return AspxCustomerMgntController.GetCustomerWishListByCustomerID(offset, limit, customerID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCustWishlistByWishID(string wishID, string userName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCustomerMgntController.DeleteCustWishlistByWishID(wishID, userName, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMultipleCustomersByCustomerID(string customerIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCustomerMgntController.DeleteMultipleCustomers(customerIDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCustomerByCustomerID(int customerId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCustomerMgntController.DeleteCustomer(customerId, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CustomerOnlineInfo> CheckIfCustomerExists(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CustomerOnlineInfo> isExistInfo = AspxCustomerMgntController.CheckIfCustomerExists(aspxCommonObj);
                return isExistInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Reportng Module

        #region Customer Reports By Order Total
        //--------------------bind Customer Order Total Roports-------------------------    

        public List<CustomerOrderTotalInfo> GetCustomerOrderTotal(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, string user)
        {
            try
            {
                List<CustomerOrderTotalInfo> lstCustOrderTot = AspxCustomerMgntController.GetCustomerOrderTotal(offset, limit, aspxCommonObj, user);
                return lstCustOrderTot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //--------------- New Account Reports--------------------------

        public List<NewAccountReportInfo> GetNewAccounts(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, bool monthly, bool weekly, bool hourly)
        {
            try
            {
                List<NewAccountReportInfo> lstNewAccounts = AspxCustomerMgntController.GetNewAccounts(offset, limit, aspxCommonObj, monthly, weekly, hourly);
                return lstNewAccounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Online Users
        public List<OnLineUserBaseInfo> GetRegisteredUserOnlineCount(int offset, int limit, string searchUserName, string searchHostAddress, string searchBrowser, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OnLineUserBaseInfo> lstOnlineUser = AspxCustomerMgntController.GetRegisteredUserOnlineCount(offset, limit, searchUserName, searchHostAddress, searchBrowser, aspxCommonObj);
                return lstOnlineUser;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<OnLineUserBaseInfo> GetAnonymousUserOnlineCount(int offset, int limit, string searchHostAddress, string searchBrowser, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OnLineUserBaseInfo> lst = AspxCustomerMgntController.GetAnonymousUserOnlineCount(offset, limit, searchHostAddress, searchBrowser, aspxCommonObj);
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #endregion

        #region Live and Abandoned Cart
        //---------------------bind Abandoned cart details-------------------------

        public List<AbandonedCartInfo> GetAbandonedCartDetails(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, decimal timeToAbandonCart)
        {
            try
            {
                List<AbandonedCartInfo> bind = AspxCartController.GetAbandonedCartDetails(offset, limit, aspxCommonObj, timeToAbandonCart);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ShoppingCartInfo> GetShoppingCartItemsDetails(int offset, System.Nullable<int> limit, string itemName, string quantity, AspxCommonInfo aspxCommonObj, decimal timeToAbandonCart)
        {
            // quantity = quantity == "" ? null : quantity;
            try
            {
                List<ShoppingCartInfo> lstShopCart = AspxCartController.GetShoppingCartItemsDetails(offset, limit, itemName, quantity, aspxCommonObj, timeToAbandonCart);
                return lstShopCart;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Catalog Pricing Rule

        public bool CheckCatalogPriorityUniqueness(int catalogPriceRuleID, int priority, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isUnique = AspxCatalogPriceRuleController.CheckCatalogPriorityUniqueness(catalogPriceRuleID, priority, aspxCommonObj);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PricingRuleAttributeInfo> GetPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<PricingRuleAttributeInfo> lstPriceRuleAttr = AspxCatalogPriceRuleController.GetPricingRuleAttributes(aspxCommonObj);
                return lstPriceRuleAttr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CatalogPriceRulePaging> GetPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            List<CatalogPriceRulePaging> lstCatalogPriceRule = AspxCatalogPriceRuleController.GetCatalogPricingRules(ruleName, startDate, endDate, isActive, aspxCommonObj, offset, limit);
            return lstCatalogPriceRule;
        }

        public CatalogPricingRuleInfo GetPricingRule(Int32 catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
        {
            CatalogPricingRuleInfo catalogPricingRuleInfo;
            catalogPricingRuleInfo = AspxCatalogPriceRuleController.GetCatalogPricingRule(catalogPriceRuleID, aspxCommonObj);
            return catalogPricingRuleInfo;
        }

        //public async void RefreshCatalogPriceRules(CatalogPricingRuleInfo objCatalogPricingRuleInfo, AspxCommonInfo aspxCommonObj, List<int> parentID)
        //{
        //    await SavePricingRuleAsync(objCatalogPricingRuleInfo, aspxCommonObj, parentID);
        //}

        public string SavePricingRule(CatalogPricingRuleInfo objCatalogPricingRuleInfo, AspxCommonInfo aspxCommonObj, List<int> parentID)
        {
            try
            {
                List<KeyValuePair<string, object>> p1 = new List<KeyValuePair<string, object>>();
                p1.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                p1.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                SQLHandler sql = new SQLHandler();
                int count = sql.ExecuteAsScalar<int>("usp_Aspx_CatalogPriceRuleCount", p1);
                int maxAllowed = 3;
                int catalogPriceRuleId = objCatalogPricingRuleInfo.CatalogPriceRule.CatalogPriceRuleID;
                if (catalogPriceRuleId > 0)
                {
                    maxAllowed++;
                }
                if (count < maxAllowed)
                {
                    AspxCatalogPriceRuleController.SaveCatalogPricingRule(objCatalogPricingRuleInfo, aspxCommonObj, parentID);
                    //return "({ \"returnStatus\" : 1 , \"Message\" : \"Saving catalog pricing rule successfully.\" })";
                    return "success";
                }
                else
                {
                    //return "({ \"returnStatus\" : -1 , \"Message\" : \"No more than 3 rules are allowed in Free version of AspxCommerce!\" })";
                    return "notify";
                }
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving catalog pricing rule!\" })";
                }
            }
        }

        public string SaveAndApplyPricingRule(CatalogPricingRuleInfo objCatalogPricingRuleInfo, AspxCommonInfo aspxCommonObj, List<int> parentID)
        {
            try
            {
                List<KeyValuePair<string, object>> p1 = new List<KeyValuePair<string, object>>();
                p1.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                p1.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                SQLHandler sql = new SQLHandler();
                int count = sql.ExecuteAsScalar<int>("usp_Aspx_CatalogPriceRuleCount", p1);
                int maxAllowed = 3;
                int catalogPriceRuleId = objCatalogPricingRuleInfo.CatalogPriceRule.CatalogPriceRuleID;
                if (catalogPriceRuleId > 0)
                {
                    maxAllowed++;
                }
                if (count < maxAllowed)
                {
                    AspxCatalogPriceRuleController.SaveCatalogPricingRule(objCatalogPricingRuleInfo, aspxCommonObj, parentID);
                    AppyCatalogRules(aspxCommonObj);
                    //objCatalog.ApplyCatalogPricingRule(aspxCommonObj);
                    //return "({ \"returnStatus\" : 1 , \"Message\" : \"Saving catalog pricing rule successfully.\" })";
                    return "success";
                }
                else
                {
                    //return "({ \"returnStatus\" : -1 , \"Message\" : \"No more than 3 rules are allowed in Free version of AspxCommerce!\" })";
                    return "notify";
                }
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving catalog pricing rule!\" })";
                }
            }
        }

        //public Task<string> SavePricingRuleAsync(CatalogPricingRuleInfo objCatalogPricingRuleInfo, AspxCommonInfo aspxCommonObj, List<int> parentID)
        //{
        //    Task<string> result = new Task<string>();

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
        //            result = Task<string>.Factory.StartNew(() => "success");
        //            return result;
        //        }
        //        else
        //        {
        //            //return "({ \"returnStatus\" : -1 , \"Message\" : \"No more than 3 rules are allowed in Free version of AspxCommerce!\" })";
        //            result = Task<string>.Factory.StartNew(() => "notify");
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler errHandler = new ErrorHandler();
        //        if (errHandler.LogWCFException(ex))
        //        {
        //            result = Task<string>.Factory.StartNew(() => "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })");
        //            return result;
        //        }
        //        else
        //        {
        //            result = Task<string>.Factory.StartNew(() => "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving catalog pricing rule!\" })");
        //            return result;
        //        }
        //    }
        //}

        public string DeletePricingRule(Int32 catalogPricingRuleID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCatalogPriceRuleController.CatalogPriceRuleDelete(catalogPricingRuleID, aspxCommonObj);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting catalog pricing rule successfully.\" })";
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting catalog pricing rule!\" })";
                }
            }
        }


        public void AppyCatalogRules(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCatalogPriceRuleController objCatalog = new AspxCatalogPriceRuleController();
                catalogThread = new Thread(() => objCatalog.ApplyCatalogPricingRule(aspxCommonObj));
                catalogThread.Name = "CatalagPriceRule";
                catalogThread.Start();               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckCatalogRuleExist(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCatalogPriceRuleController objCatalog = new AspxCatalogPriceRuleController();
                return objCatalog.CheckCatalogRuleExist(aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteMultipleCatPricingRules(string catRulesIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCatalogPriceRuleController.CatalogPriceMultipleRulesDelete(catRulesIds, aspxCommonObj);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting multiple catalog pricing rules successfully.\" })";
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting pricing rule!\" })";
                }
            }
        }
        #endregion

        #region Cart Pricing Rule


        public bool CheckCartPricePriorityUniqueness(int cartPriceRuleID, int priority, int portalID)
        {
            try
            {
                bool isUnique = AspxCartPriceRuleProvider.CheckCartPricePriorityUniqueness(cartPriceRuleID, priority, portalID);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ShippingMethodInfo> GetShippingMethods(System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShippingMethodInfo> lstShipMethod = AspxCartPriceRuleController.GetShippingMethods(isActive, aspxCommonObj);
                return lstShipMethod;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CartPricingRuleAttributeInfo> GetCartPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CartPricingRuleAttributeInfo> lst = AspxCartPriceRuleController.GetCartPricingRuleAttributes(aspxCommonObj);
                return lst;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string SaveCartPricingRule(CartPricingRuleInfo objCartPriceRule, AspxCommonInfo aspxCommonObj, List<int> parentID)
        {
            try
            {
                List<KeyValuePair<string, object>> p1 = new List<KeyValuePair<string, object>>();
                //P1.Add(new KeyValuePair<string,object>("@StoreID", storeID));
                p1.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                SQLHandler sql = new SQLHandler();
                int count = sql.ExecuteAsScalar<int>("usp_Aspx_CartPrincingRuleCount", p1);
                int maxAllowed = 3;
                int cartPriceRuleId = objCartPriceRule.CartPriceRule.CartPriceRuleID;
                if (cartPriceRuleId > 0)
                {
                    maxAllowed++;
                }
                if (count < maxAllowed)
                {

                    AspxCartPriceRuleController.SaveCartPricingRule(objCartPriceRule, aspxCommonObj, parentID);
                    //return "({ \"returnStatus\" : 1 , \"Message\" : \"Saving cart pricing rule successfully.\" })";
                    return "success";
                }
                else
                {
                    //return "({ \"returnStatus\" : -1 , \"Message\" : \"No more than 3 rules are allowed in Free version of AspxCommerce!\" })";
                    return "notify";
                }
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving cart pricing rule!\" })";
                }
            }
        }

        public List<CartPriceRulePaging> GetCartPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            List<CartPriceRulePaging> lstCartPriceRule = AspxCartPriceRuleController.GetCartPricingRules(ruleName, startDate, endDate, isActive, aspxCommonObj, offset, limit);
            return lstCartPriceRule;
        }

        public CartPricingRuleInfo GetCartPricingRule(Int32 cartPriceRuleID, AspxCommonInfo aspxCommonObj)
        {
            CartPricingRuleInfo cartPricingRuleInfo;
            cartPricingRuleInfo = AspxCartPriceRuleController.GetCartPriceRules(cartPriceRuleID, aspxCommonObj);
            return cartPricingRuleInfo;
        }

        public string DeleteCartPricingRule(Int32 cartPricingRuleID, AspxCommonInfo aspxCommonObj)
        {
            try
            {

                AspxCartPriceRuleController.CartPriceRuleDelete(cartPricingRuleID, aspxCommonObj);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting cart pricing rule successfully.\" })";
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting cart pricing rule!\" })";
                }
            }
        }

        public string DeleteMultipleCartPricingRules(string cartRulesIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {

                AspxCartPriceRuleController.CartPriceMultipleRulesDelete(cartRulesIds, aspxCommonObj);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting multiple cart pricing rules successfully.\" })";
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting cart pricing rule!\" })";
                }
            }
        }
        #endregion

        #region Category Management

        public bool CheckUniqueCategoryName(string catName, int catId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isUnique = AspxCategoryManageController.CheckUniqueCategoryName(catName, catId, aspxCommonObj);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsUnique(Int32 storeID, Int32 portalID, Int32 itemID, Int32 attributeID, Int32 attributeType, string attributeValue)
        {
            try
            {
                /*
            1	TextField
            2	TextArea
            3	Date
            4	Boolean
            5	MultipleSelect
            6	DropDown
            7	Price
            8	File
            9	Radio
            10	RadioButtonList
            11	CheckBox
            12	CheckBoxList
             */
                bool isUnique = AspxCategoryManageController.IsUnique(storeID, portalID, itemID, attributeID, attributeType, attributeValue);
                return isUnique;
            }
            catch (Exception e)
            {
                ErrorHandler errHandler = new ErrorHandler();
                errHandler.LogWCFException(e);
                return false;
            }
        }

        public List<AttributeFormInfo> GetCategoryFormAttributes(Int32 categoryID, AspxCommonInfo aspxCommonObj)
        {
            try
            {

                List<AttributeFormInfo> frmFieldList = AspxCategoryManageController.GetCategoryFormAttributes(categoryID, aspxCommonObj);
                return frmFieldList;
            }
            catch (Exception e)
            {
                ErrorHandler errHandler = new ErrorHandler();
                errHandler.LogWCFException(e);
                throw e;
            }
        }

        public List<CategoryInfo> GetCategoryAll(bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CategoryInfo> catList = AspxCategoryManageController.GetCategoryAll(isActive, aspxCommonObj);
                return catList;
            }
            catch (Exception e)
            {
                ErrorHandler errHandler = new ErrorHandler();
                errHandler.LogWCFException(e);
                throw e;
            }
        }

        public List<CategoryAttributeInfo> GetCategoryByCategoryID(Int32 categoryID, AspxCommonInfo aspxCommonObj)
        {
            List<CategoryAttributeInfo> catList = AspxCategoryManageController.GetCategoryByCategoryID(categoryID, aspxCommonObj);
            return catList;
        }

        public string SaveCategory(CategoryInfo.CategorySaveBasicInfo categoryObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int categoryId = AspxCategoryManageController.SaveCategory(categoryObj, aspxCommonObj);
                CacheHelper.Clear("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);
                CacheHelper.Clear("CategoryForSearch" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);
                if (categoryObj.ParentId > 0)
                {
                    return
                        "({\"returnStatus\":1,\"Message\":\"Sub category has been saved successfully.\",\"categoryID\":" +
                        categoryId + "})";
                }
                else
                {
                    return "({\"returnStatus\":1,\"Message\":\"Category has been saved successfully.\",\"categoryID\":" +
                           categoryId + "})";
                }
            }
            catch (Exception e)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(e))
                {
                    return "({\"returnStatus\":-1,\"errorMessage\":'" + e.Message + "'})";
                }
                else
                {
                    return "({\"returnStatus\":-1,\"errorMessage\":\"Error while saving category!\"})";
                }
            }
        }


        public string DeleteCategory(Int32 storeID, Int32 portalID, Int32 categoryID, string userName, string culture)
        {
            try
            {
                AspxCategoryManageController.DeleteCategory(storeID, portalID, categoryID, userName, culture);
                CacheHelper.Clear("CategoryInfo" + storeID.ToString() + portalID.ToString());
                CacheHelper.Clear("CategoryForSearch" + storeID.ToString() + portalID.ToString());
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Category has been deleted successfully.\" })";

            }
            catch (Exception e)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(e))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + e.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting category!\" })";
                }
            }
        }

        public List<CategoryItemInfo> GetCategoryItems(Int32 offset, System.Nullable<int> limit, GetCategoryItemInfo categoryItemsInfo, AspxCommonInfo aspxCommonObj, bool serviceBit)
        {
            try
            {

                List<CategoryItemInfo> listCategoryItem = AspxCategoryManageController.GetCategoryItems(offset, limit, categoryItemsInfo, aspxCommonObj, serviceBit);
                return listCategoryItem;
            }
            catch (Exception e)
            {
                ErrorHandler errHandler = new ErrorHandler();
                errHandler.LogWCFException(e);
                throw e;
            }
        }

        public string GetCategoryCheckedItems(int CategoryID, AspxCommonInfo aspxCommonObj)
        {
            try
            {

                string categoryItem = AspxCategoryManageController.GetCategoryCheckedItems(CategoryID, aspxCommonObj);
                return categoryItem;
            }
            catch (Exception e)
            {
                ErrorHandler errHandler = new ErrorHandler();
                errHandler.LogWCFException(e);
                throw e;
            }
        }

        public string SaveChangesCategoryTree(string categoryIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {

                AspxCategoryManageController.SaveChangesCategoryTree(categoryIDs, aspxCommonObj);
                CacheHelper.Clear("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);
                CacheHelper.Clear("CategoryForSearch" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Category tree saved successfully.\" })";

            }
            catch (Exception e)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(e))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + e.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving category tree!\" })";
                }
            }
        }

        public void ActivateCategory(int categoryID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCategoryManageController.ActivateCategory(categoryID, aspxCommonObj);
                CacheHelper.Clear("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);
                CacheHelper.Clear("CategoryForSearch" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeActivateCategory(int categoryID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCategoryManageController.DeActivateCategory(categoryID, aspxCommonObj);
                CacheHelper.Clear("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);
                CacheHelper.Clear("CategoryForSearch" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Shipping method management
        //---------------------Shipping Reports--------------------       
        public List<ShippedReportInfo> GetShippedDetails(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, ShippedReportBasicInfo ShippedReportObj)
        {
            try
            {
                List<ShippedReportInfo> lstShipReport = AspxShipMethodMgntController.GetShippedDetails(offset, limit, aspxCommonObj, ShippedReportObj);
                return lstShipReport;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool CheckUniquenessForDisplayOrder(AspxCommonInfo aspxCommonObj, int value, int shippingMethodID)
        {
            try
            {
                bool isUnique = AspxShipMethodMgntController.CheckUniquenessForDisplayOrder(aspxCommonObj, value, shippingMethodID);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------Bind Shipping methods In grid-----------------------------

        public List<ShippingMethodInfo> GetStoreProvidersAvailableMethod(int providerId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShippingMethodInfo> lstShipMethod = AspxShipMethodMgntController.GetStoreProvidersAvailableMethod(providerId, aspxCommonObj);
                return lstShipMethod;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ShippingMethodInfoByProvider> GetShippingMethodListByProvider(int offset, int limit, int shippingProviderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShippingMethodInfoByProvider> shipping = AspxShipMethodMgntController.GetShippingMethodsByProvider(offset, limit, shippingProviderId, aspxCommonObj);
                return shipping;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ShippingMethodInfo> GetShippingMethodList(int offset, int limit, ShippingMethodInfoByProvider shippingMethodObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShippingMethodInfo> shipping = AspxShipMethodMgntController.GetShippingMethods(offset, limit, shippingMethodObj, aspxCommonObj);
                return shipping;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------delete multiple shipping methods----------------------

        public void DeleteShippingByShippingMethodID(string shippingMethodIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipMethodMgntController.DeleteShippings(shippingMethodIds, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------bind shipping service list---------------

        public List<ShippingProviderListInfo> GetShippingProviderList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShippingProviderListInfo> lstShipProvider = AspxShipMethodMgntController.GetShippingProviderList(aspxCommonObj);
                return lstShipProvider;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------------SaveAndUpdate shipping methods-------------------

        public void SaveAndUpdateShippingMethods(int shippingMethodID, string shippingMethodName, string prevFilePath, string newFilePath, string alternateText, int displayOrder, string deliveryTime,
                                                 decimal weightLimitFrom, decimal weightLimitTo, int shippingProviderID, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                FileHelperController fileObj = new FileHelperController();
                string uplodedValue = string.Empty;
                if (newFilePath != null && prevFilePath != newFilePath)
                {
                    string tempFolder = @"Upload\temp";
                    uplodedValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, newFilePath, @"Modules\AspxCommerce\AspxShippingManagement\uploads\", shippingMethodID, aspxCommonObj, "ship_");
                }
                else
                {
                    uplodedValue = prevFilePath;
                }
                AspxShipMethodMgntController.SaveAndUpdateShippings(shippingMethodID, shippingMethodName, uplodedValue, alternateText, displayOrder, deliveryTime, weightLimitFrom, weightLimitTo, shippingProviderID, isActive, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------bind Cost dependencies  in Grid--------------------------

        public List<ShippingCostDependencyInfo> GetCostDependenciesListInfo(int offset, int limit, AspxCommonInfo aspxCommonObj, int shippingMethodId)
        {
            try
            {
                List<ShippingCostDependencyInfo> bind = AspxShipMethodMgntController.GetCostDependenciesListInfo(offset, limit, aspxCommonObj, shippingMethodId);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------bind Weight dependencies  in Grid--------------------------

        public List<ShippingWeightDependenciesInfo> GetWeightDependenciesListInfo(int offset, int limit, AspxCommonInfo aspxCommonObj, int shippingMethodId)
        {
            try
            {
                List<ShippingWeightDependenciesInfo> bind = AspxShipMethodMgntController.GetWeightDependenciesListInfo(offset, limit, aspxCommonObj, shippingMethodId);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------bind Item dependencies  in Grid--------------------------

        public List<ShippingItemDependenciesInfo> GetItemDependenciesListInfo(int offset, int limit, AspxCommonInfo aspxCommonObj, int shippingMethodId)
        {
            try
            {
                List<ShippingItemDependenciesInfo> bind = AspxShipMethodMgntController.GetItemDependenciesListInfo(offset, limit, aspxCommonObj, shippingMethodId);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Delete multiple cost Depandencies --------------------------

        public void DeleteCostDependencies(string shippingProductCostIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipMethodMgntController.DeleteCostDependencies(shippingProductCostIds, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Delete multiple weight Depandencies --------------------------

        public void DeleteWeightDependencies(string shippingProductWeightIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipMethodMgntController.DeleteWeightDependencies(shippingProductWeightIds, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Delete multiple item Depandencies --------------------------

        public void DeleteItemDependencies(string shippingItemIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipMethodMgntController.DeleteItemDependencies(shippingItemIds, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------save  cost dependencies----------------

        public void SaveCostDependencies(int shippingProductCostID, int shippingMethodID, string costDependenciesOptions, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipMethodMgntController.AddCostDependencies(shippingProductCostID, shippingMethodID, costDependenciesOptions, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------- save weight dependencies-------------------------------

        public void SaveWeightDependencies(int shippingProductWeightID, int shippingMethodID, string weightDependenciesOptions, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipMethodMgntController.AddWeightDependencies(shippingProductWeightID, shippingMethodID, weightDependenciesOptions, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------- save item dependencies-------------------------------

        public void SaveItemDependencies(int shippingItemID, int shippingMethodID, string itemDependenciesOptions, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipMethodMgntController.AddItemDependencies(shippingItemID, shippingMethodID, itemDependenciesOptions, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Shipping Service Providers management

        public void DeactivateShippingMethod(int shippingMethodId, AspxCommonInfo aspxCommonObj, bool isActive)
        {
            AspxShipProviderMgntController.DeactivateShippingMethod(shippingMethodId, aspxCommonObj, isActive);
        }


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
        public bool CheckShippingProviderUniqueness(AspxCommonInfo aspxCommonObj, int shippingProviderId, string shippingProviderName)
        {
            try
            {
                bool isUnique = AspxShipProviderMgntController.CheckShippingProviderUniqueness(aspxCommonObj, shippingProviderId, shippingProviderName);
                return isUnique;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ShippingProviderNameListInfo> GetShippingProviderNameList(int offset, int limit, AspxCommonInfo aspxCommonObj, string shippingProviderName, System.Nullable<bool> isActive)
        {
            try
            {
                List<ShippingProviderNameListInfo> lstShipProvider = AspxShipProviderMgntController.GetShippingProviderNameList(offset, limit, aspxCommonObj, shippingProviderName, isActive);
                return lstShipProvider;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LoadProviderSetting(int providerId, AspxCommonInfo aspxCommonObj)
        {
            string retStr = AspxShipProviderMgntController.LoadProviderSetting(providerId, aspxCommonObj);
            return retStr;
        }

        public void ShippingProviderAddUpdate(List<ShippingMethod> methods,
            ShippingProvider provider, bool isAddedZip, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipProviderMgntController.ShippingProviderAddUpdate(methods, provider, isAddedZip, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteShippingProviderByID(int shippingProviderID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipProviderMgntController.DeleteShippingProviderByID(shippingProviderID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteShippingProviderMultipleSelected(string shippingProviderIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShipProviderMgntController.DeleteShippingProviderMultipleSelected(shippingProviderIDs, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Ware house

        public List<WareHouse> GetAllWareHouse(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            List<WareHouse> wList = AspxWareHouseController.GetAllWareHouse(offset, limit, aspxCommonObj);
            return wList;
        }

        public List<WareHouseAddress> GetAllWareHouseById(int wareHouseID, AspxCommonInfo aspxCommonObj)
        {
            List<WareHouseAddress> wList = AspxWareHouseController.GetAllWareHouseById(wareHouseID, aspxCommonObj);
            return wList;
        }

        public void DeleteWareHouse(int wareHouseId, AspxCommonInfo aspxCommonObj)
        {
            AspxWareHouseController.DeleteWareHouse(wareHouseId, aspxCommonObj);
        }

        public void AddUpDateWareHouse(WareHouseAddress wareHouse, AspxCommonInfo aspxCommonObj)
        {
            AspxWareHouseController.AddUpDateWareHouse(wareHouse, aspxCommonObj);
        }

        #endregion

        #region Tax management
        //--------------item tax classes------------------

        public List<TaxItemClassInfo> GetTaxItemClassDetails(int offset, int limit, string itemClassName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<TaxItemClassInfo> lstTaxItem = AspxTaxMgntController.GetTaxItemClassDetails(offset, limit, itemClassName, aspxCommonObj);
                return lstTaxItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------save item tax class--------------------

        public void SaveAndUpdateTaxItemClass(int taxItemClassID, string taxItemClassName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTaxMgntController.SaveAndUpdateTaxItemClass(taxItemClassID, taxItemClassName, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckTaxClassUniqueness(AspxCommonInfo aspxCommonObj, string taxItemClassName)
        {
            try
            {
                bool isUnique = AspxTaxMgntController.CheckTaxClassUniqueness(aspxCommonObj, taxItemClassName);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //-----------------Delete tax item classes --------------------------------

        public void DeleteTaxItemClass(string taxItemClassIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTaxMgntController.DeleteTaxItemClass(taxItemClassIDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------customer tax classes------------------

        public List<TaxCustomerClassInfo> GetTaxCustomerClassDetails(int offset, int limit, string className, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<TaxCustomerClassInfo> lstTaxCtClass = AspxTaxMgntController.GetTaxCustomerClassDetails(offset, limit, className, aspxCommonObj);
                return lstTaxCtClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------save customer tax class--------------------

        public void SaveAndUpdateTaxCustmerClass(int taxCustomerClassID, string taxCustomerClassName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTaxMgntController.SaveAndUpdateTaxCustmerClass(taxCustomerClassID, taxCustomerClassName, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------Delete tax customer classes --------------------------------

        public void DeleteTaxCustomerClass(string taxCustomerClassIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTaxMgntController.DeleteTaxCustomerClass(taxCustomerClassIDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------tax rates------------------

        public List<TaxRateInfo> GetTaxRateDetails(int offset, System.Nullable<int> limit, TaxRateDataTnfo taxRateDataObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<TaxRateInfo> lstTaxRate = AspxTaxMgntController.GetTaxRateDetails(offset, limit, taxRateDataObj, aspxCommonObj);
                return lstTaxRate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------- save and update tax rates--------------------------

        public void SaveAndUpdateTaxRates(TaxRateDataTnfo taxRateDataObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTaxMgntController.SaveAndUpdateTaxRates(taxRateDataObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------dalete Tax rates-----------------------

        public void DeleteTaxRates(string taxRateIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTaxMgntController.DeleteTaxRates(taxRateIDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------------get customer class----------------

        public List<TaxManageRulesInfo> GetTaxRules(int offset, int limit, TaxRuleDataInfo taxRuleDataObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<TaxManageRulesInfo> lstTaxManage = AspxTaxMgntController.GetTaxRules(offset, limit, taxRuleDataObj, aspxCommonObj);
                return lstTaxManage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //------------------------bind tax customer class name list-------------------------------

        public List<TaxCustomerClassInfo> GetCustomerTaxClass(int storeID, int portalID)
        {
            try
            {
                List<TaxCustomerClassInfo> lstTaxCtClass = AspxTaxMgntController.GetCustomerTaxClass(storeID, portalID);
                return lstTaxCtClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------bind tax item class name list-------------------------------

        public List<TaxItemClassInfo> GetItemTaxClass(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<TaxItemClassInfo> lstTaxItClass = AspxTaxMgntController.GetItemTaxClass(aspxCommonObj);
                return lstTaxItClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------bind tax rate list-------------------------------

        public List<TaxRateInfo> GetTaxRate(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<TaxRateInfo> lstTaxRate = AspxTaxMgntController.GetTaxRate(aspxCommonObj);
                return lstTaxRate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------save and update tax rules--------------------------------------

        public void SaveAndUpdateTaxRule(TaxRuleDataInfo taxRuleDataObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTaxMgntController.SaveAndUpdateTaxRule(taxRuleDataObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------- delete Tax Rules----------------------------

        public void DeleteTaxManageRules(string taxManageRuleIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTaxMgntController.DeleteTaxManageRules(taxManageRuleIDs, aspxCommonObj);
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public bool CheckTaxUniqueness(AspxCommonInfo aspxCommonObj, int value, int taxRuleID)
        {
            try
            {
                bool isUnique = AspxTaxMgntController.CheckTaxUniqueness(aspxCommonObj, value, taxRuleID);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Sales Tax Report

        public List<StoreTaxesInfo> GetStoreSalesTaxes(int offset, int limit, TaxDateData taxDataObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StoreTaxesInfo> lstStoreTax = AspxTaxMgntController.GetStoreSalesTaxes(offset, limit, taxDataObj, aspxCommonObj);
                return lstStoreTax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Coupon Management

        #region Coupon Type Manage

        public bool CheckCouponTypeUniqueness(AspxCommonInfo aspxCommonObj, int couponTypeId, string couponType)
        {
            try
            {
                bool isUnique = AspxCouponManageController.CheckCouponTypeUniqueness(aspxCommonObj, couponTypeId, couponType);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CouponTypeInfo> GetCouponTypeDetails(int offset, int limit, string couponTypeName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CouponTypeInfo> lstCoupType = AspxCouponManageController.GetCouponTypeDetails(offset, limit, couponTypeName, aspxCommonObj);
                return lstCoupType;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUpdateCouponType(CouponTypeInfo couponTypeObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCouponManageController.AddUpdateCouponType(couponTypeObj, aspxCommonObj);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCouponType(string IDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCouponManageController.DeleteCouponType(IDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Coupon Manage

        public List<CouponInfo> GetCouponDetails(int offset, int limit, GetCouponDetailsInfo couponDetailObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CouponInfo> lstCoupon = AspxCouponManageController.BindAllCouponDetails(offset, limit, couponDetailObj, aspxCommonObj);
                return lstCoupon;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool CheckUniqueCouponCode(string couponCode, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isExists = AspxCouponManageProvider.CheckUniqueCouponCode(couponCode, aspxCommonObj);
                return isExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string AddUpdateCouponDetails(CouponSaveObj couponSaveObj, CouponEmailInfo couponEmailObj, AspxCommonInfo aspxCommonObj)
        {
            string checkMessage = string.Empty;
            try
            {
                try
                {
                    AspxCouponManageController.AddUpdateCoupons(couponSaveObj, aspxCommonObj);
                    checkMessage += "dataSave" + ",";
                }
                catch (Exception)
                {
                    checkMessage += "dataSaveFail" + ",";
                }

                if (checkMessage == "dataSave,")
                {
                    //if (portalUserEmailID != "")
                    if (couponEmailObj.ReceiverEmail != "")
                    {
                        try
                        {
                            // cmSQLProvider.SendCouponCodeEmail(senderEmail, portalUserEmailID, subject, messageBody);
                            AspxCouponManageController.SendCouponCodeEmail(couponEmailObj);
                            checkMessage += "emailSend";
                        }
                        catch (Exception)
                        {
                            checkMessage += "emailSendFail";
                        }
                    }
                    else
                    {
                        checkMessage += "emailIDBlank";
                    }
                }
                else
                {
                    checkMessage += "emailSendFail";
                }

                return checkMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUpdatePromoCodeDetails(PromoCodeSaveObj promoSaveObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCouponManageController.AddUpdatPromoCode(promoSaveObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CouponStatusInfo> GetCouponStatus(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CouponStatusInfo> lstCoupStat = AspxCouponManageController.BindCouponStatus(aspxCommonObj);
                return lstCoupStat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CouponSettingKeyValueInfo> GetSettinKeyValueByCouponID(int couponID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CouponSettingKeyValueInfo> lstCoupKeyVal = AspxCouponManageController.GetCouponSettingKeyValueInfo(couponID, aspxCommonObj);
                return lstCoupKeyVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CouponPortalUserListInfo> GetPortalUsersByCouponID(int offset, int limit, int couponID, string customerName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CouponPortalUserListInfo> lstCoupUser = AspxCouponManageController.GetPortalUsersList(offset, limit, couponID, customerName, aspxCommonObj);
                return lstCoupUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------delete coupons(admin)-----------

        public void DeleteCoupons(string couponIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCouponManageController.DeleteCoupons(couponIDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ////-------------------Verify Coupon Code-----------------------------
        //[WebMethod]
        //public CouponVerificationInfo VerifyCouponCode(decimal totalCost, string couponCode, string itemIds, string cartItemIds, AspxCommonInfo aspxCommonObj, int appliedCount)
        //{
        //    try
        //    {
        //        CouponVerificationInfo objCoupVeri = AspxCouponManageController.VerifyUserCoupon(totalCost, couponCode, itemIds, cartItemIds, aspxCommonObj, appliedCount);
        //        return objCoupVeri;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //--------update wherever necessary after coupon verification is successful----------

        public void UpdateCouponUserRecord(CouponSession coupon, int storeID, int portalID, string userName, int orderID)
        {
            try
            {
                AspxCouponManageController.UpdateCouponUserRecord(coupon, storeID, portalID, userName, orderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Coupons Per Sales Management

        public List<CouponPerSales> GetCouponDetailsPerSales(int offset, int? limit, string couponCode, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CouponPerSales> lstCoupPerSale = AspxCouponManageController.GetCouponDetailsPerSales(offset, limit, couponCode, aspxCommonObj);
                return lstCoupPerSale;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CouponPerSalesViewDetailInfo> GetCouponPerSalesDetailView(int offset, int? limit, CouponPerSalesGetInfo couponPerSaesObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CouponPerSalesViewDetailInfo> lstCoupPSVDetail = AspxCouponManageController.GetCouponPerSalesDetailView(offset, limit, couponPerSaesObj, aspxCommonObj);
                return lstCoupPSVDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Coupon User Management

        public List<CouponUserInfo> GetCouponUserDetails(int offset, int? limit, GetCouponUserDetailInfo couponUserObj, AspxCommonInfo aspxCommonObj, string userName)
        {
            try
            {

                List<CouponUserInfo> lstCoupUser = AspxCouponManageController.GetCouponUserDetails(offset, limit, couponUserObj, aspxCommonObj, userName);
                return lstCoupUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CouponUserListInfo> GetCouponUserList(int offset, int limit, CouponCommonInfo bindCouponUserObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CouponUserListInfo> lstCoupUser = AspxCouponManageController.GetCouponUserList(offset, limit, bindCouponUserObj, aspxCommonObj);
                return lstCoupUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetPromoItemCheckIDs(int CouponID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string lstItems = AspxCouponManageController.GetPromoItemCheckIDs(CouponID, aspxCommonObj);
                return lstItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<PromoItemsInfo> GetPromoItemList(int offset, int limit, int couponTypeId, int couponId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<PromoItemsInfo> lstItems = AspxCouponManageController.GetAllPromoItems(offset, limit, couponTypeId, couponId,
                                                                                            aspxCommonObj);
                return lstItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ItemsForPromoInfo> ItemsForPromoCode(int offset, int limit, AspxCommonInfo aspxCommonObj, string itemName, int? couponId)
        {
            try
            {
                List<ItemsForPromoInfo> lst = AspxCouponManageController.ItemsForPromoCode(offset, limit, aspxCommonObj, itemName, couponId);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCouponUser(string couponUserID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCouponManageController.DeleteCouponUser(couponUserID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCouponUser(int couponUserID, int couponStatusID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCouponManageController.UpdateCouponUser(couponUserID, couponStatusID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Coupon Status Management

        public List<CouponStatusInfo> GetAllCouponStatusList(int offset, int limit, AspxCommonInfo aspxCommonObj, string couponStatusName, System.Nullable<bool> isActive)
        {
            try
            {
                List<CouponStatusInfo> lstCouponStat = AspxCouponStatusMgmtController.GetAllCouponStatusList(offset, limit, aspxCommonObj, couponStatusName, isActive);
                return lstCouponStat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckCouponStatusUniqueness(AspxCommonInfo aspxCommonObj, int couponStatusId, string couponStatusName)
        {
            try
            {
                bool isUnique = AspxCouponStatusMgmtController.CheckCouponStatusUniqueness(aspxCommonObj, couponStatusId, couponStatusName);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CouponStatusInfo> AddUpdateCouponStatus(AspxCommonInfo aspxCommonObj, CouponStatusInfo SaveCouponStatusObj)
        {
            try
            {
                List<CouponStatusInfo> lstCouponStat = AspxCouponStatusMgmtController.AddUpdateCouponStatus(aspxCommonObj, SaveCouponStatusObj);
                return lstCouponStat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Coupon Setting Manage/Admin

        public void DeleteCouponSettingsKey(string settingID, int storeID, int portalID, string userName)
        {
            try
            {
                AspxCouponManageController.DeleteCouponSettingsKey(settingID, storeID, portalID, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CouponSettingKeyInfo> CouponSettingManageKey()
        {
            try
            {
                List<CouponSettingKeyInfo> lstCoupSetting = AspxCouponManageController.CouponSettingManageKey();
                return lstCoupSetting;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUpdateCouponSettingKey(int ID, string settingKey, int validationTypeID, string isActive, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                AspxCouponManageController.AddUpdateCouponSettingKey(ID, settingKey, validationTypeID, isActive, storeID, portalID, cultureName, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Front Coupon Show

        public List<CouponDetailFrontInfo> GetCouponDetailListFront(int count, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CouponDetailFrontInfo> lstCoupDetail = AspxCouponManageController.GetCouponDetailListFront(count, aspxCommonObj);
                return lstCoupDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region PopularTags Module

        #region Status Management
        //------------------Status DropDown-------------------   

        public List<StatusInfo> GetStatus(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StatusInfo> lstStatus = AspxCommonController.GetStatus(aspxCommonObj);
                return lstStatus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        //public void AddTagsOfItem(string itemSKU, string tags, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        AspxTagsController.AddTagsOfItem(itemSKU, tags, aspxCommonObj);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ItemTagsInfo> GetItemTags(string itemSKU, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        List<ItemTagsInfo> lstItemTags = AspxTagsController.GetItemTags(itemSKU, aspxCommonObj);
        //        return lstItemTags;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void DeleteUserOwnTag(string itemTagID, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        AspxTagsController.DeleteUserOwnTag(itemTagID, aspxCommonObj);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void DeleteMultipleTag(string itemTagIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTagsController.DeleteMultipleTag(itemTagIDs, aspxCommonObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TagDetailsInfo> GetTagDetailsListPending(int offset, int limit, string tag, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<TagDetailsInfo> lstTagDetail = AspxTagsController.GetTagDetailsListPending(offset, limit, tag, aspxCommonObj);
                return lstTagDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<TagDetailsInfo> GetTagDetailsList(int offset, int limit, string tag, string tagStatus, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<TagDetailsInfo> lstTagDetail = AspxTagsController.GetTagDetailsList(offset, limit, tag, tagStatus, aspxCommonObj);
                return lstTagDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public List<TagDetailsInfo> GetAllPopularTags(AspxCommonInfo aspxCommonObj, int count)
        //{
        //    try
        //    {
        //        List<TagDetailsInfo> lstTagDetail = AspxTagsController.GetAllPopularTags(aspxCommonObj, count);
        //        return lstTagDetail;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public List<TagDetailsInfo> GetTagsByUserName(AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        List<TagDetailsInfo> lstTagDetail = AspxTagsController.GetTagsByUserName(aspxCommonObj);
        //        return lstTagDetail;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #region Tags Reports
        //---------------------Customer tags------------

        public List<CustomerTagInfo> GetCustomerTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CustomerTagInfo> lstCustomerTag = AspxTagsController.GetCustomerTagDetailsList(offset, limit, aspxCommonObj);
                return lstCustomerTag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Show Customer tags list------------

        public List<ShowCustomerTagsListInfo> ShowCustomerTagList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShowCustomerTagsListInfo> lstCustTag = AspxTagsController.ShowCustomerTagList(offset, limit, aspxCommonObj);
                return lstCustTag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Item tags details------------

        public List<ItemTagsDetailsInfo> GetItemTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemTagsDetailsInfo> lstItemTags = AspxTagsController.GetItemTagDetailsList(offset, limit, aspxCommonObj);
                return lstItemTags;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Show Item tags list------------

        public List<ShowItemTagsListInfo> ShowItemTagList(int offset, System.Nullable<int> limit, int itemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShowItemTagsListInfo> lstShowItemTags = AspxTagsController.ShowItemTagList(offset, limit, itemID, aspxCommonObj);
                return lstShowItemTags;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Popular tags details------------

        public List<PopularTagsInfo> GetPopularTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<PopularTagsInfo> lstPopTags = AspxTagsController.GetPopularTagDetailsList(offset, limit, aspxCommonObj);
                return lstPopTags;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Show Popular tags list------------

        public List<ShowpopulartagsDetailsInfo> ShowPopularTagList(int offset, System.Nullable<int> limit, string tagName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShowpopulartagsDetailsInfo> lstShowPopTag = AspxTagsController.ShowPopularTagList(offset, limit, tagName, aspxCommonObj);
                return lstShowPopTag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset, int limit, string tagIDs, int SortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        List<ItemBasicDetailsInfo> lstItemBasic = AspxTagsController.GetUserTaggedItems(offset, limit, tagIDs, SortBy, rowTotal, aspxCommonObj);
        //        return lstItemBasic;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion
        #endregion

        #region Tags Management

        public void UpdateTag(string itemTagIDs, int? itemId, int statusID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTagsController.UpdateTag(itemTagIDs, itemId, statusID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void DeleteTag(string itemTagIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxTagsController.DeleteTag(itemTagIDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ItemBasicDetailsInfo> GetItemsByMultipleItemID(string itemIDs, string tagName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemBasicDetailsInfo> lstItemBasic = AspxTagsController.GetItemsByMultipleItemID(itemIDs, tagName, aspxCommonObj);
                return lstItemBasic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Rating/Reviews

        #region rating/ review
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

        public void UpdateItemRating(ItemReviewBasicInfo ratingManageObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCommonController.UpdateItemRating(ratingManageObj, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        public List<RatingLatestInfo> GetRecentItemReviewsAndRatings(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<RatingLatestInfo> lstRatingNew = AspxCommonController.GetRecentItemReviewsAndRatings(offset, limit, aspxCommonObj);
                return lstRatingNew;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        public List<UserRatingInformationInfo> GetAllUserReviewsAndRatings(int offset, int limit, UserRatingBasicInfo userRatingObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<UserRatingInformationInfo> bind = AspxRatingReviewController.GetAllUserReviewsAndRatings(offset, limit, userRatingObj, aspxCommonObj);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------list item names in dropdownlist/item rating management---------------------

        public List<ItemsReviewInfo> GetAllItemList(string searchText, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemsReviewInfo> items = AspxRatingReviewController.GetAllItemList(searchText, aspxCommonObj);
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        #endregion

        #region Item Rating Criteria Manage/Admin
        //--------------------Item Rating Criteria Manage/Admin--------------------------

        //public List<ItemRatingCriteriaInfo> ItemRatingCriteriaManage(int offset, int limit, string criteria, System.Nullable<bool> isActive,  AspxCommonInfo aspxCommonObj)
        //{
        public List<ItemRatingCriteriaInfo> ItemRatingCriteriaManage(int offset, int limit, ItemRatingCriteriaInfo itemCriteriaObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemRatingCriteriaInfo> lstRatingCriteria = AspxRatingReviewController.ItemRatingCriteriaManage(offset, limit, itemCriteriaObj, aspxCommonObj);
                return lstRatingCriteria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------- ItemRating Criteria Manage-------------------------------

        public void AddUpdateItemCriteria(ItemRatingCriteriaInfo itemCriteriaObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxRatingReviewController.AddUpdateItemCriteria(itemCriteriaObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------- ItemRating Criteria Manage-------------------------------

        public void DeleteItemRatingCriteria(string IDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxRatingReviewController.DeleteItemRatingCriteria(IDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Rating Reviews Reporting
        //--------------------bind Customer Reviews Roports-------------------------

        public List<CustomerReviewReportsInfo> GetCustomerReviews(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CustomerReviewReportsInfo> bind = AspxRatingReviewController.GetCustomerReviews(offset, limit, aspxCommonObj);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Show All Customer Reviews-------------------------

        public List<UserRatingInformationInfo> GetAllCustomerReviewsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, UserRatingBasicInfo customerReviewObj)
        {
            try
            {
                List<UserRatingInformationInfo> bind = AspxRatingReviewController.GetAllCustomerReviewsList(offset, limit, aspxCommonObj, customerReviewObj);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------Bind User List------------------------------

        public List<UserListInfo> GetUserList(int portalID)
        {
            try
            {
                List<UserListInfo> lstUser = AspxRatingReviewController.GetUserList(portalID);
                return lstUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Item Reviews Reports-------------------------

        public List<ItemReviewsInfo> GetItemReviews(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemReviewsInfo> bind = AspxRatingReviewController.GetItemReviews(offset, limit, aspxCommonObj);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Show All Item Reviews-------------------------

        public List<UserRatingInformationInfo> GetAllItemReviewsList(int offset, System.Nullable<int> limit, UserRatingBasicInfo itemReviewObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<UserRatingInformationInfo> bind = AspxRatingReviewController.GetAllItemReviewsList(offset, limit, itemReviewObj, aspxCommonObj);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Branch Management

        public bool CheckBranchNameUniqueness(AspxCommonInfo aspxCommonObj, int storeBranchId, string storeBranchName)
        {
            try
            {
                bool isUnique = AspxStoreBranchMgntController.CheckBranchNameUniqueness(aspxCommonObj, storeBranchId, storeBranchName);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveAndUpdateStorebranch(string branchName, string branchImage, AspxCommonInfo aspxCommonObj, int storeBranchId)
        {
            try
            {
                AspxStoreBranchMgntController.SaveAndUpdateStorebranch(branchName, branchImage, aspxCommonObj, storeBranchId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BranchDetailsInfo> GetStoreBranchList(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<BranchDetailsInfo> lstBrDetail = AspxStoreBranchMgntController.GetStoreBranchList(offset, limit, aspxCommonObj);
                return lstBrDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteStoreBranches(string storeBranchIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxStoreBranchMgntController.DeleteStoreBranches(storeBranchIds, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Store Locator

        public List<StoreLocatorInfo> GetAllStoresLocation(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StoreLocatorInfo> lstStoreLocate = AspxStoreLocateController.GetAllStoresLocation(aspxCommonObj);
                return lstStoreLocate;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<StoreLocatorInfo> GetLocationsNearBy(double latitude, double longitude, double searchDistance, AspxCommonInfo aspxCommonObj)
        {

            try
            {
                List<StoreLocatorInfo> lstStoreLocate = AspxStoreLocateController.GetLocationsNearBy(latitude, longitude, searchDistance, aspxCommonObj);
                return lstStoreLocate;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateStoreLocation(AspxCommonInfo aspxCommonObj, string storeName, String storeDescription, string streetName, string localityName, string city, string state, string country, string zip, double latitude, double longitude)
        {
            try
            {
                bool retValue = AspxStoreLocateController.UpdateStoreLocation(aspxCommonObj, storeName, storeDescription, streetName, localityName, city, state, country, zip, latitude, longitude);
                return retValue;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public void AddStoreLocatorSettings(string settingKey, string settingValue, string cultureName, int storeID, int portalID, string userName)
        {
            try
            {
                AspxStoreLocateController.AddStoreLocatorSettings(settingKey, settingValue, cultureName, storeID, portalID, userName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Store Access Management

        public bool CheckExisting(AspxCommonInfo aspxCommonObj, int storeAccesskeyId, string accessData)
        {
            try
            {
                bool isUnique = AspxStoreAccessMgntController.CheckExisting(aspxCommonObj, storeAccesskeyId, accessData);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StoreAccessAutocomplete> SearchStoreAccess(string text, int keyID)
        {
            try
            {
                List<StoreAccessAutocomplete> lstStoreAccess = AspxStoreAccessMgntController.SearchStoreAccess(text, keyID);
                return lstStoreAccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveUpdateStoreAccess(int edit, int storeAccessKeyID, string storeAccessData, string reason, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxStoreAccessMgntController.SaveUpdateStoreAccess(edit, storeAccessKeyID, storeAccessData, reason, isActive, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletStoreAccess(int storeAccessID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxStoreAccessMgntController.DeletStoreAccess(storeAccessID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AspxUserList> GetAspxUser(string userName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AspxUserList> lstUser = AspxStoreAccessMgntController.GetAspxUser(userName, aspxCommonObj);
                return lstUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AspxUserList> GetAspxUserEmail(string email, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AspxUserList> lstUserEmail = AspxStoreAccessMgntController.GetAspxUserEmail(email, aspxCommonObj);
                return lstUserEmail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<StoreAccessKey> GetStoreKeyID()
        {
            try
            {
                List<StoreAccessKey> lstStAccessKey = AspxStoreAccessMgntController.GetStoreKeyID();
                return lstStAccessKey;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StoreAccessInfo> LoadStoreAccessCustomer(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntController.LoadStoreAccessCustomer(offset, limit, search, startDate, endDate, status, aspxCommonObj);
                return lstStoreAccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StoreAccessInfo> LoadStoreAccessEmails(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntController.LoadStoreAccessEmails(offset, limit, search, startDate, endDate, status, aspxCommonObj);
                return lstStoreAccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StoreAccessInfo> LoadStoreAccessIPs(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntController.LoadStoreAccessIPs(offset, limit, search, startDate, endDate, status, aspxCommonObj);
                return lstStoreAccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StoreAccessInfo> LoadStoreAccessDomains(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntController.LoadStoreAccessDomains(offset, limit, search, startDate, endDate, status, aspxCommonObj);
                return lstStoreAccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StoreAccessInfo> LoadStoreAccessCreditCards(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntController.LoadStoreAccessCreditCards(offset, limit, search, startDate, endDate, status, aspxCommonObj);
                return lstStoreAccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CardType_Management
        //------------------------bind All CardType name list-------------------------------        

        public List<CardTypeInfo> GetAllCardTypeList(int offset, int limit, AspxCommonInfo aspxCommonObj, string cardTypeName, bool? isActive)
        {
            try
            {
                List<CardTypeInfo> lstCardType = AspxCardTypeController.GetAllCardTypeList(offset, limit, aspxCommonObj, cardTypeName, isActive);
                return lstCardType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CardTypeInfo> AddUpdateCardType(AspxCommonInfo aspxCommonObj, CardTypeSaveInfo cardTypeSaveObj)
        {

            FileHelperController imageObj = new FileHelperController();
            string uploadedFile;

            if (cardTypeSaveObj.NewFilePath != "" && cardTypeSaveObj.PrevFilePath != cardTypeSaveObj.NewFilePath)
            {
                string tempFolder = @"Upload\temp";
                uploadedFile = imageObj.MoveFileToSpecificFolder(tempFolder, cardTypeSaveObj.PrevFilePath,
                                                                 cardTypeSaveObj.NewFilePath,
                                                                 @"Modules\AspxCommerce\AspxCardTypeManagement\uploads\",
                                                                 cardTypeSaveObj.CardTypeID, aspxCommonObj, "cardType_");

            }
            else
            {
                uploadedFile = cardTypeSaveObj.PrevFilePath;
            }
            try
            {
                List<CardTypeInfo> lstCardType = AspxCardTypeController.AddUpdateCardType(aspxCommonObj, cardTypeSaveObj, uploadedFile);
                return lstCardType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteCardTypeByID(int cardTypeID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCardTypeController.DeleteCardTypeByID(cardTypeID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteCardTypeMultipleSelected(string cardTypeIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCardTypeController.DeleteCardTypeMultipleSelected(cardTypeIDs, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Payment Gateway and CheckOUT PROCESS

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
        #endregion

        #region Payment Gateway Installation

        //public void OrderTaxRuleMapping(int itemID, int orderID, int taxManageRuleID, decimal taxSubTotal, int storeID, int portalID, string addedBy, string costVariantValueIDs)
        //{
        //    try
        //    {
        //        AspxPaymentController.OrderTaxRuleMapping(itemID, orderID, taxManageRuleID, taxSubTotal, storeID, portalID, addedBy, costVariantValueIDs);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<PaymentGateWayInfo> GetAllPaymentMethod(int offset, int limit, AspxCommonInfo aspxCommonObj, PaymentGateWayBasicInfo paymentMethodObj)
        {
            try
            {
                List<PaymentGateWayInfo> lstPayGateWay = AspxPaymentController.GetAllPaymentMethod(offset, limit, aspxCommonObj, paymentMethodObj);
                return lstPayGateWay;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TransactionInfoList> GetAllTransactionDetail(AspxCommonInfo aspxCommonObj, int paymentGatewayID, System.Nullable<int> orderID)
        {
            try
            {
                List<TransactionInfoList> lstTransaction = AspxPaymentController.GetAllTransactionDetail(aspxCommonObj, paymentGatewayID, orderID);
                return lstTransaction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePaymentMethod(string paymentGatewayID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxPaymentController.DeletePaymentMethod(paymentGatewayID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePaymentMethod(AspxCommonInfo aspxCommonObj, PaymentGateWayBasicInfo updatePaymentObj)
        {
            try
            {
                AspxPaymentController.UpdatePaymentMethod(aspxCommonObj, updatePaymentObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUpdatePaymentGateWaySettings(int paymentGatewaySettingValueID, int paymentGatewayID, string settingKeys, string settingValues, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxPaymentController.AddUpdatePaymentGateWaySettings(paymentGatewaySettingValueID, paymentGatewayID, settingKeys, settingValues, isActive, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetOrderdetailsByPaymentGatewayIDInfo> GetOrderDetailsbyPayID(int offset, int limit, PaymentGateWayBasicInfo bindOrderObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GetOrderdetailsByPaymentGatewayIDInfo> lstOrderDetail = AspxPaymentController.GetOrderDetailsbyPayID(offset, limit, bindOrderObj, aspxCommonObj);
                return lstOrderDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrderDetailsByOrderIDInfo> GetAllOrderDetailsByOrderID(int orderId, int storeId, int portalId)
        {
            try
            {
                List<OrderDetailsByOrderIDInfo> lstOrderDetail = AspxPaymentController.GetAllOrderDetailsByOrderID(orderId, storeId, portalId);
                return lstOrderDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[WebMethod]
        //public List<OrderItemsInfo> GetAllOrderDetailsForView(int orderId, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        List<OrderItemsInfo> lstOrderItem = AspxPaymentController.GetAllOrderDetailsForView(orderId, aspxCommonObj);
        //        return lstOrderItem;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //[WebMethod]
        //public List<OrderItemsTaxInfo> GetTaxDetailsByOrderID(int orderId, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        List<OrderItemsTaxInfo> lstOrderItem = AspxPaymentController.GetTaxDetailsByOrderID(orderId, aspxCommonObj);
        //        return lstOrderItem;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region "Currency conversion"


        public List<CurrencyInfo> BindCurrencyList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CurrencyInfo> lstCurrency = AspxCurrencyController.BindCurrencyList(aspxCommonObj);
                return lstCurrency;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CurrencyInfo> BindCurrencyAddedLists(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CurrencyInfo> lstCurrency = AspxCurrencyController.BindCurrencyAddedLists(offset, limit, aspxCommonObj);
                return lstCurrency;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CurrencyInfoByCode> GetDetailsByCountryCode(string countryCode, string countryName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CurrencyInfoByCode> lstCountryDetails = AspxCurrencyController.GetDetailsByCountryCode(countryCode, countryName, aspxCommonObj);
                return lstCountryDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertNewCurrency(AspxCommonInfo aspxCommonObj, CurrencyInfo currencyInsertObj)
        {
            try
            {
                AspxCurrencyController.InsertNewCurrency(aspxCommonObj, currencyInsertObj);
                HttpContext.Current.Cache.Remove("AspxCurrencyRate" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool CheckUniquenessForDisplayOrderForCurrency(AspxCommonInfo aspxCommonObj, int value, int currencyID)
        {
            try
            {
                bool isUnique = AspxCurrencyController.CheckUniquenessForDisplayOrderForCurrency(aspxCommonObj, value, currencyID);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckCurrencyCodeUniqueness(AspxCommonInfo aspxCommonObj, string currencyCode, int currencyID)
        {
            try
            {
                bool isUnique = AspxCurrencyController.CheckCurrencyCodeUniqueness(aspxCommonObj, currencyCode, currencyID);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public double GetCurrencyRateOnChange(AspxCommonInfo aspxCommonObj, string from, string to, string region)
        {
            try
            {
                double result = 0.0;
                result = AspxCurrencyProvider.GetRatefromTable(aspxCommonObj, to);
                HttpContext.Current.Session["CurrencyCode"] = to;
                HttpContext.Current.Session["CurrencyRate"] = result;
                HttpContext.Current.Session["Region"] = region;
                return Math.Round(double.Parse(result.ToString()), 4);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public double GetCurrencyRate(string from, string to)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            try
            {
                double result = 0.0;

                result = AspxCommerce.Core.CurrencyConverter.GetRate(from, to);
                HttpContext.Current.Session["CurrencyRate"] = result;
                return result;

            }
            catch (Exception)
            {
                return 1;

            }
        }


        public void RealTimeUpdate(AspxCommonInfo aspxCommonObj)
        {
            List<CurrencyInfo> lstCurrency = AspxCurrencyController.BindCurrencyList(aspxCommonObj);
            int currencyCount = lstCurrency.Count;
            CurrencyInfo DefaultStoreCurrency = lstCurrency.SingleOrDefault(x => x.IsPrimaryForStore == true);
            for (int i = 0; i < currencyCount; i++)
            {
                CurrencyInfo info = lstCurrency[i];
                if (info.CurrencyCode != DefaultStoreCurrency.CurrencyCode)
                {
                    double result = 0.0;
                    result = AspxCommerce.Core.CurrencyConverter.GetRate(DefaultStoreCurrency.CurrencyCode, info.CurrencyCode);
                    AspxCurrencyController.UpdateRealTimeRate(aspxCommonObj, info.CurrencyCode, result);
                    HttpContext.Current.Cache.Remove("AspxCurrencyRate" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString());
                }
            }
        }


        public void SetStorePrimary(AspxCommonInfo aspxCommonObj, string currencyCode)
        {
            try
            {
                AspxCurrencyController.SetStorePrimary(aspxCommonObj, currencyCode);
                HttpContext.Current.Session["CurrencyCode"] = null;
                HttpContext.Current.Session["Region"] = null;
                StoreSettingConfig ssc = new StoreSettingConfig();
                HttpContext.Current.Cache.Remove("AspxStoreSetting" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString());
                HttpContext.Current.Cache.Remove("AspxCurrencyRate" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString());
                ssc.ResetStoreSettingKeys(aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void DeleteMultipleCurrencyByCurrencyID(string currencyIDs, AspxCommonInfo aspxCommonObj)
        {

            AspxCurrencyController.DeleteMultipleCurrencyByCurrencyID(currencyIDs, aspxCommonObj);

        }

        public List<CurrrencyRateInfo> GetCountryCodeRates(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CurrrencyRateInfo> currencyList = AspxCurrencyController.GetCountryCodeRates(aspxCommonObj);
                return currencyList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region "StoreSetings"

        public StoreSettingInfo GetAllStoreSettings(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                StoreSettingInfo DefaultStoreSettings;
                DefaultStoreSettings = AspxStoreSetController.GetAllStoreSettings(aspxCommonObj);
                HttpContext.Current.Session["DefaultStoreSettings"] = DefaultStoreSettings;
                return DefaultStoreSettings;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStoreSettings(string settingKeys, string settingValues, string prevFilePath, string newFilePath, string prevStoreLogoPath, string newStoreLogoPath, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxStoreSetController.UpdateStoreSettings(settingKeys, settingValues, prevFilePath, newFilePath, prevStoreLogoPath, newStoreLogoPath, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DisplayItemsOptionsInfo> BindItemsViewAsList()
        {
            try
            {
                List<DisplayItemsOptionsInfo> bind = AspxStoreSetController.BindItemsViewAsList();
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Items Reporting
        //----------------------GetMostViewedItems----------------------

        public List<MostViewedItemsInfo> GetMostViewedItemsList(int offset, int? limit, string name, string currencySymbol, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<MostViewedItemsInfo> lstMostView = AspxItemMgntController.GetAllMostViewedItems(offset, limit, name, currencySymbol, aspxCommonObj);
                return lstMostView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // --------------------------Get Low Stock Items----------------------------------------------------

        public List<LowStockItemsInfo> GetLowStockItemsList(int offset, int? limit, ItemSmallCommonInfo lowStockObj, AspxCommonInfo aspxCommonObj, int lowStock)
        {
            try
            {
                List<LowStockItemsInfo> lstLowStockItem = AspxItemMgntController.GetAllLowStockItems(offset, limit, lowStockObj, aspxCommonObj, lowStock);
                return lstLowStockItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------------------Get Ordered Items List-----------------------------------

        public List<OrderItemsGroupByItemIDInfo> GetOrderedItemsList(int offset, System.Nullable<int> limit, string name, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OrderItemsGroupByItemIDInfo> lstOrderItem = AspxItemMgntController.GetOrderedItemsList(offset, limit, name, aspxCommonObj);
                return lstOrderItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // --------------------------Get DownLoadable Items----------------------------------------------------

        public List<DownLoadableItemGetInfo> GetDownLoadableItemsList(int offset, int? limit, GetDownloadableItemInfo downloadableObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<DownLoadableItemGetInfo> lstDownItem = AspxItemMgntController.GetDownLoadableItemsList(offset, limit, downloadableObj, aspxCommonObj);
                return lstDownItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // --------------------------Get DownLoadable Items----------------------------------------------------


        public List<GiftCardReport> GetGiftCardReport(int offset, int? limit, GiftCardReport objGiftcard, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GiftCardReport> giftCardReports = AspxGiftCardController.GetGiftCardReport(offset, limit, objGiftcard,
                                                                                                aspxCommonObj);
                return giftCardReports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Cost Variants Management
        //--------------------bind Cost Variants in Grid--------------------------

        public List<CostVariantInfo> GetCostVariants(int offset, int limit, string variantName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CostVariantInfo> bind = AspxCostVarMgntController.GetCostVariants(offset, limit, variantName, aspxCommonObj);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Delete multiple cost variants --------------------------

        public void DeleteMultipleCostVariants(string costVariantIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCostVarMgntController.DeleteMultipleCostVariants(costVariantIDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------ single Cost Variants management------------------------

        public void DeleteSingleCostVariant(string costVariantID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCostVarMgntController.DeleteSingleCostVariant(costVariantID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<AttributesInputTypeInfo> GetCostVariantInputTypeList()
        {
            try
            {
                List<AttributesInputTypeInfo> ml = AspxCostVarMgntController.GetCostVariantInputTypeList();
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------- bind (edit) cost Variant management--------------------

        public List<CostVariantsGetByCostVariantIDInfo> GetCostVariantInfoByCostVariantID(int costVariantID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CostVariantsGetByCostVariantIDInfo> lstCostVar = AspxCostVarMgntController.GetCostVariantInfoByCostVariantID(costVariantID, aspxCommonObj);
                return lstCostVar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------- bind (edit) cost Variant values for cost variant ID --------------------

        public List<CostVariantsvalueInfo> GetCostVariantValuesByCostVariantID(int costVariantID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CostVariantsvalueInfo> lstCVValue = AspxCostVarMgntController.GetCostVariantValuesByCostVariantID(costVariantID, aspxCommonObj);
                return lstCVValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CostVariantsvalueInfo> GetCostVariantValuesByCostVariantIDForAdmin(int costVariantID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CostVariantsvalueInfo> lstCVValue = AspxCostVarMgntController.GetCostVariantValuesByCostVariantIDForAdmin(costVariantID, aspxCommonObj);
                return lstCVValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------Save and update Costvariant options-------------------------

        public void SaveAndUpdateCostVariant(CostVariantsGetByCostVariantIDInfo variantObj, string variantOptions, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxCostVarMgntController.SaveAndUpdateCostVariant(variantObj, variantOptions, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //---------------- Added for unique name check ---------------------

        public bool CheckUniqueCostVariantName(string costVariantName, int costVariantId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isUnique = AspxCostVarMgntController.CheckUniqueCostVariantName(costVariantName, costVariantId, aspxCommonObj);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Admin DashBoard

        public List<CategoryQuantityStatics> GetTopCategoryByItemSell(int top, int day, AspxCommonInfo aspxCommonObj)
        {

            List<CategoryQuantityStatics> lstCat = AspxAdminDashProvider.GetTopCategoryByItemSell(top, day, aspxCommonObj);
            return lstCat;
        }

        public List<CategoryRevenueStatics> GetTopCategoryByHighestRevenue(int top, int day, AspxCommonInfo aspxCommonObj)
        {
            List<CategoryRevenueStatics> lstCat = AspxAdminDashProvider.GetTopCategoryByHighestRevenue(top, day, aspxCommonObj);
            return lstCat;
        }

        public List<VisitorOrderStatics> GetVisitorsOrder(int day, AspxCommonInfo aspxCommonObj)
        {
            List<VisitorOrderStatics> lstVisitor = AspxAdminDashProvider.GetVisitorsOrder(day, aspxCommonObj);
            return lstVisitor;
        }

        public List<VisitorNewAccountStatics> GetVisitorsNewAccount(int day, AspxCommonInfo aspxCommonObj)
        {
            List<VisitorNewAccountStatics> lstVisitor = AspxAdminDashProvider.GetVisitorsNewAccount(day, aspxCommonObj);
            return lstVisitor;
        }

        public List<VisitorNewOrderStatics> GetVisitorsNewOrder(int day, AspxCommonInfo aspxCommonObj)
        {
            List<VisitorNewOrderStatics> lstVisitor = AspxAdminDashProvider.GetVisitorsNewOrder(day, aspxCommonObj);
            return lstVisitor;
        }

        public List<RefundStatics> GetTotalRefund(int day, AspxCommonInfo aspxCommonObj)
        {
            List<RefundStatics> lstRefund = AspxAdminDashProvider.GetTotalRefund(day, aspxCommonObj);
            return lstRefund;
        }


        public List<RefundReasonStatics> GetTopRefundReason(int day, AspxCommonInfo aspxCommonObj)
        {
            List<RefundReasonStatics> lstRefund = AspxAdminDashProvider.GetTopRefundReason(day, aspxCommonObj);
            return lstRefund;
        }

        public List<SearchTermInfo> GetSearchStatistics(int count, string commandName, AspxCommonInfo aspxCommonObj)
        {
            List<SearchTermInfo> lstSearchTerm = AspxCommonController.GetSearchStatistics(count, commandName, aspxCommonObj);
            return lstSearchTerm;
        }


        public List<LatestOrderStaticsInfo> GetLatestOrderItems(int count, AspxCommonInfo aspxCommonObj)
        {
            List<LatestOrderStaticsInfo> lstLOSI = AspxAdminDashController.GetLatestOrderItems(count, aspxCommonObj);
            return lstLOSI;
        }


        public List<MostViewItemInfoAdminDash> GetMostViwedItemAdmindash(int count, AspxCommonInfo aspxCommonObj)
        {
            List<MostViewItemInfoAdminDash> lstMVI = AspxAdminDashController.GetMostViwedItemAdmindash(count, aspxCommonObj);
            return lstMVI;
        }


        public List<StaticOrderStatusAdminDashInfo> GetStaticOrderStatusAdminDash(int day, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<StaticOrderStatusAdminDashInfo> lstSOS = AspxAdminDashController.GetStaticOrderStatusAdminDash(day, aspxCommonObj);
                return lstSOS;

            }

            catch (Exception e)
            {
                throw e;
            }
        }


        public List<TopCustomerOrdererInfo> GetTopCustomerOrderAdmindash(int count, AspxCommonInfo aspxCommonObj)
        {
            List<TopCustomerOrdererInfo> lstTCO = AspxAdminDashController.GetTopCustomerOrderAdmindash(count, aspxCommonObj);
            return lstTCO;
        }

        public List<TotalOrderAmountInfo> GetTotalOrderAmountAdmindash(AspxCommonInfo aspxCommonObj)
        {
            List<TotalOrderAmountInfo> lstTOAmount = AspxAdminDashController.GetTotalOrderAmountAdmindash(aspxCommonObj);
            return lstTOAmount;
        }

        public List<InventoryDetailAdminDashInfo> GetInventoryDetails(int count, AspxCommonInfo aspxCommonObj)
        {
            List<InventoryDetailAdminDashInfo> lstInvDetail = AspxAdminDashController.GetInventoryDetails(count, aspxCommonObj);
            return lstInvDetail;
        }

        public StoreQuickStaticsInfo GetStoreQuickStatics(AspxCommonInfo aspxCommonObj)
        {
            StoreQuickStaticsInfo lstInvDetail = AspxAdminDashController.GetStoreQuickStatics(aspxCommonObj);
            return lstInvDetail;
        }

        #region Admin DashBoard Chart
        //------------------------bind order Chart by last week-------------------------------

        public List<OrderChartInfo> GetOrderChartDetailsByLastWeek(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OrderChartInfo> lstOrderChart = AspxAdminDashController.GetOrderChartDetailsByLastWeek(aspxCommonObj);
                return lstOrderChart;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //------------------------bind order Chart by current month-------------------------------    

        public List<OrderChartInfo> GetOrderChartDetailsBycurentMonth(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OrderChartInfo> lstOrderChart = AspxAdminDashController.GetOrderChartDetailsBycurentMonth(aspxCommonObj);
                return lstOrderChart;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //------------------------bind order Chart by one year-------------------------------    

        public List<OrderChartInfo> GetOrderChartDetailsByOneYear(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OrderChartInfo> lstOrderChart = AspxAdminDashController.GetOrderChartDetailsByOneYear(aspxCommonObj);
                return lstOrderChart;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //------------------------bind order Chart by last 24 hours-------------------------------    

        public List<OrderChartInfo> GetOrderChartDetailsBy24Hours(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OrderChartInfo> lstOrderChart = AspxAdminDashController.GetOrderChartDetailsBy24Hours(aspxCommonObj);
                return lstOrderChart;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #endregion

        #region Attributes Management

        public bool CheckUniqueAttributeName(AttributeBindInfo attrbuteUniqueObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isUnique = AspxItemAttrMgntController.CheckUniqueName(attrbuteUniqueObj, aspxCommonObj);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributesInputTypeInfo> GetAttributesInputTypeList()
        {
            try
            {
                List<AttributesInputTypeInfo> lstAttrInputType = AspxItemAttrMgntController.GetAttributesInputType();
                return lstAttrInputType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributesItemTypeInfo> GetAttributesItemTypeList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AttributesItemTypeInfo> lstAttrItemType = AspxItemAttrMgntController.GetAttributesItemType(aspxCommonObj);
                return lstAttrItemType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributesValidationTypeInfo> GetAttributesValidationTypeList()
        {
            try
            {
                List<AttributesValidationTypeInfo> lstAttrValidType = AspxItemAttrMgntController.GetAttributesValidationType();
                return lstAttrValidType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributesBasicInfo> GetAttributesList(int offset, int limit, AttributeBindInfo attrbuteBindObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AttributesBasicInfo> lstAttrBasic = AspxItemAttrMgntController.GetItemAttributes(offset, limit, attrbuteBindObj, aspxCommonObj);
                return lstAttrBasic;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributesGetByAttributeIdInfo> GetAttributeDetailsByAttributeID(int attributeId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AttributesGetByAttributeIdInfo> lstAttr = AspxItemAttrMgntController.GetAttributesInfoByAttributeID(attributeId, aspxCommonObj);
                return lstAttr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteMultipleAttributesByAttributeID(string attributeIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemAttrMgntController.DeleteMultipleAttributes(attributeIds, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteAttributeByAttributeID(int attributeId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemAttrMgntController.DeleteAttribute(attributeId, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateAttributeIsActiveByAttributeID(int attributeId, AspxCommonInfo aspxCommonObj, bool isActive)
        {
            try
            {
                AspxItemAttrMgntController.UpdateAttributeIsActive(attributeId, aspxCommonObj, isActive);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveUpdateAttribute(AttributesGetByAttributeIdInfo attributeInfo)
        {
            try
            {
                AspxItemAttrMgntController.SaveAttribute(attributeInfo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AttributeFormInfo SaveUpdateAttributeInfo(AttributesGetByAttributeIdInfo attributeInfo, AttributeConfig config)
        {
            try
            {
                return AspxItemAttrMgntController.SaveAttribute(attributeInfo, config);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region AttributeSet Management

        public List<AttributeSetBaseInfo> GetAttributeSetGrid(int offset, int limit, AttributeSetBindInfo AttributeSetBindObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AttributeSetBaseInfo> lstAttrSet = AspxItemAttrMgntController.GetAttributeSetGrid(offset, limit, AttributeSetBindObj, aspxCommonObj);
                return lstAttrSet;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributeSetInfo> GetAttributeSetList(AspxCommonInfo aspxCommonObj)
        {
            try
            {

                List<AttributeSetInfo> lstAttrSet = AspxItemAttrMgntController.GetAttributeSet(aspxCommonObj);
                return lstAttrSet;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int SaveUpdateAttributeSetInfo(AttributeSetInfo attributeSetObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int retValue = AspxItemAttrMgntController.SaveUpdateAttributeSet(attributeSetObj, aspxCommonObj);
                return retValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int CheckAttributeSetUniqueness(AttributeSaveInfo checkUniqueAttrSet, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int retValue = AspxItemAttrMgntController.CheckAttributeSetUniqueName(checkUniqueAttrSet, aspxCommonObj);
                return retValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributeSetGetByAttributeSetIdInfo> GetAttributeSetDetailsByAttributeSetID(int attributeSetId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AttributeSetGetByAttributeSetIdInfo> lstAttrSetDetail = AspxItemAttrMgntController.GetAttributeSetInfoByAttributeSetID(attributeSetId, aspxCommonObj);
                return lstAttrSetDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteAttributeSetByAttributeSetID(int attributeSetId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemAttrMgntController.DeleteAttributeSet(attributeSetId, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateAttributeSetIsActiveByAttributeSetID(int attributeSetId, AspxCommonInfo aspxCommonObj, bool isActive)
        {
            try
            {
                AspxItemAttrMgntController.UpdateAttributeSetIsActive(attributeSetId, aspxCommonObj, isActive);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void SaveUpdateAttributeGroupInfo(AttributeSaveInfo attributeSaveObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemAttrMgntController.UpdateAttributeGroup(attributeSaveObj, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void DeleteAttributeSetGroupByAttributeSetID(AttributeSaveInfo deleteGroupObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemAttrMgntController.DeleteAttributeSetGroup(deleteGroupObj, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributeSetGroupAliasInfo> RenameAttributeSetGroupAliasByGroupID(AttributeSetGroupAliasInfo attributeSetInfoToUpdate)
        {
            try
            {
                List<AttributeSetGroupAliasInfo> lstAttrSetGroup = AspxItemAttrMgntController.RenameAttributeSetGroupAlias(attributeSetInfoToUpdate);
                return lstAttrSetGroup;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteAttributeByAttributeSetID(AttributeSaveInfo deleteGroupObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemAttrMgntController.DeleteAttribute(deleteGroupObj, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Refer-A-Friend
        //-------------------------Save AND SendEmail Messages For Refer-A-Friend----------------

        public void SaveAndSendEmailMessage(AspxCommonInfo aspxCommonObj, ReferToFriendEmailInfo referToFriendObj, WishItemEmailInfo messageBodyDetail)
        {
            try
            {
                AspxReferFriendController.SaveAndSendEmailMessage(aspxCommonObj, referToFriendObj, messageBodyDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------bind Email list in Grid--------------------------

        public List<ReferToFriendInfo> GetAllReferToAFriendEmailList(int offset, int limit, string senderName, string senderEmail, string receiverName, string receiverEmail, string subject, int storeID, int portalID, string userName)
        {
            try
            {
                List<ReferToFriendInfo> bind = AspxReferFriendController.GetAllReferToAFriendEmailList(offset, limit, senderName, senderEmail, receiverName, receiverEmail, subject, storeID, portalID, userName);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------Delete Email list --------------------------------

        public void DeleteReferToFriendEmailUser(string emailAFriendIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxReferFriendController.DeleteReferToFriendEmailUser(emailAFriendIDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Get UserReferred Friends--------------------------
        //[WebMethod]
        //public List<ReferToFriendInfo> GetUserReferredFriends(int offset, int limit, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        List<ReferToFriendInfo> lstReferFriend = AspxReferFriendController.GetUserReferredFriends(offset, limit, aspxCommonObj);
        //        return lstReferFriend;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region Items Management


        public bool CheckKitComponentExist(string ComponentName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxKitController controller = new AspxKitController();
                bool isUnique = controller.CheckKitComponentExist(ComponentName, aspxCommonObj);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteKit(string kitIds, AspxCommonInfo commonInfo)
        {
            try
            {

                AspxKitController controller = new AspxKitController();
                controller.DeleteKit(kitIds, commonInfo);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void DeleteKitComponent(string kitComponentIds, AspxCommonInfo commonInfo)
        {
            try
            {
                AspxKitController controller = new AspxKitController();
                controller.DeleteKitComponent(kitComponentIds, commonInfo);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<KitInfo> GetKitsGrid(int offset, int limit, string kitname, AspxCommonInfo commonInfo)
        {
            try
            {

                AspxKitController controller = new AspxKitController();
                return controller.LoadKits(offset, limit, kitname, commonInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public List<ItemKit> GetItemKits(int itemID, AspxCommonInfo commonInfo)
        {

            try
            {
                AspxKitController controller = new AspxKitController();
                return controller.GetItemKits(itemID, commonInfo);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void SaveItemKits(List<ItemKit> mappedKits, int itemID, AspxCommonInfo commonInfo)
        {
            try
            {
                AspxKitController controller = new AspxKitController();
                controller.SaveItemKits(mappedKits, itemID, commonInfo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public int SaveComponent(KitComponent kitcomponent, AspxCommonInfo commonInfo)
        {

            try
            {
                AspxKitController controller = new AspxKitController();
                return controller.SaveComponent(kitcomponent, commonInfo);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public int SaveKit(Kit kit, AspxCommonInfo commonInfo)
        {

            try
            {
                AspxKitController controller = new AspxKitController();
                return controller.SaveKit(kit, commonInfo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<KitComponent> GetComponents(AspxCommonInfo commonInfo)
        {
            try
            {
                AspxKitController controller = new AspxKitController();
                return controller.GetComponents(commonInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Kit> GetKits(AspxCommonInfo commonInfo)
        {
            try
            {
                AspxKitController controller = new AspxKitController();
                return controller.GetKits(commonInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItemSetting GetItemSetting(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ItemSetting lstItemSetting = AspxItemMgntController.GetItemSetting(ItemID, aspxCommonObj);
                return lstItemSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ItemPriceGroupInfo> GetItemGroupPrices(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemPriceGroupInfo> lstGroupPrice = AspxItemMgntController.GetItemGroupPrices(ItemID, aspxCommonObj);
                return lstGroupPrice;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ItemsInfo> GetItemsList(int offset, int limit, GetItemListInfo getItemListObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemsInfo> ml;
                ml = AspxItemMgntController.GetAllItems(offset, limit, getItemListObj, aspxCommonObj);
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMultipleItemsByItemID(string itemIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntController.DeleteMultipleItems(itemIds, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteItemByItemID(string itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntController.DeleteSingleItem(itemId, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AttributeFormInfo> GetItemFormAttributes(int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeFormInfo> formAttributeList;
            formAttributeList = AspxItemMgntController.GetItemFormAttributes(attributeSetID, itemTypeID, aspxCommonObj);
            return formAttributeList;
        }

        //[WebMethod]
        //public List<AttributeFormInfo> GetItemFormAttributesByitemSKUOnly(string itemSKU, AspxCommonInfo aspxCommonObj)
        //{
        //    List<AttributeFormInfo> frmItemFieldList = AspxItemMgntController.GetItemFormAttributesByItemSKUOnly(itemSKU, aspxCommonObj);
        //    return frmItemFieldList;
        //}

        public List<AttributeFormInfo> GetItemFormAttributesValuesByItemID(int itemID, int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeFormInfo> frmItemAttributes = AspxItemMgntController.GetItemAttributesValuesByItemID(itemID, attributeSetID, itemTypeID, aspxCommonObj);
            return frmItemAttributes;
        }

        public int SaveItemAndAttributes(ItemsInfo.ItemSaveBasicInfo itemObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string uplodedDownlodableFormValue = string.Empty;

                if (itemObj.ItemTypeId == 2 && itemObj.DownloadItemsValue != "")
                {
                    FileHelperController downLoadableObj = new FileHelperController();
                    string tempFolder = @"Upload\temp";
                    uplodedDownlodableFormValue = downLoadableObj.MoveFileToDownlodableItemFolder(tempFolder,
                                                                                                  itemObj.DownloadItemsValue,
                                                                                                  @"Modules/AspxCommerce/AspxItemsManagement/DownloadableItems/",
                                                                                                  itemObj.ItemId, "item_");
                    itemObj.DownloadItemsValue = uplodedDownlodableFormValue;
                }


                int itemID = AspxItemMgntController.SaveUpdateItemAndAttributes(itemObj, aspxCommonObj);

                //kit produtct
                if (itemObj.ItemTypeId == 6)
                {

                    AspxKitController _kitCtl = new AspxKitController();
                    _kitCtl.SaveKits(itemObj.KitConfig, itemID, aspxCommonObj);
                }
                //return "({\"returnStatus\":1,\"Message\":'Item saved successfully.'})";
                int storeId = aspxCommonObj.StoreID;
                int portalId = aspxCommonObj.PortalID;
                string culture = aspxCommonObj.CultureName;
                // if (itemID > 0 && sourceFileCol != "" && dataCollection != "")
                if (itemID > 0 && itemObj.SourceFileCol != "" && itemObj.DataCollection != "")
                {
                    StoreSettingConfig ssc = new StoreSettingConfig();
                    int itemLargeThumbNailHeight = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageHeight, storeId, portalId, culture));
                    int itemLargeThumbNailWidth = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageWidth, storeId, portalId, culture));
                    int itemMediumThumbNailHeight = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageHeight, storeId, portalId, culture));
                    int itemMediumThumbNailWidth = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageWidth, storeId, portalId, culture));
                    int itemSmallThumbNailHeight = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageHeight, storeId, portalId, culture));
                    int itemSmallThumbNailWidth = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageWidth, storeId, portalId, culture));
                    var dataCollection = itemObj.DataCollection;
                    dataCollection = dataCollection.Replace("../", "");
                    SaveImageContents(itemID, @"Modules/AspxCommerce/AspxItemsManagement/uploads/", itemObj.SourceFileCol,
                                      dataCollection, itemLargeThumbNailHeight, itemLargeThumbNailWidth, itemMediumThumbNailHeight, itemMediumThumbNailWidth,
                                      itemSmallThumbNailHeight, itemSmallThumbNailWidth, "item_", aspxCommonObj.CultureName, aspxCommonObj.PortalID);
                }
                else if (itemID > 0 && itemObj.SourceFileCol == "" && itemObj.DataCollection == "")
                {
                    DeleteImageContents(itemID);
                }
                return itemID;
                //if (itemID == 0)
                //{
                //    //SaveImageContents(itemID, @"Modules/AspxCommerce/AspxItemsManagement/uploads/", sourceFileCol, dataCollection, "item_");
                //    //TODO:: DELTE UPLOADED FILE FROM DOWNLOAD FOLDER

                //}
            }
            catch (Exception ex)
            {
                throw ex;
                //ErrorHandler errHandler = new ErrorHandler();
                //if (errHandler.LogWCFException(ex))
                //{
                //    return "({\"returnStatus\":-1,\"errorMessage\":'" + ex.Message + "'})";
                //}
                //else
                //{
                //    return "({\"returnStatus\":-1,\"errorMessage\":'Error while saving item!'})";
                //}
            }
        }

        public void UpdateItemIsActiveByItemID(int itemId, AspxCommonInfo aspxCommonObj, bool isActive)
        {
            try
            {
                AspxItemMgntController.UpdateItemIsActive(itemId, aspxCommonObj, isActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool CheckUniqueAttributeName(AttributeBindInfo attrbuteUniqueObj, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        bool isUnique = AspxItemAttrMgntController.CheckUniqueName(attrbuteUniqueObj, aspxCommonObj);
        //        return isUnique;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public List<CategoryInfo> GetCategoryList(string prefix, bool isActive, AspxCommonInfo aspxCommonObj, int itemId, bool serviceBit)
        {
            List<CategoryInfo> catList = AspxItemMgntController.GetCategoryList(prefix, isActive, aspxCommonObj, itemId, serviceBit);
            return catList;
        }

        public bool CheckIsItemInGroupItem(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isUnique = AspxItemMgntController.CheckIsItemInGroupItem(ItemID, aspxCommonObj);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckUniqueItemSKUCode(string SKU, int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isUnique = AspxItemMgntController.CheckUniqueSKUCode(SKU, itemId, aspxCommonObj);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Multiple Image Uploader

        public string SaveImageContents(int itemID, string imageRootPath, string sourceFileCol, string dataCollection, int itemLargeThumbNailHeight, int itemLargeThumbNailWidth, int itemMediumThumbNailHeight, int itemMediumThumbNailWidth,
                                     int itemSmallThumbNailHeight, int itemSmallThumbNailWidth, string imgPreFix, string cultureName, int portalID)
        {

            if (dataCollection.Contains("#"))
            {
                dataCollection = dataCollection.Remove(dataCollection.LastIndexOf("#"));
            }
            SQLHandler sageSql = new SQLHandler();
            string[] individualRow = dataCollection.Split('#');
            string[] words;

            StringBuilder sbPathList = new StringBuilder();
            StringBuilder sbIsActiveList = new StringBuilder();
            StringBuilder sbImageType = new StringBuilder();
            StringBuilder sbDescription = new StringBuilder();
            StringBuilder sbDisplayOrder = new StringBuilder();
            StringBuilder sbSourcePathList = new StringBuilder();
            StringBuilder sbItemImageId = new StringBuilder();

            foreach (string str in individualRow)
            {
                words = str.Split('%');
                sbPathList.Append(words[0] + "%");
                sbIsActiveList.Append(words[1] + "%");
                sbImageType.Append(words[2] + "%");
                sbDescription.Append(words[3] + "%");
                sbDisplayOrder.Append(words[4] + "%");
                sbItemImageId.Append(words[5] + "%");
            }
            string pathList = string.Empty;
            string isActive = string.Empty;
            string imageType = string.Empty;
            string description = string.Empty;
            string displayOrder = string.Empty;
            string itemImageIds = string.Empty;

            pathList = sbPathList.ToString();
            isActive = sbIsActiveList.ToString();
            imageType = sbImageType.ToString();
            description = sbDescription.ToString();
            displayOrder = sbDisplayOrder.ToString();
            itemImageIds = sbItemImageId.ToString();

            if (pathList.Contains("%"))
            {
                pathList = pathList.Remove(pathList.LastIndexOf("%"));
            }
            if (isActive.Contains("%"))
            {
                isActive = isActive.Remove(isActive.LastIndexOf("%"));
            }
            if (imageType.Contains("%"))
            {
                imageType = imageType.Remove(imageType.LastIndexOf("%"));
            }
            if (itemImageIds.Contains("%"))
            {
                itemImageIds = itemImageIds.Remove(itemImageIds.LastIndexOf("%"));
            }

            if (sourceFileCol.Contains("%"))
            {
                sourceFileCol = sourceFileCol.Remove(sourceFileCol.LastIndexOf("%"));
            }

            try
            {
                FileHelperController fhc = new FileHelperController();
                //TODO:: delete all previous files infos lists
                fhc.FileMover(itemID, imageRootPath, sourceFileCol, pathList, isActive, imageType, itemImageIds, description, displayOrder, imgPreFix, itemLargeThumbNailHeight, itemLargeThumbNailWidth, itemMediumThumbNailHeight, itemMediumThumbNailWidth,
                                      itemSmallThumbNailHeight, itemSmallThumbNailWidth, cultureName, portalID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";

        }

        public List<ItemsInfoSettings> GetImageContents(int itemID, AspxCommonInfo aspxCommonObj)
        {
            List<ItemsInfoSettings> itemsImages = AspxImageGalleryController.GetItemsImageGalleryInfoByItemID(itemID, aspxCommonObj);
            return itemsImages;
        }

        public void DeleteImageContents(Int32 itemID)
        {
            try
            {
                AspxItemMgntController.DeleteItemImageByItemID(itemID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Related, Cross Sell, Up sell Items

        public List<ItemsInfo> GetAssociatedItemsList(int offset, int limit, ItemDetailsCommonInfo IDCommonObj, int categoryID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemsInfo> ml;
                ml = AspxItemMgntController.GetAssociatedItemsByItemID(offset, limit, IDCommonObj, categoryID, aspxCommonObj);
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemsInfo> GetRelatedItemsList(int offset, int limit, ItemDetailsCommonInfo IDCommonObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemsInfo> ml;
                ml = AspxItemMgntController.GetRelatedItemsByItemID(offset, limit, IDCommonObj, aspxCommonObj);
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemsInfo> GetUpSellItemsList(int offset, int limit, ItemDetailsCommonInfo UpSellCommonObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemsInfo> ml;
                ml = AspxItemMgntController.GetUpSellItemsByItemID(offset, limit, UpSellCommonObj, aspxCommonObj);
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemsInfo> GetCrossSellItemsList(int offset, int limit, ItemDetailsCommonInfo CrossSellCommonObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemsInfo> ml;
                ml = AspxItemMgntController.GetCrossSellItemsByItemID(offset, limit, CrossSellCommonObj, aspxCommonObj);
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetAssociatedCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string ml;
                ml = AspxItemMgntController.GetAssociatedCheckIDs(ItemID, aspxCommonObj);
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetRelatedCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string ml;
                ml = AspxItemMgntController.GetRelatedCheckIDs(ItemID, aspxCommonObj);
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetUpSellCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string ml;
                ml = AspxItemMgntController.GetUpSellCheckIDs(ItemID, aspxCommonObj);
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCrossSellCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string ml;
                ml = AspxItemMgntController.GetCrossSellCheckIDs(ItemID, aspxCommonObj);
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Item Cost Variants Management

        public List<CostVariantInfo> GetCostVariantsOptionsList(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CostVariantInfo> lstCostVar = AspxItemMgntController.GetAllCostVariantOptions(itemId, aspxCommonObj);
                return lstCostVar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------ delete Item Cost Variants management------------------------    

        public void DeleteSingleItemCostVariant(string itemCostVariantID, int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntController.DeleteSingleItemCostVariant(itemCostVariantID, itemId, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //#region NeW CostVariant Combination
        //[WebMethod]
        //public List<VariantCombination> GetCostVariantCombinationbyItemSku(string itemSku, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        List<VariantCombination> lstVarCom = AspxItemMgntController.GetCostVariantCombinationbyItemSku(itemSku, aspxCommonObj);
        //        return lstVarCom;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[WebMethod]
        //public List<ItemCostVariantsInfo> GetCostVariantsByItemSKU(string itemSku, AspxCommonInfo aspxCommonObj)
        //{
        //    try
        //    {
        //        List<ItemCostVariantsInfo> lstItemCostVar = AspxItemMgntController.GetCostVariantsByItemSKU(itemSku, aspxCommonObj);
        //        return lstItemCostVar;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

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
        #endregion

        #region Item Management Setting
        public ItemTabSettingInfo ItemTabSettingGet(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ItemTabSettingInfo lstItem = AspxItemMgntController.ItemTabSettingGet(aspxCommonObj);
                return lstItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ItemTabSettingSave(string SettingKeys, string SettingValues, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntController.ItemTabSettingSave(SettingKeys, SettingValues, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Item Tax Class Name
        public List<TaxItemClassInfo> GetAllTaxItemClass(AspxCommonInfo aspxCommonObj, bool isActive)
        {
            try
            {

                List<TaxItemClassInfo> lstTaxItemClass = AspxItemMgntController.GetAllTaxItemClass(aspxCommonObj, isActive);
                return lstTaxItemClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Quantity Discount Management

        public List<ItemQuantityDiscountInfo> GetItemQuantityDiscountsByItemID(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemQuantityDiscountInfo> lstIQtyDiscount = AspxQtyDiscountMgntController.GetItemQuantityDiscountsByItemID(itemId, aspxCommonObj);
                return lstIQtyDiscount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------save quantity discount------------------

        public void SaveItemDiscountQuantity(string discountQuantity, int itemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxQtyDiscountMgntController.SaveItemDiscountQuantity(discountQuantity, itemID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------delete quantity discount------------------

        public void DeleteItemQuantityDiscount(int quantityDiscountID, int itemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxQtyDiscountMgntController.DeleteItemQuantityDiscount(quantityDiscountID, itemID, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------quantity discount shown in Item deatils ------------------

        public List<ItemQuantityDiscountInfo> GetItemQuantityDiscountByUserName(AspxCommonInfo aspxCommonObj, string itemSKU)
        {
            try
            {
                List<ItemQuantityDiscountInfo> lstIQtyDiscount = AspxQtyDiscountMgntController.GetItemQuantityDiscountByUserName(aspxCommonObj, itemSKU);
                return lstIQtyDiscount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "For Item Videos"

        public List<ItemsInfo.ItemSaveBasicInfo> GetItemVideoContents(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemsInfo.ItemSaveBasicInfo> lstItemVideo = AspxItemMgntController.GetItemVideoContents(ItemID, aspxCommonObj);
                return lstItemVideo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Downloadable Item Details
        public List<DownLoadableItemInfo> GetDownloadableItem(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<DownLoadableItemInfo> lstDownItem = AspxItemMgntController.GetDownloadableItem(itemId, aspxCommonObj);
                return lstDownItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion



    }
}
