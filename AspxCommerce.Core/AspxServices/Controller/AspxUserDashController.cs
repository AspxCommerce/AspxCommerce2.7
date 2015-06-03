using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Security.Helpers;

namespace AspxCommerce.Core
{
    public class AspxUserDashController
    {
        public AspxUserDashController()
        {
        }

        public bool ChangePassword(AspxCommonInfo aspxCommonObj, string newPassword, string retypePassword)
        {
            MembershipController m = new MembershipController();
            try
            {
                if (newPassword != "" && retypePassword != "" && newPassword == retypePassword && aspxCommonObj.UserName != "")
                {
                    UserInfo sageUser = m.GetUserDetails(aspxCommonObj.PortalID, aspxCommonObj.UserName);
                    // Guid userID = (Guid)member.ProviderUserKey;
                    string password, passwordSalt;
                    PasswordHelper.EnforcePasswordSecurity(m.PasswordFormat, newPassword, out password, out passwordSalt);
                    UserInfo user = new UserInfo(sageUser.UserID, password, passwordSalt, m.PasswordFormat);
                    m.ChangePassword(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Shared Wishlists
        //--------------------bind ShareWishList Email  in Grid--------------------------
        public static List<ShareWishListItemInfo> GetAllShareWishListItemMail(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ShareWishListItemInfo> lstShareWishItem = AspxUserDashProvider.GetAllShareWishListItemMail(offset, limit, aspxCommonObj);
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
                List<ShareWishListItemInfo> lstShareWishItem = AspxUserDashProvider.GetShareWishListItemByID(sharedWishID, aspxCommonObj);
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
                AspxUserDashProvider.DeleteShareWishListItem(shareWishListID, aspxCommonObj);
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
                int errorCode = AspxUserDashProvider.UpdateCustomer(aspxCommonObj, firstName, lastName, email);
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
                List<UserRatingInformationInfo> bind = AspxUserDashProvider.GetUserReviewsAndRatings(offset, limit, aspxCommonObj);
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
                AspxUserDashProvider.UpdateItemRatingByUser(updateItemRatingObj, aspxCommonObj);
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
                List<UserRecentHistoryInfo> lstUserHistory = AspxUserDashProvider.GetUserRecentlyViewedItems(offset, limit, aspxCommonObj);
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
                List<UserRecentCompareInfo> lstUserRCompare = AspxUserDashProvider.GetUserRecentlyComparedItems(offset, limit, aspxCommonObj);
                return lstUserRCompare;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void AddUpdateUserAddress(AddressInfo addressObj, AspxCommonInfo aspxCommonObj)
        {
            AspxUserDashProvider.AddUpdateUserAddress(addressObj, aspxCommonObj);
        }

        public static List<AddressInfo> GetUserAddressDetails(AspxCommonInfo aspxCommonObj)
        {
            List<AddressInfo> lstAddress = AspxUserDashProvider.GetUserAddressDetails(aspxCommonObj);
            return lstAddress;
        }

        public static void DeleteAddressBookDetails(int addressID, AspxCommonInfo aspxCommonObj)
        {
            AspxUserDashProvider.DeleteAddressBookDetails(addressID, aspxCommonObj);
        }

        public static List<UserProductReviewInfo> GetUserProductReviews(AspxCommonInfo aspxCommonObj)
        {
            List<UserProductReviewInfo> lstUPReview = AspxUserDashProvider.GetUserProductReviews(aspxCommonObj);
            return lstUPReview;
        }

        public static void UpdateUserProductReview(ItemReviewBasicInfo productReviewObj, AspxCommonInfo aspxCommonObj)
        {
            AspxUserDashProvider.UpdateUserProductReview(productReviewObj, aspxCommonObj);

        }

        public static void DeleteUserProductReview(int itemID, int itemReviewID, AspxCommonInfo aspxCommonObj)
        {
            AspxUserDashProvider.DeleteUserProductReview(itemID, itemReviewID, aspxCommonObj);
        }

        //---------------userDashBord/My Order List in grid----------------------------

        public static List<MyOrderListInfo> GetMyOrderList(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<MyOrderListInfo> lstMyOrder = AspxUserDashProvider.GetMyOrderList(offset, limit, aspxCommonObj);
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
                List<ReOrderItemsInfo> info = AspxUserDashProvider.GetMyOrdersforReOrder(orderID, aspxCommonObj);
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
                decimal retValue = AspxUserDashProvider.CheckItemQuantity(itemID, aspxCommonObj, itemCostVariantIDs);
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
                List<ReturnReasonListInfo> lstRRList = AspxUserDashProvider.BindReturnReasonList(aspxCommonObj);
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
                List<ProductStatusListInfo> lstPdtStatus = AspxUserDashProvider.BindProductStatusList(aspxCommonObj);
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
                List<ReturnStatusInfo> lstRetStatus = AspxUserDashProvider.GetReturnStatusList(aspxCommonObj);
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
                List<ReturnActionInfo> lstRetAction = AspxUserDashProvider.GetReturnActionList(aspxCommonObj);
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
                List<MyOrderListForReturnInfo> lstMyOrder = AspxUserDashProvider.GetMyOrderListForReturn(orderID, aspxCommonObj);
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
                AspxUserDashProvider.ReturnSaveUpdate(ReturnSaveUpdateObj, aspxCommonObj);
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
                AspxUserDashProvider.ReturnUpdate(returnDetailObj, aspxCommonObj);
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
                AspxUserDashProvider.ReturnSaveComments(returnDetailObj, aspxCommonObj);
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
                List<ReturnCommentsInfo> info = AspxUserDashProvider.GetMyReturnsComment(returnDetailObj, aspxCommonObj);
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
                List<ReturnsShippingInfo> info;
                info = AspxUserDashProvider.GetMyReturnsShippingMethod(returnDetailObj, aspxCommonObj);
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
                List<RetunReportInfo> lstRtnReport = AspxUserDashProvider.GetReturnReport(offset, limit, aspxCommonObj, returnReportObj);
                return lstRtnReport;
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
                AspxUserDashProvider.ReturnShippingAddressSaveUpdate(addressObj, aspxCommonObj);
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
                List<MyReturnListInfo> lstMyReturn = AspxUserDashProvider.GetMyReturnsList(offset, limit, aspxCommonObj);
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
                List<ReturnItemsInfo> info = AspxUserDashProvider.GetMyReturnsDetails(returnDetailObj, aspxCommonObj);
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
                List<ReturnDetailsInfo> info = AspxUserDashProvider.GetReturnDetails(offset, limit, aspxCommonObj, returnDetailObj);
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

        public List<ReturnsSettingsInfo> ReturnGetSettings(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ReturnsSettingsInfo> info = AspxReturnRequestMgntController.ReturnGetSettings(aspxCommonObj);
                return info;
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<AddressInfo> GetAddressBookDetailsByAddressID(int addressID, int storeID, int portalID, int customerID, string userName, string cultureName)
        {
            List<AddressInfo> lstAddress = AspxUserDashProvider.GetAddressBookDetailsByAddressID(addressID, storeID, portalID, customerID, userName, cultureName);
            return lstAddress;
        }
        //-----------------------UserDashBoard/ My Orders-------------------

        public static List<OrderItemsInfo> GetMyOrders(int orderID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<OrderItemsInfo> info = AspxUserDashProvider.GetMyOrders(orderID, aspxCommonObj);
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
                ml = AspxUserDashProvider.GetCustomerDownloadableItems(offset, limit, sku, name, aspxCommonObj, isActive);
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
                AspxUserDashProvider.DeleteCustomerDownloadableItem(orderItemID, aspxCommonObj);
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
                AspxUserDashProvider.UpdateDownloadCount(itemID, orderItemID, downloadIP, aspxCommonObj);
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
                bool isRemain = AspxUserDashProvider.CheckRemainingDownload(itemId, orderItemId, aspxCommonObj);
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
                AspxUserDashProvider.DeleteViewedItems(viewedItems, aspxCommonObj);
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
                AspxUserDashProvider.DeleteComparedItems(compareItems, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ReferToFriendInfo> GetUserReferredFriends(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ReferToFriendInfo> lstReferFriend = AspxReferFriendController.GetUserReferredFriends(offset, limit, aspxCommonObj);
                return lstReferFriend;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-----------------Delete Email list --------------------------------
        public static void DeleteReferToFriendEmailUser(string emailAFriendIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxReferFriendController.DeleteReferToFriendEmailUser(emailAFriendIDs, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
