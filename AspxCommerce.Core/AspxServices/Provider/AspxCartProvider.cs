using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using System.Data.SqlClient;
using System.Data;
using SageFrame.Web;

namespace AspxCommerce.Core
{
   public class AspxCartProvider
    {
       public AspxCartProvider()
       {
       }
       //------------------------------Check Cart--------------------------
       
       public static bool CheckCart(int itemID, int storeID, int portalID, string userName, string cultureName)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
               parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
               SQLHandler sqlH = new SQLHandler();
               bool isExist= sqlH.ExecuteNonQueryAsGivenType<bool>("[usp_Aspx_CheckCart]", parameter, "@IsExist");
               return isExist;

           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //------------------------------Add to Cart--------------------------

       public static bool CheckItemCart(int itemID, int storeID, int portalID, string costvarids)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
           parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           parameter.Add(new KeyValuePair<string, object>("@ItemCostVariantIDs", costvarids));
           SQLHandler sqlH = new SQLHandler();
           bool isAllowed= sqlH.ExecuteNonQueryAsGivenType<bool>("[usp_Aspx_CheckCostVarintQuantityInCart]", parameter, "@IsAllowAddtoCart");
           return isAllowed;
       }
       
       public static bool AddtoCart(int itemID, int storeID, int portalID, string userName, string cultureName)
       {

           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
               parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
               SQLHandler sqlH = new SQLHandler();
               bool isExist= sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckCostVariantForItem", parameter, "@IsExist");
               return isExist;

           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //------------------------------Cart Details--------------------------
       
       public static List<CartInfo> GetCartDetails(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<CartInfo> lstCart= sqlH.ExecuteAsList<CartInfo>("usp_Aspx_GetCartDetails", parameter);
               return lstCart;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static DataSet GetCartDetailsDataSet(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               DataSet lstCart = sqlH.ExecuteAsDataSet("usp_Aspx_GetCartDetails", parameter);
               return lstCart;
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       
       public static List<CartInfo> GetCartCheckOutDetails(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<CartInfo> lstCart= sqlH.ExecuteAsList<CartInfo>("usp_Aspx_GetCartOverView", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               decimal qtyDiscount= sqlH.ExecuteNonQueryAsGivenType<decimal>("usp_Aspx_GetItemQuantityDiscountAmount", parameter, "@QtyDiscount");
               return qtyDiscount;
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPSCt(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@CartID", cartID));
               parameter.Add(new KeyValuePair<string, object>("@CartItemID", cartItemID));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteCartItem", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //------------------------Clear My Carts----------------------------
       
       public static void ClearAllCartItems(int cartID, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPSCt(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@CartID", cartID));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_ClearCartItems", parameter);
       }

       
       public static ItemCartInfo CheckItemQuantityInCart(int itemID, AspxCommonInfo aspxCommonObj, string itemCostVariantIDs)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPSCt(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
               parameter.Add(new KeyValuePair<string, object>("@ItemCostVariantIDs", itemCostVariantIDs));
               SQLHandler sqlH = new SQLHandler();
               ItemCartInfo itemCartObj = sqlH.ExecuteAsObject<ItemCartInfo>("usp_Aspx_CheckCustomerQuantityInCart", parameter);
               return itemCartObj;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       
       public static decimal GetCostVariantQuantity(int itemID, int storeID, int portalID, string itemCostVariantIDs)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@ItemCostVariantIDs", itemCostVariantIDs));
               SQLHandler sqlH = new SQLHandler();
               decimal cvQty= sqlH.ExecuteAsScalar<decimal>("usp_Aspx_GetCostVariantQuantity", parameter);
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
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@ItemCostVariantIDs", itemCostVariantIDs));
               SQLHandler sqlH = new SQLHandler();
               bool isOutStock= sqlH.ExecuteNonQueryAsGivenType<bool>("[dbo].[usp_Aspx_CheckOutOfStockForCostVariants]", parameter, "@IsOutOfStock");
               return isOutStock;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       
       public static CartExistInfo CheckCustomerCartExist(AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPSCt(aspxCommonObj);
           SQLHandler sqlH = new SQLHandler();
           CartExistInfo objCartExist= sqlH.ExecuteAsObject<CartExistInfo>("usp_Aspx_CheckCartExists", parameter);
           return objCartExist;
       }

       //------------------------------Get ShippingMethodByTotalItemsWeight--------------------------
       
       public static List<ShippingMethodInfo> GetShippingMethodByWeight(int storeID, int portalID, int customerID, string userName, string cultureName, string sessionCode)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
               parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
               parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
               parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
               SQLHandler sqlH = new SQLHandler();
               List<ShippingMethodInfo> lstShipMethod= sqlH.ExecuteAsList<ShippingMethodInfo>("usp_Aspx_GetShippingMethodByTotalWeight", parameter);
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
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
               parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
               parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
               parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
               SQLHandler sqlH = new SQLHandler();
               List<ShippingCostInfo> lstShipCost= sqlH.ExecuteAsList<ShippingCostInfo>("usp_Aspx_ShippingDetailsForItem", parameter);
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
               List<KeyValuePair<string, object>> parameter =CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@CartID", updateCartObj.CartID));
               parameter.Add(new KeyValuePair<string, object>("@CartItemIDs", updateCartObj.CartItemIDs));
               parameter.Add(new KeyValuePair<string, object>("@Quantities", updateCartObj.Quantities));
               parameter.Add(new KeyValuePair<string, object>("@AllowOutOfStock", updateCartObj.AllowOutOfStock));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_UpdateShoppingCart", parameter);
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
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
               parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
               SQLHandler sqlH = new SQLHandler();
               bool isUpdate= sqlH.ExecuteNonQueryAsBool("usp_Aspx_UpdateCartAnonymousUserToRegistered", parameter, "@IsUpdate");
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFParamNoSCode(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemID", cartTaxObj.ItemID));
               parameter.Add(new KeyValuePair<string, object>("@Country", cartTaxObj.Country));
               parameter.Add(new KeyValuePair<string, object>("@State", cartTaxObj.State));
               parameter.Add(new KeyValuePair<string, object>("@Zip", cartTaxObj.Zip));
               parameter.Add(new KeyValuePair<string, object>("@AddressID", cartTaxObj.AddressID));
               SQLHandler sqlH = new SQLHandler();
               List<CartTaxInfo> lstCartTax= sqlH.ExecuteAsList<CartTaxInfo>("usp_Aspx_GetCartTax", parameter);
               return lstCartTax;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static List<CartUnitTaxInfo> GetCartUnitTax(CartDataInfo cartUnitTaxObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFParamNoSCode(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemID", cartUnitTaxObj.ItemID));
               parameter.Add(new KeyValuePair<string, object>("@Country", cartUnitTaxObj.Country));
               parameter.Add(new KeyValuePair<string, object>("@State", cartUnitTaxObj.State));
               parameter.Add(new KeyValuePair<string, object>("@Zip", cartUnitTaxObj.Zip));
               parameter.Add(new KeyValuePair<string, object>("@AddressID", cartUnitTaxObj.AddressID));
               parameter.Add(new KeyValuePair<string, object>("@CostVariantsValueIDs", cartUnitTaxObj.CostVariantsValueIDs));
               SQLHandler sqlH = new SQLHandler();
               List<CartUnitTaxInfo> lstCartUnitTax= sqlH.ExecuteAsList<CartUnitTaxInfo>("usp_Aspx_GetCartUnitTax", parameter);
               return lstCartUnitTax;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
  
       public static List<CartTaxforOrderInfo> GetCartTaxforOrder(CartDataInfo cartTaxOrderObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFParamNoSCode(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemID", cartTaxOrderObj.ItemID));
               parameter.Add(new KeyValuePair<string, object>("@Country", cartTaxOrderObj.Country));
               parameter.Add(new KeyValuePair<string, object>("@State", cartTaxOrderObj.State));
               parameter.Add(new KeyValuePair<string, object>("@Zip", cartTaxOrderObj.Zip));
               parameter.Add(new KeyValuePair<string, object>("@AddressID", cartTaxOrderObj.AddressID));
               parameter.Add(new KeyValuePair<string, object>("@CostVariantsValueIDs", cartTaxOrderObj.CostVariantsValueIDs));
               SQLHandler sqlH = new SQLHandler();
               List<CartTaxforOrderInfo> lstCartTaxOrder= sqlH.ExecuteAsList<CartTaxforOrderInfo>("usp_Aspx_GetCartTaxforOrder", parameter);
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
               SqlConnection sqlConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
               SqlCommand sqlCmd = new SqlCommand();
               SqlDataAdapter sqlAdapter = new SqlDataAdapter();
               DataSet sqlDs = new DataSet();
               sqlCmd.Connection = sqlConn;
               sqlCmd.CommandText = "[dbo].[usp_Aspx_GetDiscountCartPriceRule]";
               sqlCmd.CommandType = CommandType.StoredProcedure;
               sqlCmd.Parameters.AddWithValue("@CartID", cartID);
               sqlCmd.Parameters.AddWithValue("@StoreID", aspxCommonObj.StoreID);
               sqlCmd.Parameters.AddWithValue("@PortalID", aspxCommonObj.PortalID);
               sqlCmd.Parameters.AddWithValue("@CultureName", aspxCommonObj.CultureName);
               sqlCmd.Parameters.AddWithValue("@UserName", aspxCommonObj.UserName);
               sqlCmd.Parameters.AddWithValue("@ShippingCost", shippingCost);
               sqlAdapter.SelectCommand = sqlCmd;
               sqlConn.Open();
               SqlDataReader dr = null;

               dr = sqlCmd.ExecuteReader();

               string discount = string.Empty;
               if (dr.Read())
               {
                   discount = dr["Discount"].ToString();

               }

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
               SqlConnection sqlConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
               SqlCommand sqlCmd = new SqlCommand();
               SqlDataAdapter sqlAdapter = new SqlDataAdapter();
               DataSet sqlDs = new DataSet();
               sqlCmd.Connection = sqlConn;
               sqlCmd.CommandText = "[dbo].[usp_Aspx_GetCartID]";
               sqlCmd.CommandType = CommandType.StoredProcedure;
               sqlCmd.Parameters.AddWithValue("@CustomerID", customerID);
               sqlCmd.Parameters.AddWithValue("@StoreID", storeID);
               sqlCmd.Parameters.AddWithValue("@PortalID", portalID);
               sqlCmd.Parameters.AddWithValue("@SessionCode", sessionCode);

               sqlAdapter.SelectCommand = sqlCmd;
               sqlConn.Open();
               SqlDataReader dr = null;

               dr = sqlCmd.ExecuteReader();

               int cartId = 0;
               if (dr.Read())
               {
                   cartId = int.Parse(dr["CartID"].ToString());

               }

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
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
               SQLHandler sqlH = new SQLHandler();
               bool isAllDownload= sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckForDownloadableItemsInCart]", parameter, "@IsAllDownloadable");
               return isAllDownload;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
  
       public static List<PaymentGatewayListInfo> GetPGList(AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
           SQLHandler sageSQL = new SQLHandler();
           List<PaymentGatewayListInfo> pginfo = sageSQL.ExecuteAsList<PaymentGatewayListInfo>("[dbo].[usp_Aspx_GetPaymentGatewayList]", paramCol);
           return pginfo;
       }
     
       public static List<PaymentGateway> GetPaymentGateway(int portalID, string cultureName, string userName)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
               parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
               SQLHandler sqlH = new SQLHandler();
               List<PaymentGateway> lstPayGateWay = sqlH.ExecuteAsList<PaymentGateway>("sp_GetPaymentGateway", parameter);
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
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
               parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
               SQLHandler sqlH = new SQLHandler();
               List<UserAddressInfo> lstUserAddress= sqlH.ExecuteAsList<UserAddressInfo>("usp_Aspx_GetUserAddressBookDetails", parameter);
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@CreditCard", creditCardNo));
               //parameter.Add(new KeyValuePair<string, object>("@IsExist", 0));
               bool isExist= sqlH.ExecuteNonQueryAsBool("usp_Aspx_CheckCreditCardBlackList", parameter, "@IsExist");
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@Email", email));
               bool isExist= sqlH.ExecuteNonQueryAsBool("usp_Aspx_CheckEmailIsAdmin", parameter, "@IsExist");
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
           // quantity = quantity == "" ? null : quantity;
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@ItemName", itemName));
               parameter.Add(new KeyValuePair<string, object>("@Quantity", quantity));
               parameter.Add(new KeyValuePair<string, object>("@TimeToAbandonCart", timeToAbandonCart));              
               SQLHandler sqlH = new SQLHandler();
               List<ShoppingCartInfo> lstShopCart = sqlH.ExecuteAsList<ShoppingCartInfo>("usp_Aspx_GetLiveCarts", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@TimeToAbandonCart", timeToAbandonCart));           
               SQLHandler sqlH = new SQLHandler();
               List<AbandonedCartInfo> bind = sqlH.ExecuteAsList<AbandonedCartInfo>("usp_Aspx_GetAbandonedCarts", parameter);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
