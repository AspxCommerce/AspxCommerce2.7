using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxCustomerMgntProvider
    {
        public AspxCustomerMgntProvider()
        {
        }

        #region CustomerDetails

        public static List<CustomerDetailsInfo> GetCustomerDetails(string customerName, int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CustomerName", customerName));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler sqlH = new SQLHandler();
                List<CustomerDetailsInfo> lstCustDetail = sqlH.ExecuteAsList<CustomerDetailsInfo>("[dbo].[usp_Aspx_GetCustomerDetails]", parameter);
                return lstCustDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static CustomerPersonalInfo GetCustomerDetailsByCustomerID(int CustomerID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@customerID", CustomerID));
                SQLHandler sqlH = new SQLHandler();

                System.Data.SqlClient.SqlDataReader drCustDetail = sqlH.ExecuteAsDataReader("[dbo].[usp_Aspx_GetCustomerDetailsByCustomerID]", parameter);

                CustomerPersonalInfo customerPersonalInfo = new CustomerPersonalInfo();
                int count = 0;
                while (drCustDetail.Read())
                {
                    if (count == 0)
                    {
                        DateTime tempDateTime;
                        int tempInt;
                        //customerPersonalInfo.AccountCreatedIn = DateTime.TryParse(drCustDetail["CreateDate"].ToString(), out tempDateTime) ? tempDateTime : DateTime.MinValue;
                        customerPersonalInfo.AccountCreatedOn = DateTime.TryParse(drCustDetail["CreateDate"].ToString(), out tempDateTime) ? tempDateTime.ToString("HH:mm dd/MM/yyyy") : "Error retrieving data";
                        //customerPersonalInfo.ConfirmedEmail = drCustDetail["Email"].ToString();
                        customerPersonalInfo.CustomerID = Int32.TryParse(drCustDetail["CustomerID"].ToString(), out tempInt) ? tempInt : 0;
                        customerPersonalInfo.LastLoggedIn = DateTime.TryParse(drCustDetail["LastLoginDate"].ToString(), out tempDateTime) ? tempDateTime.ToString("HH:mm dd/MM/yyyy") : "Never";
                        customerPersonalInfo.PortalID = Int32.TryParse(drCustDetail["PortalID"].ToString(), out tempInt) ? tempInt : 0;
                        customerPersonalInfo.UserName = drCustDetail["UserName"].ToString();

                        string strLifetimeSales = FixDouble(drCustDetail["LifetimeSales"].ToString());
                        customerPersonalInfo.LifetimeSales = strLifetimeSales;
                        string strAverageSales = FixDouble(drCustDetail["AverageSales"].ToString());
                        customerPersonalInfo.AverageSales = strAverageSales;

                        if (drCustDetail["DefaultBilling"].ToString() == "True" && drCustDetail["DefaultShipping"].ToString() == "True")
                        {
                            //Billing address
                            customerPersonalInfo.BillingAddress = new AddressInfo();
                            customerPersonalInfo.BillingAddress.Address1 = drCustDetail["saAddress1"].ToString();
                            customerPersonalInfo.BillingAddress.Address2 = drCustDetail["saAddress2"].ToString();
                            customerPersonalInfo.BillingAddress.City = drCustDetail["saCity"].ToString();
                            customerPersonalInfo.BillingAddress.Company = drCustDetail["saCompany"].ToString();
                            customerPersonalInfo.BillingAddress.Country = drCustDetail["saCountry"].ToString();
                            customerPersonalInfo.BillingAddress.FirstName = drCustDetail["saFirstName"].ToString();
                            customerPersonalInfo.BillingAddress.LastName = drCustDetail["saLastName"].ToString();
                            customerPersonalInfo.BillingAddress.Zip = drCustDetail["saZip"].ToString();
                            customerPersonalInfo.BillingAddress.State = drCustDetail["saState"].ToString();

                            //Shipping address
                            customerPersonalInfo.ShippingAddress = new AddressInfo();
                            customerPersonalInfo.ShippingAddress.Address1 = drCustDetail["saAddress1"].ToString();
                            customerPersonalInfo.ShippingAddress.Address2 = drCustDetail["saAddress2"].ToString();
                            customerPersonalInfo.ShippingAddress.City = drCustDetail["saCity"].ToString();
                            customerPersonalInfo.ShippingAddress.Company = drCustDetail["saCompany"].ToString();
                            customerPersonalInfo.ShippingAddress.Country = drCustDetail["saCountry"].ToString();
                            customerPersonalInfo.ShippingAddress.FirstName = drCustDetail["saFirstName"].ToString();
                            customerPersonalInfo.ShippingAddress.LastName = drCustDetail["saLastName"].ToString();
                            customerPersonalInfo.ShippingAddress.Zip = drCustDetail["saZip"].ToString();
                            customerPersonalInfo.ShippingAddress.State = drCustDetail["saState"].ToString();

                        }
                        else if (drCustDetail["DefaultBilling"].ToString() == "True")
                        {
                            //Billing address
                            customerPersonalInfo.BillingAddress = new AddressInfo();
                            customerPersonalInfo.BillingAddress.Address1 = drCustDetail["saAddress1"].ToString();
                            customerPersonalInfo.BillingAddress.Address2 = drCustDetail["saAddress2"].ToString();
                            customerPersonalInfo.BillingAddress.City = drCustDetail["saCity"].ToString();
                            customerPersonalInfo.BillingAddress.Company = drCustDetail["saCompany"].ToString();
                            customerPersonalInfo.BillingAddress.Country = drCustDetail["saCountry"].ToString();
                            customerPersonalInfo.BillingAddress.FirstName = drCustDetail["saFirstName"].ToString();
                            customerPersonalInfo.BillingAddress.LastName = drCustDetail["saLastName"].ToString();
                            customerPersonalInfo.BillingAddress.Zip = drCustDetail["saZip"].ToString();
                            customerPersonalInfo.BillingAddress.State = drCustDetail["saState"].ToString();
                        }
                        else if (drCustDetail["DefaultShipping"].ToString() == "True")
                        {
                            //Shipping address
                            customerPersonalInfo.ShippingAddress = new AddressInfo();
                            customerPersonalInfo.ShippingAddress.Address1 = drCustDetail["saAddress1"].ToString();
                            customerPersonalInfo.ShippingAddress.Address2 = drCustDetail["saAddress2"].ToString();
                            customerPersonalInfo.ShippingAddress.City = drCustDetail["saCity"].ToString();
                            customerPersonalInfo.ShippingAddress.Company = drCustDetail["saCompany"].ToString();
                            customerPersonalInfo.ShippingAddress.Country = drCustDetail["saCountry"].ToString();
                            customerPersonalInfo.ShippingAddress.FirstName = drCustDetail["saFirstName"].ToString();
                            customerPersonalInfo.ShippingAddress.LastName = drCustDetail["saLastName"].ToString();
                            customerPersonalInfo.ShippingAddress.Zip = drCustDetail["saZip"].ToString();
                            customerPersonalInfo.ShippingAddress.State = drCustDetail["saState"].ToString();
                        }
                    }
                    else
                    {
                        if (drCustDetail["DefaultBilling"].ToString() == "True")
                        {
                            //Billing address
                            customerPersonalInfo.BillingAddress = new AddressInfo();
                            customerPersonalInfo.BillingAddress.Address1 = drCustDetail["saAddress1"].ToString();
                            customerPersonalInfo.BillingAddress.Address2 = drCustDetail["saAddress2"].ToString();
                            customerPersonalInfo.BillingAddress.City = drCustDetail["saCity"].ToString();
                            customerPersonalInfo.BillingAddress.Company = drCustDetail["saCompany"].ToString();
                            customerPersonalInfo.BillingAddress.Country = drCustDetail["saCountry"].ToString();
                            customerPersonalInfo.BillingAddress.FirstName = drCustDetail["saFirstName"].ToString();
                            customerPersonalInfo.BillingAddress.LastName = drCustDetail["saLastName"].ToString();
                            customerPersonalInfo.BillingAddress.Zip = drCustDetail["saZip"].ToString();
                            customerPersonalInfo.BillingAddress.State = drCustDetail["saState"].ToString();
                        }
                        else if (drCustDetail["DefaultShipping"].ToString() == "True")
                        {
                            //Shipping address
                            customerPersonalInfo.ShippingAddress = new AddressInfo();
                            customerPersonalInfo.ShippingAddress.Address1 = drCustDetail["saAddress1"].ToString();
                            customerPersonalInfo.ShippingAddress.Address2 = drCustDetail["saAddress2"].ToString();
                            customerPersonalInfo.ShippingAddress.City = drCustDetail["saCity"].ToString();
                            customerPersonalInfo.ShippingAddress.Company = drCustDetail["saCompany"].ToString();
                            customerPersonalInfo.ShippingAddress.Country = drCustDetail["saCountry"].ToString();
                            customerPersonalInfo.ShippingAddress.FirstName = drCustDetail["saFirstName"].ToString();
                            customerPersonalInfo.ShippingAddress.LastName = drCustDetail["saLastName"].ToString();
                            customerPersonalInfo.ShippingAddress.Zip = drCustDetail["saZip"].ToString();
                            customerPersonalInfo.ShippingAddress.State = drCustDetail["saState"].ToString();
                        }
                    }

                    count++;
                }

                return customerPersonalInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string FixDouble(string strDouble)
        {
            return strDouble.Contains('.') ? strDouble.Substring(0, strDouble.IndexOf('.') + 4) : strDouble;
        }

        public static List<CustomerRecentOrders> GetCustomerRecentOrdersByCustomerID(int offset, int limit, int customerID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@customerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler sqlH = new SQLHandler();
                List<CustomerRecentOrders> lstCustRecentOrders = sqlH.ExecuteAsList<CustomerRecentOrders>("[dbo].[usp_Aspx_GetCustomerRecentOrdersByCustomerID]", parameter);
                return lstCustRecentOrders;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CustCartInfo> GetCustomerShoppingCartByCustomerID(int customerID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@customerID", customerID));
                SQLHandler sqlH = new SQLHandler();
                List<CustCartInfo> lstCustShoppingCart = sqlH.ExecuteAsList<CustCartInfo>("usp_Aspx_GetCustCartDetails", parameter);
                return lstCustShoppingCart;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CustomerWishList> GetCustomerWishListByCustomerID(int offset, int limit, int customerID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@customerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));

                SQLHandler sqlH = new SQLHandler();
                List<CustomerWishList> lstCustWishList = sqlH.ExecuteAsList<CustomerWishList>("[dbo].[usp_Aspx_GetCustWishlistDetails]", parameter);
                return lstCustWishList;
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
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@CustomerIDs", customerIDs));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_CustomerDeleteMultipleSelected", parameterCollection);
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
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@CustomerID", customerId));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteCustomerByCustomerID]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteCustShoppingCartByShopID(int shoppingID, int customerID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@CartItemID", shoppingID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteCartItemByCartItemID]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteCustWishlistByWishID(int wishID, string userName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@WishItemID", wishID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteWishItem]", parameterCollection);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@User", user));
                SQLHandler sqlH = new SQLHandler();
                List<CustomerOrderTotalInfo> lstCustOrderTot = sqlH.ExecuteAsList<CustomerOrderTotalInfo>("usp_Aspx_GetCustomerOrderTotal", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler sqlH = new SQLHandler();
                if (monthly == true)
                {
                    List<NewAccountReportInfo> lstNewAccount = sqlH.ExecuteAsList<NewAccountReportInfo>("usp_Aspx_GetNewAccountDetails", parameter);
                    return lstNewAccount;
                }
                if (weekly == true)
                {
                    List<NewAccountReportInfo> lstNewAccount = sqlH.ExecuteAsList<NewAccountReportInfo>("usp_Aspx_GetNewAccountDetailsByCurrentMonth", parameter);
                    return lstNewAccount;
                }
                if (hourly == true)
                {
                    List<NewAccountReportInfo> lstNewAccount = sqlH.ExecuteAsList<NewAccountReportInfo>("usp_Aspx_GetNewAccountDetailsBy24hours", parameter);
                    return lstNewAccount;
                }
                else
                    return new List<NewAccountReportInfo>();
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
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
                ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));
                ParaMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                List<CustomerOnlineInfo> isExistInfo = sqlH.ExecuteAsList<CustomerOnlineInfo>("[dbo].[usp_Aspx_CheckUserExists]", ParaMeter);
                return isExistInfo;
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
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@Offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@Limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@SearchUserName", searchUserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@HostAddress", searchHostAddress));
                parameterCollection.Add(new KeyValuePair<string, object>("@Browser", searchBrowser));
                SQLHandler sqlH = new SQLHandler();
                List<OnLineUserBaseInfo> lstOnlineUser = sqlH.ExecuteAsList<OnLineUserBaseInfo>("usp_Aspx_GetOnlineRegisteredUsers", parameterCollection);
                return lstOnlineUser;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static List<OnLineUserBaseInfo> GetAnonymousUserOnlineCount(int offset, int limit, string searchHostAddress, string searchBrowser, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@Offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@Limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@HostAddress", searchHostAddress));
            parameterCollection.Add(new KeyValuePair<string, object>("@Browser", searchBrowser));
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<OnLineUserBaseInfo> lst = sqlH.ExecuteAsList<OnLineUserBaseInfo>("usp_Aspx_GetOnlineAnonymousUsers", parameterCollection);
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
