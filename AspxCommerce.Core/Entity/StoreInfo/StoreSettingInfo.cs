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
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class StoreSettingInfo
    {
        [DataMember]
        private string _defaultProductImageURL;

        [DataMember]
        private string _myAccountURL;

        [DataMember]
        private string _shoppingCartURL;

        [DataMember]
        private string _detailPageURL;

        [DataMember]
        private string _itemDetailURL;

        [DataMember]
        private string _categoryDetailURL;

        [DataMember]
        private string _singleCheckOutURL;

        [DataMember]
        private string _multiCheckOutURL;

       [DataMember]
        private string _storeLocatorURL;

       [DataMember]
        private string _rssFeedURL;

       [DataMember]
        private string _trackPackageUrl;

        [DataMember]
        private string _shipDetailPageURL;

        [DataMember]
        private string _itemMgntPageURL;

        [DataMember]
        private string _categoryMgntPageURL;

        [DataMember]
        private string _mainCurrency;

        [DataMember]
        private string _allowRealTimeNotifications;

        [DataMember]
        private string _storeName;

        [DataMember]
        private string _storeLogoURL;

        [DataMember]
        private string _timeToDeleteAbandonedCart;

        [DataMember]
        private string _cartAbandonedTime;

        [DataMember]
        private int _lowStockQuantity;

        [DataMember]
        private int _outOfStockQuantity;

        [DataMember]
        private string _shoppingOptionRange;

        [DataMember]
        private string _allowAnonymousCheckOut;

        [DataMember]
        private string _allowMultipleShippingAddress;

        [DataMember]
        private string _allowOutStockPurchase;

        [DataMember]
        private string _sendEcommerceEmailsFrom;

        [DataMember]
        private string _sendOrderNotification;

        [DataMember]
        private string _maximumImageSize;

        [DataMember]
        private string _maxDownloadFileSize;

        [DataMember]
        private string _resizeImagesProportionally;

        [DataMember]
        private string _itemLargeThumbnailImageHeight;

        [DataMember]
        private string _itemLargeThumbnailImageWidth;

        [DataMember]
        private string _itemMediumThumbnailImageHeight;

        [DataMember]
        private string _itemMediumThumbnailImageWidth;

        [DataMember]
        private string _itemSmallThumbnailImageHeight;

        [DataMember]
        private string _itemSmallThumbnailImageWidth;

        [DataMember]
        private string _itemImageMaxWidth;

        [DataMember]
        private string _itemImageMaxHeight;

        [DataMember]
        private string _categoryLargeThumbnailImageHeight;

        [DataMember]
        private string _categoryLargeThumbnailImageWidth;

        [DataMember]
        private string _categoryMediumThumbnailImageHeight;

         [DataMember]
        private string _categoryMediumThumbnailImageWidth;

        [DataMember]
        private string _categorySmallThumbnailImageHeight;

        [DataMember]
        private string _categorySmallThumbnailImageWidth;

        [DataMember]
        private string _categoryBannerImageWidth;

        [DataMember]
        private string _categoryBannerImageHeight;

        [DataMember]
        private string _showItemImagesInCart;

        [DataMember]
        private string _showItemImagesInWishList;

        [DataMember]
        private string _allowUsersToCreateMultipleAddress;

        [DataMember]
        private string _allowShippingRateEstimate;

        [DataMember]
        private string _allowCouponDiscount;

        [DataMember]
        private string _minimumCartSubTotalAmount;
       
       [DataMember]
        private string _enableEmailAFriend;

        [DataMember]
        private string _showMiniShoppingCart;

        [DataMember]
        private string _allowMultipleReviewsPerUser;

        [DataMember]
        private string _allowMultipleReviewsPerIP;

        [DataMember]
        private string _allowAnonymousUserToWriteItemRatingAndReviews;

        [DataMember]
        private string _weightUnit;

        [DataMember]
        private string _dimensionUnit;

        [DataMember]
        private string _noOfDisplayItems;

        [DataMember]
        private string _additionalCVR;

        [DataMember]
        private int _minCartQuantity;

        [DataMember]
        private int _maxCartQuantity;

        [DataMember]
        public string _itemDisplayMode;

        [DataMember]
        public string _moduleCollapsible;
        [DataMember]
        private string _allowedBillingCountry;

        [DataMember]
        private string _allowedShippingCountry;

        public StoreSettingInfo()
        {

        }

        public string DefaultProductImageURL
        {
            get
            {
                return this._defaultProductImageURL;
            }
            set
            {
                if ((this._defaultProductImageURL != value))
                {
                    this._defaultProductImageURL = value;
                }
            }
        }


        public string MyAccountURL
        {
            get
            {
                return this._myAccountURL;
            }
            set
            {
                if ((this._myAccountURL != value))
                {
                    this._myAccountURL = value;
                }
            }
        }


        public string ShoppingCartURL
        {
            get
            {
                return this._shoppingCartURL;
            }
            set
            {
                if ((this._shoppingCartURL != value))
                {
                    this._shoppingCartURL = value;
                }
            }
        }
      

        public string DetailPageURL
        {
            get
            {
                return this._detailPageURL;
            }
            set
            {
                if ((this._detailPageURL != value))
                {
                    this._detailPageURL = value;
                }
            }
        }


        public string ItemDetailURL
        {
            get
            {
                return this._itemDetailURL;
            }
            set
            {
                if ((this._itemDetailURL != value))
                {
                    this._itemDetailURL = value;
                }
            }
        }

        public string CategoryDetailURL
        {
            get
            {
                return this._categoryDetailURL;
            }
            set
            {
                if ((this._categoryDetailURL != value))
                {
                    this._categoryDetailURL = value;
                }
            }
        }
      
        public string SingleCheckOutURL
        {
            get
            {
                return this._singleCheckOutURL;
            }
            set
            {
                if ((this._singleCheckOutURL != value))
                {
                    this._singleCheckOutURL = value;
                }
            }
        }


        public string MultiCheckOutURL
        {
            get
            {
                return this._multiCheckOutURL;
            }
            set
            {
                if ((this._multiCheckOutURL != value))
                {
                    this._multiCheckOutURL = value;
                }
            }
        }


        public string StoreLocatorURL
        {
            get
            {
                return this._storeLocatorURL;
            }
            set
            {
                if ((this._storeLocatorURL != value))
                {
                    this._storeLocatorURL = value;
                }
            }
        }

        public string RssFeedURL
        {
            get
            {
                return this._rssFeedURL;
            }
            set
            {
                if ((this._rssFeedURL != value))
                {
                    this._rssFeedURL = value;
                }
            }
        }

        public string TrackPackageUrl
        {
            get
            {
                return this._trackPackageUrl;
            }
            set
            {
                if ((this._trackPackageUrl != value))
                {
                    this._trackPackageUrl = value;
                }
            }
        }

        public string ShipDetailPageURL
        {
            get
            {
                return this._shipDetailPageURL;
            }
            set
            {
                if ((this._shipDetailPageURL != value))
                {
                    this._shipDetailPageURL = value;
                }
            }
        }

        public string ItemMgntPageURL
        {
            get
            {
                return this._itemMgntPageURL;
            }
            set
            {
                if ((this._itemMgntPageURL != value))
                {
                    this._itemMgntPageURL = value;
                }
            }
        }

        public string CategoryMgntPageURL
        {
            get
            {
                return this._categoryMgntPageURL;
            }
            set
            {
                if ((this._categoryMgntPageURL != value))
                {
                    this._categoryMgntPageURL = value;
                }
            }
        }
     
        public string MainCurrency
        {
            get
            {
                return this._mainCurrency;
            }
            set
            {
                if ((this._mainCurrency != value))
                {
                    this._mainCurrency = value;
                }
            }
        }
        public string AllowRealTimeNotifications
        {
            get
            {
                return this._allowRealTimeNotifications;
            }
            set
            {
                if ((this._allowRealTimeNotifications != value))
                {
                    this._allowRealTimeNotifications = value;
                }
            }
        }

        public string StoreName
        {
            get
            {
                return this._storeName;
            }
            set
            {
                if ((this._storeName != value))
                {
                    this._storeName = value;
                }
            }
        }

        public string StoreLogoURL
        {
            get
            {
                return this._storeLogoURL;
            }
            set
            {
                if ((this._storeLogoURL != value))
                {
                    this._storeLogoURL = value;
                }
            }
        }

        public string TimeToDeleteAbandonedCart
        {
            get
            {
                return this._timeToDeleteAbandonedCart;
            }
            set
            {
                if ((this._timeToDeleteAbandonedCart != value))
                {
                    this._timeToDeleteAbandonedCart = value;
                }
            }
        }

        public string CartAbandonedTime
        {
            get
            {
                return this._cartAbandonedTime;
            }
            set
            {
                if ((this._cartAbandonedTime != value))
                {
                    this._cartAbandonedTime = value;
                }
            }
        }

        public int LowStockQuantity
        {
            get
            {
                return this._lowStockQuantity;
            }
            set
            {
                if ((this._lowStockQuantity != value))
                {
                    this._lowStockQuantity = value;
                }
            }
        }

        public int OutOfStockQuantity
        {
            get
            {
                return this._outOfStockQuantity;
            }
            set
            {
                if ((this._outOfStockQuantity != value))
                {
                    this._outOfStockQuantity = value;
                }
            }
        }

        public string ShoppingOptionRange
        {
            get
            {
                return this._shoppingOptionRange;
            }
            set
            {
                if ((this._shoppingOptionRange != value))
                {
                    this._shoppingOptionRange = value;
                }
            }
        }

        public string AllowAnonymousCheckOut
        {
            get
            {
                return this._allowAnonymousCheckOut;
            }
            set
            {
                if ((this._allowAnonymousCheckOut != value))
                {
                    this._allowAnonymousCheckOut = value;
                }
            }
        }

        public string AllowMultipleShippingAddress
        {
            get
            {
                return this._allowMultipleShippingAddress;
            }
            set
            {
                if ((this._allowMultipleShippingAddress != value))
                {
                    this._allowMultipleShippingAddress = value;
                }
            }
        }



        public string AllowOutStockPurchase
        {
            get
            {
                return this._allowOutStockPurchase;
            }
            set
            {
                if ((this._allowOutStockPurchase != value))
                {
                    this._allowOutStockPurchase = value;
                }
            }
        }

        public string SendEcommerceEmailsFrom
        {
            get
            {
                return this._sendEcommerceEmailsFrom;
            }
            set
            {
                if ((this._sendEcommerceEmailsFrom != value))
                {
                    this._sendEcommerceEmailsFrom = value;
                }
            }
        }



        public string SendOrderNotification
        {
            get
            {
                return this._sendOrderNotification;
            }
            set
            {
                if ((this._sendOrderNotification != value))
                {
                    this._sendOrderNotification = value;
                }
            }
        }

        public string MaximumImageSize
        {
            get
            {
                return this._maximumImageSize;
            }
            set
            {
                if ((this._maximumImageSize != value))
                {
                    this._maximumImageSize = value;
                }
            }
        }

        public string MaxDownloadFileSize
        {
            get
            {
                return this._maxDownloadFileSize;
            }
            set
            {
                if ((this._maxDownloadFileSize != value))
                {
                    this._maxDownloadFileSize = value;
                }
            }
        }

        public string ResizeImagesProportionally
        {
            get
            {
                return this._resizeImagesProportionally;
            }
            set
            {
                if ((this._resizeImagesProportionally != value))
                {
                    this._resizeImagesProportionally = value;
                }
            }
        }
        public string ItemLargeThumbnailImageHeight
        {
            get
            {
                return this._itemLargeThumbnailImageHeight;
            }
            set
            {
                if ((this._itemLargeThumbnailImageHeight != value))
                {
                    this._itemLargeThumbnailImageHeight = value;
                }
            }
        }

        public string ItemLargeThumbnailImageWidth
        {
            get
            {
                return this._itemLargeThumbnailImageWidth;
            }
            set
            {
                if ((this._itemLargeThumbnailImageWidth != value))
                {
                    this._itemLargeThumbnailImageWidth = value;
                }
            }
        }

        public string ItemMediumThumbnailImageHeight
        {
            get
            {
                return this._itemMediumThumbnailImageHeight;
            }
            set
            {
                if ((this._itemMediumThumbnailImageHeight != value))
                {
                    this._itemMediumThumbnailImageHeight = value;
                }
            }
        }

        public string ItemMediumThumbnailImageWidth
        {
            get
            {
                return this._itemMediumThumbnailImageWidth;
            }
            set
            {
                if ((this._itemMediumThumbnailImageWidth != value))
                {
                    this._itemMediumThumbnailImageWidth = value;
                }
            }
        }

        public string ItemSmallThumbnailImageHeight
        {
            get
            {
                return this._itemSmallThumbnailImageHeight;
            }
            set
            {
                if ((this._itemSmallThumbnailImageHeight != value))
                {
                    this._itemSmallThumbnailImageHeight = value;
                }
            }
        }

        public string ItemSmallThumbnailImageWidth
        {
            get
            {
                return this._itemSmallThumbnailImageWidth;
            }
            set
            {
                if ((this._itemSmallThumbnailImageWidth != value))
                {
                    this._itemSmallThumbnailImageWidth = value;
                }
            }
        }
        public string ItemImageMaxWidth
        {
            get
            {
                return this._itemImageMaxWidth;
            }
            set
            {
                if ((this._itemImageMaxWidth != value))
                {
                    this._itemImageMaxWidth = value;
                }
            }
        }

        public string ItemImageMaxHeight
        {
            get
            {
                return this._itemImageMaxHeight;
            }
            set
            {
                if ((this._itemImageMaxHeight != value))
                {
                    this._itemImageMaxHeight = value;
                }
            }
        }

        public string CategoryLargeThumbnailImageHeight
        {
            get
            {
                return this._categoryLargeThumbnailImageHeight;
            }
            set
            {
                if ((this._categoryLargeThumbnailImageHeight != value))
                {
                    this._categoryLargeThumbnailImageHeight = value;
                }
            }
        }

        public string CategoryLargeThumbnailImageWidth
        {
            get
            {
                return this._categoryLargeThumbnailImageWidth;
            }
            set
            {
                if ((this._categoryLargeThumbnailImageWidth != value))
                {
                    this._categoryLargeThumbnailImageWidth = value;
                }
            }
        }

        public string CategoryMediumThumbnailImageHeight
        {
            get
            {
                return this._categoryMediumThumbnailImageHeight;
            }
            set
            {
                if ((this._categoryMediumThumbnailImageHeight != value))
                {
                    this._categoryMediumThumbnailImageHeight = value;
                }
            }
        }

        public string CategoryMediumThumbnailImageWidth
        {
            get
            {
                return this._categoryMediumThumbnailImageWidth;
            }
            set
            {
                if ((this._categoryMediumThumbnailImageWidth != value))
                {
                    this._categoryMediumThumbnailImageWidth = value;
                }
            }
        }

        public string CategorySmallThumbnailImageHeight
        {
            get
            {
                return this._categorySmallThumbnailImageHeight;
            }
            set
            {
                if ((this._categorySmallThumbnailImageHeight != value))
                {
                    this._categorySmallThumbnailImageHeight = value;
                }
            }
        }

        public string CategorySmallThumbnailImageWidth
        {
            get
            {
                return this._categorySmallThumbnailImageWidth;
            }
            set
            {
                if ((this._categorySmallThumbnailImageWidth != value))
                {
                    this._categorySmallThumbnailImageWidth = value;
                }
            }
        }


        public string CategoryBannerImageWidth
        {
            get
            {
                return this._categoryBannerImageWidth;
            }
            set
            {
                if ((this._categoryBannerImageWidth != value))
                {
                    this._categoryBannerImageWidth = value;
                }
            }
        }
        public string CategoryBannerImageHeight
        {
            get
            {
                return this._categoryBannerImageHeight;
            }
            set
            {
                if ((this._categoryBannerImageHeight != value))
                {
                    this._categoryBannerImageHeight = value;
                }
            }
        }

        public string ShowItemImagesInCart
        {
            get
            {
                return this._showItemImagesInCart;
            }
            set
            {
                if ((this._showItemImagesInCart != value))
                {
                    this._showItemImagesInCart = value;
                }
            }
        }

        public string ShowItemImagesInWishList
        {
            get
            {
                return this._showItemImagesInWishList;
            }
            set
            {
                if ((this._showItemImagesInWishList != value))
                {
                    this._showItemImagesInWishList = value;
                }
            }
        }

        public string AllowUsersToCreateMultipleAddress
        {
            get
            {
                return this._allowUsersToCreateMultipleAddress;
            }
            set
            {
                if ((this._allowUsersToCreateMultipleAddress != value))
                {
                    this._allowUsersToCreateMultipleAddress = value;
                }
            }
        }

        public string AllowShippingRateEstimate
        {
            get
            {
                return this._allowShippingRateEstimate;
            }
            set
            {
                if ((this._allowShippingRateEstimate != value))
                {
                    this._allowShippingRateEstimate = value;
                }
            }
        }

        public string AllowCouponDiscount
        {
            get
            {
                return this._allowCouponDiscount;
            }
            set
            {
                if ((this._allowCouponDiscount != value))
                {
                    this._allowCouponDiscount = value;
                }
            }
        }



        public string MinimumCartSubTotalAmount
        {
            get
            {
                return this._minimumCartSubTotalAmount;
            }
            set
            {
                if ((this._minimumCartSubTotalAmount != value))
                {
                    this._minimumCartSubTotalAmount = value;
                }
            }
        }
      
        public string EnableEmailAFriend
        {
            get
            {
                return this._enableEmailAFriend;
            }
            set
            {
                if ((this._enableEmailAFriend != value))
                {
                    this._enableEmailAFriend = value;
                }
            }
        }

        public string ShowMiniShoppingCart
        {
            get
            {
                return this._showMiniShoppingCart;
            }
            set
            {
                if ((this._showMiniShoppingCart != value))
                {
                    this._showMiniShoppingCart = value;
                }
            }
        }

        public string AllowMultipleReviewsPerUser
        {
            get
            {
                return this._allowMultipleReviewsPerUser;
            }
            set
            {
                if ((this._allowMultipleReviewsPerUser != value))
                {
                    this._allowMultipleReviewsPerUser = value;
                }
            }
        }

        public string AllowMultipleReviewsPerIP
        {
            get
            {
                return this._allowMultipleReviewsPerIP;
            }
            set
            {
                if ((this._allowMultipleReviewsPerIP != value))
                {
                    this._allowMultipleReviewsPerIP = value;
                }
            }
        }

        public string AllowAnonymousUserToWriteItemRatingAndReviews
        {
            get
            {
                return this._allowAnonymousUserToWriteItemRatingAndReviews;
            }
            set
            {
                if ((this._allowAnonymousUserToWriteItemRatingAndReviews != value))
                {
                    this._allowAnonymousUserToWriteItemRatingAndReviews = value;
                }
            }
        }
     
        public string WeightUnit
        {
            get
            {
                return this._weightUnit;
            }
            set
            {
                if ((this._weightUnit != value))
                {
                    this._weightUnit = value;
                }
            }
        }

        public string DimensionUnit
        {
            get
            {
                return this._dimensionUnit;
            }
            set
            {
                if ((this._dimensionUnit != value))
                {
                    this._dimensionUnit = value;
                }
            }
        }

        public string NoOfDisplayItems
        {
            get
            {
                return this._noOfDisplayItems;
            }
            set
            {
                if ((this._noOfDisplayItems != value))
                {
                    this._noOfDisplayItems = value;
                }
            }
        }

        public string AdditionalCVR
        {
            get
            {
                return this._additionalCVR;
            }
            set
            {
                if ((this._additionalCVR != value))
                {
                    this._additionalCVR = value;
                }
            }
        }
        public int MinCartQuantity
        {
            get
            {
                return this._minCartQuantity;
            }
            set
            {
                if ((this._minCartQuantity != value))
                {
                    this._minCartQuantity = value;
                }
            }
        }
        public int MaxCartQuantity
        {
            get
            {
                return this._maxCartQuantity;
            }
            set
            {
                if ((this._maxCartQuantity != value))
                {
                    this._maxCartQuantity = value;
                }
            }
        }
        public string ItemDisplayMode
        {
            get
            {
                return this._itemDisplayMode;
            }
            set
            {
                if ((this._itemDisplayMode != value))
                {
                    this._itemDisplayMode = value;
                }
            }
        }
        public string ModuleCollapsible
        {
            get
            {
                return this._moduleCollapsible;
            }
            set
            {
                if ((this._moduleCollapsible != value))
                {
                    this._moduleCollapsible = value;
                }
            }
        }

        public string AllowedBillingCountry
        {
            get
            {
                return this._allowedBillingCountry;
            }
            set
            {
                if ((this._allowedBillingCountry != value))
                {
                    this._allowedBillingCountry = value;
                }
            }
        }

        public string AllowedShippingCountry
        {
            get
            {
                return this._allowedShippingCountry;
            }
            set
            {
                if ((this._allowedShippingCountry != value))
                {
                    this._allowedShippingCountry = value;
                }
            }
        }        

        public string NewCategoryRss { get; set; }
        public string NewCategoryRssCount { get; set; }
       
        public string NewOrderRss { get; set; }
        public string NewOrderRssCount { get; set; }

        public string NewCustomerRss { get; set; }
        public string NewCustomerRssCount { get; set; }

        public string NewItemTagRss { get; set; }
        public string NewItemTagRssCount { get; set; }

        public string NewItemReviewRss { get; set; }
        public string NewItemReviewRssCount { get; set; }

        public string LowStockItemRss { get; set; }
        public string LowStockItemRssCount { get; set; }
        
        public string WaterMarkText { get; set; }
        public string WaterMarkTextPosition { get; set; }
        public string WaterMarkTextRotation { get; set; }
        public string WaterMarkImagePosition { get; set; }
        public string WaterMarkImageRotation { get; set; }
        public string ShowWaterMarkImage { get; set; }

        public string ShowAddToCartButton { get; set; }
        public string AddToCartButtonText { get; set; }
        public string ViewAsOptions { get; set; }
        public string ViewAsOptionsDefault { get; set; }
        public string SortByOptions { get; set; }
        public string SortByOptionsDefault { get; set; }
        public string AskCustomerToSubscribe { get; set; }
        public string EstimateShippingCostInCartPage { get; set; }
    }

}

