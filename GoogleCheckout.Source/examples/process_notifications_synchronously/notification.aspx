<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="GCheckout" %>
<%@ Import Namespace="GCheckout.AutoGen" %>
<%@ Import Namespace="GCheckout.Util" %>
<script runat="server" language="c#">

  void Page_Load(Object sender, EventArgs e) {
    // Extract the XML from the request.
    Stream RequestStream = Request.InputStream;
    StreamReader RequestStreamReader = new StreamReader(RequestStream);
    string RequestXml = RequestStreamReader.ReadToEnd();
    RequestStream.Close();
    // Act on the XML.
    switch (EncodeHelper.GetTopElement(RequestXml)) {
      case "new-order-notification":
        NewOrderNotification N1 = (NewOrderNotification) EncodeHelper.Deserialize(RequestXml, typeof(NewOrderNotification));
        string OrderNumber1 = N1.googleordernumber;
        string ShipToName = N1.buyershippingaddress.contactname;
        string ShipToAddress1 = N1.buyershippingaddress.address1;
        string ShipToAddress2 = N1.buyershippingaddress.address2;
        string ShipToCity = N1.buyershippingaddress.city;
        string ShipToState = N1.buyershippingaddress.region;
        string ShipToZip = N1.buyershippingaddress.postalcode;
        foreach (Item ThisItem in N1.shoppingcart.items) {
          string Name = ThisItem.itemname;
          int Quantity = ThisItem.quantity;
          decimal Price = ThisItem.unitprice.Value;
        }
        break;
      case "risk-information-notification":
        RiskInformationNotification N2 = (RiskInformationNotification) EncodeHelper.Deserialize(RequestXml, typeof(RiskInformationNotification));
        // This notification tells us that Google has authorized the order and it has passed the fraud check.
        // Use the data below to determine if you want to accept the order, then start processing it.
        string OrderNumber2 = N2.googleordernumber;
        string AVS = N2.riskinformation.avsresponse;
        string CVN = N2.riskinformation.cvnresponse;
        bool SellerProtection = N2.riskinformation.eligibleforprotection;
        break;
      case "order-state-change-notification":
        OrderStateChangeNotification N3 = (OrderStateChangeNotification) EncodeHelper.Deserialize(RequestXml, typeof(OrderStateChangeNotification));
        // The order has changed either financial or fulfillment state in Google's system.
        string OrderNumber3 = N3.googleordernumber;
        string NewFinanceState = N3.newfinancialorderstate.ToString();
        string NewFulfillmentState = N3.newfulfillmentorderstate.ToString();
        string PrevFinanceState = N3.previousfinancialorderstate.ToString();
        string PrevFulfillmentState = N3.previousfulfillmentorderstate.ToString();
        break;
      case "charge-amount-notification":
        ChargeAmountNotification N4 = (ChargeAmountNotification) EncodeHelper.Deserialize(RequestXml, typeof(ChargeAmountNotification));
        // Google has successfully charged the customer's credit card.
        string OrderNumber4 = N4.googleordernumber;
        decimal ChargedAmount = N4.latestchargeamount.Value;
        break;
      case "refund-amount-notification":
        RefundAmountNotification N5 = (RefundAmountNotification) EncodeHelper.Deserialize(RequestXml, typeof(RefundAmountNotification));
        // Google has successfully refunded the customer's credit card.
        string OrderNumber5 = N5.googleordernumber;
        decimal RefundedAmount = N5.latestrefundamount.Value;
        break;
      case "chargeback-amount-notification":
        ChargebackAmountNotification N6 = (ChargebackAmountNotification) EncodeHelper.Deserialize(RequestXml, typeof(ChargebackAmountNotification));
        // A customer initiated a chargeback with his credit card company to get her money back.
        string OrderNumber6 = N6.googleordernumber;
        decimal ChargebackAmount = N6.latestchargebackamount.Value;
        break;
      default:
        break;
    }
  }

</script>
<?xml version="1.0" encoding="UTF-8"?>
<notification-acknowledgment xmlns="http://checkout.google.com/schema/2"/>
