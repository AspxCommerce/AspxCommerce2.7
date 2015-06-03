using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class PriceHistoryController
    {
       public static List<PriceHistoryInfo> GetPriceHistory(int itemId,AspxCommonInfo aspxCommerceObj)
       {
           List<PriceHistoryInfo> list = PriceHistoryPrivider.GetPriceHistory(itemId, aspxCommerceObj);
           return list;
       }
       public static List<PriceHistoryInfo> BindPriceHistory(int offset,int limit, AspxCommonInfo aspxCommerceObj,string itemName,string userName)
       {
           List<PriceHistoryInfo> list = PriceHistoryPrivider.BindPriceHistory(offset, limit, aspxCommerceObj,itemName,userName);
           return list;
       } 
    }
}
