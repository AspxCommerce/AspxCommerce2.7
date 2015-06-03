using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxCustomerMgntController
    {
       public AspxCustomerMgntController()
       {
       }

       #region CustomerDetails

       public static List<CustomerDetailsInfo> GetCustomerDetails(string customerName, int offset, int limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CustomerDetailsInfo> lstCustDetail = AspxCustomerMgntProvider.GetCustomerDetails(customerName, offset, limit, aspxCommonObj);
               return lstCustDetail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static CustomerPersonalInfo GetCustomerDetailsByCustomerID(int customerId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               CustomerPersonalInfo customerPersonalInfo = AspxCustomerMgntProvider.GetCustomerDetailsByCustomerID(customerId, aspxCommonObj);
               return customerPersonalInfo;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<CustomerRecentOrders> GetCustomerRecentOrdersByCustomerID(int offset,int limit,int customerId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CustomerRecentOrders> lstCustDetail = AspxCustomerMgntProvider.GetCustomerRecentOrdersByCustomerID(offset,limit,customerId, aspxCommonObj);
               return lstCustDetail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<CustCartInfo> GetCustomerShopingCartByCustomerID(int customerId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CustCartInfo> lstCustDetail = AspxCustomerMgntProvider.GetCustomerShoppingCartByCustomerID(customerId, aspxCommonObj);
               return lstCustDetail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<CustomerWishList> GetCustomerWishListByCustomerID(int offset,int limit,int customerId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CustomerWishList> lstCustDetail = AspxCustomerMgntProvider.GetCustomerWishListByCustomerID(offset, limit,customerId, aspxCommonObj);
               return lstCustDetail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteMultipleCustomers(string customerIDs, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxCustomerMgntProvider.DeleteMultipleCustomers(customerIDs, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static void DeleteCustomer(int customerId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxCustomerMgntProvider.DeleteCustomer(customerId, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteCustShoppingCartByShopID(string shoppingIDs, int customerID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
                List<string> tempIds = shoppingIDs.Split(',').ToList();
                //multiple deletion
                foreach (string id in tempIds)
                {
                    int tempID = 0;
                    if (Int32.TryParse(id, out tempID))
                    {
                        AspxCustomerMgntProvider.DeleteCustShoppingCartByShopID(tempID, customerID, aspxCommonObj);
                    }
                }
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteCustWishlistByWishID(string wishID, string userName, AspxCommonInfo aspxCommonObj)
       {
           try
           {
                List<string> tempIds = wishID.Split(',').ToList();
                //multiple deletion
                foreach (string id in tempIds)
                {
                    int tempID = 0;
                    if (Int32.TryParse(id, out tempID))
                    {
                        AspxCustomerMgntProvider.DeleteCustWishlistByWishID(tempID, userName, aspxCommonObj);
                    }
                }
               
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       #endregion

       #region Customer Reports By Order Total
       //--------------------bind Customer Order Total Roports-------------------------    
       public static List<CustomerOrderTotalInfo> GetCustomerOrderTotal(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, string user)
       {
           try
           {
               List<CustomerOrderTotalInfo> lstCustOrderTot = AspxCustomerMgntProvider.GetCustomerOrderTotal(offset, limit, aspxCommonObj, user);
               return lstCustOrderTot;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       #endregion

       //--------------- New Account Reports--------------------------
       public static List<NewAccountReportInfo> GetNewAccounts(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, bool monthly, bool weekly, bool hourly)
       {
           try
           {
               List<NewAccountReportInfo> lstNewAccounts = AspxCustomerMgntProvider.GetNewAccounts(offset, limit, aspxCommonObj, monthly, weekly, hourly);
               return lstNewAccounts;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<CustomerOnlineInfo> CheckIfCustomerExists(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<CustomerOnlineInfo> isExistsInfo = AspxCustomerMgntProvider.CheckIfCustomerExists(aspxCommonObj);
               return isExistsInfo;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       #region Online Users
       public static List<OnLineUserBaseInfo> GetRegisteredUserOnlineCount(int offset, int limit, string searchUserName, string searchHostAddress, string searchBrowser, AspxCommonInfo aspxCommonObj)
       {
           try
           {

               List<OnLineUserBaseInfo> lstOnlineUser = AspxCustomerMgntProvider.GetRegisteredUserOnlineCount(offset, limit, searchUserName, searchHostAddress, searchBrowser, aspxCommonObj);
               return lstOnlineUser;
           }
           catch (Exception e)
           {

               throw e;
           }
       }

       public static List<OnLineUserBaseInfo> GetAnonymousUserOnlineCount(int offset, int limit, string searchHostAddress, string searchBrowser, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<OnLineUserBaseInfo> lst = AspxCustomerMgntProvider.GetAnonymousUserOnlineCount(offset, limit, searchHostAddress, searchBrowser, aspxCommonObj);
               return lst;
           }
           catch (Exception e)
           {

               throw e;
           }
       }
       #endregion

       
    }
}
