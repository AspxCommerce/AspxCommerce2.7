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
   public class PriceHistoryPrivider
    {
       public static List<PriceHistoryInfo> GetPriceHistory(int itemId,AspxCommonInfo aspxCommerceObj)
       {
           try
           {
              List<KeyValuePair<string,object>> parameter=new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@StoreID",aspxCommerceObj.StoreID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID",aspxCommerceObj.PortalID));
               parameter.Add(new KeyValuePair<string, object>("@ItemID",itemId));
               SQLHandler sqlH = new SQLHandler();
               List<PriceHistoryInfo> list = sqlH.ExecuteAsList<PriceHistoryInfo>("[dbo].[usp_Aspx_GetPriceHistoryByItemID]", parameter);
               return list;
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }
       public static List<PriceHistoryInfo> BindPriceHistory(int offset,int limit, AspxCommonInfo aspxCommerceObj,string itemName,string userName)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@offset",offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommerceObj.StoreID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommerceObj.PortalID));
               parameter.Add(new KeyValuePair<string, object>("@ItemName",itemName));
               parameter.Add(new KeyValuePair<string, object>("@UserName",userName));
               SQLHandler sqlH = new SQLHandler();
               List<PriceHistoryInfo> list = sqlH.ExecuteAsList<PriceHistoryInfo>("[dbo].[usp_Aspx_GetPriceHistoryList]", parameter);
               return list;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
