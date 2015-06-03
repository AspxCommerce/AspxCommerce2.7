using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
  public class AspxItemViewListController
    {
      public AspxItemViewListController()
      {
      }

      public static List<CategoryDetailsOptionsInfo> GetLatestItemsDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy,int rowTotal)
      {
          try
          {
              List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListProvider.GetLatestItemsDetails(offset, limit, aspxCommonObj, sortBy,rowTotal);
              return lstCatDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<CategoryDetailsOptionsInfo> GetGiftCardItemsDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
      {
          try
          {
              List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListProvider.GetGiftCardItemsDetails(offset, limit, aspxCommonObj, sortBy,rowTotal);
              return lstCatDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<CategoryDetailsOptionsInfo> GetBestSoldItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
      {
          try
          {
              List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListProvider.GetBestSoldItemDetails(offset, limit, aspxCommonObj, sortBy,rowTotal);
              return lstCatDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }
      public static List<CategoryDetailsOptionsInfo> GetRecentlyViewedItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
      {
          try
          {
              List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListProvider.GetRecentlyViewedItemDetails(offset, limit, aspxCommonObj, sortBy,rowTotal);
              return lstCatDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<CategoryDetailsOptionsInfo> GetSpecialItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
      {
          try
          {
              List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListProvider.GetSpecialItemDetails(offset, limit, aspxCommonObj, sortBy,rowTotal);
              return lstCatDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<CategoryDetailsOptionsInfo> GetFeatureItemDetails(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
      {
          try
          {
              List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListProvider.GetFeatureItemDetails(offset, limit, aspxCommonObj, sortBy,rowTotal);
              return lstCatDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }
      public static List<CategoryDetailsOptionsInfo> GetAllHeavyDiscountItems(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
      {
          try
          {
              List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListProvider.GetAllHeavyDiscountItems(offset, limit, aspxCommonObj, sortBy,rowTotal);
              return lstCatDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }
      public static List<CategoryDetailsOptionsInfo> GetAllSeasonalItems(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy)
      {
          try
          {
              List<CategoryDetailsOptionsInfo> lstCatDetail = AspxItemViewListProvider.GetAllSeasonalItems(offset, limit, aspxCommonObj, sortBy);
              return lstCatDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      } 
    }
}
