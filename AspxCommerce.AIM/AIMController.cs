using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;

namespace AuthorizeNetAIM
{
    public class AIMController
    {   
        public List<AIMAuthorizeSettingInfo> GetAllAuthorizedNetAIMSetting(int paymentGatewayID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                var parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                var sqLh = new SQLHandler();
                return sqLh.ExecuteAsList<AIMAuthorizeSettingInfo>("usp_Aspx_AIMAuthorizeNETSettingsGetAll", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
               
        public string SendPaymentInfoAIM(OrderDetailsCollection OrderDetail, string TemplateName, string addressPath)
        {

            var objRequest = new WebClient();
            var objInf = new System.Collections.Specialized.NameValueCollection(30);

            string strError;

            //OrderDetail.ObjOrderDetails.CustomerID = int.Parse(Crypto.GenerateCustomerID());
            OrderDetail.ObjOrderDetails.InvoiceNumber = Crypto.GenerateInvoiceNumber();
            OrderDetail.ObjOrderDetails.PurchaseOrderNumber = Crypto.GeneratePurchaseOrderNumber();

            //merchant generated field
            objInf.Add("x_version", OrderDetail.ObjOrderDetails.Version);
            objInf.Add("x_delim_data", "True");
            objInf.Add("x_login", OrderDetail.ObjOrderDetails.APILogin);
            objInf.Add("x_tran_key", OrderDetail.ObjOrderDetails.TransactionKey);
            objInf.Add("x_relay_response", "False");
            objInf.Add("x_delim_char", ",");
            objInf.Add("x_encap_char", "|");
            objInf.Add("x_invoice_num", OrderDetail.ObjOrderDetails.InvoiceNumber);
            objInf.Add("x_cust_id", OrderDetail.ObjOrderDetails.CustomerID.ToString(CultureInfo.InvariantCulture));
            objInf.Add("x_po_num", OrderDetail.ObjOrderDetails.PurchaseOrderNumber);

            // Billing Address
            objInf.Add("x_first_name", OrderDetail.ObjBillingAddressInfo.FirstName);
            objInf.Add("x_last_name", OrderDetail.ObjBillingAddressInfo.LastName);
            objInf.Add("x_company", OrderDetail.ObjBillingAddressInfo.CompanyName);
            objInf.Add("x_email", OrderDetail.ObjBillingAddressInfo.EmailAddress);
            objInf.Add("x_address", OrderDetail.ObjBillingAddressInfo.Address);
            objInf.Add("x_city", OrderDetail.ObjBillingAddressInfo.City);
            objInf.Add("x_state", OrderDetail.ObjBillingAddressInfo.State);
            objInf.Add("x_zip", OrderDetail.ObjBillingAddressInfo.Zip);
            objInf.Add("x_country", OrderDetail.ObjBillingAddressInfo.Country);
            objInf.Add("x_phone", OrderDetail.ObjBillingAddressInfo.Phone);
            objInf.Add("x_fax", OrderDetail.ObjBillingAddressInfo.Fax);
            objInf.Add("x_email_customer", OrderDetail.ObjOrderDetails.IsEmailCustomer);

            if (OrderDetail.ObjOrderDetails.IsMultipleCheckOut == false)
            {
                //shipping address
                objInf.Add("x_ship_to_first_name", OrderDetail.ObjShippingAddressInfo.FirstName);
                objInf.Add("x_ship_to_last_name", OrderDetail.ObjShippingAddressInfo.LastName);
                objInf.Add("x_ship_to_company", OrderDetail.ObjShippingAddressInfo.CompanyName);
                objInf.Add("x_ship_to_address", OrderDetail.ObjShippingAddressInfo.Address);
                objInf.Add("x_ship_to_city", OrderDetail.ObjShippingAddressInfo.City);
                objInf.Add("x_ship_to_state", OrderDetail.ObjShippingAddressInfo.State);
                objInf.Add("x_ship_to_zip", OrderDetail.ObjShippingAddressInfo.Zip);
                objInf.Add("x_ship_to_country", OrderDetail.ObjShippingAddressInfo.Country);
            }
            // Card Details
            objInf.Add("x_card_num", OrderDetail.ObjPaymentInfo.CardNumber);
            objInf.Add("x_card_type", OrderDetail.ObjPaymentInfo.CardType);
            objInf.Add("x_exp_date", OrderDetail.ObjPaymentInfo.ExpireDate);

            var ssc = new StoreSettingConfig();
            double rate;
            string mainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, OrderDetail.ObjCommonInfo.StoreID, OrderDetail.ObjCommonInfo.PortalID, OrderDetail.ObjCommonInfo.CultureName);
            const string gateWayCurrency = "USD";
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.CustomerID = OrderDetail.ObjOrderDetails.CustomerID;
            aspxCommonObj.SessionCode = OrderDetail.ObjOrderDetails.SessionCode;
            aspxCommonObj.StoreID = OrderDetail.ObjCommonInfo.StoreID;
            aspxCommonObj.PortalID = OrderDetail.ObjCommonInfo.PortalID;

            if (gateWayCurrency.ToLower().Trim() == mainCurrency.ToLower().Trim())
            {
                rate = 1;
            }
            else
            {
                AspxCoreController acc = new AspxCoreController();
                rate = acc.GetCurrencyRateOnChange(aspxCommonObj, mainCurrency, gateWayCurrency.Trim(), "en-US");
            }
            //double amountTotal = double.Parse(HttpContext.Current.Session["GrandTotalAll"].ToString()) * rate;
            double amountTotal = double.Parse( CheckOutSessions.Get<Double>("GrandTotalAll", 0).ToString()) * rate;
            decimal amount = decimal.Parse(amountTotal.ToString(CultureInfo.InvariantCulture));

            //string amount = Regex.Replace(OrderDetail.ObjOrderDetails.GrandTotal.ToString("0.00"), @"[A-Z]", String.Empty);
            objInf.Add("x_amount", Math.Round(amount, 2).ToString(CultureInfo.InvariantCulture));
            objInf.Add("x_test_request", "False");
            if (OrderDetail.ObjPaymentInfo.PaymentMethodCode == "CC")
            {
                // Authorization code of the card (CCV)
                objInf.Add("x_card_code", OrderDetail.ObjPaymentInfo.CardCode.Trim());
                objInf.Add("x_method", OrderDetail.ObjPaymentInfo.PaymentMethodCode.Trim());
                objInf.Add("x_type", OrderDetail.ObjPaymentInfo.TransactionType.Trim());
                // string amount = Regex.Replace(OrderDetail.ObjOrderDetails.GrandTotal.ToString("0.00"), @"[A-Z]", String.Empty);
                // objInf.Add("x_amount", amount);
                objInf.Add("x_description", OrderDetail.ObjOrderDetails.Remarks.Trim());

            }
            else
            {
                //bank 
                objInf.Add("x_bank_aba_code", OrderDetail.ObjPaymentInfo.RoutingNumber.Trim());
                objInf.Add("x_bank_acct_num", OrderDetail.ObjPaymentInfo.AccountNumber.Trim());
                objInf.Add("x_bank_acct_type", OrderDetail.ObjPaymentInfo.AccountType.Trim());
                objInf.Add("x_bank_name", OrderDetail.ObjPaymentInfo.BankName.Trim());
                objInf.Add("x_bank_acct_name", OrderDetail.ObjPaymentInfo.AccountHolderName.Trim());
                objInf.Add("x_echeck_type", OrderDetail.ObjPaymentInfo.ChequeType.Trim());
                objInf.Add("x_bank_check_number", OrderDetail.ObjPaymentInfo.ChequeNumber.Trim());
            }

            // Currency setting. Check the guide for other supported currencies
            objInf.Add("x_currency_code", OrderDetail.ObjOrderDetails.CurrencyCode.Trim());

            if (OrderDetail.ObjOrderDetails.IsTest.ToLower().Trim() == "true" || OrderDetail.ObjOrderDetails.IsTest.ToLower() == "1")
            {
                objRequest.BaseAddress = "https://test.authorize.net/gateway/transact.dll";
            }
            else
            {
                objRequest.BaseAddress = "https://secure.authorize.net/gateway/transact.dll";
            }

            try
            {
                // POST request

                byte[] objRetBytes = objRequest.UploadValues(objRequest.BaseAddress, "POST", objInf);
                string[] objRetVals = System.Text.Encoding.ASCII.GetString(objRetBytes).Split(",".ToCharArray());

                // Process Return Values
                OrderDetail.ObjOrderDetails.ResponseCode = int.Parse(objRetVals[0].Trim(char.Parse("|")));
                OrderDetail.ObjOrderDetails.ResponseReasonCode = int.Parse(objRetVals[2].Trim(char.Parse("|")));

                if (objRetVals[0].Trim(char.Parse("|")) == "1")
                {
                    // Returned Authorisation Code
                    //response.AuthorizationCode = objRetVals[4].Trim(char.Parse("|"));
                    // Returned Transaction ID
                    OrderDetail.ObjOrderDetails.TransactionID = Convert.ToString(objRetVals[6].Trim(char.Parse("|")));
                    strError = "Transaction completed successfully.";

                    AspxCoreController acc = new AspxCoreController();
                    OrderDetail.ObjOrderDetails.OrderStatusID = 8;
                    OrderDetail.ObjOrderDetails.TransactionID = Convert.ToString(objRetVals[6].Trim(char.Parse("|")));
                    acc.AddOrderDetails(OrderDetail);
                    HttpContext.Current.Session["TransDetailsAIM"] = OrderDetail.ObjOrderDetails.InvoiceNumber + "#" + OrderDetail.ObjOrderDetails.TransactionID + "#" + "AIM Authorize.Net";
                    if (HttpContext.Current.Session["OrderCollection"] != null)
                    {
                      
                        OrderDetailsCollection orderdata2 = new OrderDetailsCollection();
                        if (HttpContext.Current.Session["OrderCollection"] != null)
                        {
                            orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];

                        }

                        AspxOrderController.UpdateItemQuantity(orderdata2);
                        AspxGiftCardController.IssueGiftCard(orderdata2.LstOrderItemsInfo, orderdata2.ObjOrderDetails.OrderID, true, aspxCommonObj);
                        if (orderdata2.GiftCardDetail != null && HttpContext.Current.Session["UsedGiftCard"] != null)
                        {   //updating giftcard used in chekout
                            AspxGiftCardController.UpdateGiftCardUsage(orderdata2.GiftCardDetail, orderdata2.ObjCommonInfo.StoreID,
                                                 orderdata2.ObjCommonInfo.PortalID, orderdata2.ObjOrderDetails.OrderID, orderdata2.ObjCommonInfo.AddedBy,
                                                 orderdata2.ObjCommonInfo.CultureName);
                            HttpContext.Current.Session.Remove("UsedGiftCard");
                        }


                        var tinfo = new TransactionLogInfo();
                        var tlog = new TransactionLog();

                        tinfo.TransactionID = OrderDetail.ObjOrderDetails.TransactionID;
                        tinfo.AuthCode = objRetVals[4].Trim(char.Parse("|"));//auth Code
                        tinfo.TotalAmount = OrderDetail.ObjOrderDetails.GrandTotal;
                        tinfo.ResponseCode = "1";
                        tinfo.ResponseReasonText = strError;
                        tinfo.OrderID = orderdata2.ObjOrderDetails.OrderID;
                        tinfo.StoreID = OrderDetail.ObjCommonInfo.StoreID;
                        tinfo.PortalID = OrderDetail.ObjCommonInfo.PortalID;
                        tinfo.AddedBy = OrderDetail.ObjCommonInfo.AddedBy;
                        tinfo.CustomerID = OrderDetail.ObjOrderDetails.CustomerID;
                        tinfo.SessionCode = OrderDetail.ObjOrderDetails.SessionCode;
                        tinfo.PaymentGatewayID = OrderDetail.ObjOrderDetails.PaymentGatewayTypeID;
                        tinfo.PaymentStatus = "Processed";
                        tinfo.PayerEmail = OrderDetail.ObjBillingAddressInfo.EmailAddress;
                        tinfo.CreditCard = OrderDetail.ObjPaymentInfo.CardNumber;
                        tinfo.CurrencyCode = gateWayCurrency.Trim();
                        tlog.SaveTransactionLog(tinfo);
                    }

                    var cms = new AspxCommerce.Core.CartManageSQLProvider();
                    cms.ClearCartAfterPayment(aspxCommonObj);

                    // StoreSettingConfig ssc = new StoreSettingConfig();
                    string sendEmailFrom = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, OrderDetail.ObjCommonInfo.StoreID, OrderDetail.ObjCommonInfo.PortalID, OrderDetail.ObjCommonInfo.CultureName);
                    string sendOrderNotice = ssc.GetStoreSettingsByKey(StoreSetting.SendOrderNotification, OrderDetail.ObjCommonInfo.StoreID, OrderDetail.ObjCommonInfo.PortalID, OrderDetail.ObjCommonInfo.CultureName);

                 

                    if (sendOrderNotice.ToLower() == "true")
                    {
                        try
                        {
                            EmailTemplate.SendEmailForOrder(OrderDetail.ObjCommonInfo.PortalID, OrderDetail, addressPath,
                                                            TemplateName, OrderDetail.ObjOrderDetails.TransactionID);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    acc.ClearSessionVariable("OrderCollection");
                    CheckOutHelper cHelper = new CheckOutHelper();
                    cHelper.ClearSessions();
                }
                else
                {
                    // Error!
                    strError = objRetVals[3].Trim(char.Parse("|")) + " (" +
                               objRetVals[2].Trim(char.Parse("|")) + ")";

                    if (objRetVals[2].Trim(char.Parse("|")) == "44")
                    {
                        // CCV transaction decline
                        strError += "Our Card Code Verification (CCV) returned " +
                                    "the following error: ";

                        switch (objRetVals[38].Trim(char.Parse("|")))
                        {
                            case "N":
                                strError += "Card Code does not match.";
                                break;
                            case "P":
                                strError += "Card Code was not processed.";
                                break;
                            case "S":
                                strError += "Card Code should be on card but was not indicated.";
                                break;
                            case "U":
                                strError += "Issuer was not certified for Card Code.";
                                break;
                        }
                    }

                    if (objRetVals[2].Trim(char.Parse("|")) == "45")
                    {
                        if (strError.Length > 1)
                            strError += "<br />n";

                        // AVS transaction decline
                        strError += "Our Address Verification System (AVS) " +
                                    "returned the following error: ";

                        switch (objRetVals[5].Trim(char.Parse("|")))
                        {
                            case "A":
                                strError += " the zip code entered does not match " +
                                            "the billing address.";
                                break;
                            case "B":
                                strError += " no information was provided for the AVS check.";
                                break;
                            case "E":
                                strError += " a general error occurred in the AVS system.";
                                break;
                            case "G":
                                strError += " the credit card was issued by a non-US bank.";
                                break;
                            case "N":
                                strError += " neither the entered street address nor zip " +
                                            "code matches the billing address.";
                                break;
                            case "P":
                                strError += " AVS is not applicable for this transaction.";
                                break;
                            case "R":
                                strError += " please retry the transaction; the AVS system " +
                                            "was unavailable or timed out.";
                                break;
                            case "S":
                                strError += " the AVS service is not supported by your " +
                                            "credit card issuer.";
                                break;
                            case "U":
                                strError += " address information is unavailable for the " +
                                            "credit card.";
                                break;
                            case "W":
                                strError += " the 9 digit zip code matches, but the " +
                                            "street address does not.";
                                break;
                            case "Z":
                                strError += " the zip code matches, but the address does not.";
                                break;
                        }
                    }
                    var tinfo = new TransactionLogInfo();
                    var tlog = new TransactionLog();

                    tinfo.TransactionID = "";
                    tinfo.AuthCode = objRetVals[4].Trim(char.Parse("|"));//auth Code
                    tinfo.TotalAmount = amount;
                    tinfo.ResponseCode = objRetVals[2].Trim(char.Parse("|"));
                    tinfo.ResponseReasonText = strError;
                    tinfo.OrderID = OrderDetail.ObjOrderDetails.OrderID;
                    tinfo.StoreID = OrderDetail.ObjCommonInfo.StoreID;
                    tinfo.PortalID = OrderDetail.ObjCommonInfo.PortalID;
                    tinfo.AddedBy = OrderDetail.ObjCommonInfo.AddedBy;
                    tinfo.CustomerID = OrderDetail.ObjOrderDetails.CustomerID;
                    tinfo.SessionCode = OrderDetail.ObjOrderDetails.SessionCode;
                    tinfo.PaymentGatewayID = OrderDetail.ObjOrderDetails.PaymentGatewayTypeID;
                    tinfo.PaymentStatus = "Failed";
                    tinfo.PayerEmail = OrderDetail.ObjBillingAddressInfo.EmailAddress;
                    tinfo.CreditCard = OrderDetail.ObjPaymentInfo.CardNumber;
                    tinfo.CurrencyCode = gateWayCurrency.Trim();
                    tlog.SaveTransactionLog(tinfo);
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            OrderDetail.ObjOrderDetails.ResponseReasonText = strError;
            return OrderDetail.ObjOrderDetails.ResponseReasonText;
        }
                
        public List<AuthorizeNetAIM.CardType> GetCardType(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                var paraMeter = new List<KeyValuePair<string, object>>();
                paraMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                paraMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                paraMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                var sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<AuthorizeNetAIM.CardType>("usp_Aspx_GetCardType", paraMeter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public List<AuthorizeNetAIM.TransactionType> GetTransactionType(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                var paraMeter = new List<KeyValuePair<string, object>>();
                paraMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                paraMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                paraMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                var sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<AuthorizeNetAIM.TransactionType>("usp_Aspx_GetTransactionType", paraMeter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}