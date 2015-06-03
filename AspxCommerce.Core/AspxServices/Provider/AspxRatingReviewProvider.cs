using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxRatingReviewProvider
    {
       public AspxRatingReviewProvider()
       {
       }

       #region rating/ review
       public static List<ItemRatingAverageInfo> GetItemAverageRating(string itemSKU, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
               List<ItemRatingAverageInfo> avgRating = sqlH.ExecuteAsList<ItemRatingAverageInfo>("usp_Aspx_ItemRatingGetAverage", parameter);
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
               parameter.Add(new KeyValuePair<string, object>("@IsFlag", isFlag));
               List<RatingCriteriaInfo> rating = sqlH.ExecuteAsList<RatingCriteriaInfo>("usp_Aspx_GetItemRatingCriteriaForPending", parameter);
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@RatingCriteriaValue", ratingSaveObj.ItemRatingCriteria));
               parameter.Add(new KeyValuePair<string, object>("@StatusID", ratingSaveObj.StatusID));
               parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", 0));
               parameter.Add(new KeyValuePair<string, object>("@ReviewSummary", ratingSaveObj.ReviewSummary));
               parameter.Add(new KeyValuePair<string, object>("@Review", ratingSaveObj.Review));
               parameter.Add(new KeyValuePair<string, object>("@ViewFromIP", ratingSaveObj.ViewFromIP));
               parameter.Add(new KeyValuePair<string, object>("@ViewFromCountry", ratingSaveObj.viewFromCountry));
               parameter.Add(new KeyValuePair<string, object>("@ItemID", ratingSaveObj.ItemID));
               parameter.Add(new KeyValuePair<string, object>("@UserName", ratingSaveObj.UserName));
               parameter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
               sqlH.ExecuteNonQuery("usp_Aspx_SaveItemRating", parameter);
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
               List<ItemRatingByUserInfo> lstItemRating= sqlH.ExecuteAsList<ItemRatingByUserInfo>("usp_Aspx_GetItemAverageRatingByUser", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemReviewIDs", itemReviewIDs));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleSelectionItemRating", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userRatingObj.UserName));
               parameter.Add(new KeyValuePair<string, object>("@StatusName", userRatingObj.Status));
               parameter.Add(new KeyValuePair<string, object>("@ItemName", userRatingObj.ItemName));             
               SQLHandler sqlH = new SQLHandler();
               List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetAllReviewsAndRatings", parameter);
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@searchText", searchText));
               List<ItemsReviewInfo> items = sqlH.ExecuteAsList<ItemsReviewInfo>("usp_Aspx_GetAllItemsListReview", parameter);
               return items;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool CheckReviewByUser(int itemID, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
           SQLHandler sqlH = new SQLHandler();
           bool isReviewExist= sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckReviewAlreadyExist", parameter, "@IsReviewAlreadyExist");
           return isReviewExist;
       }
       
       public static bool CheckReviewByIP(int itemID, AspxCommonInfo aspxCommonObj, string userIP)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
           parameter.Add(new KeyValuePair<string, object>("@UserIP", userIP));
           SQLHandler sqlH = new SQLHandler();
           bool isReviewExist= sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckReviewAlreadyExist", parameter, "@IsReviewAlreadyExist");
           return isReviewExist;
       }
       public static ReviewStatusInfo GetUserReviewStatus(int itemID, AspxCommonInfo aspxCommonObj, string userIP)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
           parameter.Add(new KeyValuePair<string, object>("@UserIP", userIP));
           SQLHandler sqlH = new SQLHandler();
           return sqlH.ExecuteAsObject<ReviewStatusInfo>("[usp_Aspx_CheckReviewExistByIPUser]", parameter);

       }
         

       #endregion

       #region Item Rating Criteria Manage/Admin
       public static List<ItemRatingCriteriaInfo> ItemRatingCriteriaManage(int offset, int limit, ItemRatingCriteriaInfo itemCriteriaObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@RatingCriteria", itemCriteriaObj.ItemRatingCriteria));
               parameter.Add(new KeyValuePair<string, object>("@IsActive", itemCriteriaObj.IsActive));
               SQLHandler sqlH = new SQLHandler();
               List<ItemRatingCriteriaInfo> lstRatingCriteria= sqlH.ExecuteAsList<ItemRatingCriteriaInfo>("usp_Aspx_GetAllItemRatingCriteria", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ID", itemCriteriaObj.ItemRatingCriteriaID));
               parameter.Add(new KeyValuePair<string, object>("@Criteria", itemCriteriaObj.ItemRatingCriteria));
               parameter.Add(new KeyValuePair<string, object>("@IsActive", itemCriteriaObj.IsActive));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateItemRatingCriteria", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@CriteriaID", IDs));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteItemRatingCriteria", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               SQLHandler sqlH = new SQLHandler();
               List<CustomerReviewReportsInfo> bind = sqlH.ExecuteAsList<CustomerReviewReportsInfo>("usp_Aspx_GetCustomerReviews", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@User", customerReviewObj.UserName));
               parameter.Add(new KeyValuePair<string, object>("@StatusName", customerReviewObj.Status));
               parameter.Add(new KeyValuePair<string, object>("@ItemName", customerReviewObj.ItemName));
               SQLHandler sqlH = new SQLHandler();
               List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetCustomerWiseReviewsList", parameter);
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
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               SQLHandler sqlH = new SQLHandler();
               List<UserListInfo> lstUser = sqlH.ExecuteAsList<UserListInfo>("usp_PortalUserListGet", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               SQLHandler sqlH = new SQLHandler();
               List<ItemReviewsInfo> bind = sqlH.ExecuteAsList<ItemReviewsInfo>("usp_Aspx_GetItemReviewsList", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemReviewObj.ItemID));
               parameter.Add(new KeyValuePair<string, object>("@UserName", itemReviewObj.UserName));
               parameter.Add(new KeyValuePair<string, object>("@StatusName", itemReviewObj.Status));
               parameter.Add(new KeyValuePair<string, object>("@ItemName", itemReviewObj.ItemName));
               SQLHandler sqlH = new SQLHandler();
               List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetItemWiseReviewsList", parameter);
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
