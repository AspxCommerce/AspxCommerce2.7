using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
  public class AspxRatingReviewController
    {
      public AspxRatingReviewController()
      {
      }
      #region rating/ review
      public static List<ItemRatingAverageInfo> GetItemAverageRating(string itemSKU, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ItemRatingAverageInfo> avgRating = AspxRatingReviewProvider.GetItemAverageRating(itemSKU, aspxCommonObj);
              return avgRating;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<RatingCriteriaInfo> GetItemRatingCriteriaByReviewID(AspxCommonInfo aspxCommonObj, int itemReviewID, bool isFlag)
      {
          try
          {
              List<RatingCriteriaInfo> rating = AspxRatingReviewProvider.GetItemRatingCriteriaByReviewID(aspxCommonObj, itemReviewID, isFlag);
              return rating;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static void SaveItemRating(ItemReviewBasicInfo ratingSaveObj, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxRatingReviewProvider.SaveItemRating(ratingSaveObj, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<ItemRatingByUserInfo> GetItemRatingPerUser(int offset, int limit, string itemSKU, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ItemRatingByUserInfo> lstItemRating = AspxRatingReviewProvider.GetItemRatingPerUser(offset, limit, itemSKU, aspxCommonObj);
              return lstItemRating;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static void DeleteMultipleItemRatings(string itemReviewIDs, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxRatingReviewProvider.DeleteMultipleItemRatings(itemReviewIDs, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<UserRatingInformationInfo> GetAllUserReviewsAndRatings(int offset, int limit, UserRatingBasicInfo userRatingObj, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<UserRatingInformationInfo> bind = AspxRatingReviewProvider.GetAllUserReviewsAndRatings(offset, limit, userRatingObj, aspxCommonObj);
              return bind;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<ItemsReviewInfo> GetAllItemList(string searchText,AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ItemsReviewInfo> items = AspxRatingReviewProvider.GetAllItemList(searchText,aspxCommonObj);
              return items;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static bool CheckReviewByUser(int itemID, AspxCommonInfo aspxCommonObj)
      {
          bool isReviewExist = AspxRatingReviewProvider.CheckReviewByUser(itemID, aspxCommonObj);
          return isReviewExist;
      }

      public static bool CheckReviewByIP(int itemID, AspxCommonInfo aspxCommonObj, string userIP)
      {
          bool isReviewExist = AspxRatingReviewProvider.CheckReviewByIP(itemID, aspxCommonObj, userIP);
          return isReviewExist;
      }

      public static ReviewStatusInfo GetUserReviewStatus(int itemID, AspxCommonInfo aspxCommonObj, string userIP)
      {
          return AspxRatingReviewProvider.GetUserReviewStatus(itemID, aspxCommonObj, userIP);
         
      }



      #endregion

      #region Item Rating Criteria Manage/Admin
      public static List<ItemRatingCriteriaInfo> ItemRatingCriteriaManage(int offset, int limit, ItemRatingCriteriaInfo itemCriteriaObj, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ItemRatingCriteriaInfo> lstRatingCriteria = AspxRatingReviewProvider.ItemRatingCriteriaManage(offset, limit, itemCriteriaObj, aspxCommonObj);
              return lstRatingCriteria;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static void AddUpdateItemCriteria(ItemRatingCriteriaInfo itemCriteriaObj, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxRatingReviewProvider.AddUpdateItemCriteria(itemCriteriaObj, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static void DeleteItemRatingCriteria(string IDs, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxRatingReviewProvider.DeleteItemRatingCriteria(IDs, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }
      #endregion

      #region Rating Reviews Reporting

      public static List<CustomerReviewReportsInfo> GetCustomerReviews(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<CustomerReviewReportsInfo> bind = AspxRatingReviewProvider.GetCustomerReviews(offset, limit, aspxCommonObj);
              return bind;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<UserRatingInformationInfo> GetAllCustomerReviewsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, UserRatingBasicInfo customerReviewObj)
      {
          try
          {
              List<UserRatingInformationInfo> bind = AspxRatingReviewProvider.GetAllCustomerReviewsList(offset, limit, aspxCommonObj, customerReviewObj);
              return bind;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<UserListInfo> GetUserList(int portalID)
      {
          try
          {
              List<UserListInfo> lstUser = AspxRatingReviewProvider.GetUserList(portalID);
              return lstUser;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<ItemReviewsInfo> GetItemReviews(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<ItemReviewsInfo> bind = AspxRatingReviewProvider.GetItemReviews(offset, limit, aspxCommonObj);
              return bind;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<UserRatingInformationInfo> GetAllItemReviewsList(int offset, System.Nullable<int> limit, UserRatingBasicInfo itemReviewObj, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<UserRatingInformationInfo> bind = AspxRatingReviewProvider.GetAllItemReviewsList(offset, limit, itemReviewObj, aspxCommonObj);
              return bind;
          }
          catch (Exception e)
          {
              throw e;
          }
      }
      #endregion
    }
}
