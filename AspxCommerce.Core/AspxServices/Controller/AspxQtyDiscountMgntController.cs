using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxQtyDiscountMgntController
    {
       public AspxQtyDiscountMgntController()
       {
       }

       public static List<ItemQuantityDiscountInfo> GetItemQuantityDiscountsByItemID(int itemId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ItemQuantityDiscountInfo> lstIQtyDiscount = AspxQtyDiscountMgntProvider.GetItemQuantityDiscountsByItemID(itemId, aspxCommonObj);
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
               AspxQtyDiscountMgntProvider.SaveItemDiscountQuantity(discountQuantity, itemID, aspxCommonObj);
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
               AspxQtyDiscountMgntProvider.DeleteItemQuantityDiscount(quantityDiscountID, itemID, aspxCommonObj);
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
               List<ItemQuantityDiscountInfo> lstIQtyDiscount = AspxQtyDiscountMgntProvider.GetItemQuantityDiscountByUserName(aspxCommonObj, itemSKU);
               return lstIQtyDiscount;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
