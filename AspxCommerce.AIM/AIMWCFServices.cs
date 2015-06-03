using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using AspxCommerce.Core;
using System.Collections.Generic;
using AuthorizeNetAIM;
using System.Net;
using System.Text.RegularExpressions;
using SageFrame.Web.Utilities;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class AIMWCFServices
{
      [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<AIMAuthorizeSettingInfo> GetAllAuthorizedNetAIMSetting(int paymentGatewayID, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            SQLHandler sqLH = new SQLHandler();
            return sqLH.ExecuteAsList<AIMAuthorizeSettingInfo>("usp_Aspx_AIMAuthorizeNETSettingsGetAll", parameterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
      public string SendPaymentInfoAIM(OrderDetailsCollection OrderDetail, string TemplateName, string addressPath)
    {
        WebClient objRequest = new WebClient();
        System.Collections.Specialized.NameValueCollection objInf = new System.Collections.Specialized.NameValueCollection(30);

        string strError;

        //OrderDetail.ObjOrderDetails.CustomerID = int.Parse(Crypto.GenerateCustomerID());
        OrderDetail.ObjOrderDetails.InvoiceNumber = Crypto.GenerateInvoiceNumber();
        OrderDetail.ObjOrderDetails.PurchaseOrderNumber = Crypto.GeneratePurchaseOrderNumber();

        //merchant generated field
        objInf.Add("x_version", OrderDetail.ObjOrderDetails.Version);
        objInf.Add("x_delim_data", OrderDetail.ObjOrderDetails.DelimData);
        objInf.Add("x_login", OrderDetail.ObjOrderDetails.APILogin);
        objInf.Add("x_tran_key", OrderDetail.ObjOrderDetails.TransactionKey);
        objInf.Add("x_relay_response", OrderDetail.ObjOrderDetails.RelayResponse);
        objInf.Add("x_delim_char", OrderDetail.ObjOrderDetails.DelimChar);
        objInf.Add("x_encap_char", OrderDetail.ObjOrderDetails.EncapeChar);
        objInf.Add("x_invoice_num", OrderDetail.ObjOrderDetails.InvoiceNumber);
        objInf.Add("x_cust_id", OrderDetail.ObjOrderDetails.CustomerID.ToString());
        objInf.Add("x_po_num", OrderDetail.ObjOrderDetails.PurchaseOrderNumber);
        //for (int i = 0; i < arr; i++)
        //{

        //}

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
        if (OrderDetail.ObjPaymentInfo.PaymentMethodCode=="CC")
        {
            // Authorization code of the card (CCV)
            objInf.Add("x_card_code", OrderDetail.ObjPaymentInfo.CardCode.ToString());
            objInf.Add("x_method", OrderDetail.ObjPaymentInfo.PaymentMethodCode);
            objInf.Add("x_type", OrderDetail.ObjPaymentInfo.TransactionType);
            string amount = Regex.Replace(OrderDetail.ObjOrderDetails.GrandTotal.ToString("0.00"), @"[A-Z]", String.Empty);
            objInf.Add("x_amount", amount);
            objInf.Add("x_description", OrderDetail.ObjOrderDetails.Remarks);
            objInf.Add("x_test_request", OrderDetail.ObjOrderDetails.IsTest);
        }
        else
        {


            //bank 
            objInf.Add("x_bank_aba_code", OrderDetail.ObjPaymentInfo.RoutingNumber);
            objInf.Add("x_bank_acct_num", OrderDetail.ObjPaymentInfo.AccountNumber);
            objInf.Add("x_bank_acct_type", OrderDetail.ObjPaymentInfo.AccountType);
            objInf.Add("x_bank_name", OrderDetail.ObjPaymentInfo.BankName);
            objInf.Add("x_bank_acct_name", OrderDetail.ObjPaymentInfo.AccountHolderName);
            objInf.Add("x_echeck_type", OrderDetail.ObjPaymentInfo.ChequeType);
            objInf.Add("x_bank_check_number", OrderDetail.ObjPaymentInfo.ChequeNumber);
        }

        // Currency setting. Check the guide for other supported currencies
        objInf.Add("x_currency_code", OrderDetail.ObjOrderDetails.CurrencyCode);
        objRequest.BaseAddress ="https://test.authorize.net/gateway/transact.dll";

        try
        {
            // POST request
            byte[] objRetBytes;
            string[] objRetVals;

            objRetBytes =
                objRequest.UploadValues(objRequest.BaseAddress, "POST", objInf);
            objRetVals =
                System.Text.Encoding.ASCII.GetString(objRetBytes).Split(",".ToCharArray());

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
               // AspxCommerceWebService asws = new AspxCommerceWebService();
                AspxCoreController asws = new AspxCoreController();
                OrderDetail.ObjOrderDetails.OrderStatusID = 8;
                OrderDetail.ObjOrderDetails.TransactionID = Convert.ToString(objRetVals[6].Trim(char.Parse("|")));
                asws.AddOrderDetails(OrderDetail);
                CartManageSQLProvider cms = new CartManageSQLProvider();
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.CustomerID = OrderDetail.ObjOrderDetails.CustomerID;
                aspxCommonObj.SessionCode = OrderDetail.ObjOrderDetails.SessionCode;
                aspxCommonObj.StoreID = OrderDetail.ObjCommonInfo.StoreID;
                aspxCommonObj.PortalID = OrderDetail.ObjCommonInfo.PortalID;
                cms.ClearCartAfterPayment(aspxCommonObj);

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
            }
        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        OrderDetail.ObjOrderDetails.ResponseReasonText = strError;       
        return OrderDetail.ObjOrderDetails.ResponseReasonText;
    }

  

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<AuthorizeNetAIM.CardType> GetCardType(int _storeID, int _portalID, string _cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", _storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", _portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", _cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<AuthorizeNetAIM.CardType>("usp_Aspx_GetCardType", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<AuthorizeNetAIM.TransactionType> GetTransactionType(int _storeID, int _portalID, string _cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", _storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", _portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", _cultureName));           
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<AuthorizeNetAIM.TransactionType>("usp_Aspx_GetTransactionType", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
