using System;
using System.Collections.Generic;
using GCheckout.OrderProcessing;
using AspxCommerce.Core;
using System.Xml;
using AspxCommerce.GoogleCheckOut;
using System.Web;

///<summary>

/// Google Checkout helper for doing the process

///</summary>

public class GoogleCheckoutHelper
{
    public static int orderID;
    public static string userName;
    public static decimal totalAmount;
    public static int portalID;
    public static int customerID;
    public static int storeID;
    public string aspxPaymentModulePath;
    public string cultureName;
    public static string sessionCode = string.Empty;
    public int GoogleCheckOut;
    public static string Spath, itemIds, couponCode, strpgID, paymentStatus, transID, MerchantID, MerchantKey, Environment;
    public static int pgID;
    public static string selectedCurrency = string.Empty;

    private static void HandleAuthorizationAmountNotification(GCheckout.AutoGen.AuthorizationAmountNotification inputAuthorizationAmountNotification)
    {
        // TODO: Add custom processing for this notification type
    }

    private static void HandleChargeAmountNotification(GCheckout.AutoGen.ChargeAmountNotification inputChargeAmountNotification)
    {
        // TODO: Add custom processing for this notification type
    }

    private static void HandleNewOrderNotification(GCheckout.AutoGen.NewOrderNotification inputNewOrderNotification)
    {


        try
        {
            if (inputNewOrderNotification.shoppingcart.merchantprivatedata != null && inputNewOrderNotification.shoppingcart.merchantprivatedata.Any != null && inputNewOrderNotification.shoppingcart.merchantprivatedata.Any.Length > 0)
            {
                //Retrieve data from MerchantPrivateData
                GCheckout.AutoGen.anyMultiple oneAnyMultiple = inputNewOrderNotification.shoppingcart.merchantprivatedata;
                System.Xml.XmlNode[] oneXmlNodeArray = oneAnyMultiple.Any;
                foreach (XmlNode xn in oneXmlNodeArray)
                {


                    if (xn.Name == "OrderID")
                    {
                        orderID = Int32.Parse(xn.InnerText.ToString());
                    }

                    if (xn.Name == "userName")
                    {
                        userName = xn.InnerText.ToString();
                    }

                    if (xn.Name == "amount")
                    {
                        totalAmount = decimal.Parse(xn.InnerText.ToString());
                    }

                    if (xn.Name == "selectedCurrency")
                    {
                        selectedCurrency = xn.InnerText.ToString();
                    }

                    if (xn.Name == "portalID")
                    {
                        portalID = int.Parse(xn.InnerText.ToString());
                    }
                    if (xn.Name == "customerID")
                    {
                        customerID = int.Parse(xn.InnerText.ToString());
                    }
                    if (xn.Name == "itemIds")
                    {
                        itemIds = xn.InnerText.ToString();
                    }
                    if (xn.Name == "storeID")
                    {
                        storeID = int.Parse(xn.InnerText.ToString());
                    }
                    if (xn.Name == "couponCode")
                    {
                        couponCode = xn.InnerText.ToString();
                    }
                    if (xn.Name == "sessionCode")
                    {
                        sessionCode = xn.InnerText.ToString();
                    }
                    if (xn.Name == "pgID")
                    {
                        pgID = int.Parse(xn.InnerText.ToString());
                    }
                    if (xn.Name == "MerchantID")
                    {
                        MerchantID = xn.InnerText.ToString();
                    }
                    if (xn.Name == "MerchantKey")
                    {
                        MerchantID = xn.InnerText.ToString();
                    }
                }
                paymentStatus = GCNotificationStatus.Succeeded.ToString();
                transID = inputNewOrderNotification.googleordernumber;

                if (paymentStatus == "Succeeded")
                {
                    TransactionLogInfo tinfo = new TransactionLogInfo();
                    TransactionLog Tlog = new TransactionLog();
                    OrderDetailsCollection odc = new OrderDetailsCollection();
                   
                    tinfo.ResponseReasonText = "Succeeded";
                    tinfo.OrderID = orderID;
                    tinfo.StoreID = storeID;
                    tinfo.PortalID = portalID;
                    tinfo.AddedBy = userName;
                    tinfo.CustomerID = customerID;
                    tinfo.SessionCode = sessionCode;
                    tinfo.PaymentGatewayID = pgID;
                    tinfo.PaymentStatus = paymentStatus;
                    tinfo.PayerEmail = "";
                    tinfo.CreditCard = "";
                    tinfo.TotalAmount = totalAmount;
                    tinfo.TransactionID = transID;
                    tinfo.RecieverEmail = "";
                    tinfo.CurrencyCode = selectedCurrency;
                    Tlog.SaveTransactionLog(tinfo);

                    GoogleCheckOutHandler.ParseIPN(orderID, transID, paymentStatus, storeID, portalID, userName, customerID, sessionCode);
                    GoogleCheckOutHandler.UpdateItemQuantity(itemIds, couponCode, storeID, portalID, userName);
                    CartManageSQLProvider cms = new CartManageSQLProvider();
                    AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                    aspxCommonObj.CustomerID = customerID;
                    aspxCommonObj.SessionCode = sessionCode;
                    aspxCommonObj.StoreID = storeID;
                    aspxCommonObj.PortalID = portalID;
                    aspxCommonObj.CultureName = null;
                    aspxCommonObj.UserName = null;                    
                    cms.ClearCartAfterPayment(aspxCommonObj);

                   
                   

                }

            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
        
        // TODO: Process the MerchantPrivateData if provided

        foreach (GCheckout.AutoGen.Item oneItem in inputNewOrderNotification.shoppingcart.items)
        {
            // TODO: Get MerchantItemId from shopping cart item (oneItem.merchantitemid) and process it
        }

        // TODO: Add custom processing for this notification type
    }

    private static void HandleOrderStateChangeNotification(GCheckout.AutoGen.OrderStateChangeNotification inputOrderStateChangeNotification)
    {
        // Charge Order If Chargeable
        if ((inputOrderStateChangeNotification.previousfinancialorderstate.ToString().Equals("REVIEWING")) && (inputOrderStateChangeNotification.newfinancialorderstate.ToString().Equals("CHARGEABLE")))
        {
            GCheckout.OrderProcessing.ChargeOrderRequest oneChargeOrderRequest = new GCheckout.OrderProcessing.ChargeOrderRequest(inputOrderStateChangeNotification.googleordernumber);
            GCheckout.Util.GCheckoutResponse oneGCheckoutResponse = oneChargeOrderRequest.Send();
        }

        // Update License If Charged
        if ((inputOrderStateChangeNotification.previousfinancialorderstate.ToString().Equals("CHARGING")) && (inputOrderStateChangeNotification.newfinancialorderstate.ToString().Equals("CHARGED")))
        {
            // TODO: For each shopping cart item received in the NewOrderNotification, authorize the license
        }

        // TODO: Add custom processing for this notification type
    }

    private static void HandleRiskInformationNotification(GCheckout.AutoGen.RiskInformationNotification inputRiskInformationNotification)
    {
        // TODO: Add custom processing for this notification type
    }

    //private GoogleCheckOutSettingInfo GetSettingGooglePayment()
    //{
    //    GoogleCheckOutSettingInfo setting = new GoogleCheckOutSettingInfo();
    //    GoogleCheckOutWCFService ser = new GoogleCheckOutWCFService();

    //}
    public static void ProcessNotification(string serialNumber)
    {
        GoogleCheckOutWCFService pw = new GoogleCheckOutWCFService();
        List<GoogleCheckOutSettingKeyInfo> sf;
        sf = pw.GoogleCheckOutSettingKey();
        MerchantID = sf[0].GoogleMerchantID;
        MerchantKey = sf[0].GoogleMerchantKey;
        Environment = sf[0].GoogleEnvironmentType;

        GCheckout.OrderProcessing.NotificationHistorySerialNumber sn = new NotificationHistorySerialNumber(serialNumber);
        GCheckout.OrderProcessing.NotificationHistoryRequest oneNotificationHistoryRequest = new NotificationHistoryRequest(MerchantID, MerchantKey, Environment, sn); //newNotificationHistorySerialNumber(serialNumber)
        //Response
        NotificationHistoryResponse oneNotificationHistoryResponse = (NotificationHistoryResponse)oneNotificationHistoryRequest.Send();

//Get type of notification
object notification = GCheckout.Util.EncodeHelper.Deserialize(oneNotificationHistoryResponse.ResponseXml);

//Check for the notification and call functions according to that
if (notification.GetType().Equals(typeof(GCheckout.AutoGen.NewOrderNotification)))

{

GCheckout.AutoGen.NewOrderNotification oneNewOrderNotification = (GCheckout.AutoGen.NewOrderNotification)notification;

if (oneNewOrderNotification.serialnumber.Equals(serialNumber))

{

HandleNewOrderNotification(oneNewOrderNotification);

}

}

else if (notification.GetType().Equals(typeof(GCheckout.AutoGen.OrderStateChangeNotification)))

{

GCheckout.AutoGen.OrderStateChangeNotification oneOrderStateChangeNotification = (GCheckout.AutoGen.OrderStateChangeNotification)notification;

if (oneOrderStateChangeNotification.serialnumber.Equals(serialNumber))

{

HandleOrderStateChangeNotification(oneOrderStateChangeNotification);

}

}

else if (notification.GetType().Equals(typeof(GCheckout.AutoGen.RiskInformationNotification)))

{

GCheckout.AutoGen.RiskInformationNotification oneRiskInformationNotification = (GCheckout.AutoGen.RiskInformationNotification)notification;

if (oneRiskInformationNotification.serialnumber.Equals(serialNumber))

{

HandleRiskInformationNotification(oneRiskInformationNotification);

}

}

else if (notification.GetType().Equals(typeof(GCheckout.AutoGen.AuthorizationAmountNotification)))

{

GCheckout.AutoGen.AuthorizationAmountNotification oneAuthorizationAmountNotification = (GCheckout.AutoGen.AuthorizationAmountNotification)notification;

if (oneAuthorizationAmountNotification.serialnumber.Equals(serialNumber))

{

HandleAuthorizationAmountNotification(oneAuthorizationAmountNotification);

}

}

else if (notification.GetType().Equals(typeof(GCheckout.AutoGen.ChargeAmountNotification)))

{

GCheckout.AutoGen.ChargeAmountNotification oneChargeAmountNotification = (GCheckout.AutoGen.ChargeAmountNotification)notification;

if (oneChargeAmountNotification.serialnumber.Equals(serialNumber))

{

HandleChargeAmountNotification(oneChargeAmountNotification);

}

}

else

{

string exceptionText = "Unhandled Type [" + notification.GetType().ToString() + "]!; serialNumber=[" + serialNumber + "];";

throw new ArgumentOutOfRangeException(exceptionText);

}

}

}