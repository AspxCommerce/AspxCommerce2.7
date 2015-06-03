using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxCartController
    {
       public AspxCartController()
       {
       }

       public static bool CheckCart(int itemID, int storeID, int portalID, string userName, string cultureName)
       {
           try
           {
               bool isExist = AspxCartProvider.CheckCart(itemID,storeID,portalID,userName,cultureName);
               return isExist;
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public static bool CheckItemCart(int itemID, int storeID, int portalID, string costvarids)
       {
           bool isAllowed = AspxCartProvider.CheckItemCart(itemID, storeID, portalID, costvarids);
           return isAllowed;
       }

       //------------------------------Add to Cart--------------------------

       public static bool AddtoCart(int itemID, int storeID, int portalID, string userName, string cultureName)
       {

           try
           {
               bool isExist = AspxCartProvider.AddtoCart(itemID, storeID, portalID, userName, cultureName);
               return isExist;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //------------------------------Cart Details--------------------------

       //public static List<CartInfo> GetCartDetails(AspxCommonInfo aspxCommonObj)
       //{
       //    try
       //    {

       //        List<CartInfo> lstCart = AspxCartProvider.GetCartDetails(aspxCommonObj);
       //        return lstCart;
       //    }
       //    catch (Exception e)
       //    {
       //        throw e;
       //    }
       //}

       public static List<CartInfo> GetCartCheckOutDetails(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CartInfo> lstCart = AspxCartProvider.GetCartCheckOutDetails(aspxCommonObj);
               return lstCart;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //Cart Item Qty Discount Calculations

       public static decimal GetDiscountQuantityAmount(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               decimal qtyDiscount = AspxCartProvider.GetDiscountQuantityAmount(aspxCommonObj);
               return qtyDiscount;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       ////------------------------------Delete Cart Items--------------------------

       //public static void DeleteCartItem(int cartID, int cartItemID, AspxCommonInfo aspxCommonObj)
       //{
       //    try
       //    {
       //        AspxCartProvider.DeleteCartItem(cartID, cartItemID, aspxCommonObj);
       //    }
       //    catch (Exception e)
       //    {
       //        throw e;
       //    }
       //}

       //------------------------Clear My Carts----------------------------

       public static void ClearAllCartItems(int cartID, AspxCommonInfo aspxCommonObj)
       {
           AspxCartProvider.ClearAllCartItems(cartID, aspxCommonObj);
       }

       //public static decimal CheckItemQuantityInCart(int itemID, AspxCommonInfo aspxCommonObj, string itemCostVariantIDs)
       //{
       //    try
       //    {
       //        decimal retValue = AspxCartProvider.CheckItemQuantityInCart(itemID, aspxCommonObj, itemCostVariantIDs);
       //        return retValue;
       //    }
       //    catch (Exception e)
       //    {
       //        throw e;
       //    }
       //}

       public static decimal GetCostVariantQuantity(int itemID, int storeID, int portalID, string itemCostVariantIDs)
       {
           try
           {
               decimal cvQty = AspxCartProvider.GetCostVariantQuantity(itemID,storeID,portalID,itemCostVariantIDs);
               return cvQty;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool CheckOutOfStock(int itemID, int storeID, int portalID, string itemCostVariantIDs)
       {
           try
           {
               bool isOutStock = AspxCartProvider.CheckOutOfStock(itemID, storeID, portalID, itemCostVariantIDs);
               return isOutStock;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static CartExistInfo CheckCustomerCartExist(AspxCommonInfo aspxCommonObj)
       {
           CartExistInfo objCartExist =AspxCartProvider.CheckCustomerCartExist(aspxCommonObj);
           return objCartExist;
       }

       //------------------------------Get ShippingMethodByTotalItemsWeight--------------------------

       public static List<ShippingMethodInfo> GetShippingMethodByWeight(int storeID, int portalID, int customerID, string userName, string cultureName, string sessionCode)
       {
           try
           {
               List<ShippingMethodInfo> lstShipMethod = AspxCartProvider.GetShippingMethodByWeight(storeID, portalID, customerID, userName, cultureName, sessionCode);
               return lstShipMethod;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<ShippingCostInfo> GetShippingCostByItem(int storeID, int portalID, int customerID, string sessionCode, string userName, string cultureName)
       {
           try
           {
               List<ShippingCostInfo> lstShipCost = AspxCartProvider.GetShippingCostByItem(storeID, portalID, customerID, sessionCode, userName, cultureName);
               return lstShipCost;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void UpdateShoppingCart(UpdateCartInfo updateCartObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxCartProvider.UpdateShoppingCart(updateCartObj, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool UpdateCartAnonymoususertoRegistered(int storeID, int portalID, int customerID, string sessionCode)
       {
           try
           {
               bool isUpdate = AspxCartProvider.UpdateCartAnonymoususertoRegistered(storeID, portalID, customerID, sessionCode);
               return isUpdate;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       # region "Cart Tax"
       public static List<CartTaxInfo> GetCartTax(CartDataInfo cartTaxObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CartTaxInfo> lstCartTax = AspxCartProvider.GetCartTax(cartTaxObj, aspxCommonObj);
               return lstCartTax;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //public static List<CartUnitTaxInfo> GetCartUnitTax(CartDataInfo cartUnitTaxObj, AspxCommonInfo aspxCommonObj)
       //{
       //    try
       //    {
       //        List<CartUnitTaxInfo> lstCartUnitTax = AspxCartProvider.GetCartUnitTax(cartUnitTaxObj, aspxCommonObj);
       //        return lstCartUnitTax;
       //    }
       //    catch (Exception e)
       //    {
       //        throw e;
       //    }
       //}

       public static List<CartTaxforOrderInfo> GetCartTaxforOrder(CartDataInfo cartTaxOrderObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CartTaxforOrderInfo> lstCartTaxOrder = AspxCartProvider.GetCartTaxforOrder(cartTaxOrderObj, aspxCommonObj);
               return lstCartTaxOrder;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       # endregion 

       public static string GetDiscountPriceRule(int cartID, AspxCommonInfo aspxCommonObj, decimal shippingCost)
       {

           try
           {
               string discount = AspxCartProvider.GetDiscountPriceRule(cartID, aspxCommonObj, shippingCost);
               return discount;

           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static int GetCartId(int storeID, int portalID, int customerID, string sessionCode)
       {
           try
           {
               int cartId = AspxCartProvider.GetCartId(storeID, portalID, customerID, sessionCode);
               return cartId;
              
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       #region Payment Gateway and CheckOUT PROCESS

       public static bool CheckDownloadableItemOnly(int storeID, int portalID, int customerID, string sessionCode)
       {
           try
           {
               bool isAllDownload = AspxCartProvider.CheckDownloadableItemOnly(storeID, portalID, customerID, sessionCode);
               return isAllDownload;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<PaymentGatewayListInfo> GetPGList(AspxCommonInfo aspxCommonObj)
       {
           List<PaymentGatewayListInfo> pginfo = AspxCartProvider.GetPGList(aspxCommonObj);
           return pginfo;
       }

       public static List<PaymentGateway> GetPaymentGateway(int portalID, string cultureName, string userName)
       {
           try
           {
               List<PaymentGateway> lstPayGateWay = AspxCartProvider.GetPaymentGateway(portalID, cultureName, userName);
               return lstPayGateWay;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<UserAddressInfo> GetUserAddressForCheckOut(int storeID, int portalID, string userName, string cultureName)
       {
           try
           {
               List<UserAddressInfo> lstUserAddress = AspxCartProvider.GetUserAddressForCheckOut(storeID, portalID, userName, cultureName);
               return lstUserAddress;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool CheckCreditCard(AspxCommonInfo aspxCommonObj, string creditCardNo)
       {
           try
           {
               bool isExist = AspxCartProvider.CheckCreditCard(aspxCommonObj, creditCardNo);
               return isExist;
           }
           catch (Exception e)
           {

               throw e;
           }
       }

       public static bool CheckEmailAddress(string email, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               bool isExist = AspxCartProvider.CheckEmailAddress(email, aspxCommonObj);
               return isExist;
           }
           catch (Exception e)
           {

               throw e;
           }
       }
       #endregion

       // ShoppingCartManagement ---------------------get Cart details in grid-------------------------------

       public static List<ShoppingCartInfo> GetShoppingCartItemsDetails(int offset, System.Nullable<int> limit, string itemName, string quantity, AspxCommonInfo aspxCommonObj, decimal timeToAbandonCart)
       {
           try
           {
               List<ShoppingCartInfo> lstShopCart = AspxCartProvider.GetShoppingCartItemsDetails(offset, limit, itemName, quantity, aspxCommonObj, timeToAbandonCart);
               return lstShopCart;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //---------------------bind Abandoned cart details-------------------------

       public static List<AbandonedCartInfo> GetAbandonedCartDetails(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, decimal timeToAbandonCart)
       {
           try
           {
               List<AbandonedCartInfo> bind = AspxCartProvider.GetAbandonedCartDetails(offset, limit, aspxCommonObj, timeToAbandonCart);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-------------------Verify Coupon Code-----------------------------      
       public CouponVerificationInfo VerifyCouponCode(decimal totalCost, string couponCode, string itemIds, string cartItemIds, AspxCommonInfo aspxCommonObj, int appliedCount)
       {
           try
           {
               CouponVerificationInfo objCoupVeri = AspxCouponManageController.VerifyUserCoupon(totalCost, couponCode, itemIds, cartItemIds, aspxCommonObj, appliedCount);
               return objCoupVeri;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    }
}
