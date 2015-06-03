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
using System.Collections.Generic;
using SageFrame.Web.Utilities;
using SageFrame.Pages;

namespace AspxCommerce.Core
{
    public class StoreSetting
    {
        public static string DefaultProductImageURL = "DefaultProductImageURL";
        public static string MyAccountURL = "MyAccountURL";
        public static string ShoppingCartURL = "ShoppingCartURL";        
        public static string SearchResultURL = "SearchResultURL";
        public static string DetailPageURL = "DetailPageURL";
        public static string ItemDetailURL = "ItemDetailURL";
        public static string CategoryDetailURL = "CategoryDetailURL";        
        public static string SingleCheckOutURL = "SingleCheckOutURL";
        public static string MultiCheckOutURL = "MultiCheckOutURL";       
        public static string StoreLocatorURL = "StoreLocatorURL";        
        public static string RssFeedURL = "RssFeedURL";        
        public static string ShipDetailPageURL = "ShipDetailPageURL";
        public static string ItemMgntPageURL = "ItemMgntPageURL";
        public static string CategoryMgntPageURL = "CategoryMgntPageURL";        
        public static string MainCurrency = "MainCurrency";
        public static string AllowRealTimeNotifications = "AllowRealTimeNotifications";
        public static string DefaultCountry = "DefaultCountry";
        public static string StoreName = "StoreName";
        public static string StoreLogoURL = "StoreLogoURL";
        public static string StoreClosePageContent = "StoreClosePageContent";
        public static string StoreClosed = "StoreClosed";
        public static string StoreNOTAccessPageContent = "StoreNOTAccessPageContent";
        public static string TimeToDeleteAbandonedCart = "TimeToDeleteAbandonedCart";
        public static string CartAbandonedTime = "CartAbandonedTime";
        public static string AllowAnonymousCheckOut = "AllowAnonymousCheckOut";
        public static string AllowMultipleShippingAddress = "AllowMultipleShippingAddress";
        public static string SendEcommerceEmailsFrom = "SendEcommerceEmailsFrom";
        public static string SendEcommerceEmailTo = "SendEcommerceEmailTo";
        public static string SendOrderNotification = "SendOrderNotification";
        public static string SendPaymentNotification = "SendPaymentNotification";
        public static string StoreAdminEmail = "StoreAdminEmail";
        public static string EnableStoreNamePrefix = "EnableStoreNamePrefix";
        public static string DefaultTitle = "DefaultTitle";
        public static string DefaultMetaDescription = "DefaultMetaDescription";
        public static string DefaultMetaKeyWords = "DefaultMetaKeyWords";
        public static string ShowWelcomeMessageOnHomePage = "ShowWelcomeMessageOnHomePage";
        public static string DisplayNewsRSSFeedLinkInBrowserAddressBar = "DisplayNewsRSSFeedLinkInBrowserAddressBar";
        public static string DisplayBlogRSSFeedLinkInBrowserAddressBar = "DisplayBlogRSSFeedLinkInBrowserAddressBar";
        public static string MaximumImageSize = "MaximumImageSize";
        public static string MaxDownloadFileSize = "MaxDownloadFileSize";
        public static string ResizeImagesProportionally = "ResizeImagesProportionally";
        public static string ItemLargeThumbnailImageHeight = "ItemLargeThumbnailImageHeight";
        public static string ItemLargeThumbnailImageWidth = "ItemLargeThumbnailImageWidth";
        public static string ItemMediumThumbnailImageHeight = "ItemMediumThumbnailImageHeight";
        public static string ItemMediumThumbnailImageWidth = "ItemMediumThumbnailImageWidth";
        public static string ItemSmallThumbnailImageHeight = "ItemSmallThumbnailImageHeight";
        public static string ItemSmallThumbnailImageWidth = "ItemSmallThumbnailImageWidth";
        public static string CategoryLargeThumbnailImageHeight = "CategoryLargeThumbnailImageHeight";
        public static string CategoryLargeThumbnailImageWidth = "CategoryLargeThumbnailImageWidth";
        public static string CategoryMediumThumbnailImageHeight = "CategoryMediumThumbnailImageHeight";
        public static string CategoryMediumThumbnailImageWidth = "CategoryMediumThumbnailImageWidth";
        public static string CategorySmallThumbnailImageHeight = "CategorySmallThumbnailImageHeight";
        public static string CategorySmallThumbnailImageWidth = "CategorySmallThumbnailImageWidth";
        public static string ShowItemImagesInCart = "ShowItemImagesInCart";
        public static string ShowItemImagesInWishList = "ShowItemImagesInWishList";
        public static string AllowUsersToCreateMultipleAddress = "AllowUsersToCreateMultipleAddress";
        public static string AllowShippingRateEstimate = "AllowShippingRateEstimate";
        public static string AllowCouponDiscount = "AllowCouponDiscount";
        public static string AllowUsersToStoreCreditCardDataInProfile = "AllowUsersToStoreCreditCardDataInProfile";
        public static string MinimumCartSubTotalAmount = "MinimumCartSubTotalAmount";
        public static string AllowCustomerToSignUpForUserGroup = "AllowCustomerToSignUpForUserGroup";
        public static string AllowCustomersToPayOrderAgainIfTransactionWasDeclined = "AllowCustomersToPayOrderAgainIfTransactionWasDeclined";

        public static string WaterMarkText = "WaterMarkText";
        public static string WaterMarkTextPosition = "WaterMarkTextPosition";
        public static string WaterMarkTextRotation = "WaterMarkTextRotation";
        public static string WaterMarkImage = "WaterMarkImage";
        public static string WaterMarkImagePosition = "WaterMarkImagePosition";
        public static string WaterMarkImageRotation = "WaterMarkImageRotation";
        public static string ShowWaterMarkImage = "ShowWaterMarkImage";


