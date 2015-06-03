using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
  public class AspxTagsController
    {
      public AspxTagsController()
      {
      }
      #region PopularTags Module

      public static void AddTagsOfItem(string itemSKU, string tags, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxTagsProvider.AddTagsOfItem(itemSKU, tags, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }


      public static List<ItemTagsInfo> GetItemTags(string itemSKU, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ItemTagsInfo> lstItemTags = AspxTagsProvider.GetItemTags(itemSKU, aspxCommonObj);
              return lstItemTags;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static void DeleteUserOwnTag(string itemTagID, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxTagsProvider.DeleteUserOwnTag(itemTagID, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static void DeleteMultipleTag(string itemTagIDs, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxTagsProvider.DeleteMultipleTag(itemTagIDs, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<TagDetailsInfo> GetTagDetailsListPending(int offset, int limit, string tag, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<TagDetailsInfo> lstTagDetail = AspxTagsProvider.GetTagDetailsListPending(offset, limit, tag, aspxCommonObj);
              return lstTagDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<TagDetailsInfo> GetTagDetailsList(int offset, int limit, string tag, string tagStatus, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<TagDetailsInfo> lstTagDetail = AspxTagsProvider.GetTagDetailsList(offset, limit, tag, tagStatus, aspxCommonObj);
              return lstTagDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<TagDetailsInfo> GetAllPopularTags(AspxCommonInfo aspxCommonObj, int count)
      {
          try
          {
              List<TagDetailsInfo> lstTagDetail = AspxTagsProvider.GetAllPopularTags(aspxCommonObj, count);
              return lstTagDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<TagDetailsInfo> GetTagsByUserName(AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<TagDetailsInfo> lstTagDetail = AspxTagsProvider.GetTagsByUserName(aspxCommonObj);
              return lstTagDetail;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      #region Tags Reports
      //---------------------Customer tags------------

      public static List<CustomerTagInfo> GetCustomerTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<CustomerTagInfo> lstCustomerTag = AspxTagsProvider.GetCustomerTagDetailsList(offset, limit, aspxCommonObj);
              return lstCustomerTag;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //---------------------Show Customer tags list------------

      public static List<ShowCustomerTagsListInfo> ShowCustomerTagList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ShowCustomerTagsListInfo> lstCustTag = AspxTagsProvider.ShowCustomerTagList(offset, limit, aspxCommonObj);
              return lstCustTag;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //---------------------Item tags details------------

      public static List<ItemTagsDetailsInfo> GetItemTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ItemTagsDetailsInfo> lstItemTags = AspxTagsProvider.GetItemTagDetailsList(offset, limit, aspxCommonObj);
              return lstItemTags;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //---------------------Show Item tags list------------

      public static List<ShowItemTagsListInfo> ShowItemTagList(int offset, System.Nullable<int> limit, int itemID, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ShowItemTagsListInfo> lstShowItemTags = AspxTagsProvider.ShowItemTagList(offset, limit, itemID, aspxCommonObj);
              return lstShowItemTags;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //---------------------Popular tags details------------

      public static List<PopularTagsInfo> GetPopularTagDetailsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<PopularTagsInfo> lstPopTags = AspxTagsProvider.GetPopularTagDetailsList(offset, limit, aspxCommonObj);
              return lstPopTags;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //---------------------Show Popular tags list------------

      public static List<ShowpopulartagsDetailsInfo> ShowPopularTagList(int offset, System.Nullable<int> limit, string tagName, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ShowpopulartagsDetailsInfo> lstShowPopTag = AspxTagsProvider.ShowPopularTagList(offset, limit, tagName, aspxCommonObj);
              return lstShowPopTag;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //public static List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset, int limit, string tagIDs, int SortBy,int rowTotal, AspxCommonInfo aspxCommonObj)
      //{
      //    try
      //    {
      //        List<ItemBasicDetailsInfo> lstItemBasic = AspxTagsProvider.GetUserTaggedItems(offset, limit, tagIDs, SortBy,rowTotal, aspxCommonObj);
      //        return lstItemBasic;
      //    }
      //    catch (Exception e)
      //    {
      //        throw e;
      //    }
      //}
      #endregion
      #endregion

      #region Tags Management

      public static void UpdateTag(string itemTagIDs, int? itemId, int statusID, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxTagsProvider.UpdateTag(itemTagIDs, itemId, statusID, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static void DeleteTag(string itemTagIDs, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxTagsProvider.DeleteTag(itemTagIDs, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<ItemBasicDetailsInfo> GetItemsByMultipleItemID(string itemIDs,string tagName, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ItemBasicDetailsInfo> lstItemBasic = AspxTagsProvider.GetItemsByMultipleItemID(itemIDs,tagName, aspxCommonObj);
              return lstItemBasic;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      #endregion
    }
}
