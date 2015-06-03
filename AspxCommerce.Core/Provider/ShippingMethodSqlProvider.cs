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

namespace AspxCommerce.Core
{
    public class ShippingMethodSqlProvider
    {
        public ShippingMethodSqlProvider()
        {
        }

        public List<ShippingMethodInfoByProvider> GetShippingMethodsByProvider(int offset, int limit, int shippingProviderId,AspxCommonInfo aspxCommonObj)
        {
           
            var sqlH = new SQLHandler();
           var parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@ShippingProviderId", shippingProviderId));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            var shipping = sqlH.ExecuteAsList<ShippingMethodInfoByProvider>("usp_Aspx_GetShippingMethodbyProvider", parameterCollection);
            return shipping;
        }

        public List<ShippingMethodInfo> GetShippingMethods(int offset, int limit,ShippingMethodInfoByProvider shippingMethodObj,AspxCommonInfo aspxCommonObj)
        {
            List<ShippingMethodInfo> shipping;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@ShippingMethodName", shippingMethodObj.ShippingMethodName));
            parameterCollection.Add(new KeyValuePair<string, object>("@DeliveryTime", shippingMethodObj.DeliveryTime));
            parameterCollection.Add(new KeyValuePair<string, object>("@WeightLimitFrom", shippingMethodObj.WeightLimitFrom));
            parameterCollection.Add(new KeyValuePair<string, object>("@WeightLimitTo", shippingMethodObj.WeightLimitTo));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", shippingMethodObj.IsActive));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            shipping = sqlH.ExecuteAsList<ShippingMethodInfo>("usp_Aspx_BindShippingMethodInGrid", parameterCollection);
            return shipping;
        }

        public void DeleteShippings(string shippingMethodIds,AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ShippingMethodIDs", shippingMethodIds));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteShippingMethods", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveAndUpdateShippings(int shippingMethodID, string shippingMethodName, string imagePath, string alternateText, int displayOrder, string deliveryTime,
            decimal weightLimitFrom, decimal weightLimitTo, int shippingProviderID,  bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {

                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodName", shippingMethodName));
                parameter.Add(new KeyValuePair<string, object>("@ImagePath", imagePath));
                parameter.Add(new KeyValuePair<string, object>("@AlternateText", alternateText));
                parameter.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                parameter.Add(new KeyValuePair<string, object>("@DeliveryTime", deliveryTime));
                parameter.Add(new KeyValuePair<string, object>("@WeightLimitFrom", weightLimitFrom));
                parameter.Add(new KeyValuePair<string, object>("@WeightLimitTo", weightLimitTo));
                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", shippingProviderID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveAndUpdateShippingMethods", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddCostDependencies(int shippingProductCostID, int shippingMethodID, string costDependenciesOptions, AspxCommonInfo aspxCommonObj)
        {
            try
            {

                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ShippingProductCostID", shippingProductCostID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                parameter.Add(new KeyValuePair<string, object>("@CostDependenciesOptions", costDependenciesOptions));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));

                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveCostDependencies", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddWeightDependencies(int shippingProductWeightID, int shippingMethodID, string weightDependenciesOptions, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ShippingProductWeightID", shippingProductWeightID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                parameter.Add(new KeyValuePair<string, object>("@WeightDependenciesOptions", weightDependenciesOptions));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveWeightDependencies", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddItemDependencies(int shippingItemID, int shippingMethodID, string itemDependenciesOptions, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ShippingItemID", shippingItemID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                parameter.Add(new KeyValuePair<string, object>("@ItemDependenciesOptions", itemDependenciesOptions));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveItemDependencies", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
