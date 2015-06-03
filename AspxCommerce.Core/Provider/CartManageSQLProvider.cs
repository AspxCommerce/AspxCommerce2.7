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



using System.Collections.Generic;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class CartManageSQLProvider
    {

        public bool CheckCart(int itemID, int storeID, int portalID, string userName, string cultureName)
        {

            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("[usp_Aspx_CheckCart]", parameter, "@IsExist");

        }
        public bool CheckItemCart(int itemID, int storeID, int portalID,string costvarids)
        {

            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@ItemCostVariantIDs", costvarids));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("[usp_Aspx_CheckCostVarintQuantityInCart]", parameter, "@IsAllowAddtoCart");

        }

        public void AddToCart(int itemID, int storeID, int portalID, string userName, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_AddToCart", parameter);
        }

        public List<CartInfo> GetCartDetails(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CartInfo>("usp_Aspx_GetCartDetails", parameter);
        }

        public List<CartInfo> GetCartCheckOutDetails(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));            
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CartInfo>("usp_Aspx_GetCartOverView", parameter);
        }
       
        public List<ShippingMethodInfo> GetShippingMethodByWeight(int storeID, int portalID, int customerID, string userName, string cultureName, string sessionCode)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ShippingMethodInfo>("usp_Aspx_GetShippingMethodByTotalWeight", parameter);
        }

        public decimal GetTotalShippingCost(int shippingMethodID, int storeID, int portalID, string userName, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsScalar<decimal>("usp_Aspx_ShippingCost", parameter);
        }

        public void UpdateShoppingCart(UpdateCartInfo updateCartObj, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CartID", updateCartObj.CartID));
            parameter.Add(new KeyValuePair<string, object>("@CartItemIDs", updateCartObj.CartItemIDs));
            parameter.Add(new KeyValuePair<string, object>("@Quantities", updateCartObj.Quantities));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@AllowOutOfStock", updateCartObj.AllowOutOfStock));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_UpdateShoppingCart", parameter);
        }

        public void DeleteCartItem(int cartID, int cartItemID,AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CartID", cartID));
            parameter.Add(new KeyValuePair<string, object>("@CartItemID", cartItemID));
            parameter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            parameter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteCartItem", parameter);
        }

        public void ClearAllCartItems(int cartID, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CartID", cartID));
            parameter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            parameter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_ClearCartItems", parameter);
        }

        public void ClearCartAfterPayment(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            parameter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_ClearCartAfterPayment", parameter);
        }
    }
}