        public static string AllowPrivateMessages = "AllowPrivateMessages";
        public static string DefaultStoreTimeZone = "DefaultStoreTimeZone";
        public static string EnableEmailAFriend = "Enable.EmailAFriend";
        public static string ShowMiniShoppingCart = "Show.MiniShoppingCart";
        public static string NotifyAboutNewItemReviews = "NotifyAboutNewItemReviews";
        public static string ItemReviewMustBeApproved = "ItemReviewMustBeApproved";
        public static string AllowMultipleReviewsPerUser = "AllowMultipleReviewsPerUser";
        public static string AllowMultipleReviewsPerIP = "AllowMultipleReviewsPerIP";
        public static string AllowAnonymousUserToWriteItemRatingAndReviews = "AllowAnonymousUserToWriteItemRatingAndReviews";

       
        public static string WeightUnit = "WeightUnit";
        public static string DimensionUnit = "DimensionUnit";
        public static string GoogleMapAPIKey = "GoogleMapAPIKey";
        public static string LowStockQuantity = "LowStockQuantity";
        public static string OutOfStockQuantity = "OutOfStockQuantity";
        public static string ShoppingOptionRange = "ShoppingOptionRange";
        public static string MinimumItemQuantity = "MinimumItemQuantity";
        public static string MaximumItemQuantity = "MaximumItemQuantity";
        public static string SSLSecurePages = "SSLSecurePages";
        public static string AllowOutStockPurchase = "AllowOutStockPurchase";
        public static string MaxNoOfItemsToCompare = "MaxNoOfItemsToCompare";
        public static string NoOfDisplayItems = "NoOfDisplayItems";       
        public static string AllowedBillingCountry = "AllowedBillingCountry";
        public static string AllowedShippingCountry = "AllowedShippingCountry";
        public static string AdditionalCVR = "AdditionalCVR";
        public static string MinCartQuantity = "MinCartQuantity";
        public static string MaxCartQuantity = "MaxCartQuantity";
        public static string ItemDisplayMode = "ItemDisplayMode";
        public static string ModuleCollapsible = "ModuleCollapsible";

       
        public static string NewCategoryRss = "NewCategoryRss";
        public static string NewCategoryRssCount = "NewCategoryRssCount";       
        public static string NewOrderRss = "NewOrderRss";
        public static string NewOrderRssCount = "NewOrderRssCount";
        public static string NewCustomerRss = "NewCustomerRss";
        public static string NewCustomerRssCount = "NewCustomerRssCount";
        public static string NewItemTagRss = "NewItemTagRss";
        public static string NewItemTagRssCount = "NewItemTagRssCount";
        public static string NewItemReviewRss = "NewItemReviewRss";
        public static string NewItemReviewRssCount = "NewItemReviewRssCount";
        public static string LowStockItemRss = "LowStockItemRss";
        public static string LowStockItemRssCount = "LowStockItemRssCount";      

        public static string ShowAddToCartButton = "Show.AddToCartButton";
        public static string AddToCartButtonText = "AddToCartButtonText";
        public static string ViewAsOptions = "ViewAsOptions";
        public static string ViewAsOptionsDefault = "ViewAsOptionsDefault";
        public static string SortByOptions = "SortByOptions";
        public static string SortByOptionsDefault = "SortByOptionsDefault";
        public static string AskCustomerToSubscribe = "AskCustomerToSubscribe";
        public static string EstimateShippingCostInCartPage = "EstimateShippingCostInCartPage";

        public static string ItemImageMaxWidth = "ItemImageMaxWidth";
        public static string ItemImageMaxHeight = "ItemImageMaxHeight";
        public static string CategoryBannerImageWidth = "CategoryBannerImageWidth";
        public static string CategoryBannerImageHeight = "CategoryBannerImageHeight";

        public static string GetStoreSettingValueByKey(string settingKey, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<string>("usp_Aspx_GetStoreSettingValueBYKey", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetRegionFromCurrencyCode(string currencyCode, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CurrencyCode", currencyCode));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<string>("[usp_Aspx_GetRegionFromCurrencyCode]", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetSymbolFromCurrencyCode(string currencyCode, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CurrencyCode", currencyCode));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<string>("[usp_Aspx_GetSymbolFromCurrencyCode]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PageEntity> GetActivePortalPages(int PortalID, string UserName, string prefix, bool IsActive,
                                                            bool IsDeleted, object IsVisible, object IsRequiredPage)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>
                                                                         {
                                                                             new KeyValuePair<string, object>("@prefix",
                                                                                                              prefix),
                                                                             new KeyValuePair<string, object>(
                                                                                 "@IsActive",
                                                                                 IsActive),
                                                                             new KeyValuePair<string, object>(
                                                                                 "@IsDeleted",
                                                                                 IsDeleted),
                                                                             new KeyValuePair<string, object>(
                                                                                 "@PortalID",
                                                                                 PortalID),
                                                                             new KeyValuePair<string, object>(
                                                                                 "@UserName",
                                                                                 UserName),
                                                                             new KeyValuePair<string, object>(
                                                                                 "@IsVisible",
                                                                                 IsVisible),
                                                                             new KeyValuePair<string, object>(
                                                                                 "@IsRequiredPage",
                                                                                 IsRequiredPage)

                                                                         };
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<PageEntity>("[dbo].[usp_PageStorePortalGetByCustomPrefix]", ParaMeterCollection);

        }
    }
}
