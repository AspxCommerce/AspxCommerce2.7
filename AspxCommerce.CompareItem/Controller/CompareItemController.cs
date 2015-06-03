using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AspxCommerce.CompareItem
{
    public class CompareItemController
    {

        public CompareItemController() { }
       

       public  int SaveCompareItems(SaveCompareItemInfo saveCompareItemObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               CompareItemProvider provider = new CompareItemProvider();
               int compareID = provider.SaveCompareItems(saveCompareItemObj, aspxCommonObj);
               return compareID;
           }
           catch (Exception e)
           {
               throw e;
           }

       }

       public  List<ItemsCompareInfo> GetItemCompareList(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               CompareItemProvider provider = new CompareItemProvider();
               List<ItemsCompareInfo> lstItemCompare = provider.GetItemCompareList(aspxCommonObj);
               return lstItemCompare;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public  void DeleteCompareItem(int compareItemID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               CompareItemProvider provider = new CompareItemProvider();
               provider.DeleteCompareItem(compareItemID, aspxCommonObj);

           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public  void ClearAll(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               CompareItemProvider provider = new CompareItemProvider();
               provider.ClearAll(aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public  bool CheckCompareItems(int ID, AspxCommonInfo aspxCommonObj, string costVariantValueIDs)
       {
           try
           {
               CompareItemProvider provider = new CompareItemProvider();
               bool isExist = provider.CheckCompareItems(ID, aspxCommonObj, costVariantValueIDs);
               return isExist;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public  List<ItemBasicDetailsInfo> GetCompareListImage(string itemIDs, string CostVariantValueIDs, AspxCommonInfo aspxCommonObj)
       {
           CompareItemProvider provider = new CompareItemProvider();
           List<ItemBasicDetailsInfo> lstItemBasic = provider.GetCompareListImage(itemIDs, CostVariantValueIDs, aspxCommonObj);
           return lstItemBasic;
       }
       public  ItemsCompareInfo GetItemDetailsForCompare(int ItemID, AspxCommonInfo aspxCommonObj, string costVariantValueIDs)
       {
           try
           {
               CompareItemProvider provider = new CompareItemProvider();
               ItemsCompareInfo objItemDetails = provider.GetItemDetailsForCompare(ItemID, aspxCommonObj,
                                                                                                  costVariantValueIDs);
               return objItemDetails;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }


       public  List<CompareItemListInfo> GetCompareList(string itemIDs, string CostVariantValueIDs, AspxCommonInfo aspxCommonObj)
       {
           CompareItemProvider provider = new CompareItemProvider();
           List<CompareItemListInfo> lstCompItem = provider.GetCompareList(itemIDs, CostVariantValueIDs, aspxCommonObj);
           return lstCompItem;
       }

       #region RecentlyComparedProducts

       public  void AddComparedItems(string IDs, string CostVarinatIds, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               CompareItemProvider provider = new CompareItemProvider();
               provider.AddComparedItems(IDs, CostVarinatIds, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public  List<ItemsCompareInfo> GetRecentlyComparedItemList(int count, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               CompareItemProvider provider = new CompareItemProvider();
               List<ItemsCompareInfo> lstCompItem = provider.GetRecentlyComparedItemList(count, aspxCommonObj);
               return lstCompItem;

           }
           catch (Exception e)
           {
               throw e;
           }
       }
       #endregion


       public CompareItemsSettingInfo GetCompareItemsSetting(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               CompareItemProvider cip = new CompareItemProvider();
               CompareItemsSettingInfo compItemSettingInfo = new CompareItemsSettingInfo();
               compItemSettingInfo = cip.GetCompareItemsSetting(aspxCommonObj);
               return compItemSettingInfo;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static DataSet GetCompareItemsDataSet(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               return CompareItemProvider.GetCompareItemsDataSet(aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public void SaveAndUpdateCompareItemsSetting(AspxCommonInfo aspxCommonObj, CompareItemsSettingKeyPairInfo compareItems)
       {
           CompareItemProvider cip = new CompareItemProvider();
           cip.SaveAndUpdateCompareItemsSetting(aspxCommonObj, compareItems);
       }
    }
}
