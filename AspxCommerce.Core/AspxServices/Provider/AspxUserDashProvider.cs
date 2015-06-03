using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxUserDashProvider
    {
       public AspxUserDashProvider()
       {
       }

       #region Shared Wishlists
       //--------------------bind ShareWishList Email  in Grid--------------------------
       public static List<ShareWishListItemInfo> GetAllShareWishListItemMail(int offset, int limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               SQLHandler sqlH = new SQLHandler();
               List<ShareWishListItemInfo> lstShareWishItem= sqlH.ExecuteAsList<ShareWishListItemInfo>("[dbo].[usp_Aspx_GetShareWishListMailDetailGrid]", parameter);
               return lstShareWishItem;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<ShareWishListItemInfo> GetShareWishListItemByID(int sharedWishID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@SharedWishID", sharedWishID));
               SQLHandler sqlH = new SQLHandler();
               List<ShareWishListItemInfo> lstShareWishItem= sqlH.ExecuteAsList<ShareWishListItemInfo>("[dbo].[usp_Aspx_GetShareWishListByID]", parameter);
               return lstShareWishItem;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-----------------Delete ShareWishList --------------------------------
       
       public static void DeleteShareWishListItem(string shareWishListID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ShareWishIDs", shareWishListID));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("[usp_Aspx_DeleteShareWishList]", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       #endregion

       //-------------------------Update Customer Account Information----------------------------------------  
       
       public static int UpdateCustomer(AspxCommonInfo aspxCommonObj, string firstName, string lastName, string email)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUCt(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@FirstName", firstName));
               parameterCollection.Add(new KeyValuePair<string, object>("@LastName", lastName));
               parameterCollection.Add(new KeyValuePair<string, object>("@Email", email));
               SQLHandler sqlh = new SQLHandler();
               int errorCode = sqlh.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_UpdateCustomer", parameterCollection, "@ErrorCode");
               return errorCode;
           }
           catch (Exception)
           {
               throw;
           }
       }

       //---------------User Item Reviews and Ratings-----------------------
       
       public static List<UserRatingInformationInfo> GetUserReviewsAndRatings(int offset, int limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               SQLHandler sqlH = new SQLHandler();
               List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetUserItemReviews", parameter);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //---------------------update rating/ review Items From User DashBoard-----------------------
       
       public static void UpdateItemRatingByUser(ItemReviewBasicInfo updateItemRatingObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ReviewSummary", updateItemRatingObj.ReviewSummary));
               parameter.Add(new KeyValuePair<string, object>("@Review", updateItemRatingObj.Review));
               parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", updateItemRatingObj.ItemReviewID));
               parameter.Add(new KeyValuePair<string, object>("@ItemID", updateItemRatingObj.ItemID));
               parameter.Add(new KeyValuePair<string, object>("@UserBy", aspxCommonObj.UserName));
               sqlH.ExecuteNonQuery("usp_Aspx_UpdateItemRatingByUser", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-----------User DashBoard/Recent History-------------------
       
       public static List<UserRecentHistoryInfo> GetUserRecentlyViewedItems(int offset, int limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               SQLHandler sqlH = new SQLHandler();
               List<UserRecentHistoryInfo> lstUserHistory= sqlH.ExecuteAsList<UserRecentHistoryInfo>("usp_Aspx_GetUserRecentlyViewedItems", parameter);
               return lstUserHistory;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-----------User DashBoard/Recent History-------------------
       
       public static List<UserRecentCompareInfo> GetUserRecentlyComparedItems(int offset, int limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               SQLHandler sqlH = new SQLHandler();
               List<UserRecentCompareInfo> lstUserRCompare= sqlH.ExecuteAsList<UserRecentCompareInfo>("usp_Aspx_GetUserRecentlyComparedItems", parameter);
               return lstUserRCompare;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void AddUpdateUserAddress(AddressInfo addressObj, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFParamNoSCode(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@AddressID", addressObj.AddressID));
           parameter.Add(new KeyValuePair<string, object>("@FirstName", addressObj.FirstName));
           parameter.Add(new KeyValuePair<string, object>("@LastName", addressObj.LastName));
           parameter.Add(new KeyValuePair<string, object>("@Email", addressObj.Email));
           parameter.Add(new KeyValuePair<string, object>("@Company", addressObj.Company));
           parameter.Add(new KeyValuePair<string, object>("@Address1", addressObj.Address1));
           parameter.Add(new KeyValuePair<string, object>("@Address2", addressObj.Address2));
           parameter.Add(new KeyValuePair<string, object>("@City", addressObj.City));
           parameter.Add(new KeyValuePair<string, object>("@State", addressObj.State));
           parameter.Add(new KeyValuePair<string, object>("@Zip", addressObj.Zip));
           parameter.Add(new KeyValuePair<string, object>("@Phone", addressObj.Phone));
           parameter.Add(new KeyValuePair<string, object>("@Mobile", addressObj.Mobile));
           parameter.Add(new KeyValuePair<string, object>("@Fax", addressObj.Fax));
           parameter.Add(new KeyValuePair<string, object>("@WebSite", addressObj.Website));
           parameter.Add(new KeyValuePair<string, object>("@Country", addressObj.Country));
           parameter.Add(new KeyValuePair<string, object>("@IsDefaultShipping", addressObj.DefaultShipping));
           parameter.Add(new KeyValuePair<string, object>("@IsDefaultBilling", addressObj.DefaultBilling));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateUserAddress", parameter);
       }

       public static List<AddressInfo> GetUserAddressDetails(AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFParamNoSCode(aspxCommonObj);
           SQLHandler sqlh = new SQLHandler();
           List<AddressInfo> lstAddress= sqlh.ExecuteAsList<AddressInfo>("usp_Aspx_GetUserAddressBookDetails", parameter);
           return lstAddress;
       }

       public static void DeleteAddressBookDetails(int addressID, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@AddressID", addressID));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_DeleteAddressBook", parameter);
       }

       public static List<UserProductReviewInfo> GetUserProductReviews(AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           SQLHandler sqlH = new SQLHandler();
           List<UserProductReviewInfo> lstUPReview= sqlH.ExecuteAsList<UserProductReviewInfo>("usp_Aspx_GetUserProductReviews", parameter);
           return lstUPReview;
       }

       public static void UpdateUserProductReview(ItemReviewBasicInfo productReviewObj, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@ItemID", productReviewObj.ItemID));
           parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", productReviewObj.ItemReviewID));
           parameter.Add(new KeyValuePair<string, object>("@RatingIDs", productReviewObj.RatingIDs));
           parameter.Add(new KeyValuePair<string, object>("@RatingValues", productReviewObj.RatingValues));
           parameter.Add(new KeyValuePair<string, object>("@ReviewSummary", productReviewObj.ReviewSummary));
           parameter.Add(new KeyValuePair<string, object>("@Review", productReviewObj.Review));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_GetUserProductReviewUpdate", parameter);

       }

       public static void DeleteUserProductReview(int itemID, int itemReviewID, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
           parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_DeleteUserProductReview", parameter);
       }

       //---------------userDashBord/My Order List in grid----------------------------
       
       public static List<MyOrderListInfo> GetMyOrderList(int offset, int limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPCtC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               SQLHandler sqlH = new SQLHandler();
               List<MyOrderListInfo> lstMyOrder= sqlH.ExecuteAsList<MyOrderListInfo>("usp_Aspx_GetMyOrdersList", parameter);
               return lstMyOrder;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<ReOrderItemsInfo> GetMyOrdersforReOrder(int orderID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFParamNoSCode(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
               SQLHandler sqlh = new SQLHandler();    
               List<ReOrderItemsInfo> info = sqlh.ExecuteAsList<ReOrderItemsInfo>("usp_Aspx_GetMyOrdersforReOrder", parameter);
               return info;
           }

           catch (Exception e)
           {
               throw e;
           }
       }
   
       public static decimal CheckItemQuantity(int itemID, AspxCommonInfo aspxCommonObj, string itemCostVariantIDs)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPSCt(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
               parameter.Add(new KeyValuePair<string, object>("@ItemCostVariantIDs", itemCostVariantIDs));
               SQLHandler sqlH = new SQLHandler();
               decimal retValue= sqlH.ExecuteAsScalar<decimal>("usp_Aspx_CheckItemQuantity", parameter);
               return retValue;

           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<ReturnReasonListInfo> BindReturnReasonList(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<ReturnReasonListInfo> lstRRList = sqlH.ExecuteAsList<ReturnReasonListInfo>("usp_Aspx_BindReturnReasonList", parameter);
               return lstRRList;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static List<ProductStatusListInfo> BindProductStatusList(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<ProductStatusListInfo> lstPdtStatus= sqlH.ExecuteAsList<ProductStatusListInfo>("usp_Aspx_BindProductStatusList", parameter);
               return lstPdtStatus;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
  
       public static List<ReturnStatusInfo> GetReturnStatusList(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<ReturnStatusInfo> lstRetStatus= sqlH.ExecuteAsList<ReturnStatusInfo>("usp_Aspx_BindReturnStatusList", parameter);
               return lstRetStatus;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
      
       public static List<ReturnActionInfo> GetReturnActionList(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<ReturnActionInfo> lstRetAction= sqlH.ExecuteAsList<ReturnActionInfo>("usp_Aspx_BindReturnActionList", parameter);
               return lstRetAction;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    
       public static List<MyOrderListForReturnInfo> GetMyOrderListForReturn(int orderID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPCtC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
               SQLHandler sqlH = new SQLHandler();
               List<MyOrderListForReturnInfo> lstMyOrder= sqlH.ExecuteAsList<MyOrderListForReturnInfo>("usp_Aspx_GetMyOrdersForReturn", parameter);
               return lstMyOrder;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static void ReturnSaveUpdate(ReturnSaveUpdateInfo ReturnSaveUpdateObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetFParamNoU(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@OrderID", ReturnSaveUpdateObj.OrderID));
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", ReturnSaveUpdateObj.ItemID));
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", ReturnSaveUpdateObj.ItemName));
               parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantValueIDs", ReturnSaveUpdateObj.CostVariantIDs));
               parameterCollection.Add(new KeyValuePair<string, object>("@CostVariant", ReturnSaveUpdateObj.CostVariants));
               parameterCollection.Add(new KeyValuePair<string, object>("@Quantity", ReturnSaveUpdateObj.Quantity));
               parameterCollection.Add(new KeyValuePair<string, object>("@ReturnProductStatusID", ReturnSaveUpdateObj.ProductStatusID));
               parameterCollection.Add(new KeyValuePair<string, object>("@ReturnReasonID", ReturnSaveUpdateObj.ReturnReasonID));
               parameterCollection.Add(new KeyValuePair<string, object>("@OtherDetails", ReturnSaveUpdateObj.OtherDetails));
               parameterCollection.Add(new KeyValuePair<string, object>("@ReturnShippingAddressID", ReturnSaveUpdateObj.ReturnShippingAddressID));
               parameterCollection.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_ReturnSaveUpdate", parameterCollection);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static void ReturnUpdate(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamNoCID(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ReturnID", returnDetailObj.ReturnID));
               parameter.Add(new KeyValuePair<string, object>("@OrderID", returnDetailObj.OrderID));
               parameter.Add(new KeyValuePair<string, object>("@ReturnActionID", returnDetailObj.ReturnActionID));
               parameter.Add(new KeyValuePair<string, object>("@ReturnStatusID", returnDetailObj.ReturnStatusID));
               parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", returnDetailObj.shippingMethodID));
               parameter.Add(new KeyValuePair<string, object>("@ShippingCost", returnDetailObj.ShippingCost));
               parameter.Add(new KeyValuePair<string, object>("@OtherPostalCharges", returnDetailObj.OtherPostalCharges));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_ReturnUpdate", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }

       }
       
       public static void ReturnSaveComments(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ReturnID", returnDetailObj.ReturnID));
               parameter.Add(new KeyValuePair<string, object>("@OrderID", returnDetailObj.OrderID));
               parameter.Add(new KeyValuePair<string, object>("@CommentText", returnDetailObj.CommentText));
               parameter.Add(new KeyValuePair<string, object>("@IsNotifiedByEmail", returnDetailObj.IsCustomerNotifiedByEmail));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_ReturnSaveComments", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }

       }
       
       public static List<ReturnCommentsInfo> GetMyReturnsComment(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPCtC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ReturnID", returnDetailObj.ReturnID));
               parameter.Add(new KeyValuePair<string, object>("@OrderID", returnDetailObj.OrderID));
               SQLHandler sqlh = new SQLHandler();       
               List<ReturnCommentsInfo> info = sqlh.ExecuteAsList<ReturnCommentsInfo>("usp_Aspx_GetMyReturnsComment", parameter);
               return info;
           }

           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static List<ReturnsShippingInfo> GetMyReturnsShippingMethod(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ReturnID", returnDetailObj.ReturnID));
               parameter.Add(new KeyValuePair<string, object>("@OrderID", returnDetailObj.OrderID));
               SQLHandler sqlh = new SQLHandler();
               List<ReturnsShippingInfo> info;
               info = sqlh.ExecuteAsList<ReturnsShippingInfo>("usp_Aspx_GetShippingMethodByTotalWeightForReturn", parameter);
               return info;
           }

           catch (Exception e)
           {
               throw e;
           }
       }

       
       public static List<RetunReportInfo> GetReturnReport(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, ReturnReportBasicInfo returnReportObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@ReturnStatus", returnReportObj.ReturnStatus));
               SQLHandler sqlh = new SQLHandler();
               if (returnReportObj.Monthly == true)
               {
                   return sqlh.ExecuteAsList<RetunReportInfo>("usp_Aspx_GetReturnReport", parameter);
               }
               if (returnReportObj.Weekly == true)
               {
                   return sqlh.ExecuteAsList<RetunReportInfo>("usp_Aspx_GetReturnReportByCurrentMonth", parameter);
               }
               if (returnReportObj.Hourly == true)
               {
                   return sqlh.ExecuteAsList<RetunReportInfo>("usp_Aspx_GetReturnReportBy24hours", parameter);
               }
               else
                   return new List<RetunReportInfo>();
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       
       public static void ReturnShippingAddressSaveUpdate(AddressBasicInfo addressObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetFParamNoU(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@OrderID", addressObj.OrderID));
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", addressObj.ItemID));
               parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantValueIDs", addressObj.CostVariantIDs));
               parameterCollection.Add(new KeyValuePair<string, object>("@AddressID", addressObj.AddressID));
               parameterCollection.Add(new KeyValuePair<string, object>("@FirstName", addressObj.FirstName));
               parameterCollection.Add(new KeyValuePair<string, object>("@LastName", addressObj.LastName));
               parameterCollection.Add(new KeyValuePair<string, object>("@Company", addressObj.Company));
               parameterCollection.Add(new KeyValuePair<string, object>("@Address1", addressObj.Address1));
               parameterCollection.Add(new KeyValuePair<string, object>("@Address2", addressObj.Address2));
               parameterCollection.Add(new KeyValuePair<string, object>("@City", addressObj.City));
               parameterCollection.Add(new KeyValuePair<string, object>("@State", addressObj.State));
               parameterCollection.Add(new KeyValuePair<string, object>("@Country", addressObj.Country));
               parameterCollection.Add(new KeyValuePair<string, object>("@Zip", addressObj.Zip));
               parameterCollection.Add(new KeyValuePair<string, object>("@Email", addressObj.Email));
               parameterCollection.Add(new KeyValuePair<string, object>("@Phone", addressObj.Phone));
               parameterCollection.Add(new KeyValuePair<string, object>("@Mobile", addressObj.Mobile));
               parameterCollection.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_ReturnShippingAddressSaveUpdate", parameterCollection);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       
       public static List<MyReturnListInfo> GetMyReturnsList(int offset, int limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPCtC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               SQLHandler sqlH = new SQLHandler();
               List<MyReturnListInfo> lstMyReturn= sqlH.ExecuteAsList<MyReturnListInfo>("[usp_Aspx_GetMyReturnsList]", parameter);
               return lstMyReturn;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static List<ReturnItemsInfo> GetMyReturnsDetails(RetunDetailsBasicInfo returnDetailObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFParamNoSCode(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ReturnID", returnDetailObj.ReturnID));
               parameter.Add(new KeyValuePair<string, object>("@OrderID", returnDetailObj.OrderID));
               SQLHandler sqlh = new SQLHandler();
               List<ReturnItemsInfo> info = sqlh.ExecuteAsList<ReturnItemsInfo>("usp_Aspx_GetMyReturnsDetails", parameter);
               return info;
           }

           catch (Exception e)
           {
               throw e;
           }
       }
  
       public static List<ReturnDetailsInfo> GetReturnDetails(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, RetunDetailsBasicInfo returnDetailObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@ReturnID", returnDetailObj.ReturnID));
               parameter.Add(new KeyValuePair<string, object>("@OrderID", returnDetailObj.OrderID));
               parameter.Add(new KeyValuePair<string, object>("@UserName", returnDetailObj.CustomerName));
               parameter.Add(new KeyValuePair<string, object>("@StatusName", returnDetailObj.ReturnStatus));
               parameter.Add(new KeyValuePair<string, object>("@DateAdded", returnDetailObj.DateAdded));
               parameter.Add(new KeyValuePair<string, object>("@DateModified", returnDetailObj.DateModified));
               SQLHandler sqlH = new SQLHandler();
               List<ReturnDetailsInfo> info = sqlH.ExecuteAsList<ReturnDetailsInfo>("usp_Aspx_GetReturnDetails", parameter);
               return info;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static void ReturnSendEmail(AspxCommonInfo aspxCommonObj, SendEmailInfo sendEmailObj)
       {
           try
           {
               EmailTemplate.SendEmailForReturns(aspxCommonObj, sendEmailObj);

           }
           catch (Exception e)
           {
               throw e;
           }
       }

       
       public static List<AddressInfo> GetAddressBookDetailsByAddressID(int addressID, int storeID, int portalID, int customerID, string userName, string cultureName)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@AddressID", addressID));
           parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
           parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
           SQLHandler sqlh = new SQLHandler();
           List<AddressInfo> lstAddress= sqlh.ExecuteAsList<AddressInfo>("usp_Aspx_GetUserAddressBookDetailsByAddressID", parameter);
           return lstAddress;
       }
       //-----------------------UserDashBoard/ My Orders-------------------

       public static List<OrderItemsInfo> GetMyOrders(int orderID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFParamNoSCode(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
               SQLHandler sqlh = new SQLHandler();
               List<OrderItemsInfo> info = sqlh.ExecuteAsList<OrderItemsInfo>("usp_Aspx_GetMyOrders", parameter);
               return info;
           }

           catch (Exception e)
           {
               throw e;
           }
       }

       //-------------------------UserDashBoard/User Downloadable Items------------------------------
       
       public static List<DownloadableItemsByCustomerInfo> GetCustomerDownloadableItems(int offset, int limit, string sku, string name, AspxCommonInfo aspxCommonObj, bool isActive)
       {
           try
           {
               List<DownloadableItemsByCustomerInfo> ml;
               SQLHandler Sq = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
               parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
               parameterCollection.Add(new KeyValuePair<string, object>("@SKU", sku));
               parameterCollection.Add(new KeyValuePair<string, object>("@Name", name));
               parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
               ml = Sq.ExecuteAsList<DownloadableItemsByCustomerInfo>("dbo.usp_Aspx_GetCustomerDownloadableItems", parameterCollection);
               return ml;
           }

           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static void DeleteCustomerDownloadableItem(string orderItemID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@OrderItemID", orderItemID));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteCustomerDownloadableItem", parameterCollection);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static void UpdateDownloadCount(int itemID, int orderItemID, string downloadIP, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
               parameter.Add(new KeyValuePair<string, object>("@OrderItemID", orderItemID));
               parameter.Add(new KeyValuePair<string, object>("@DownloadIP", downloadIP));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_UpdateDownloadCount", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool CheckRemainingDownload(int itemId, int orderItemId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
               parameterCollection.Add(new KeyValuePair<string, object>("@OrderItemID", orderItemId));
               bool isRemain = sqlH.ExecuteNonQueryAsBool("dbo.usp_Aspx_CheckRemainingDownloadForCustomer", parameterCollection, "@IsRemainDowload");
               return isRemain;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteViewedItems(string viewedItems, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ViewedItems", viewedItems));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleViewedItems", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteComparedItems(string compareItems, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@CompareItems", compareItems));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleComparedItems", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
