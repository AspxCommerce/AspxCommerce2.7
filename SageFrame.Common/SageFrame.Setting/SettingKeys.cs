#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace SageFrame.Setting
{
    /// <summary>
    /// Application setting keys.
    /// </summary>
    public enum SettingKey
    {
        User_RegistrationEmailValidation = 1,
        PaymentMethod_PaypalStandard_UseSandbox = 2,
        PaymentMethod_PaypalStandard_BusinessEmail = 3,
        PaymentMethod_PaypalStandard_PTIIdentityToken = 4,
        PaymentMethod_Manual_TransactionMode = 5,
        PaymentMethod_PaypalDirect_UseSandbox = 6,
        PaymentMethod_PaypalDirect_APIAccountName = 7,
        PaymentMethod_PaypalDirect_APIAccountPassword = 8,
        PaymentMethod_PaypalDirect_Signature = 9,
        Email_AdminEmailAddress = 10,
        Email_AdminEmailHost = 11,
        Email_AdminEmailPort = 12,
        Email_AdminEmailUser = 13,
        Email_AdminEmailPassword = 14,
        Email_AdminEmailEnableSsl = 15,
        Email_AdminEmailUseDefaultCredentials = 16,
        Display_RecentlyViewedProductCount = 17,
        PaymentMethod_TwoCheckout_VendorID = 18,
        PaymentMethod_TwoCheckout_UseSandbox = 19,
        Common_StoreName = 20,
        Common_StoreURL = 21,
        SEO_DefaultTitle = 22,
        SEO_DefaultMetaDescription = 23,
        SEO_DefaultMetaKeywords = 24,
        PaymentMethod_AuthorizeNET_TransactionMode = 25,
        PaymentMethod_AuthorizeNET_UseSandbox = 26,
        PaymentMethod_AuthorizeNET_TransactionKey = 27,
        PaymentMethod_AuthorizeNET_LoginID = 28,
        PaymentMethod_PaypalExpress_TransactionMode = 29,
        PaymentMethod_PaypalExpress_UseSandbox = 30,
        PaymentMethod_PaypalExpress_APIAccountName = 31,
        PaymentMethod_PaypalExpress_APIAccountPassword = 32,
        PaymentMethod_PaypalExpress_Signature = 33,
        PaymentMethod_PaypalDirect_TransactionMode = 34,
        PaymentMethod_eWay_UseSandbox = 35,
        PaymentMethod_eWay_TestCustomerID = 36,
        PaymentMethod_eWay_LiveCustomerID = 37,
        Common_EnableCompareProducts = 38,
        PaymentMethod_Moneybookers_PayToEmail = 39,
        Shipping_FreeShippingOverX_Enabled = 40,
        Shipping_FreeShippingOverX_Value = 41,
        Common_StoreClosed = 42,
        Common_BaseWeightIn = 43,
        Common_BaseDimensionIn = 44,
        Common_EnableEmailAFirend = 45,
        PaymentMethod_Worldpay_InstanceID = 46,
        PaymentMethod_Worldpay_CallbackPassword = 47,
        SEO_IncludeStoreNameInTitle = 48,
        Display_ShowCategoriesOnMainPage = 49,
        Common_SharedSSL = 50,
        Common_UseSSL = 51,
        PaymentMethod_Worldpay_UseSandbox = 52,
        PaymentMethod_GoogleCheckout_TransactionMode = 53,
        Common_CurrentVersion = 54,
        PaymentMethod_PayFlowPro_TransactionMode = 55,
        PaymentMethod_PayFlowPro_UseSandbox = 56,
        PaymentMethod_PayFlowPro_User = 57,
        PaymentMethod_PayFlowPro_Vendor = 58,
        PaymentMethod_PayFlowPro_Partner = 59,
        PaymentMethod_PayFlowPro_Password = 60,
        PaymentMethod_CashOnDelivery_Info = 61,
        PaymentMethod_Check_Info = 62,
        PaymentMethod_PSIGate_UseSandbox = 63,
        PaymentMethod_PSIGate_StoreID = 64,
        PaymentMethod_PSIGate_Passphrase = 65,
        PaymentMethod_CDGcommerce_RestrictKey = 66,
        PaymentMethod_CDGcommerce_LoginID = 67,
        Display_ShowWelcomeMessageOnMainPage = 68,
        Checkout_AnonymousCheckoutAllowed = 69,

        Media_Category_Large_MaximumImageSize = 70,
        Media_Product_Medium_ThumbnailImageSize = 71,
        Media_Product_Small_ThumbnailImageSize = 72,
        Media_Category_Medium_ThumbnailImageSize = 73,
        Media_Category_Small_ThumbnailImageSize = 74,
        Media_Product_Large_ImageSize = 75,

        SEO_Sitemaps_IncludeCategories = 76,
        SEO_Sitemaps_IncludeManufacturers = 77,
        SEO_Sitemaps_IncludeProducts = 78,
        Display_HideProductsOnCategoriesHomePage = 79,
        Tax_PricesIncludeTax = 80,
        Tax_ShippingIsTaxable = 81,
        Shipping_ShippingOrigin_CountryID = 82,
        Shipping_ShippingOrigin_StateProvinceID = 83,
        Shipping_ShippingOrigin_ZipPostalCode = 84,
        Common_DefaultStoreTimeZoneID = 85,
        Display_PublicStoreTheme = 86,
        Display_SystemThemes = 87,
        Display_RecentlyViewedProductsEnabled = 88,
        Display_RecentlyAddedProductsEnabled = 89,
        SEO_Sitemaps_IncludeTopics = 90,
        SEO_Sitemaps_OtherPages = 91,
        Customer_NewCustomerRegistrationDisabled = 92,
        PaymentMethod_GoogleCheckout_DebugModeEnabled = 93,
        Display_AdminArea_ShowFullErrors = 94,
        Analytics_GoogleEnabled = 95,
        Analytics_GoogleJS = 96,
        Analytics_GoogleID = 97,
        Common_Store_CSS = 99,
        Common_Administration_CSS = 101

    }
}
