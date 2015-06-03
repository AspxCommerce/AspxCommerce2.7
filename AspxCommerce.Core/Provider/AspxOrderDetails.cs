/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Collections.Generic;
using SageFrame.Web.Utilities;
using System.Data.SqlClient;
using System.Data;

namespace AspxCommerce.Core
{
    public class AspxOrderDetails
    {
        public int AddAddress(OrderDetailsCollection orderData, SqlTransaction tran)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@FirstName", orderData.ObjBillingAddressInfo.FirstName));
                parameter.Add(new KeyValuePair<string, object>("@LastName", orderData.ObjBillingAddressInfo.LastName));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", orderData.ObjOrderDetails.CustomerID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", orderData.ObjOrderDetails.SessionCode));
                parameter.Add(new KeyValuePair<string, object>("@Email", orderData.ObjBillingAddressInfo.EmailAddress));
                parameter.Add(new KeyValuePair<string, object>("@Company", orderData.ObjBillingAddressInfo.CompanyName));
                parameter.Add(new KeyValuePair<string, object>("@Address1", orderData.ObjBillingAddressInfo.Address));
                parameter.Add(new KeyValuePair<string, object>("@Address2", orderData.ObjBillingAddressInfo.Address2));
                parameter.Add(new KeyValuePair<string, object>("@City", orderData.ObjBillingAddressInfo.City));
                parameter.Add(new KeyValuePair<string, object>("@State", orderData.ObjBillingAddressInfo.State));
                parameter.Add(new KeyValuePair<string, object>("@Country", orderData.ObjBillingAddressInfo.Country));
                parameter.Add(new KeyValuePair<string, object>("@Zip", orderData.ObjBillingAddressInfo.Zip));
                parameter.Add(new KeyValuePair<string, object>("@Phone", orderData.ObjBillingAddressInfo.Phone));
                parameter.Add(new KeyValuePair<string, object>("@Mobile", orderData.ObjBillingAddressInfo.Mobile));
                parameter.Add(new KeyValuePair<string, object>("@Fax", orderData.ObjBillingAddressInfo.Fax));
                parameter.Add(new KeyValuePair<string, object>("@UserName", orderData.ObjBillingAddressInfo.EmailAddress));
                parameter.Add(new KeyValuePair<string, object>("@WebSite", orderData.ObjBillingAddressInfo.WebSite));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", orderData.ObjCommonInfo.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));

                SQLHandler sqlH = new SQLHandler();

                int addressID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,
                                                            "usp_Aspx_UserAddress", parameter,
                                                            "@AddressID");

                return addressID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddBillingAddress(OrderDetailsCollection orderData, SqlTransaction tran, int addressID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@AddressID", addressID));
                parameter.Add(new KeyValuePair<string, object>("@IsDefault",
                                                               orderData.ObjBillingAddressInfo.IsDefaultBilling));

                parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", orderData.ObjCommonInfo.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));

                SQLHandler sqlH = new SQLHandler();

                int billingAddressID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,
                                                            "usp_Aspx_UserBillingAddress", parameter,
                                                            "@UserBillingAddressID");

                return addressID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddShippingAddress(OrderDetailsCollection orderData, SqlTransaction tran, int addressID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@AddressID", addressID));
                parameter.Add(new KeyValuePair<string, object>("@IsDefault",
                                                               orderData.ObjShippingAddressInfo.IsDefaultShipping));

                parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", orderData.ObjCommonInfo.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));

                SQLHandler sqlH = new SQLHandler();

                int shippingAddressID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,
                                                             "usp_Aspx_UserShippingAddress", parameter,
                                                             "@UserShippingAddressID");

                return addressID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddBillingAddress(OrderDetailsCollection orderData, SqlTransaction tran)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", orderData.ObjOrderDetails.CustomerID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", orderData.ObjOrderDetails.SessionCode));
                parameter.Add(new KeyValuePair<string, object>("@FirstName", orderData.ObjBillingAddressInfo.FirstName));
                parameter.Add(new KeyValuePair<string, object>("@LastName", orderData.ObjBillingAddressInfo.LastName));
                parameter.Add(new KeyValuePair<string, object>("@Email", orderData.ObjBillingAddressInfo.EmailAddress));
                parameter.Add(new KeyValuePair<string, object>("@Company", orderData.ObjBillingAddressInfo.CompanyName));
                parameter.Add(new KeyValuePair<string, object>("@Address1", orderData.ObjBillingAddressInfo.Address));
                parameter.Add(new KeyValuePair<string, object>("@Address2", orderData.ObjBillingAddressInfo.Address2));
                parameter.Add(new KeyValuePair<string, object>("@City", orderData.ObjBillingAddressInfo.City));
                parameter.Add(new KeyValuePair<string, object>("@State", orderData.ObjBillingAddressInfo.State));
                parameter.Add(new KeyValuePair<string, object>("@Country", orderData.ObjBillingAddressInfo.Country));
                parameter.Add(new KeyValuePair<string, object>("@Zip", orderData.ObjBillingAddressInfo.Zip));
                parameter.Add(new KeyValuePair<string, object>("@Phone", orderData.ObjBillingAddressInfo.Phone));
                parameter.Add(new KeyValuePair<string, object>("@Mobile", orderData.ObjBillingAddressInfo.Mobile));
                parameter.Add(new KeyValuePair<string, object>("@Fax", orderData.ObjBillingAddressInfo.Fax));
                parameter.Add(new KeyValuePair<string, object>("@WebSite", orderData.ObjBillingAddressInfo.WebSite));
                parameter.Add(new KeyValuePair<string, object>("@UserName", orderData.ObjBillingAddressInfo.EmailAddress));
                parameter.Add(new KeyValuePair<string, object>("@IsDefault", orderData.ObjBillingAddressInfo.IsDefaultBilling));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", orderData.ObjCommonInfo.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));

                SQLHandler sqlH = new SQLHandler();

                int billingAddressID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,
                                                            "usp_Aspx_BillingAddress", parameter,
                                                            "@UserBillingAddressID");

                return billingAddressID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddShippingAddress(OrderDetailsCollection orderData, SqlTransaction tran)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", orderData.ObjOrderDetails.CustomerID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", orderData.ObjOrderDetails.SessionCode));
                parameter.Add(new KeyValuePair<string, object>("@FirstName", orderData.ObjShippingAddressInfo.FirstName));
                parameter.Add(new KeyValuePair<string, object>("@LastName", orderData.ObjShippingAddressInfo.LastName));
                parameter.Add(new KeyValuePair<string, object>("@Email", orderData.ObjShippingAddressInfo.EmailAddress));
                parameter.Add(new KeyValuePair<string, object>("@Company", orderData.ObjShippingAddressInfo.CompanyName));
                parameter.Add(new KeyValuePair<string, object>("@Address1", orderData.ObjShippingAddressInfo.Address));
                parameter.Add(new KeyValuePair<string, object>("@Address2", orderData.ObjShippingAddressInfo.Address2));
                parameter.Add(new KeyValuePair<string, object>("@City", orderData.ObjShippingAddressInfo.City));
                parameter.Add(new KeyValuePair<string, object>("@State", orderData.ObjShippingAddressInfo.State));
                parameter.Add(new KeyValuePair<string, object>("@Country", orderData.ObjShippingAddressInfo.Country));
                parameter.Add(new KeyValuePair<string, object>("@Zip", orderData.ObjShippingAddressInfo.Zip));
                parameter.Add(new KeyValuePair<string, object>("@Phone", orderData.ObjShippingAddressInfo.Phone));
                parameter.Add(new KeyValuePair<string, object>("@Mobile", orderData.ObjShippingAddressInfo.Mobile));
                parameter.Add(new KeyValuePair<string, object>("@Fax", orderData.ObjShippingAddressInfo.Fax));
                parameter.Add(new KeyValuePair<string, object>("@WebSite", orderData.ObjShippingAddressInfo.WebSite));
                parameter.Add(new KeyValuePair<string, object>("@UserName", orderData.ObjShippingAddressInfo.EmailAddress));
                parameter.Add(new KeyValuePair<string, object>("@IsDefault", orderData.ObjBillingAddressInfo.IsDefaultBilling));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", orderData.ObjCommonInfo.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));
                SQLHandler sqlH = new SQLHandler();
                int shippingAddressID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,
                                                             "usp_Aspx_ShippingAddress", parameter,
                                                             "@UserShippingAddressID");

                return shippingAddressID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddPaymentInfo(OrderDetailsCollection orderData, SqlTransaction tran)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@PaymentMethodName",
                                                               orderData.ObjPaymentInfo.PaymentMethodName));
                parameter.Add(new KeyValuePair<string, object>("@PaymentMethodCode",
                                                               orderData.ObjPaymentInfo.PaymentMethodCode));
                if (orderData.ObjPaymentInfo.PaymentMethodCode == "CC")
                {
                    parameter.Add(new KeyValuePair<string, object>("@CardNumber", orderData.ObjPaymentInfo.CardNumber));
                    parameter.Add(new KeyValuePair<string, object>("@TransactionType",
                                                                   orderData.ObjPaymentInfo.TransactionType));
                    parameter.Add(new KeyValuePair<string, object>("@CardType", orderData.ObjPaymentInfo.CardType));
                    parameter.Add(new KeyValuePair<string, object>("@CardCode", orderData.ObjPaymentInfo.CardCode));
                    parameter.Add(new KeyValuePair<string, object>("@ExpireDate", orderData.ObjPaymentInfo.ExpireDate));

                    parameter.Add(new KeyValuePair<string, object>("@AccountNumber",
                                                                   ""));
                    parameter.Add(new KeyValuePair<string, object>("@RoutingNumber",
                                                                   ""));
                    parameter.Add(new KeyValuePair<string, object>("@AccountType", ""));
                    parameter.Add(new KeyValuePair<string, object>("@BankName", ""));
                    parameter.Add(new KeyValuePair<string, object>("@AccountHolderName",
                                                                   ""));
                    parameter.Add(new KeyValuePair<string, object>("@ChequeType", ""));
                    parameter.Add(new KeyValuePair<string, object>("@ChequeNumber",
                                                                   ""));
                    parameter.Add(new KeyValuePair<string, object>("@RecurringBillingStatus",
                                                                   false));
                }
                else if (orderData.ObjPaymentInfo.PaymentMethodCode == "ECHECK")
                {
                    parameter.Add(new KeyValuePair<string, object>("@CardNumber", ""));
                    parameter.Add(new KeyValuePair<string, object>("@TransactionType", ""));
                    parameter.Add(new KeyValuePair<string, object>("@CardType", ""));
                    parameter.Add(new KeyValuePair<string, object>("@CardCode", ""));
                    parameter.Add(new KeyValuePair<string, object>("@ExpireDate", ""));
                    parameter.Add(new KeyValuePair<string, object>("@AccountNumber", orderData.ObjPaymentInfo.AccountNumber));
                    parameter.Add(new KeyValuePair<string, object>("@RoutingNumber", orderData.ObjPaymentInfo.RoutingNumber));
                    parameter.Add(new KeyValuePair<string, object>("@AccountType", orderData.ObjPaymentInfo.AccountType));
                    parameter.Add(new KeyValuePair<string, object>("@BankName", orderData.ObjPaymentInfo.BankName));
                    parameter.Add(new KeyValuePair<string, object>("@AccountHolderName", orderData.ObjPaymentInfo.AccountHolderName));
                    parameter.Add(new KeyValuePair<string, object>("@ChequeType", orderData.ObjPaymentInfo.ChequeType));
                    parameter.Add(new KeyValuePair<string, object>("@ChequeNumber", orderData.ObjPaymentInfo.ChequeNumber));
                    parameter.Add(new KeyValuePair<string, object>("@RecurringBillingStatus", orderData.ObjPaymentInfo.RecurringBillingStatus));
                }
                else
                {
                    parameter.Add(new KeyValuePair<string, object>("@CardNumber", ""));
                    parameter.Add(new KeyValuePair<string, object>("@TransactionType", ""));
                    parameter.Add(new KeyValuePair<string, object>("@CardType", ""));
                    parameter.Add(new KeyValuePair<string, object>("@CardCode", ""));
                    parameter.Add(new KeyValuePair<string, object>("@ExpireDate", ""));
                    parameter.Add(new KeyValuePair<string, object>("@AccountNumber", ""));
                    parameter.Add(new KeyValuePair<string, object>("@RoutingNumber", ""));
                    parameter.Add(new KeyValuePair<string, object>("@AccountType", ""));
                    parameter.Add(new KeyValuePair<string, object>("@BankName", ""));
                    parameter.Add(new KeyValuePair<string, object>("@AccountHolderName", ""));
                    parameter.Add(new KeyValuePair<string, object>("@ChequeType", ""));
                    parameter.Add(new KeyValuePair<string, object>("@ChequeNumber", ""));
                    parameter.Add(new KeyValuePair<string, object>("@RecurringBillingStatus", orderData.ObjPaymentInfo.RecurringBillingStatus));

                }

                parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", orderData.ObjCommonInfo.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));
                SQLHandler sqlH = new SQLHandler();
                int paymentMethodID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[usp_Aspx_PaymentInfo]",
                                                           parameter, "@PaymentMethodID");
                return paymentMethodID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddOrderWithMultipleCheckOut(OrderDetailsCollection orderData, SqlTransaction tran, int paymentMethodId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@InvoiceNumber", orderData.ObjOrderDetails.InvoiceNumber));
                parameter.Add(new KeyValuePair<string, object>("@TransactionID", orderData.ObjOrderDetails.TransactionID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", ""));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", orderData.ObjOrderDetails.CustomerID));
                parameter.Add(new KeyValuePair<string, object>("@GrandTotal", orderData.ObjOrderDetails.GrandTotal));
                parameter.Add(new KeyValuePair<string, object>("@DiscountAmount", orderData.ObjOrderDetails.DiscountAmount));
                parameter.Add(new KeyValuePair<string, object>("@CouponDiscountAmount", orderData.ObjOrderDetails.CouponDiscountAmount));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", orderData.ObjOrderDetails.CouponCode));
                parameter.Add(new KeyValuePair<string, object>("@RewardDiscountAmount",
                                                           orderData.ObjOrderDetails.RewardDiscountAmount));               
                parameter.Add(new KeyValuePair<string, object>("@UsedRewardPoints",
                                                           orderData.ObjOrderDetails.UsedRewardPoints));
                parameter.Add(new KeyValuePair<string, object>("@PurchaseOrderNumber", orderData.ObjOrderDetails.PurchaseOrderNumber));
                parameter.Add(new KeyValuePair<string, object>("@PaymentGateTypeID", orderData.ObjOrderDetails.PaymentGatewayTypeID));
                parameter.Add(new KeyValuePair<string, object>("@PaymentGateSubTypeID", orderData.ObjOrderDetails.PaymentGatewaySubTypeID));
                parameter.Add(new KeyValuePair<string, object>("@ClientIPAddress", orderData.ObjOrderDetails.ClientIPAddress));
                parameter.Add(new KeyValuePair<string, object>("@UserBillingAddressID", orderData.ObjOrderDetails.UserBillingAddressID));
                parameter.Add(new KeyValuePair<string, object>("@PaymentMethodID", paymentMethodId));
                parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", orderData.ObjOrderDetails.OrderStatusID));
                parameter.Add(new KeyValuePair<string, object>("@IsGuestUser", "False"));
                parameter.Add(new KeyValuePair<string, object>("@TaxTotal", orderData.ObjOrderDetails.TaxTotal));
                parameter.Add(new KeyValuePair<string, object>("@CurrencyCode", orderData.ObjOrderDetails.CurrencyCode));
                parameter.Add(new KeyValuePair<string, object>("@ResponseCode", orderData.ObjOrderDetails.ResponseCode));
                parameter.Add(new KeyValuePair<string, object>("@ResponseReasonCode", orderData.ObjOrderDetails.ResponseReasonCode));
                parameter.Add(new KeyValuePair<string, object>("@ResponseReasonText", orderData.ObjOrderDetails.ResponseReasonText));
                parameter.Add(new KeyValuePair<string, object>("@Remarks", orderData.ObjOrderDetails.Remarks));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", orderData.ObjCommonInfo.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));

                SQLHandler sqlH = new SQLHandler();
                int orderID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[usp_Aspx_OrderDetails]",
                                                   parameter, "@OrderID");
                return orderID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddOrder(OrderDetailsCollection orderData, SqlTransaction tran, int billingAddressID, int paymentMethodId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@InvoiceNumber", orderData.ObjOrderDetails.InvoiceNumber));
                if (orderData.ObjOrderDetails.CustomerID == 0)
                {
                    parameter.Add(new KeyValuePair<string, object>("@SessionCode", orderData.ObjOrderDetails.SessionCode));
                    parameter.Add(new KeyValuePair<string, object>("@CustomerID", 0));
                }
                else
                {
                    parameter.Add(new KeyValuePair<string, object>("@SessionCode", ""));
                    parameter.Add(new KeyValuePair<string, object>("@CustomerID", orderData.ObjOrderDetails.CustomerID));
                }
                parameter.Add(new KeyValuePair<string, object>("@TransactionID", orderData.ObjOrderDetails.TransactionID));
                parameter.Add(new KeyValuePair<string, object>("@GrandTotal", orderData.ObjOrderDetails.GrandTotal));
                parameter.Add(new KeyValuePair<string, object>("@DiscountAmount",
                                                               orderData.ObjOrderDetails.DiscountAmount));
                parameter.Add(new KeyValuePair<string, object>("@CouponDiscountAmount",
                                                               orderData.ObjOrderDetails.CouponDiscountAmount));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", orderData.ObjOrderDetails.CouponCode));
                parameter.Add(new KeyValuePair<string, object>("@RewardDiscountAmount",
                                                               orderData.ObjOrderDetails.RewardDiscountAmount));                
                parameter.Add(new KeyValuePair<string, object>("@UsedRewardPoints",
                                                           orderData.ObjOrderDetails.UsedRewardPoints));
                parameter.Add(new KeyValuePair<string, object>("@PurchaseOrderNumber",
                                                               orderData.ObjOrderDetails.PurchaseOrderNumber));
                parameter.Add(new KeyValuePair<string, object>("@PaymentGateTypeID",
                                                               orderData.ObjOrderDetails.PaymentGatewayTypeID));
                parameter.Add(new KeyValuePair<string, object>("@PaymentGateSubTypeID",
                                                               orderData.ObjOrderDetails.PaymentGatewaySubTypeID));
                parameter.Add(new KeyValuePair<string, object>("@ClientIPAddress",
                                                               orderData.ObjOrderDetails.ClientIPAddress));
                parameter.Add(new KeyValuePair<string, object>("@UserBillingAddressID",
                                                               billingAddressID));
                parameter.Add(new KeyValuePair<string, object>("@PaymentMethodID",
                                                               paymentMethodId));
                parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", orderData.ObjOrderDetails.OrderStatusID));
                parameter.Add(new KeyValuePair<string, object>("@IsGuestUser", orderData.ObjOrderDetails.IsGuestUser));
                parameter.Add(new KeyValuePair<string, object>("@TaxTotal", orderData.ObjOrderDetails.TaxTotal));
                parameter.Add(new KeyValuePair<string, object>("@CurrencyCode", orderData.ObjOrderDetails.CurrencyCode));
                parameter.Add(new KeyValuePair<string, object>("@ResponseCode", orderData.ObjOrderDetails.ResponseCode));
                parameter.Add(new KeyValuePair<string, object>("@ResponseReasonCode",
                                                               orderData.ObjOrderDetails.ResponseReasonCode));
                parameter.Add(new KeyValuePair<string, object>("@ResponseReasonText",
                                                               orderData.ObjOrderDetails.ResponseReasonText));
                parameter.Add(new KeyValuePair<string, object>("@Remarks", orderData.ObjOrderDetails.Remarks));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", orderData.ObjCommonInfo.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));

                SQLHandler sqlH = new SQLHandler();
                int orderID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[usp_Aspx_OrderDetails]",
                                                   parameter, "@OrderID");
                return orderID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrderDetails(OrderDetailsCollection orderData)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                //  parameter.Add(new KeyValuePair<string, object>("@InvoiceNumber", DateTime.Now.ToString("yyyyMMddhhmmss"))); 
                parameter.Add(new KeyValuePair<string, object>("@TransactionID", orderData.ObjOrderDetails.TransactionID));
                parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", orderData.ObjOrderDetails.OrderStatusID));
                parameter.Add(new KeyValuePair<string, object>("@ResponseCode", orderData.ObjOrderDetails.ResponseCode));
                parameter.Add(new KeyValuePair<string, object>("@ResponseReasonCode", orderData.ObjOrderDetails.ResponseReasonCode));
                parameter.Add(new KeyValuePair<string, object>("@ResponseReasonText", orderData.ObjOrderDetails.ResponseReasonText));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderData.ObjOrderDetails.OrderID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_UpdateOrderDetails]", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReduceCouponUsed(string couponCode, int storeID, int portalID, string userName, int orderID)
        {
            CouponManageSQLProvider csm = new CouponManageSQLProvider();
            csm.UpdateCouponUserRecord(couponCode, storeID, portalID, userName, orderID);
        }


        public void UpdateItemQuantity(OrderDetailsCollection orderData)
        {
            try
            {
                if (orderData.ObjOrderDetails.CouponCode != null && System.Web.HttpContext.Current.Session["CouponApplied"] != null)
                {
                    ReduceCouponUsed(orderData.ObjOrderDetails.CouponCode.Trim(), orderData.ObjCommonInfo.StoreID, orderData.ObjCommonInfo.PortalID, orderData.ObjCommonInfo.AddedBy, orderData.ObjOrderDetails.OrderID);
                }

                foreach (OrderItemInfo objItems in orderData.LstOrderItemsInfo)
                {
                    if (objItems.IsDownloadable != true)
                    {
                        List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                        parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                        parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                        parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));
                        parameter.Add(new KeyValuePair<string, object>("@ItemID", objItems.ItemID));
                        parameter.Add(new KeyValuePair<string, object>("@Quantity", objItems.Quantity));
                        parameter.Add(new KeyValuePair<string, object>("@OrderID", orderData.ObjOrderDetails.OrderID));
                        parameter.Add(new KeyValuePair<string, object>("@CostVariantsIDs", objItems.Variants));
                        SQLHandler sqlH = new SQLHandler();
                        sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_UpdateItemQuantitybyOrder]", parameter);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //multiple
        public void AddOrderItemsList(OrderDetailsCollection orderData, SqlTransaction tran, int orderID)
        {
            try
            {
                foreach (OrderItemInfo objItems in orderData.LstOrderItemsInfo)
                {
                    List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                    parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                    parameter.Add(new KeyValuePair<string, object>("@UserShippingAddressID", objItems.ShippingAddressID));
                    parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", objItems.ShippingMethodID));
                    parameter.Add(new KeyValuePair<string, object>("@ItemID", objItems.ItemID));
                    parameter.Add(new KeyValuePair<string, object>("@Price", objItems.Price));
                    parameter.Add(new KeyValuePair<string, object>("@Quantity", objItems.Quantity));
                    parameter.Add(new KeyValuePair<string, object>("@Weight", objItems.Weight));
                    parameter.Add(new KeyValuePair<string, object>("@Remarks", objItems.Remarks));
                    parameter.Add(new KeyValuePair<string, object>("@ShippingRate", objItems.ShippingRate));
                    parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                    parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                    parameter.Add(new KeyValuePair<string, object>("@CultureName", orderData.ObjCommonInfo.CultureName));
                    parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));
                    parameter.Add(new KeyValuePair<string, object>("@CostVariants", objItems.Variants));
                    parameter.Add(new KeyValuePair<string, object>("@RewardedPoints", objItems.RewardedPoints));

                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[usp_Aspx_OrderItem]", parameter);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //single
        public void AddOrderItems(OrderDetailsCollection orderData, SqlTransaction tran, int orderID, int shippingAddressID)
        {
            try
            {
                foreach (OrderItemInfo objItems in orderData.LstOrderItemsInfo)
                {
                    List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                    parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                    parameter.Add(new KeyValuePair<string, object>("@UserShippingAddressID", shippingAddressID));
                    parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", objItems.ShippingMethodID));
                    parameter.Add(new KeyValuePair<string, object>("@ItemID", objItems.ItemID));
                    parameter.Add(new KeyValuePair<string, object>("@Price", objItems.Price));
                    parameter.Add(new KeyValuePair<string, object>("@Quantity", objItems.Quantity));
                    parameter.Add(new KeyValuePair<string, object>("@Weight", objItems.Weight));
                    parameter.Add(new KeyValuePair<string, object>("@Remarks", objItems.Remarks));
                    parameter.Add(new KeyValuePair<string, object>("@ShippingRate", objItems.ShippingRate));

                    parameter.Add(new KeyValuePair<string, object>("@StoreID", orderData.ObjCommonInfo.StoreID));
                    parameter.Add(new KeyValuePair<string, object>("@PortalID", orderData.ObjCommonInfo.PortalID));
                    parameter.Add(new KeyValuePair<string, object>("@CultureName", orderData.ObjCommonInfo.CultureName));
                    parameter.Add(new KeyValuePair<string, object>("@AddedBy", orderData.ObjCommonInfo.AddedBy));
                    parameter.Add(new KeyValuePair<string, object>("@CostVariants", objItems.Variants));
                    parameter.Add(new KeyValuePair<string, object>("@RewardedPoints", objItems.RewardedPoints));

                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[usp_Aspx_OrderItem]", parameter);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
