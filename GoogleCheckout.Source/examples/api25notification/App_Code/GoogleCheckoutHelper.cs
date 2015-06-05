using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GCheckout.OrderProcessing;

//original source from http://www.capprime.com/software_development_weblog/2010/11/29/UsingTheGoogleCheckout25APIWithASPNETMVCTheMissingSample.aspx

/// <summary>
/// Summary description for GoogleCheckoutHelper
/// </summary>
public class GoogleCheckoutHelper {

  private static void HandleAuthorizationAmountNotification(GCheckout.AutoGen.AuthorizationAmountNotification inputAuthorizationAmountNotification) {
    // TODO: Add custom processing for this notification type
  }

  private static void HandleChargeAmountNotification(GCheckout.AutoGen.ChargeAmountNotification inputChargeAmountNotification) {
    // TODO: Add custom processing for this notification type
  }

  private static void HandleNewOrderNotification(GCheckout.AutoGen.NewOrderNotification inputNewOrderNotification) {
    // Retrieve data from MerchantPrivateData
    GCheckout.AutoGen.anyMultiple oneAnyMultiple = inputNewOrderNotification.shoppingcart.merchantprivatedata;
    System.Xml.XmlNode[] oneXmlNodeArray = oneAnyMultiple.Any;
    string hiddenMerchantPrivateData = oneXmlNodeArray[0].InnerText;
    // TODO: Process the MerchantPrivateData if provided

    foreach (GCheckout.AutoGen.Item oneItem in inputNewOrderNotification.shoppingcart.items) {
      // TODO: Get MerchantItemId from shopping cart item (oneItem.merchantitemid) and process it
    }

    // TODO: Add custom processing for this notification type
  }

  private static void HandleOrderStateChangeNotification(GCheckout.AutoGen.OrderStateChangeNotification notification) {
    // Charge Order If Chargeable
    if ((notification.previousfinancialorderstate == GCheckout.AutoGen.FinancialOrderState.REVIEWING) 
      && (notification.newfinancialorderstate == GCheckout.AutoGen.FinancialOrderState.CHARGEABLE)) {
      
      ChargeOrderRequest oneChargeOrderRequest = new ChargeOrderRequest(notification.googleordernumber);
      GCheckout.Util.GCheckoutResponse oneGCheckoutResponse = oneChargeOrderRequest.Send();
    }

    // Update License If Charged
    if ((notification.previousfinancialorderstate == GCheckout.AutoGen.FinancialOrderState.CHARGING) 
      && (notification.newfinancialorderstate == GCheckout.AutoGen.FinancialOrderState.CHARGED)) {
      // TODO: For each shopping cart item received in the NewOrderNotification, authorize the license
    }

    // TODO: Add custom processing for this notification type
  }

  private static void HandleRiskInformationNotification(GCheckout.AutoGen.RiskInformationNotification notification) {
    // TODO: Add custom processing for this notification type
  }

  public static void ProcessNotification(string serialNumber) {

    //The next two statements set up a request and call google checkout for the details based on that serial number.

    NotificationHistoryRequest oneNotificationHistoryRequest
      = new NotificationHistoryRequest(new NotificationHistorySerialNumber(serialNumber));

    NotificationHistoryResponse oneNotificationHistoryResponse
      = (NotificationHistoryResponse)oneNotificationHistoryRequest.Send();

    // oneNotificationHistoryResponse.ResponseXml contains the complete response

    //what you need to do now is process the data that was returned.

    // Iterate through the notification history for this order looking for the notification that exactly matches the given serial number
    foreach (object oneNotification in oneNotificationHistoryResponse.NotificationResponses) {
      if (oneNotification.GetType().Equals(typeof(GCheckout.AutoGen.NewOrderNotification))) {
        GCheckout.AutoGen.NewOrderNotification oneNewOrderNotification = (GCheckout.AutoGen.NewOrderNotification)oneNotification;
        if (oneNewOrderNotification.serialnumber.Equals(serialNumber)) {
          HandleNewOrderNotification(oneNewOrderNotification);
        }
      }
      else if (oneNotification.GetType().Equals(typeof(GCheckout.AutoGen.OrderStateChangeNotification))) {
        GCheckout.AutoGen.OrderStateChangeNotification oneOrderStateChangeNotification = (GCheckout.AutoGen.OrderStateChangeNotification)oneNotification;
        if (oneOrderStateChangeNotification.serialnumber.Equals(serialNumber)) {
          HandleOrderStateChangeNotification(oneOrderStateChangeNotification);
        }
      }
      else if (oneNotification.GetType().Equals(typeof(GCheckout.AutoGen.RiskInformationNotification))) {
        GCheckout.AutoGen.RiskInformationNotification oneRiskInformationNotification = (GCheckout.AutoGen.RiskInformationNotification)oneNotification;
        if (oneRiskInformationNotification.serialnumber.Equals(serialNumber)) {
          HandleRiskInformationNotification(oneRiskInformationNotification);
        }
      }
      else if (oneNotification.GetType().Equals(typeof(GCheckout.AutoGen.AuthorizationAmountNotification))) {
        GCheckout.AutoGen.AuthorizationAmountNotification oneAuthorizationAmountNotification = (GCheckout.AutoGen.AuthorizationAmountNotification)oneNotification;
        if (oneAuthorizationAmountNotification.serialnumber.Equals(serialNumber)) {
          HandleAuthorizationAmountNotification(oneAuthorizationAmountNotification);
        }
      }
      else if (oneNotification.GetType().Equals(typeof(GCheckout.AutoGen.ChargeAmountNotification))) {
        GCheckout.AutoGen.ChargeAmountNotification oneChargeAmountNotification = (GCheckout.AutoGen.ChargeAmountNotification)oneNotification;
        if (oneChargeAmountNotification.serialnumber.Equals(serialNumber)) {
          HandleChargeAmountNotification(oneChargeAmountNotification);
        }
      }
      else {
        throw new ArgumentOutOfRangeException("Unhandled Type [" + oneNotification.GetType().ToString() + "]!; serialNumber=[" + serialNumber + "];");
      }
    }
  }
}
