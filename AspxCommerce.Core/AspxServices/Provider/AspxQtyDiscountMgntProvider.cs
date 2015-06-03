using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxQtyDiscountMgntProvider
    {
       public AspxQtyDiscountMgntProvider()
       {
       }    
       public static List<ItemQuantityDiscountInfo> GetItemQuantityDiscountsByItemID(int itemId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
               List<ItemQuantityDiscountInfo> lstIQtyDiscount= sqlH.ExecuteAsList<ItemQuantityDiscountInfo>("usp_Aspx_GetQuantityDiscountByItemID", parameter);
               return lstIQtyDiscount;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //------------------------save quantity discount------------------
       
       public static void SaveItemDiscountQuantity(string discountQuantity, int itemID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@DiscountQuantity", discountQuantity));
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
               sqlH.ExecuteNonQuery("usp_Aspx_SaveItemQuantityDiscounts", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //------------------------delete quantity discount------------------
       
       public static void DeleteItemQuantityDiscount(int quantityDiscountID, int itemID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@QuantityDiscountID", quantityDiscountID));
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteItemQuantityDiscounts", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //------------------------quantity discount shown in Item deatils ------------------

       public static List<ItemQuantityDiscountInfo> GetItemQuantityDiscountByUserName(AspxCommonInfo aspxCommonObj, string itemSKU)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
               List<ItemQuantityDiscountInfo> lstIQtyDiscount = sqlH.ExecuteAsList<ItemQuantityDiscountInfo>("usp_Aspx_GetItemQuantityDiscountByUserName", parameter);
               return lstIQtyDiscount;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
